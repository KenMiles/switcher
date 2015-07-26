namespace SwitcherUi.switching.cfg
{
    partial class ProjectJavaSettings
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
            this.lvDesiredJavaVersion = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbPreferredJavaVersion = new System.Windows.Forms.ComboBox();
            this.cbAvailableJdkToAdd = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.btnDecrease = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblJavaVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvDesiredJavaVersion
            // 
            this.lvDesiredJavaVersion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvDesiredJavaVersion.FullRowSelect = true;
            this.lvDesiredJavaVersion.HideSelection = false;
            this.lvDesiredJavaVersion.Location = new System.Drawing.Point(28, 79);
            this.lvDesiredJavaVersion.Name = "lvDesiredJavaVersion";
            this.lvDesiredJavaVersion.Size = new System.Drawing.Size(543, 129);
            this.lvDesiredJavaVersion.TabIndex = 0;
            this.lvDesiredJavaVersion.UseCompatibleStateImageBehavior = false;
            this.lvDesiredJavaVersion.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "JDK";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Available";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Path";
            this.columnHeader3.Width = 350;
            // 
            // cbPreferredJavaVersion
            // 
            this.cbPreferredJavaVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPreferredJavaVersion.FormattingEnabled = true;
            this.cbPreferredJavaVersion.Location = new System.Drawing.Point(135, 16);
            this.cbPreferredJavaVersion.Name = "cbPreferredJavaVersion";
            this.cbPreferredJavaVersion.Size = new System.Drawing.Size(121, 21);
            this.cbPreferredJavaVersion.TabIndex = 1;
            this.cbPreferredJavaVersion.SelectedIndexChanged += new System.EventHandler(this.cbPreferredJavaVersion_SelectedIndexChanged);
            // 
            // cbAvailableJdkToAdd
            // 
            this.cbAvailableJdkToAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAvailableJdkToAdd.FormattingEnabled = true;
            this.cbAvailableJdkToAdd.Location = new System.Drawing.Point(341, 16);
            this.cbAvailableJdkToAdd.Name = "cbAvailableJdkToAdd";
            this.cbAvailableJdkToAdd.Size = new System.Drawing.Size(151, 21);
            this.cbAvailableJdkToAdd.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(498, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnIncrease
            // 
            this.btnIncrease.Location = new System.Drawing.Point(577, 112);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(105, 23);
            this.btnIncrease.TabIndex = 4;
            this.btnIncrease.Text = "Increase Priority";
            this.btnIncrease.UseVisualStyleBackColor = true;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // btnDecrease
            // 
            this.btnDecrease.Location = new System.Drawing.Point(577, 152);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(105, 23);
            this.btnDecrease.TabIndex = 5;
            this.btnDecrease.Text = "Decrease Priority";
            this.btnDecrease.UseVisualStyleBackColor = true;
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(28, 223);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblJavaVersion
            // 
            this.lblJavaVersion.AutoSize = true;
            this.lblJavaVersion.Location = new System.Drawing.Point(25, 20);
            this.lblJavaVersion.Name = "lblJavaVersion";
            this.lblJavaVersion.Size = new System.Drawing.Size(104, 13);
            this.lblJavaVersion.TabIndex = 7;
            this.lblJavaVersion.Text = "Project Java Version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "JDK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "JDK to set as JAVA home (searched in order to one found) - leave empty if Java no" +
    "t applicable";
            // 
            // ProjectJavaSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblJavaVersion);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnDecrease);
            this.Controls.Add(this.btnIncrease);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbAvailableJdkToAdd);
            this.Controls.Add(this.cbPreferredJavaVersion);
            this.Controls.Add(this.lvDesiredJavaVersion);
            this.Name = "ProjectJavaSettings";
            this.Size = new System.Drawing.Size(747, 262);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDesiredJavaVersion;
        private System.Windows.Forms.ComboBox cbPreferredJavaVersion;
        private System.Windows.Forms.ComboBox cbAvailableJdkToAdd;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.Button btnDecrease;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblJavaVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
