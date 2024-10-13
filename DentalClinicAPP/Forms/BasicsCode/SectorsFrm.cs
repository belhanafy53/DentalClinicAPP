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
    public partial class SectorsFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();


        int Vint_Code = 0;
        public SectorsFrm()
        {
            InitializeComponent();
        }
        private void Dg_E()
        {
            dataGridView1.DataSource = DNtMDB.Tbl_Sectors.OrderBy(x => x.ID).ToList();
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            //dataGridView1.Columns["code"].Visible = true;
            dataGridView1.Columns["Name_E"].Visible = true;
            dataGridView1.Columns["Name_Ar"].Visible = true;
            dataGridView1.Columns["id"].Visible = false;
            //dataGridView1.Columns["Tbl_Cities"].Visible = false;
            dataGridView1.Columns["Name_E"].Width = 200;
            dataGridView1.Columns["Name_Ar"].Width = 200;
            DescEtxt.Focus();
        }
        private void Dg_Ar()
        {
            dataGridView1.DataSource = DNtMDB.Tbl_Sectors.OrderBy(x => x.ID).ToList();
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            //dataGridView1.Columns["code"].HeaderText = "الكود";
            dataGridView1.Columns["Name_E"].HeaderText = "الاسم الانجليزي";
            dataGridView1.Columns["Name_Ar"].HeaderText = "الاسم العربي";
            dataGridView1.Columns["id"].Visible = false;
            //dataGridView1.Columns["Tbl_Cities"].Visible = false;
            dataGridView1.Columns["Name_E"].Width = 200;
            dataGridView1.Columns["Name_Ar"].Width = 200;
            DescEtxt.Focus();
        }
        private void SectorsFrm_Load(object sender, EventArgs e)
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

            DescEtxt.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Sectors.OrderBy(x => x.ID).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_E();
            LangTxtBox.Text = "en";
            DescEtxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Sectors.OrderBy(x => x.ID).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_Ar();
            LangTxtBox.Text = "ar-EG";
            DescEtxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DescEtxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter Sector want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل القطاع  المراد اضافته !");
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
                    Tbl_Sectors Act = new Tbl_Sectors
                    {

                        Name_E = DescEtxt.Text,
                        Name_Ar = DescArtxt.Text,


                    };
                    DNtMDB.Tbl_Sectors.Add(Act);
                    DNtMDB.SaveChanges();
                    int Vint_LastRow = Act.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Add",
                        TableName = "Tbl_Sectors",
                        TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Sectors "


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

                    DescEtxt.Text = "";
                    DescArtxt.Text = "";

                    DescEtxt.Focus();
                    DescEtxt.Select();
                    this.ActiveControl = DescEtxt;






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
                    var result = DNtMDB.Tbl_Sectors.FirstOrDefault(x => x.ID == Vint_Code);
                    if (result != null)
                    {

                        result.Name_E = DescEtxt.Text;
                        result.Name_Ar = DescArtxt.Text;
                    }

                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Sector Update",
                        TableName = "Tbl_Sectors",
                        TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Sectors"
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

                    DescEtxt.Text = "";

                    RowIDtxt.Text = "";
                    DescEtxt.Focus(); ;








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
                    var result1 = MessageBox.Show("Do you want to delete this Sector  ?", "Delete Sector ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {


                        var resultR = DNtMDB.Tbl_Sectors.Find(Vint_Code);
                        DNtMDB.Tbl_Sectors.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Sector",
                            TableName = "Tbl_Sectors",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Sectors"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";

                            simpleButton3_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            DescArtxt.Text = "";
                            simpleButton4_Click(sender, e);
                        }

                    }




                    DescEtxt.Select();
                    this.ActiveControl = DescEtxt;
                    DescEtxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا القطاع  ؟", "حذف قطاع ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_Sectors.Find(Vint_Code);
                        DNtMDB.Tbl_Sectors.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Sector",
                            TableName = "Tbl_Sectors",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Sectors"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";
                            DescArtxt.Text = "";
                            simpleButton3_Click(sender, e);

                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            DescArtxt.Text = "";
                            DescEtxt.Focus();
                            simpleButton4_Click(sender, e);
                        }

                    }






                    

                    DescEtxt.Select();
                    this.ActiveControl = DescEtxt;
                    DescEtxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Sector to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر القطاع المراد حذفه");
                }

                DescEtxt.Select();
                this.ActiveControl = DescEtxt;
                DescEtxt.Focus();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            RowIDtxt.Text = "";


            Vint_Code = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            RowIDtxt.Text = Vint_Code.ToString();
            DescEtxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            DescArtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
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
