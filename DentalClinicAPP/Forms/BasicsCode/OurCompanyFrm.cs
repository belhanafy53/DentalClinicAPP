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
using DevComponents.DotNetBar.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace DentalClinicAPP.Forms.BasicsCode
{
    public partial class OurCompanyFrm : Form
    {
        Model1 DNtMDB = new Model1();
        ModelEvents DNtEVDB = new ModelEvents();
        ModelSecurity DNtSCDB = new ModelSecurity();
        int Vint_codGvrn = 0;
        string Vst_Taxtype = "";
        public OurCompanyFrm()
        {
            InitializeComponent();
        }

        private void OurCompanyFrm_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'taxeDs.Tbl_CompanyType' table. You can move, or remove it, as needed.
            //this.tbl_CompanyTypeTableAdapter.Fill(this.taxeDs.Tbl_CompanyType);
            LangTxtBox.Text = Program.GlbV_Language;
            if (LangTxtBox.Text == "en" && Program.GlbV_Language == "en")
            {
                simpleButton6_Click(sender, e);
            }
            else if (LangTxtBox.Text == "ar-EG" && Program.GlbV_Language == "ar-EG")
            {
                simpleButton5_Click(sender, e);
            }


            EnNametxt.Focus();

        }

        private void EnNametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ArNametxt.Focus();
            }
        }

        private void ArNametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                taxRgtxt.Focus();
            }
        }

        private void taxRgtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CompanyTypeCmb.Focus();
            }
        }

        private void CountryCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GovernorateCmb.Focus();
            }
        }

        private void GovernorateCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CityCmb.Focus();
            }
        }

        private void CityCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Streettxt.Focus();
            }
        }

        private void Streettxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BldNoTxt.Focus();
            }
        }

        private void BldNoTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FloorTxt.Focus();
            }
        }

        private void FloorTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RoomTxt.Focus();
            }
        }

        private void RoomTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PostalCodeTxt.Focus();
            }
        }

        private void PostalCodeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActivityTypeCmb.Focus();
            }
        }

        private void ActivityTypeCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActivityDG.Focus();
            }
        }

        private void TaxTypeCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TaxSubTypeCmb.Focus();
            }
        }

        private void TaxSubTypeCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TaxTybDG.Focus();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en")
            {
                var ListOurCompany = DNtMDB.Tbl_OurCompany.FirstOrDefault(x => x.ID == 1);

                if (EnNametxt.Text == "")
                {
                    MessageBox.Show("Please Enter English Name Of Company");
                    EnNametxt.Focus();
                }
                else if (ArNametxt.Text == "")
                {
                    MessageBox.Show("Please Enter Arabic Name Of Company");
                    ArNametxt.Focus();
                }
                else if (taxRgtxt.Text == "")
                {
                    MessageBox.Show("Please Enter Tax Register Number Of Company");
                    taxRgtxt.Focus();
                }
                else if (CountryCmb.Text == "")
                {
                    MessageBox.Show("Please Enter Country Of Company");
                    CountryCmb.Focus();
                }
                else if (GovernorateCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter Governorate Of Company");
                    GovernorateCmb.Focus();
                }
                else if (CityCmb.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Enter city Of Company");
                    CityCmb.Focus();
                }

                else if (ListOurCompany == null)

                {
                    Tbl_OurCompany OrCmp = new Tbl_OurCompany()
                    {
                        Name_E = EnNametxt.Text,
                        Name_Ar = ArNametxt.Text,
                        TaxRegisterNo = taxRgtxt.Text,
                        CountryCode = CountryCmb.SelectedValue.ToString(),
                        GovernorateID = int.Parse(GovernorateCmb.SelectedValue.ToString()),
                        CityID = int.Parse(CityCmb.SelectedValue.ToString()),
                        Street = Streettxt.Text,
                        BuildingNumber = BldNoTxt.Text,
                        Floor = FloorTxt.Text,
                        room = RoomTxt.Text,
                        PostalCode = PostalCodeTxt.Text,
                        CompanyType = CompanyTypeCmb.SelectedValue.ToString(),
                    };
                    DNtMDB.Tbl_OurCompany.Add(OrCmp);
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Delete Our Company Activity",
                        TableName = "Tbl_OurCompActivity",
                        TableRecordId = OrCmp.ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "OurCompanyFrm"


                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //*********************
                    if (ActivityDG.RowCount > 0)
                    {
                        string Vst_ActiveCode = "";
                        int LastCompId = 0;
                        LastCompId = OrCmp.ID;
                        foreach (DataGridViewRow row in ActivityDG.Rows)
                        {
                            Vst_ActiveCode = row.Cells[1].Value.ToString();

                            Tbl_OurCompActivity AOrCmp = new Tbl_OurCompActivity()
                            {
                                ActivityCode = Vst_ActiveCode,
                                OurCompanyRef = LastCompId,
                            };
                            var ListActivity = DNtMDB.Tbl_OurCompActivity.Where(a => a.ActivityCode == Vst_ActiveCode).ToList();
                            if (ListActivity.Count == 0)
                            {
                                DNtMDB.Tbl_OurCompActivity.Add(AOrCmp);
                                DNtMDB.SaveChanges();
                            }
                        }
                    }
                    if (TaxTybDG.RowCount > 0)
                    {
                        string Vst_TaxTypeCode = "";
                        string Vst_TaxTypeName = "";
                        string Vst_TaxSubTypeCode = "";
                        string Vst_TaxSubTypeName = "";
                        int LastCompId = 0;
                        LastCompId = OrCmp.ID;
                        foreach (DataGridViewRow row in TaxTybDG.Rows)
                        {
                            Vst_TaxTypeCode = row.Cells[1].Value.ToString();
                            Vst_TaxTypeName = row.Cells[2].Value.ToString();
                            Vst_TaxSubTypeCode = row.Cells[3].Value.ToString();
                            Vst_TaxSubTypeName = row.Cells[4].Value.ToString();
                            Tbl_TaxOurCompany TOrCmp = new Tbl_TaxOurCompany()
                            {
                                TaxTypeCode = Vst_TaxTypeCode,
                                TaxSubTypeCode = Vst_TaxSubTypeCode,
                                OurCompanyRef = LastCompId,
                            };
                            var ListTax = DNtMDB.Tbl_TaxOurCompany.Where(a => a.TaxTypeCode == Vst_TaxTypeCode && a.TaxSubTypeCode == Vst_TaxSubTypeCode && a.OurCompanyRef == LastCompId).ToList();
                            if (ListTax.Count == 0)
                            {
                                DNtMDB.Tbl_TaxOurCompany.Add(TOrCmp);
                                DNtMDB.SaveChanges();
                            }
                        }
                    }
                    if (UniteTDG.RowCount > 0)
                    {
                        string Vst_UniteCode = "";
                        int LastCompId = 0;
                        LastCompId = OrCmp.ID;
                        foreach (DataGridViewRow row in UniteTDG.Rows)
                        {
                            Vst_UniteCode = row.Cells[1].Value.ToString();

                            Tbl_OurCompanyUnite UOrCmp = new Tbl_OurCompanyUnite()
                            {
                                UniteCode = Vst_UniteCode,
                                OurCompanyRef = LastCompId,
                            };
                            var ListActivity = DNtMDB.Tbl_OurCompanyUnite.Where(a => a.UniteCode == Vst_UniteCode).ToList();
                            if (ListActivity.Count == 0)
                            {
                                DNtMDB.Tbl_OurCompanyUnite.Add(UOrCmp);
                                DNtMDB.SaveChanges();
                            }
                        }
                    }
                    if (CurrencyDG.RowCount > 0)
                    {
                        string Vst_CurrencyCode = "";
                        int LastCompId = 0;
                        LastCompId = OrCmp.ID;
                        foreach (DataGridViewRow row in CurrencyDG.Rows)
                        {
                            Vst_CurrencyCode = row.Cells[1].Value.ToString();

                            Tbl_OurCompanyCurrency COrCmp = new Tbl_OurCompanyCurrency()
                            {
                                CurrencyCode = Vst_CurrencyCode,
                                OurCompanyRef = LastCompId,
                            };
                            var ListCurrency = DNtMDB.Tbl_OurCompanyCurrency.Where(a => a.CurrencyCode == Vst_CurrencyCode).ToList();
                            if (ListCurrency.Count == 0)
                            {
                                DNtMDB.Tbl_OurCompanyCurrency.Add(COrCmp);
                                DNtMDB.SaveChanges();
                            }
                        }
                    }
                    MessageBox.Show("Saved");
                }
                else if (ListOurCompany != null)
                {
                    ListOurCompany.Name_E = EnNametxt.Text;
                    ListOurCompany.Name_Ar = ArNametxt.Text;
                    ListOurCompany.TaxRegisterNo = taxRgtxt.Text;
                    ListOurCompany.CountryCode = CountryCmb.SelectedValue.ToString();
                    ListOurCompany.GovernorateID = int.Parse(GovernorateCmb.SelectedValue.ToString());
                    ListOurCompany.CityID = int.Parse(CityCmb.SelectedValue.ToString());
                    ListOurCompany.Street = Streettxt.Text;
                    ListOurCompany.BuildingNumber = BldNoTxt.Text;
                    ListOurCompany.Floor = FloorTxt.Text;
                    ListOurCompany.room = RoomTxt.Text;
                    ListOurCompany.PostalCode = PostalCodeTxt.Text;
                    ListOurCompany.CompanyType = CompanyTypeCmb.SelectedValue.ToString();

                    if (ActivityDG.RowCount > 0)
                    {
                        string Vst_ActiveCode = "";
                        int LastCompId = 0;
                        LastCompId = ListOurCompany.ID;
                        foreach (DataGridViewRow row in ActivityDG.Rows)
                        {
                            Vst_ActiveCode = row.Cells[1].Value.ToString();
                            var listActiv = DNtMDB.Tbl_OurCompActivity.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.ActivityCode == Vst_ActiveCode).ToList();
                            if (listActiv.Count > 0)
                            {
                                listActiv[0].ActivityCode = Vst_ActiveCode;
                            }
                            else
                            {
                                Tbl_OurCompActivity AOrCmp = new Tbl_OurCompActivity()
                                {
                                    ActivityCode = Vst_ActiveCode,
                                    OurCompanyRef = LastCompId,
                                };
                                var ListActivity = DNtMDB.Tbl_OurCompActivity.Where(a => a.ActivityCode == Vst_ActiveCode).ToList();
                                if (ListActivity.Count == 0)
                                {
                                    DNtMDB.Tbl_OurCompActivity.Add(AOrCmp);
                                    DNtMDB.SaveChanges();
                                }
                            }

                            DNtMDB.SaveChanges();
                        }
                    }
                    if (TaxTybDG.RowCount > 0)
                    {
                        string Vst_TaxTypeCode = "";
                        string Vst_TaxTypeName = "";
                        string Vst_TaxSubTypeCode = "";
                        string Vst_TaxSubTypeName = "";
                        int LastCompId = 0;

                        LastCompId = ListOurCompany.ID;
                        foreach (DataGridViewRow row in TaxTybDG.Rows)
                        {
                            Vst_TaxTypeCode = row.Cells[1].Value.ToString();
                            Vst_TaxTypeName = row.Cells[2].Value.ToString();
                            Vst_TaxSubTypeCode = row.Cells[3].Value.ToString();
                            Vst_TaxSubTypeName = row.Cells[4].Value.ToString();

                            var listTax = DNtMDB.Tbl_TaxOurCompany.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.TaxTypeCode == Vst_TaxTypeCode && x.TaxTypeCode == Vst_TaxSubTypeCode).ToList();
                            if (listTax.Count > 0)
                            {
                                listTax[0].TaxTypeCode = Vst_TaxTypeCode;
                                listTax[0].TaxSubTypeCode = Vst_TaxSubTypeCode;
                            }
                            else
                            {
                                Tbl_TaxOurCompany TOrCmp = new Tbl_TaxOurCompany()
                                {
                                    TaxTypeCode = Vst_TaxTypeCode,
                                    TaxSubTypeCode = Vst_TaxSubTypeCode,
                                    OurCompanyRef = ListOurCompany.ID,
                                };
                                var ListTax = DNtMDB.Tbl_TaxOurCompany.Where(a => a.TaxTypeCode == Vst_TaxTypeCode && a.TaxSubTypeCode == Vst_TaxSubTypeCode && a.OurCompanyRef == LastCompId).ToList();
                                if (ListTax.Count == 0)
                                {
                                    DNtMDB.Tbl_TaxOurCompany.Add(TOrCmp);

                                }

                            }

                            DNtMDB.SaveChanges();
                        }
                    }
                    if (UniteTDG.RowCount > 0)
                    {
                        string Vst_UniteCode = "";
                        int LastCompId = 0;
                        LastCompId = ListOurCompany.ID;
                        foreach (DataGridViewRow row in UniteTDG.Rows)
                        {
                            Vst_UniteCode = row.Cells[1].Value.ToString();
                            var listUnit = DNtMDB.Tbl_OurCompanyUnite.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.UniteCode == Vst_UniteCode).ToList();
                            if (listUnit.Count > 0)
                            {
                                listUnit[0].UniteCode = Vst_UniteCode;
                            }
                            else
                            {
                                Tbl_OurCompanyUnite UOrCmp = new Tbl_OurCompanyUnite()
                                {
                                    UniteCode = Vst_UniteCode,
                                    OurCompanyRef = LastCompId,
                                };
                                var ListUnite = DNtMDB.Tbl_OurCompanyUnite.Where(a => a.UniteCode == Vst_UniteCode).ToList();
                                if (ListUnite.Count == 0)
                                {
                                    DNtMDB.Tbl_OurCompanyUnite.Add(UOrCmp);
                                    DNtMDB.SaveChanges();
                                }
                            }

                            DNtMDB.SaveChanges();
                        }
                        if (CurrencyDG.RowCount > 0)
                        {
                            string Vst_CurrencyCode = "";
                            int LastCompId1 = 0;
                            LastCompId1 = ListOurCompany.ID;
                            foreach (DataGridViewRow row in CurrencyDG.Rows)
                            {
                                Vst_CurrencyCode = row.Cells[1].Value.ToString();
                                var listCurrency = DNtMDB.Tbl_OurCompanyCurrency.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.CurrencyCode == Vst_CurrencyCode).ToList();
                                if (listCurrency.Count > 0)
                                {
                                    listCurrency[0].CurrencyCode = Vst_CurrencyCode;
                                }
                                else
                                {
                                    Tbl_OurCompanyCurrency CurOrCmp = new Tbl_OurCompanyCurrency()
                                    {
                                        CurrencyCode = Vst_CurrencyCode,
                                        OurCompanyRef = LastCompId1,
                                    };
                                    var ListCurrency = DNtMDB.Tbl_OurCompanyCurrency.Where(a => a.CurrencyCode == Vst_CurrencyCode).ToList();
                                    if (ListCurrency.Count == 0)
                                    {
                                        DNtMDB.Tbl_OurCompanyCurrency.Add(CurOrCmp);
                                        DNtMDB.SaveChanges();
                                    }
                                }

                                DNtMDB.SaveChanges();
                            }
                        }
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Our Company Activity",
                            TableName = "Tbl_OurCompActivity",
                            TableRecordId = ListOurCompany.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "OurCompanyFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        MessageBox.Show("Updated");
                    }
                }
                else if (LangTxtBox.Text == "ar-EG")
                {
                    var ListOurCompany1 = DNtMDB.Tbl_OurCompany.FirstOrDefault(x => x.ID == 1);
                    if (EnNametxt.Text == "")
                    {
                        MessageBox.Show("من فضلك ادخل اسم الشركه الانجليزي");
                        EnNametxt.Focus();
                    }
                    else if (ArNametxt.Text == "")
                    {
                        MessageBox.Show("من فضلك ادخل اسم الشركه العربي");
                        ArNametxt.Focus();
                    }
                    else if (taxRgtxt.Text == "")
                    {
                        MessageBox.Show("من فضلك ادخل اسم رقم التسجيل الضريبي للشركه");
                        taxRgtxt.Focus();
                    }
                    else if (CountryCmb.SelectedIndex == -1)
                    {
                        MessageBox.Show("من فضلك ادخل الدوله التابع لها للشركه");
                        CountryCmb.Focus();
                    }
                    else if (GovernorateCmb.SelectedIndex == -1)
                    {
                        MessageBox.Show("من فضلك ادخل المحافظه");
                        GovernorateCmb.Focus();
                    }
                    else if (CityCmb.SelectedIndex == -1)
                    {
                        MessageBox.Show("من فضلك ادخل المدينه  ");
                        CityCmb.Focus();
                    }

                    else if (ListOurCompany1 == null)

                    {
                        Tbl_OurCompany OrCmp = new Tbl_OurCompany()
                        {
                            Name_E = EnNametxt.Text,
                            Name_Ar = ArNametxt.Text,
                            TaxRegisterNo = taxRgtxt.Text,
                            CountryCode = CountryCmb.SelectedValue.ToString(),
                            GovernorateID = int.Parse(GovernorateCmb.SelectedValue.ToString()),
                            CityID = int.Parse(CityCmb.SelectedValue.ToString()),
                            Street = Streettxt.Text,
                            BuildingNumber = BldNoTxt.Text,
                            Floor = FloorTxt.Text,
                            room = RoomTxt.Text,
                            PostalCode = PostalCodeTxt.Text,
                            CompanyType = CompanyTypeCmb.SelectedValue.ToString(),
                        };
                        DNtMDB.Tbl_OurCompany.Add(OrCmp);
                        if (ActivityDG.RowCount > 0)
                        {
                            string Vst_ActiveCode = "";
                            int LastCompId = 0;
                            LastCompId = OrCmp.ID;
                            foreach (DataGridViewRow row in ActivityDG.Rows)
                            {
                                Vst_ActiveCode = row.Cells[1].Value.ToString();
                                Tbl_OurCompActivity AOrCmp = new Tbl_OurCompActivity()
                                {
                                    ActivityCode = Vst_ActiveCode,
                                    OurCompanyRef = LastCompId,
                                };
                                var ListActivity = DNtMDB.Tbl_OurCompActivity.Where(a => a.ActivityCode == Vst_ActiveCode).ToList();
                                if (ListActivity.Count == 0)
                                {
                                    DNtMDB.Tbl_OurCompActivity.Add(AOrCmp);
                                    DNtMDB.SaveChanges();
                                }

                            }
                        }
                        if (TaxTybDG.RowCount > 0)
                        {
                            string Vst_TaxTypeCode = "";
                            string Vst_TaxTypeName = "";
                            string Vst_TaxSubTypeCode = "";
                            string Vst_TaxSubTypeName = "";
                            int LastCompId = 0;
                            LastCompId = OrCmp.ID;
                            foreach (DataGridViewRow row in TaxTybDG.Rows)
                            {
                                Vst_TaxTypeCode = row.Cells[1].Value.ToString();
                                Vst_TaxTypeName = row.Cells[2].Value.ToString();
                                Vst_TaxSubTypeCode = row.Cells[3].Value.ToString();
                                Vst_TaxSubTypeName = row.Cells[4].Value.ToString();
                                Tbl_TaxOurCompany TOrCmp = new Tbl_TaxOurCompany()
                                {
                                    TaxTypeCode = Vst_TaxTypeCode,
                                    TaxSubTypeCode = Vst_TaxSubTypeCode,
                                    OurCompanyRef = LastCompId,
                                };
                                var ListTax = DNtMDB.Tbl_TaxOurCompany.Where(a => a.TaxTypeCode == Vst_TaxTypeCode && a.TaxSubTypeCode == Vst_TaxSubTypeCode && a.OurCompanyRef == LastCompId).ToList();
                                if (ListTax.Count == 0)
                                {
                                    DNtMDB.Tbl_TaxOurCompany.Add(TOrCmp);
                                    DNtMDB.SaveChanges();
                                }
                            }
                        }
                        if (UniteTDG.RowCount > 0)
                        {
                            string Vst_UniteCode = "";
                            int LastCompId = 0;
                            LastCompId = OrCmp.ID;
                            foreach (DataGridViewRow row in UniteTDG.Rows)
                            {
                                Vst_UniteCode = row.Cells[1].Value.ToString();

                                Tbl_OurCompanyUnite UOrCmp = new Tbl_OurCompanyUnite()
                                {
                                    UniteCode = Vst_UniteCode,
                                    OurCompanyRef = LastCompId,
                                };
                                var ListActivity = DNtMDB.Tbl_OurCompanyUnite.Where(a => a.UniteCode == Vst_UniteCode).ToList();
                                if (ListActivity.Count == 0)
                                {
                                    DNtMDB.Tbl_OurCompanyUnite.Add(UOrCmp);
                                    DNtMDB.SaveChanges();
                                }
                            }
                        }
                        if (CurrencyDG.RowCount > 0)
                        {
                            string Vst_CurrencyCode = "";
                            int LastCompId = 0;
                            LastCompId = OrCmp.ID;
                            foreach (DataGridViewRow row in CurrencyDG.Rows)
                            {
                                Vst_CurrencyCode = row.Cells[1].Value.ToString();

                                Tbl_OurCompanyCurrency COrCmp = new Tbl_OurCompanyCurrency()
                                {
                                    CurrencyCode = Vst_CurrencyCode,
                                    OurCompanyRef = LastCompId,
                                };
                                var ListCurrency = DNtMDB.Tbl_OurCompanyCurrency.Where(a => a.CurrencyCode == Vst_CurrencyCode).ToList();
                                if (ListCurrency.Count == 0)
                                {
                                    DNtMDB.Tbl_OurCompanyCurrency.Add(COrCmp);
                                    DNtMDB.SaveChanges();
                                }
                            }
                        }
                        DNtMDB.SaveChanges();
                        MessageBox.Show("تم الحفظ");
                    }
                    else if (ListOurCompany != null)
                    {
                        ListOurCompany.Name_E = EnNametxt.Text;
                        ListOurCompany.Name_Ar = ArNametxt.Text;
                        ListOurCompany.TaxRegisterNo = taxRgtxt.Text;
                        ListOurCompany.CountryCode = CountryCmb.SelectedValue.ToString();
                        ListOurCompany.GovernorateID = int.Parse(GovernorateCmb.SelectedValue.ToString());
                        ListOurCompany.CityID = int.Parse(CityCmb.SelectedValue.ToString());
                        ListOurCompany.Street = Streettxt.Text;
                        ListOurCompany.BuildingNumber = BldNoTxt.Text;
                        ListOurCompany.Floor = FloorTxt.Text;
                        ListOurCompany.room = RoomTxt.Text;
                        ListOurCompany.PostalCode = PostalCodeTxt.Text;
                        ListOurCompany.CompanyType = CompanyTypeCmb.SelectedValue.ToString();
                        if (ActivityDG.RowCount > 0)
                        {
                            string Vst_ActiveCode = "";
                            int LastCompId = 0;
                            LastCompId = ListOurCompany.ID;
                            foreach (DataGridViewRow row in ActivityDG.Rows)
                            {
                                Vst_ActiveCode = row.Cells[1].Value.ToString();
                                var listActiv = DNtMDB.Tbl_OurCompActivity.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.ActivityCode == Vst_ActiveCode).ToList();
                                if (listActiv.Count > 0)
                                {
                                    listActiv[0].ActivityCode = Vst_ActiveCode;
                                }
                                else
                                {
                                    Tbl_OurCompActivity AOrCmp = new Tbl_OurCompActivity()
                                    {
                                        ActivityCode = Vst_ActiveCode,
                                        OurCompanyRef = LastCompId,
                                    };

                                    var ListActivity = DNtMDB.Tbl_OurCompActivity.Where(a => a.ActivityCode == Vst_ActiveCode).ToList();
                                    if (ListActivity.Count == 0)
                                    {
                                        DNtMDB.Tbl_OurCompActivity.Add(AOrCmp);
                                        DNtMDB.SaveChanges();
                                    }
                                }

                                DNtMDB.SaveChanges();
                            }
                        }
                        if (TaxTybDG.RowCount > 0)
                        {
                            string Vst_TaxTypeCode = "";
                            string Vst_TaxTypeName = "";
                            string Vst_TaxSubTypeCode = "";
                            string Vst_TaxSubTypeName = "";
                            int LastCompId = 0;

                            LastCompId = ListOurCompany.ID;
                            foreach (DataGridViewRow row in TaxTybDG.Rows)
                            {
                                Vst_TaxTypeCode = row.Cells[1].Value.ToString();
                                Vst_TaxTypeName = row.Cells[2].Value.ToString();
                                Vst_TaxSubTypeCode = row.Cells[3].Value.ToString();
                                Vst_TaxSubTypeName = row.Cells[4].Value.ToString();

                                var listTax = DNtMDB.Tbl_TaxOurCompany.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.TaxTypeCode == Vst_TaxTypeCode && x.TaxTypeCode == Vst_TaxSubTypeCode).ToList();
                                if (listTax.Count > 0)
                                {
                                    listTax[0].TaxTypeCode = Vst_TaxTypeCode;
                                    listTax[0].TaxSubTypeCode = Vst_TaxSubTypeCode;
                                }
                                else
                                {
                                    Tbl_TaxOurCompany TOrCmp = new Tbl_TaxOurCompany()
                                    {
                                        TaxTypeCode = Vst_TaxTypeCode,
                                        TaxSubTypeCode = Vst_TaxSubTypeCode,
                                        OurCompanyRef = ListOurCompany.ID,
                                    };
                                    var ListTax = DNtMDB.Tbl_TaxOurCompany.Where(a => a.TaxTypeCode == Vst_TaxTypeCode && a.TaxSubTypeCode == Vst_TaxSubTypeCode && a.OurCompanyRef == LastCompId).ToList();
                                    if (ListTax.Count == 0)
                                    {
                                        DNtMDB.Tbl_TaxOurCompany.Add(TOrCmp);

                                    }

                                }

                                DNtMDB.SaveChanges();
                            }
                        }
                        if (UniteTDG.RowCount > 0)
                        {
                            string Vst_UniteCode = "";
                            int LastCompId = 0;
                            LastCompId = ListOurCompany.ID;
                            foreach (DataGridViewRow row in UniteTDG.Rows)
                            {
                                Vst_UniteCode = row.Cells[1].Value.ToString();
                                var listUnit = DNtMDB.Tbl_OurCompanyUnite.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.UniteCode == Vst_UniteCode).ToList();
                                if (listUnit.Count > 0)
                                {
                                    listUnit[0].UniteCode = Vst_UniteCode;
                                }
                                else
                                {
                                    Tbl_OurCompanyUnite UOrCmp = new Tbl_OurCompanyUnite()
                                    {
                                        UniteCode = Vst_UniteCode,
                                        OurCompanyRef = LastCompId,
                                    };
                                    var ListUnite = DNtMDB.Tbl_OurCompanyUnite.Where(a => a.UniteCode == Vst_UniteCode).ToList();
                                    if (ListUnite.Count == 0)
                                    {
                                        DNtMDB.Tbl_OurCompanyUnite.Add(UOrCmp);
                                        DNtMDB.SaveChanges();
                                    }
                                }

                                DNtMDB.SaveChanges();
                            }
                        }
                        if (CurrencyDG.RowCount > 0)
                        {
                            string Vst_CurrencyCode = "";
                            int LastCompId1 = 0;
                            LastCompId1 = ListOurCompany.ID;
                            foreach (DataGridViewRow row in CurrencyDG.Rows)
                            {
                                Vst_CurrencyCode = row.Cells[1].Value.ToString();
                                var listCurrency = DNtMDB.Tbl_OurCompanyCurrency.Where(x => x.OurCompanyRef == ListOurCompany.ID && x.CurrencyCode == Vst_CurrencyCode).ToList();
                                if (listCurrency.Count > 0)
                                {
                                    listCurrency[0].CurrencyCode = Vst_CurrencyCode;
                                }
                                else
                                {
                                    Tbl_OurCompanyCurrency CurOrCmp = new Tbl_OurCompanyCurrency()
                                    {
                                        CurrencyCode = Vst_CurrencyCode,
                                        OurCompanyRef = LastCompId1,
                                    };
                                    var ListCurrency = DNtMDB.Tbl_OurCompanyCurrency.Where(a => a.CurrencyCode == Vst_CurrencyCode).ToList();
                                    if (ListCurrency.Count == 0)
                                    {
                                        DNtMDB.Tbl_OurCompanyCurrency.Add(CurOrCmp);
                                        DNtMDB.SaveChanges();
                                    }
                                }

                                DNtMDB.SaveChanges();
                            }
                        }
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Our Company Activity",
                            TableName = "Tbl_OurCompActivity",
                            TableRecordId = ListOurCompany.ID.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "OurCompanyFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        MessageBox.Show("Updated");


                        DNtMDB.SaveChanges();
                        MessageBox.Show("تم التعديل");
                    }
                }


            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            LangTxtBox.Text = "en";


            CountryCmb.DataSource = DNtMDB.Tbl_Country.ToList();
            CountryCmb.DisplayMember = "dec_en";
            CountryCmb.ValueMember = "code";
            CountryCmb.Text = "Choose a Country";

            GovernorateCmb.DataSource = DNtMDB.Tbl_Governerate.ToList();
            GovernorateCmb.DisplayMember = "Name_Ar";
            GovernorateCmb.ValueMember = "ID";
            GovernorateCmb.Text = "Choose a Governorate";

            ActivityTypeCmb.DataSource = DNtMDB.Tbl_Activities.OrderBy(x => x.Activecode).ToList();
            ActivityTypeCmb.DisplayMember = "Activecode";
            ActivityTypeCmb.ValueMember = "Activecode";
            ActivityTypeCmb.Text = "Choose a Active Code";

            TaxTypeCmb.DataSource = DNtMDB.Tbl_TaxType.ToList();
            TaxTypeCmb.DisplayMember = "Des_En";
            TaxTypeCmb.ValueMember = "code";
            TaxTypeCmb.Text = "Choose a Tax Type";

            CompanyTypeCmb.DataSource = DNtMDB.Tbl_CompanyType.ToList();
            CompanyTypeCmb.DisplayMember = "Name_E";
            CompanyTypeCmb.ValueMember = "code";
            CompanyTypeCmb.Text = "Choose a Company Type";

            cmbUniteType.DataSource = DNtMDB.Tbl_UniteType.OrderBy(x=>x.Desc_En).ToList();
            cmbUniteType.DisplayMember = "Desc_En";
            cmbUniteType.ValueMember = "code";
            cmbUniteType.Text = "Choose a Unite Type";

            CmbCurrency.DataSource = DNtMDB.Tbl_Currency.ToList();
            CmbCurrency.DisplayMember = "dec_en";
            CmbCurrency.ValueMember = "code";
            CmbCurrency.Text = "Choose a Currency";


            var ListOurCompany = DNtMDB.Tbl_OurCompany.ToList();
            if (ListOurCompany.Count == 1)
            {
                var listCity = DNtMDB.Tbl_Cities.ToList();
                CityCmb.DataSource = listCity;
                CityCmb.DisplayMember = "Name_Ar";
                CityCmb.ValueMember = "ID";

                EnNametxt.Text = ListOurCompany[0].Name_E.ToString();
                ArNametxt.Text = ListOurCompany[0].Name_Ar.ToString();
                taxRgtxt.Text = ListOurCompany[0].TaxRegisterNo.ToString();

                CountryCmb.SelectedValue = ListOurCompany[0].CountryCode.ToString();

                int Vin_Governorate = int.Parse(ListOurCompany[0].GovernorateID.ToString());
                string vst_Governorate = DNtMDB.Tbl_Governerate.FirstOrDefault(x => x.ID == Vin_Governorate).Name_Ar.ToString();
                GovernorateCmb.SelectedValue = Vin_Governorate;
                GovernorateCmb.Text = vst_Governorate;
                int Vin_City = int.Parse(ListOurCompany[0].CityID.ToString());
                CityCmb.SelectedValue = Vin_City;
                Streettxt.Text = ListOurCompany[0].Street.ToString();
                BldNoTxt.Text = ListOurCompany[0].BuildingNumber.ToString();
                FloorTxt.Text = ListOurCompany[0].Floor.ToString();
                RoomTxt.Text = ListOurCompany[0].room.ToString();
                PostalCodeTxt.Text = ListOurCompany[0].PostalCode.ToString();
                string Vst_CompanyTypeCode = ListOurCompany[0].CompanyType.ToString();
                string vst_CompanyType = DNtMDB.Tbl_CompanyType.FirstOrDefault(x => x.Code == Vst_CompanyTypeCode).Name_E.ToString();
                CompanyTypeCmb.SelectedValue = ListOurCompany[0].CompanyType.ToString();
                CompanyTypeCmb.Text = vst_CompanyType;
                int OurCompId = int.Parse(ListOurCompany[0].ID.ToString());
                var ListCompActivity = DNtMDB.Tbl_OurCompActivity.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListCompActivity.Count > 0)
                {
                    AddColumnDtaGrdActive();
                    string Vst_ActivityCode = ListCompActivity[0].ActivityCode.ToString();
                    var VAcName = DNtMDB.Tbl_Activities.Where(x => x.Activecode == Vst_ActivityCode).ToList();
                    string Vst_ActivityName = VAcName[0].dec_en;
                    ActivityDG.AllowUserToAddRows = true;
                    ActivityDG.Rows.Add(0, Vst_ActivityCode, Vst_ActivityName);
                    ActivityDG.AllowUserToAddRows = false;
                    GrdCheckActive();
                }
                var ListTaxComp = DNtMDB.Tbl_TaxOurCompany.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListTaxComp.Count > 0)
                {
                    AddColumnDtaGrdTax();

                    string Vst_TaxTypeCode = ListTaxComp[0].TaxTypeCode.ToString();
                    var VTxName = DNtMDB.Tbl_TaxType.Where(x => x.code == Vst_TaxTypeCode).ToList();
                    string Vst_TaxTypeName = VTxName[0].Des_En.ToString();
                    string Vst_TaxSubTypeCode = ListTaxComp[0].TaxSubTypeCode.ToString();
                    var VTxSubName = DNtMDB.Tbl_TaxSubType.Where(x => x.Code == Vst_TaxSubTypeCode).ToList();
                    string Vst_TaxSubTypeName = VTxSubName[0].Desc_En.ToString();
                    int Vint_ID = int.Parse(ListTaxComp[0].ID.ToString());
                    TaxTybDG.AllowUserToAddRows = true;
                    TaxTybDG.Rows.Add(Vint_ID, Vst_TaxTypeCode, Vst_TaxTypeName, Vst_TaxSubTypeCode, Vst_TaxSubTypeName);
                    TaxTybDG.AllowUserToAddRows = false;
                    GrdCheckTax();

                }
                var ListCompUnit = DNtMDB.Tbl_OurCompanyUnite.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListCompUnit.Count > 0)
                {
                    AddColumnDtaGrdUnit();
                    string Vst_UniteCode = ListCompUnit[0].UniteCode.ToString();
                    var VUnName = DNtMDB.Tbl_UniteType.Where(x => x.Code == Vst_UniteCode).ToList();
                    string Vst_UniteName = VUnName[0].Desc_Ar;
                    string Vst_UniteNameE = VUnName[0].Desc_En;
                    UniteTDG.AllowUserToAddRows = true;
                    UniteTDG.Rows.Add(0, Vst_UniteCode, Vst_UniteNameE, Vst_UniteName);
                    UniteTDG.AllowUserToAddRows = false;
                    GrdCheckUnite();
                }
                var ListCompCurr = DNtMDB.Tbl_OurCompanyCurrency.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListCompCurr.Count > 0)
                {
                    AddColumnDtaGrdCurrency();
                    string Vst_CurrencyCode = ListCompCurr[0].CurrencyCode.ToString();
                    var VCurrName = DNtMDB.Tbl_Currency.Where(x => x.code == Vst_CurrencyCode).ToList();
                    string Vst_CurrencyName = VCurrName[0].dec_en;
                    CurrencyDG.AllowUserToAddRows = true;
                    CurrencyDG.Rows.Add(0, Vst_CurrencyCode, Vst_CurrencyName);
                    CurrencyDG.AllowUserToAddRows = false;
                    GrdCheckCurrency();
                }
            }
            EnNametxt.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            LangTxtBox.Text = "ar-EG";



            CountryCmb.DataSource = DNtMDB.Tbl_Country.ToList();
            CountryCmb.DisplayMember = "dec_ar";
            CountryCmb.ValueMember = "code";
            CountryCmb.Text = "اختر الدوله";

            GovernorateCmb.DataSource = DNtMDB.Tbl_Governerate.ToList();
            GovernorateCmb.DisplayMember = "Name_E";
            GovernorateCmb.ValueMember = "ID";
            GovernorateCmb.Text = "اختر المحافظه";

            ActivityTypeCmb.DataSource = DNtMDB.Tbl_Activities.OrderBy(x => x.Activecode).ToList();
            ActivityTypeCmb.DisplayMember = "Activecode";
            ActivityTypeCmb.ValueMember = "Activecode";
            ActivityTypeCmb.Text = "اختر كود النشاط";


            TaxTypeCmb.DataSource = DNtMDB.Tbl_TaxType.ToList();
            TaxTypeCmb.DisplayMember = "Des_Ar";
            TaxTypeCmb.ValueMember = "code";
            TaxTypeCmb.Text = "اختر الضريبه";

            CompanyTypeCmb.DataSource = DNtMDB.Tbl_CompanyType.ToList();
            CompanyTypeCmb.DisplayMember = "Name_Ar";
            CompanyTypeCmb.ValueMember = "code";
            CompanyTypeCmb.Text = "اختر طبيعة النشاط";

            cmbUniteType.DataSource = DNtMDB.Tbl_UniteType.OrderBy(x => x.Desc_Ar).ToList();
            cmbUniteType.DisplayMember = "Desc_Ar";
            cmbUniteType.ValueMember = "code";
            cmbUniteType.Text = "اختر الوحده";

            CmbCurrency.DataSource = DNtMDB.Tbl_Currency.ToList();
            CmbCurrency.DisplayMember = "Desc_Ar";
            CmbCurrency.ValueMember = "code";
            CmbCurrency.Text = "اختر العمله";

            var ListOurCompany = DNtMDB.Tbl_OurCompany.ToList();
            if (ListOurCompany.Count == 1)
            {
                var listCity = DNtMDB.Tbl_Cities.ToList();
                CityCmb.DataSource = listCity;
                CityCmb.DisplayMember = "Name_E";
                CityCmb.ValueMember = "ID";


                EnNametxt.Text = ListOurCompany[0].Name_E.ToString();
                ArNametxt.Text = ListOurCompany[0].Name_Ar.ToString();
                taxRgtxt.Text = ListOurCompany[0].TaxRegisterNo.ToString();

                CountryCmb.SelectedValue = ListOurCompany[0].CountryCode.ToString();
                int Vint_Governorate = int.Parse(ListOurCompany[0].GovernorateID.ToString());
                string vst_Governorate = DNtMDB.Tbl_Governerate.FirstOrDefault(x => x.ID == Vint_Governorate).Name_E.ToString();
                GovernorateCmb.SelectedValue = Vint_Governorate;
                GovernorateCmb.Text = vst_Governorate;
                int Vin_City = int.Parse(ListOurCompany[0].CityID.ToString());
                CityCmb.SelectedValue = Vin_City;
                Streettxt.Text = ListOurCompany[0].Street.ToString();
                BldNoTxt.Text = ListOurCompany[0].BuildingNumber.ToString();
                FloorTxt.Text = ListOurCompany[0].Floor.ToString();
                RoomTxt.Text = ListOurCompany[0].room.ToString();
                PostalCodeTxt.Text = ListOurCompany[0].PostalCode.ToString();
                string Vst_CompanyTypeCode = ListOurCompany[0].CompanyType.ToString();
                string vst_CompanyType = DNtMDB.Tbl_CompanyType.FirstOrDefault(x => x.Code == Vst_CompanyTypeCode).Name_Ar.ToString();
                CompanyTypeCmb.SelectedValue = ListOurCompany[0].CompanyType.ToString();
                CompanyTypeCmb.Text = vst_CompanyType;
                int OurCompId = int.Parse(ListOurCompany[0].ID.ToString());
                var ListCompActivity = DNtMDB.Tbl_OurCompActivity.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListCompActivity.Count > 0)
                {
                    AddColumnDtaGrdActive();
                    string Vst_ActivityCode = ListCompActivity[0].ActivityCode.ToString();
                    var VAcName = DNtMDB.Tbl_Activities.Where(x => x.Activecode == Vst_ActivityCode).ToList();
                    string Vst_ActivityName = VAcName[0].dec_ar;
                    ActivityDG.AllowUserToAddRows = true;
                    ActivityDG.Rows.Add(0, Vst_ActivityCode, Vst_ActivityName);
                    ActivityDG.AllowUserToAddRows = false;
                    GrdCheckActive();
                }
                var ListTaxComp = DNtMDB.Tbl_TaxOurCompany.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListTaxComp.Count > 0)
                {
                    AddColumnDtaGrdTax();

                    string Vst_TaxTypeCode = ListTaxComp[0].TaxTypeCode.ToString();
                    var VTxName = DNtMDB.Tbl_TaxType.Where(x => x.code == Vst_TaxTypeCode).ToList();
                    string Vst_TaxTypeName = VTxName[0].Des_Ar.ToString();
                    string Vst_TaxSubTypeCode = ListTaxComp[0].TaxSubTypeCode.ToString();
                    var VTxSubName = DNtMDB.Tbl_TaxSubType.Where(x => x.Code == Vst_TaxSubTypeCode).ToList();
                    string Vst_TaxSubTypeName = VTxSubName[0].Desc_Ar.ToString();
                    int Vint_ID = int.Parse(ListTaxComp[0].ID.ToString());
                    TaxTybDG.AllowUserToAddRows = true;
                    TaxTybDG.Rows.Add(Vint_ID, Vst_TaxTypeCode, Vst_TaxTypeName, Vst_TaxSubTypeCode, Vst_TaxSubTypeName);
                    TaxTybDG.AllowUserToAddRows = false;
                    GrdCheckTax();

                }
                var ListCompUnit = DNtMDB.Tbl_OurCompanyUnite.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListCompUnit.Count > 0)
                {
                    AddColumnDtaGrdUnit();
                    string Vst_UniteCode = ListCompUnit[0].UniteCode.ToString();
                    var VUnName = DNtMDB.Tbl_UniteType.Where(x => x.Code == Vst_UniteCode).ToList();
                    string Vst_UniteName = VUnName[0].Desc_Ar;
                    string Vst_UniteNameE = VUnName[0].Desc_En;
                    UniteTDG.AllowUserToAddRows = true;
                    UniteTDG.Rows.Add(0, Vst_UniteCode, Vst_UniteNameE, Vst_UniteName);
                    UniteTDG.AllowUserToAddRows = false;
                    GrdCheckUnite();
                }
                var ListCompCurr = DNtMDB.Tbl_OurCompanyCurrency.Where(x => x.OurCompanyRef == OurCompId).ToList();
                if (ListCompCurr.Count > 0)
                {
                    AddColumnDtaGrdCurrency();
                    string Vst_CurrencyCode = ListCompCurr[0].CurrencyCode.ToString();
                    var VCurrName = DNtMDB.Tbl_Currency.Where(x => x.code == Vst_CurrencyCode).ToList();
                    string Vst_CurrencyName = VCurrName[0].dec_en;
                    CurrencyDG.AllowUserToAddRows = true;
                    CurrencyDG.Rows.Add(0, Vst_CurrencyCode, Vst_CurrencyName);
                    CurrencyDG.AllowUserToAddRows = false;
                    GrdCheckCurrency();
                }
            }
            EnNametxt.Focus();
        }



        private void GovernorateCmb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en" && Program.GlbV_Language == "en")
            {
                Vint_codGvrn = int.Parse(GovernorateCmb.SelectedValue.ToString());
                CityCmb.DataSource = DNtMDB.Tbl_Cities.Where(x => x.GovernorateID == Vint_codGvrn).ToList();
                CityCmb.DisplayMember = "Name_Ar";
                CityCmb.ValueMember = "ID";
                CityCmb.Text = "Choose a Governorate";
            }
            else if (LangTxtBox.Text == "ar-EG" && Program.GlbV_Language == "ar-EG")
            {
                Vint_codGvrn = int.Parse(GovernorateCmb.SelectedValue.ToString());
                CityCmb.DataSource = DNtMDB.Tbl_Cities.Where(x => x.GovernorateID == Vint_codGvrn).ToList();
                CityCmb.DisplayMember = "Name_E";
                CityCmb.ValueMember = "ID";
                CityCmb.Text = "اختر المدينه";
            }

        }



        private void TaxTypeCmb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en" && Program.GlbV_Language == "en")
            {
                Vst_Taxtype = TaxTypeCmb.SelectedValue.ToString();
                TaxSubTypeCmb.DataSource = DNtMDB.Tbl_TaxSubType.Where(x => x.TaxTypeRefrence == Vst_Taxtype).OrderBy(x => x.Code).ToList();
                TaxSubTypeCmb.DisplayMember = "Desc_En";
                TaxSubTypeCmb.ValueMember = "Code";
                TaxSubTypeCmb.Text = "Choose a Tax Sub Type";
            }
            else if (LangTxtBox.Text == "ar-EG" && Program.GlbV_Language == "ar-EG")
            {
                Vst_Taxtype = TaxTypeCmb.SelectedValue.ToString();
                TaxSubTypeCmb.DataSource = DNtMDB.Tbl_TaxSubType.Where(x => x.TaxTypeRefrence == Vst_Taxtype).OrderBy(x => x.Code).ToList();
                TaxSubTypeCmb.DisplayMember = "Desc_Ar";
                TaxSubTypeCmb.ValueMember = "Code";
                TaxSubTypeCmb.Text = "اختر الضريبه الفرعيه";
            }
        }

        private void CompanyTypeCmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CountryCmb.Focus();
            }
        }
        private void AddColumnDtaGrdActive()
        {
            DataGridViewTextBoxColumn newColumnID = new DataGridViewTextBoxColumn();
            newColumnID.HeaderText = "ID"; // Header text of the column
            newColumnID.Name = "ID"; // Unique name of the column
            ActivityDG.Columns.Add(newColumnID);

            DataGridViewTextBoxColumn newColumnCode = new DataGridViewTextBoxColumn();
            newColumnCode.HeaderText = "ActivityCode"; // Header text of the column
            newColumnCode.Name = "ActivityCode"; // Unique name of the column
            ActivityDG.Columns.Add(newColumnCode);

            DataGridViewTextBoxColumn newColumnName = new DataGridViewTextBoxColumn();
            newColumnName.HeaderText = "ActivityName"; // Header text of the column
            newColumnName.Name = "ActivityName"; // Unique name of the column
            ActivityDG.Columns.Add(newColumnName);

        }
        private void GrdCheckActive()
        {
            if (Program.GlbV_Language == "en" && LangTxtBox.Text == "en")
            {
                ActivityDG.Columns["ID"].Visible = false;
                ActivityDG.Columns["ActivityCode"].HeaderText = "Activity Code";
                ActivityDG.Columns["ActivityName"].HeaderText = "Activity Name";
                ActivityDG.Columns["ActivityName"].Width = 150;
            }
            else if (Program.GlbV_Language == "ar-EG" && LangTxtBox.Text == "ar-EG")
            {
                ActivityDG.Columns["ID"].Visible = false;
                ActivityDG.Columns["ActivityCode"].HeaderText = "كود النشاط";
                ActivityDG.Columns["ActivityName"].HeaderText = "النشاط";
                ActivityDG.Columns["ActivityName"].Width = 150;
            }
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (ActivityTypeCmb.SelectedIndex >= 0)
            {
                if (ActivityDG.Rows.Count == 0)
                {
                    AddColumnDtaGrdActive();
                }
                string Vst_ActivityCode = ActivityTypeCmb.SelectedValue.ToString();

                string Vst_ActivityName = DNtMDB.Tbl_Activities.FirstOrDefault(x => x.Activecode == Vst_ActivityCode).dec_en.ToString();
                ActivityDG.AllowUserToAddRows = true;
                ActivityDG.Rows.Add(0, Vst_ActivityCode, Vst_ActivityName);
                ActivityDG.AllowUserToAddRows = false;
                GrdCheckActive();
            }
        }
        private void AddColumnDtaGrdTax()
        {
            DataGridViewTextBoxColumn newColumnID = new DataGridViewTextBoxColumn();
            newColumnID.HeaderText = "ID"; // Header text of the column
            newColumnID.Name = "ID"; // Unique name of the column
            TaxTybDG.Columns.Add(newColumnID);

            DataGridViewTextBoxColumn newColumnTCode = new DataGridViewTextBoxColumn();
            newColumnTCode.HeaderText = "TaxTypeCode"; // Header text of the column
            newColumnTCode.Name = "TaxTypeCode"; // Unique name of the column
            TaxTybDG.Columns.Add(newColumnTCode);

            DataGridViewTextBoxColumn newColumnTName = new DataGridViewTextBoxColumn();
            newColumnTName.HeaderText = "TaxTypeName"; // Header text of the column
            newColumnTName.Name = "TaxTypeName"; // Unique name of the column
            TaxTybDG.Columns.Add(newColumnTName);

            DataGridViewTextBoxColumn newColumnTSCode = new DataGridViewTextBoxColumn();
            newColumnTSCode.HeaderText = "TaxSubTypeCode"; // Header text of the column
            newColumnTSCode.Name = "TaxSubTypeCode"; // Unique name of the column
            TaxTybDG.Columns.Add(newColumnTSCode);

            DataGridViewTextBoxColumn newColumnTSName = new DataGridViewTextBoxColumn();
            newColumnTSName.HeaderText = "TaxSubTypeName"; // Header text of the column
            newColumnTSName.Name = "TaxSubTypeName"; // Unique name of the column
            TaxTybDG.Columns.Add(newColumnTSName);

        }
        private void GrdCheckTax()
        {
            TaxTybDG.Columns["ID"].Visible = false;
            if (Program.GlbV_Language == "en" && LangTxtBox.Text == "en")
            {
                TaxTybDG.Columns["TaxTypeCode"].HeaderText = "Tax Type Code";
                TaxTybDG.Columns["TaxTypeName"].HeaderText = "Tax Type Name";
                TaxTybDG.Columns["TaxTypeName"].Width = 150;

                TaxTybDG.Columns["TaxSubTypeCode"].HeaderText = "Tax Sub Type Code";
                TaxTybDG.Columns["TaxSubTypeCode"].Width = 150;
                TaxTybDG.Columns["TaxSubTypeName"].HeaderText = "Tax Sub Type Name";
                TaxTybDG.Columns["TaxSubTypeName"].Width = 150;
            }
            else if (Program.GlbV_Language == "ar-EG" && LangTxtBox.Text == "ar-EG")
            {
                TaxTybDG.Columns["TaxTypeCode"].HeaderText = " كود الضريبه";
                TaxTybDG.Columns["TaxTypeName"].HeaderText = "الضريبه";
                TaxTybDG.Columns["TaxTypeName"].Width = 150;

                TaxTybDG.Columns["TaxSubTypeCode"].HeaderText = "كود الضريبه الفرعي";
                TaxTybDG.Columns["TaxSubTypeCode"].Width = 150;
                TaxTybDG.Columns["TaxSubTypeName"].HeaderText = "الضريبه الفرعيه";
                TaxTybDG.Columns["TaxSubTypeName"].Width = 150;
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (TaxTypeCmb.SelectedIndex >= 0 && TaxSubTypeCmb.SelectedIndex >= 0)
            {
                if (TaxTybDG.Rows.Count == 0)
                {
                    AddColumnDtaGrdTax();
                }
                string Vst_TaxTypeCode = TaxTypeCmb.SelectedValue.ToString();
                string Vst_TaxTypeName = TaxTypeCmb.Text.ToString();
                string Vst_TaxSubTypeCode = TaxSubTypeCmb.SelectedValue.ToString();
                string Vst_TaxSubTypeName = TaxSubTypeCmb.Text.ToString();
                TaxTybDG.AllowUserToAddRows = true;
                TaxTybDG.Rows.Add(0, Vst_TaxTypeCode, Vst_TaxTypeName, Vst_TaxSubTypeCode, Vst_TaxSubTypeName);
                TaxTybDG.AllowUserToAddRows = false;
                GrdCheckTax();
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Forms.BasicsCode.TokenFrm BscFrm = new Forms.BasicsCode.TokenFrm();
            BscFrm.Show();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            Forms.BasicsCode.DeviceFrm BscFrm = new Forms.BasicsCode.DeviceFrm();
            BscFrm.Show();
        }

        private void ActivityDG_DoubleClick(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en")
            {
                var result1 = MessageBox.Show("Do you want to delete this Activity  ؟", "Delete Activity ", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string Vst_ActiveCode = ActivityDG.CurrentRow.Cells[1].Value.ToString().Trim();
                    var resultR = DNtMDB.Tbl_OurCompActivity.FirstOrDefault(x => x.ActivityCode == Vst_ActiveCode);
                    if (resultR != null)
                    {
                        DNtMDB.Tbl_OurCompActivity.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Our Company Activity",
                            TableName = "Tbl_OurCompActivity",
                            TableRecordId = Vst_ActiveCode.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "OurCompanyFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        MessageBox.Show("Deleted");
                        //************************************

                        simpleButton6_Click(sender, e);
                    }
                    else
                    {
                        ActivityDG.AllowUserToDeleteRows = true;
                    }
                }
            }
            else if (LangTxtBox.Text == "ar-EG")
            {
                var result1 = MessageBox.Show("هل تريد حذف طبيعة نشاط  ؟", "حذف طبيعة نشاط ", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string Vst_ActiveCode = ActivityDG.CurrentRow.Cells[1].Value.ToString().Trim();
                    var resultR = DNtMDB.Tbl_OurCompActivity.FirstOrDefault(p => p.ActivityCode == Vst_ActiveCode);
                    if (resultR != null)
                    {
                        DNtMDB.Tbl_OurCompActivity.Remove(resultR);
                        DNtMDB.SaveChanges();
                        //---------------خفظ ااحداث 
                        SecurityEvent sev = new SecurityEvent
                        {
                            ActionName = "Delete Our Company Activity",
                            TableName = "Tbl_OurCompActivity",
                            TableRecordId = Vst_ActiveCode.ToString(),
                            Description = "",
                            ManagementName = Program.GlbV_Management,
                            ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                            EmployeeName = Program.GlbV_EmpName,
                            User_ID = Program.GlbV_UserId,
                            UserName = Program.GlbV_UserName,
                            FormName = "OurCompanyFrm"


                        };
                        DNtEVDB.SecurityEvents.Add(sev);
                        DNtEVDB.SaveChanges();
                        //************************************
                        MessageBox.Show("تم الحذف");
                        simpleButton6_Click(sender, e);
                    }
                }
            }
        }

        private void TaxTybDG_DoubleClick(object sender, EventArgs e)
        {
            if (LangTxtBox.Text == "en")
            {
                var result1 = MessageBox.Show("Do you want to delete this Tax ?", "Delete Tax ", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    int Vint_ID = int.Parse(TaxTybDG.CurrentRow.Cells[0].Value.ToString().Trim());

                    var resultR = DNtMDB.Tbl_TaxOurCompany.Find(Vint_ID);
                    DNtMDB.Tbl_TaxOurCompany.Remove(resultR);
                    DNtMDB.SaveChanges();
                    //---------------حفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Delete Our Company Activity",
                        TableName = "Tbl_OurCompActivity",
                        TableRecordId = Vint_ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "OurCompanyFrm"
                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    MessageBox.Show("Deleted");
                    //************************************
                    simpleButton6_Click(sender, e);
                }
            }
            else if (LangTxtBox.Text == "ar-EG")
            {
                var result1 = MessageBox.Show("هل تريد حذف الضريبه  ؟", "حذف الضريبه", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    int Vint_ID = int.Parse(TaxTybDG.CurrentRow.Cells[0].Value.ToString().Trim());
                    var resultR = DNtMDB.Tbl_TaxOurCompany.Find(Vint_ID);
                    DNtMDB.Tbl_TaxOurCompany.Remove(resultR);
                    DNtMDB.SaveChanges();
                    //---------------خفظ ااحداث 
                    SecurityEvent sev = new SecurityEvent
                    {
                        ActionName = "Delete Our Company Activity",
                        TableName = "Tbl_OurCompActivity",
                        TableRecordId = Vint_ID.ToString(),
                        Description = "",
                        ManagementName = Program.GlbV_Management,
                        ActionDate = Convert.ToDateTime(DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")),
                        EmployeeName = Program.GlbV_EmpName,
                        User_ID = Program.GlbV_UserId,
                        UserName = Program.GlbV_UserName,
                        FormName = "OurCompanyFrm"
                    };
                    DNtEVDB.SecurityEvents.Add(sev);
                    DNtEVDB.SaveChanges();
                    //************************************
                    MessageBox.Show("تم الحذف");
                    simpleButton5_Click(sender, e);
                }
            }
        }
        private void AddColumnDtaGrdUnit()
        {
            DataGridViewTextBoxColumn newColumnID = new DataGridViewTextBoxColumn();
            newColumnID.HeaderText = "ID"; // Header text of the column
            newColumnID.Name = "ID"; // Unique name of the column
            UniteTDG.Columns.Add(newColumnID);

            DataGridViewTextBoxColumn newColumnCode = new DataGridViewTextBoxColumn();
            newColumnCode.HeaderText = "Code"; // Header text of the column
            newColumnCode.Name = "Code"; // Unique name of the column
            UniteTDG.Columns.Add(newColumnCode);

            DataGridViewTextBoxColumn newColumnName = new DataGridViewTextBoxColumn();
            newColumnName.HeaderText = "Desc_En"; // Header text of the column
            newColumnName.Name = "Desc_En"; // Unique name of the column
            UniteTDG.Columns.Add(newColumnName);

            DataGridViewTextBoxColumn newColumnNameAr = new DataGridViewTextBoxColumn();
            newColumnNameAr.HeaderText = "Desc_Ar"; // Header text of the column
            newColumnNameAr.Name = "Desc_Ar"; // Unique name of the column
            UniteTDG.Columns.Add(newColumnNameAr);

        }
        private void GrdCheckUnite()
        {
            if (Program.GlbV_Language == "en" && LangTxtBox.Text == "en")
            {
                UniteTDG.Columns["ID"].Visible = false;
               
                UniteTDG.Columns["Code"].HeaderText = "Code";
                UniteTDG.Columns["Desc_En"].HeaderText = "Unite Name En";
                UniteTDG.Columns["Desc_Ar"].HeaderText = "Unite Name Ar";
                UniteTDG.Columns["Desc_En"].Width = 150;
            }
            else if (Program.GlbV_Language == "ar-EG" && LangTxtBox.Text == "ar-EG")
            {
                UniteTDG.Columns["ID"].Visible = false;
                
                UniteTDG.Columns["Code"].HeaderText = "كود الوحده";
                
                UniteTDG.Columns["Desc_En"].HeaderText = "الوحده بالانجليزي";
                UniteTDG.Columns["Desc_Ar"].HeaderText = "الوحده";
                UniteTDG.Columns["Desc_Ar"].Width = 150;
            }
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (cmbUniteType.SelectedIndex >= 0)
            {
                if (UniteTDG.Rows.Count == 0)
                {
                    AddColumnDtaGrdUnit();
                }
                string Vst_UniteTypeCode = cmbUniteType.SelectedValue.ToString();

                string Vst_UniteName = DNtMDB.Tbl_UniteType.FirstOrDefault(x => x.Code == Vst_UniteTypeCode).Desc_En.ToString();
                UniteTDG.AllowUserToAddRows = true;
                UniteTDG.Rows.Add(0, Vst_UniteTypeCode, Vst_UniteName);
                UniteTDG.AllowUserToAddRows = false;
                GrdCheckUnite();
            }
        }
        private void AddColumnDtaGrdCurrency()
        {
            DataGridViewTextBoxColumn newColumnID = new DataGridViewTextBoxColumn();
            newColumnID.HeaderText = "ID"; // Header text of the column
            newColumnID.Name = "ID"; // Unique name of the column
            CurrencyDG.Columns.Add(newColumnID);

            DataGridViewTextBoxColumn newColumnCode = new DataGridViewTextBoxColumn();
            newColumnCode.HeaderText = "code"; // Header text of the column
            newColumnCode.Name = "code"; // Unique name of the column
            CurrencyDG.Columns.Add(newColumnCode);

            DataGridViewTextBoxColumn newColumnName = new DataGridViewTextBoxColumn();
            newColumnName.HeaderText = "CurrencyName"; // Header text of the column
            newColumnName.Name = "dec_en"; // Unique name of the column
            CurrencyDG.Columns.Add(newColumnName);

        }
        private void GrdCheckCurrency()
        {
            if (Program.GlbV_Language == "en" && LangTxtBox.Text == "en")
            {
                CurrencyDG.Columns["ID"].Visible = false;
                CurrencyDG.Columns["code"].HeaderText = " Code";
                CurrencyDG.Columns["dec_en"].HeaderText = "Currency";
                CurrencyDG.Columns["dec_en"].Width = 150;
            }
            else if (Program.GlbV_Language == "ar-EG" && LangTxtBox.Text == "ar-EG")
            {
                CurrencyDG.Columns["ID"].Visible = false;
                CurrencyDG.Columns["code"].HeaderText = "كود ";
                CurrencyDG.Columns["dec_en"].HeaderText = "العمله";
                CurrencyDG.Columns["dec_en"].Width = 150;
            }
        }
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (cmbUniteType.SelectedIndex >= 0)
            {
                if (CurrencyDG.Rows.Count == 0)
                {
                    AddColumnDtaGrdCurrency();
                }
                string Vst_CurrencyCode = CmbCurrency.SelectedValue.ToString();

                string Vst_CurrencyName = DNtMDB.Tbl_Currency.FirstOrDefault(x => x.code == Vst_CurrencyCode).dec_en.ToString();
                CurrencyDG.AllowUserToAddRows = true;
                CurrencyDG.Rows.Add(0, Vst_CurrencyCode, Vst_CurrencyName);
                CurrencyDG.AllowUserToAddRows = false;
                GrdCheckCurrency();
            }
        }
    }
}