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
            this.lvDatabase.Location = new System.Drawing.Point(12, 52);
            this.lvDatabase.MultiSelect = false;
            this.lvDatabase.Name = "lvDatabase";
            this.lvDatabase.Size = new System.Drawing.Size(776, 108);
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
            this.lvServices.Location = new System.Drawing.Point(92, 253);
            this.lvServices.Name = "lvServices";
            this.lvServices.Size = new System.Drawing.Size(558, 111);
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
            this.label2.Location = new System.Drawing.Point(13, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database";
            // 
            // edtDatabaseName
            // 
            this.edtDatabaseName.Location = new System.Drawing.Point(92, 218);
            this.edtDatabaseName.Name = "edtDatabaseName";
            this.edtDatabaseName.Size = new System.Drawing.Size(185, 20);
            this.edtDatabaseName.TabIndex = 4;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(256, 174);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(56, 24);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Delete";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(136, 174);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(56, 24);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(75, 174);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 24);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Database";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Available Services";
            // 
            // btnDecrease
            // 
            this.btnDecrease.Location = new System.Drawing.Point(669, 316);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(105, 23);
            this.btnDecrease.TabIndex = 14;
            this.btnDecrease.Text = "Start Later";
            this.btnDecrease.UseVisualStyleBackColor = true;
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // btnIncrease
            // 
            this.btnIncrease.Location = new System.Drawing.Point(669, 276);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(105, 23);
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
            this.cbServices.Location = new System.Drawing.Point(126, 418);
            this.cbServices.Name = "cbServices";
            this.cbServices.Size = new System.Drawing.Size(275, 21);
            this.cbServices.TabIndex = 15;
            // 
            // btnAddService
            // 
            this.btnAddService.Location = new System.Drawing.Point(420, 417);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(75, 23);
            this.btnAddService.TabIndex = 16;
            this.btnAddService.Text = "Add";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // btnRemoveServices
            // 
            this.btnRemoveServices.Location = new System.Drawing.Point(92, 370);
            this.btnRemoveServices.Name = "btnRemoveServices";
            this.btnRemoveServices.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveServices.TabIndex = 17;
            this.btnRemoveServices.Text = "Remove";
            this.btnRemoveServices.UseVisualStyleBackColor = true;
            this.btnRemoveServices.Click += new System.EventHandler(this.btnRemoveServices_Click);
            // 
            // lblEditing
            // 
            this.lblEditing.AutoSize = true;
            this.lblEditing.Location = new System.Drawing.Point(283, 221);
            this.lblEditing.Name = "lblEditing";
            this.lblEditing.Size = new System.Drawing.Size(33, 13);
            this.lblEditing.TabIndex = 18;
            this.lblEditing.Text = "(new)";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(15, 174);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 24);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "New";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClearChanges
            // 
            this.btnClearChanges.Location = new System.Drawing.Point(196, 174);
            this.btnClearChanges.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearChanges.Name = "btnClearChanges";
            this.btnClearChanges.Size = new System.Drawing.Size(56, 24);
            this.btnClearChanges.TabIndex = 20;
            this.btnClearChanges.Text = "Reset";
            this.btnClearChanges.UseVisualStyleBackColor = true;
            this.btnClearChanges.Click += new System.EventHandler(this.btnClearChanges_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(719, 482);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 28);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(638, 482);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 28);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Timeout (seconds)";
            // 
            // edtTimeoutSeconds
            // 
            this.edtTimeoutSeconds.Location = new System.Drawing.Point(116, 11);
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
            this.edtTimeoutSeconds.Size = new System.Drawing.Size(76, 20);
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
            this.label5.Location = new System.Drawing.Point(213, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(395, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "(How quick to give up checking Service has started - services will contine starti" +
    "ng)";
            // 
            // FrmDatabaseServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(806, 521);
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
    }
}