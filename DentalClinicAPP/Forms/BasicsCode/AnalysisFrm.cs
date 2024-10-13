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
    public partial class AnalysisFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Analysisid = 0;

        public AnalysisFrm()
        {
            InitializeComponent();
            //LangTxtBox.Text = "en";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";
           
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            //var listAnalysis = DNtMDB.Tbl_MedicalAnalysis.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["RangeFrom"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_MedicalAnalysisPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "en";
            AnalysisNameTxt.Focus();

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";
           
            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            //var listAnalysis = DNtMDB.Tbl_MedicalAnalysis.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["RangeFrom"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_MedicalAnalysisPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "ar-EG";
            AnalysisNameTxt.Focus();
        }

        private void AnalysisFrm_Load(object sender, EventArgs e)
        {
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
            //var listAnalysis = DNtMDB.Tbl_MedicalAnalysis.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["RangeFrom"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_MedicalAnalysisPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;

        }

        private void AnalysisNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listAnalysisSerch = (from MdAnls in DNtMDB.Tbl_MedicalAnalysis
            //                         where (MdAnls.Name.Contains(AnalysisNameTxt.Text))
            //                         select new
            //                         {
            //                             ID = MdAnls.ID,
            //                             Name = MdAnls.Name,


            //                         }).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listAnalysisSerch;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (AnalysisNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name medical analysis want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم التحليل المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Tbl_MedicalAnalysis MedAnlys = new Tbl_MedicalAnalysis
                    //{
                    //    Name = AnalysisNameTxt.Text,

                    //};

                    //DNtMDB.Tbl_MedicalAnalysis.Add(MedAnlys);
                    //DNtMDB.SaveChanges();
                    //long Vint_LastRow = MedAnlys.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Medical Analysis Add",
                        TableName = "Tbl_MedicalAnalysis",
                        //TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "AnalysisFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Saved");
                    }
                    else
                    {
                        MessageBox.Show("تم الحفظ");
                    }

                    textBox1.Text = "";
                    AnalysisNameTxt.Text = "";

                    AnalysisNameTxt.Focus();
                    AnalysisNameTxt.Select();
                    this.ActiveControl = AnalysisNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalAnalysis.ToList();
                    //AnalysisNameTxt.Focus();
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
                    Vint_Analysisid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_MedicalAnalysis.SingleOrDefault(x => x.ID == Vint_Analysisid);
                    //result.Name = AnalysisNameTxt.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Medical Analysis Update",
                        TableName = "Tbl_MedicalAnalysis",
                        //TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "AnalysisFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Updated");
                    }
                    else
                    {
                        MessageBox.Show("تم التعديل");
                    }

                    textBox1.Text = "";
                    AnalysisNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalAnalysis.ToList();
                    //AnalysisNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Analysis");
                    //}
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Analysisid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_Analysisid.ToString();
            AnalysisNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 4 && w.ProcdureId == 4);
            //if (validationSaveUser != null)
            //{
            int Vint_D1rows = dataGridView1.RowCount;

            if (Vint_D1rows != 0 && textBox1.Text != "")
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this analys  ؟", "Delete Analys ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Analysisid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_MedicalAnalysis.Find(Vint_Analysisid);
                        //DNtMDB.Tbl_MedicalAnalysis.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Analys",
                            TableName = "Tbl_MedicalAnalysis",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "AnalysisFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                        }

                    }
                    textBox1.Text = "";
                    AnalysisNameTxt.Text = "";



                    AnalysisNameTxt.Select();
                    this.ActiveControl = AnalysisNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalAnalysis.ToList();
                    AnalysisNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا التحليل  ؟", "حذف التحليل ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Analysisid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_MedicalAnalysis.Find(Vint_Analysisid);
                        //DNtMDB.Tbl_MedicalAnalysis.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Analys",
                            TableName = "Tbl_MedicalAnalysis",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "AnalysisFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                        }

                    }
                    textBox1.Text = "";
                    AnalysisNameTxt.Text = "";



                    AnalysisNameTxt.Select();
                    this.ActiveControl = AnalysisNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalAnalysis.ToList();
                    AnalysisNameTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose medical analys to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر التحليل المراد حذفه");
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
    }

}