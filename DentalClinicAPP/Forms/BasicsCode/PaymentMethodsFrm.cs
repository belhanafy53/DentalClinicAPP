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
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class PaymentMethodsFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        string Vst_Code = "";
        public PaymentMethodsFrm()
        {
            InitializeComponent();
        }
        private void Dg_E()
        {
            CodeTxt.Text = "";
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Code"].Visible = true;
            dataGridView1.Columns["Desc_En"].Visible = true;
            dataGridView1.Columns["Desc_Ar"].Visible = true;
            //dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Desc_En"].Width = 200;
            dataGridView1.Columns["Desc_Ar"].Width = 200;
            CodeTxt.Focus();
        }
        private void Dg_Ar()
        {
            CodeTxt.Text = "";
            DescEtxt.Text = "";
            DescArtxt.Text = "";
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Code"].HeaderText = "الكود";
            dataGridView1.Columns["Desc_En"].HeaderText = "الاسم الانجليزي";
            dataGridView1.Columns["Desc_Ar"].HeaderText = "الاسم العربي";
            //dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Desc_En"].Width = 200;
            dataGridView1.Columns["Desc_Ar"].Width = 200;
            CodeTxt.Focus();
        }
        private void PaymentMethodsFrm_Load(object sender, EventArgs e)
        {
            RowIDtxt.Text = "";
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

            CodeTxt.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_PaymentMethods.OrderBy(x => x.Code).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_E();
            LangTxtBox.Text = "en";
            CodeTxt.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_PaymentMethods.OrderBy(x => x.Code).ToList();
            dataGridView1.DataSource = listAnalysis;
            Dg_Ar();
            LangTxtBox.Text = "ar-EG";
            CodeTxt.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (CodeTxt.Text == "")
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please Enter Code Activity Type want to add !  ");
                }
                else
                {
                    MessageBox.Show("من فضلك ادخل كود طريقة دفع المراد اضافته !");
                }

            }
            else
            {
                if (RowIDtxt.Text == "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 1);
                    //if (validationSaveUser != null)
                    //{
                    //Vint_Uniteid = int.Parse(comboBox1.SelectedValue.ToString());
                    Tbl_PaymentMethods Act = new Tbl_PaymentMethods
                    {
                        Code = CodeTxt.Text,
                        Desc_En = DescEtxt.Text,
                        Desc_Ar = DescArtxt.Text,


                    };
                    DNtMDB.Tbl_PaymentMethods.Add(Act);
                    DNtMDB.SaveChanges();
                    //int Vint_LastRow = Act.;
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "payment Methode Add",
                        TableName = "Tbl_PaymentMethods",
                        TableRecordId = Act.Code.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "PaymentMethods "


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Saved");
                        simpleButton3_Click(sender, e);
                        //Dg_E();
                    }
                    else
                    {
                        MessageBox.Show("تم الحفظ");
                        simpleButton4_Click(sender, e);
                        //Dg_Ar();
                    }

                    DescEtxt.Text = "";
                    CodeTxt.Text = "";

                    CodeTxt.Focus();
                    CodeTxt.Select();
                    this.ActiveControl = CodeTxt;






                    //}

                    //else
                    //{
                    //    MessageBox.Show("You Don't have permition to add Medical Analysis");
                    //}
                }
                else if (RowIDtxt.Text != "")
                {
                    //var validationSaveUser = Program.SecurityProceduresList.FirstOrDefault(w => w.FormId == 42 && w.ProcdureId == 3);
                    //if (validationSaveUser != null)
                    //{
                    //Vint_Uniteid = int.Parse(textBox1.Text);
                    var result = DNtMDB.Tbl_PaymentMethods.FirstOrDefault(x => x.Code == CodeTxt.Text);
                    if (result != null)
                    {
                        result.Code = CodeTxt.Text;
                        result.Desc_En = DescEtxt.Text;
                        result.Desc_Ar = DescArtxt.Text;
                    }

                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "TaxType Update",
                        TableName = "Tbl_PaymentMethods",
                        TableRecordId = result.Code.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "PaymentMethods"
                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    if (LangTxtBox.Text == "en")
                    {
                        MessageBox.Show("Updated");
                        simpleButton3_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("تم التعديل");
                        simpleButton4_Click(sender, e);
                    }

                    DescEtxt.Text = "";
                    CodeTxt.Text = "";
                    RowIDtxt.Text = "";
                    CodeTxt.Focus(); ;

                    //Vint_Uniteid = int.Parse(comboBox1.SelectedValue.ToString());






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

            if (Vint_D1rows != 0 && DescEtxt.Text != "")
            {

                if (LangTxtBox.Text == "en")
                {
                    var result1 = MessageBox.Show("Do you want to delete this Payment Method  ?", "Delete Payment Method ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {


                        var resultR = DNtMDB.Tbl_PaymentMethods.FirstOrDefault(x => x.Code == Vst_Code);
                        DNtMDB.Tbl_PaymentMethods.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete TaxType",
                            TableName = "Tbl_PaymentMethods",
                            TableRecordId = resultR.Code.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "PaymentMethods"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";
                            simpleButton3_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";
                            DescEtxt.Text = "";
                            simpleButton4_Click(sender, e);
                        }

                    }

                    


                    CodeTxt.Select();
                    this.ActiveControl = CodeTxt;
                    CodeTxt.Focus();
                }
                else
                {
                    var result1 = MessageBox.Show("هل تريد حذف هذه طريقة دفع  ؟", "حذف طريقة دفع ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {

                        var resultR = DNtMDB.Tbl_PaymentMethods.Find(Vst_Code);
                        DNtMDB.Tbl_PaymentMethods.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete PaymentMethods",
                            TableName = "Tbl_PaymentMethods",
                            TableRecordId = resultR.Code.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "PaymentMethods"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************

                        if (LangTxtBox.Text == "en")
                        {
                            MessageBox.Show(" Deleted");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("تم الحذف");
                            DescEtxt.Text = "";
                            CodeTxt.Text = "";
                            DescEtxt.Text = "";
                        }

                    }






                    

                    CodeTxt.Select();
                    this.ActiveControl = CodeTxt;
                    CodeTxt.Focus();
                }




            }
            else
            {
                if (LangTxtBox.Text == "en")
                {
                    MessageBox.Show("Please choose Activity Type to want to delete");
                }
                else
                {
                    MessageBox.Show("من فضلك اختر طريقة دفع المراد حذفها");
                }

                DescEtxt.Select();
                this.ActiveControl = DescEtxt;
                DescEtxt.Focus();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DescEtxt.Text = "";
            CodeTxt.Text = "";
            RowIDtxt.Text = "";
            Vst_Code = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            CodeTxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            RowIDtxt.Text = CodeTxt.Text;
            DescEtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            DescArtxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void CodeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DescEtxt.Focus();
            }
        }

        private void DescEtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DescArtxt.Focus();
            }

        }

        private void DescArtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }
    }
}
