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
    public partial class Governorates : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Governorateid = 0;
        public Governorates()
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
            //var listAnalysis = DNtMDB.Tbl_Governorates.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;
            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["RangeFrom"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_GovernoratesPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "en";
            GovernorateNameTxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            //var listAnalysis = DNtMDB.Tbl_Governorates.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;
            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["RangeFrom"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_GovernoratesPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "ar-EG";
            GovernorateNameTxt.Focus();
        }

        private void Governorates_Load(object sender, EventArgs e)
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
            //var listAnalysis = DNtMDB.Tbl_Governorates.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listAnalysis;

            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["RangeFrom"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["Tbl_GovernoratesPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
        }

        private void AnalysisNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listGovernoratesSerch = (from MdAnls in DNtMDB.Tbl_Governorates
            //                         where (MdAnls.Name.Contains(GovernorateNameTxt.Text))
            //                         select new
            //                         {
            //                             ID = MdAnls.ID,
            //                             Name = MdAnls.Name,


            //                         }).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listGovernoratesSerch;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (GovernorateNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name Governorate want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم المحافظه المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Tbl_Governorates Gover = new Tbl_Governorates
                    //{
                    //    Name = GovernorateNameTxt.Text

                    //};

                    //DNtMDB.Tbl_Governorates.Add(Gover);
                    DNtMDB.SaveChanges();
                    //long Vint_LastRow = Gover.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Add",
                        TableName = "Tbl_Governorates",
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
                    GovernorateNameTxt.Text = "";

                    GovernorateNameTxt.Focus();
                    GovernorateNameTxt.Select();
                    this.ActiveControl = GovernorateNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Governorates.ToList();
                    GovernorateNameTxt.Focus();
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
                    Vint_Governorateid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_Governorates.SingleOrDefault(x => x.ID == Vint_Governorateid);
                    //result.Name = GovernorateNameTxt.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Update",
                        TableName = "Tbl_Governorates",
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
                    GovernorateNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Governorates.ToList();
                    GovernorateNameTxt.Focus();

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
            Vint_Governorateid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_Governorateid.ToString();
            GovernorateNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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

                        Vint_Governorateid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Governorates.Find(Vint_Governorateid);
                        //DNtMDB.Tbl_Governorates.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Governorate",
                            TableName = "Tbl_Governorates",
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
                    GovernorateNameTxt.Text = "";



                    GovernorateNameTxt.Select();
                    this.ActiveControl = GovernorateNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Governorates.ToList();
                    GovernorateNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا المحافظة  ؟", "حذف المحافظة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Governorateid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Governorates.Find(Vint_Governorateid);
                        //DNtMDB.Tbl_Governorates.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Analys",
                            TableName = "Tbl_Governorates",
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
                    GovernorateNameTxt.Text = "";



                    GovernorateNameTxt.Select();
                    this.ActiveControl = GovernorateNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Governorates.ToList();
                    GovernorateNameTxt.Focus();
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
    
    }
}