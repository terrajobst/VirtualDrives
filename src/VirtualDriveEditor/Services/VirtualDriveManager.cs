using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Win32;

namespace VirtualDrives.Services;

internal static class VirtualDriveManager
{
    private const string KeyPath = "SYSTEM\\CurrentControlSet\\Control\\Session Manager\\DOS Devices";
    private const string ObjectManagerGlobalDirectory = "\\GLOBAL??";
    private const string ObjectManagerFileSystemPrefix = @"\??\";

    public static IReadOnlyList<VirtualDrive> GetDrives()
    {
        var result = new List<VirtualDrive>();

        using var key = Registry.LocalMachine.OpenSubKey(KeyPath, RegistryKeyPermissionCheck.ReadSubTree);

        if (key is not null)
        {
            foreach (var name in key.GetValueNames())
            {
                var letter = GetDriveLetter(name);
                if (letter is not null)
                {
                    var objectManagerPath = Convert.ToString(key.GetValue(name));
                    if (TryStripRootPrefix(objectManagerPath, out var path))
                    {
                        var drive = new VirtualDrive(letter.Value, path);
                        result.Add(drive);
                    }
                }
            }
        }

        return result.ToArray();
    }

    private static IReadOnlyList<(char Letter, string Target, bool IsVirtual)> GetActiveDrives()
    {
        var result = new List<(char Letter, string Target, bool IsVirtual)>();

        foreach (var (name, type) in ObjectManager.EnumerateObjects(ObjectManagerGlobalDirectory))
        {
            var letter = GetDriveLetter(name);
            if (letter is null)
                continue;

            var qualifiedName = Path.Join(ObjectManagerGlobalDirectory, name);
            var resolvedTarget = ObjectManager.ResolveSymbolLink(qualifiedName);
            if (resolvedTarget is null)
                continue;

            var isVirtual = resolvedTarget.StartsWith(ObjectManagerFileSystemPrefix, StringComparison.Ordinal);
            result.Add((letter.Value, resolvedTarget, isVirtual));
        }

        return result.ToArray();
    }

    public static ISet<char> GetAvailableLetters()
    {
        var set = new SortedSet<char>();

        for (var c = 'A'; c <= 'Z'; c++)
            set.Add(c);

        foreach (var (letter, _, isVirtual) in GetActiveDrives())
        {
            if (!isVirtual)
                set.Remove(letter);
        }

        foreach (var virtualDrive in GetDrives())
            set.Remove(virtualDrive.Letter);

        return set;
    }

    public static bool CheckForCycle(char letter, string path, [MaybeNullWhen(false)] out string cycle)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);

        var drives = GetDrives().ToDictionary(d => d.Letter, d => d.Path);
        drives[letter] = path;

        var ancestors = new List<char>();
        if (HasCycle(letter, drives, ancestors))
        {
            cycle = string.Join(" -> ", ancestors);
            return true;
        }

        cycle = null;
        return false;

        static bool HasCycle(char letter, IReadOnlyDictionary<char, string> drives, List<char> ancestors)
        {
            if (!drives.TryGetValue(letter, out var path))
                return false;

            var pathLetter = GetDriveLetter(path);
            if (pathLetter is null)
                return false;

            var cycle = ancestors.Contains(letter);
            ancestors.Add(letter);

            if (cycle)
                return true;

            return HasCycle(pathLetter.Value, drives, ancestors);
        }
    }

    public static void SetDrivePath(char letter, string path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);

        using var key = Registry.LocalMachine.OpenSubKey(KeyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
        Debug.Assert(key is not null);

        var keyName = $"{letter}:";
        path = EnsureHasRootPrefix(path);

        key.SetValue(keyName, path);
    }

    public static void DeleteDrive(char letter)
    {
        using var key = Registry.LocalMachine.OpenSubKey(KeyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
        Debug.Assert(key is not null);

        var name = $"{letter}:";
        key.DeleteValue(name);
    }

    public static bool HasPendingChanges()
    {
        var activeVirtualDrives = new HashSet<(char Letter, string Path)>();

        foreach (var (letter, target, isVirtual) in GetActiveDrives())
        {
            if (isVirtual && TryStripRootPrefix(target, out var path))
                activeVirtualDrives.Add((letter, path));
        }

        var configuredDrives = GetDrives().Select(d => (d.Letter, d.Path)).ToHashSet();
        var identical = activeVirtualDrives.SetEquals(configuredDrives);

        return !identical;
    }

    private static char? GetDriveLetter(string driveName)
    {
        var indexOfColon = driveName.IndexOf(':');
        if (indexOfColon != 1)
            return null;

        var letter = char.ToUpper(driveName[0]);
        if (letter < 'A' || letter > 'Z')
            return null;

        return letter;
    }

    private static string EnsureHasRootPrefix(string path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);

        if (!path.StartsWith(ObjectManagerFileSystemPrefix, StringComparison.Ordinal))
            path = $"{ObjectManagerFileSystemPrefix}{path}";

        return path;
    }

    private static bool TryStripRootPrefix(string? path, [MaybeNullWhen(false)] out string result)
    {
        if (path is not null && path.StartsWith(ObjectManagerFileSystemPrefix, StringComparison.Ordinal))
        {
            result = path.Substring(ObjectManagerFileSystemPrefix.Length);
            return true;
        }

        result = null;
        return false;
    }
}