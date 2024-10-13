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
    public partial class SystemUnitesFrm : DevExpress.XtraEditors.XtraForm
    {
        ModelSecurity FsDb = new ModelSecurity();
        ModelEvents FsEvDb = new ModelEvents();
        int xcatid;
        public SystemUnitesFrm()
        {
            InitializeComponent();
        }

        private void SystemUnitesFrm_Load(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                dataGridView1.DataSource = FsDb.Tbl_SystemUnites.ToList();
                dataGridView1.Columns["Name"].HeaderText = "Unite";

                dataGridView1.Columns["Name"].Width = 420;
                dataGridView1.Columns["ID"].Visible = false;
                simpleButton3_Click(sender, e);

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                dataGridView1.DataSource = FsDb.Tbl_SystemUnites.ToList();
                dataGridView1.Columns["Name"].HeaderText = "الوحده";

                dataGridView1.Columns["Name"].Width = 420;
                dataGridView1.Columns["ID"].Visible = false;
                simpleButton4_Click(sender, e);
            }
            
            //dataGridView1.Columns["Tbl_Document"].Visible = false;
            Nametxt.Text = "";
            Nametxt.Select();
            this.ActiveControl = Nametxt;
            Nametxt.Focus();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Nametxt.Text = "";
                xcatid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var result = FsDb.Tbl_SystemUnites.SingleOrDefault(x => x.ID == xcatid);
                Nametxt.Text = result.Name;
                IDtxt.Text = xcatid.ToString();
            }
        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FsDb.Tbl_SystemUnites.Where(x => x.Name.Contains(Nametxt.Text)).ToList();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Nametxt.Text = "";
            xcatid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var result = FsDb.Tbl_SystemUnites.SingleOrDefault(x => x.ID == xcatid);
            Nametxt.Text = result.Name;
            IDtxt.Text = xcatid.ToString();
        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 4);
                //if (validationSaveUser != null)
                //{
                int xrows = dataGridView1.RowCount;
                if (xrows != 0 && Nametxt.Text != "")
                {
                    var result1 = MessageBox.Show("Are you sure to Delete This unite ?", "Delete Unite  ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        var result = FsDb.Tbl_SystemUnites.Find(xcatid);
                        FsDb.Tbl_SystemUnites.Remove(result);
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 
                        //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "System unite Delete",
                            TableName = "Tbl_SystemUnites",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "SystemUnitesFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        MessageBox.Show("Deleted");
                    }
                    Nametxt.Text = "";
                    IDtxt.Text = "";
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
                else
                {
                    MessageBox.Show("Please Selecte unite to delete");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
                //}
                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية حذف  وحدة منظومة  .. برجاء مراجعة مدير المنظومه");
                //} 

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 4);
                //if (validationSaveUser != null)
                //{
                int xrows = dataGridView1.RowCount;
                if (xrows != 0 && Nametxt.Text != "")
                {
                    var result1 = MessageBox.Show("هل تريد حدف هده الوحده  ؟", "حدف وحده ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        var result = FsDb.Tbl_SystemUnites.Find(xcatid);
                        FsDb.Tbl_SystemUnites.Remove(result);
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 
                        //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "حذف وحدة منظومة",
                            TableName = "Tbl_SystemUnites",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "SystemUnitesFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        MessageBox.Show("  تم الحدف");
                    }
                    Nametxt.Text = "";
                    IDtxt.Text = "";
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
                else
                {
                    MessageBox.Show("من فضلك حدد الوحده المراد حدفه");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
                //}
                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية حذف  وحدة منظومة  .. برجاء مراجعة مدير المنظومه");
                //} 
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("Please Enter Unite Name ");
                }
                else
                {
                    int xrows = dataGridView1.RowCount;
                    if (IDtxt.Text == "" && Nametxt.Text != "")


                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 1);
                        //if (validationSaveUser != null)
                        //{
                            Tbl_SystemUnites CatF = new Tbl_SystemUnites
                            {
                                Name = Nametxt.Text,

                            };
                            FsDb.Tbl_SystemUnites.Add(CatF);
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            int Vint_LastRow = int.Parse(CatF.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "System Unite Add",
                                TableName = "Tbl_SystemUnites",
                                TableRecordId = Vint_LastRow.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "SystemUnitesFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************
                            MessageBox.Show("Saved");
                            dataGridView1.DataSource = FsDb.Tbl_SystemUnites.ToList();
                            Nametxt.Text = "";
                            IDtxt.Text = "";
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Don't have Add authority at this form");
                        //}
                    }
                    else
                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 3);
                        //if (validationSaveUser != null)
                        //{
                            xcatid = int.Parse(IDtxt.Text);
                            var result = FsDb.Tbl_SystemUnites.SingleOrDefault(x => x.ID == xcatid);
                            result.Name = Nametxt.Text;
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "System Unite Update",
                                TableName = "Tbl_SystemUnites",
                                TableRecordId = result.ID.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "SystemUnitesFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************
                            MessageBox.Show("Updated");
                            dataGridView1.DataSource = FsDb.Tbl_SystemUnites.ToList();
                            Nametxt.Text = "";
                            IDtxt.Text = "";
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Don't have upadte authority   at this form");
                        //}
                    }
                }

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل اوحده ");
                }
                else
                {
                    int xrows = dataGridView1.RowCount;
                    if (IDtxt.Text == "" && Nametxt.Text != "")


                    {
                        var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 1);
                        if (validationSaveUser != null)
                        {
                            Tbl_SystemUnites CatF = new Tbl_SystemUnites
                            {
                                Name = Nametxt.Text,

                            };
                            FsDb.Tbl_SystemUnites.Add(CatF);
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            int Vint_LastRow = int.Parse(CatF.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "اضافة وحدة منظومة",
                                TableName = "Tbl_SystemUnites",
                                TableRecordId = Vint_LastRow.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "SystemUnitesFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************
                            MessageBox.Show("تم الحفظ");
                            dataGridView1.DataSource = FsDb.Tbl_SystemUnites.ToList();
                            Nametxt.Text = "";
                            IDtxt.Text = "";
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                        else
                        {
                            MessageBox.Show("ليس لديك صلاحية اضافة  وحدة منظومة  .. برجاء مراجعة مدير المنظومه");
                        }
                    }
                    else
                    {
                        var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 20 && w.ProcdureId == 3);
                        if (validationSaveUser != null)
                        {
                            xcatid = int.Parse(IDtxt.Text);
                            var result = FsDb.Tbl_SystemUnites.SingleOrDefault(x => x.ID == xcatid);
                            result.Name = Nametxt.Text;
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "تعديل وحدة منظومة",
                                TableName = "Tbl_SystemUnites",
                                TableRecordId = result.ID.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "SystemUnitesFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************
                            MessageBox.Show("تم التعديل");
                            dataGridView1.DataSource = FsDb.Tbl_SystemUnites.ToList();
                            Nametxt.Text = "";
                            IDtxt.Text = "";
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                        else
                        {
                            MessageBox.Show("ليس لديك صلاحية تعديل   وحدة منظومة  .. برجاء مراجعة مدير المنظومه");
                        }
                    }
                }
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
    }
}
