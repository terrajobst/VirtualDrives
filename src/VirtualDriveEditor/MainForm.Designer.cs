namespace VirtualDrives;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        listView = new ListView();
        columnHeader1 = new ColumnHeader();
        columnHeader2 = new ColumnHeader();
        contextMenuStrip = new ContextMenuStrip(components);
        addToolStripMenuItem = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        toolStripMenuItem1 = new ToolStripSeparator();
        removeToolStripMenuItem = new ToolStripMenuItem();
        addButton = new Button();
        editButton = new Button();
        removeButton = new Button();
        restartInfoPanel = new Panel();
        restartButton = new Button();
        label1 = new Label();
        contextMenuStrip.SuspendLayout();
        restartInfoPanel.SuspendLayout();
        SuspendLayout();
        // 
        // listView
        // 
        listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        listView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
        listView.ContextMenuStrip = contextMenuStrip;
        listView.FullRowSelect = true;
        listView.Location = new Point(12, 12);
        listView.Name = "listView";
        listView.Size = new Size(736, 420);
        listView.TabIndex = 0;
        listView.UseCompatibleStateImageBehavior = false;
        listView.View = View.Details;
        listView.SelectedIndexChanged += listView_SelectedIndexChanged;
        listView.MouseDoubleClick += listView_MouseDoubleClick;
        // 
        // columnHeader1
        // 
        columnHeader1.Text = "Drive";
        // 
        // columnHeader2
        // 
        columnHeader2.Text = "Path";
        columnHeader2.Width = 594;
        // 
        // contextMenuStrip
        // 
        contextMenuStrip.ImageScalingSize = new Size(24, 24);
        contextMenuStrip.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, editToolStripMenuItem, toolStripMenuItem1, removeToolStripMenuItem });
        contextMenuStrip.Name = "contextMenuStrip";
        contextMenuStrip.Size = new Size(187, 106);
        // 
        // addToolStripMenuItem
        // 
        addToolStripMenuItem.Name = "addToolStripMenuItem";
        addToolStripMenuItem.ShortcutKeys = Keys.Insert;
        addToolStripMenuItem.Size = new Size(186, 32);
        addToolStripMenuItem.Text = "Add...";
        addToolStripMenuItem.Click += addToolStripMenuItem_Click;
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Size = new Size(186, 32);
        editToolStripMenuItem.Text = "Edit...";
        editToolStripMenuItem.Click += editToolStripMenuItem_Click;
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(183, 6);
        // 
        // removeToolStripMenuItem
        // 
        removeToolStripMenuItem.Name = "removeToolStripMenuItem";
        removeToolStripMenuItem.ShortcutKeys = Keys.Delete;
        removeToolStripMenuItem.Size = new Size(186, 32);
        removeToolStripMenuItem.Text = "Remove";
        removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
        // 
        // addButton
        // 
        addButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        addButton.Location = new Point(754, 12);
        addButton.Name = "addButton";
        addButton.Size = new Size(112, 34);
        addButton.TabIndex = 1;
        addButton.Text = "&Add...";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += addButton_Click;
        // 
        // editButton
        // 
        editButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        editButton.Location = new Point(754, 52);
        editButton.Name = "editButton";
        editButton.Size = new Size(112, 34);
        editButton.TabIndex = 2;
        editButton.Text = "&Edit...";
        editButton.UseVisualStyleBackColor = true;
        editButton.Click += editButton_Click;
        // 
        // removeButton
        // 
        removeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        removeButton.Location = new Point(754, 92);
        removeButton.Name = "removeButton";
        removeButton.Size = new Size(112, 34);
        removeButton.TabIndex = 3;
        removeButton.Text = "&Remove";
        removeButton.UseVisualStyleBackColor = true;
        removeButton.Click += removeButton_Click;
        // 
        // restartInfoPanel
        // 
        restartInfoPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        restartInfoPanel.BackColor = SystemColors.Info;
        restartInfoPanel.BorderStyle = BorderStyle.FixedSingle;
        restartInfoPanel.Controls.Add(restartButton);
        restartInfoPanel.Controls.Add(label1);
        restartInfoPanel.Location = new Point(12, 382);
        restartInfoPanel.Name = "restartInfoPanel";
        restartInfoPanel.Size = new Size(854, 50);
        restartInfoPanel.TabIndex = 4;
        // 
        // restartButton
        // 
        restartButton.Location = new Point(718, 7);
        restartButton.Name = "restartButton";
        restartButton.Size = new Size(112, 34);
        restartButton.TabIndex = 2;
        restartButton.Text = "Re&start";
        restartButton.UseVisualStyleBackColor = true;
        restartButton.Click += restartButton_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(9, 12);
        label1.Name = "label1";
        label1.Size = new Size(703, 25);
        label1.TabIndex = 0;
        label1.Text = "You made changes. In order for them to take effect, you need to restart your computer.";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(878, 444);
        Controls.Add(restartInfoPanel);
        Controls.Add(removeButton);
        Controls.Add(editButton);
        Controls.Add(addButton);
        Controls.Add(listView);
        MinimumSize = new Size(900, 500);
        Name = "MainForm";
        Text = "Virtual Drives";
        Shown += MainForm_Shown;
        contextMenuStrip.ResumeLayout(false);
        restartInfoPanel.ResumeLayout(false);
        restartInfoPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private ListView listView;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private Button addButton;
    private Button editButton;
    private Button removeButton;
    private ContextMenuStrip contextMenuStrip;
    private ToolStripMenuItem addToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem removeToolStripMenuItem;
    private Panel restartInfoPanel;
    private Label label1;
    private Button restartButton;
}