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
    public partial class MedicinsFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Medicinid = 0;
        int Vint_MedicalUniteID = 0;
        string Image_Path = "";
        public MedicinsFrm()
        {
            InitializeComponent();
            //LangTxtBox.Text = "en";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
        }

        private void MedicinsFrm_Load(object sender, EventArgs e)
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
            //var listMedicins = DNtMDB.Tbl_Medicines.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listMedicins;

            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["RangeFrom"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_MedicinesPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;

            //comboBox1.DataSource = DNtMDB.Tbl_MedicineUnit.ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.Text = "Choose a Medical Unite";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MedicinNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name Medicinis want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم الدواء المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    Vint_MedicalUniteID = int.Parse(comboBox1.SelectedValue.ToString());
                    //Tbl_Medicines MedAnlys = new Tbl_Medicines
                    //{
                    //    Name = MedicinNameTxt.Text,
                    //    MedicineUnitId = Vint_MedicalUniteID

                    //};

                    //DNtMDB.Tbl_Medicines.Add(MedAnlys);
                    DNtMDB.SaveChanges();
                    //long Vint_LastRow = MedAnlys.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Medicinis Add",
                        TableName = "Tbl_Medicines",
                        //TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "MedicinFrm"


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
                    MedicinNameTxt.Text = "";
                    PicturePath.ImageLocation = "";
                    comboBox1.SelectedValue = -1;
                    if (LangTxtBox.Text == "en")
                    {
                        comboBox1.Text = "Choose Medicine Unite ";
                    }
                    else
                    {
                        comboBox1.Text = "اختر وحدة الدواء ";
                    }
                   

                    MedicinNameTxt.Focus();
                    MedicinNameTxt.Select();
                    this.ActiveControl = MedicinNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Medicines.ToList();
                    MedicinNameTxt.Focus();
                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medicinis");
                    //}
                }
                else if (textBox1.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    Vint_Medicinid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_Medicines.SingleOrDefault(x => x.ID == Vint_Medicinid);
                    Vint_MedicalUniteID = int.Parse(comboBox1.SelectedValue.ToString());
                    //result.Name = MedicinNameTxt.Text;
                    //result.MedicineUnitId = Vint_MedicalUniteID;
                    //result.ImagePath = Environment.CurrentDirectory + "\\Images\\Medicins\\" + result.ID + ".jpg";
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Medicinis Update",
                        TableName = "Tbl_Medicines",
                        //TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "MedicinFrm"


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
                    MedicinNameTxt.Text = "";
                    PicturePath.ImageLocation = "";
                    comboBox1.SelectedValue = -1;
                    if (LangTxtBox.Text == "en")
                    {
                        comboBox1.Text = "Choose Medicine Unite ";
                    }
                    else
                    {
                        comboBox1.Text = "اختر وحدة الدواء ";
                    }
                    if (Image_Path != "")
                    {


                        string NewPath = Environment.CurrentDirectory + "\\Images\\Medicins\\" + Vint_Medicinid + ".jpg";

                        File.Copy(Image_Path, NewPath, true);
                        NewPath = Image_Path;
                    }
                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Medicines.ToList();
                    MedicinNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medicinis");
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
                    var result1 = MessageBox.Show("Do you want to delete this analys  ؟", "Delete Medicin ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Medicinid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Medicines.Find(Vint_Medicinid);
                        //DNtMDB.Tbl_Medicines.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Medicin",
                            TableName = "Tbl_Medicines",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "MedicinFrm"


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
                    MedicinNameTxt.Text = "";
                    if (Image_Path != "")
                    {


                        Image_Path = Environment.CurrentDirectory + "\\Images\\Medicins\\" + Vint_Medicinid + ".jpg";

                        File.Delete(Image_Path);
                        PicturePath.ImageLocation = "";

                    }
                    comboBox1.SelectedValue = -1;
                    if (LangTxtBox.Text == "en")
                    {
                        comboBox1.Text = "Choose Medicine Unite ";
                    }
                    else
                    {
                        comboBox1.Text = "اختر وحدة الدواء ";
                    }


                    MedicinNameTxt.Select();
                    this.ActiveControl = MedicinNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Medicines.ToList();
                    MedicinNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا الدواء  ؟", "حذف الدواء ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Medicinid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Medicines.Find(Vint_Medicinid);
                        //DNtMDB.Tbl_Medicines.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Medicin",
                            TableName = "Tbl_Medicines",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "MedicinFrm"


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
                    MedicinNameTxt.Text = "";
                    comboBox1.SelectedValue = -1;
                    if (LangTxtBox.Text == "en")
                    {
                        comboBox1.Text = "Choose Medicine Unite ";
                    }
                    else
                    {
                        comboBox1.Text = "اختر وحدة الدواء ";
                    }


                    MedicinNameTxt.Select();
                    this.ActiveControl = MedicinNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Medicines.ToList();
                    MedicinNameTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Medicin to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر الدواء المراد حذفه");
                }

                textBox1.Select();
                this.ActiveControl = textBox1;
                textBox1.Focus();
            }
            //}
            //else
            //{
            //    MessageBox.Show("You Don't have permition to add Medicinis");
            //}

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Medicinid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_Medicinid.ToString();
            MedicinNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[3].Value != null)
            {
                PicturePath.ImageLocation = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            if (dataGridView1.CurrentRow.Cells[2].Value != null)
            {
                comboBox1.SelectedValue = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            }
        }

        private void MedicinNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listMedicinSerch = DNtMDB.Tbl_Medicines.Where(x => x.Name.Contains(MedicinNameTxt.Text)).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listMedicinSerch;
            
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