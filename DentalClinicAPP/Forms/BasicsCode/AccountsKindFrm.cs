using DentalClinicAPP.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace FinancialSysApp.Forms
{
    public partial class AccountsKindFrm : DevExpress.XtraEditors.XtraForm
    {
        Model1 FsDb = new Model1();
        int xcatid;
        public AccountsKindFrm()
        {
            InitializeComponent();
        }

        private void AccountsKindFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FsDb.Tbl_AccountsKind.ToList();
            dataGridView1.Columns["Name"].HeaderText = "النوع";

            dataGridView1.Columns["Name"].Width = 420;
            dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["Tbl_Document"].Visible = false;
            Nametxt.Text = "";
            Nametxt.Select();
            this.ActiveControl = Nametxt;
            Nametxt.Focus();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Nametxt.Text = "";
                xcatid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var result = FsDb.Tbl_AccountsKind.SingleOrDefault(x => x.ID == xcatid);
                Nametxt.Text = result.Name;
                IDtxt.Text = xcatid.ToString();
            }
        }

        private void Nametxt_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FsDb.Tbl_AccountsKind.Where(x => x.Name.Contains(Nametxt.Text)).ToList();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Nametxt.Text = "";
            xcatid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var result = FsDb.Tbl_AccountsKind.SingleOrDefault(x => x.ID == xcatid);
            Nametxt.Text = result.Name;
            IDtxt.Text = xcatid.ToString();
        }

        private void Nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.Focus();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try {
                int xrows = dataGridView1.RowCount;
                if (xrows != 0 && Nametxt.Text != "")
                {
                    var result1 = MessageBox.Show("هل تريد حدف هدا النوع  ؟", "حدف نوع ", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        var result = FsDb.Tbl_AccountsKind.Find(xcatid);
                        FsDb.Tbl_AccountsKind.Remove(result);
                        FsDb.SaveChanges();
                        MessageBox.Show("  تم الحدف");
                    }
                    Nametxt.Text = "";
                    IDtxt.Text = "";
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
                else
                {
                    MessageBox.Show("من فضلك حدد النوع المراد حدفه");
                    Nametxt.Select();
                    this.ActiveControl = Nametxt;
                    Nametxt.Focus();
                }
            }
            catch


            {
                MessageBox.Show("هذا النوع لايمكن حذفه لوجود حسابات له", "المنظومه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل النوع ");
                Nametxt.Select();
                this.ActiveControl = Nametxt;
                Nametxt.Focus();
            }
                else
                {
                    int xrows = dataGridView1.RowCount;
                    if (IDtxt.Text == "" && Nametxt.Text != "")


                    {
                        Tbl_AccountsKind CatF = new Tbl_AccountsKind
                        {
                            Name = Nametxt.Text,

                        };
                        FsDb.Tbl_AccountsKind.Add(CatF);
                        FsDb.SaveChanges();
                        MessageBox.Show("تم الحفظ");
                        dataGridView1.DataSource = FsDb.Tbl_AccountsKind.ToList();
                    Nametxt.Text = "";
                    IDtxt.Text = "";
                    Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                    }
                    else  
                    {
                        xcatid = int.Parse(IDtxt.Text);
                        var result = FsDb.Tbl_AccountsKind.SingleOrDefault(x => x.ID == xcatid);
                        result.Name = Nametxt.Text;
                        FsDb.SaveChanges();
                        MessageBox.Show("تم التعديل");
                        dataGridView1.DataSource = FsDb.Tbl_AccountsKind.ToList();
                    Nametxt.Text = "";
                    IDtxt.Text = "";
                    Nametxt.Select();
                        this.ActiveControl = Nametxt;
                        Nametxt.Focus();
                    }
                }

            }
        }
    }
    

