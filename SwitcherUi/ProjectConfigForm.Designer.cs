namespace SwitcherUi
{
    partial class ProjectConfigForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSubsDrive = new System.Windows.Forms.TabPage();
            this.tabPageJavaHome = new System.Windows.Forms.TabPage();
            this.tabEnvironmentVariables = new System.Windows.Forms.TabPage();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabDatabases = new System.Windows.Forms.TabPage();
            this.projectSubsDriveSettings1 = new SwitcherUi.switching.cfg.ProjectSubsDriveSettings();
            this.projectJavaSettings1 = new SwitcherUi.switching.cfg.ProjectJavaSettings();
            this.projectEnvVariablesSettings1 = new SwitcherUi.switching.cfg.ProjectEnvVariablesSettings();
            this.projectDatabaseStarter1 = new SwitcherUi.switching.cfg.ProjectDatabaseStarter();
            this.tabControl1.SuspendLayout();
            this.tabSubsDrive.SuspendLayout();
            this.tabPageJavaHome.SuspendLayout();
            this.tabEnvironmentVariables.SuspendLayout();
            this.tabDatabases.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSubsDrive);
            this.tabControl1.Controls.Add(this.tabPageJavaHome);
            this.tabControl1.Controls.Add(this.tabEnvironmentVariables);
            this.tabControl1.Controls.Add(this.tabDatabases);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 312);
            this.tabControl1.TabIndex = 1;
            // 
            // tabSubsDrive
            // 
            this.tabSubsDrive.Controls.Add(this.projectSubsDriveSettings1);
            this.tabSubsDrive.Location = new System.Drawing.Point(4, 22);
            this.tabSubsDrive.Name = "tabSubsDrive";
            this.tabSubsDrive.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubsDrive.Size = new System.Drawing.Size(764, 286);
            this.tabSubsDrive.TabIndex = 1;
            this.tabSubsDrive.Text = "Subst Drive";
            this.tabSubsDrive.UseVisualStyleBackColor = true;
            // 
            // tabPageJavaHome
            // 
            this.tabPageJavaHome.Controls.Add(this.projectJavaSettings1);
            this.tabPageJavaHome.Location = new System.Drawing.Point(4, 22);
            this.tabPageJavaHome.Name = "tabPageJavaHome";
            this.tabPageJavaHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJavaHome.Size = new System.Drawing.Size(764, 286);
            this.tabPageJavaHome.TabIndex = 0;
            this.tabPageJavaHome.Text = "Java Home";
            this.tabPageJavaHome.UseVisualStyleBackColor = true;
            // 
            // tabEnvironmentVariables
            // 
            this.tabEnvironmentVariables.Controls.Add(this.projectEnvVariablesSettings1);
            this.tabEnvironmentVariables.Location = new System.Drawing.Point(4, 22);
            this.tabEnvironmentVariables.Name = "tabEnvironmentVariables";
            this.tabEnvironmentVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabEnvironmentVariables.Size = new System.Drawing.Size(764, 286);
            this.tabEnvironmentVariables.TabIndex = 2;
            this.tabEnvironmentVariables.Text = "Environment Variables";
            this.tabEnvironmentVariables.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(704, 350);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(623, 350);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 28);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabDatabases
            // 
            this.tabDatabases.Controls.Add(this.projectDatabaseStarter1);
            this.tabDatabases.Location = new System.Drawing.Point(4, 22);
            this.tabDatabases.Name = "tabDatabases";
            this.tabDatabases.Padding = new System.Windows.Forms.Padding(3);
            this.tabDatabases.Size = new System.Drawing.Size(764, 286);
            this.tabDatabases.TabIndex = 3;
            this.tabDatabases.Text = "Database";
            this.tabDatabases.UseVisualStyleBackColor = true;
            // 
            // projectSubsDriveSettings1
            // 
            this.projectSubsDriveSettings1.Location = new System.Drawing.Point(7, 7);
            this.projectSubsDriveSettings1.Name = "projectSubsDriveSettings1";
            this.projectSubsDriveSettings1.Size = new System.Drawing.Size(747, 262);
            this.projectSubsDriveSettings1.TabIndex = 0;
            // 
            // projectJavaSettings1
            // 
            this.projectJavaSettings1.Location = new System.Drawing.Point(15, 6);
            this.projectJavaSettings1.Name = "projectJavaSettings1";
            this.projectJavaSettings1.Size = new System.Drawing.Size(743, 254);
            this.projectJavaSettings1.TabIndex = 1;
            // 
            // projectEnvVariablesSettings1
            // 
            this.projectEnvVariablesSettings1.Location = new System.Drawing.Point(6, 7);
            this.projectEnvVariablesSettings1.Name = "projectEnvVariablesSettings1";
            this.projectEnvVariablesSettings1.Size = new System.Drawing.Size(752, 273);
            this.projectEnvVariablesSettings1.TabIndex = 0;
            // 
            // projectDatabaseStarter1
            // 
            this.projectDatabaseStarter1.Location = new System.Drawing.Point(3, 6);
            this.projectDatabaseStarter1.Name = "projectDatabaseStarter1";
            this.projectDatabaseStarter1.Size = new System.Drawing.Size(755, 266);
            this.projectDatabaseStarter1.TabIndex = 0;
            // 
            // ProjectConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 401);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Name = "ProjectConfigForm";
            this.Text = "ProjectConfig";
            this.tabControl1.ResumeLayout(false);
            this.tabSubsDrive.ResumeLayout(false);
            this.tabPageJavaHome.ResumeLayout(false);
            this.tabEnvironmentVariables.ResumeLayout(false);
            this.tabDatabases.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageJavaHome;
        private switching.cfg.ProjectJavaSettings projectJavaSettings1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabPage tabSubsDrive;
        private switching.cfg.ProjectSubsDriveSettings projectSubsDriveSettings1;
        private System.Windows.Forms.TabPage tabEnvironmentVariables;
        private switching.cfg.ProjectEnvVariablesSettings projectEnvVariablesSettings1;
        private System.Windows.Forms.TabPage tabDatabases;
        private switching.cfg.ProjectDatabaseStarter projectDatabaseStarter1;
    }
}