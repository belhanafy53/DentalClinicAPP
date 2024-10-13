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

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class CountryFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();


        int Vint_Code = 0;
        public CountryFrm()
        {
            InitializeComponent();
        }
        private void Dg_E()
        {
            CodeTxt.Text = "";
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            dataGridView1.Columns["code"].Visible = true;
            dataGridView1.Columns["dec_en"].Visible = true;
            dataGridView1.Columns["dec_ar"].Visible = true;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["dec_en"].Width = 200;
            dataGridView1.Columns["dec_ar"].Width = 200;
            CodeTxt.Focus();
        }
        private void Dg_Ar()
        {
            CodeTxt.Text = "";
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            dataGridView1.Columns["code"].HeaderText = "الكود";
            dataGridView1.Columns["dec_en"].HeaderText = "الاسم الانجليزي";
            dataGridView1.Columns["dec_ar"].HeaderText = "الاسم العربي";
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["dec_en"].Width = 200;
            dataGridView1.Columns["dec_ar"].Width = 200;
            CodeTxt.Focus();
        }
        private void CountryFrm_Load(object sender, EventArgs e)
        {
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

            CodeTxt.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Country.OrderBy(x => x.id).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_E();
            LangTxtBox.Text = "en";
            CodeTxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Country.OrderBy(x => x.id).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_Ar();
            LangTxtBox.Text = "ar-EG";
            CodeTxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (CodeTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter Code Activity Type want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل كود البلد المراد اضافته !");
                }

            }
            else
            {
                if (RowIDtxt.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                    Tbl_Country Act = new Tbl_Country
                    {
                        code = CodeTxt.Text,
                        dec_en = DescEtxt.Text,
                        dec_ar = DescArtxt.Text,


                    };
                    DNtMDB.Tbl_Country.Add(Act);
                    DNtMDB.SaveChanges();
                    //int Vint_LastRow = Act.;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Add",
                        TableName = "Tbl_Country",
                        TableRecordId = Act.code.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Countries "


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Saved");
                        Dg_E();
                    }
                    else
                    {
                        MessageBox.Show("تم الحفظ");
                        Dg_Ar();
                    }

                    DescEtxt.Text = "";
                    CodeTxt.Text = "";

                    CodeTxt.Focus();
                    CodeTxt.Select();
                    this.ActiveControl = CodeTxt;



                   


                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medical Analysis");
                    //}
                }
                else if (RowIDtxt.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    //Vint_Governorateid = int.Parse(textBox1.Text);
                    var result = DNtMDB.Tbl_Country.FirstOrDefault(x => x.code == CodeTxt.Text);
                    if (result != null)
                    {
                        result.code = CodeTxt.Text;
                        result.dec_en = DescEtxt.Text;
                        result.dec_ar = DescArtxt.Text;
                    }

                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Country Update",
                        TableName = "Tbl_Country",
                        TableRecordId = result.code.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Countries"
                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Updated");
                        Dg_E();
                    }
                    else
                    {
                        MessageBox.Show("تم التعديل");
                        Dg_Ar();
                    }

                    DescEtxt.Text = "";
                    CodeTxt.Text = "";
                    RowIDtxt.Text = "";
                     CodeTxt.Focus(); ;

                    //Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                    var listActivesSerch = (from Act in DNtMDB.Tbl_Country

                                            select new
                                            {
                                                code = Act.code,
                                                dec_en = Act.dec_en,
                                                dec_ar = Act.dec_ar,

                                            }).OrderBy(t => t.code).ToList();

                    dataGridView1.DataSource = listActivesSerch;



                    


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
            int Vint_D1rows = dataGridView1.RowCount;

            if (Vint_D1rows != 0 && DescEtxt.Text != "")
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this Activity Type  ?", "Delete Activity ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {


                        var resultR = DNtMDB.Tbl_Country.Find(Vint_Code);
                        DNtMDB.Tbl_Country.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Country",
                            TableName = "Tbl_Country",
                            TableRecordId = resultR.code.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Countries"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";
                            Dg_E();
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";
                            DescEtxt.Text = "";
                            Dg_Ar();
                        }

                    }

                    var listActiveSerch = (from Act in DNtMDB.Tbl_Country

                                           select new
                                           {
                                               code = Act.code,
                                               dec_en = Act.dec_en,
                                               dec_ar = Act.dec_ar,

                                           }).OrderBy(t => t.code).ToList();

                    dataGridView1.DataSource = listActiveSerch;


                    CodeTxt.Select();
                    this.ActiveControl = CodeTxt;
                    CodeTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذه البلد  ؟", "حذف مدينة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_Country.Find(Vint_Code);
                        DNtMDB.Tbl_Country.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Country",
                            TableName = "Tbl_Country",
                            TableRecordId = resultR.code.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Countries"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";
                            DescEtxt.Text = "";
                        }

                    }






                    var listCountrysSerch = (from Act in DNtMDB.Tbl_Country

                                          select new
                                          {
                                              code = Act.code,
                                              Dec_E = Act.dec_en,
                                              dec_ar = Act.dec_ar,

                                          }).OrderBy(t => t.code).ToList();

                    dataGridView1.DataSource = listCountrysSerch;

                    CodeTxt.Select();
                    this.ActiveControl = CodeTxt;
                    CodeTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Activity Type to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر البلد المراد حذفه");
                }

                DescEtxt.Select();
                this.ActiveControl = DescEtxt;
                DescEtxt.Focus();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DescEtxt.Text = "";
            CodeTxt.Text = "";
            RowIDtxt.Text = "";
            Vint_Code = int.Parse( dataGridView1.CurrentRow.Cells[3].Value.ToString());

            CodeTxt.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            RowIDtxt.Text = CodeTxt.Text;
            DescEtxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            DescArtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void CodeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DescEtxt.Focus();
            }
        }

        private void DescEtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DescArtxt.Focus();
            }
        }

        private void DescArtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }
    }
}
