namespace DentalClinicAPP.Forms.BasicsCode
{
    partial class ItemsFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodeName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUniqueNumber = new System.Windows.Forms.TextBox();
            this.txtCodeType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniqueNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblItemsOfTaxBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemsDs = new DentalClinicAPP.ItemsDs();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.tbl_ItemsOfTaxTableAdapter = new DentalClinicAPP.ItemsDsTableAdapters.Tbl_ItemsOfTaxTableAdapter();
            this.txtCodeTypeID = new System.Windows.Forms.TextBox();
            this.LangTxtBox = new System.Windows.Forms.TextBox();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.tblItemsOfTaxBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblItemsOfTaxBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblItemsOfTaxBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCodeName
            // 
            resources.ApplyResources(this.txtCodeName, "txtCodeName");
            this.txtCodeName.Name = "txtCodeName";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtUniqueNumber
            // 
            resources.ApplyResources(this.txtUniqueNumber, "txtUniqueNumber");
            this.txtUniqueNumber.Name = "txtUniqueNumber";
            // 
            // txtCodeType
            // 
            resources.ApplyResources(this.txtCodeType, "txtCodeType");
            this.txtCodeType.Name = "txtCodeType";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.codeTypeDataGridViewTextBoxColumn,
            this.uniqueNumberDataGridViewTextBoxColumn,
            this.codeNameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tblItemsOfTaxBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            resources.ApplyResources(this.iDDataGridViewTextBoxColumn, "iDDataGridViewTextBoxColumn");
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // codeTypeDataGridViewTextBoxColumn
            // 
            this.codeTypeDataGridViewTextBoxColumn.DataPropertyName = "CodeType";
            resources.ApplyResources(this.codeTypeDataGridViewTextBoxColumn, "codeTypeDataGridViewTextBoxColumn");
            this.codeTypeDataGridViewTextBoxColumn.Name = "codeTypeDataGridViewTextBoxColumn";
            // 
            // uniqueNumberDataGridViewTextBoxColumn
            // 
            this.uniqueNumberDataGridViewTextBoxColumn.DataPropertyName = "UniqueNumber";
            resources.ApplyResources(this.uniqueNumberDataGridViewTextBoxColumn, "uniqueNumberDataGridViewTextBoxColumn");
            this.uniqueNumberDataGridViewTextBoxColumn.Name = "uniqueNumberDataGridViewTextBoxColumn";
            // 
            // codeNameDataGridViewTextBoxColumn
            // 
            this.codeNameDataGridViewTextBoxColumn.DataPropertyName = "CodeName";
            resources.ApplyResources(this.codeNameDataGridViewTextBoxColumn, "codeNameDataGridViewTextBoxColumn");
            this.codeNameDataGridViewTextBoxColumn.Name = "codeNameDataGridViewTextBoxColumn";
            // 
            // tblItemsOfTaxBindingSource
            // 
            this.tblItemsOfTaxBindingSource.DataMember = "Tbl_ItemsOfTax";
            this.tblItemsOfTaxBindingSource.DataSource = this.itemsDs;
            // 
            // itemsDs
            // 
            this.itemsDs.DataSetName = "ItemsDs";
            this.itemsDs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton1.Appearance.Font")));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            resources.ApplyResources(this.simpleButton2, "simpleButton2");
            this.simpleButton2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton2.Appearance.Font")));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // tbl_ItemsOfTaxTableAdapter
            // 
            this.tbl_ItemsOfTaxTableAdapter.ClearBeforeFill = true;
            // 
            // txtCodeTypeID
            // 
            resources.ApplyResources(this.txtCodeTypeID, "txtCodeTypeID");
            this.txtCodeTypeID.Name = "txtCodeTypeID";
            // 
            // LangTxtBox
            // 
            resources.ApplyResources(this.LangTxtBox, "LangTxtBox");
            this.LangTxtBox.Name = "LangTxtBox";
            // 
            // simpleButton3
            // 
            resources.ApplyResources(this.simpleButton3, "simpleButton3");
            this.simpleButton3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton3.Appearance.Font")));
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            resources.ApplyResources(this.simpleButton4, "simpleButton4");
            this.simpleButton4.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton4.Appearance.Font")));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // tblItemsOfTaxBindingSource1
            // 
            this.tblItemsOfTaxBindingSource1.DataMember = "Tbl_ItemsOfTax";
            this.tblItemsOfTaxBindingSource1.DataSource = this.itemsDs;
            // 
            // ItemsFrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.LangTxtBox);
            this.Controls.Add(this.txtCodeTypeID);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodeName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUniqueNumber);
            this.Controls.Add(this.txtCodeType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Name = "ItemsFrm";
            this.Load += new System.EventHandler(this.ItemsFrm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblItemsOfTaxBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblItemsOfTaxBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodeName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUniqueNumber;
        private System.Windows.Forms.TextBox txtCodeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ItemsDs itemsDs;
        private System.Windows.Forms.BindingSource tblItemsOfTaxBindingSource;
        private ItemsDsTableAdapters.Tbl_ItemsOfTaxTableAdapter tbl_ItemsOfTaxTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uniqueNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtCodeTypeID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox LangTxtBox;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private System.Windows.Forms.BindingSource tblItemsOfTaxBindingSource1;
    }
}