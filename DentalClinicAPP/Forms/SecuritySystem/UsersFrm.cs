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
using System.Threading;

namespace DentalClinicAPP.Forms.SecuritySystem
{
    public partial class UsersFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        int Vint_UserID = 0;
        string Image_Path = "";
        string Vstr_PassIncr = "";
        int Vint_UserStatus = 0;
        int Vint_TransfeerUser = 0;
        int Vint_SysUnit = 0;
        string Current_Path = Environment.CurrentDirectory;
        public UsersFrm()
        {
            InitializeComponent();
            LangTxtBox.Text = "en";
            comboBox1.Select();
            this.ActiveControl = comboBox1;
            comboBox1.Focus();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Nametxt.Text == "" || Passwordtxt.Text == "" || ConfirmPasswordtxt.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات المطلوبه");
            }
            else
            {

                if (comboBox1.SelectedItem == "")
                {
                    MessageBox.Show("حدد العيادة ");
                    comboBox2.Focus();
                }
                else if (comboBox2.SelectedItem == "")
                {
                    MessageBox.Show("حدد نوع المستخدم ");
                    comboBox2.Focus();
                }
                else if (comboBox3.SelectedItem == "")
                {
                    MessageBox.Show("حدد حالة المستخدم فعال / غير فعال");
                    comboBox3.Focus();
                }
                else if (txtEmployee.Text == "")
                {
                    MessageBox.Show("اختر الموظف المراد انشاء حساب له");
                    txtEmployee.Focus();
                }
                else
                {


                    Vstr_PassIncr = Security.Encrypt_MD5(Passwordtxt.Text);
                    Tbl_User u = new Tbl_User
                    {

                        Name = Nametxt.Text,
                        Password = Vstr_PassIncr,
                        ImagePath = Image_Path,
                        NationalId = Cardtxt.Text,
                        UserType_ID = int.Parse(comboBox2.SelectedValue.ToString()),
                        UserStatus_ID = int.Parse(comboBox3.SelectedValue.ToString()),
                        UserName = NameUstxt.Text,
                        Employee_id = int.Parse(txtEmployeeID.Text)

                    };
                    Vint_SysUnit = int.Parse(comboBox1.SelectedValue.ToString());


                    if (textBox2.Text == "" && Vint_TransfeerUser == 1)
                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 24 && w.ProcdureId == 1);
                        //if (validationSaveUser != null)
                        //{
                        var resultSysUserOld = DNtSCDB.Tbl_User_SysUnites.SingleOrDefault(h => h.User_ID == Vint_UserID && h.SysUnite_StatusID == 1);
                        if (resultSysUserOld != null)
                        {
                            DateTime dt = DateTime.Now.AddDays(-1);

                            DateTime newdt = new DateTime(dt.Year, dt.Month, dt.Day);
                            resultSysUserOld.To_Date = Convert.ToDateTime(newdt);
                            //**********************************الوحده غير الفعاله للمستخدم
                            resultSysUserOld.SysUnite_StatusID = 0;
                        }

                        Tbl_User_SysUnites UsrSysU = new Tbl_User_SysUnites
                        {
                            SysUnites_ID = Vint_SysUnit,
                            From_Date = dateTimePicker1.Value.Date,
                            To_Date = dateTimePicker2.Value.Date,
                            // *********************- الوحده الحالية والفعاله للمستخدم
                            SysUnite_StatusID = 1,
                            User_ID = Vint_UserID,
                            Emp_ID = int.Parse(txtEmployeeID.Text)

                        };
                        DNtSCDB.Tbl_User_SysUnites.Add(UsrSysU);
                        DNtSCDB.SaveChanges();
                        var resultSysUsernew = DNtSCDB.Tbl_User_SysUnites.SingleOrDefault(h => h.User_ID == Vint_UserID && h.SysUnite_StatusID == 1);
                        resultSysUsernew.User_ID = Vint_UserID;


                        //*****************حذف كل الصلاحيات القديمه للمستخدم
                        DNtSCDB.Tbl_UserAuthForms.RemoveRange(DNtSCDB.Tbl_UserAuthForms.Where(h => h.User_ID == Vint_UserID));
                        DNtSCDB.Tbl_UsersProcedureForm.RemoveRange(DNtSCDB.Tbl_UsersProcedureForm.Where(h => h.User_ID == Vint_UserID));
                        DNtSCDB.SaveChanges();
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
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************
                        MessageBox.Show("تم الحفظ ");
                        Nametxt.Text = "";
                        Passwordtxt.Text = "";
                        ConfirmPasswordtxt.Text = "";
                        textBox2.Text = "";
                        Cardtxt.Text = "";
                        NameUstxt.Text = "";
                        txtEmployee.Text = "";
                        txtEmployeeID.Text = "";
                        UsersFrm_Load(sender, e);
                        Cmb(sender, e);



                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  مستخدم لوحدة .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                    else if (textBox2.Text == "" && Vint_TransfeerUser == 0)
                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 24 && w.ProcdureId == 1);
                        //if (validationSaveUser != null)
                        //{
                        DNtSCDB.Tbl_User.Add(u);
                        DNtSCDB.SaveChanges();
                        Tbl_User_SysUnites UsrSysU = new Tbl_User_SysUnites
                        {
                            SysUnites_ID = Vint_SysUnit,
                            From_Date = dateTimePicker1.Value.Date,
                            To_Date = dateTimePicker2.Value.Date,
                            // *********************- الوحده الحالية والفعاله للمستخدم
                            SysUnite_StatusID = 1,
                            User_ID = u.ID,
                            Emp_ID = int.Parse(txtEmployeeID.Text)

                        };
                        DNtSCDB.Tbl_User_SysUnites.Add(UsrSysU);
                        DNtSCDB.SaveChanges();
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
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();

