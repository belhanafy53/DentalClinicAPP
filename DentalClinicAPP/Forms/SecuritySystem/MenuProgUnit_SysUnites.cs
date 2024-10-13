using System;
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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;

using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace DentalClinicAPP.Forms.SecuritySystem
{
    public partial class MenuProgUnit_SysUnites : Form
    {
        ModelSecurity FsDb = new ModelSecurity();
        ModelEvents FsEvDb = new ModelEvents();
        int? Vint_SysUnitID;
        int? Vint_MenuPr;
        string VStr_MenuPr;
        public MenuProgUnit_SysUnites()
        {

            InitializeComponent();

        }


        private void MenuProgUnit_SysUnites_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'securityDs.Tbl_MenuProgramUnites' table.You can move, or remove it, as needed.
            this.tbl_MenuProgramUnitesTableAdapter.Fill(this.securityDs.Tbl_MenuProgramUnites);
            // TODO: This line of code loads data into the 'securityDs.Tbl_SystemUnites' table. You can move, or remove it, as needed.
            //this.tbl_SystemUnitesTableAdapter.Fill(this.securityDs.Tbl_SystemUnites);
           
                
                comboBox1.DataSource = FsDb.Tbl_SystemUnites.ToList();
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
                comboBox1.SelectedIndex = -1;
                comboBox1.Text = "اختر الوحده";

                if (Program.GlbV_Language == "en")
                {
                    simpleButton3_Click(sender, e);

                }
                else if (Program.GlbV_Language == "ar-EG")
                {
                    simpleButton4_Click(sender, e);
                }
           
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "")
            {

                Vint_SysUnitID = int.Parse(comboBox1.SelectedValue.ToString());


                int Vint_TreelistCount = treeList1.Nodes.Count();
                List<TreeListNode> nodes = treeList1.GetNodeList();
                foreach (TreeListNode item in nodes)
                {


                    Vint_MenuPr = int.Parse(item.GetValue("ID").ToString());
                    VStr_MenuPr = item.GetValue("Name").ToString();
                    Vint_SysUnitID = int.Parse(comboBox1.SelectedValue.ToString());

                    var listMs = FsDb.Tbl_MenuProgUnit_SysUnites.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);

                    if (listMs != null)
                    {
                        item.CheckState = CheckState.Checked;
                    }

                }

            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.SelectedItem.ToString() != "")
                {

                    Vint_SysUnitID = int.Parse(comboBox1.SelectedValue.ToString());


                    int Vint_TreelistCount = treeList1.Nodes.Count();
                    List<TreeListNode> nodes = treeList1.GetNodeList();
                    foreach (TreeListNode item in nodes)
                    {


                        Vint_MenuPr = int.Parse(item.GetValue("ID").ToString());
                        VStr_MenuPr = item.GetValue("Name").ToString();
                        Vint_SysUnitID = int.Parse(comboBox1.SelectedValue.ToString());

                        var listMs = FsDb.Tbl_MenuProgUnit_SysUnites.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);

                        if (listMs != null)
                        {
                            item.CheckState = CheckState.Checked;
                        }

                    }

                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 35 && w.ProcdureId == 1);
                //if (validationSaveUser != null)
                //{
                if (comboBox1.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please Choose Program unite ");
                    comboBox1.Focus();
                }
                else
                {
                    Vint_SysUnitID = int.Parse(comboBox1.SelectedValue.ToString());
                    var listremove = FsDb.Tbl_MenuProgUnit_SysUnites.Where(x => x.SysUnites_ID == Vint_SysUnitID);

                    FsDb.Tbl_MenuProgUnit_SysUnites.RemoveRange(listremove);

                    FsDb.SaveChanges();
                    Vint_SysUnitID = int.Parse(comboBox1.SelectedValue.ToString());
                    foreach (TreeListNode n in treeList1.GetAllCheckedNodes())
                    {
                        Vint_MenuPr = int.Parse(n.GetValue("ID").ToString());


                        Tbl_MenuProgUnit_SysUnites menuprsys = new Tbl_MenuProgUnit_SysUnites
                        {
                            SysUnites_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                            MenuProgUnit_ID = int.Parse(n.GetValue("ID").ToString())

                        };
                        FsDb.Tbl_MenuProgUnit_SysUnites.Add(menuprsys);
                        FsDb.SaveChanges();
                    }
                    int Vint_TreelistCount = treeList1.Nodes.Count();
                    List<TreeListNode> nodes = treeList1.GetNodeList();
                    //****************لحذف الصفحات التي لم يتم اختيارها ان وجدت
                    foreach (TreeListNode item in nodes)
                    {
                        if (item.CheckState == CheckState.Unchecked)
                        {
                            Vint_MenuPr = int.Parse(item.GetValue("ID").ToString());
                            var ListForm = FsDb.Tbl_MenuProgramUnites.FirstOrDefault(x => x.ID == Vint_MenuPr);
                            if (ListForm.Forms_ID != null)
                            {
                                int Vint_FormId = int.Parse(ListForm.Forms_ID.ToString());
                                //******************تحديد المستخدمين التابعين للوحده 
                                var listUserSysunite = FsDb.Tbl_User_SysUnites.Where(x => x.SysUnites_ID == Vint_SysUnitID).ToList();

                                for (int u = 0; u < listUserSysunite.Count; u++)
                                {
                                    int? Vint_UserId = listUserSysunite[u].User_ID;
                                    //*****************************
                                    //*****************  الاجراءات الخاصه بصفحات المستخدم 
                                    var listProcedureForm = FsDb.Tbl_ProceduresForms.Where(x => x.Form_ID == Vint_FormId).ToList();
                                    for (int o = 0; o < listProcedureForm.Count; o++)
                                    {
                                        int? Vint_procedureFomrID = int.Parse(listProcedureForm[o].ID.ToString());
                                        var ListUserprocedurForm = FsDb.Tbl_UsersProcedureForm.FirstOrDefault(z => z.User_ID == Vint_UserId && z.ProceduresForms_ID == Vint_procedureFomrID);
                                        if (ListUserprocedurForm != null)
                                        {
                                            FsDb.Tbl_UsersProcedureForm.Remove(ListUserprocedurForm);
                                            FsDb.SaveChanges();
                                        }
                                    }
                                    //******************
                                }
                            }
                            var listMs = FsDb.Tbl_MenuProgUnit_SysUnites.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);

                            var listfts = FsDb.Tbl_FormsUserTypeUser.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);
                            var listAusts = FsDb.Tbl_UserAuthForms.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);
                            if (listMs != null)
                            {
                                FsDb.Tbl_MenuProgUnit_SysUnites.Remove(listMs);
                                FsDb.SaveChanges();
                                if (listfts != null)
                                {
                                    FsDb.Tbl_FormsUserTypeUser.Remove(listfts);
                                    FsDb.SaveChanges();
                                    if (listAusts != null)
                                    {
                                        FsDb.Tbl_UserAuthForms.Remove(listAusts);
                                        FsDb.SaveChanges();
                                    }
                                }
                            }

                        }
                    }
                    //***************************************
                    var listmnu = FsDb.Tbl_MenuProgramUnites.Where(x => x.Parent_ID == 1).ToList();

                    for (int i = 0; i < listmnu.Count; i++)
                    {
                        int Vint_MenuPr = int.Parse(listmnu[i].ID.ToString());
                        string VStr_MenuPr = listmnu[i].Name.ToString();

                        var listMs = FsDb.Tbl_MenuProgUnit_SysUnites.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);

                        if (listMs == null)
                        {
                            Tbl_MenuProgUnit_SysUnites menuprsys = new Tbl_MenuProgUnit_SysUnites
                            {
                                SysUnites_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                                MenuProgUnit_ID = Vint_MenuPr

                            };
                            FsDb.Tbl_MenuProgUnit_SysUnites.Add(menuprsys);
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            int Vint_LastRow = int.Parse(menuprsys.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "Program unite Form Add",
                                TableName = "Tbl_MenuProgUnit_SysUnites",
                                TableRecordId = Vint_LastRow.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "MenuProgUnit_SysUnitesFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************
                        }
                    }

                    MessageBox.Show("Save");
                }
                //}


                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية اضافة  صفحات وحدات المنظومة .. برجاء مراجعة مدير المنظومه");
                //}
            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 35 && w.ProcdureId == 1);
                //if (validationSaveUser != null)
                //{
                if (comboBox1.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("من فضلك قم بإختيار وحدة المنظومة ");
                    comboBox1.Focus();
                }
                else
                {
                    var listremove = FsDb.Tbl_MenuProgUnit_SysUnites.Where(x => x.SysUnites_ID == Vint_SysUnitID);

                    FsDb.Tbl_MenuProgUnit_SysUnites.RemoveRange(listremove);

                    FsDb.SaveChanges();
                    Vint_SysUnitID = int.Parse(comboBox1.SelectedValue.ToString());
                    foreach (TreeListNode n in treeList1.GetAllCheckedNodes())
                    {
                        Vint_MenuPr = int.Parse(n.GetValue("ID").ToString());


                        Tbl_MenuProgUnit_SysUnites menuprsys = new Tbl_MenuProgUnit_SysUnites
                        {
                            SysUnites_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                            MenuProgUnit_ID = int.Parse(n.GetValue("ID").ToString())

                        };
                        FsDb.Tbl_MenuProgUnit_SysUnites.Add(menuprsys);
                        FsDb.SaveChanges();
                    }
                    int Vint_TreelistCount = treeList1.Nodes.Count();
                    List<TreeListNode> nodes = treeList1.GetNodeList();
                    //****************لحذف الصفحات التي لم يتم اختيارها ان وجدت
                    foreach (TreeListNode item in nodes)
                    {
                        if (item.CheckState == CheckState.Unchecked)
                        {
                            Vint_MenuPr = int.Parse(item.GetValue("ID").ToString());
                            var ListForm = FsDb.Tbl_MenuProgramUnites.FirstOrDefault(x => x.ID == Vint_MenuPr);
                            if (ListForm.Forms_ID != null)
                            {
                                int Vint_FormId = int.Parse(ListForm.Forms_ID.ToString());
                                //******************تحديد المستخدمين التابعين للوحده 
                                var listUserSysunite = FsDb.Tbl_User_SysUnites.Where(x => x.SysUnites_ID == Vint_SysUnitID).ToList();

                                for (int u = 0; u < listUserSysunite.Count; u++)
                                {
                                    int? Vint_UserId = listUserSysunite[u].User_ID;
                                    //*****************************
                                    //*****************  الاجراءات الخاصه بصفحات المستخدم 
                                    var listProcedureForm = FsDb.Tbl_ProceduresForms.Where(x => x.Form_ID == Vint_FormId).ToList();
                                    for (int o = 0; o < listProcedureForm.Count; o++)
                                    {
                                        int? Vint_procedureFomrID = int.Parse(listProcedureForm[o].ID.ToString());
                                        var ListUserprocedurForm = FsDb.Tbl_UsersProcedureForm.FirstOrDefault(z => z.User_ID == Vint_UserId && z.ProceduresForms_ID == Vint_procedureFomrID);
                                        if (ListUserprocedurForm != null)
                                        {
                                            FsDb.Tbl_UsersProcedureForm.Remove(ListUserprocedurForm);
                                            FsDb.SaveChanges();
                                        }
                                    }
                                    //******************
                                }
                            }
                            var listMs = FsDb.Tbl_MenuProgUnit_SysUnites.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);

                            var listfts = FsDb.Tbl_FormsUserTypeUser.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);
                            var listAusts = FsDb.Tbl_UserAuthForms.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);
                            if (listMs != null)
                            {
                                FsDb.Tbl_MenuProgUnit_SysUnites.Remove(listMs);
                                FsDb.SaveChanges();
                                if (listfts != null)
                                {
                                    FsDb.Tbl_FormsUserTypeUser.Remove(listfts);
                                    FsDb.SaveChanges();
                                    if (listAusts != null)
                                    {
                                        FsDb.Tbl_UserAuthForms.Remove(listAusts);
                                        FsDb.SaveChanges();
                                    }
                                }
                            }

                        }
                    }
                    //***************************************
                    var listmnu = FsDb.Tbl_MenuProgramUnites.Where(x => x.Parent_ID == 1).ToList();

                    for (int i = 0; i < listmnu.Count; i++)
                    {
                        int Vint_MenuPr = int.Parse(listmnu[i].ID.ToString());
                        string VStr_MenuPr = listmnu[i].Name.ToString();

                        var listMs = FsDb.Tbl_MenuProgUnit_SysUnites.FirstOrDefault(x => x.SysUnites_ID == Vint_SysUnitID && x.MenuProgUnit_ID == Vint_MenuPr);

                        if (listMs == null)
                        {
                            Tbl_MenuProgUnit_SysUnites menuprsys = new Tbl_MenuProgUnit_SysUnites
                            {
                                SysUnites_ID = int.Parse(comboBox1.SelectedValue.ToString()),
                                MenuProgUnit_ID = Vint_MenuPr

                            };
                            FsDb.Tbl_MenuProgUnit_SysUnites.Add(menuprsys);
                            FsDb.SaveChanges();
                            //---------------خفظ ااحداث 
                            int Vint_LastRow = int.Parse(menuprsys.ID.ToString());
                            SecurityEvent sev = new SecurityEvent
                            {
                                ActionName = "اضافة وحدات صفحات المنظومة",
                                TableName = "Tbl_MenuProgUnit_SysUnites",
                                TableRecordId = Vint_LastRow.ToString(),
                                Description = "",
                                ManagementName = Program.GlbV_Management,
                                ActionDate = Convert.ToDateTime(Program.GlbV_DateTime),
                                EmployeeName = Program.GlbV_EmpName,
                                User_ID = Program.GlbV_UserId,
                                UserName = Program.GlbV_UserName,
                                FormName = "MenuProgUnit_SysUnitesFrm"


                            };
                            FsEvDb.SecurityEvents.Add(sev);
                            FsEvDb.SaveChanges();
                            //************************************
                        }
                    }

                    MessageBox.Show("تم الحفظ");
                }
                //}


                //else
                //{
                //    MessageBox.Show("ليس لديك صلاحية اضافة  صفحات وحدات المنظومة .. برجاء مراجعة مدير المنظومه");
                //}
            }

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            treeList1.ExpandAll();
            treeList1.UncheckAll();
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

