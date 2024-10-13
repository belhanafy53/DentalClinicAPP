﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;
 

namespace DentalClinicAPP.Forms.SecuritySystem
{
    public partial class ProceduresFormsFrm : DevExpress.XtraEditors.XtraForm
    {
        int xID;
        ModelSecurity FsDb = new ModelSecurity();
        ModelEvents FsEvDb = new ModelEvents();
        //int xcheckb;
        public ProceduresFormsFrm()
        {
            InitializeComponent();

            comboBox1.DataSource = FsDb.Tbl_Forms.ToList();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.SelectedItem = null;

            textBox1.Select();
            this.ActiveControl = textBox1;
            textBox1.Focus();


            // This line of code is generated by Data Source Configuration Wizard
            // Instantiate a new DBContext
            //DentalClinicAPP.DataBase.ModelSecurity.ModelSecurity dbContext = new DentalClinicAPP.DataBase.ModelSecurity.ModelSecurity();
            //// Call the LoadAsync method to asynchronously get the data for the given DbSet from the database.
            //dbContext.Tbl_MenuProgramUnites.LoadAsync().ContinueWith(loadTask =>
            //{
            //    // Bind data to control when loading complete
            //    tbl_MenuProgramUnitesBindingSource.DataSource = dbContext.Tbl_MenuProgramUnites.Local.ToBindingList();
            //}, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());

            //treeList1.ExpandAll();
        }

        private void ProceduresFormsFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'securityDs.Tbl_Procedures' table. You can move, or remove it, as needed.
            this.tbl_ProceduresTableAdapter.Fill(this.securityDs.Tbl_Procedures);
            // TODO: This line of code loads data into the 'securityDs.Tbl_MenuProgramUnites' table. You can move, or remove it, as needed.
            this.tbl_MenuProgramUnitesTableAdapter.Fill(this.securityDs.Tbl_MenuProgramUnites);
            

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = -1;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            treeList1.DataSource = FsDb.Tbl_MenuProgramUnites.Where(x => x.Name.Contains(textBox1.Text)).OrderBy(x => x.ID).ToList();
            treeList1.ExpandAll();

            textBox2.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

