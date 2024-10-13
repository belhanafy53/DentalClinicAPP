using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;
using DentalClinicAPP.Classes;
namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class ItemsFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        //ModelSecurity DNtSCDB = new ModelSecurity();
        public ItemsFrm()
        {
            InitializeComponent();
        }

        private void ItemsFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'itemsDs.Tbl_ItemsOfTax' table. You can move, or remove it, as needed.
            this.tbl_ItemsOfTaxTableAdapter.Fill(this.itemsDs.Tbl_ItemsOfTax);
            txtCodeTypeID.Text = "";
           
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
            

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            txtCodeTypeID.Text =  dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtCodeType.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtUniqueNumber.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtCodeName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtCodeType.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter Code Type want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل كود الصنف المراد اضافته !");
                }

            }
            if (txtUniqueNumber.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter Unique Number want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل الكود الموحد للصنف المراد اضافته !");
                }

            }
            if (txtCodeName.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter Code Name want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل الاسم للصنف المراد اضافته !");
                }

            }
            else
            {
                if (txtCodeTypeID.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    
                    Tbl_ItemsOfTax ItTax = new Tbl_ItemsOfTax
                    {
                        CodeType = txtCodeType.Text,
                        UniqueNumber = txtUniqueNumber.Text,
                        CodeName = txtCodeName.Text,

                    };
                    DNtMDB.Tbl_ItemsOfTax.Add(ItTax);
                    DNtMDB.SaveChanges();
                    int Vint_LastRow = ItTax.ID;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Items Of Tax Add",
                        TableName = "Tbl_ItemsOfTax",
                        TableRecordId = Vint_LastRow.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "ItemsFrm "


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

                    txtCodeType.Text = "";
                      txtUniqueNumber.Text = "";
                      txtCodeName.Text = "";

                    txtCodeType.Focus();
                    txtCodeType.Select();
                    this.ActiveControl = txtCodeType;

                   var listCitysSerch = DNtMDB.Tbl_ItemsOfTax.ToList();

                    dataGridView1.DataSource = listCitysSerch;
                    
                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medical Analysis");
                    //}
                }
                else if (txtCodeTypeID.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    int Vint_ID = int.Parse(txtCodeTypeID.Text);
                    var result = DNtMDB.Tbl_ItemsOfTax.FirstOrDefault(x => x.ID == Vint_ID);
                    result.CodeType = txtCodeType.Text;
                    result.UniqueNumber = txtUniqueNumber.Text;
                    result.CodeName = txtCodeName.Text;
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "ItemOfTax Update",
                        TableName = "Tbl_ItemsOfTax",
                        TableRecordId = result.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "ItemsFrm"
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

                    txtCodeType.Text = "";
                    txtUniqueNumber.Text = "";
                    txtCodeName.Text = "";

                    this.ActiveControl = txtCodeType;
                    txtCodeType.Focus();

                    var listCitysSerch = DNtMDB.Tbl_ItemsOfTax.ToList();

                    dataGridView1.DataSource = listCitysSerch;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to update Medical Analysis");
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
            int Vint_ID = 0;
            Vint_ID = int.Parse(txtCodeTypeID.Text);
            if (Vint_D1rows != 0)
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this ItemsO f Tax  ?", "Delete Item ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {


                        var resultR = DNtMDB.Tbl_ItemsOfTax.Find(Vint_ID);
                        DNtMDB.Tbl_ItemsOfTax.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Items Of Tax",
                            TableName = "Tbl_ItemsOfTax",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ItemsFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            simpleButton3_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            simpleButton4_Click(sender, e);
                        }

                    }


                    txtCodeType.Select();
                    this.ActiveControl = txtCodeType;
                    txtCodeType.Focus();
                    this.tbl_ItemsOfTaxTableAdapter.Fill(this.itemsDs.Tbl_ItemsOfTax);
                    txtCodeTypeID.Text = "";
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذا الصنف  ؟", "حذف صنف ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_ItemsOfTax.Find(Vint_ID);
                        DNtMDB.Tbl_ItemsOfTax.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Items Of Tax",
                            TableName = "Tbl_ItemsOfTax",
                            TableRecordId = resultR.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "ItemsFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            simpleButton3_Click(sender, e);

                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            simpleButton4_Click(sender, e);
                        }
                        this.tbl_ItemsOfTaxTableAdapter.Fill(this.itemsDs.Tbl_ItemsOfTax);
                        txtCodeTypeID.Text = "";

                    }


                    txtCodeType.Select();
                    this.ActiveControl = txtCodeType;
                    txtCodeType.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Item to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر الصنف المراد حذفه");
                }

                txtCodeType.Select();
                this.ActiveControl = txtCodeType;
                txtCodeType.Focus();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var listItems = DNtMDB.Tbl_ItemsOfTax.Where(x => x.CodeName.Contains(txtSearch.Text)).OrderBy(x => x.ID).ToList();
            dataGridView1.DataSource = listItems;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
             
            InitializeComponent();
            this.tbl_ItemsOfTaxTableAdapter.Fill(this.itemsDs.Tbl_ItemsOfTax);
            txtCodeTypeID.Text = "";
            
            LangTxtBox.Text = "en";
            txtCodeType.Select();
            this.ActiveControl = txtCodeType;
            txtCodeType.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            LangTxtBox.Text = "ar-EG";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
          
            InitializeComponent();
            this.tbl_ItemsOfTaxTableAdapter.Fill(this.itemsDs.Tbl_ItemsOfTax);
            txtCodeTypeID.Text = "";
            txtCodeType.Select();
            this.ActiveControl = txtCodeType;
            txtCodeType.Focus();
        }
    }
}
