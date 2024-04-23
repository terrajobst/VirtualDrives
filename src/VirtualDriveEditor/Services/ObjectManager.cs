using System.Runtime.InteropServices;

namespace VirtualDrives.Services;

unsafe static class ObjectManager
{
    public static IEnumerable<(string Name, string Type)> EnumerateObjects(string directoryPath)
    {
        var objectName = new UNICODE_STRING();
        nint buffer = 0;

        try
        {
            var result = new List<(string Name, string Type)>();

            // Create a UNICODE_STRING for the object name
            objectName.Buffer = Marshal.StringToHGlobalUni(directoryPath);
            objectName.Length = (ushort)(2 * directoryPath.Length);
            objectName.MaximumLength = (ushort)(2 * 260);

            // Initialize the OBJECT_ATTRIBUTES structure
            OBJECT_ATTRIBUTES oa;
            InitializeObjectAttributes(out oa, &objectName, 0, nint.Zero, nint.Zero);

            // Open the object manager directory
            nint directoryHandle;
            uint status = NtOpenDirectoryObject(out directoryHandle, 0x0002 | 0x0001, ref oa); // DIRECTORY_QUERY access

            if (status != 0)
                return Array.Empty<(string, string)>();

            // Allocate a buffer for the directory entries
            var bufferSize = 4096;
            buffer = Marshal.AllocHGlobal(bufferSize);
            var bufferCount = bufferSize / Marshal.SizeOf<OBJECT_DIRECTORY_INFORMATION>();
            uint context = 0;
            uint returnLength;

            // Enumerate the objects
            while (NtQueryDirectoryObject(directoryHandle, buffer, 4096, true, false, ref context, out returnLength) == 0)
            {
                var current = buffer;

                while (true)
                {
                    // Cast the buffer to our structure
                    var dirInfo = Marshal.PtrToStructure<OBJECT_DIRECTORY_INFORMATION>(current);

                    if (dirInfo.Name.Buffer != 0)
                    {
                        // Get the name and type
                        string name = Marshal.PtrToStringUni(dirInfo.Name.Buffer, dirInfo.Name.Length / 2);
                        string type = Marshal.PtrToStringUni(dirInfo.TypeName.Buffer, dirInfo.TypeName.Length / 2);
                        if (name.Contains(":"))
                            result.Add((name, type));
                    }
                    else
                    {
                        break;
                    }

                    // Move to the next entry
                    current = nint.Add(current, Marshal.SizeOf<OBJECT_DIRECTORY_INFORMATION>());
                }
            }

            return result;
        }
        finally
        {
            if (objectName.Buffer != 0)
                Marshal.FreeHGlobal(objectName.Buffer);

            if (buffer != 0)
                Marshal.FreeHGlobal(buffer);
        }
    }

    public static string? ResolveSymbolLink(string qualifiedName)
    {
        var objectName = new UNICODE_STRING();
        var linkTarget = new UNICODE_STRING();

        try
        {
            // Create a UNICODE_STRING for the object name
            objectName.Buffer = Marshal.StringToHGlobalUni(qualifiedName);
            objectName.Length = (ushort)(2 * qualifiedName.Length);
            objectName.MaximumLength = (ushort)(2 * 260);

            // Initialize the OBJECT_ATTRIBUTES structure
            OBJECT_ATTRIBUTES oa;
            InitializeObjectAttributes(out oa, &objectName, 0, nint.Zero, nint.Zero);

            nint linkHandle;
            uint status = NtOpenSymbolicLinkObject(out linkHandle, 0x80000000, ref oa);
            if (status != 0)
                return null;

            linkTarget.Buffer = Marshal.AllocHGlobal(2 * 260);
            linkTarget.Length = 0;
            linkTarget.MaximumLength = 2 * 260;

            if (NtQuerySymbolicLinkObject(linkHandle, &linkTarget, out var linkTargetLength) != 0)
                return null;

            return Marshal.PtrToStringUni(linkTarget.Buffer, linkTarget.Length / Marshal.SystemDefaultCharSize);
        }
        finally
        {
            if (objectName.Buffer != 0)
                Marshal.FreeHGlobal(objectName.Buffer);

            if (linkTarget.Buffer != 0)
                Marshal.FreeHGlobal(linkTarget.Buffer);
        }
    }

    // Define the UNICODE_STRING structure
    [StructLayout(LayoutKind.Sequential)]
    public struct UNICODE_STRING
    {
        public ushort Length;
        public ushort MaximumLength;
        public nint Buffer;
    }

    // Define the OBJECT_DIRECTORY_INFORMATION structure
    [StructLayout(LayoutKind.Sequential)]
    public struct OBJECT_DIRECTORY_INFORMATION
    {
        public UNICODE_STRING Name;
        public UNICODE_STRING TypeName;
    }

    // Import NtQueryDirectoryObject from ntdll.dll
    [DllImport("ntdll.dll")]
    public static extern uint NtQueryDirectoryObject(
        nint DirectoryHandle,
        nint Buffer,
        int Length,
        bool ReturnSingleEntry,
        bool RestartScan,
        ref uint Context,
        out uint ReturnLength);

    [DllImport("ntdll.dll")]
    public static extern uint NtOpenDirectoryObject(
        out nint DirectoryHandle,
        uint DesiredAccess,
        ref OBJECT_ATTRIBUTES ObjectAttributes);

    [DllImport("ntdll.dll")]
    public static extern uint NtOpenSymbolicLinkObject(
        out nint LinkHandle,
        uint DesiredAccess,
        ref OBJECT_ATTRIBUTES ObjectAttributes);

    [DllImport("ntdll.dll")]
    public static extern uint NtQuerySymbolicLinkObject(
        nint LinkHandle,
        UNICODE_STRING* LinkTarget,
        out uint ReturnedLength
    );

    // Define the OBJECT_ATTRIBUTES structure
    [StructLayout(LayoutKind.Sequential)]
    public struct OBJECT_ATTRIBUTES
    {
        public int Length;
        public nint RootDirectory;
        public UNICODE_STRING* ObjectName;
        public uint Attributes;
        public nint SecurityDescriptor;
        public nint SecurityQualityOfService;
    }

    // Define the InitializeObjectAttributes macro
    public static void InitializeObjectAttributes(
        out OBJECT_ATTRIBUTES p,
        UNICODE_STRING* n,
        uint a,
        nint r,
        nint s)
    {
        p.Length = Marshal.SizeOf(typeof(OBJECT_ATTRIBUTES));
        p.RootDirectory = r;
        p.Attributes = a;
        p.ObjectName = n;
        p.SecurityDescriptor = s;
        p.SecurityQualityOfService = nint.Zero;
    }
}
