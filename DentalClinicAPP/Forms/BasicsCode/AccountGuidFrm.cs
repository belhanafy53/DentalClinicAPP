using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Threading;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class AccountGuidFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 FsDb = new Model1();
        ModelEvents FsEvDb = new ModelEvents();
        ModelSecurity ScDb = new ModelSecurity();
        int xcheckPersonal = 0;
        bool xcheckCosts = false;
        bool xBrokerAccount = false;
        bool xExtrasFinancialYear = false;
        bool xHighamountsAccount = false;
        bool XChequeOut = false;
        bool XElectronicPayments = false;
        bool XReciept = false;
        public AccountGuidFrm()
        {
            InitializeComponent();
        }

        private void AccountGuidFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'accountGuidDs.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.accountGuidDs.DataTable1);
        comboBox1.DataSource = FsDb.Tbl_AccountsKind.ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.SelectedIndex = -1;
            comboBox2.DataSource = FsDb.Tbl_AccountsKind.ToList();
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
            comboBox2.SelectedIndex = -1;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                treeList1.DataSource = FsDb.Tbl_Accounting_Guid.Where(x => x.Name.Contains(textBox1.Text)).OrderBy(x => x.Account_NO).ToList();
                treeList1.ExpandAll();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                CostChckBox.Checked = false;
                checkBoxBrokerAccount.Checked = false;
                checkBoxExtrasFinancialYear.Checked = false;
                checkBoxHighamountsAccount.Checked = false;
                comboBox1.SelectedItem = null;
                comboBox2.SelectedItem = null;


            }
            else if (radioButton2.Checked == true)
            {
                treeList1.DataSource = FsDb.Tbl_Accounting_Guid.Where(x => x.Account_NO.StartsWith(textBox1.Text)).OrderBy(x => x.Account_NO).ToList();
                treeList1.ExpandAll();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                CostChckBox.Checked = false;
                checkBoxBrokerAccount.Checked = false;
                checkBoxExtrasFinancialYear.Checked = false;
                checkBoxHighamountsAccount.Checked = false;
                comboBox1.SelectedItem = null;
                comboBox2.SelectedItem = null;

            }
            else
            {
                MessageBox.Show("من فضلك حدد طريقة البحث");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                treeList1.Focus();
            }
        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                textBox2.Text = "";
                textBox3.Text = "";
                CostChckBox.Checked = false;
                checkBox1.Checked = false;
                CostChckBox2.Checked = false;
                checkBoxBrokerAccount.Checked = false;
                checkBoxExtrasFinancialYear.Checked = false;
                checkBoxHighamountsAccount.Checked = false;
                int c = 0;
                if (treeList1.Nodes.Count > 0)
                {
                    textBox2.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[0]).ToString();
                    textBox3.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[1]).ToString();

                    comboBox1.SelectedValue = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[2]).ToString());

                    //int? c = string.IsNullOrEmpty(cc) ? (int?)null : int.Parse(cc);
                    c = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[3]).ToString());

                    if (c > 0)
                    {
                        checkBox1.Checked = true;
                    }


                    CostChckBox.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[4]).ToString());
                    checkBoxBrokerAccount.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[6]).ToString());
                    checkBoxExtrasFinancialYear.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[7]).ToString());
                    checkBoxHighamountsAccount.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[5]).ToString());

                }
                textBox4.Select();
                this.ActiveControl = textBox4;
                textBox4.Focus();

            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox2.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox2.Focus();
            }
        }

        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            CostChckBox.Checked = false;
            checkBox1.Checked = false;
            CostChckBox2.Checked = false;
            checkBoxBrokerAccount.Checked = false;
            checkBoxExtrasFinancialYear.Checked = false;
            checkBoxHighamountsAccount.Checked = false;
            textBox2.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[0]).ToString();
            textBox3.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[1]).ToString();
            if ( treeList1.GetFocusedRowCellValue(treeList1.Columns[2]).ToString()  != "" )
            {
                comboBox1.SelectedValue = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[2]).ToString());
            }

            //int? c = string.IsNullOrEmpty(cc) ? (int?)null : int.Parse(cc);
            int c = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[3]).ToString());
            if (c > 0)
            {
                checkBox1.Checked = true;
            }

            CostChckBox.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[4]).ToString());
            checkBoxBrokerAccount.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[6]).ToString());
            checkBoxExtrasFinancialYear.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[7]).ToString());
            checkBoxHighamountsAccount.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[5]).ToString());

            textBox4.Select();
            this.ActiveControl = textBox4;
            textBox4.Focus();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                var result = FsDb.Tbl_Accounting_Guid.Where(y => y.Parent_ID == textBox3.Text).Max(y => y.Account_NO);
                int Vint_lP = (textBox3.Text).Length;
                if (result != null)
                {
                    string Vstr_text = result.ToString();
                    string Vstr_MaxNum = Vstr_text.Substring(Vint_lP);
                    string Vstr_Concat = textBox3.Text + (int.Parse(Vstr_MaxNum) + 1);



                    textBox5.Text = Vstr_Concat;
                    treeList1.DataSource = FsDb.Tbl_Accounting_Guid.Where(x => x.Account_NO.StartsWith(textBox1.Text)).OrderBy(x => x.Account_NO).ToList();
                    treeList1.ExpandAll();
                }
                else
                {

                    textBox5.Text = textBox3.Text + "1";
                    treeList1.DataSource = FsDb.Tbl_Accounting_Guid.Where(x => x.Account_NO.StartsWith(textBox1.Text)).OrderBy(x => x.Account_NO).ToList();
                    treeList1.ExpandAll();
                }
            }
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            CostChckBox.Checked = false;
            checkBox1.Checked = false;
            CostChckBox2.Checked = false;
            checkBoxBrokerAccount.Checked = false;
            checkBoxExtrasFinancialYear.Checked = false;
            checkBoxHighamountsAccount.Checked = false;
            checkBox4.Checked = false;
            checkBox3.Checked = false;
            checkBox5.Checked = false;
            textBox2.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[0]).ToString();
            textBox3.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[1]).ToString();

            comboBox1.SelectedValue = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[2]).ToString());

            //int? c = string.IsNullOrEmpty(cc) ? (int?)null : int.Parse(cc);
            int c = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[3]).ToString());
            if (c > 0)
            {
                checkBox1.Checked = true;
            }

            CostChckBox.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[4]).ToString());
            checkBoxBrokerAccount.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[6]).ToString());
            checkBoxExtrasFinancialYear.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[7]).ToString());
            checkBox4.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[8]).ToString());
            checkBox3.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[9]).ToString());
            checkBoxHighamountsAccount.Checked = bool.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[5]).ToString());


            textBox4.Select();
            this.ActiveControl = textBox4;
            textBox4.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" && textBox3.Text == "")
                {
                    MessageBox.Show("من فضلك قم بإختيار حساب من شجرة الحسابات ");
                }
                else if (textBox4.Text == "" && textBox5.Text == "")

                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 10 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    if (checkBox1.Checked == true)
                    {
                        xcheckPersonal = 1;
                    }

                    var result = FsDb.Tbl_Accounting_Guid.SingleOrDefault(x => x.Account_NO == textBox3.Text);
                    result.Name = textBox2.Text;
                    result.Account_NO = textBox3.Text;
                    result.Advac_AccountingNO = textBox3.Text;
                    result.PersonalAccount = xcheckPersonal;
                    result.AccountsKind_ID = int.Parse(comboBox1.SelectedValue.ToString());
                    if (CostChckBox.Checked == true)
                    {
                        result.ExpensesAccount = true;
                    }
                    if (checkBoxBrokerAccount.Checked == true)
                    {
                        result.BrokerAccount = true;
                    }
                    if (checkBoxExtrasFinancialYear.Checked == true)
                    {
                        result.ExtrasFinancialYear = true;
                    }
                    if (checkBoxHighamountsAccount.Checked == true)
                    {
                        result.HighamountsAccount = true;
                    }
                    if (checkBox4.Checked == true)
                    {
                        result.ElectronicPayments = true;
                    }

                    if (checkBox3.Checked == true)
                    {
                        result.ChequeOut = true;
                    }

                    if (checkBox4.Checked == false)
                    {
                        result.ElectronicPayments = false;
                    }

                    if (checkBox3.Checked == false)
                    {
                        result.ChequeOut = false;
                    }
                    if (checkBox5.Checked == false)
                    {
                        result.Reciept = false;
                    }
                    if (checkBox5.Checked == true)
                    {
                        result.Reciept = true;
                    }

                    FsDb.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "تعديل  بند دليل الحسابات",
                        TableName = "Tbl_Accounting_Guid",
                        TableRecordId = result.ID.ToString(),
                        Description = "",
                        //ManagementName = Program.GlbV_Management,
                        //ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                        //EmployeeName = Program.GlbV_EmpName,
                        //User_ID = Program.GlbV_UserId,
                        //UserName = Program.GlbV_UserName,
                        FormName = "Accounting_GuidFrm"


                    };
                    FsEvDb.SecurityEvents.Add(sev);
                    FsEvDb.SaveChanges();
                    //-------------------------
                    MessageBox.Show("تم التعديل");
                    string Vstr_TxtSearch = textBox1.Text;
                    treeList1.DataSource = FsDb.Tbl_Accounting_Guid.ToList();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    CostChckBox.Checked = false;
                    checkBox1.Checked = false;
                    CostChckBox2.Checked = false;
                    checkBoxHighamountsAccount.Checked = false;
                    checkBoxExtrasFinancialYear.Checked = false;
                    checkBoxBrokerAccount.Checked = false;
                    textBox4.Text = "";
                    textBox5.Text = "";
                    CostChckBox.Checked = false;
                    comboBox2.SelectedItem = null;
                    checkBox2.Checked = false;
                    xcheckPersonal = 0;
                    xcheckCosts = false;
                    checkBox4.Checked = false;
                    checkBox3.Checked = false;
                    XChequeOut = false;
                    XElectronicPayments = false;
                    checkBox5.Checked = false;
                    textBox1.Focus();
                    //}

                    //else
                    //{
                    //    MessageBox.Show("ليس لديك صلاحية تعديل بنود الدليل .. برجاء مراجعة مدير المنظومه");
                    //}

                }
                else if (textBox4.Text != "" && textBox5.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 10 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    if (checkBox2.Checked == true)
                    {
                        xcheckPersonal = 1;
                    }
                    else
                    {
                        xcheckPersonal = 0;
                    }
                    if (CostChckBox.Checked == true)
                    {
                        xcheckCosts = true;
                    }
                    else
                    {
                        xcheckCosts = false;
                    }


                    if (checkBoxBrokerAccount1.Checked == true)
                    {
                        xBrokerAccount = true;
                    }
                    else
                    {
                        xBrokerAccount = false;
                    }

                    if (checkBoxExtrasFinancialYear1.Checked == true)
                    {
                        xExtrasFinancialYear = true;
                    }
                    else
                    {
                        xExtrasFinancialYear = false;
                    }
                    if (checkBoxHighamountsAccount1.Checked == true)
                    {
                        xHighamountsAccount = true;
                    }
                    else
                    {
                        xHighamountsAccount = false;
                    }
                    if (checkBox4.Checked == true)
                    {
                        XElectronicPayments = true;
                    }
                    else
                    {
                        XElectronicPayments = false;
                    }
                    if (checkBox3.Checked == true)
                    {
                        XChequeOut = true;
                    }
                    else
                    {
                        XChequeOut = false;
                    }
                    if (checkBox5.Checked == true)
                    {
                        XReciept = true;
                    }
                    else
                    {
                        XReciept = false;
                    }
                    
                    Tbl_Accounting_Guid accgTbl = new Tbl_Accounting_Guid
                    {
                        Name = textBox4.Text,
                        Account_NO = textBox5.Text,
                        Parent_ID = textBox3.Text,
                        AccountsKind_ID = int.Parse(comboBox2.SelectedValue.ToString()),
                        PersonalAccount = xcheckPersonal,
                        ExpensesAccount = xcheckCosts,
                        BrokerAccount = xBrokerAccount,
                        ExtrasFinancialYear = xExtrasFinancialYear,
                        HighamountsAccount = xHighamountsAccount,
                        Advac_AccountingNO = textBox5.Text,
                        ChequeOut = XChequeOut,
                        ElectronicPayments = XElectronicPayments,
                        Reciept= XReciept
                    };
                    FsDb.Tbl_Accounting_Guid.Add(accgTbl);
                    FsDb.SaveChanges();
                    int Vint_LastRow = accgTbl.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "اضافة بند لدليل الحسابات",
                        TableName = "Tbl_Accounting_Guid",
                        TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        //ManagementName = Program.GlbV_Management,
                        //ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                        //EmployeeName = Program.GlbV_EmpName,
                        //User_ID = Program.GlbV_UserId,
                        //UserName = Program.GlbV_UserName,
                        FormName = "Accounting_GuidFrm"


                    };
                    FsEvDb.SecurityEvents.Add(sev);
                    FsEvDb.SaveChanges();
                    //------------------------------------

                    MessageBox.Show("تم الحفظ");
                    treeList1.DataSource = FsDb.Tbl_Accounting_Guid.ToList();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    CostChckBox.Checked = false;
                    checkBox1.Checked = false;
                    CostChckBox2.Checked = false;
                    //checkBoxHighamountsAccount.Checked = false;
                    checkBoxExtrasFinancialYear1.Checked = false;
                    checkBoxHighamountsAccount1.Checked = false;
                    checkBoxBrokerAccount1.Checked = false;
                    //checkBoxExtrasFinancialYear.Checked = false;
                    textBox4.Text = "";
                    textBox5.Text = "";
                    CostChckBox.Checked = false;
                    comboBox2.SelectedItem = null;
                    checkBox2.Checked = false;
                    xcheckPersonal = 0;
                    xcheckCosts = false;
                    textBox1.Focus();
                    checkBox5.Checked = false;
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل اسم الحساب");
                }
                //}
                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية اضافة بند لدليل الحسابات .. برجاء مراجعة مدير المنظومه");
                //}
            }
            catch
            {
                MessageBox.Show("من فضلك ادخل البيانات بالكامل");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 10 && w.ProcdureId == 4);
                //if (validationSaveUser != null)
                //{
                if (textBox3.Text != "")
                {

                    int result = FsDb.Tbl_Accounting_Guid.Count(x => x.Parent_ID == textBox3.Text);
                    if (result > 0)
                    {
                        MessageBox.Show("  لا يمكن حدف هدا الحساب لإشتماله على حسابات فرعية");
                    }
                    else
                    {

                        var result1 = MessageBox.Show("هل تريد حدف هدا الحساب  ؟", "حدف حساب ", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {
                            var resultr = FsDb.Tbl_Accounting_Guid.FirstOrDefault(x => x.Account_NO == textBox3.Text);
                            FsDb.Tbl_Accounting_Guid.Remove(resultr);
                            FsDb.SaveChanges();

                            //---------------خفظ ااحداث 
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "حدف بند من دليل الحسابات",
                                TableName = "Tbl_Accounting_Guid",
                                TableRecordId = resultr.ID.ToString(),
                                Description = "",
                                //ManagementName = Program.GlbV_Management,
                                //ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                //EmployeeName = Program.GlbV_EmpName,
                                //User_ID = Program.GlbV_UserId,
                                //UserName = Program.GlbV_UserName,
                                FormName = "Accounting_GuidFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //-------------------------

                            MessageBox.Show("  تم الحدف");
                            treeList1.DataSource = FsDb.Tbl_Accounting_Guid.ToList();
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            CostChckBox.Checked = false;
                            CostChckBox2.Checked = false;
                            checkBoxHighamountsAccount.Checked = false;
                            checkBoxExtrasFinancialYear.Checked = false;
                            checkBoxBrokerAccount.Checked = false;
                            textBox2.Text = "";
                            textBox3.Text = "";
                            CostChckBox.Checked = false;
                            checkBox1.Checked = false;
                            CostChckBox2.Checked = false;
                            checkBox5.Checked = false;
                            treeList1.DataSource = FsDb.Tbl_Accounting_Guid.Where(x => x.Account_NO.StartsWith(textBox1.Text)).OrderBy(x => x.Account_NO).ToList();
                            treeList1.ExpandAll();
                            textBox1.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("من فضلك قم بإختيار الحساب المراد حدفه");
                    textBox1.Focus();
                }
                //}
                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية حدف بند من الدليل .. برجاء مراجعة مدير المنظومه");
                //}
            }
            catch


            {
                MessageBox.Show("هذا الحساب لايمكن حذفه لوجود مستندات له", "المنظومه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";
            this.dataTable1TableAdapter.Fill(this.accountGuidDs.DataTable1);
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            //var listAnalysis = DNtMDB.Tbl_Cities.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;
            //dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["GovernorateID"].Visible = false;
            //dataGridView1.Columns["Tbl_Governorates"].Visible = false;
            //dataGridView1.Columns["Name"].Width = 300;
            //LangTxtBox.Text = "en";
            //comboBox1.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";
            this.dataTable1TableAdapter.Fill(this.accountGuidDs.DataTable1);
            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            //var listAnalysis = DNtMDB.Tbl_Cities.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;
            //dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["GovernorateID"].Visible = false;
            //dataGridView1.Columns["Tbl_Governorates"].Visible = false;
            //dataGridView1.Columns["Name"].Width = 300;
            //LangTxtBox.Text = "ar-EG";
            //comboBox1.Focus();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
        }
    }
}