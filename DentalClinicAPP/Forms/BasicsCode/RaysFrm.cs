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
    public partial class RaysFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Raysid = 0;

        public RaysFrm()
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
            //var listRays = DNtMDB.TBL_Rays.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listRays;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["RangeFrom"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["TBL_RaysPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "en";
            RaysNameTxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            //var listRays = DNtMDB.TBL_Rays.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listRays;
            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["RangeFrom"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["TBL_RaysPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "ar-EG";
            RaysNameTxt.Focus();
        }

        private void RaysFrm_Load(object sender, EventArgs e)
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
            //var listRays = DNtMDB.TBL_Rays.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listRays;

            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["RangeFrom"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["RangeTo"].Visible = false;
            //dataGridView1.Columns["TBL_RaysPationtMedicalVisit"].Visible = false;
            dataGridView1.Columns["Name"].Width = 300;
        }

       

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (RaysNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name medical Rays want to add !  ");
                    RaysNameTxt.Focus();
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم الاشعه المراد اضافته !");
                    RaysNameTxt.Focus();
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //TBL_Rays MedAnlys = new TBL_Rays
                    //{
                    //    Name = RaysNameTxt.Text,

                    //};

                    //DNtMDB.TBL_Rays.Add(MedAnlys);
                    DNtMDB.SaveChanges();
                    //long Vint_LastRow = MedAnlys.ID;
                    ////---------------خفظ ااحداث 
                    //SecurityEvent sev = new SecurityEvent
                    //{
                    //    ActionName = "Medical Rays Add",
                    //    TableName = "TBL_Rays",
                    //    TableRecordId = Vint_LastRow.ToString(),
                    //    Description = "",
                    //    ManagementName = Program.GlbV_Management,
                    //    ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                    //    EmployeeName = Program.GlbV_EmpName,
                    //    User_ID = Program.GlbV_UserId,
                    //    UserName = Program.GlbV_UserName,
                    //    FormName = "RaysFrm"


                    //};
                    //DNtEVDB.SecurityEvents.Add(sev);
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
                    RaysNameTxt.Text = "";

                    RaysNameTxt.Focus();
                    RaysNameTxt.Select();
                    this.ActiveControl = RaysNameTxt;

                    //dataGridView1.DataSource = DNtMDB.TBL_Rays.ToList();
                    RaysNameTxt.Focus();
                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medical Rays");
                    //}
                }
                else if (textBox1.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    Vint_Raysid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.TBL_Rays.SingleOrDefault(x => x.ID == Vint_Raysid);
                    //result.Name = RaysNameTxt.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Medical Rays Update",
                        TableName = "TBL_Rays",
                        //TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "RaysFrm"


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
                    RaysNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.TBL_Rays.ToList();
                    RaysNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Rays");
                    //}
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Raysid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_Raysid.ToString();
            RaysNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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

                        Vint_Raysid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.TBL_Rays.Find(Vint_Raysid);
                        //DNtMDB.TBL_Rays.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Analys",
                            TableName = "TBL_Rays",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "RaysFrm"


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
                    RaysNameTxt.Text = "";



                    RaysNameTxt.Select();
                    this.ActiveControl = RaysNameTxt;

                    //dataGridView1.DataSource = DNtMDB.TBL_Rays.ToList();
                    RaysNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا الاشعه  ؟", "حذف الاشعه ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Raysid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.TBL_Rays.Find(Vint_Raysid);
                        //DNtMDB.TBL_Rays.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Analys",
                            TableName = "TBL_Rays",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "RaysFrm"


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
                    RaysNameTxt.Text = "";



                    RaysNameTxt.Select();
                    this.ActiveControl = RaysNameTxt;

                    //dataGridView1.DataSource = DNtMDB.TBL_Rays.ToList();
                    RaysNameTxt.Focus();
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
                    MessageBox.Show("من فضلك اختر الاشعه المراد حذفه");
                }

                textBox1.Select();
                this.ActiveControl = textBox1;
                textBox1.Focus();
            }
            //}
            //else
            //{
            //    MessageBox.Show("You Don't have permition to add Medical Rays");
            //}
        }

        private void RaysNameTxt_TextChanged_1(object sender, EventArgs e)
        {
            //var listRaysSerch = (from MdAnls in DNtMDB.TBL_Rays
            //                     where (MdAnls.Name.Contains(RaysNameTxt.Text))
            //                     select new
            //                     {
            //                         ID = MdAnls.ID,
            //                         Name = MdAnls.Name,


            //                     }).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listRaysSerch;
        }
    }
}