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
using System.IO;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class ClinicksFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Clinicid = 0;
        string Image_Path = "";
        public ClinicksFrm()
        {
            InitializeComponent();
            //LangTxtBox.Text = "en";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
        }

        private void ClinicksFrm_Load(object sender, EventArgs e)
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
            var listAnalysis = DNtMDB.Tbl_Clinics.OrderBy(x => x.Name).ToList();
            dataGridView1.DataSource = listAnalysis;

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["ImagePath"].Visible = false;
            dataGridView1.Columns["Tbl_MedicalVisite"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            ClinicNameTxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ClinicNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter  Clinic name want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم العيادة المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    
                    Tbl_Clinics MedAnlys = new Tbl_Clinics
                    {
                        Name = ClinicNameTxt.Text,
                        ImagePath = Image_Path

                    };

                    DNtMDB.Tbl_Clinics.Add(MedAnlys);
                    DNtMDB.SaveChanges();
                    Vint_Clinicid = MedAnlys.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Clinic Add",
                        TableName = "Tbl_Clinics",
                        TableRecordId = Vint_Clinicid.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "ClinicksFrm"


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
                    PicturePath.ImageLocation = "";
                    textBox1.Text = "";
                    ClinicNameTxt.Text = "";

                    ClinicNameTxt.Focus();
                    ClinicNameTxt.Select();
                    this.ActiveControl = ClinicNameTxt;

                    dataGridView1.DataSource = DNtMDB.Tbl_Clinics.ToList();
                    ClinicNameTxt.Focus();
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
                    Vint_Clinicid = int.Parse(textBox1.Text);
                    var result = DNtMDB.Tbl_Clinics.SingleOrDefault(x => x.ID == Vint_Clinicid);
                    result.Name = ClinicNameTxt.Text;
                    result.ImagePath = Environment.CurrentDirectory + "\\Images\\Clinics\\" + result.ID + ".jpg";
                    //Image_Path = Environment.CurrentDirectory + "\\Images\\Clinics\\" + result.ID + ".jpg";

                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Clinic Update",
                        TableName = "Tbl_Clinics",
                        TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "ClinicksFrm"


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
                    ClinicNameTxt.Text = "";
                    PicturePath.ImageLocation = "";
                    this.ActiveControl = textBox1;

                    dataGridView1.DataSource = DNtMDB.Tbl_Clinics.ToList();
                    ClinicNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Analysis");
                    //}
                }
                if (Image_Path != "")
                {

                   
                    string NewPath = Environment.CurrentDirectory + "\\Images\\Clinics\\" + Vint_Clinicid + ".jpg";

                    File.Copy(Image_Path, NewPath, true);
                    NewPath = Image_Path;
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
                    var result1 = MessageBox.Show("Do you want to delete this analys  ؟", "Delete Analys ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Clinicid = int.Parse(textBox1.Text);
                        var resultR = DNtMDB.Tbl_Clinics.Find(Vint_Clinicid);
                        DNtMDB.Tbl_Clinics.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Clinic",
                            TableName = "Tbl_Clinics",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ClinicksFrm"


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
                    if (Image_Path != "")
                    {


                        Image_Path = Environment.CurrentDirectory + "\\Images\\Clinics\\" + Vint_Clinicid + ".jpg";

                        File.Delete(Image_Path);
                        PicturePath.ImageLocation = "";

                    }
                    textBox1.Text = "";
                    ClinicNameTxt.Text = "";



                    ClinicNameTxt.Select();
                    this.ActiveControl = ClinicNameTxt;

                    dataGridView1.DataSource = DNtMDB.Tbl_Clinics.ToList();
                    ClinicNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا العيادة  ؟", "حذف العيادة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Clinicid = int.Parse(textBox1.Text);
                        var resultR = DNtMDB.Tbl_Clinics.Find(Vint_Clinicid);
                        DNtMDB.Tbl_Clinics.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Clinic",
                            TableName = "Tbl_Clinics",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ClinicksFrm"


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
                        if (Image_Path != "")
                        {


                            Image_Path = Environment.CurrentDirectory + "\\Images\\Clinics\\" + Vint_Clinicid + ".jpg";

                            File.Delete(Image_Path);
                            PicturePath.ImageLocation = "";

                        }
                    }
                    textBox1.Text = "";
                    ClinicNameTxt.Text = "";



                    ClinicNameTxt.Select();
                    this.ActiveControl = ClinicNameTxt;

                    dataGridView1.DataSource = DNtMDB.Tbl_Clinics.ToList();
                    ClinicNameTxt.Focus();
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
                    MessageBox.Show("من فضلك اختر العيادة المراد حذفه");
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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Clinicid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            ClinicNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[2].Value != null)
            {
                PicturePath.ImageLocation = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            textBox1.Text = Vint_Clinicid.ToString();
            
        }

        private void PicturePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PicturePath.ImageLocation = dialog.FileName;
                Image_Path = dialog.FileName;
            }
        }
    }
}
   
