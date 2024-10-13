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
    public partial class Dosages : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Dosageid = 0;
        public Dosages()
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
            //var listDosage = DNtMDB.Dosages.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listDosage;
            dataGridView1.Columns["ID"].Visible = false;
           
            
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "en";
            DosagesNameTxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            //var listDosage = DNtMDB.Dosages.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listDosage;
            dataGridView1.Columns["ID"].Visible = false;
            
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "ar-EG";
            DosagesNameTxt.Focus();
        }

        private void Dosages_Load(object sender, EventArgs e)
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
            //var listDosage = DNtMDB.Dosages.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listDosage;

            dataGridView1.Columns["ID"].Visible = false;
             
            dataGridView1.Columns["Name"].Width = 300;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DosagesNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name medical Dosage want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم الجرعه المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Dosage MedAnlys = new Dosage
                    //{
                    //    Name = DosagesNameTxt.Text,

                    //};

                    //DNtMDB.Dosages.Add(MedAnlys);
                    DNtMDB.SaveChanges();
                    //long Vint_LastRow = MedAnlys.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Medical Dosage Add",
                        TableName = "Dosages",
                        //TableRecordId = Vint_LastRow.ToString(),
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
                        MessageBox.Show("Saved");
                    }
                    else
                    {
                        MessageBox.Show("تم الحفظ");
                    }

                    textBox1.Text = "";
                    DosagesNameTxt.Text = "";

                    DosagesNameTxt.Focus();
                    DosagesNameTxt.Select();
                    this.ActiveControl = DosagesNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Dosages.ToList();
                    DosagesNameTxt.Focus();
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
                    Vint_Dosageid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Dosages.SingleOrDefault(x => x.ID == Vint_Dosageid);
                    //result.Name = DosagesNameTxt.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Medical Dosage Update",
                        TableName = "Dosages",
                        //TableRecordId = result.ID.ToString(),
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
                        MessageBox.Show("Updated");
                    }
                    else
                    {
                        MessageBox.Show("تم التعديل");
                    }

                    textBox1.Text = "";
                    DosagesNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Dosages.ToList();
                    DosagesNameTxt.Focus();

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
                    var result1 = MessageBox.Show("Do you want to delete this dosage  ؟", "Delete dosage ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Dosageid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Dosages.Find(Vint_Dosageid);
                        //DNtMDB.Dosages.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete dosage",
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
                    DosagesNameTxt.Text = "";



                    DosagesNameTxt.Select();
                    this.ActiveControl = DosagesNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Dosages.ToList();
                    DosagesNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا الجرعه  ؟", "حذف الجرعه ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Dosageid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Dosages.Find(Vint_Dosageid);
                        //DNtMDB.Dosages.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete dosage",
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
                    DosagesNameTxt.Text = "";



                    DosagesNameTxt.Select();
                    this.ActiveControl = DosagesNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Dosages.ToList();
                    DosagesNameTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose medical dosage to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر الجرعه المراد حذفه");
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

        private void DosageNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listDosageSerch = (from MdAnls in DNtMDB.Dosages
            //                       where (MdAnls.Name.Contains(DosagesNameTxt.Text))
            //                     select new
            //                     {
            //                         ID = MdAnls.ID,
            //                         Name = MdAnls.Name,


            //                     }).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listDosageSerch;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Dosageid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_Dosageid.ToString();
            DosagesNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}