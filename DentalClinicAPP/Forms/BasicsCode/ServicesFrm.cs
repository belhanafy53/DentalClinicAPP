﻿using System;
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
    public partial class ServicesFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        int Vint_Servicesid = 0;
        public ServicesFrm()
        {
            InitializeComponent();
            //LangTxtBox.Text = "en";
        }

        private void Services_Load(object sender, EventArgs e)
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
            //var listServiceis = DNtMDB.Tbl_Service.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listServiceis;

            dataGridView1.Columns["ID"].Visible = false;
           
            dataGridView1.Columns["Name"].Width = 300;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            //var listServiceis = DNtMDB.Tbl_Service.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listServiceis;
            dataGridView1.Columns["ID"].Visible = false;
           
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "en";
            ServicesNameTxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            //var listServiceis = DNtMDB.Tbl_Service.OrderBy(x => x.Name).ToList();
            //dataGridView1.DataSource = listServiceis;
            dataGridView1.Columns["ID"].Visible = false;
           
            dataGridView1.Columns["Name"].Width = 300;
            LangTxtBox.Text = "ar-EG";
            ServicesNameTxt.Focus();
        }

        private void ServicesNameTxt_TextChanged(object sender, EventArgs e)
        {
            //var listServiceisSerch = (from MdAnls in DNtMDB.Tbl_Service
            //                         where (MdAnls.Name.Contains(ServicesNameTxt.Text))
            //                         select new
            //                         {
            //                             ID = MdAnls.ID,
            //                             Name = MdAnls.Name,


            //                         }).OrderBy(t => t.Name).ToList();

            //dataGridView1.DataSource = listServiceisSerch;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ServicesNameTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter name Serviceis want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل اسم الخدمة المراد اضافته !");
                }

            }
            else
            {
                if (textBox1.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Tbl_Service MedAnlys = new Tbl_Service
                    //{
                    //    Name = ServicesNameTxt.Text,

                    //};

                    //DNtMDB.Tbl_Service.Add(MedAnlys);
                    DNtMDB.SaveChanges();
                    //long Vint_LastRow = MedAnlys.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Serviceis Add",
                        TableName = "Tbl_Service",
                        //TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "ServicesFrm"


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
                    ServicesNameTxt.Text = "";

                    ServicesNameTxt.Focus();
                    ServicesNameTxt.Select();
                    this.ActiveControl = ServicesNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Service.ToList();
                    ServicesNameTxt.Focus();
                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Serviceis");
                    //}
                }
                else if (textBox1.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    Vint_Servicesid = int.Parse(textBox1.Text);
                    //var result = DNtMDB.Tbl_Service.SingleOrDefault(x => x.ID == Vint_Servicesid);
                    //result.Name = ServicesNameTxt.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Serviceis Update",
                        TableName = "Tbl_Service",
                        //TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "ServicesFrm"


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
                    ServicesNameTxt.Text = "";

                    this.ActiveControl = textBox1;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Service.ToList();
                    ServicesNameTxt.Focus();

                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Serviceis");
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
                    var result1 = MessageBox.Show("Do you want to delete this Service  ؟", "Delete Service ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Servicesid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Service.Find(Vint_Servicesid);
                        //DNtMDB.Tbl_Service.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Service",
                            TableName = "Tbl_Service",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ServicesFrm"


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
                    ServicesNameTxt.Text = "";



                    ServicesNameTxt.Select();
                    this.ActiveControl = ServicesNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Service.ToList();
                    ServicesNameTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا الخدمة  ؟", "حذف الخدمة ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        Vint_Servicesid = int.Parse(textBox1.Text);
                        //var resultR = DNtMDB.Tbl_Service.Find(Vint_Servicesid);
                        //DNtMDB.Tbl_Service.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Service",
                            TableName = "Tbl_Service",
                            //TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ServicesFrm"


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
                    ServicesNameTxt.Text = "";



                    ServicesNameTxt.Select();
                    this.ActiveControl = ServicesNameTxt;

                    //dataGridView1.DataSource = DNtMDB.Tbl_Service.ToList();
                    ServicesNameTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Service to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر الخدمة المراد حذفه");
                }

                ServicesNameTxt.Select();
                this.ActiveControl = ServicesNameTxt;
                ServicesNameTxt.Focus();
            }
            //}
            //else
            //{
            //    MessageBox.Show("You Don't have permition to add Serviceis");
            //}

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Vint_Servicesid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox1.Text = Vint_Servicesid.ToString();
            ServicesNameTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}