namespace VirtualDrives
{
    partial class EditVirtualDriveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            okButton = new Button();
            cancelButton = new Button();
            driveComboBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            browseButton = new Button();
            pathComboBox = new ComboBox();
            folderBrowserDialog = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(558, 127);
            okButton.Name = "okButton";
            okButton.Size = new Size(112, 34);
            okButton.TabIndex = 0;
            okButton.Text = "&OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(676, 127);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(112, 34);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "&Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // driveComboBox
            // 
            driveComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            driveComboBox.FormattingEnabled = true;
            driveComboBox.Location = new Point(174, 12);
            driveComboBox.Name = "driveComboBox";
            driveComboBox.Size = new Size(101, 33);
            driveComboBox.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(57, 25);
            label1.TabIndex = 4;
            label1.Text = "&Drive:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(66, 25);
            label2.TabIndex = 5;
            label2.Text = "&Folder:";
            // 
            // browseButton
            // 
            browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseButton.Location = new Point(676, 49);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(112, 34);
            browseButton.TabIndex = 6;
            browseButton.Text = "&Browse...";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // pathComboBox
            // 
            pathComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            pathComboBox.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
            pathComboBox.FormattingEnabled = true;
            pathComboBox.Location = new Point(174, 51);
            pathComboBox.Name = "pathComboBox";
            pathComboBox.Size = new Size(496, 33);
            pathComboBox.TabIndex = 7;
            pathComboBox.TextChanged += pathComboBox_TextChanged;
            // 
            // EditVirtualDriveForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(800, 173);
            Controls.Add(pathComboBox);
            Controls.Add(browseButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(driveComboBox);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditVirtualDriveForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Virtual Drive";
            FormClosing += EditVirtualDriveForm_FormClosing;
            Shown += EditVirtualDriveForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private ComboBox driveComboBox;
        private Label label1;
        private Label label2;
        private Button browseButton;
        private ComboBox pathComboBox;
        private FolderBrowserDialog folderBrowserDialog;
    }
}