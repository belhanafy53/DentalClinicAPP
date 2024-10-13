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
    public partial class EmployeeFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 DntDB = new Model1();
        ModelSecurity FsDb = new ModelSecurity();
        ModelEvents FsEvDb = new ModelEvents();
        int xID;
        public EmployeeFrm()
        {
            InitializeComponent();
            
            //Phonetxt.Text = "";

            Nametxt.Select();
            this.ActiveControl = Nametxt;

            Nametxt.Focus();
        }

        private void EmployeeFrm_Load(object sender, EventArgs e)
        {
            ClearData();
            // TODO: This line of code loads data into the 'managements.Tbl_Management' table. You can move, or remove it, as needed.
            this.tbl_ManagementTableAdapter.Fill(this.managements.Tbl_Management);
            //var listManagement = FsDb.Tbl_Management.ToList();
            tblManagementBindingSource.DataSource = DntDB.Tbl_Management.ToList();
            AutoCompleteStringCollection MNG = new AutoCompleteStringCollection();
            foreach (Tbl_Management d in tblManagementBindingSource.DataSource as List<Tbl_Management>)
                MNG.Add(d.Name);
            txtManagement.AutoCompleteCustomSource = MNG;



            if (Program.GlbV_Language == "en")
            {
                simpleButton3_Click(sender, e);
                GrdEmployee.DataSource = FsDb.Tbl_Employee.Take(20).OrderBy(x => x.Name).ToList();
                //GrdEmployee.Columns["Management_ID"].Visible = false;
                GrdEmployee.Columns["ID"].Visible = false;



                GrdEmployee.Columns["Name"].HeaderText = "Employee";
                GrdEmployee.Columns["NationalId"].HeaderText = "National ID";

                GrdEmployee.Columns["GenderID"].HeaderText = "Gender";
                GrdEmployee.Columns["workerJob"].HeaderText = "Job";

                GrdEmployee.Columns["Status"].HeaderText = "Status";
                GrdEmployee.Columns["StatusDate"].HeaderText = "Status Date";

                GrdEmployee.Columns["StatusDetail"].HeaderText = "Status Detail";
                GrdEmployee.Columns["Management"].HeaderText = "Management";
                //if (GrdEmployee.Columns["GenderID"].Selected.ToString() == "1")
                //{

                //}
                GrdEmployee.Columns["GenderID"].Visible = false;
                //GrdEmployee.Columns["Tbl_Beneficiary"].Visible = false;
                //GrdEmployee.Columns["Tbl_OrderHandler"].Visible = false;
                GrdEmployee.Columns["Tbl_User"].Visible = false;
                GrdEmployee.Columns["Tbl_User_SysUnites"].Visible = false;
                GrdEmployee.Columns["Name"].Width = 300;
                GrdEmployee.Columns["NationalId"].Width = 150;
            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                simpleButton6_Click(sender, e);
                GrdEmployee.DataSource = FsDb.Tbl_Employee.Take(20).OrderBy(x => x.Name).ToList();
                //GrdEmployee.Columns["Management_ID"].Visible = false;
                GrdEmployee.Columns["ID"].Visible = false;



                GrdEmployee.Columns["Name"].HeaderText = "الموظف";
                GrdEmployee.Columns["NationalId"].HeaderText = "الرقم القومي";

                GrdEmployee.Columns["GenderID"].HeaderText = "النوع";
                GrdEmployee.Columns["workerJob"].HeaderText = "الوظيفة";

                GrdEmployee.Columns["Status"].HeaderText = "الحاله";
                GrdEmployee.Columns["StatusDate"].HeaderText = "تاريخ الحاله";

                GrdEmployee.Columns["StatusDetail"].HeaderText = "تفاصيل الحاله";
                GrdEmployee.Columns["Management"].HeaderText = "الاداره";
                //if (GrdEmployee.Columns["GenderID"].Selected.ToString() == "1")
                //{

                //}
                GrdEmployee.Columns["GenderID"].Visible = false;
                //GrdEmployee.Columns["Tbl_Beneficiary"].Visible = false;
                //GrdEmployee.Columns["Tbl_OrderHandler"].Visible = false;
                GrdEmployee.Columns["Tbl_User"].Visible = false;
                GrdEmployee.Columns["Tbl_User_SysUnites"].Visible = false;
                GrdEmployee.Columns["Name"].Width = 300;
                GrdEmployee.Columns["NationalId"].Width = 150;
            }

          
            //GrdEmployee.Columns["Address"].Width = 200;
            Nametxt.Select();
            this.ActiveControl = Nametxt;
            Nametxt.Focus();
        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {
            if (Nametxt.Text == "")
            {

                ClearData();


            }
            GrdEmployee.DataSource = FsDb.Tbl_Employee.Where(x => x.Name.Contains(Nametxt.Text)).ToList();
        }

        private void ClearData()
        {
            Nametxt.Text = "";
            Nationaltxt.Text = "";
            Emailtxt.Text = "";
            jobtxtbox.Text = "";
            jobtxtbox.Enabled = true;
            txtManagement.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBoxStatus.SelectedIndex = -1;
            comboBoxStatus.Text = "اختر الحاله";
            comboBoxStatusDetails.SelectedIndex = -1;
            comboBoxStatusDetails.Text = "اختر الحاله";
            Nametxt.Select();
            this.ActiveControl = Nametxt;

            Nametxt.Focus();

        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                if (GrdEmployee.RowCount != 0)
                {
                    GrdEmployee.Focus();
                }
                else
                {
                    jobtxtbox.Focus();
                }

            }
        }

        private void GrdEmployee_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxStatus.Text = "";
            comboBoxStatusDetails.Text = "";
            xID = int.Parse(GrdEmployee.CurrentRow.Cells[0].Value.ToString());
            var result = FsDb.Tbl_Employee.SingleOrDefault(x => x.ID == xID);
            Nametxt.Text = result.Name.ToString();
           
            Nationaltxt.Text = result.NationalId.ToString();
            if (result.Status != null)
            {
                comboBoxStatus.SelectedText = result.Status.Trim();
            }
            if (result.StatusDetail != null)
            {
                comboBoxStatusDetails.SelectedText = result.StatusDetail.Trim();
            }

            if (result.StatusDate != null)
            {
                dateTimePickerStatus.Value = Convert.ToDateTime(result.StatusDate);
            }
            if (result.WorkerJob != null)
            {
                jobtxtbox.Text = result.WorkerJob.ToString();
            }
            if (result.GenderID == 1)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            if (result.Management != null)
            {
                txtManagement.Text = result.Management.Trim();
            }

            Nationaltxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (Program.GlbV_Language == "en")
            {
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("Please Enter Employee Name ");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }

                else
                {
                    int? GenderV = 0;
                    if (radioButton1.Checked == true)
                    {
                        GenderV = 1;
                    }
                    else if (radioButton2.Checked == true)
                    {
                        GenderV = 2;
                    }
                    if (xID == 0)

                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 3 && w.ProcdureId == 1);
                        //if (validationSaveUser != null)
                        //{
                        string vStt_stDetails = "";
                        if (comboBoxStatusDetails.SelectedItem != null)
                        {
                            vStt_stDetails = comboBoxStatusDetails.SelectedItem.ToString();
                        }
                        Tbl_Employee emp = new Tbl_Employee
                        {
                            Name = Nametxt.Text,
                            NationalId = Nationaltxt.Text,
                            GenderID = GenderV,
                            Management = txtManagement.Text,
                            Status = comboBoxStatus.SelectedItem.ToString(),
                            StatusDetail = vStt_stDetails,
                            StatusDate = dateTimePickerStatus.Value

                        };
                        int VintEmpId = emp.ID;
                        //var listbenf = FsDb.Tbl_Beneficiary.FirstOrDefault(x => x.ID_Supp == VintEmpId);
                        //if (listbenf == null)
                        //{
                        //    Tbl_Beneficiary bnf = new Tbl_Beneficiary
                        //    {
                        //        ID_Emp = VintEmpId,
                        //        Name = Nametxt.Text,
                        //        BeneficiaryKind_ID = 1

                        //    };
                        //    FsDb.Tbl_Beneficiary.Add(bnf);
                        FsDb.Tbl_Employee.Add(emp);
                        FsDb.SaveChanges();

                        //---------------خفظ ااحداث 
                        int Vint_LastRow = emp.ID;
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Employee Add",
                            TableName = "Tbl_Employee",
                            TableRecordId = Vint_LastRow.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "EmployeeFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //***************************
                        MessageBox.Show("Saved");

                        ClearData();
                        GrdEmployee.DataSource = FsDb.Tbl_Employee.ToList();
                        //}
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  موظف .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                    else
                    {
                        //var validationSaveUser1 = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 3 && w.ProcdureId == 3);
                        //if (validationSaveUser1 != null)
                        //{
                        int? GenderVU = 0;
                        if (radioButton1.Checked == true)
                        {
                            GenderVU = 1;
                        }
                        else if (radioButton2.Checked == true)
                        {
                            GenderVU = 2;
                        }
                        var result = FsDb.Tbl_Employee.SingleOrDefault(h => h.ID == xID);
                        result.Name = Nametxt.Text;
                        result.NationalId = Nationaltxt.Text;
                        result.Management = txtManagement.Text;
                        result.GenderID = GenderVU;

                        if (comboBoxStatus.SelectedItem != null)
                        {
                            result.Status = comboBoxStatus.SelectedItem.ToString();
                        }
                        if (comboBoxStatusDetails.SelectedItem != null)
                        {
                            result.StatusDetail = comboBoxStatusDetails.SelectedItem.ToString();
                        }
                        result.StatusDate = Convert.ToDateTime(dateTimePickerStatus.Value);
                        int VintEmpId = result.ID;
                        //var listbenf = FsDb.Tbl_Beneficiary.FirstOrDefault(x => x.ID_Emp == VintEmpId);
                        //if (listbenf != null)
                        //{
                        //    listbenf.Name = Nametxt.Text;
                        //}
                        //else
                        //{
                        //    Tbl_Beneficiary bnf = new Tbl_Beneficiary
                        //    {
                        //        ID_Emp = VintEmpId,
                        //        Name = Nametxt.Text,
                        //        BeneficiaryKind_ID = 1

                        //    };
                        //    FsDb.Tbl_Beneficiary.Add(bnf);
                        //}
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 

                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Employee Update",
                            TableName = "Tbl_Employee",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "EmployeeFrm"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //***************************
                        MessageBox.Show("Updated");

                        ClearData();
                        GrdEmployee.DataSource = FsDb.Tbl_Employee.ToList();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية تعديل  موظف .. برجاء مراجعة مدير المنظومه");
                        //}
                    }

                }

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل اسم الموظف  ");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }

                else
                {
                    int? GenderV = 0;
                    if (radioButton1.Checked == true)
                    {
                        GenderV = 1;
                    }
                    else if (radioButton2.Checked == true)
                    {
                        GenderV = 2;
                    }
                    if (xID == 0)

                    {
                        //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 3 && w.ProcdureId == 1);
                        //if (validationSaveUser != null)
                        //{
                        string vStt_stDetails = "";
                        if (comboBoxStatusDetails.SelectedItem != null)
                        {
                            vStt_stDetails = comboBoxStatusDetails.SelectedItem.ToString();
                        }
                        Tbl_Employee emp = new Tbl_Employee
                        {
                            Name = Nametxt.Text,
                            NationalId = Nationaltxt.Text,
                            GenderID = GenderV,
                            Management = txtManagement.Text,
                            Status = comboBoxStatus.SelectedItem.ToString(),
                            StatusDetail = vStt_stDetails,
                            StatusDate = dateTimePickerStatus.Value

                        };
                        int VintEmpId = emp.ID;
                        //var listbenf = FsDb.Tbl_Beneficiary.FirstOrDefault(x => x.ID_Supp == VintEmpId);
                        //if (listbenf == null)
                        //{
                        //    Tbl_Beneficiary bnf = new Tbl_Beneficiary
                        //    {
                        //        ID_Emp = VintEmpId,
                        //        Name = Nametxt.Text,
                        //        BeneficiaryKind_ID = 1

                        //    };
                        //    FsDb.Tbl_Beneficiary.Add(bnf);
                        FsDb.Tbl_Employee.Add(emp);
                        FsDb.SaveChanges();

                        //---------------خفظ ااحداث 
                        int Vint_LastRow = emp.ID;
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "اضافة موظف",
                            TableName = "Tbl_Employee",
                            TableRecordId = Vint_LastRow.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "شاشة الموظفين"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //***************************
                        MessageBox.Show("تم الحفظ");

                        ClearData();
                        GrdEmployee.DataSource = FsDb.Tbl_Employee.ToList();
                        //}
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  موظف .. برجاء مراجعة مدير المنظومه");
                        //}
                    }
                    else
                    {
                        //var validationSaveUser1 = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 3 && w.ProcdureId == 3);
                        //if (validationSaveUser1 != null)
                        //{
                        int? GenderVU = 0;
                        if (radioButton1.Checked == true)
                        {
                            GenderVU = 1;
                        }
                        else if (radioButton2.Checked == true)
                        {
                            GenderVU = 2;
                        }
                        var result = FsDb.Tbl_Employee.SingleOrDefault(h => h.ID == xID);
                        result.Name = Nametxt.Text;
                        result.NationalId = Nationaltxt.Text;
                        result.Management = txtManagement.Text;
                        result.GenderID = GenderVU;

                        if (comboBoxStatus.SelectedItem != null)
                        {
                            result.Status = comboBoxStatus.SelectedItem.ToString();
                        }
                        if (comboBoxStatusDetails.SelectedItem != null)
                        {
                            result.StatusDetail = comboBoxStatusDetails.SelectedItem.ToString();
                        }
                        result.StatusDate = Convert.ToDateTime(dateTimePickerStatus.Value);
                        int VintEmpId = result.ID;
                        //var listbenf = FsDb.Tbl_Beneficiary.FirstOrDefault(x => x.ID_Emp == VintEmpId);
                        //if (listbenf != null)
                        //{
                        //    listbenf.Name = Nametxt.Text;
                        //}
                        //else
                        //{
                        //    Tbl_Beneficiary bnf = new Tbl_Beneficiary
                        //    {
                        //        ID_Emp = VintEmpId,
                        //        Name = Nametxt.Text,
                        //        BeneficiaryKind_ID = 1

                        //    };
                        //    FsDb.Tbl_Beneficiary.Add(bnf);
                        //}
                        FsDb.SaveChanges();
                        //---------------خفظ ااحداث 

                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "تعديل موظف",
                            TableName = "Tbl_Employee",
                            TableRecordId = result.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "شاشة الموظفين"


                        };
                        FsEvDb.SecurityEvents.Add(sev);
                        FsEvDb.SaveChanges();
                        //***************************
                        MessageBox.Show("تم التعديل");

                        ClearData();
                        GrdEmployee.DataSource = FsDb.Tbl_Employee.ToList();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية تعديل  موظف .. برجاء مراجعة مدير المنظومه");
                        //}
                    }

                }
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (Program.GlbV_Language == "en")
            {
                try
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 3 && w.ProcdureId == 4);
                    //if (validationSaveUser != null)
                    //{
                    if (xID > 0)
                    {
                        var result1 = MessageBox.Show("Are you Sure , want to delete this employee  ?", "Employee Delete ", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {
                            var result = FsDb.Tbl_Employee.Find(xID);
                            FsDb.Tbl_Employee.Remove(result);
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 

                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "Employee Delete",
                                TableName = "Tbl_Employee",
                                TableRecordId = result.ID.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "EmployeeFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //***************************
                            MessageBox.Show("Deleted");
                            ClearData();



                            GrdEmployee.DataSource = FsDb.Tbl_Employee.ToList();
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("من فضلك حدد الموظف المراد حدفه");
                        Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ليس لديك صلاحية حذف  موظف .. برجاء مراجعة مدير المنظومه");
                    //}
                }
                catch


                {
                    MessageBox.Show("هذا الموظف لايمكن حذفه لوجود مستندات له", "المنظومة المالية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                try
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 3 && w.ProcdureId == 4);
                    //if (validationSaveUser != null)
                    //{
                    if (xID > 0)
                    {
                        var result1 = MessageBox.Show("هل تريد حدف هدا الموظف  ؟", "حدف موظف ", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {
                            var result = FsDb.Tbl_Employee.Find(xID);
                            FsDb.Tbl_Employee.Remove(result);
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 

                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "حذف موظف",
                                TableName = "Tbl_Employee",
                                TableRecordId = result.ID.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "شاشة الموظفين"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //***************************
                            MessageBox.Show("  تم الحدف");
                            ClearData();



                            GrdEmployee.DataSource = FsDb.Tbl_Employee.ToList();
                            Nametxt.Select();
                            this.ActiveControl = Nametxt;
                            Nametxt.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("من فضلك حدد الموظف المراد حدفه");
                        Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ليس لديك صلاحية حذف  موظف .. برجاء مراجعة مدير المنظومه");
                    //}
                }
                catch


                {
                    MessageBox.Show("هذا الموظف لايمكن حذفه لوجود مستندات له", "المنظومة المالية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
               
        }

        private void GrdEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBoxStatus.Text = "";
                comboBoxStatusDetails.Text = "";
                xID = int.Parse(GrdEmployee.CurrentRow.Cells[0].Value.ToString());
                var result = FsDb.Tbl_Employee.SingleOrDefault(x => x.ID == xID);
                Nametxt.Text = result.Name.ToString();

                Nationaltxt.Text = result.NationalId.ToString();
                if (result.Status != null)
                {
                    comboBoxStatus.SelectedText = result.Status.Trim();
                }
                if (result.StatusDetail != null)
                {
                    comboBoxStatusDetails.SelectedText = result.StatusDetail.Trim();
                }

                if (result.StatusDate != null)
                {
                    dateTimePickerStatus.Value = Convert.ToDateTime(result.StatusDate);
                }
                if (result.WorkerJob != null)
                {
                    jobtxtbox.Text = result.WorkerJob.ToString();
                }
                if (result.GenderID == 1)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                if (result.Management != null)
                {
                    txtManagement.Text = result.Management.Trim();
                }

                Nationaltxt.Focus();
            }
        }

        private void comboBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            cbo.PreviewKeyDown -= comboBox1_PreviewKeyDown;
            if (cbo.DroppedDown) cbo.Focus();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            cbo.PreviewKeyDown += new PreviewKeyDownEventHandler(comboBox1_PreviewKeyDown);
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                comboBoxStatus.Focus();

            }
        }

        private void Nationaltxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                radioButton1.Focus();

            }
        }

      

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            GrdEmployee.DataSource = FsDb.Tbl_Employee.OrderBy(x => x.Name).ToList();
            int? VintEmpId;
            if (GrdEmployee.RowCount > 0)
            {
                int WCount = int.Parse(GrdEmployee.RowCount.ToString());

                for (int i = 0; i <= WCount - 1; i++)
                {
                    VintEmpId = string.IsNullOrEmpty(GrdEmployee.Rows[i].Cells[0].Value.ToString()) ? (int?)null : int.Parse(GrdEmployee.Rows[i].Cells[0].Value.ToString());


                    //var listbenf = FsDb.Tbl_Beneficiary.SingleOrDefault(x => x.ID_Supp == VintEmpId);
                    //if (listbenf == null)
                    //{
                    //    Tbl_Beneficiary bnf = new Tbl_Beneficiary
                    //    {
                    //        Name = GrdEmployee.Rows[i].Cells[1].Value.ToString(),
                    //        ID_Emp = int.Parse(GrdEmployee.Rows[i].Cells[0].Value.ToString()),
                    //        BeneficiaryKind_ID = 1

                    //    };
                    //    FsDb.Tbl_Beneficiary.Add(bnf);
                    //    FsDb.SaveChanges();
                    //}

                }
                MessageBox.Show("تم النقل");
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            GrdEmployee.DataSource = FsDb.Tbl_Employee.OrderBy(x => x.Name).ToList();
            int? VintEmpId;
            if (GrdEmployee.RowCount > 0)
            {
                int WCount = int.Parse(GrdEmployee.RowCount.ToString());

                for (int i = 0; i <= WCount - 1; i++)
                {
                    VintEmpId = string.IsNullOrEmpty(GrdEmployee.Rows[i].Cells[0].Value.ToString()) ? (int?)null : int.Parse(GrdEmployee.Rows[i].Cells[0].Value.ToString());


                    //var listbenf = FsDb.Tbl_Handler.SingleOrDefault(x => x.Employee_ID == VintEmpId);
                    //if (listbenf == null)
                    //{
                    //    Tbl_Handler bnf = new Tbl_Handler
                    //    {
                    //        Name = GrdEmployee.Rows[i].Cells[1].Value.ToString(),
                    //        Employee_ID = int.Parse(GrdEmployee.Rows[i].Cells[0].Value.ToString()),


                    //    };
                    //    FsDb.Tbl_Handler.Add(bnf);
                    //    FsDb.SaveChanges();
                    //}

                }
                MessageBox.Show("تم النقل");
            }
        }

        private void jobtxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                Nationaltxt.Focus();

            }
        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                radioButton2.Focus();

            }
        }

        private void radioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                comboBoxStatus.Focus();

            }
        }

        private void comboBoxStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                comboBoxStatusDetails.Focus();

            }
            
        }

        private void comboBoxStatusDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                dateTimePickerStatus.Focus();

            }

        }

        private void dateTimePickerStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                txtManagement.Focus();

            }
        }

        private void txtManagement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                simpleButton1.Focus();

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
        }
    }
}
