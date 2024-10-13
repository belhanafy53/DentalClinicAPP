namespace DentalClinicAPP.Forms.BasicsCode
{
    partial class ItemsNFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsNFrm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.Nametxt = new System.Windows.Forms.TextBox();
            this.grbNodeSelected = new System.Windows.Forms.GroupBox();
            this.txtRefItem = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSelected = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBranchID = new System.Windows.Forms.TextBox();
            this.txtStoreCode = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbItemStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSelected = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.LangTxtBox = new System.Windows.Forms.TextBox();
            this.txtRefItemTaxID = new System.Windows.Forms.TextBox();
            this.itemsDs = new DentalClinicAPP.ItemsDs();
            this.tblItemsOfTaxBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbl_ItemsOfTaxTableAdapter = new DentalClinicAPP.ItemsDsTableAdapters.Tbl_ItemsOfTaxTableAdapter();
            this.grbNodeSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblItemsOfTaxBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.HotTracking = true;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Name = "treeView1";
            this.treeView1.StateImageList = this.imageList1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.Click += new System.EventHandler(this.treeView1_Click);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-folder-30.png");
            this.imageList1.Images.SetKeyName(1, "icons8-opened-folder-30.png");
            this.imageList1.Images.SetKeyName(2, "icons8-general-ledger-40.png");
            this.imageList1.Images.SetKeyName(3, "icons8-general-ledger-40.png");
            this.imageList1.Images.SetKeyName(4, "icons8-opened-folder-48.png");
            this.imageList1.Images.SetKeyName(5, "icons8-opened-folder-100.png");
            this.imageList1.Images.SetKeyName(6, "icons8-folder-48.png");
            this.imageList1.Images.SetKeyName(7, "open-file.png");
            this.imageList1.Images.SetKeyName(8, "folder.png");
            this.imageList1.Images.SetKeyName(9, "open-folder.png");
            this.imageList1.Images.SetKeyName(10, "folders.png");
            this.imageList1.Images.SetKeyName(11, "folder (1).png");
            this.imageList1.Images.SetKeyName(12, "open-folder (1).png");
            this.imageList1.Images.SetKeyName(13, "folder (2).png");
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // Nametxt
            // 
            resources.ApplyResources(this.Nametxt, "Nametxt");
            this.Nametxt.Name = "Nametxt";
            this.Nametxt.TextChanged += new System.EventHandler(this.Nametxt_TextChanged);
            this.Nametxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nametxt_KeyDown);
            // 
            // grbNodeSelected
            // 
            resources.ApplyResources(this.grbNodeSelected, "grbNodeSelected");
            this.grbNodeSelected.BackColor = System.Drawing.Color.White;
            this.grbNodeSelected.Controls.Add(this.txtRefItem);
            this.grbNodeSelected.Controls.Add(this.label13);
            this.grbNodeSelected.Controls.Add(this.label12);
            this.grbNodeSelected.Controls.Add(this.txtBranch);
            this.grbNodeSelected.Controls.Add(this.label11);
            this.grbNodeSelected.Controls.Add(this.label9);
            this.grbNodeSelected.Controls.Add(this.textBox1);
            this.grbNodeSelected.Controls.Add(this.label6);
            this.grbNodeSelected.Controls.Add(this.label7);
            this.grbNodeSelected.Controls.Add(this.simpleButton4);
            this.grbNodeSelected.Controls.Add(this.simpleButton2);
            this.grbNodeSelected.Controls.Add(this.simpleButton1);
            this.grbNodeSelected.Controls.Add(this.label2);
            this.grbNodeSelected.Controls.Add(this.txtSelected);
            this.grbNodeSelected.Controls.Add(this.label3);
            this.grbNodeSelected.Controls.Add(this.label1);
            this.grbNodeSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grbNodeSelected.Name = "grbNodeSelected";
            this.grbNodeSelected.TabStop = false;
            // 
            // txtRefItem
            // 
            resources.ApplyResources(this.txtRefItem, "txtRefItem");
            this.txtRefItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtRefItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtRefItem.Name = "txtRefItem";
            this.txtRefItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRefItem_KeyDown);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // txtBranch
            // 
            resources.ApplyResources(this.txtBranch, "txtBranch");
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Name = "label9";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // simpleButton4
            // 
            resources.ApplyResources(this.simpleButton4, "simpleButton4");
            this.simpleButton4.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton4.Appearance.Font")));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
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
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton1.Appearance.Font")));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Name = "label2";
            // 
            // txtSelected
            // 
            resources.ApplyResources(this.txtSelected, "txtSelected");
            this.txtSelected.Name = "txtSelected";
            this.txtSelected.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSelected_KeyDown);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBranchID
            // 
            resources.ApplyResources(this.txtBranchID, "txtBranchID");
            this.txtBranchID.Name = "txtBranchID";
            // 
            // txtStoreCode
            // 
            resources.ApplyResources(this.txtStoreCode, "txtStoreCode");
            this.txtStoreCode.Name = "txtStoreCode";
            this.txtStoreCode.TextChanged += new System.EventHandler(this.txtStoreCode_TextChanged);
            this.txtStoreCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStoreCode_KeyDown);
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox2_MouseClick);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox1_MouseClick);
            // 
            // cmbItemStatus
            // 
            resources.ApplyResources(this.cmbItemStatus, "cmbItemStatus");
            this.cmbItemStatus.FormattingEnabled = true;
            this.cmbItemStatus.Name = "cmbItemStatus";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cmbSelected
            // 
            resources.ApplyResources(this.cmbSelected, "cmbSelected");
            this.cmbSelected.FormattingEnabled = true;
            this.cmbSelected.Items.AddRange(new object[] {
            resources.GetString("cmbSelected.Items"),
            resources.GetString("cmbSelected.Items1"),
            resources.GetString("cmbSelected.Items2"),
            resources.GetString("cmbSelected.Items3")});
            this.cmbSelected.Name = "cmbSelected";
            this.cmbSelected.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSelected_KeyDown);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // simpleButton3
            // 
            resources.ApplyResources(this.simpleButton3, "simpleButton3");
            this.simpleButton3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton3.Appearance.Font")));
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Name = "dataGridView1";
            // 
            // simpleButton5
            // 
            resources.ApplyResources(this.simpleButton5, "simpleButton5");
            this.simpleButton5.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton5.Appearance.Font")));
            this.simpleButton5.Appearance.Options.UseFont = true;
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton6
            // 
            resources.ApplyResources(this.simpleButton6, "simpleButton6");
            this.simpleButton6.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton6.Appearance.Font")));
            this.simpleButton6.Appearance.Options.UseFont = true;
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // LangTxtBox
            // 
            resources.ApplyResources(this.LangTxtBox, "LangTxtBox");
            this.LangTxtBox.Name = "LangTxtBox";
            // 
            // txtRefItemTaxID
            // 
            resources.ApplyResources(this.txtRefItemTaxID, "txtRefItemTaxID");
            this.txtRefItemTaxID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtRefItemTaxID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtRefItemTaxID.Name = "txtRefItemTaxID";
            // 
            // itemsDs
            // 
            this.itemsDs.DataSetName = "ItemsDs";
            this.itemsDs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblItemsOfTaxBindingSource
            // 
            this.tblItemsOfTaxBindingSource.DataMember = "Tbl_ItemsOfTax";
            this.tblItemsOfTaxBindingSource.DataSource = this.itemsDs;
            // 
            // tbl_ItemsOfTaxTableAdapter
            // 
            this.tbl_ItemsOfTaxTableAdapter.ClearBeforeFill = true;
            // 
            // ItemsNFrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRefItemTaxID);
            this.Controls.Add(this.LangTxtBox);
            this.Controls.Add(this.txtBranchID);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton6);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.cmbItemStatus);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cmbSelected);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.txtStoreCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Nametxt);
            this.Controls.Add(this.grbNodeSelected);
            this.Name = "ItemsNFrm";
            this.Load += new System.EventHandler(this.ItemsNFrm_Load);
            this.grbNodeSelected.ResumeLayout(false);
            this.grbNodeSelected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsDs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblItemsOfTaxBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Nametxt;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox grbNodeSelected;
        private System.Windows.Forms.ComboBox cmbSelected;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSelected;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStoreCode;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private System.Windows.Forms.ComboBox cmbItemStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox LangTxtBox;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBranchID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRefItem;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRefItemTaxID;
        private ItemsDs itemsDs;
        private System.Windows.Forms.BindingSource tblItemsOfTaxBindingSource;
        private ItemsDsTableAdapters.Tbl_ItemsOfTaxTableAdapter tbl_ItemsOfTaxTableAdapter;
    }
}