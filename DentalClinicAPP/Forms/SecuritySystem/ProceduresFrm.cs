using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace DentalClinicAPP.Forms.SecuritySystem
{
    public partial class ProceduresFrm : Form
    {
        ModelSecurity FsDb = new ModelSecurity();
        ModelEvents FsEvDb = new ModelEvents();
        int xcatid;
        public ProceduresFrm()
        {
            InitializeComponent();
        }

        private void ProceduresFrm_Load(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                dataGridView1.Columns["Name"].HeaderText = "Procedure Name English";
                dataGridView1.Columns["Name_Ar"].HeaderText = "Procedure Name Arabic";

                dataGridView1.Columns["Name"].Width = 200;
                dataGridView1.Columns["Name_Ar"].Width = 200;
                dataGridView1.Columns["ID"].Visible = false;

                Nametxt.Text = "";
                textBox1.Text = "";
            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                dataGridView1.Columns["Name"].HeaderText = "اسم الاجراء انجليزي";
                dataGridView1.Columns["Name_Ar"].HeaderText = "اسم الاجراء العربي";

                dataGridView1.Columns["Name"].Width = 200;
                dataGridView1.Columns["Name_Ar"].Width = 200;
                dataGridView1.Columns["ID"].Visible = false;
                Nametxt.Text = "";
                textBox1.Text = "";
            }

            Nametxt.Select();
            this.ActiveControl = Nametxt;
            Nametxt.Focus();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Nametxt.Text = "";
                textBox1.Text = "";
                xcatid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var result = FsDb.Tbl_Procedures.SingleOrDefault(x => x.ID == xcatid);
                Nametxt.Text = result.Name;
                textBox1.Text = result.Name_Ar;
                IDtxt.Text = xcatid.ToString();
            }
        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FsDb.Tbl_Procedures.Where(x => x.Name.Contains(Nametxt.Text)).ToList();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Nametxt.Text = "";
            textBox1.Text = "";
            xcatid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var result = FsDb.Tbl_Procedures.SingleOrDefault(x => x.ID == xcatid);
            Nametxt.Text = result.Name;
            textBox1.Text = result.Name_Ar;
            IDtxt.Text = xcatid.ToString();
        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("Please Enter Procedure Name English ");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
                else
                {

                    int xrows = dataGridView1.RowCount;
                    if (IDtxt.Text == "" && Nametxt.Text != "")


                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 22 && w.ProcdureId == 1);
                        //if (validationSaveUser != null)
                        //{
                        Tbl_Procedures CatF = new Tbl_Procedures
                        {
                            Name = Nametxt.Text,
                            Name_Ar = textBox1.Text

                        };
                        FsDb.Tbl_Procedures.Add(CatF);
                        //---------------خفظ ااحداث 
                        int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Procedure Add",
                            TableName = "Tbl_Procedures",
                            TableRecordId = Vint_LastRow.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ProceduresFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        FsDb.SaveChanges();
                        MessageBox.Show("Saved");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        Nametxt.Text = "";
                        textBox1.Text = "";
                        IDtxt.Text = "";
                        Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  اجراء  .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                    else
                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 22 && w.ProcdureId == 3);
                        //if (validationSaveUser != null)
                        //{
                        xcatid = int.Parse(IDtxt.Text);
                        var result = FsDb.Tbl_Procedures.SingleOrDefault(x => x.ID == xcatid);
                        result.Name = Nametxt.Text;
                        result.Name_Ar = textBox1.Text;
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 
                        //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Procedure Update",
                            TableName = "Tbl_Procedures",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ProceduresFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        MessageBox.Show("Updated");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        Nametxt.Text = "";
                        textBox1.Text = "";
                        IDtxt.Text = "";
                        Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية تعديل  اجراء  .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                }

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل الاجراء ");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    textBox1.Focus();
                }
                else
                {

                    int xrows = dataGridView1.RowCount;
                    if (IDtxt.Text == "" && textBox1.Text != "")


                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 22 && w.ProcdureId == 1);
                        //if (validationSaveUser != null)
                        //{
                        Tbl_Procedures CatF = new Tbl_Procedures
                        {
                            Name = Nametxt.Text,
                            Name_Ar = textBox1.Text

                        };
                        FsDb.Tbl_Procedures.Add(CatF);
                        //---------------خفظ ااحداث 
                        int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "اضافة اجراء",
                            TableName = "Tbl_Procedures",
                            TableRecordId = Vint_LastRow.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ProceduresFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        FsDb.SaveChanges();
                        MessageBox.Show("تم الحفظ");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        Nametxt.Text = "";
                        textBox1.Text = "";
                        IDtxt.Text = "";
                        textBox1.Select();
                        this.ActiveControl = textBox1;
                        textBox1.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  اجراء  .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                    else
                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 22 && w.ProcdureId == 3);
                        //if (validationSaveUser != null)
                        //{
                        xcatid = int.Parse(IDtxt.Text);
                        var result = FsDb.Tbl_Procedures.SingleOrDefault(x => x.ID == xcatid);
                        result.Name = Nametxt.Text;
                        result.Name_Ar = textBox1.Text;
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 
                        //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "تعديل اجراء",
                            TableName = "Tbl_Procedures",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ProceduresFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        MessageBox.Show("تم التعديل");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        Nametxt.Text = "";
                        textBox1.Text = "";
                        IDtxt.Text = "";
                        textBox1.Select();
                        this.ActiveControl = textBox1;
                        textBox1.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية تعديل  اجراء  .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                }
            }


        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.GlbV_Language == "en")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 4);
                    //if (validationSaveUser != null)
                    //{

                        int xrows = dataGridView1.RowCount;
                        if ( Nametxt.Text != "")
                        {
                            var result1 = MessageBox.Show("Are you Sure to delete this Procedure  ?", "Procedure Delete ", MessageBoxButtons.YesNo);
                            if (result1 == DialogResult.Yes)
                            {
                            xcatid = int.Parse(IDtxt.Text);
                                var result = FsDb.Tbl_Procedures.Find(xcatid);
                                FsDb.Tbl_Procedures.Remove(result);
                                FsDb.SaveChanges();
                                //---------------خفظ ااحداث 
                                //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                                SecurityEvent sev = new SecurityEvent
                                {
                                    ActionName = "Procedure Delete",
                                    TableName = "Tbl_Procedures",
                                    TableRecordId = result.ID.ToString(),
                                    Description = "",
                                    ManagementName = Program.GlbV_Management,
                                    ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                    EmployeeName = Program.GlbV_EmpName,
                                    User_ID = Program.GlbV_UserId,
                                    UserName = Program.GlbV_UserName,
                                    FormName = "ProceduresFrm"


                                };
                                FsEvDb.SecurityEvents.Add(sev);
                                FsEvDb.SaveChanges();
                                //************************************
                                MessageBox.Show(" Deleted");
                            }
                        Nametxt.Text = "";
                        textBox1.Text = "";
                        IDtxt.Text = "";
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Please Selected Procedure want to delete");
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Don't have Permision to delete procedure, Call Administrator");
                    //}
                }
                else if (Program.GlbV_Language == "ar-EG")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 4);
                    //if (validationSaveUser != null)
                    //{

                        int xrows = dataGridView1.RowCount;
                        if ( textBox1.Text != "")
                        {
                            var result1 = MessageBox.Show("هل تريد حدف هدا الاجراء  ؟", "حدف اجراء ", MessageBoxButtons.YesNo);
                            if (result1 == DialogResult.Yes)
                        {
                            xcatid = int.Parse(IDtxt.Text);
                            var result = FsDb.Tbl_Procedures.Find(xcatid);
                                FsDb.Tbl_Procedures.Remove(result);
                                FsDb.SaveChanges();
                                //---------------خفظ ااحداث 
                                //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                                SecurityEvent sev = new SecurityEvent
                                {
                                    ActionName = "حذف اجراء",
                                    TableName = "Tbl_Procedures",
                                    TableRecordId = result.ID.ToString(),
                                    Description = "",
                                    ManagementName = Program.GlbV_Management,
                                    ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                    EmployeeName = Program.GlbV_EmpName,
                                    User_ID = Program.GlbV_UserId,
                                    UserName = Program.GlbV_UserName,
                                    FormName = "ProceduresFrm"


                                };
                                FsEvDb.SecurityEvents.Add(sev);
                                FsEvDb.SaveChanges();
                                //************************************
                                MessageBox.Show("  تم الحدف");
                            }
                        Nametxt.Text = "";
                        textBox1.Text = "";
                        IDtxt.Text = "";
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                        else
                        {
                            MessageBox.Show("من فضلك حدد الاجراء المراد حدفه");
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ليس لديك صلاحية تعديل  اجراء  .. برجاء مراجعة مدير المنظومه");
                    //}
                }
            }
            catch


            {
                MessageBox.Show("هذا الاجراء لايمكن حذفه لوجود مستخدمين له", "المنظومه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FsDb.Tbl_Procedures.Where(x => x.Name_Ar.Contains(Nametxt.Text)).ToList();
        }
    }
}
