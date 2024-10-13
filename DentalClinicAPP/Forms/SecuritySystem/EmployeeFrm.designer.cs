namespace DentalClinicAPP.Forms.SecuritySystem
{
    partial class EmployeeFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeFrm));
            this.label4 = new System.Windows.Forms.Label();
            this.Nametxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Nationaltxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtManagement = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerStatus = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxStatusDetails = new System.Windows.Forms.ComboBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.jobtxtbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GrdEmployee = new System.Windows.Forms.DataGridView();
            this.Emailtxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.managements = new DentalClinicAPP.Managements();
            this.tblManagementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbl_ManagementTableAdapter = new DentalClinicAPP.ManagementsTableAdapters.Tbl_ManagementTableAdapter();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.LangTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblManagementBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // Nametxt
            // 
            resources.ApplyResources(this.Nametxt, "Nametxt");
            this.Nametxt.Name = "Nametxt";
            this.Nametxt.TextChanged += new System.EventHandler(this.Nametxt_TextChanged);
            this.Nametxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nametxt_KeyDown);
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
            // Nationaltxt
            // 
            resources.ApplyResources(this.Nationaltxt, "Nationaltxt");
            this.Nationaltxt.Name = "Nationaltxt";
            this.Nationaltxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nationaltxt_KeyDown);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtManagement);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dateTimePickerStatus);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.Nametxt);
            this.groupBox1.Controls.Add(this.comboBoxStatusDetails);
            this.groupBox1.Controls.Add(this.comboBoxStatus);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.jobtxtbox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.Nationaltxt);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtManagement
            // 
            resources.ApplyResources(this.txtManagement, "txtManagement");
            this.txtManagement.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtManagement.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtManagement.Name = "txtManagement";
            this.txtManagement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManagement_KeyDown);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // dateTimePickerStatus
            // 
            resources.ApplyResources(this.dateTimePickerStatus, "dateTimePickerStatus");
            this.dateTimePickerStatus.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStatus.Name = "dateTimePickerStatus";
            this.dateTimePickerStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePickerStatus_KeyDown);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBoxStatusDetails
            // 
            resources.ApplyResources(this.comboBoxStatusDetails, "comboBoxStatusDetails");
            this.comboBoxStatusDetails.FormattingEnabled = true;
            this.comboBoxStatusDetails.Items.AddRange(new object[] {
            resources.GetString("comboBoxStatusDetails.Items"),
            resources.GetString("comboBoxStatusDetails.Items1"),
            resources.GetString("comboBoxStatusDetails.Items2"),
            resources.GetString("comboBoxStatusDetails.Items3"),
            resources.GetString("comboBoxStatusDetails.Items4"),
            resources.GetString("comboBoxStatusDetails.Items5"),
            resources.GetString("comboBoxStatusDetails.Items6"),
            resources.GetString("comboBoxStatusDetails.Items7"),
            resources.GetString("comboBoxStatusDetails.Items8"),
            resources.GetString("comboBoxStatusDetails.Items9"),
            resources.GetString("comboBoxStatusDetails.Items10"),
            resources.GetString("comboBoxStatusDetails.Items11")});
            this.comboBoxStatusDetails.Name = "comboBoxStatusDetails";
            this.comboBoxStatusDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxStatusDetails_KeyDown);
            // 
            // comboBoxStatus
            // 
            resources.ApplyResources(this.comboBoxStatus, "comboBoxStatus");
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            resources.GetString("comboBoxStatus.Items"),
            resources.GetString("comboBoxStatus.Items1"),
            resources.GetString("comboBoxStatus.Items2"),
            resources.GetString("comboBoxStatus.Items3")});
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxStatus_KeyDown);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // jobtxtbox
            // 
            resources.ApplyResources(this.jobtxtbox, "jobtxtbox");
            this.jobtxtbox.Name = "jobtxtbox";
            this.jobtxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.jobtxtbox_KeyDown);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButton2_KeyDown);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioButton1_KeyDown);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.GrdEmployee);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // GrdEmployee
            // 
            resources.ApplyResources(this.GrdEmployee, "GrdEmployee");
            this.GrdEmployee.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GrdEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrdEmployee.Name = "GrdEmployee";
            this.GrdEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdEmployee_KeyDown);
            this.GrdEmployee.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GrdEmployee_MouseClick);
            // 
            // Emailtxt
            // 
            resources.ApplyResources(this.Emailtxt, "Emailtxt");
            this.Emailtxt.Name = "Emailtxt";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
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
            // simpleButton4
            // 
            resources.ApplyResources(this.simpleButton4, "simpleButton4");
            this.simpleButton4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton4.ImageOptions.SvgImage")));
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton5
            // 
            resources.ApplyResources(this.simpleButton5, "simpleButton5");
            this.simpleButton5.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton5.ImageOptions.SvgImage")));
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
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
            // simpleButton3
            // 
            resources.ApplyResources(this.simpleButton3, "simpleButton3");
            this.simpleButton3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("simpleButton3.Appearance.Font")));
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
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
            // EmployeeFrm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LangTxtBox);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton6);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Emailtxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Name = "EmployeeFrm";
            this.Load += new System.EventHandler(this.EmployeeFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblManagementBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Nationaltxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView GrdEmployee;
        private System.Windows.Forms.TextBox Emailtxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.TextBox Nametxt;
        public System.Windows.Forms.TextBox jobtxtbox;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxStatusDetails;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePickerStatus;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtManagement;
        private Managements managements;
        private System.Windows.Forms.BindingSource tblManagementBindingSource;
        private ManagementsTableAdapters.Tbl_ManagementTableAdapter tbl_ManagementTableAdapter;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private System.Windows.Forms.TextBox LangTxtBox;
    }
}