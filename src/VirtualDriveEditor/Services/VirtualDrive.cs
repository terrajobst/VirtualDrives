namespace VirtualDrives.Services;

internal sealed class VirtualDrive
{
    public VirtualDrive(char letter, string path)
    {
        Letter = letter;
        Name = $"{char.ToUpper(letter)}:";
        Path = path;
    }

    public char Letter { get; }

    public string Name { get; }

    public string Path { get; }
}