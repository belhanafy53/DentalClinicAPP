namespace DentalClinicAPP.Forms.SecuritySystem
{
    partial class MenuProgUnit_SysUnites
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
                //ecomponents.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuProgUnit_SysUnites));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tblSystemUnitesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.securityDs = new DentalClinicAPP.SecurityDs();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParent_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colForms_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colName_Ar = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tblMenuProgramUnitesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.tbl_SystemUnitesTableAdapter = new DentalClinicAPP.SecurityDsTableAdapters.Tbl_SystemUnitesTableAdapter();
            this.tbl_MenuProgramUnitesTableAdapter = new DentalClinicAPP.SecurityDsTableAdapters.Tbl_MenuProgramUnitesTableAdapter();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.LangTxtBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tblSystemUnitesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityDs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMenuProgramUnitesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.tblSystemUnitesBindingSource, "ID", true));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // tblSystemUnitesBindingSource
            // 
            this.tblSystemUnitesBindingSource.DataMember = "Tbl_SystemUnites";
            this.tblSystemUnitesBindingSource.DataSource = this.securityDs;
            // 
            // securityDs
            // 
            this.securityDs.DataSetName = "SecurityDs";
            this.securityDs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.treeList1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // treeList1
            // 
            resources.ApplyResources(this.treeList1, "treeList1");
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colID,
            this.colName,
            this.colParent_ID,
            this.colForms_ID,
            this.colName_Ar});
            this.treeList1.CustomizationFormBounds = new System.Drawing.Rectangle(740, 391, 252, 266);
            this.treeList1.DataSource = this.tblMenuProgramUnitesBindingSource;
            this.treeList1.FixedLineWidth = 3;
            this.treeList1.HorzScrollStep = 4;
            this.treeList1.MinWidth = 27;
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = true;
            this.treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeList1.OptionsFilter.ShowAllValuesInCheckedFilterPopup = false;
            this.treeList1.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.treeList1.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            this.treeList1.ParentFieldName = "Parent_ID";
            this.treeList1.TreeLevelWidth = 24;
            // 
            // colID
            // 
            resources.ApplyResources(this.colID, "colID");
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            // 
            // colParent_ID
            // 
            resources.ApplyResources(this.colParent_ID, "colParent_ID");
            this.colParent_ID.FieldName = "Parent_ID";
            this.colParent_ID.Name = "colParent_ID";
            // 
            // colForms_ID
            // 
            resources.ApplyResources(this.colForms_ID, "colForms_ID");
            this.colForms_ID.FieldName = "Forms_ID";
            this.colForms_ID.Name = "colForms_ID";
            // 
            // colName_Ar
            // 
            resources.ApplyResources(this.colName_Ar, "colName_Ar");
            this.colName_Ar.FieldName = "Name_Ar";
            this.colName_Ar.Name = "colName_Ar";
            // 
            // tblMenuProgramUnitesBindingSource
            // 
            this.tblMenuProgramUnitesBindingSource.DataMember = "Tbl_MenuProgramUnites";
            this.tblMenuProgramUnitesBindingSource.DataSource = this.securityDs;
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
            // tbl_SystemUnitesTableAdapter
            // 
            this.tbl_SystemUnitesTableAdapter.ClearBeforeFill = true;
            // 
            // tbl_MenuProgramUnitesTableAdapter
            // 
            this.tbl_MenuProgramUnitesTableAdapter.ClearBeforeFill = true;
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
            // LangTxtBox
            // 
            resources.ApplyResources(this.LangTxtBox, "LangTxtBox");
            this.LangTxtBox.Name = "LangTxtBox";
            // 
            // MenuProgUnit_SysUnites
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LangTxtBox);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MenuProgUnit_SysUnites";
            this.Load += new System.EventHandler(this.MenuProgUnit_SysUnites_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblSystemUnitesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityDs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMenuProgramUnitesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        //private FinancialSysDataSet10 financialSysDataSet102;
        //private FinancialSysDataSet10TableAdapters.Tbl_MenuProgramUnitesTableAdapter tbl_MenuProgramUnitesTableAdapter1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private SecurityDs securityDs;
        private System.Windows.Forms.BindingSource tblSystemUnitesBindingSource;
        private SecurityDsTableAdapters.Tbl_SystemUnitesTableAdapter tbl_SystemUnitesTableAdapter;
        private System.Windows.Forms.BindingSource tblMenuProgramUnitesBindingSource;
        private SecurityDsTableAdapters.Tbl_MenuProgramUnitesTableAdapter tbl_MenuProgramUnitesTableAdapter;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colForms_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParent_ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName_Ar;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private System.Windows.Forms.TextBox LangTxtBox;
        //private FinancialSysDataSet12TableAdapters.Tbl_MenuProgramUnitesTableAdapter tbl_MenuProgramUnitesTableAdapter;
    }
}