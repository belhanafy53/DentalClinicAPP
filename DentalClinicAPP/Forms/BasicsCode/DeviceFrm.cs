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
    public partial class DeviceFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        public DeviceFrm()
        {
            InitializeComponent();
        }

        private void DeviceFrm_Load(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en" || Program.GlbV_Language == "en")
            {
                simpleButton3_Click(sender,e);

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
            var listAnalysis = DNtMDB.Tbl_Device.ToList();
            if (listAnalysis.Count > 0)
            {
                dataGridView1.DataSource = listAnalysis;
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["DeviceName"].Width = 200 ;
                dataGridView1.Columns["DeviceNO"].Width = 150 ;
                dataGridView1.Columns["DeviceName"].HeaderText = "Device Name";
                dataGridView1.Columns["DeviceNO"].HeaderText = "Device NO";

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
            var listAnalysis = DNtMDB.Tbl_Device.ToList();
            if (listAnalysis.Count > 0)
            {
                dataGridView1.DataSource = listAnalysis;
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["DeviceName"].Width = 200;
                dataGridView1.Columns["DeviceNO"].Width = 150;
                dataGridView1.Columns["DeviceName"].HeaderText = "اسم الجهاز";
                dataGridView1.Columns["DeviceNO"].HeaderText = "الجهاز رقم";

            }
            textBox1.Text = "";
            Nametxt.Focus();
            this.ActiveControl = Nametxt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var listAnalysis = DNtMDB.Tbl_Device.ToList();
            if (textBox1.Text != "")
            {
                //*******Update
                listAnalysis[0].DeviceName = Nametxt.Text;
                listAnalysis[0].DeviceNO = DevNotxt.Text;
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
                Tbl_Device Dv = new Tbl_Device()
                {
                    DeviceName = Nametxt.Text,
                    DeviceNO = DevNotxt.Text
                };
                DNtMDB.Tbl_Device.Add(Dv);
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
            int Vint_Dev = int.Parse (dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var listAnalysis = DNtMDB.Tbl_Device.Where(x=>x.ID == Vint_Dev).ToList();
            Nametxt.Text = listAnalysis[0].DeviceName.ToString();
            DevNotxt.Text = listAnalysis[0].DeviceNO.ToString();
            textBox1.Text = listAnalysis[0].ID.ToString();
            Nametxt.Focus();
        }
    }
}
