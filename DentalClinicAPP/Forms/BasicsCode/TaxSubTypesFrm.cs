using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DentalClinicAPP.Classes;
using System.Threading;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class TaxSubTypesFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        string Vst_TaxTypeCode = "";
        string Vst_TaxSubTypeid = "";
        public TaxSubTypesFrm()
        {
            InitializeComponent();
        }

        private void TaxSubTypesFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'taxeDs.Tbl_TaxType' table. You can move, or remove it, as needed.
            //this.tbl_TaxTypeTableAdapter.Fill(this.taxeDs.Tbl_TaxType);

            //var listTaxsubTypeSearch = (from tax in DNtMDB.Tbl_TaxSubType

            //                      select new
            //                      {
            //                          code = tax.Code,
            //                          Desc_En = tax.Desc_En,
            //                          Desc_Ar = tax.Desc_Ar,
            //                          TaxTypeRefrence = tax.TaxTypeRefrence
            //                      }).OrderBy(t => t.Desc_En).ToList();

            //dataGridView1.DataSource = listTaxsubTypeSearch;
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



            textBox1.Text = "";
            textBox2.Text = "";

            comboBox1.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            
            var listTaxsubTypeSearch = (from tax in DNtMDB.Tbl_TaxSubType

                                        select new
                                        {
                                            code = tax.Code,
                                            Desc_En = tax.Desc_En,
                                            Desc_Ar = tax.Desc_Ar,
                                            TaxTypeRefrence = tax.TaxTypeRefrence
                                        }).OrderBy(t => t.code).ToList();

            dataGridView1.DataSource = listTaxsubTypeSearch;

            dataGridView1.Columns["Desc_En"].Width = 200;
            dataGridView1.Columns["Desc_Ar"].Width = 200;
            dataGridView1.Columns["Desc_En"].HeaderText = "English Name";
            dataGridView1.Columns["Desc_Ar"].HeaderText =  "Arabic name";
            dataGridView1.Columns["TaxTypeRefrence"].HeaderText = "Tax Type Refrence";

            comboBox1.DataSource = DNtMDB.Tbl_TaxType.ToList();
            comboBox1.DisplayMember = "Des_En";
            comboBox1.ValueMember = "code";
            comboBox1.Text = "Choose a TaxType";

            LangTxtBox.Text = "en";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            CityNameTxt.Text = "";
            comboBox1.Focus();
            comboBox1.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            
            var listTaxsubTypeSearch = (from tax in DNtMDB.Tbl_TaxSubType

                                        select new
                                        {
                                            code = tax.Code,
                                            Desc_En = tax.Desc_En,
                                            Desc_Ar = tax.Desc_Ar,
                                            TaxTypeRefrence = tax.TaxTypeRefrence
                                        }).OrderBy(t => t.code).ToList();

            dataGridView1.DataSource = listTaxsubTypeSearch;
            //dataGridView1.Columns["Tbl_TaxType"].Visible = false;
            dataGridView1.Columns["Desc_En"].HeaderText = "المسمى الانجليزي";
            dataGridView1.Columns["Desc_Ar"].HeaderText =  "المسمى العربي";
            dataGridView1.Columns["TaxTypeRefrence"].HeaderText = "كود الضريبه";
            dataGridView1.Columns["Desc_En"].Width = 200;
            dataGridView1.Columns["Desc_Ar"].Width = 200;
            LangTxtBox.Text = "ar-EG";
            comboBox1.DataSource = DNtMDB.Tbl_TaxType.ToList();
            comboBox1.DisplayMember = "Des_Ar";
            comboBox1.ValueMember = "code";
            comboBox1.Text = "اختر الضريبه";
            LangTxtBox.Text = "ar-EG";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            CityNameTxt.Text = "";
            comboBox1.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (CityNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name TaxSubType want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم الضريبه المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    Vst_TaxTypeCode = comboBox1.SelectedValue.ToString();
                    Tbl_TaxSubType City = new Tbl_TaxSubType
                    {
                        Code = textBox3.Text,
                        Desc_Ar = CityNameTxt.Text,
                        TaxTypeRefrence = Vst_TaxTypeCode,
                        Desc_En = textBox2.Text,

                    };
                    DNtMDB.Tbl_TaxSubType.Add(City);
                    DNtMDB.SaveChanges();
                    //int Vint_LastRow = City.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "TaxSubType Add",
                        TableName = "Tbl_TaxSubType",
                        TableRecordId = City.Code.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "TaxSubType "


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Saved");
                        simpleButton3_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("تم الحفظ");
                        simpleButton4_Click(sender, e);
                    }

                   

                    

                   
                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medical Analysis");
                    //}
                }
                else if (textBox1.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    Vst_TaxTypeCode = textBox1.Text;
                    var result = DNtMDB.Tbl_TaxSubType.FirstOrDefault(x => x.Code == Vst_TaxTypeCode);
                    result.Code = textBox3.Text;
                    result.Desc_En = textBox2.Text;
                    result.Desc_Ar = CityNameTxt.Text;
                    result.TaxTypeRefrence = textBox4.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "TaxSubType Update",
                        TableName = "Tbl_TaxSubType",
                        TableRecordId = result.Code.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "TaxSubType"
                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Updated");
                        simpleButton3_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("تم التعديل");
                        simpleButton4_Click(sender, e);
                    }

                    textBox1.Text = "";
                    textBox2.Text = "";
                    CityNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                     
                     


                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Analysis");
                    //}
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 4 && w.ProcdureId == 4);
            //if (validationSaveUser != null)
            //{
            

            if (  textBox3.Text != "")
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this Tax Sub Type  ؟", "Delete Tax Sub Type ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {


                        var resultR = DNtMDB.Tbl_TaxSubType.Find(Vst_TaxSubTypeid);
                        DNtMDB.Tbl_TaxSubType.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Tax Sub Type",
                            TableName = "Tbl_TaxSubType",
                            TableRecordId = resultR.Code.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Tax Sub Type"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        
                            MessageBox.Show(" Deleted");
                            simpleButton3_Click(sender, e);
                        

                    }
 
                    
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذه الضريبه  ؟", "حذف ضريبه  ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_TaxSubType.Find(Vst_TaxSubTypeid);
                        DNtMDB.Tbl_TaxSubType.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Tax Sub Type",
                            TableName = "Tbl_TaxSubType",
                            TableRecordId = resultR.Code.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Tax Sub Type"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        
                            MessageBox.Show("تم الحذف");
                            simpleButton4_Click(sender, e);
                       

                    }
                                       
                }
                            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Tax Sub Type to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر الضريبه  المراد حذفه");
                }

                textBox1.Select();
                this.ActiveControl = textBox1;
                textBox1.Focus();
            }
            //}
            //else
            //{
            //    MessageBox.Show("You Don't have permition to add Medical Analysis");
            //}
        }

        private void CityNameTxt_TextChanged(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                if (CityNameTxt.Text != "")
                {
                    if (comboBox1.SelectedValue != null)
                    {
                        Vst_TaxTypeCode = comboBox1.SelectedValue.ToString();
                        var listTaxsubTypeSearch = (from tax in DNtMDB.Tbl_TaxSubType
                                                    where (tax.Desc_En.Contains(CityNameTxt.Text) && tax.TaxTypeRefrence == Vst_TaxSubTypeid)
                                                    select new
                                                    {
                                                        code = tax.Code,
                                                        Desc_En = tax.Desc_En,
                                                        Desc_Ar = tax.Desc_Ar,
                                                        TaxTypeRefrence = tax.TaxTypeRefrence
                                                    }).OrderBy(t => t.Desc_En).ToList();

                        dataGridView1.DataSource = listTaxsubTypeSearch;
                       
                        
                        dataGridView1.Columns["Desc_En"].Width = 300;
                        dataGridView1.Columns["Desc_Ar"].Width = 300;
                    }
                }

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                if (CityNameTxt.Text != "")
                {
                    if (comboBox1.SelectedValue != null)
                    {
                        Vst_TaxTypeCode = comboBox1.SelectedValue.ToString();
                        var listTaxsubTypeSearch = (from tax in DNtMDB.Tbl_TaxSubType
                                                    where (tax.Desc_En.Contains(CityNameTxt.Text) && tax.TaxTypeRefrence == Vst_TaxSubTypeid)
                                                    select new
                                                    {
                                                        code = tax.Code,
                                                        Desc_En = tax.Desc_En,
                                                        Desc_Ar = tax.Desc_Ar,
                                                        TaxTypeRefrence = tax.TaxTypeRefrence
                                                    }).OrderBy(t => t.Desc_En).ToList();

                        dataGridView1.DataSource = listTaxsubTypeSearch;
                        

                        dataGridView1.Columns["Desc_En"].Width = 300;
                        dataGridView1.Columns["Desc_Ar"].Width = 300;
                    }
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Vst_TaxTypeCode = comboBox1.SelectedValue.ToString();
            var listTaxsubTypeSearch = (from tax in DNtMDB.Tbl_TaxSubType
                                        where (tax.TaxTypeRefrence == Vst_TaxTypeCode)
                                        select new
                                        {
                                            code = tax.Code,
                                            Desc_En = tax.Desc_En,
                                            Desc_Ar = tax.Desc_Ar,
                                            TaxTypeRefrence = tax.TaxTypeRefrence
                                        }).OrderBy(t => t.Desc_En).ToList();

            dataGridView1.DataSource = listTaxsubTypeSearch;
            

            dataGridView1.Columns["Desc_En"].Width = 300;
            dataGridView1.Columns["Desc_Ar"].Width = 300;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
        }

        private void CityNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CityNameTxt.Focus();
            }
            
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            CityNameTxt.Text = "";
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Vst_TaxSubTypeid = textBox3.Text;
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            CityNameTxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            var list = DNtMDB.Tbl_TaxType.Single(x => x.code == textBox4.Text);
            comboBox1.Text = "";
            comboBox1.SelectedText = list.Des_En;
            comboBox1.SelectedValue = textBox4.Text;
            comboBox1.DisplayMember = "Des_En";
            comboBox1.ValueMember = "code";
            textBox1.Text = textBox3.Text;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
