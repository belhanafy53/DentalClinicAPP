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
    public partial class DoctorsFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Doctorid = 0;
        int Vint_sexID = 0;
        string Image_Path = "";
        public DoctorsFrm()
        {
            InitializeComponent();
          //  LangTxtBox.Text = "en";
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
           
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
        }

        private void DoctorsFrm_Load(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                LangTxtBox.Text = "en";
                button2_Click(sender, e);

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                LangTxtBox.Text = "ar-EG";
                button1_Click(sender, e);
            }
            //var listAnalysis = DNtMDB.Tbl_Doctors.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;

            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["ImagePath"].Visible = false;
            dataGridView1.Columns["Tbl_MedicalVisite"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            DoctorNameTxt.Focus();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Doctorid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            DoctorNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[2].Value !=null)
            {
                comboBox1.SelectedIndex = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[3].Value != null)
            {
                PicturePath.ImageLocation = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            textBox1.Text = Vint_Doctorid.ToString();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PicturePath.ImageLocation = dialog.FileName;
                Image_Path = dialog.FileName;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DoctorNameTxt.Text == "")
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
                      Vint_sexID = int.Parse(comboBox1.SelectedIndex.ToString());
                    //Tbl_Doctors MedAnlys = new Tbl_Doctors
                    //{
                    //    Name = DoctorNameTxt.Text,
                    //    ImagePath = Image_Path ,
                    //    Sex = Vint_sexID

                    //};

                    //DNtMDB.Tbl_Doctors.Add(MedAnlys);
                    DNtMDB.SaveChanges();
                    //Vint_Doctorid = MedAnlys.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Clinic Add",
                        TableName = "Tbl_Doctors",
                        TableRecordId = Vint_Doctorid.ToString(),
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
                    DoctorNameTxt.Text = "";
                    comboBox1.SelectedIndex = -1;
                    comboBox1.Text = "Choose Sex";
                    DoctorNameTxt.Focus();
                    DoctorNameTxt.Select();
                    this.ActiveControl = DoctorNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Doctors.ToList();
                    DoctorNameTxt.Focus();
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
                    Vint_Doctorid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_Doctors.SingleOrDefault(x => x.ID == Vint_Doctorid);
                    //result.Name = DoctorNameTxt.Text;
                    //result.Sex = int.Parse(comboBox1.SelectedIndex.ToString());
                    //result.ImagePath = Environment.CurrentDirectory + "\\Images\\Doctors\\" + result.ID + ".jpg";
                    //Image_Path = Environment.CurrentDirectory + "\\Images\\Doctors\\" + result.ID + ".jpg";

                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Clinic Update",
                        TableName = "Tbl_Doctors",
                        //TableRecordId = result.ID.ToString(),
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
                    DoctorNameTxt.Text = "";
                    PicturePath.ImageLocation = "";
                    comboBox1.SelectedIndex = -1;
                    comboBox1.Text = "Choose Sex";
                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Doctors.ToList();
                    DoctorNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Analysis");
                    //}
                }
                if (Image_Path != "")
                {


                    string NewPath = Environment.CurrentDirectory + "\\Images\\Doctors\\" + Vint_Doctorid + ".jpg";

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

                        Vint_Doctorid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Doctors.Find(Vint_Doctorid);
                        //DNtMDB.Tbl_Doctors.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Clinic",
                            TableName = "Tbl_Doctors",
                            //TableRecordId = resultR.ID.ToString(),
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


                        Image_Path = Environment.CurrentDirectory + "\\Images\\Doctors\\" + Vint_Doctorid + ".jpg";

                        File.Delete(Image_Path);
                        PicturePath.ImageLocation = "";

                    }
                    textBox1.Text = "";
                    DoctorNameTxt.Text = "";
                    comboBox1.SelectedIndex = -1;
                    comboBox1.Text = "Choose Sex";


                    DoctorNameTxt.Select();
                    this.ActiveControl = DoctorNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Doctors.ToList();
                    DoctorNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا العيادة  ؟", "حذف العيادة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Doctorid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Doctors.Find(Vint_Doctorid);
                        //DNtMDB.Tbl_Doctors.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Clinic",
                            TableName = "Tbl_Doctors",
                            //TableRecordId = resultR.ID.ToString(),
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


                            Image_Path = Environment.CurrentDirectory + "\\Images\\Doctors\\" + Vint_Doctorid + ".jpg";

                            File.Delete(Image_Path);
                            PicturePath.ImageLocation = "";

                        }
                    }
                    textBox1.Text = "";
                    DoctorNameTxt.Text = "";



                    DoctorNameTxt.Select();
                    this.ActiveControl = DoctorNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Doctors.ToList();
                    DoctorNameTxt.Focus();
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

        private void DoctorNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }

        private void DoctorNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listPatientSerch = DNtMDB.Tbl_Doctors.Where(x => x.Name.Contains(DoctorNameTxt.Text)).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listPatientSerch;
        }
    }
}