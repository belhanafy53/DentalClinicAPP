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
    public partial class MedicinUnitesFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_MedicalUniteid = 0;
        public MedicinUnitesFrm()
        {
            InitializeComponent();
            //LangTxtBox.Text = "en";
        }

        private void MedicinUnitesFrm_Load(object sender, EventArgs e)
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
            //var listAnalysis = DNtMDB.Tbl_MedicineUnit.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;

            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["RangeFrom"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_MedicineUnitPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
        }

        private void GovernorateNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listMdclUniteSerch =  DNtMDB.Tbl_MedicineUnit.Where (x=>x.Name.Contains(MdclUniteNameTxt.Text)).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listMdclUniteSerch;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MdclUniteNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name Medical Unite want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم وحدة الدواء المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Tbl_MedicineUnit Mdun = new Tbl_MedicineUnit
                    //{
                    //    Name = MdclUniteNameTxt.Text ,


                    //};

                    //DNtMDB.Tbl_MedicineUnit.Add(Mdun);
                    DNtMDB.SaveChanges();
                    //int Vint_LastRow = Mdun.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Add",
                        TableName = "Tbl_MedicineUnit",
                        //TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Governorates"


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
                    MdclUniteNameTxt.Text = "";
                    
                    MdclUniteNameTxt.Focus();
                    MdclUniteNameTxt.Select();
                    this.ActiveControl = MdclUniteNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicineUnit.ToList();
                    MdclUniteNameTxt.Focus();
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
                    Vint_MedicalUniteid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_MedicineUnit.SingleOrDefault(x => x.ID == Vint_MedicalUniteid);
                    //result.Name = MdclUniteNameTxt.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Update",
                        TableName = "Tbl_MedicineUnit",
                        //TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Governorates"


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
                    MdclUniteNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicineUnit.ToList();
                    MdclUniteNameTxt.Focus();

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

            if (Vint_D1rows != 0 && textBox1.Text != "")
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this analys  ؟", "Delete Analys ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_MedicalUniteid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_MedicineUnit.Find(Vint_MedicalUniteid);
                        //DNtMDB.Tbl_MedicineUnit.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Governorate",
                            TableName = "Tbl_MedicineUnit",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Governorates"


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
                    MdclUniteNameTxt.Text = "";



                    MdclUniteNameTxt.Select();
                    this.ActiveControl = MdclUniteNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicineUnit.ToList();
                    MdclUniteNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا المحافظة  ؟", "حذف المحافظة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_MedicalUniteid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_MedicineUnit.Find(Vint_MedicalUniteid);
                        //DNtMDB.Tbl_MedicineUnit.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Analys",
                            TableName = "Tbl_MedicineUnit",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Governorates"


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
                    MdclUniteNameTxt.Text = "";



                    MdclUniteNameTxt.Select();
                    this.ActiveControl = MdclUniteNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_MedicineUnit.ToList();
                    MdclUniteNameTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose governorate to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر المحافظة المراد حذفه");
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
            Vint_MedicalUniteid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_MedicalUniteid.ToString();
            MdclUniteNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
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
    }
}