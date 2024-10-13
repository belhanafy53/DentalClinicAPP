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
using DentalClinicAPP.Classes;
using System.Threading;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class CompaniesType : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        public CompaniesType()
        {
            InitializeComponent();
        }

        private void CompaniesType_Load(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en" || Program.GlbV_Language == "en")
            {
                simpleButton3_Click(sender, e);

            }
            else if (LangTxtBox.Text == "ar-EG" || Program.GlbV_Language == "ar-EG")
            {
                simpleButton4_Click(sender, e);
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";
            LangTxtBox.Text = "en";
            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_CompanyType.ToList();
            if (listAnalysis.Count > 0)
            {
                dataGridView1.DataSource = listAnalysis;
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["Name_E"].Width = 200;
                dataGridView1.Columns["Code"].Width = 150;
                dataGridView1.Columns["Name_E"].HeaderText = "Company Type";
                dataGridView1.Columns["Code"].HeaderText = "Code";

            }
            textBox1.Text = "";
            Nametxt.Focus();
            this.ActiveControl = Nametxt;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";
            LangTxtBox.Text = "ar-EG";
            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            var listAnalysis = DNtMDB.Tbl_CompanyType.ToList();
            if (listAnalysis.Count > 0)
            {
                dataGridView1.DataSource = listAnalysis;
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["Name_Ar"].Width = 200;
                dataGridView1.Columns["Code"].Width = 150;
                dataGridView1.Columns["Name_Ar"].HeaderText = "طبيعة الشركه";
                dataGridView1.Columns["Code"].HeaderText = "الكود";

            }
            textBox1.Text = "";
            Nametxt.Focus();
            this.ActiveControl = Nametxt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int Vint_Id = int.Parse(textBox1.Text);
            var listAnalysis = DNtMDB.Tbl_CompanyType.Where(x=>x.ID == Vint_Id).ToList();
            if (textBox1.Text != "")
            {
                //*******Update
                listAnalysis[0].Name_E = Nametxt.Text;
                listAnalysis[0].Name_Ar = NameArtxt.Text;
                listAnalysis[0].Code = DevNotxt.Text;
                DNtMDB.SaveChanges();
                if (LangTxtBox.Text == "en" || Program.GlbV_Language == "en")
                {
                    MessageBox.Show("Updated");
                    simpleButton3_Click(sender, e);
                }
                else if (LangTxtBox.Text == "ar-EG" || Program.GlbV_Language == "ar-EG")
                {
                    MessageBox.Show("تم التعديل");
                    simpleButton4_Click(sender, e);
                }

            }
            else if (textBox1.Text == "")
            {
                //***********Insert
                Tbl_CompanyType Dv = new Tbl_CompanyType()
                {
                    Name_E = Nametxt.Text,
                    Name_Ar = NameArtxt.Text,
                    Code = DevNotxt.Text
                };
                DNtMDB.Tbl_CompanyType.Add(Dv);
                DNtMDB.SaveChanges();
                if (LangTxtBox.Text == "en" || Program.GlbV_Language == "en")
                {
                    MessageBox.Show("Saved");
                    simpleButton3_Click(sender, e);
                }
                else if (LangTxtBox.Text == "ar-EG" || Program.GlbV_Language == "ar-EG")
                {
                    MessageBox.Show("تم الحفظ");
                    simpleButton4_Click(sender, e);
                }


            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int Vint_Dev = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var listAnalysis = DNtMDB.Tbl_CompanyType.Where(x => x.ID == Vint_Dev).ToList();
            Nametxt.Text = listAnalysis[0].Name_E.ToString();
            NameArtxt.Text = listAnalysis[0].Name_Ar.ToString();
            DevNotxt.Text = listAnalysis[0].Code.ToString();
            textBox1.Text = listAnalysis[0].ID.ToString();
            Nametxt.Focus();
        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NameArtxt.Focus();
            }
        }

        private void NameArtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DevNotxt.Focus();
            }
        }

        private void DevNotxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }
    }
}
