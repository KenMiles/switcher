namespace SwitcherUi.allowSwitching.cfg
{
    partial class ProcessesForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvBlocking = new System.Windows.Forms.ListView();
            this.colExecutableName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddBlocking = new System.Windows.Forms.Button();
            this.btnRemoveBlocking = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemoveWarning = new System.Windows.Forms.Button();
            this.btnAddWarning = new System.Windows.Forms.Button();
            this.lvWarning = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMoveWarning = new System.Windows.Forms.Button();
            this.btnMoveBlocking = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(510, 457);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(618, 457);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 34);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvBlocking
            // 
            this.lvBlocking.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colExecutableName,
            this.colDescription});
            this.lvBlocking.FullRowSelect = true;
            this.lvBlocking.HideSelection = false;
            this.lvBlocking.Location = new System.Drawing.Point(27, 43);
            this.lvBlocking.Name = "lvBlocking";
            this.lvBlocking.Size = new System.Drawing.Size(605, 153);
            this.lvBlocking.TabIndex = 4;
            this.lvBlocking.UseCompatibleStateImageBehavior = false;
            this.lvBlocking.View = System.Windows.Forms.View.Details;
            // 
            // colExecutableName
            // 
            this.colExecutableName.Text = "Executable Name";
            this.colExecutableName.Width = 240;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 320;
            // 
            // btnAddBlocking
            // 
            this.btnAddBlocking.Location = new System.Drawing.Point(645, 43);
            this.btnAddBlocking.Name = "btnAddBlocking";
            this.btnAddBlocking.Size = new System.Drawing.Size(75, 29);
            this.btnAddBlocking.TabIndex = 5;
            this.btnAddBlocking.Text = "Add";
            this.btnAddBlocking.UseVisualStyleBackColor = true;
            this.btnAddBlocking.Click += new System.EventHandler(this.btnAddBlocking_Click);
            // 
            // btnRemoveBlocking
            // 
            this.btnRemoveBlocking.Location = new System.Drawing.Point(645, 78);
            this.btnRemoveBlocking.Name = "btnRemoveBlocking";
            this.btnRemoveBlocking.Size = new System.Drawing.Size(75, 29);
            this.btnRemoveBlocking.TabIndex = 6;
            this.btnRemoveBlocking.Text = "Remove";
            this.btnRemoveBlocking.UseVisualStyleBackColor = true;
            this.btnRemoveBlocking.Click += new System.EventHandler(this.btnRemoveBlocking_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Blocking Processes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Warning Processes";
            // 
            // btnRemoveWarning
            // 
            this.btnRemoveWarning.Location = new System.Drawing.Point(645, 311);
            this.btnRemoveWarning.Name = "btnRemoveWarning";
            this.btnRemoveWarning.Size = new System.Drawing.Size(75, 29);
            this.btnRemoveWarning.TabIndex = 10;
            this.btnRemoveWarning.Text = "Remove";
            this.btnRemoveWarning.UseVisualStyleBackColor = true;
            this.btnRemoveWarning.Click += new System.EventHandler(this.btnRemoveWarning_Click);
            // 
            // btnAddWarning
            // 
            this.btnAddWarning.Location = new System.Drawing.Point(645, 276);
            this.btnAddWarning.Name = "btnAddWarning";
            this.btnAddWarning.Size = new System.Drawing.Size(75, 29);
            this.btnAddWarning.TabIndex = 9;
            this.btnAddWarning.Text = "Add";
            this.btnAddWarning.UseVisualStyleBackColor = true;
            this.btnAddWarning.Click += new System.EventHandler(this.btnAddWarning_Click);
            // 
            // lvWarning
            // 
            this.lvWarning.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvWarning.FullRowSelect = true;
            this.lvWarning.HideSelection = false;
            this.lvWarning.Location = new System.Drawing.Point(27, 276);
            this.lvWarning.Name = "lvWarning";
            this.lvWarning.Size = new System.Drawing.Size(605, 153);
            this.lvWarning.TabIndex = 8;
            this.lvWarning.UseCompatibleStateImageBehavior = false;
            this.lvWarning.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Executable Name";
            this.columnHeader1.Width = 240;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 320;
            // 
            // btnMoveWarning
            // 
            this.btnMoveWarning.Location = new System.Drawing.Point(207, 220);
            this.btnMoveWarning.Name = "btnMoveWarning";
            this.btnMoveWarning.Size = new System.Drawing.Size(116, 30);
            this.btnMoveWarning.TabIndex = 12;
            this.btnMoveWarning.Text = "Make Warning";
            this.btnMoveWarning.UseVisualStyleBackColor = true;
            this.btnMoveWarning.Click += new System.EventHandler(this.btnMoveWarning_Click);
            // 
            // btnMoveBlocking
            // 
            this.btnMoveBlocking.Location = new System.Drawing.Point(329, 220);
            this.btnMoveBlocking.Name = "btnMoveBlocking";
            this.btnMoveBlocking.Size = new System.Drawing.Size(120, 30);
            this.btnMoveBlocking.TabIndex = 13;
            this.btnMoveBlocking.Text = "Make Blocking";
            this.btnMoveBlocking.UseVisualStyleBackColor = true;
            this.btnMoveBlocking.Click += new System.EventHandler(this.btnMoveBlocking_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Exes|*.exe|All Files|*.*";
            this.openFileDialog1.Multiselect = true;
            // 
            // ProcessesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 513);
            this.Controls.Add(this.btnMoveBlocking);
            this.Controls.Add(this.btnMoveWarning);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemoveWarning);
            this.Controls.Add(this.btnAddWarning);
            this.Controls.Add(this.lvWarning);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveBlocking);
            this.Controls.Add(this.btnAddBlocking);
            this.Controls.Add(this.lvBlocking);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "ProcessesForm";
            this.Text = "Define Avoid Processes";
            this.Load += new System.EventHandler(this.ProcessesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvBlocking;
        private System.Windows.Forms.ColumnHeader colExecutableName;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.Button btnAddBlocking;
        private System.Windows.Forms.Button btnRemoveBlocking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemoveWarning;
        private System.Windows.Forms.Button btnAddWarning;
        private System.Windows.Forms.ListView lvWarning;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnMoveWarning;
        private System.Windows.Forms.Button btnMoveBlocking;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}