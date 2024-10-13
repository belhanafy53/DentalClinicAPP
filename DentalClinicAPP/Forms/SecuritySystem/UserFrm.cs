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
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;

namespace DentalClinicAPP.Forms.SecuritySystem
{
    public partial class UserFrm : DevExpress.XtraEditors.XtraForm
    {

        Model1 FScDb = new Model1();
        ModelEvents FsEvDb = new ModelEvents();
        ModelSecurity ScDb = new ModelSecurity();
        int Vint_UserID;
        string Image_Path = "";
        string Vstr_PassIncr;
        int Vint_UserStatus = 0;
        int Vint_TransfeerUser;
        string Current_Path = Environment.CurrentDirectory;
        public UserFrm()
        {
            InitializeComponent();
            comboBox1.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Nametxt.Text == "" || Passwordtxt.Text == "" || ConfirmPasswordtxt.Text == "" )
            {
                MessageBox.Show("من فضلك اكمل البيانات المطلوبه");
            }
            else
            {
               
                if (comboBox2.SelectedItem == "")
                {
                    MessageBox.Show("حدد نوع المستخدم ");
                    comboBox2.Focus();
                }
                else if (comboBox3.SelectedItem == "")
                {
                    MessageBox.Show("حدد حالة المستخدم فعال / غير فعال");
                    comboBox3.Focus();
                }
                else
                {
    
                    Vint_UserStatus = 0;
                    Vstr_PassIncr = Security.Encrypt_MD5(Passwordtxt.Text);
                    Tbl_User u = new Tbl_User
                    {

                        Name = Nametxt.Text,
                        Password = Vstr_PassIncr,
                        ImagePath = Image_Path,
                       
                        UserType_ID = int.Parse(comboBox2.SelectedValue.ToString()),
                        UserStatus_ID = int.Parse(comboBox3.SelectedValue.ToString())


                    };

                    Tbl_User_SysUnites UsrSysU = new Tbl_User_SysUnites
                    {
                        SysUnites_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                       
                        //From_Date = dateTimePicker1.Value.Date,
                        //To_Date = dateTimePicker2.Value.Date,
                        // *********************- الوحده الحالية والفعاله للمستخدم
                        SysUnite_StatusID = 1,
                        //User_ID = xidu

                    };

                    int vint_empId = int.Parse(textBox1.Text);

                    if (textBox2.Text == "" && Vint_TransfeerUser == 1)
                    {
                        var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 24 && w.ProcdureId == 1);
                        if (validationSaveUser != null)
                        {
                            var resultSysUserOld = ScDb.Tbl_User_SysUnites.SingleOrDefault(h => h.ID == Vint_UserID && h.SysUnite_StatusID == 1);
                            if (resultSysUserOld != null)
                            {
                                DateTime dt = DateTime.Now.AddDays(-1);

                                DateTime newdt = new DateTime(dt.Year, dt.Month, dt.Day);
                                resultSysUserOld.To_Date = Convert.ToDateTime(newdt);
                                //**********************************الوحده غير الفعاله للمستخدم
                                resultSysUserOld.SysUnite_StatusID = 0;
                            }
                            //ScDb.Tbl_User_SysUnites.Add(UsrSysU);
                            //ScDb.SaveChanges();
                            var listuser = ScDb.Tbl_User.SingleOrDefault(x => x.ID == Vint_UserID);
                            int vint_userid = listuser.ID;
                            var resultSysUsernew = ScDb.Tbl_User_SysUnites.SingleOrDefault(h => h.ID == Vint_UserID && h.SysUnite_StatusID == 1);
                            resultSysUsernew.User_ID = vint_userid;

                            //*****************حذف كل الصلاحيات القديمه للمستخدم
                            ScDb.Tbl_UserAuthForms.RemoveRange(ScDb.Tbl_UserAuthForms.Where(h => h.User_ID == vint_userid));
                            ScDb.Tbl_UsersProcedureForm.RemoveRange(ScDb.Tbl_UsersProcedureForm.Where(h => h.User_ID == vint_userid));
                            ScDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            //int Vint_LastRow = int.Parse(UsrSysU.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "اضافة المستخدم على وحدة منظومة",
                                TableName = "Tbl_User_SysUnites",
                                //TableRecordId = Vint_LastRow.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "UsersFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************
                            MessageBox.Show("تم الحفظ ");
                            Nametxt.Text = "";
                            Passwordtxt.Text = "";
                            ConfirmPasswordtxt.Text = "";
                            textBox2.Text = "";
                            
                          
                            comboBox2.SelectedValue = -1;
                            comboBox3.SelectedValue = -1;
                           
                          
                            
                        }
                        else
                        {
                            MessageBox.Show("ليس لديك صلاحية اضافة  مستخدم لوحدة .. برجاء مراجعة مدير المنظومه");
                        }
                    }
                    else if (textBox2.Text == "" && Vint_TransfeerUser == 0)
                    {
                        var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 24 && w.ProcdureId == 1);
                        if (validationSaveUser != null)
                        {
                            ScDb.Tbl_User.Add(u);
                            //ScDb.Tbl_User_SysUnites.Add(UsrSysU);
                            ScDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            int Vint_LastRow = int.Parse(u.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "اضافة مستخدم ",
                                TableName = "Tbl_User",
                                TableRecordId = Vint_LastRow.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "UsersFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();

                            MessageBox.Show("تم الحفظ ");
                            Nametxt.Text = "";
                            Passwordtxt.Text = "";
                            ConfirmPasswordtxt.Text = "";
                            textBox2.Text = "";
                          
                            comboBox2.SelectedValue = -1;
                            comboBox3.SelectedValue = -1;
                            
                        }
                        else
                        {
                            MessageBox.Show("ليس لديك صلاحية اضافة  مستخدم  .. برجاء مراجعة مدير المنظومه");
                        }
                    }
                    else if (textBox2.Text != "" && Vint_TransfeerUser == 0)
                    {
                        var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 24 && w.ProcdureId == 3);
                        if (validationSaveUser != null)
                        {
                            Vint_UserStatus = int.Parse(comboBox3.SelectedValue.ToString());

                            Vstr_PassIncr = Security.Encrypt_MD5(Passwordtxt.Text);
                            int XuserID = int.Parse(textBox2.Text);
                            int XEMPID = int.Parse(textBox1.Text);
                            //*********************Tbl_User
                            var result = ScDb.Tbl_User.SingleOrDefault(h => h.ID == XuserID);
                            u.ID = result.ID;
                            result.Name = Nametxt.Text;
                            result.Password = Vstr_PassIncr;
                            result.UserType_ID = int.Parse(comboBox2.SelectedValue.ToString());
                            result.ImagePath = Environment.CurrentDirectory + "\\Images\\" + u.ID + ".jpg";
                            result.UserStatus_ID = Vint_UserStatus;

                            ScDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            //int Vint_LastRowS = int.Parse(UsrSysU.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "تعديل  بيانات  مستخدم",
                                TableName = "Tbl_User",
                                TableRecordId = result.ID.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "UsersFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************

                            //**********************Tbl_User_SysUnites
                            var resultSysUser = ScDb.Tbl_User_SysUnites.SingleOrDefault(h => h.User_ID == Vint_UserID && h.SysUnite_StatusID == 1);
                            
                            resultSysUser.User_ID = u.ID;
                              resultSysUser.SysUnite_StatusID = 1;
                            ScDb.SaveChanges();
                             MessageBox.Show("تم التعديل ");
                            Nametxt.Text = "";
                            Passwordtxt.Text = "";
                            ConfirmPasswordtxt.Text = "";
                            textBox2.Text = "";
                            
                            comboBox2.SelectedValue = -1;
                            comboBox3.SelectedValue = -1;
                          
                        }
                        else
                        {
                            MessageBox.Show("ليس لديك صلاحية تعديل بيانات مستخدم  .. برجاء مراجعة مدير المنظومه");
                        }
                    }
                    
