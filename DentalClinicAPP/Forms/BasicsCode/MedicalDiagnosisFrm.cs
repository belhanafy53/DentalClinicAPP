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
    public partial class MedicalDiagnosisFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        int Vint_Diagnosisid = 0;
        public MedicalDiagnosisFrm()
        {
            InitializeComponent();
            //LangTxtBox.Text = "en";
        }

        private void MedicalDiagnosisFrm_Load(object sender, EventArgs e)
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
            //var listDosage = DNtMDB.Tbl_MedicalDiagnosis.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listDosage;

            dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.Columns["Name"].Width = 300;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            //var listDosage = DNtMDB.Tbl_MedicalDiagnosis.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listDosage;
            dataGridView1.Columns["ID"].Visible = false;


            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "en";
            MedicalDiagnosisNameTxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            //var listDosage = DNtMDB.Tbl_MedicalDiagnosis.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listDosage;
            dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "ar-EG";
            MedicalDiagnosisNameTxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MedicalDiagnosisNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name medical Dosage want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم التشخيص  المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Tbl_MedicalDiagnosis MedDiags = new Tbl_MedicalDiagnosis
                    //{
                    //    Name = MedicalDiagnosisNameTxt.Text,

                    //};

                    //DNtMDB.Tbl_MedicalDiagnosis.Add(MedDiags);
                    //DNtMDB.SaveChanges();
                    //long Vint_LastRow = MedDiags.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "MedicalDiagnosis Add",
                        TableName = "Tbl_MedicalDiagnosis",
                        //TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "MedicalDiagnosisFrm"


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
                    MedicalDiagnosisNameTxt.Text = "";

                    MedicalDiagnosisNameTxt.Focus();
                    MedicalDiagnosisNameTxt.Select();
                    this.ActiveControl = MedicalDiagnosisNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalDiagnosis.ToList();
                    MedicalDiagnosisNameTxt.Focus();
                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medical Dosage");
                    //}
                }
                else if (textBox1.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    Vint_Diagnosisid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_MedicalDiagnosis.SingleOrDefault(x => x.ID == Vint_Diagnosisid);
                    //result.Name = MedicalDiagnosisNameTxt.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "MedicalDiagnosis Update",
                        TableName = "Tbl_MedicalDiagnosis",
                        //TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "MedicalDiagnosisFrm"


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
                    MedicalDiagnosisNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalDiagnosis.ToList();
                    MedicalDiagnosisNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Dosage");
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

            if (Vint_D1rows != 0 && textBox1.Text != "")
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this MedicalDiagnosis  ؟", "Delete MedicalDiagnosis ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Diagnosisid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_MedicalDiagnosis.Find(Vint_Diagnosisid);
                        //DNtMDB.Tbl_MedicalDiagnosis.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete MedicalDiagnosis",
                            TableName = "Dosage",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "DosageFrm"


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
                    MedicalDiagnosisNameTxt.Text = "";



                    MedicalDiagnosisNameTxt.Select();
                    this.ActiveControl = MedicalDiagnosisNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalDiagnosis.ToList();
                    MedicalDiagnosisNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا التشخيص   ؟", "حذف التشخيص  ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Diagnosisid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_MedicalDiagnosis.Find(Vint_Diagnosisid);
                        //DNtMDB.Tbl_MedicalDiagnosis.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete MedicalDiagnosis",
                            TableName = "Dosage",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "DosageFrm"


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
                    MedicalDiagnosisNameTxt.Text = "";



                    MedicalDiagnosisNameTxt.Select();
                    this.ActiveControl = MedicalDiagnosisNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicalDiagnosis.ToList();
                    MedicalDiagnosisNameTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose medical MedicalDiagnosis to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر التشخيص  المراد حذفه");
                }

                textBox1.Select();
                this.ActiveControl = textBox1;
                textBox1.Focus();
            }
            //}
            //else
            //{
            //    MessageBox.Show("You Don't have permition to add Medical Dosage");
            //}
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Diagnosisid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_Diagnosisid.ToString();
            MedicalDiagnosisNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void DosagesNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listDosageSerch = (from MdAnls in DNtMDB.Tbl_MedicalDiagnosis
            //                       where (MdAnls.Name.Contains(MedicalDiagnosisNameTxt.Text))
            //                       select new
            //                       {
            //                           ID = MdAnls.ID,
            //                           Name = MdAnls.Name,


            //                       }).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listDosageSerch;
            
        }
    }
}