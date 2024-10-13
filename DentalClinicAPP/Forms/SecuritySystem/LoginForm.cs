using DentalClinicAPP;
using DentalClinicAPP.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DentalClinicAPP.Classes;
using System.Data.Entity;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;

namespace DentalClinicAPP.Forms.SecuritySystem
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        int Vint_UserID;
        public LoginForm()
        {
            InitializeComponent();
            LangTxtBox.Text = "en";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Vstr_PassIncr = Security.Encrypt_MD5(Passwordtxt.Text);
           
            
            var result = DNtSCDB.Tbl_User.FirstOrDefault(x => x.Name == Nametxt.Text && x.Password == Vstr_PassIncr);
           
            if (result != null)
            {

                if (result.UserStatus_ID == 1)
                {
                    UserFormsProcedures(result);

                    var listRemoveRange = DNtEVDB.SecurityUserActivities.Where(x => x.User_ID == result.ID && x.LogoutTime == null && x.PeriodTime == null);
                    if (listRemoveRange != null)
                    {
                        DNtEVDB.SecurityUserActivities.RemoveRange(listRemoveRange);
                        DNtEVDB.SaveChanges();
                    }
                    Program.GlbV_UserName = result.Name;
                    int? EmpV = result.Employee_id;

                    Program.GlbV_CPassword = Vstr_PassIncr;
                    var resultR = DNtSCDB.Tbl_User_SysUnites.SingleOrDefault(z => z.ID == Vint_UserID && z.SysUnite_StatusID == 1);
                    Program.GlbV_Language = LangTxtBox.Text;
                    Program.GlbV_UserId = result.ID;
                    Program.GlbV_UserTypeId = result.Tbl_UserType.ID;
                    Program.GlbV_UserType = result.Tbl_UserType.Name;
                    //Program.GlbV_SysUnite_ID = resultR.SysUnites_ID;
                    Program.GlbV_EmpName = result.Tbl_Employee.Name;
                    DateTime td = Convert.ToDateTime(Convert.ToDateTime(GetServerDate.Cs_GetServerDate()).ToString("yyyy/MM/dd HH:mm:ss tt"));
                    
                    Program.GlbV_DateTime = td.ToString();
                    SecurityUserActivity SEvent = new SecurityUserActivity
                    {

                        Name = result.Name,
                        UserName = result.UserName,
                        User_ID = result.ID,
                        LoginTime = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss"))
                    };
                    DNtEVDB.SecurityUserActivities.Add(SEvent);
                    DNtEVDB.SaveChanges();
                    if (result.ID > 0)
                    {

                        //Program.GlbV_UserName = 
                        this.Close();
                        Thread th = new Thread(openform);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();
                        //MessageBox.Show(result.Count().ToString());


                    }
                }
                else
                {
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("This user dosen't have permision, Please Call Administrator!  ");
                    }
                    else
                    {
                        MessageBox.Show("هدا المستخدم غير مفعل برجاء مراجعة مدير المنظومه");
                    }
                   
                }
            }

            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("This user or Password is wrong!  ");
                }
                else
                {
                    MessageBox.Show("اسم المستخدم او كلمة المرور خطأ");
                }
               
            }
        }
        private void UserFormsProcedures(Tbl_User result)
        {
            //صفحات المستخدم
            Program.SecurityFormsList = DNtSCDB.Tbl_UsersProcedureForm.Include(t => t.Tbl_ProceduresForms).Where(x => x.User_ID == result.ID).Select(x => x.Tbl_ProceduresForms.Form_ID).Distinct().ToList();

            //إجراءات صفحات المستخدم 
            var proceduresForms = DNtSCDB.Tbl_UsersProcedureForm.Include(t => t.Tbl_ProceduresForms).Where(x => x.User_ID == result.ID).Select(x => x.Tbl_ProceduresForms).ToList();
            foreach (var proceduresForm in proceduresForms)
            {
                Program.SecurityProceduresList.Add(new DentalClinicAPP.Classes.FormProcedures() { FormId = proceduresForm.Form_ID, ProcdureId = proceduresForm.Procedure_ID });
            }

        }
        void openform()
        {
            Application.Run(new MainFrm());
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            simpleButton4_Click(sender, e);
            Nametxt.Focus();
        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Passwordtxt.Focus();
            }
        }

        private void Passwordtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_Cities.OrderBy(x => x.Name_E).ToList();
           Program.GlbV_Language = "en";
            LangTxtBox.Text = "en";
            Nametxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            Program.GlbV_Language = "ar-EG";
            LangTxtBox.Text = "ar-EG";
            Nametxt.Focus();
        }
    }
}