            {
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox3.Text = "";
                textBox2.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[0]).ToString();
                textBox4.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[1]).ToString();
                xID = int.Parse(textBox4.Text);
                var list = FsDb.Tbl_MenuProgramUnites.Find(xID);
                if (list.Parent_ID != null)
                {
                    textBox5.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[2]).ToString();
                }
                if (list.Forms_ID != null)
                {
                    comboBox1.SelectedValue = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[3]).ToString());
                    int Vint_Form = int.Parse(comboBox1.SelectedValue.ToString());
                    int vint_formid = int.Parse(comboBox1.SelectedValue.ToString());
                    //var listPrFrm = FsDb.Tbl_ProceduresForms.Where(x => x.Form_ID == vint_formid).ToList();
                    var listPrFrm = FsDb.Tbl_Procedures.ToList();
                    if (listPrFrm.Count != 0)

                    {
                        textBox3.Text = "0";
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        for (int i = 0; i < listPrFrm.Count(); i++)
                        {

                            int Vint_Id = 0;
                            Vint_Id = int.Parse(dataGridView1.Rows[i].Cells[ID.Name].Value.ToString());
                            var listPrfrm = FsDb.Tbl_ProceduresForms.Where(x => x.Procedure_ID == Vint_Id && x.Form_ID == Vint_Form).ToList();

                            if (listPrfrm.Count() > 0)
                            {
                                dataGridView1.Rows[i].Cells[Column1.Name].Value = true;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[Column1.Name].Value = false;
                            }

                        }
                    }
                    else
                    {
                        textBox3.Text = "1";
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                    }
                }
                else
                {

                    comboBox1.SelectedValue = -1;
                    var listPrFrm = FsDb.Tbl_Procedures.ToList();

                    textBox3.Text = "0";
                    dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                    for (int i = 0; i < listPrFrm.Count(); i++)
                    {
                        dataGridView1.Rows[i].Cells[Column1.Name].Value = false;
                    }
                }
                dataGridView1.Select();
                this.ActiveControl = dataGridView1;
                dataGridView1.Focus();
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                treeList1.Focus();
            }
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 34 && w.ProcdureId == 3 || w.ProcdureId == 1);
                //if (validationSaveUser != null)
                //{
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Please Select Program Unit to set Procedure ");
                    textBox1.Focus();
                }
                else

                {


                    int Vint_ProcedureCount = int.Parse(dataGridView1.Rows.Count.ToString());
                    for (int i = 0; i < Vint_ProcedureCount; i++)
                    {
                        bool Vbool_tr = false;
                        int Vint_Id = 0;

                        Vint_Id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        string Vstr_Name = dataGridView1.Rows[i].Cells[1].Value.ToString();

                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[2].Value) == true)
                        {
                            Vbool_tr = true;
                            int Vint_frm = int.Parse(comboBox1.SelectedValue.ToString());
                            var list = FsDb.Tbl_ProceduresForms.SingleOrDefault(x => x.Form_ID == Vint_frm && x.Procedure_ID == Vint_Id);
                            if (textBox3.Text == "1")
                            {
                                Tbl_ProceduresForms prfrm = new Tbl_ProceduresForms
                                {
                                    Form_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                                    Procedure_ID = Vint_Id
                                };
                                FsDb.Tbl_ProceduresForms.Add(prfrm);
                                FsDb.SaveChanges();
                                //---------------خفظ ااحداث 
                                int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                SecurityEvent sev = new SecurityEvent
                                {
                                    ActionName = "Add Procedure Form",
                                    TableName = "Tbl_ProceduresForms",
                                    TableRecordId = Vint_LastRow.ToString(),
                                    Description = "",
                                    ManagementName = Program.GlbV_Management,
                                    ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                    EmployeeName = Program.GlbV_EmpName,
                                    User_ID = Program.GlbV_UserId,
                                    UserName = Program.GlbV_UserName,
                                    FormName = "ProceduresFormsFrm"


                                };
                                FsEvDb.SecurityEvents.Add(sev);
                                FsEvDb.SaveChanges();
                                //************************************
                            }
                            else if (textBox3.Text == "0" && list == null)
                            {
                                Tbl_ProceduresForms prfrm = new Tbl_ProceduresForms
                                {
                                    Form_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                                    Procedure_ID = Vint_Id
                                };
                                FsDb.Tbl_ProceduresForms.Add(prfrm);
                                FsDb.SaveChanges();
                                //---------------خفظ ااحداث 
                                int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                SecurityEvent sev = new SecurityEvent
                                {
                                    ActionName = "Update Procedure Form",
                                    TableName = "Tbl_ProceduresForms",
                                    TableRecordId = Vint_LastRow.ToString(),
                                    Description = "",
                                    ManagementName = Program.GlbV_Management,
                                    ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                    EmployeeName = Program.GlbV_EmpName,
                                    User_ID = Program.GlbV_UserId,
                                    UserName = Program.GlbV_UserName,
                                    FormName = "ProceduresFormsFrm"


                                };
                                FsEvDb.SecurityEvents.Add(sev);
                                FsEvDb.SaveChanges();
                                //************************************
                            }
                            else if (textBox3.Text == "0" && list != null)
                            {
                                list.Procedure_ID = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());

                            }

                        }
                        else
                        {
                            Vbool_tr = false;

                            int Vint_frm = int.Parse(comboBox1.SelectedValue.ToString());
                            var list = FsDb.Tbl_ProceduresForms.FirstOrDefault(x => x.Form_ID == Vint_frm && x.Procedure_ID == Vint_Id);
                            if (list != null)
                            {
                                var listP = FsDb.Tbl_UsersProcedureForm.Where(x => x.ProceduresForms_ID == list.ID).ToList();
                                dataGridView2.DataSource = listP;
                                if (list != null && listP == null)
                                {
                                    FsDb.Tbl_ProceduresForms.Remove(list);
                                    FsDb.SaveChanges();
                                    //---------------خفظ ااحداث 
                                    //int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                    SecurityEvent sev = new SecurityEvent
                                    {
                                        ActionName = "Add Procedure Form",
                                        TableName = "Tbl_UsersProcedureForm",
                                        TableRecordId = list.ID.ToString(),
                                        Description = "",
                                        ManagementName = Program.GlbV_Management,
                                        ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                        EmployeeName = Program.GlbV_EmpName,
                                        User_ID = Program.GlbV_UserId,
                                        UserName = Program.GlbV_UserName,
                                        FormName = "ProceduresFormsFrm"


                                    };
                                    FsEvDb.SecurityEvents.Add(sev);
                                    FsEvDb.SaveChanges();
                                    //************************************
                                }
                                else if (list != null && listP != null)
                                {
                                    int Vint_ProcedureFormCount = int.Parse(dataGridView2.Rows.Count.ToString());
                                    for (int t = 0; t < Vint_ProcedureFormCount; t++)
                                    {
                                        int Vint_ProcedureFormID = int.Parse(dataGridView2.Rows[t].Cells[0].Value.ToString());
                                        var listr = FsDb.Tbl_UsersProcedureForm.FirstOrDefault(x => x.ID == Vint_ProcedureFormID);
                                        FsDb.Tbl_UsersProcedureForm.Remove(listr);
                                        FsDb.SaveChanges();
                                    }
                                    FsDb.Tbl_ProceduresForms.Remove(list);
                                    FsDb.SaveChanges();
                                    //---------------خفظ ااحداث 
                                    //int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                    SecurityEvent sev = new SecurityEvent
                                    {
                                        ActionName = "Add Procedure Form",
                                        TableName = "Tbl_UsersProcedureForm",
                                        TableRecordId = list.ID.ToString(),
                                        Description = "",
                                        ManagementName = Program.GlbV_Management,
                                        ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                        EmployeeName = Program.GlbV_EmpName,
                                        User_ID = Program.GlbV_UserId,
                                        UserName = Program.GlbV_UserName,
                                        FormName = "ProceduresFormsFrm"


                                    };
                                    FsEvDb.SecurityEvents.Add(sev);
                                    FsEvDb.SaveChanges();
                                    //************************************
                                }
                            }

                        }


                    }
                    FsDb.SaveChanges();
                    if (textBox3.Text == "1")
                    {
                        //var validationSaveUserIns = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 34 && w.ProcdureId == 1);
                        //if (validationSaveUserIns != null)
                        //{
                        MessageBox.Show("Saved");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        treeList1.ExpandAll();
                        textBox1.Select();
                        this.ActiveControl = textBox1;
                        textBox1.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  اجراءات الصفحات .. برجاء مراجعة مدير المنظومه");
                        //}

                    }
                    else if (textBox3.Text == "0")
                    {
                        //var validationSaveUserUpd = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 34 && w.ProcdureId == 3);
                        //if (validationSaveUserUpd != null)
                        //{
                        MessageBox.Show("Updated");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        treeList1.ExpandAll();
                        textBox1.Select();
                        this.ActiveControl = textBox1;
                        textBox1.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية تعديل  اجراءات الصفحات .. برجاء مراجعة مدير المنظومه");
                        //}

                    }
                    treeList1.DataSource = FsDb.Tbl_MenuProgramUnites.ToList();
                    dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                    treeList1.ExpandAll();
                    textBox2.Text = "";
                    comboBox1.SelectedValue = -1;
                }
                //}

                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية اضافة او تعديل  اجراءات الصفحات .. برجاء مراجعة مدير المنظومه");
                //}
            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 34 && w.ProcdureId == 3 || w.ProcdureId == 1);
                //if (validationSaveUser != null)
                //{
                if (textBox2.Text == "")
                {
                    MessageBox.Show("من فضلك قم بإختيار وحدة البرنامج  لتحديد الصفحه المراده ");
                    textBox1.Focus();
                }
                else

                {


                    int Vint_ProcedureCount = int.Parse(dataGridView1.Rows.Count.ToString());
                    for (int i = 0; i < Vint_ProcedureCount; i++)
                    {
                        bool Vbool_tr = false;
                        int Vint_Id = 0;

                        Vint_Id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        string Vstr_Name = dataGridView1.Rows[i].Cells[1].Value.ToString();

                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[2].Value) == true)
                        {
                            Vbool_tr = true;
                            int Vint_frm = int.Parse(comboBox1.SelectedValue.ToString());
                            var list = FsDb.Tbl_ProceduresForms.SingleOrDefault(x => x.Form_ID == Vint_frm && x.Procedure_ID == Vint_Id);
                            if (textBox3.Text == "1")
                            {
                                Tbl_ProceduresForms prfrm = new Tbl_ProceduresForms
                                {
                                    Form_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                                    Procedure_ID = Vint_Id
                                };
                                FsDb.Tbl_ProceduresForms.Add(prfrm);
                                FsDb.SaveChanges();
                                //---------------خفظ ااحداث 
                                int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                SecurityEvent sev = new SecurityEvent
                                {
                                    ActionName = "اضافة اجراءات الصفحات",
                                    TableName = "Tbl_ProceduresForms",
                                    TableRecordId = Vint_LastRow.ToString(),
                                    Description = "",
                                    ManagementName = Program.GlbV_Management,
                                    ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                    EmployeeName = Program.GlbV_EmpName,
                                    User_ID = Program.GlbV_UserId,
                                    UserName = Program.GlbV_UserName,
                                    FormName = "ProceduresFormsFrm"


                                };
                                FsEvDb.SecurityEvents.Add(sev);
                                FsEvDb.SaveChanges();
                                //************************************
                            }
                            else if (textBox3.Text == "0" && list == null)
                            {
                                Tbl_ProceduresForms prfrm = new Tbl_ProceduresForms
                                {
                                    Form_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                                    Procedure_ID = Vint_Id
                                };
                                FsDb.Tbl_ProceduresForms.Add(prfrm);
                                FsDb.SaveChanges();
                                //---------------خفظ ااحداث 
                                int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                SecurityEvent sev = new SecurityEvent
                                {
                                    ActionName = "تعديل اجراءات الصفحات",
                                    TableName = "Tbl_ProceduresForms",
                                    TableRecordId = Vint_LastRow.ToString(),
                                    Description = "",
                                    ManagementName = Program.GlbV_Management,
                                    ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                    EmployeeName = Program.GlbV_EmpName,
                                    User_ID = Program.GlbV_UserId,
                                    UserName = Program.GlbV_UserName,
                                    FormName = "ProceduresFormsFrm"


                                };
                                FsEvDb.SecurityEvents.Add(sev);
                                FsEvDb.SaveChanges();
                                //************************************
                            }
                            else if (textBox3.Text == "0" && list != null)
                            {
                                list.Procedure_ID = int.Parse(dataGridView1.Rows[i].Cells[ID.Name].Value.ToString());

                            }

                        }
                        else
                        {
                            Vbool_tr = false;

                            int Vint_frm = int.Parse(comboBox1.SelectedValue.ToString());
                            var list = FsDb.Tbl_ProceduresForms.FirstOrDefault(x => x.Form_ID == Vint_frm && x.Procedure_ID == Vint_Id);
                            if (list != null)
                            {
                                var listP = FsDb.Tbl_UsersProcedureForm.Where(x => x.ProceduresForms_ID == list.ID).ToList();
                                dataGridView2.DataSource = listP;
                                if (list != null && listP == null)
                                {
                                    FsDb.Tbl_ProceduresForms.Remove(list);
                                    FsDb.SaveChanges();
                                    //---------------خفظ ااحداث 
                                    //int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                    SecurityEvent sev = new SecurityEvent
                                    {
                                        ActionName = "اضافة اجراءات الصفحات",
                                        TableName = "Tbl_UsersProcedureForm",
                                        TableRecordId = list.ID.ToString(),
                                        Description = "",
                                        ManagementName = Program.GlbV_Management,
                                        ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                        EmployeeName = Program.GlbV_EmpName,
                                        User_ID = Program.GlbV_UserId,
                                        UserName = Program.GlbV_UserName,
                                        FormName = "ProceduresFormsFrm"


                                    };
                                    FsEvDb.SecurityEvents.Add(sev);
                                    FsEvDb.SaveChanges();
                                    //************************************
                                }
                                else if (list != null && listP != null)
                                {
                                    int Vint_ProcedureFormCount = int.Parse(dataGridView2.Rows.Count.ToString());
                                    for (int t = 0; t < Vint_ProcedureFormCount; t++)
                                    {
                                        int Vint_ProcedureFormID = int.Parse(dataGridView2.Rows[t].Cells[0].Value.ToString());
                                        var listr = FsDb.Tbl_UsersProcedureForm.FirstOrDefault(x => x.ID == Vint_ProcedureFormID);
                                        FsDb.Tbl_UsersProcedureForm.Remove(listr);
                                        FsDb.SaveChanges();
                                    }
                                    FsDb.Tbl_ProceduresForms.Remove(list);
                                    FsDb.SaveChanges();
                                    //---------------خفظ ااحداث 
                                    //int Vint_LastRow = int.Parse(prfrm.ID.ToString());
                                    SecurityEvent sev = new SecurityEvent
                                    {
                                        ActionName = "اضافة اجراءات الصفحات",
                                        TableName = "Tbl_UsersProcedureForm",
                                        TableRecordId = list.ID.ToString(),
                                        Description = "",
                                        ManagementName = Program.GlbV_Management,
                                        ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                        EmployeeName = Program.GlbV_EmpName,
                                        User_ID = Program.GlbV_UserId,
                                        UserName = Program.GlbV_UserName,
                                        FormName = "ProceduresFormsFrm"


                                    };
                                    FsEvDb.SecurityEvents.Add(sev);
                                    FsEvDb.SaveChanges();
                                    //************************************
                                }
                            }

                        }


                    }
                    FsDb.SaveChanges();
                    if (textBox3.Text == "1")
                    {
                        //var validationSaveUserIns = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 34 && w.ProcdureId == 1);
                        //if (validationSaveUserIns != null)
                        //{
                        MessageBox.Show("تم الحفظ");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        treeList1.ExpandAll();
                        textBox1.Select();
                        this.ActiveControl = textBox1;
                        textBox1.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية اضافة  اجراءات الصفحات .. برجاء مراجعة مدير المنظومه");
                        //}

                    }
                    else if (textBox3.Text == "0")
                    {
                        //var validationSaveUserUpd = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 34 && w.ProcdureId == 3);
                        //if (validationSaveUserUpd != null)
                        //{
                        MessageBox.Show("تم التعديل");
                        dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                        treeList1.ExpandAll();
                        textBox1.Select();
                        this.ActiveControl = textBox1;
                        textBox1.Focus();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ليس لديك صلاحية تعديل  اجراءات الصفحات .. برجاء مراجعة مدير المنظومه");
                        //}

                    }
                    treeList1.DataSource = FsDb.Tbl_MenuProgramUnites.ToList();
                    dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                    treeList1.ExpandAll();
                    textBox2.Text = "";
                    comboBox1.SelectedValue = -1;
                }
                //}

                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية اضافة او تعديل  اجراءات الصفحات .. برجاء مراجعة مدير المنظومه");
                //}
            }

        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
            textBox2.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[0]).ToString();
            textBox4.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[1]).ToString();
            xID = int.Parse(textBox4.Text);
            var list = FsDb.Tbl_MenuProgramUnites.Find(xID);
            if (list.Parent_ID != null)
            {
                textBox5.Text = treeList1.GetFocusedRowCellValue(treeList1.Columns[2]).ToString();
            }
            if (list.Forms_ID != null)
            {
                comboBox1.SelectedValue = int.Parse(treeList1.GetFocusedRowCellValue(treeList1.Columns[3]).ToString());
                int Vint_Form = int.Parse(comboBox1.SelectedValue.ToString());
                int vint_formid = int.Parse(comboBox1.SelectedValue.ToString());
                //var listPrFrm = FsDb.Tbl_ProceduresForms.Where(x => x.Form_ID == vint_formid).ToList();
                var listPrFrm = FsDb.Tbl_Procedures.ToList();
                if (listPrFrm.Count != 0)

                {
                    textBox3.Text = "0";
                    dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                    for (int i = 0; i < listPrFrm.Count(); i++)
                    {

                        int Vint_Id = 0;
                        Vint_Id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        var listPrfrm = FsDb.Tbl_ProceduresForms.Where(x => x.Procedure_ID == Vint_Id && x.Form_ID == Vint_Form).ToList();

                        if (listPrfrm.Count() > 0)
                        {
                            dataGridView1.Rows[i].Cells[Column1.Name].Value = true;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[Column1.Name].Value = false;
                        }

                    }
                }
                else
                {
                    textBox3.Text = "1";
                    dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                }
            }
            else
            {

                comboBox1.SelectedValue = -1;
                var listPrFrm = FsDb.Tbl_Procedures.ToList();

                textBox3.Text = "0";
                dataGridView1.DataSource = FsDb.Tbl_Procedures.ToList();
                for (int i = 0; i < listPrFrm.Count(); i++)
                {
                    dataGridView1.Rows[i].Cells[Column1.Name].Value = false;
                }

            }
            dataGridView1.Select();
            this.ActiveControl = dataGridView1;
            dataGridView1.Focus();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
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
