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
    public partial class Cities : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();

        int Vint_Governorateid = 0;
        int Vint_Cityid = 0;
        public Cities()
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
            var listAnalysis = DNtMDB.Tbl_Cities.OrderBy(x => x.ID).ToList();
            var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                  //where (Cit.GovernorateID == Vint_Governorateid)
                                  select new
                                  {
                                      ID = Cit.ID,
                                      Name_E = Cit.Name_E,
                                      Name_Ar = Cit.Name_Ar,
                                      manGovernorateID = Vint_Governorateid
                                  }).OrderBy(t => t.Name_E).ToList();

            dataGridView1.DataSource = listCitysSerch;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["manGovernorateID"].Visible = false;
            //dataGridView1.Columns["Tbl_Governerate"].Visible = false;
            dataGridView1.Columns["Name_E"].Width = 200;
            dataGridView1.Columns["Name_Ar"].Width = 200;
            dataGridView1.Columns["Name_E"].HeaderText = "Arabic name";
            dataGridView1.Columns["Name_Ar"].HeaderText = "English Name";

            comboBox1.DataSource = DNtMDB.Tbl_Governerate.ToList();
            comboBox1.DisplayMember = "Name_Ar";
            comboBox1.ValueMember = "ID";
            comboBox1.Text = "Choose a Governorate";

            LangTxtBox.Text = "en";
            comboBox1.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Cities.OrderBy(x => x.ID).ToList();
            var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                  //where (Cit.GovernorateID == Vint_Governorateid)
                                  select new
                                  {
                                      ID = Cit.ID,
                                      Name_E = Cit.Name_E,
                                      Name_Ar = Cit.Name_Ar,
                                      manGovernorateID = Vint_Governorateid
                                  }).OrderBy(t => t.Name_E).ToList();

            dataGridView1.DataSource = listCitysSerch;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["manGovernorateID"].Visible = false;
            //dataGridView1.Columns["Tbl_Governerate"].Visible = false;
            dataGridView1.Columns["Name_E"].HeaderText = "المسمى العربي";
            dataGridView1.Columns["Name_Ar"].HeaderText = "المسمى الانجليزي";
            dataGridView1.Columns["Name_E"].Width = 200;
            dataGridView1.Columns["Name_Ar"].Width = 200;
            LangTxtBox.Text = "ar-EG";
            comboBox1.DataSource = DNtMDB.Tbl_Governerate.ToList();
            comboBox1.DisplayMember = "Name_E";
            comboBox1.ValueMember = "ID";
            comboBox1.Text = "اختر المحافظه";
            comboBox1.Focus();
        }

        private void Cities_Load(object sender, EventArgs e)
        {
            var listAnalysis = DNtMDB.Tbl_Cities.OrderBy(x => x.ID).ToList();
            var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                  
                                  select new
                                  {
                                      ID = Cit.ID,
                                      Name_E = Cit.Name_E,
                                      Name_Ar = Cit.Name_Ar,
                                      manGovernorateID = Vint_Governorateid
                                  }).OrderBy(t => t.Name_E).ToList();

            dataGridView1.DataSource = listCitysSerch;
            if (Program.GlbV_Language == "en")
            {
                LangTxtBox.Text = "en";
                simpleButton3_Click(sender, e);
                //comboBox1.DataSource = DNtMDB.Tbl_Governerate.ToList();
                //comboBox1.DisplayMember = "Name_E";
                //comboBox1.ValueMember = "ID";
                //comboBox1.Text = "Choose a Governorate";
                //dataGridView1.Columns["ID"].Visible = false;
                //dataGridView1.Columns["GovernorateID"].Visible = false;
                //dataGridView1.Columns["Tbl_Governerate"].Visible = false;
                //dataGridView1.Columns["Name_E"].HeaderText = "English Name";
                //dataGridView1.Columns["Name_Ar"].HeaderText = "Arabic Name";
                //dataGridView1.Columns["Name_E"].Width = 200;
                //dataGridView1.Columns["Name_Ar"].Width = 200;
            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                LangTxtBox.Text = "ar-EG";
                simpleButton4_Click(sender, e);
                //comboBox1.DataSource = DNtMDB.Tbl_Governerate.ToList();
                //comboBox1.DisplayMember = "Name_Ar";
                //comboBox1.ValueMember = "ID";
                //comboBox1.Text = "اختر المحافظه";
                //dataGridView1.Columns["ID"].Visible = false;
                //dataGridView1.Columns["GovernorateID"].Visible = false;
                //dataGridView1.Columns["Tbl_Governerate"].Visible = false;
                //dataGridView1.Columns["Name_E"].HeaderText = "المسمى الانجليزي";
                //dataGridView1.Columns["Name_Ar"].HeaderText = "المسمى العربي";
                //dataGridView1.Columns["Name_E"].Width = 200;
                //dataGridView1.Columns["Name_Ar"].Width = 200;
            }



            textBox1.Text = "";
            textBox2.Text = "";

            comboBox1.Focus();
        }

        private void CityNameTxt_TextChanged(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                if (CityNameTxt.Text != "")
                {
                    if (comboBox1.SelectedValue != null)
                    {
                        Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                        var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                              where (Cit.Name_E.Contains(CityNameTxt.Text) && Cit.GovernorateID == Vint_Governorateid)
                                              select new
                                              {
                                                  ID = Cit.ID,
                                                  Name_E = Cit.Name_E,
                                                  Name_Ar = Cit.Name_Ar,
                                                  GovernorateID = Vint_Governorateid
                                              }).OrderBy(t => t.Name_E).ToList();

                        dataGridView1.DataSource = listCitysSerch;
                        dataGridView1.Columns["ID"].Visible = false;
                        dataGridView1.Columns["GovernorateID"].Visible = false;
                        //dataGridView1.Columns["Tbl_Governerates"].Visible = false;
                        dataGridView1.Columns["Name_E"].Width = 300;
                    }
                }

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                if (CityNameTxt.Text != "")
                {
                    if (comboBox1.SelectedValue != null)
                    {
                        Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                        var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                              where (Cit.Name_Ar.Contains(CityNameTxt.Text) && Cit.GovernorateID == Vint_Governorateid)
                                              select new
                                              {
                                                  ID = Cit.ID,
                                                  Name_E = Cit.Name_E,
                                                  Name_Ar = Cit.Name_Ar,
                                                  GovernorateID = Vint_Governorateid
                                              }).OrderBy(t => t.Name_E).ToList();

                        dataGridView1.DataSource = listCitysSerch;
                        dataGridView1.Columns["ID"].Visible = false;
                        dataGridView1.Columns["GovernorateID"].Visible = false;
                        //dataGridView1.Columns["Tbl_Governerates"].Visible = false;
                        dataGridView1.Columns["Name_Ar"].Width = 300;
                    }
                }
            }
           
        }



        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
            var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                  where (Cit.GovernorateID == Vint_Governorateid)
                                  select new
                                  {
                                      ID = Cit.ID,
                                      Name_E = Cit.Name_E,
                                      Name_Ar = Cit.Name_Ar,
                                      GovernorateID = Vint_Governorateid
                                  }).OrderBy(t => t.Name_E).ToList();

            dataGridView1.DataSource = listCitysSerch;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["GovernorateID"].Visible = false;
            //dataGridView1.Columns["Tbl_Governerates"].Visible = false;
            dataGridView1.Columns["Name_E"].Width = 300;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CityNameTxt.Focus();
            }
        }

        private void CityNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (CityNameTxt.Text == "")
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
                    Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                    Tbl_Cities City = new Tbl_Cities
                    {
                        Name_E = CityNameTxt.Text,
                        GovernorateID = Vint_Governorateid,
                        Name_Ar = textBox2.Text,

                    };
                    DNtMDB.Tbl_Cities.Add(City);
                    DNtMDB.SaveChanges();
                    int Vint_LastRow = City.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Add",
                        TableName = "Tbl_Cities",
                        TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Cities "


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
                    textBox2.Text = "";
                    CityNameTxt.Text = "";

                    CityNameTxt.Focus();
                    CityNameTxt.Select();
                    this.ActiveControl = CityNameTxt;

                    Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                    var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                          where (Cit.GovernorateID == Vint_Governorateid)
                                          select new
                                          {
                                              ID = Cit.ID,
                                              Name_E = Cit.Name_E,
                                              Name_Ar = Cit.Name_Ar,
                                              manGovernorateID = Vint_Governorateid
                                          }).OrderBy(t => t.Name_E).ToList();

                    dataGridView1.DataSource = listCitysSerch;
                    dataGridView1.Columns["manGovernorateID"].Visible = false;
                    CityNameTxt.Focus();
                    //var list = DNtMDB.Tbl_Governerates.Single(x => x.ID == Vint_Governorateid);
                    comboBox1.Text = "";
                    //comboBox1.SelectedText = list.Name;
                    comboBox1.SelectedValue = Vint_Governorateid;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "ID";
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
                    var result = DNtMDB.Tbl_Cities.FirstOrDefault(x => x.ID  == Vint_Cityid);
                    result.Name_E = CityNameTxt.Text;
                    result.GovernorateID = Vint_Governorateid;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Governorate Update",
                        TableName = "Tbl_Cities",
                        TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "Cities"
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
                    textBox2.Text = "";
                    CityNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                    var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                          where (Cit.GovernorateID == Vint_Governorateid)
                                          select new
                                          {
                                              ID = Cit.ID,
                                              Name_E = Cit.Name_E,
                                              Name_Ar = Cit.Name_Ar,
                                              manGovernorateID = Vint_Governorateid
                                          }).OrderBy(t => t.Name_E).ToList();

                    dataGridView1.DataSource = listCitysSerch;

                    dataGridView1.Columns["manGovernorateID"].Visible = false;
                    //var list = DNtMDB.Tbl_Governerates.Single(x => x.ID == Vint_Governorateid);
                    comboBox1.Text = "";
                    //comboBox1.SelectedText = list.Name;
                    comboBox1.SelectedValue = Vint_Governorateid;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "ID";
                    comboBox1.Focus();


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
            textBox1.Text = "";
            CityNameTxt.Text = "";
            Vint_Cityid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Vint_Governorateid = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            CityNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            var list = DNtMDB.Tbl_Governerate.Single(x => x.ID == Vint_Governorateid);
            comboBox1.Text = "";
            comboBox1.SelectedText = list.Name_E;
            comboBox1.SelectedValue = Vint_Governorateid;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            textBox1.Text = Vint_Governorateid.ToString();
           
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
                    var result1 = MessageBox.Show("Do you want to delete this City  ؟", "Delete City ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        
                        var resultR = DNtMDB.Tbl_Cities.Find(Vint_Cityid);
                        DNtMDB.Tbl_Cities.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete City",
                            TableName = "Tbl_Cities",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Cities"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            CityNameTxt.Text = "";
                            comboBox1.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            CityNameTxt.Text = "";
                            comboBox1.Text = "";
                        }

                    }






                    Vint_Governorateid = int.Parse(comboBox1.SelectedValue.ToString());
                    var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                          where (Cit.GovernorateID == Vint_Governorateid)
                                          select new
                                          {
                                              ID = Cit.ID,
                                              Name_E = Cit.Name_E,
                                              Name_Ar = Cit.Name_Ar,
                                              GovernorateID = Vint_Governorateid
                                          }).OrderBy(t => t.Name_E).ToList();

                    dataGridView1.DataSource = listCitysSerch;
                    dataGridView1.Columns["manGovernorateID"].Visible = false;
                    //var list = DNtMDB.Tbl_Governerates.Single(x => x.ID == Vint_Governorateid);

                    //comboBox1.SelectedText = list.Name;
                    comboBox1.SelectedValue = Vint_Governorateid;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "ID";
                    //textBox1.Text = Vint_Governorateid.ToString();
                    CityNameTxt.Select();
                    this.ActiveControl = CityNameTxt;
                    CityNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذه المدينة  ؟", "حذف مدينة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_Cities.Find(Vint_Cityid);
                        DNtMDB.Tbl_Cities.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete City",
                            TableName = "Tbl_Cities",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "Cities"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            CityNameTxt.Text = "";
                            comboBox1.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            CityNameTxt.Text = "";
                            comboBox1.Text = "";
                        }

                    }
                 





                    var listCitysSerch = (from Cit in DNtMDB.Tbl_Cities
                                          where (Cit.GovernorateID == Vint_Governorateid)
                                          select new
                                          {
                                              ID = Cit.ID,
                                              Name_E = Cit.Name_E,
                                              Name_Ar = Cit.Name_Ar,
                                              manGovernorateID = Vint_Governorateid
                                          }).OrderBy(t => t.Name_E).ToList();

                    dataGridView1.DataSource = listCitysSerch;
                    dataGridView1.Columns["manGovernorateID"].Visible = false;
                    //var list = DNtMDB.Tbl_Governerates.Single(x => x.ID == Vint_Governorateid);
                    comboBox1.Text = "";
                    //comboBox1.SelectedText = list.Name;
                    comboBox1.SelectedValue = Vint_Governorateid;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "ID";
                    //textBox1.Text = Vint_Governorateid.ToString();
                    CityNameTxt.Select();
                    this.ActiveControl = CityNameTxt;
                    CityNameTxt.Focus();
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
                    MessageBox.Show("من فضلك اختر التحليل المراد حذفه");
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }
    }
    
}