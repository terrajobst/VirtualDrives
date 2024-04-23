using VirtualDrives.Services;

namespace VirtualDrives;

internal sealed partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        LoadDrives();
        UpdateCommandState();
    }

    private void LoadDrives()
    {
        listView.Items.Clear();

        foreach (var drive in VirtualDriveManager.GetDrives().OrderBy(d => d.Letter))
        {
            var item = new ListViewItem();
            item.Text = drive.Name;
            item.SubItems.Add(drive.Path);
            item.Tag = drive;
            listView.Items.Add(item);
        }

        restartInfoPanel.Visible = VirtualDriveManager.HasPendingChanges();
        FixupListViewHeigh();
    }

    private void FixupListViewHeigh()
    {
        const int verticalGap = 8;

        if (restartInfoPanel.Visible)
            listView.Height -= restartInfoPanel.Height + verticalGap;
        else
            listView.Height += restartInfoPanel.Height + verticalGap;
    }

    private void UpdateCommandState()
    {
        bool hasSelectedItems = listView.SelectedItems.Count > 0;

        editButton.Enabled = hasSelectedItems;
        removeButton.Enabled = hasSelectedItems;

        editToolStripMenuItem.Enabled = editButton.Enabled;
        removeToolStripMenuItem.Enabled = removeButton.Enabled;
    }

    private void AddVirtualDrive()
    {
        var availableDrives = VirtualDriveManager.GetAvailableLetters();
        if (availableDrives.Count == 0)
        {
            MessageBox.Show("No more drive letters available.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var dialog = new EditVirtualDriveForm(availableDrives)
        {
            Letter = availableDrives.First()
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        VirtualDriveManager.SetDrivePath(dialog.Letter, dialog.Path);
        LoadDrives();
    }

    private void EditSelectedDrive()
    {
        var selected = listView.SelectedItems.OfType<ListViewItem>().FirstOrDefault()?.Tag as VirtualDrive;
        if (selected is null)
            return;

        var availableDrives = VirtualDriveManager.GetAvailableLetters();
        availableDrives.Add(selected.Letter);

        var dialog = new EditVirtualDriveForm(availableDrives)
        {
            Letter = selected.Letter,
            Path = selected.Path
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        VirtualDriveManager.DeleteDrive(selected.Letter);
        VirtualDriveManager.SetDrivePath(dialog.Letter, dialog.Path);
        LoadDrives();
    }

    private void RemoveSelectedDrives()
    {
        if (listView.SelectedItems.Count == 0)
            return;

        if (listView.SelectedItems.Count > 1)
        {
            var message = $"Do you want to the selected {listView.SelectedItems.Count} virtual drives?";
            var caption = Text;
            var confirmationResult = MessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (confirmationResult != DialogResult.Yes)
                return;
        }
        else
        {
            var selected = listView.SelectedItems.OfType<ListViewItem>().FirstOrDefault()?.Tag as VirtualDrive;
            if (selected is null)
                return;

            var message = $"Do you want to remove virtual drive '{selected.Name}'?";
            var caption = Text;
            var confirmationResult = MessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (confirmationResult != DialogResult.Yes)
                return;
        }

        foreach (var drive in listView.SelectedItems.OfType<ListViewItem>().Select(i => i.Tag).OfType<VirtualDrive>())
            VirtualDriveManager.DeleteDrive(drive.Letter);

        LoadDrives();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
        FixupListViewHeigh();
    }

    private void addButton_Click(object sender, EventArgs e)
    {
        AddVirtualDrive();
    }

    private void editButton_Click(object sender, EventArgs e)
    {
        EditSelectedDrive();
    }

    private void removeButton_Click(object sender, EventArgs e)
    {
        RemoveSelectedDrives();
    }

    private void listView_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateCommandState();
    }

    private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        EditSelectedDrive();
    }

    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddVirtualDrive();
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        EditSelectedDrive();
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        RemoveSelectedDrives();
    }

    private void restartButton_Click(object sender, EventArgs e)
    {
        var confirmation = MessageBox.Show(this, "Do you want to restart now?", Text,
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

        if (confirmation != DialogResult.OK)
            return;

        if (!RestartServices.Restart())
            MessageBox.Show(this, "Cannot restart computer.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
