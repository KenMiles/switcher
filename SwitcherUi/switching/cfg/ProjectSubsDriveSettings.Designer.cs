namespace SwitcherUi.switching.cfg
{
    partial class ProjectSubsDriveSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbFolders = new System.Windows.Forms.ComboBox();
            this.lblPointDrive = new System.Windows.Forms.Label();
            this.lblInFolder = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // cbFolders
            // 
            this.cbFolders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFolders.FormattingEnabled = true;
            this.cbFolders.Location = new System.Drawing.Point(167, 30);
            this.cbFolders.Name = "cbFolders";
            this.cbFolders.Size = new System.Drawing.Size(181, 21);
            this.cbFolders.TabIndex = 1;
            // 
            // lblPointDrive
            // 
            this.lblPointDrive.AutoSize = true;
            this.lblPointDrive.Location = new System.Drawing.Point(26, 33);
            this.lblPointDrive.Name = "lblPointDrive";
            this.lblPointDrive.Size = new System.Drawing.Size(120, 13);
            this.lblPointDrive.TabIndex = 2;
            this.lblPointDrive.Text = "Point Drive {0} at folder:";
            // 
            // lblInFolder
            // 
            this.lblInFolder.AutoSize = true;
            this.lblInFolder.Location = new System.Drawing.Point(409, 33);
            this.lblInFolder.Name = "lblInFolder";
            this.lblInFolder.Size = new System.Drawing.Size(62, 13);
            this.lblInFolder.TabIndex = 3;
            this.lblInFolder.Text = "In folder {0}";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(364, 28);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(29, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ProjectSubsDriveSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblInFolder);
            this.Controls.Add(this.lblPointDrive);
            this.Controls.Add(this.cbFolders);
            this.Name = "ProjectSubsDriveSettings";
            this.Size = new System.Drawing.Size(747, 262);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFolders;
        private System.Windows.Forms.Label lblPointDrive;
        private System.Windows.Forms.Label lblInFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
