namespace SwitcherUi.switching.cfg
{
    partial class FrmDatabaseServices
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
            this.lvDatabase = new System.Windows.Forms.ListView();
            this.colDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colServices = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvServices = new System.Windows.Forms.ListView();
            this.colService = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colServiceDisplay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.edtDatabaseName = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDecrease = new System.Windows.Forms.Button();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.cbServices = new System.Windows.Forms.ComboBox();
            this.btnAddService = new System.Windows.Forms.Button();
            this.btnRemoveServices = new System.Windows.Forms.Button();
            this.lblEditing = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClearChanges = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.edtTimeoutSeconds = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCopyServices = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.edtTimeoutSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // lvDatabase
            // 
            this.lvDatabase.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDatabase,
            this.colServices});
            this.lvDatabase.FullRowSelect = true;
            this.lvDatabase.HideSelection = false;
            this.lvDatabase.Location = new System.Drawing.Point(16, 64);
            this.lvDatabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvDatabase.MultiSelect = false;
            this.lvDatabase.Name = "lvDatabase";
            this.lvDatabase.Size = new System.Drawing.Size(1033, 132);
            this.lvDatabase.TabIndex = 0;
            this.lvDatabase.UseCompatibleStateImageBehavior = false;
            this.lvDatabase.View = System.Windows.Forms.View.Details;
            this.lvDatabase.SelectedIndexChanged += new System.EventHandler(this.lvDatabase_SelectedIndexChanged);
            // 
            // colDatabase
            // 
            this.colDatabase.Text = "Database";
            this.colDatabase.Width = 200;
            // 
            // colServices
            // 
            this.colServices.Text = "Services";
            this.colServices.Width = 540;
            // 
            // lvServices
            // 
            this.lvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colService,
            this.colServiceDisplay});
            this.lvServices.FullRowSelect = true;
            this.lvServices.HideSelection = false;
            this.lvServices.Location = new System.Drawing.Point(123, 311);
            this.lvServices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvServices.Name = "lvServices";
            this.lvServices.Size = new System.Drawing.Size(743, 136);
            this.lvServices.TabIndex = 2;
            this.lvServices.UseCompatibleStateImageBehavior = false;
            this.lvServices.View = System.Windows.Forms.View.Details;
            // 
            // colService
            // 
            this.colService.Text = "Service Name";
            this.colService.Width = 200;
            // 
            // colServiceDisplay
            // 
            this.colServiceDisplay.Text = "Display Name";
            this.colServiceDisplay.Width = 320;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 268);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database";
            // 
            // edtDatabaseName
            // 
            this.edtDatabaseName.Location = new System.Drawing.Point(123, 268);
            this.edtDatabaseName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.edtDatabaseName.Name = "edtDatabaseName";
            this.edtDatabaseName.Size = new System.Drawing.Size(245, 22);
            this.edtDatabaseName.TabIndex = 4;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(182, 214);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 30);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Delete";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(101, 214);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 214);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 327);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Database";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 519);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Available Services";
            // 
            // btnDecrease
            // 
            this.btnDecrease.Location = new System.Drawing.Point(892, 389);
            this.btnDecrease.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(140, 28);
            this.btnDecrease.TabIndex = 14;
            this.btnDecrease.Text = "Start Later";
            this.btnDecrease.UseVisualStyleBackColor = true;
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // btnIncrease
            // 
            this.btnIncrease.Location = new System.Drawing.Point(892, 340);
            this.btnIncrease.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(140, 28);
            this.btnIncrease.TabIndex = 13;
            this.btnIncrease.Text = "Start Earlier";
            this.btnIncrease.UseVisualStyleBackColor = true;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // cbServices
            // 
            this.cbServices.DisplayMember = "DisplayName";
            this.cbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServices.FormattingEnabled = true;
            this.cbServices.Location = new System.Drawing.Point(168, 515);
            this.cbServices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbServices.Name = "cbServices";
            this.cbServices.Size = new System.Drawing.Size(365, 24);
            this.cbServices.TabIndex = 15;
            // 
            // btnAddService
            // 
            this.btnAddService.Location = new System.Drawing.Point(560, 513);
            this.btnAddService.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(100, 28);
            this.btnAddService.TabIndex = 16;
            this.btnAddService.Text = "Add";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // btnRemoveServices
            // 
            this.btnRemoveServices.Location = new System.Drawing.Point(123, 455);
            this.btnRemoveServices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemoveServices.Name = "btnRemoveServices";
            this.btnRemoveServices.Size = new System.Drawing.Size(100, 28);
            this.btnRemoveServices.TabIndex = 17;
            this.btnRemoveServices.Text = "Remove";
            this.btnRemoveServices.UseVisualStyleBackColor = true;
            this.btnRemoveServices.Click += new System.EventHandler(this.btnRemoveServices_Click);
            // 
            // lblEditing
            // 
            this.lblEditing.AutoSize = true;
            this.lblEditing.Location = new System.Drawing.Point(377, 272);
            this.lblEditing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEditing.Name = "lblEditing";
            this.lblEditing.Size = new System.Drawing.Size(43, 17);
            this.lblEditing.TabIndex = 18;
            this.lblEditing.Text = "(new)";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(394, 214);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClearChanges
            // 
            this.btnClearChanges.Location = new System.Drawing.Point(313, 214);
            this.btnClearChanges.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearChanges.Name = "btnClearChanges";
            this.btnClearChanges.Size = new System.Drawing.Size(75, 30);
            this.btnClearChanges.TabIndex = 20;
            this.btnClearChanges.Text = "Reset";
            this.btnClearChanges.UseVisualStyleBackColor = true;
            this.btnClearChanges.Click += new System.EventHandler(this.btnClearChanges_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(959, 593);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 34);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(851, 593);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 34);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Timeout (seconds)";
            // 
            // edtTimeoutSeconds
            // 
            this.edtTimeoutSeconds.Location = new System.Drawing.Point(155, 14);
            this.edtTimeoutSeconds.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.edtTimeoutSeconds.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.edtTimeoutSeconds.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.edtTimeoutSeconds.Name = "edtTimeoutSeconds";
            this.edtTimeoutSeconds.Size = new System.Drawing.Size(100, 22);
            this.edtTimeoutSeconds.TabIndex = 24;
            this.edtTimeoutSeconds.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(522, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "(How quick to give up checking Service has started - services will contine starti" +
    "ng)";
            // 
            // btnCopyServices
            // 
            this.btnCopyServices.Location = new System.Drawing.Point(706, 512);
            this.btnCopyServices.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyServices.Name = "btnCopyServices";
            this.btnCopyServices.Size = new System.Drawing.Size(193, 28);
            this.btnCopyServices.TabIndex = 26;
            this.btnCopyServices.Text = "Service List To Clipboard";
            this.btnCopyServices.UseVisualStyleBackColor = true;
            this.btnCopyServices.Click += new System.EventHandler(this.btnCopyServices_Click);
            // 
            // FrmDatabaseServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1075, 641);
            this.Controls.Add(this.btnCopyServices);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.edtTimeoutSeconds);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClearChanges);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblEditing);
            this.Controls.Add(this.btnRemoveServices);
            this.Controls.Add(this.btnAddService);
            this.Controls.Add(this.cbServices);
            this.Controls.Add(this.btnDecrease);
            this.Controls.Add(this.btnIncrease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.edtDatabaseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvServices);
            this.Controls.Add(this.lvDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDatabaseServices";
            this.Text = "FrmDatabaseServices";
            this.Load += new System.EventHandler(this.FrmDatabaseServices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edtTimeoutSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDatabase;
        private System.Windows.Forms.ColumnHeader colDatabase;
        private System.Windows.Forms.ColumnHeader colServices;
        private System.Windows.Forms.ListView lvServices;
        private System.Windows.Forms.ColumnHeader colService;
        private System.Windows.Forms.ColumnHeader colServiceDisplay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtDatabaseName;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDecrease;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.ComboBox cbServices;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnRemoveServices;
        private System.Windows.Forms.Label lblEditing;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClearChanges;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown edtTimeoutSeconds;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCopyServices;
    }
}