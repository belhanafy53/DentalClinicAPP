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

namespace DentalClinicAPP.Forms.SecuritySystem
{
    public partial class UserTypeFrm : DevExpress.XtraEditors.XtraForm
    {
        ModelSecurity FsDb = new ModelSecurity();
        ModelEvents FsEvDb = new ModelEvents();
        int xcatid;
        public UserTypeFrm()
        {
            InitializeComponent();
        }

        private void UserTypeFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FsDb.Tbl_UserType.ToList();
            dataGridView1.Columns["Name"].HeaderText = "النوع";

            dataGridView1.Columns["Name"].Width = 420;
            dataGridView1.Columns["ID"].Visible = false;
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
                var result = FsDb.Tbl_UserType.SingleOrDefault(x => x.ID == xcatid);
                Nametxt.Text = result.Name;
                IDtxt.Text = xcatid.ToString();
            }
        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FsDb.Tbl_UserType.Where(x => x.Name.Contains(Nametxt.Text)).ToList();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Nametxt.Text = "";
            xcatid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var result = FsDb.Tbl_UserType.SingleOrDefault(x => x.ID == xcatid);
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
            var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 19 && w.ProcdureId == 4);
            if (validationSaveUser != null)
            {
                int xrows = dataGridView1.RowCount;
                if (xrows != 0 && Nametxt.Text != "")
                {
                    var result1 = MessageBox.Show("هل تريد حدف هدا النوع  ؟", "حدف نوع ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        var result = FsDb.Tbl_UserType.Find(xcatid);
                        FsDb.Tbl_UserType.Remove(result);
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 
                        //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "حذف نوع مستخدم",
                            TableName = "Tbl_UserType",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "UserTypeFrm"


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
                    MessageBox.Show("من فضلك حدد النوع المراد حدفه");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
            }
            else
            {
                MessageBox.Show("ليس لديك صلاحية حذف  نوع مستخدم  .. برجاء مراجعة مدير المنظومه");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Nametxt.Text == "")
            {
                MessageBox.Show("من فضلك ادخل النوع ");
            }
            else
            {
                int xrows = dataGridView1.RowCount;
                if (IDtxt.Text == "" && Nametxt.Text != "")


                {
                    var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 19 && w.ProcdureId == 1);
                    if (validationSaveUser != null)
                    {
                        Tbl_UserType CatF = new Tbl_UserType
                        {
                            Name = Nametxt.Text,

                        };
                        FsDb.Tbl_UserType.Add(CatF);
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 
                        int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "اضافة نوع مستخدم",
                            TableName = "Tbl_UserType",
                            TableRecordId = Vint_LastRow.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "UserTypeFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        MessageBox.Show("تم الحفظ");
                        dataGridView1.DataSource = FsDb.Tbl_UserType.ToList();
                        Nametxt.Text = "";
                        IDtxt.Text = "";
                        Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                    }
                    else
                    {
                        MessageBox.Show("ليس لديك صلاحية اضافة  نوع مستخدم  .. برجاء مراجعة مدير المنظومه");
                    }
                }
                else
                {
                    var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 19 && w.ProcdureId == 3);
                    if (validationSaveUser != null)
                    {
                        xcatid = int.Parse(IDtxt.Text);
                        var result = FsDb.Tbl_UserType.SingleOrDefault(x => x.ID == xcatid);
                        result.Name = Nametxt.Text;
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 
                        //int Vint_LastRow = int.Parse(CatF.ID.ToString());
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "اضافة نوع مستخدم",
                            TableName = "Tbl_UserType",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "UserTypeFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //************************************
                        MessageBox.Show("تم التعديل");
                        dataGridView1.DataSource = FsDb.Tbl_UserType.ToList();
                        Nametxt.Text = "";
                        IDtxt.Text = "";
                        Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                    }
                    else
                    {
                        MessageBox.Show("ليس لديك صلاحية تعديل  نوع مستخدم  .. برجاء مراجعة مدير المنظومه");
                    }
                }
            }
        }
    }
}
