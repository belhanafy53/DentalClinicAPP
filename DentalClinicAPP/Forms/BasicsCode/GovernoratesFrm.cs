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
    public partial class GovernoratesFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();


        int Vint_Code = 0;
        public GovernoratesFrm()
        {
            InitializeComponent();
        }
        private void Dg_E()
        {
            dataGridView1.DataSource = DNtMDB.Tbl_Governerate.OrderBy(x=>x.ID).ToList();
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            //dataGridView1.Columns["code"].Visible = true;
            dataGridView1.Columns["Name_E"].Visible = true;
            dataGridView1.Columns["Name_Ar"].Visible = true;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Tbl_Cities"].Visible = false;
            dataGridView1.Columns["Name_E"].Width = 200;
            dataGridView1.Columns["Name_Ar"].Width = 200;
            DescEtxt.Focus();
        }
        private void Dg_Ar()
        {
            dataGridView1.DataSource = DNtMDB.Tbl_Governerate.OrderBy(x => x.ID).ToList();
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            //dataGridView1.Columns["code"].HeaderText = "الكود";
            dataGridView1.Columns["Name_E"].HeaderText = "الاسم الانجليزي";
            dataGridView1.Columns["Name_Ar"].HeaderText = "الاسم العربي";
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Tbl_Cities"].Visible = false;
            dataGridView1.Columns["Name_E"].Width = 200;
            dataGridView1.Columns["Name_Ar"].Width = 200;
            DescEtxt.Focus();
        }
        private void GovernoratesFrm_Load(object sender, EventArgs e)
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

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Governerate.OrderBy(x => x.ID).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_Ar();
            LangTxtBox.Text = "ar-EG";
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
            var listAnalysis = DNtMDB.Tbl_Governerate.OrderBy(x => x.ID).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_E();
            LangTxtBox.Text = "en";
            DescEtxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DescEtxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter Code Governerate want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل كود المحافظه المراد اضافته !");
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
                    Tbl_Governerate Act = new Tbl_Governerate
                    {
                        
                        Name_E = DescEtxt.Text,
                        Name_Ar = DescArtxt.Text,


                    };
                    DNtMDB.Tbl_Governerate.Add(Act);
                    DNtMDB.SaveChanges();
                    int Vint_LastRow = Act.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Add",
                        TableName = "Tbl_Governerate",
                        TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Governerates "


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
                    var result = DNtMDB.Tbl_Governerate.FirstOrDefault(x => x.ID == Vint_Code);
                    if (result != null)
                    {
                        
                        result.Name_E = DescEtxt.Text;
                        result.Name_Ar = DescArtxt.Text;
                    }

                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governerate Update",
                        TableName = "Tbl_Governerate",
                        TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Governerates"
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
                    var result1 = MessageBox.Show("Do you want to delete this Governerate  ?", "Delete Activity ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {


                        var resultR = DNtMDB.Tbl_Governerate.Find(Vint_Code);
                        DNtMDB.Tbl_Governerate.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Governerate",
                            TableName = "Tbl_Governerate",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Governerates"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";
                           
                            Dg_E();
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            DescArtxt.Text = "";
                            Dg_Ar();
                        }

                    }

                   


                    DescEtxt.Select();
                    this.ActiveControl = DescEtxt;
                    DescEtxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذه المحافظه  ؟", "حذف مدينة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_Governerate.Find(Vint_Code);
                        DNtMDB.Tbl_Governerate.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Governerate",
                            TableName = "Tbl_Governerate",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Governerates"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";
                            DescArtxt.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            DescArtxt.Text = "";
                            DescEtxt.Focus();
                        }

                    }






                    var listGoverneratesSerch = (from Act in DNtMDB.Tbl_Governerate

                                             select new
                                             {
                                               
                                                 Dec_E = Act.Name_E,
                                                 Name_Ar = Act.Name_Ar,

                                             }).OrderBy(t => t.Dec_E).ToList();

                    dataGridView1.DataSource = listGoverneratesSerch;

                    DescEtxt.Select();
                    this.ActiveControl = DescEtxt;
                    DescEtxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Governerate to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر المحافظه المراد حذفه");
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
            

            Vint_Code =int.Parse( dataGridView1.CurrentRow.Cells[0].Value.ToString());
            RowIDtxt.Text = Vint_Code.ToString();
            DescEtxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            DescArtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void DescEtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DescEtxt.Focus();
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
