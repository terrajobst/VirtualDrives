using VirtualDrives.Services;

namespace VirtualDrives;

internal sealed partial class EditVirtualDriveForm : Form
{
    private readonly ISet<char> _availableDrives;

    public EditVirtualDriveForm(ISet<char> availableDrives)
    {
        InitializeComponent();

        foreach (var drive in availableDrives)
            driveComboBox.Items.Add(drive);

        driveComboBox.SelectedIndex = 0;
        _availableDrives = availableDrives;
    }

    public char Letter
    {
        get => (char)driveComboBox.SelectedItem!;
        set
        {
            if (!_availableDrives.Contains(value))
                throw new ArgumentOutOfRangeException(nameof(value), value, null);

            driveComboBox.SelectedIndex = driveComboBox.Items.IndexOf(value);
        }
    }

    public string Path
    {
        get => pathComboBox.Text;
        set => pathComboBox.Text = value;
    }

    public bool IsPathValid()
    {
        return !string.IsNullOrEmpty(Path) && Directory.Exists(Path);
    }

    private void UpdateCommandState()
    {
        okButton.Enabled = IsPathValid();
    }

    private void EditVirtualDriveForm_Shown(object sender, EventArgs e)
    {
        UpdateCommandState();
    }

    private void EditVirtualDriveForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (DialogResult == DialogResult.OK)
        {
            e.Cancel = !IsPathValid();

            if (!e.Cancel)
            {
                if (VirtualDriveManager.CheckForCycle(Letter, Path, out var cycle))
                {
                    MessageBox.Show(this, $"This path would create a cycle {cycle}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }
    }

    private void pathComboBox_TextChanged(object sender, EventArgs e)
    {
        UpdateCommandState();
    }

    private void browseButton_Click(object sender, EventArgs e)
    {
        folderBrowserDialog.InitialDirectory = pathComboBox.Text;

        if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK)
            return;

        pathComboBox.Text = folderBrowserDialog.SelectedPath;
    }
}
