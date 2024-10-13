using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;
using DevComponents.DotNetBar.Controls;
using System.Runtime.Remoting.Contexts;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class CustomersFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        int Vint_ID = 0;
        int Vint_codGvrn = 0;
        public CustomersFrm()
        {
            InitializeComponent();
        }
        private void Dg_E()
        {


            CustomersDG.Columns["NameSectore"].HeaderText = "Sector";
            CustomersDG.Columns["CustomerCode"].HeaderText = "Customer Code";
            CustomersDG.Columns["CustomerName"].HeaderText = "Customer Name";
            CustomersDG.Columns["MotherCompCode"].HeaderText = "Mother Comp Code";
            CustomersDG.Columns["MotherCompName"].HeaderText = "Mother Comp Name";

            CustomersDG.Columns["TaxRecordNo"].HeaderText = "Tax record No.";

            CustomersDG.Columns["CountryCode"].HeaderText = "Country Code";
            CustomersDG.Columns["Governorate"].HeaderText = "Governorate";
            CustomersDG.Columns["City"].HeaderText = "City";

            CustomersDG.Columns["Address"].HeaderText = "Address";
            CustomersDG.Columns["CompanyType"].HeaderText = "Company Type";

            CustomersDG.Columns["ID"].Visible = false;

            CustomersDG.Columns["GovernerateID"].Visible = false;
            CustomersDG.Columns["CityId"].Visible = false;

            CustomersDG.Columns["AccountGuidID"].Visible = false;
            CustomersDG.Columns["BuildingNo"].Visible = false;
            CustomersDG.Columns["PostCode"].Visible = false;
            CustomersDG.Columns["SignStar"].Visible = false;
            CustomersDG.Columns["CompanyID"].Visible = false;

            CustomersDG.Columns["CustomerName"].Width = 150;
            CustomersDG.Columns["Address"].Width = 150;

            NameEtxt.Focus();
        }
        private void Dg_Ar()
        {
            CustomersDG.Columns["ID"].Visible = false;
            CustomersDG.Columns["NameSectore"].HeaderText = "القطاع";
            CustomersDG.Columns["CustomerCode"].HeaderText = "كود العميل";
            CustomersDG.Columns["CustomerName"].HeaderText = "اسم العميل";
            CustomersDG.Columns["MotherCompCode"].HeaderText = "كود الشركه الام";
            CustomersDG.Columns["MotherCompName"].HeaderText = "اسم الشركه الام";

            CustomersDG.Columns["TaxRecordNo"].HeaderText = "رقم التسجيل الضريبي";

            CustomersDG.Columns["CountryCode"].HeaderText = "كود البلد";
            CustomersDG.Columns["Governorate"].HeaderText = "المحافظه";
            CustomersDG.Columns["City"].HeaderText = "المدينه";

            CustomersDG.Columns["Address"].HeaderText = "العنوان";
            CustomersDG.Columns["CompanyType"].HeaderText = "طبيعة النشاط";
            

            CustomersDG.Columns["ID"].Visible = false;

            CustomersDG.Columns["GovernerateID"].Visible = false;
            CustomersDG.Columns["CityId"].Visible = false;


            CustomersDG.Columns["BuildingNo"].Visible = false;
            CustomersDG.Columns["PostCode"].Visible = false;
            CustomersDG.Columns["SignStar"].Visible = false;
            CustomersDG.Columns["CompanyID"].Visible = false;
            CustomersDG.Columns["AccountGuidID"].Visible = false;
            

            CustomersDG.Columns["CustomerName"].Width = 150;
            CustomersDG.Columns["Address"].Width = 150;


            NameEtxt.Focus();
        }
        private void CustomersFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'accountGuidDs.DataTable1' table. You can move, or remove it, as needed.




            RowIDtxt.Text = "";
            if (Program.GlbV_Language == "en")
            {
                LangTxtBox.Text = "en";
                simpleButton3_Click(sender, e);

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                LangTxtBox.Text = "ar-EG";
                simpleButton4_Click(sender, e);

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            ClearData();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Customer.OrderBy(x=>x.ID).ToList();
            CustomersDG.DataSource = listAnalysis;
            Dg_E();
            LangTxtBox.Text = "en";

            CountryCmb.DataSource = DNtMDB.Tbl_Country.ToList();
            CountryCmb.DisplayMember = "dec_en";
            CountryCmb.ValueMember = "code";
            CountryCmb.Text = "Choose a Country";

            GovernorateCmb.DataSource = DNtMDB.Tbl_Governerate.ToList();
            GovernorateCmb.DisplayMember = "Name_Ar";
            GovernorateCmb.ValueMember = "ID";
            GovernorateCmb.Text = "Choose a Governorate";

            // TODO: This line of code loads data into the 'managements.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.managements.DataTable1);
            CompanyCmb.DisplayMember = "Name";
            CompanyCmb.ValueMember = "ID";
            CompanyCmb.Text = "Choose a Company";

            CompanyTypeCmb.DataSource = DNtMDB.Tbl_CompanyType.ToList();
            CompanyTypeCmb.DisplayMember = "Name_E";
            CompanyTypeCmb.ValueMember = "code";
            CompanyTypeCmb.Text = "Choose a Company Type";

            this.dataTable1TableAdapter1.Fill(this.accountGuidDs.DataTable1);

            NameEtxt.Focus();

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");

            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            ClearData();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Customer.OrderBy(x => x.ID).ToList();
            CustomersDG.DataSource = listAnalysis;
            Dg_Ar();
            LangTxtBox.Text = "ar-EG";
            CountryCmb.DataSource = DNtMDB.Tbl_Country.ToList();
            CountryCmb.DisplayMember = "dec_ar";
            CountryCmb.ValueMember = "code";
            CountryCmb.Text = "اختر البلد";

            GovernorateCmb.DataSource = DNtMDB.Tbl_Governerate.ToList();
            GovernorateCmb.DisplayMember = "Name_E";
            GovernorateCmb.ValueMember = "ID";
            GovernorateCmb.Text = "اختر المحافظه";

            // TODO: This line of code loads data into the 'managements.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.managements.DataTable1);
            CompanyCmb.DisplayMember = "Name";
            CompanyCmb.ValueMember = "ID";
            CompanyCmb.Text = "اختر الشركه الام";

            CompanyTypeCmb.DataSource = DNtMDB.Tbl_CompanyType.ToList();
            CompanyTypeCmb.DisplayMember = "Name_Ar";
            CompanyTypeCmb.ValueMember = "code";
            CompanyTypeCmb.Text = "اختر طبيعة النشاط";
            this.dataTable1TableAdapter1.Fill(this.accountGuidDs.DataTable1);
            NameEtxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en")
            {
                var ListOurCompany = DNtMDB.Tbl_Customer.FirstOrDefault(x => x.ID == Vint_ID);

                if (NameEtxt.Text == "")
                {
                    MessageBox.Show("Please Enter Sector Name ");
                    NameEtxt.Focus();
                }
                else if (Codetxt.Text == "")
                {
                    MessageBox.Show("Please Enter Code Customer ");
                    Sectortxt.Focus();
                }
                else if (TaxRecordtxt.Text == "")
                {
                    MessageBox.Show("Please Enter Tax Register Number Of Customer");
                    TaxRecordtxt.Focus();
                }
                else if (CountryCmb.Text == "")
                {
                    MessageBox.Show("Please Enter Country Of Customer");
                    CountryCmb.Focus();
                }
                else if (GovernorateCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter Governorate Of Customer");
                    GovernorateCmb.Focus();
                }
                else if (CityCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter city Of Customer");
                    CityCmb.Focus();
                }
                else if (Addresstxt.Text == "")
                {
                    MessageBox.Show("Please Enter Governorate Of Customer");
                    Addresstxt.Focus();
                }
                else if (CompanyCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter Company Customer");
                    CompanyCmb.Focus();
                }
                else if (CompanyTypeCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter Company Type");
                    CompanyCmb.Focus();
                }
                else if (RowIDtxt.Text == "")

                {
                    int Vint_AccGuid = 0;
                    if (RowAccGuidUptxt.Text != "")
                    {
                        Vint_AccGuid = int.Parse(RowAccGuidUptxt.Text.Trim());
                    }
                     
                    Tbl_Customer Cust = new Tbl_Customer()
                    {
                        NameSectore = Sectortxt.Text.Trim(),
                        CustomerCode = Codetxt.Text.Trim(),
                        CustomerName = NameEtxt.Text.Trim(),
                        MotherCompCode = CompanyCodetxt.Text.Trim(),
                        MotherCompName = CompanyNametxt.Text.Trim(),
                        TaxRecordNo =Convert.ToInt32( TaxRecordtxt.Text.Trim()),
                        CountryCode = CountryCmb.SelectedValue.ToString().Trim(),
                        Governorate = GovernorateNametxt.Text.Trim(),
                        GovernerateID = int.Parse(GovernorateCmb.SelectedValue.ToString().Trim()),
                        City = CityNametxt.Text.Trim(),
                        CityId = int.Parse(CityCmb.SelectedValue.ToString().Trim()),
                        Address = Addresstxt.Text.Trim(),
                        BuildingNo = int.Parse(Buildingtxt.Text.Trim()),
                        PostCode = PostalCodetxt.Text.Trim(),
                        CompanyID = int.Parse(CompanyCmb.SelectedValue.ToString().Trim()),
                        AccountGuidID = Vint_AccGuid,
                        CompanyType = CompanyTypeCmb.SelectedValue.ToString().Trim(),
                        AccountNO = Convert.ToInt32( textBox8.Text.Trim())

                    };
                    DNtMDB.Tbl_Customer.Add(Cust);
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Add Customer Datay",
                        TableName = "Tbl_Customer",
                        TableRecordId = Cust.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "CustomersFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //*********************

                    MessageBox.Show("Saved");
                    simpleButton3_Click(sender, e);
                }
                else if (RowIDtxt.Text != "")
                {
                    ListOurCompany.NameSectore = Sectortxt.Text.Trim();
                    ListOurCompany.CustomerCode = Codetxt.Text.Trim();
                    ListOurCompany.CustomerName = NameEtxt.Text.Trim();
                    ListOurCompany.MotherCompCode = CompanyCodetxt.Text.Trim();
                    ListOurCompany.MotherCompName = CompanyNametxt.Text.Trim();
                    if (TaxRecordtxt.Text != "")
                    {
                        ListOurCompany.TaxRecordNo = Convert.ToInt32(TaxRecordtxt.Text.Trim());
                    }
                    ListOurCompany.CountryCode = CountryCmb.SelectedValue.ToString().Trim();
                    ListOurCompany.Governorate = GovernorateNametxt.Text.Trim();
                    ListOurCompany.GovernerateID = int.Parse(GovernorateCmb.SelectedValue.ToString().Trim());
                    ListOurCompany.City = CityNametxt.Text.Trim();
                    ListOurCompany.CityId = int.Parse(CityCmb.SelectedValue.ToString().Trim());
                    if (Addresstxt.Text != "")
                    {
                        ListOurCompany.Address = Addresstxt.Text.Trim();
                    }

                    if (Buildingtxt.Text != "")
                    { ListOurCompany.BuildingNo = int.Parse(Buildingtxt.Text.Trim()); }

                    ListOurCompany.PostCode = PostalCodetxt.Text.Trim();
                    ListOurCompany.CompanyID = int.Parse(CompanyCmb.SelectedValue.ToString().Trim());

                    if (RowAccGuidUptxt.Text != "")
                    {
                        ListOurCompany.AccountGuidID = int.Parse(RowAccGuidUptxt.Text.Trim());
                        ListOurCompany.AccountNO = Convert.ToInt32(textBox8.Text.Trim());
                    }
                    if (CompanyTypeCmb.SelectedValue != null)

                    { ListOurCompany.CompanyType = CompanyTypeCmb.SelectedValue.ToString().Trim(); }
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Update Customers Data",
                        TableName = "Tbl_Customer",
                        TableRecordId = ListOurCompany.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "CustomersFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    MessageBox.Show("Updated");
                    simpleButton3_Click(sender, e);
                }
            }
            else if (LangTxtBox.Text == "ar-EG")
            {
                var ListOurCompany = DNtMDB.Tbl_Customer.FirstOrDefault(x => x.ID == Vint_ID);

                if (NameEtxt.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل اسم  للعميل");
                    NameEtxt.Focus();
                }
                else if (Sectortxt.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل اسم القطاع");
                    Sectortxt.Focus();
                }
                else if (TaxRecordtxt.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل رقم التسجيل الضريبي للعميل");
                    TaxRecordtxt.Focus();
                }
                else if (CountryCmb.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل بلد للعميل");
                    CountryCmb.Focus();
                }
                else if (GovernorateCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("من فضلك ادخل محافظة للعميل");
                    GovernorateCmb.Focus();
                }
                else if (CityCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("من فضلك ادخل مدينة  للعميل");
                    CityCmb.Focus();
                }
                else if (Addresstxt.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل عنوان للعميل");
                    Addresstxt.Focus();
                }
                else if (CompanyCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("من فضلك ادخل شركة للعميل");
                    CompanyCmb.Focus();
                }
                else if (ListOurCompany == null)

                {
                    int GovernerateID = GovernorateCmb.SelectedValue != null ? int.Parse(GovernorateCmb.SelectedValue.ToString().Trim()) : 0;
                    int CityId = CityCmb.SelectedValue != null ? int.Parse(CityCmb.SelectedValue.ToString().Trim()) : 0;
                    int CompanyID = CompanyCmb.SelectedValue != null ? int.Parse(CompanyCmb.SelectedValue.ToString().Trim()) : 0;
                    int BuildingNo = Buildingtxt.Text != null ? int.Parse("1") : 0;
                    //Tbl_Customer Cust = new Tbl_Customer()
                    //{

                    //    NameSectore = Sectortxt.Text.Trim(),
                    //    CustomerCode = Codetxt.Text.Trim(),
                    //    CustomerName = NameEtxt.Text.Trim(),
                    //    MotherCompCode = CompanyCodetxt.Text.Trim(),
                    //    MotherCompName = CompanyNametxt.Text.Trim(),
                    //    TaxRecordNo = Convert.ToInt32(TaxRecordtxt.Text.Trim()),
                    //    CountryCode = CountryCmb.SelectedValue.ToString(),
                    //    Governorate = GovernorateNametxt.Text.Trim(),
                    //    GovernerateID = GovernerateID,
                    //    City = CityNametxt.Text.Trim(),
                    //    CityId = CityId,
                    //    Address = Addresstxt.Text.Trim(),
                    //    BuildingNo = BuildingNo,
                    //    PostCode = PostalCodetxt.Text.Trim(),
                    //    CompanyID = CompanyID,
                    //    AccountNO = Convert.ToInt32(textBox8.Text.Trim())

                    //};
                    int taxRecordNo;
                    int accountNo;

                    // محاولة تحويل قيمة TaxRecordtxt إلى عدد صحيح
                    if (!int.TryParse(TaxRecordtxt.Text.Trim(), out taxRecordNo))
                    {
                        // معالجة إدخال غير صالح (مثل إظهار رسالة للمستخدم)
                        MessageBox.Show("رقم السجل الضريبي غير صالح.");
                        return; // أو أي إجراء آخر تراه مناسبًا
                    }

                    // محاولة تحويل قيمة textBox8 إلى عدد صحيح
                    if (!int.TryParse(textBox8.Text.Trim(), out accountNo))
                    {
                        // معالجة إدخال غير صالح
                        MessageBox.Show("رقم الحساب غير صالح.");
                        return; // أو أي إجراء آخر تراه مناسبًا
                    }

                    Tbl_Customer Cust = new Tbl_Customer()
                    {
                        NameSectore = Sectortxt.Text.Trim(),
                        CustomerCode = Codetxt.Text.Trim(),
                        CustomerName = NameEtxt.Text.Trim(),
                        MotherCompCode = CompanyCodetxt.Text.Trim(),
                        MotherCompName = CompanyNametxt.Text.Trim(),
                        TaxRecordNo = taxRecordNo,
                        CountryCode = CountryCmb.SelectedValue.ToString(),
                        Governorate = GovernorateNametxt.Text.Trim(),
                        GovernerateID = GovernerateID,
                        City = CityNametxt.Text.Trim(),
                        CityId = CityId,
                        Address = Addresstxt.Text.Trim(),
                        BuildingNo = BuildingNo,
                        PostCode = PostalCodetxt.Text.Trim(),
                        CompanyID = CompanyID,
                        AccountNO = accountNo,
                        CompanyType = CompanyTypeCmb.SelectedValue.ToString().Trim()
                    };

                    DNtMDB.Tbl_Customer.Add(Cust);
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "اضافة بيانات عميل",
                        TableName = "Tbl_Customer",
                        TableRecordId = Cust.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "CustomersFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //*********************

                    MessageBox.Show("تم الحفظ");
                    simpleButton4_Click(sender, e);
                }
                else if (ListOurCompany != null)
                {
                    ListOurCompany.NameSectore = Sectortxt.Text;
                    ListOurCompany.CustomerCode = Codetxt.Text;
                    ListOurCompany.CustomerName = NameEtxt.Text;
                    ListOurCompany.MotherCompCode = CompanyCodetxt.Text;
                    ListOurCompany.MotherCompName = CompanyNametxt.Text;

                    ListOurCompany.TaxRecordNo = Convert.ToInt32(TaxRecordtxt.Text.Trim());
                    ListOurCompany.CountryCode = CountryCmb.SelectedValue.ToString();
                    ListOurCompany.Governorate = GovernorateNametxt.Text;
                    ListOurCompany.GovernerateID = int.Parse(GovernorateCmb.SelectedValue.ToString());
                    ListOurCompany.City = CityNametxt.Text;
                    ListOurCompany.CityId = int.Parse(CityCmb.SelectedValue.ToString());
                    
                    if (Addresstxt.Text != "")
                    {
                        ListOurCompany.Address = Addresstxt.Text;
                    }

                    if (Buildingtxt.Text != "")
                    { ListOurCompany.BuildingNo = int.Parse(Buildingtxt.Text); }

                    ListOurCompany.PostCode = PostalCodetxt.Text;
                    ListOurCompany.CompanyID = int.Parse(CompanyCmb.SelectedValue.ToString());

                    if (RowAccGuidUptxt.Text != "")
                    {
                        ListOurCompany.AccountGuidID = int.Parse(RowAccGuidUptxt.Text);
                        ListOurCompany.AccountNO = Convert.ToInt32(textBox8.Text.Trim());
                    }
                    if (CompanyTypeCmb.SelectedValue != null)

                    { ListOurCompany.CompanyType = CompanyTypeCmb.SelectedValue.ToString().Trim(); }
                    DNtMDB.SaveChanges();


                    
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "تعديل بيانات عميل",
                        TableName = "Tbl_Customer",
                        TableRecordId = ListOurCompany.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "CustomersFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    MessageBox.Show("تم التعديل");
                    simpleButton4_Click(sender, e);
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 4 && w.ProcdureId == 4);
            //if (validationSaveUser != null)
            //{
            int Vint_D1rows = CustomersDG.RowCount;

            if (Vint_D1rows != 0)
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this Customer  ?", "Delete Customer ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {


                        var resultR = DNtMDB.Tbl_Customer.Find(Vint_ID);
                        DNtMDB.Tbl_Customer.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Currency",
                            TableName = "Tbl_Customer",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Currencies"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            simpleButton3_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            simpleButton4_Click(sender, e);
                        }

                    }


                    NameEtxt.Select();
                    this.ActiveControl = NameEtxt;
                    NameEtxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا العميل  ؟", "حذف عميل ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_Customer.Find(Vint_ID);
                        DNtMDB.Tbl_Customer.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Customer",
                            TableName = "Tbl_Customer",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "CustomersFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            simpleButton3_Click(sender, e);

                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            simpleButton4_Click(sender, e);
                        }

                    }


                    NameEtxt.Select();
                    this.ActiveControl = NameEtxt;
                    NameEtxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Customer to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر العميل المراد حذفه");
                }

                NameEtxt.Select();
                this.ActiveControl = NameEtxt;
                NameEtxt.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Codetxt.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GovernorateCmb.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Sectortxt.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CountryCmb.Focus();
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CompanyCmb.Focus();
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CityCmb.Focus();
            }
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Addresstxt.Focus();
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buildingtxt.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PostalCodetxt.Focus();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox8.Focus();
            }
        }

        private void NameArtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TaxRecordtxt.Focus();
            }
        }
        private void ClearData()
        {
            Vint_ID = 0;
            RowIDtxt.Text = "";

            Sectortxt.Text = "";


            Codetxt.Text = "";

            NameEtxt.Text = Codetxt.Text = "";


            CompanyCodetxt.Text = "";

            CompanyNametxt.Text = "";

            TaxRecordtxt.Text = "";

            CountryCmb.SelectedValue = "";

            GovernorateNametxt.Text = "";

            CityNametxt.Text = "";
            Addresstxt.Text = "";
            Buildingtxt.Text = "";
            PostalCodetxt.Text = "";

            textBox1.Text = "";
            textBox8.Text = "";
            RowAccGuidUptxt.Text = "";
            RowIDtxt.Text = "";
            GovernorateCmb.SelectedIndex = -1;

            CityCmb.SelectedIndex = -1;

            CompanyTypeCmb.SelectedIndex = -1;

            CompanyCmb.SelectedIndex = -1;

        }
        private void CustomersDG_MouseClick(object sender, MouseEventArgs e)
        {


            ClearData();
            Vint_ID = int.Parse(CustomersDG.CurrentRow.Cells[0].Value.ToString());
            RowIDtxt.Text = Vint_ID.ToString();
            if (CustomersDG.CurrentRow.Cells[1].Value != null)
            {
                Sectortxt.Text = CustomersDG.CurrentRow.Cells[1].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[2].Value != null)
            {
                Codetxt.Text = CustomersDG.CurrentRow.Cells[2].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[3].Value != null)
            {
                NameEtxt.Text = CustomersDG.CurrentRow.Cells[3].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[4].Value != null)
            {
                CompanyCodetxt.Text = CustomersDG.CurrentRow.Cells[4].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[5].Value != null)
            {
                CompanyNametxt.Text = CustomersDG.CurrentRow.Cells[5].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[6].Value != null)
            {
                TaxRecordtxt.Text = CustomersDG.CurrentRow.Cells[6].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[7].Value != null)
            {
                CountryCmb.SelectedValue = CustomersDG.CurrentRow.Cells[7].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[8].Value != null)
            {
                GovernorateNametxt.Text = CustomersDG.CurrentRow.Cells[8].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[9].Value != null)
            {
                CityNametxt.Text = CustomersDG.CurrentRow.Cells[9].Value.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[10].Value != null)
            { Addresstxt.Text = CustomersDG.CurrentRow.Cells[10].Value.ToString(); }
            if (CustomersDG.CurrentRow.Cells[11].Value != null)
            { Buildingtxt.Text = CustomersDG.CurrentRow.Cells[11].Value.ToString(); }
            if (CustomersDG.CurrentRow.Cells[12].Value != null)
            { PostalCodetxt.Text = CustomersDG.CurrentRow.Cells[12].Value.ToString(); }

            if (CustomersDG.CurrentRow.Cells[14].Value != null)
            {
                int Vint_Governorate = int.Parse(CustomersDG.CurrentRow.Cells[14].Value.ToString());
                GovernorateCmb.SelectedValue = int.Parse(CustomersDG.CurrentRow.Cells[14].Value.ToString());
                CityCmb.DataSource = DNtMDB.Tbl_Cities.Where(x => x.GovernorateID == Vint_Governorate).ToList();
                CityCmb.DisplayMember = "Name_E";
                CityCmb.ValueMember = "ID";
                CityCmb.SelectedValue = int.Parse(CustomersDG.CurrentRow.Cells[15].Value.ToString());
            }
            if (CustomersDG.CurrentRow.Cells[16].Value != null)
            {
                CompanyCmb.SelectedValue = int.Parse(CustomersDG.CurrentRow.Cells[16].Value.ToString());
            }
            if (CustomersDG.CurrentRow.Cells[17].Value != null && CustomersDG.CurrentRow.Cells[17].Value.ToString() != "0")
            {
                int Vint_AccGuid = int.Parse(CustomersDG.CurrentRow.Cells[17].Value.ToString());
                var ListAccGuid = DNtMDB.Tbl_Accounting_Guid.Where(x => x.ID == Vint_AccGuid).ToList();
                textBox8.Text = ListAccGuid[0].Account_NO.ToString();
                textBox1.Text = ListAccGuid[0].Name.ToString();
            }
            if (CustomersDG.CurrentRow.Cells[18].Value != null)
            {
               
                string Vst_CompanyTypeCode = CustomersDG.CurrentRow.Cells[18].Value.ToString();
                string vst_CompanyType = DNtMDB.Tbl_CompanyType.FirstOrDefault(x => x.Code == Vst_CompanyTypeCode).Name_E.ToString();
                CompanyTypeCmb.SelectedValue = Vst_CompanyTypeCode;
                CompanyTypeCmb.Text = vst_CompanyType;
            }
        }

        private void GovernorateCmb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en" && Program.GlbV_Language == "en")
            {
                Vint_codGvrn = int.Parse(GovernorateCmb.SelectedValue.ToString());
                CityCmb.DataSource = DNtMDB.Tbl_Cities.Where(x => x.GovernorateID == Vint_codGvrn).ToList();
                CityCmb.DisplayMember = "Name_Ar";
                CityCmb.ValueMember = "ID";
                CityCmb.Text = "Choose a City";
            }
            else if (LangTxtBox.Text == "ar-EG" && Program.GlbV_Language == "ar-EG")
            {
                Vint_codGvrn = int.Parse(GovernorateCmb.SelectedValue.ToString());
                CityCmb.DataSource = DNtMDB.Tbl_Cities.Where(x => x.GovernorateID == Vint_codGvrn).ToList();
                CityCmb.DisplayMember = "Name_E";
                CityCmb.ValueMember = "ID";
                CityCmb.Text = "اختر المدينه";
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            textBox8.BackColor = Color.Coral;
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox8.Text != string.Empty)
            {
                if (dataGridViewX1.Rows.Count == 1)
                {
                    textBox1.Text = dataGridViewX1.CurrentRow.Cells[1].Value.ToString();
                    CompanyTypeCmb.Focus();

                }
                if (dataGridViewX1.Rows.Count > 1)
                {
                    dataGridViewX1.Focus();
                }


                //simpleButton1.Focus();
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            textBox8.BackColor = Color.White;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string Vst_AccountNoSearch = textBox8.Text.Trim();
            this.dataTable1TableAdapter1.FillByName(this.accountGuidDs.DataTable1, Vst_AccountNoSearch);
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox8.Text = dataGridViewX1.CurrentRow.Cells[2].Value.ToString().Trim();
                textBox1.Text = dataGridViewX1.CurrentRow.Cells[1].Value.ToString().Trim();
                RowAccGuidUptxt.Text = dataGridViewX1.CurrentRow.Cells[0].Value.ToString().Trim();
            }
            catch { }
        }

        private void dataGridViewX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox8.Text = dataGridViewX1.CurrentRow.Cells[2].Value.ToString().Trim();
                textBox1.Text = dataGridViewX1.CurrentRow.Cells[1].Value.ToString().Trim();
                RowAccGuidUptxt.Text = dataGridViewX1.CurrentRow.Cells[0].Value.ToString().Trim();
                simpleButton1.Focus();

            }
        }

        private void CompanyTypeCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            ClearData();
            if (Program.GlbV_Language == "en")
            {
                simpleButton3_Click(sender, e);
            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                simpleButton4_Click(sender, e);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var listAnalysis = DNtMDB.Tbl_Customer.Where(x=>x.CustomerName.Contains(textBox2.Text)).OrderBy(x => x.ID).ToList();
            CustomersDG.DataSource = listAnalysis;
        }

        private void CountryCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(CountryCmb.SelectedValue.ToString());
        }
    }
}
