namespace DentalClinicAPP.Forms.BasicsCode
{
    partial class FindBranchFrm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtBranche = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.managements = new DentalClinicAPP.Managements();
            this.tblManagementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbl_ManagementTableAdapter = new DentalClinicAPP.ManagementsTableAdapters.Tbl_ManagementTableAdapter();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managementIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exchangeCenterIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parentIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managementCategoryIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblManagementBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBranche
            // 
            this.txtBranche.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranche.Location = new System.Drawing.Point(14, 49);
            this.txtBranche.Name = "txtBranche";
            this.txtBranche.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBranche.Size = new System.Drawing.Size(473, 26);
            this.txtBranche.TabIndex = 74;
            this.txtBranche.TextChanged += new System.EventHandler(this.txtBranche_TextChanged);
            this.txtBranche.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranche_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(447, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 22);
            this.label4.TabIndex = 121;
            this.label4.Text = "الفرع";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.managementIDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.exchangeCenterIDDataGridViewTextBoxColumn,
            this.parentIDDataGridViewTextBoxColumn,
            this.managementCategoryIDDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView1.DataSource = this.tblManagementBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(14, 86);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(473, 405);
            this.dataGridView1.TabIndex = 122;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // managements
            // 
            this.managements.DataSetName = "Managements";
            this.managements.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblManagementBindingSource
            // 
            this.tblManagementBindingSource.DataMember = "Tbl_Management";
            this.tblManagementBindingSource.DataSource = this.managements;
            // 
            // tbl_ManagementTableAdapter
            // 
            this.tbl_ManagementTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // managementIDDataGridViewTextBoxColumn
            // 
            this.managementIDDataGridViewTextBoxColumn.DataPropertyName = "Management_ID";
            this.managementIDDataGridViewTextBoxColumn.HeaderText = "Management_ID";
            this.managementIDDataGridViewTextBoxColumn.Name = "managementIDDataGridViewTextBoxColumn";
            this.managementIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.managementIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 400;
            // 
            // exchangeCenterIDDataGridViewTextBoxColumn
            // 
            this.exchangeCenterIDDataGridViewTextBoxColumn.DataPropertyName = "ExchangeCenter_ID";
            this.exchangeCenterIDDataGridViewTextBoxColumn.HeaderText = "ExchangeCenter_ID";
            this.exchangeCenterIDDataGridViewTextBoxColumn.Name = "exchangeCenterIDDataGridViewTextBoxColumn";
            this.exchangeCenterIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.exchangeCenterIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // parentIDDataGridViewTextBoxColumn
            // 
            this.parentIDDataGridViewTextBoxColumn.DataPropertyName = "Parent_ID";
            this.parentIDDataGridViewTextBoxColumn.HeaderText = "Parent_ID";
            this.parentIDDataGridViewTextBoxColumn.Name = "parentIDDataGridViewTextBoxColumn";
            this.parentIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.parentIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // managementCategoryIDDataGridViewTextBoxColumn
            // 
            this.managementCategoryIDDataGridViewTextBoxColumn.DataPropertyName = "ManagementCategory_ID";
            this.managementCategoryIDDataGridViewTextBoxColumn.HeaderText = "ManagementCategory_ID";
            this.managementCategoryIDDataGridViewTextBoxColumn.Name = "managementCategoryIDDataGridViewTextBoxColumn";
            this.managementCategoryIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.managementCategoryIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "BrancheName";
            this.dataGridViewTextBoxColumn2.HeaderText = "BrancheName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "KindBranchDirect";
            this.dataGridViewTextBoxColumn3.HeaderText = "KindBranchDirect";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // FindBranchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 512);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBranche);
            this.Name = "FindBranchFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FindBranchFrm_FormClosed);
            this.Load += new System.EventHandler(this.FindBranchFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblManagementBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtBranche;
        public System.Windows.Forms.DataGridView dataGridView1;
        //private FinancialSysDataSetTableAdapters.Tbl_Management1TableAdapter tbl_Management1TableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brancheNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kindBranchDirectDataGridViewTextBoxColumn;
        private Managements managements;
        private System.Windows.Forms.BindingSource tblManagementBindingSource;
        private ManagementsTableAdapters.Tbl_ManagementTableAdapter tbl_ManagementTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn managementIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exchangeCenterIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parentIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn managementCategoryIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}