                    if (Image_Path != "")
                    {

                        //Image_Path = Environment.CurrentDirectory + "\\Images\\" + u.ID + ".jpg";
                        string NewPath = Environment.CurrentDirectory + "\\Images\\" + u.ID + ".jpg";

                        //File.Copy(Image_Path, NewPath, true);
                        NewPath = Image_Path;
                        //dataGridView1.DataSource = ScDb.Tbl_User.ToList();
                        Nametxt.Text = "";
                        Passwordtxt.Text = "";
                        
                        ConfirmPasswordtxt.Text = "";
                        //PicturePath.Image = DentalClinicAPP.Properties.Resources.download__2_;
                       


                    }

                }
            }
        }

        private void PicturePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PicturePath.ImageLocation = dialog.FileName;
                Image_Path = dialog.FileName;
            }
        }

        private void UserFrm_Load(object sender, EventArgs e)
        {

           
        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView3.RowCount > 0)
                {
                    dataGridView3.Focus();
                }
                else
                {
                    Cardtxt.Focus();
                }
            }
        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {
            //var listSearchUser = (from usr in ScDb.Tbl_User
            //   join us in ScDb.Tbl_User_SysUnites on usr.ID equals us.User_ID
            //   where (us.SysUnite_StatusID == 1 && usr.UserName.Contains(Nametxt.Text))

            //   select new
            //   {
            //       ID = usr.ID,
            //       Name = usr.Name,
            //       UserType_ID = usr.UserType_ID,
            //       UserName = usr.UserName,
            //       //UserSysunitID = us.Tbl_SystemUnites.ID,
            //       UserType = usr.Tbl_UserType.Name,
            //       UserImage = usr.ImagePath,
            //       UserFDate = us.From_Date,
            //       UserTDate = us.To_Date,
            //       UserStatus_ID = usr.UserStatus_ID,


            //       //UserSysUnit = us.Tbl_SystemUnites.Name


            //   }).Take(30).OrderBy(x => x.UserType).ToList();
             
            //dataGridView3.DataSource = listSearchUser;
            //dataGridView3.Columns["ID"].Visible = false;
            //dataGridView3.Columns["UserName"].Visible = true;
            //dataGridView3.Columns["UserName"].HeaderText = "اسم المستخدم";
            //dataGridView3.Columns["UserName"].Width = 200;
            //dataGridView3.Columns["Name"].Visible = false;

            //dataGridView3.Columns["UserType"].HeaderText = "نوع المستخدم";
            //dataGridView3.Columns["UserType"].Width = 100;

            //dataGridView3.Columns["UserSysUnit"].HeaderText = "العيادة";
            //dataGridView3.Columns["UserSysUnit"].Width = 100;

            //dataGridView3.Columns["UserSysunitID"].Visible = false;

            //dataGridView3.Columns["UserType_ID"].Visible = false;
            //dataGridView3.Columns["UserImage"].Visible = false;

            //dataGridView3.Columns["UserFDate"].Visible = false;

            //dataGridView3.Columns["UserTDate"].Visible = false;
            //dataGridView3.Columns["UserStatus_ID"].Visible = false;
        }

        private void Passwordtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmPasswordtxt.Focus();
            }
        }

        private void ConfirmPasswordtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Passwordtxt.Text != ConfirmPasswordtxt.Text)
                {
                    MessageBox.Show("كلمة المرور غير متطابقه");
                    ConfirmPasswordtxt.Text = "";
                    ConfirmPasswordtxt.Focus();
                }
                else
                {
                    comboBox2.Focus();
                }
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker1.Focus();
            }
           
        }

        private void Cardtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                if ((Cardtxt.Text).Length == 14)
                {
                    NameUstxt.Text = (Cardtxt.Text).Substring(7).ToString();
                    Passwordtxt.Focus();
                }
                else  
                {
                    MessageBox.Show("من فضلك ادخل الرقم القومي بصورة صحيحه");
                    NameUstxt.Text = "";
                    Cardtxt.Focus();

                }
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Nametxt.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker2.Focus();
            }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}