                        MessageBox.Show("تم الحفظ ");
                        Nametxt.Text = "";
                        Passwordtxt.Text = "";
                        ConfirmPasswordtxt.Text = "";
                        textBox2.Text = "";
                        Cardtxt.Text = "";
                        NameUstxt.Text = "";
                        txtEmployee.Text = "";
                        txtEmployeeID.Text = "";
                        UsersFrm_Load(sender, e);
                        Cmb(sender, e);

                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  مستخدم  .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                    else if (textBox2.Text != "" && Vint_TransfeerUser == 0)
                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 24 && w.ProcdureId == 3);
                        //if (validationSaveUser != null)
                        //{
                        Vint_UserStatus = int.Parse(comboBox3.SelectedValue.ToString());

                        Vstr_PassIncr = Security.Encrypt_MD5(Passwordtxt.Text);
                        Vint_UserID = int.Parse(textBox2.Text);

                        //*********************Tbl_User
                        var result = DNtSCDB.Tbl_User.SingleOrDefault(h => h.ID == Vint_UserID);
                        u.ID = result.ID;
                        result.Name = Nametxt.Text;
                        result.Password = Vstr_PassIncr;
                        result.UserType_ID = int.Parse(comboBox2.SelectedValue.ToString());
                        result.ImagePath = Environment.CurrentDirectory + "\\Images\\" + u.ID + ".jpg";
                        result.UserStatus_ID = Vint_UserStatus;
                        result.Employee_id = int.Parse(txtEmployeeID.Text);

                        DNtSCDB.SaveChanges();
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
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        //**********************Tbl_User_SysUnites
                        var resultSysUser = DNtSCDB.Tbl_User_SysUnites.SingleOrDefault(h => h.User_ID == Vint_UserID && h.SysUnite_StatusID == 1);

                        resultSysUser.User_ID = u.ID;
                        resultSysUser.Emp_ID = int.Parse(txtEmployeeID.Text);
                        resultSysUser.SysUnite_StatusID = 1;
                        DNtSCDB.SaveChanges();



                        MessageBox.Show("تم التعديل ");
                        Nametxt.Text = "";
                        Passwordtxt.Text = "";
                        ConfirmPasswordtxt.Text = "";
                        textBox2.Text = "";
                        Cardtxt.Text = "";
                        NameUstxt.Text = "";
                        txtEmployee.Text = "";
                        txtEmployeeID.Text = "";
                        UsersFrm_Load(sender, e);
                        Cmb(sender, e);

                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية تعديل بيانات مستخدم  .. برجاء مراجعة مدير المنظومه");
                        //}
                    }

                    if (Image_Path != "")
                    {

                        //Image_Path = Environment.CurrentDirectory + "\\Images\\" + u.ID + ".jpg";
                        string NewPath = Environment.CurrentDirectory + "\\Images\\" + u.ID + ".jpg";

                        //File.Copy(Image_Path, NewPath, true);
                        NewPath = Image_Path;
                        //dataGridView1.DataSource = DNtSCDB.Tbl_User.ToList();
                        Nametxt.Text = "";
                        Passwordtxt.Text = "";

                        ConfirmPasswordtxt.Text = "";
                        //PicturePath.Image = DentalClinicAPP.Properties.Resources.download__2_;



                    }

                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void UsersFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'securityDs.Tbl_Employee' table. You can move, or remove it, as needed.
            //this.tbl_EmployeeTableAdapter.Fill(this.securityDs.Tbl_Employee);
            if (Program.GlbV_Language == "en")
            {
                LangTxtBox.Text = "en";
                //simpleButton3_Click(sender, e);

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                LangTxtBox.Text = "ar-EG";
                //simpleButton4_Click(sender, e);
            }
            tblEmployeeBindingSource.DataSource = DNtSCDB.Tbl_Employee.ToList();
            AutoCompleteStringCollection Emp = new AutoCompleteStringCollection();
            foreach (Tbl_Employee E in tblEmployeeBindingSource.DataSource as List<Tbl_Employee>)
                Emp.Add(E.Name);

            txtEmployee.AutoCompleteCustomSource = Emp;

            Cmb(sender, e);
            Slct(sender, e);

            DG3(sender, e);
            comboBox1.Select();
            this.ActiveControl = comboBox1;
            comboBox1.Focus();
            Cmb(sender, e);
            DG3(sender, e);

        }

        public void Slct(object sender, EventArgs e)
        {
            var list = (from usr in DNtSCDB.Tbl_User
                        join us in DNtSCDB.Tbl_User_SysUnites on usr.ID equals us.User_ID
                        where (us.SysUnite_StatusID == 1)

                        select new
                        {
                            ID = usr.ID,
                            Name = usr.Name,
                            UserType_ID = usr.UserType_ID,
                            UserName = usr.UserName,
                            Nationalid = usr.NationalId,
                            UserType = usr.Tbl_UserType.Name,
                            UserImage = usr.ImagePath,
                            UserFDate = us.From_Date,
                            UserTDate = us.To_Date,
                            UserStatus_ID = usr.UserStatus_ID,
                            SysUnites = us.SysUnites_ID,
                            EmployeeID = usr.Employee_id
                        }).Take(30).OrderBy(x => x.UserType_ID).ToList();
            dataGridView3.DataSource = list;
        }
        public void Cmb(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en")
            {
                var listUnites = DNtMDB.Tbl_Clinics.ToList();
                comboBox1.DataSource = listUnites;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;


                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = -1;
                }


                comboBox1.Text = "Choose Unite";

                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.DataSource = DNtSCDB.Tbl_UserType.ToList();
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "ID";
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = -1;
                }

                comboBox2.Text = "Choose User Type";

                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.DataSource = DNtSCDB.Tbl_UserStatus.ToList();
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "ID";

                if (comboBox3.Items.Count > 0)
                {
                    comboBox3.SelectedIndex = -1;
                }
                comboBox3.Text = "Choose User Status";
            }
            else if (LangTxtBox.Text == "ar-EG")
            {
                var listUnites = DNtMDB.Tbl_Clinics.ToList();
                comboBox1.DataSource = listUnites;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;


                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = -1;
                }


                comboBox1.Text = "اختر الوحده";

                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox2.DataSource = DNtSCDB.Tbl_UserType.ToList();
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "ID";
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = -1;
                }

                comboBox2.Text = "اختر نوع المستخدم";

                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox3.DataSource = DNtSCDB.Tbl_UserStatus.ToList();
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "ID";

                if (comboBox3.Items.Count > 0)
                {
                    comboBox3.SelectedIndex = -1;
                }
                comboBox3.Text = "اختر حالة المستخدم";
            }
        }
        public void DG3(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en")
            {
                dataGridView3.Columns["ID"].Visible = false;
                dataGridView3.Columns["UserName"].Visible = false;
                dataGridView3.Columns["UserName"].HeaderText = "User Name";
                dataGridView3.Columns["UserName"].Width = 200;
                dataGridView3.Columns["Name"].Visible = true;
                dataGridView3.Columns["Name"].HeaderText = "Name";
                dataGridView3.Columns["Name"].Width = 200;

                dataGridView3.Columns["UserType"].HeaderText = "User Type";
                dataGridView3.Columns["UserType"].Width = 100;

                dataGridView3.Columns["SysUnites"].HeaderText = "Clinic";
                dataGridView3.Columns["SysUnites"].Width = 100;

                //dataGridView3.Columns["UserSysunitID"].Visible = false;

                dataGridView3.Columns["UserType_ID"].Visible = false;
                dataGridView3.Columns["UserImage"].Visible = false;

                dataGridView3.Columns["UserFDate"].Visible = false;

                dataGridView3.Columns["UserTDate"].Visible = false;
                dataGridView3.Columns["UserStatus_ID"].Visible = false;
                dateTimePicker1.Value = DateTime.Today.AddDays(-1);
                dateTimePicker2.Value = DateTime.Today.AddYears(10);

                comboBox1.Select();
                this.ActiveControl = comboBox1;
                comboBox1.Focus();
            }
            else if (LangTxtBox.Text == "ar-EG")
            {
                dataGridView3.Columns["ID"].Visible = false;
                dataGridView3.Columns["UserName"].Visible = false;
                dataGridView3.Columns["UserName"].HeaderText = "User Name";
                dataGridView3.Columns["UserName"].Width = 200;
                dataGridView3.Columns["Name"].Visible = true;
                dataGridView3.Columns["Name"].HeaderText = "اسم المستخدم";
                dataGridView3.Columns["Name"].Width = 200;

                dataGridView3.Columns["NationalId"].HeaderText = "الرقم القومي";
                dataGridView3.Columns["NationalId"].Width = 200;

                dataGridView3.Columns["UserType"].HeaderText = "نوع المستخدم";
                dataGridView3.Columns["UserType"].Width = 100;

                dataGridView3.Columns["SysUnites"].HeaderText = "العيادة";
                dataGridView3.Columns["SysUnites"].Width = 100;

                //dataGridView3.Columns["UserSysunitID"].Visible = false;

                dataGridView3.Columns["UserType_ID"].Visible = false;
                dataGridView3.Columns["UserImage"].Visible = false;

                dataGridView3.Columns["UserFDate"].Visible = false;

                dataGridView3.Columns["UserTDate"].Visible = false;
                dataGridView3.Columns["UserStatus_ID"].Visible = false;
                dateTimePicker1.Value = DateTime.Today.AddDays(-1);
                dateTimePicker2.Value = DateTime.Today.AddYears(10);
                comboBox1.Select();
                this.ActiveControl = comboBox1;
                comboBox1.Focus();
            }
        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {
            var listSearchUser = (from usr in DNtSCDB.Tbl_User
                                  join us in DNtSCDB.Tbl_User_SysUnites on usr.ID equals us.User_ID
                                  where (us.SysUnite_StatusID == 1 && usr.Name.Contains(Nametxt.Text))

                                  select new
                                  {
                                      ID = usr.ID,
                                      Name = usr.Name,
                                      UserType_ID = usr.UserType_ID,
                                      UserName = usr.UserName,
                                      nationalid = usr.NationalId,
                                      UserType = usr.Tbl_UserType.Name,
                                      UserImage = usr.ImagePath,
                                      UserFDate = us.From_Date,
                                      UserTDate = us.To_Date,
                                      UserStatus_ID = usr.UserStatus_ID,
                                      SysUnites = us.SysUnites_ID,
                                      EmployeeID = usr.Employee_id
                                  }).Take(30).OrderBy(x => x.UserType_ID).ToList();

            dataGridView3.DataSource = listSearchUser;



            DG3(sender, e);
            Nametxt.Select();
            this.ActiveControl = Nametxt;
            Nametxt.Focus();

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
                    Passwordtxt.Focus();
                }
            }
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
                txtEmployee.Focus();
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

        private void button2_Click(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            //LangTxtBox.Text = "";

            //this.RightToLeft = RightToLeft.Yes;
            //RightToLeftLayout = true;
            //this.Controls.Clear();
            //InitializeComponent();

            //LangTxtBox.Text = "ar-EG";
            //UsersFrm_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            //LangTxtBox.Text = "";

            //this.RightToLeft = RightToLeft.No;
            //RightToLeftLayout = false;
            //this.Controls.Clear();
            //InitializeComponent();
            //LangTxtBox.Text = "ar-EG";
            //UsersFrm_Load(sender, e);
        }

        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
            if (Program.GlbV_UserTypeId == 1)
            {
                txtEmployee.Text = "";
                txtEmployeeID.Text = "";
                DialogResult resultmsg = MessageBox.Show("  هل تم نقل هدا الموظف ؟ ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultmsg == DialogResult.Yes)
                {
                    Vint_TransfeerUser = 1;
                    Vint_UserID = int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString());
                    Nametxt.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                    comboBox2.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString());
                    NameUstxt.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                    Cardtxt.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
                    PicturePath.ImageLocation = dataGridView3.CurrentRow.Cells[6].Value.ToString();
                    dateTimePicker1.Value = DateTime.Today.AddDays(0);
                    dateTimePicker2.Value = DateTime.Today.AddYears(10);
                    Vint_UserStatus = int.Parse(dataGridView3.CurrentRow.Cells[9].Value.ToString());
                    comboBox3.SelectedValue = Vint_UserStatus;
                    comboBox3.SelectedItem = "";
                    comboBox1.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[10].Value.ToString());
                    txtEmployeeID.Text = dataGridView3.CurrentRow.Cells[11].Value.ToString();
                    int Vint_EmpID = int.Parse(txtEmployeeID.Text);
                    var listUser = DNtSCDB.Tbl_User.Where(x => x.Employee_id == Vint_EmpID).ToList();
                    if (listUser.Count > 0)
                    {
                        txtEmployee.Text = DNtSCDB.Tbl_Employee.FirstOrDefault(x => x.ID == Vint_EmpID).Name.ToString();
                    }
                    textBox2.Text = Vint_UserID.ToString();

                    Passwordtxt.Select();
                    this.ActiveControl = Passwordtxt;
                    Passwordtxt.Focus();
                }
                else
                {
                    Vint_TransfeerUser = 0;
                    Vint_UserID = int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString());
                    Nametxt.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                    comboBox2.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString());
                    NameUstxt.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                    Cardtxt.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
                    PicturePath.ImageLocation = dataGridView3.CurrentRow.Cells[6].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView3.CurrentRow.Cells[7].Value.ToString());
                    dateTimePicker2.Value = DateTime.Parse(dataGridView3.CurrentRow.Cells[8].Value.ToString());
                    Vint_UserStatus = int.Parse(dataGridView3.CurrentRow.Cells[9].Value.ToString());
                    comboBox3.SelectedValue = Vint_UserStatus;
                    comboBox1.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[10].Value.ToString());
                    txtEmployeeID.Text = dataGridView3.CurrentRow.Cells[11].Value.ToString();
                    int Vint_EmpID = int.Parse(txtEmployeeID.Text);
                    var listUser = DNtSCDB.Tbl_User.Where(x => x.Employee_id == Vint_EmpID).ToList();
                    if (listUser.Count > 0)
                    {
                        txtEmployee.Text = DNtSCDB.Tbl_Employee.FirstOrDefault(x => x.ID == Vint_EmpID).Name.ToString();
                    }

                    textBox2.Text = Vint_UserID.ToString();
                    Passwordtxt.Select();
                    this.ActiveControl = Passwordtxt;
                    Passwordtxt.Focus();
                }
            }
            else if (Program.GlbV_UserTypeId != 1)
            {
                Vint_TransfeerUser = 0;
                Vint_UserID = int.Parse(dataGridView3.CurrentRow.Cells[0].Value.ToString());
                Nametxt.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                comboBox2.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[2].Value.ToString());
                NameUstxt.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                Cardtxt.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
                PicturePath.ImageLocation = dataGridView3.CurrentRow.Cells[6].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView3.CurrentRow.Cells[7].Value.ToString());
                dateTimePicker2.Value = DateTime.Parse(dataGridView3.CurrentRow.Cells[8].Value.ToString());
                Vint_UserStatus = int.Parse(dataGridView3.CurrentRow.Cells[9].Value.ToString());
                comboBox3.SelectedValue = Vint_UserStatus;
                comboBox3.SelectedItem = "";
                comboBox1.SelectedValue = int.Parse(dataGridView3.CurrentRow.Cells[10].Value.ToString());
                txtEmployeeID.Text = dataGridView3.CurrentRow.Cells[11].Value.ToString();
                int Vint_EmpID = int.Parse(txtEmployeeID.Text);
                var listUser = DNtSCDB.Tbl_User.Where(x => x.Employee_id == Vint_EmpID).ToList();
                if (listUser.Count > 0)
                {
                    txtEmployee.Text = DNtSCDB.Tbl_Employee.FirstOrDefault(x => x.ID == Vint_EmpID).Name.ToString();
                }

                Passwordtxt.Select();
                this.ActiveControl = Passwordtxt;
                Passwordtxt.Focus();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();

            LangTxtBox.Text = "en";
            this.RightToLeft = RightToLeft.No;
            this.RightToLeftLayout = false;
            Cmb(sender, e);
            Slct(sender, e);
            DG3(sender, e);

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            LangTxtBox.Text = "ar-EG";
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            Cmb(sender, e);
            Slct(sender, e);
            DG3(sender, e);

        }

        private void txtEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cardtxt.Text = DNtSCDB.Tbl_Employee.FirstOrDefault(x => x.Name == txtEmployee.Text).NationalId.ToString();
                txtEmployeeID.Text = DNtSCDB.Tbl_Employee.FirstOrDefault(x => x.Name == txtEmployee.Text).ID.ToString();
                int Vint_EmpID = int.Parse(txtEmployeeID.Text);
                var listUser = DNtSCDB.Tbl_User.Where(x => x.Employee_id == Vint_EmpID).ToList();
                if (listUser.Count > 0)
                {
                    MessageBox.Show("This Employee has Created User!");
                    txtEmployee.Text = "";
                    txtEmployeeID.Text = "";
                    txtEmployee.Focus();

                    ;
                }
                simpleButton1.Focus();
            }
        }

        private void txtEmployee_Leave(object sender, EventArgs e)
        {
            if (txtEmployee.Text != string.Empty)
            {
                Cardtxt.Text = DNtSCDB.Tbl_Employee.FirstOrDefault(x => x.Name == txtEmployee.Text).NationalId.ToString();
                txtEmployeeID.Text = DNtSCDB.Tbl_Employee.FirstOrDefault(x => x.Name == txtEmployee.Text).ID.ToString();
                int Vint_EmpID = int.Parse(txtEmployeeID.Text);

                var listUser = DNtSCDB.Tbl_User.Where(x => x.Employee_id == Vint_EmpID).ToList();
                if (listUser.Count > 0)
                {
                    MessageBox.Show("This Employee has Created User!");
                    txtEmployee.Text = "";
                    txtEmployeeID.Text = "";
                    txtEmployee.Focus();

                    ;
                }
            }
        }
    }
}

