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
    public partial class TokenFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        public TokenFrm()
        {
            InitializeComponent();
        }

        private void TokenFrm_Load(object sender, EventArgs e)
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
            var listAnalysis = DNtMDB.Tbl_Token.ToList();
            if (listAnalysis.Count > 0)
            {
                Nametxt.Text = listAnalysis[0].Name.ToString();
                Pintxt.Text = listAnalysis[0].Pin.ToString();
                textBox1.Text = listAnalysis[0].ID.ToString();
                txtCertificateSer.Text = listAnalysis[0].CerificateSer.ToString();
            }
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
            var listAnalysis = DNtMDB.Tbl_Token.ToList();
            if (listAnalysis.Count > 0)
            {
                Nametxt.Text = listAnalysis[0].Name.ToString();
                Pintxt.Text = listAnalysis[0].Pin.ToString();
                textBox1.Text = listAnalysis[0].ID.ToString();
                txtCertificateSer.Text = listAnalysis[0].CerificateSer.ToString();
            }
            Nametxt.Focus();
            this.ActiveControl = Nametxt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var listAnalysis = DNtMDB.Tbl_Token.ToList();
            if (listAnalysis.Count > 0)
            {
                //*******Update
                listAnalysis[0].Name = Nametxt.Text;
                listAnalysis[0].Pin = Pintxt.Text;
                listAnalysis[0].CerificateSer = txtCertificateSer.Text;
                DNtMDB.SaveChanges();
                if (LangTxtBox.Text == "en" || Program.GlbV_Language == "en")
                {
                    MessageBox.Show("Updated");

                }
                else if (LangTxtBox.Text == "ar-EG" || Program.GlbV_Language == "ar-EG")
                {
                    MessageBox.Show("تم التعديل");
                }

            }
            else if (listAnalysis.Count == 0)
            {
                //***********Insert
                Tbl_Token Dv = new Tbl_Token()
                {
                     Name = Nametxt.Text,
                    Pin = Pintxt.Text
                };
                DNtMDB.Tbl_Token.Add(Dv);
                DNtMDB.SaveChanges();
                if (LangTxtBox.Text == "en" || Program.GlbV_Language == "en")
                {
                    MessageBox.Show("Saved");

                }
                else if (LangTxtBox.Text == "ar-EG" || Program.GlbV_Language == "ar-EG")
                {
                    MessageBox.Show("تم الحفظ");
                }


            }
        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pintxt.Focus();
            }
        }

        private void Pintxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCertificateSer.Focus();
            }
        }

        private void txtCertificateSer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }
    }
}
