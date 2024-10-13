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
using DevExpress.XtraBars;

namespace DentalClinicAPP
{
    public partial class MainFrm : DevExpress.XtraEditors.XtraForm
    {
        public MainFrm()
        {
            InitializeComponent();
            
        }

        private void tileControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.No;
            RightToLeftLayout = false;
            this.Controls.Clear();
            InitializeComponent();
            LangTxtBox.Text = "en";
            //pictureBox1.ImageLocation = "E:\\DentalClinic\\DentalClinicAPP\\DentalClinicAPP\\Images\\enp.jpg";
            this.barStaticItem1.Caption = "User Name  : " + " " + Program.GlbV_UserName;
            this.barStaticItem2.Caption = "Date   : " + " " + DateTime.Now.Date.ToShortDateString();
            //this.barStaticItem3.Caption = "الوحدة   : " + " " + Program.GlbV_SysUnite;
            //this.barStaticItem5.Caption = "الادارة : " + " " + Program.GlbV_Management;
            //this.barStaticItem4.Caption = "اسم الموظف   : " + " " + Program.GlbV_EmpName;
            //this.barHeaderItem1.Caption = "اسم الموظف  : " + " " + Program.GlbV_EmpName;
            //this.barStaticItem6.Caption = "الوظيفه : " + " " + Program.GlbV_job;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
            LangTxtBox.Text = "";

            this.RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            this.Controls.Clear();
            InitializeComponent();
            LangTxtBox.Text = "ar-EG";
            //pictureBox1.ImageLocation = "E:\\DentalClinic\\DentalClinicAPP\\DentalClinicAPP\\Images\\arp.jpg";
            this.barStaticItem1.Caption = "اسم المستخدم  : " + " " + Program.GlbV_UserName;
            this.barStaticItem2.Caption = "التاريخ   : " + " " + DateTime.Now.Date.ToShortDateString();
            this.barButtonItem6.Caption = "الوحدة   : " + " " + Program.GlbV_SysUnite;
            //this.barStaticItem5.Caption = "الادارة : " + " " + Program.GlbV_Management;
            //this.barStaticItem4.Caption = "اسم الموظف   : " + " " + Program.GlbV_EmpName;
            this.barButtonItem5.Caption = "اسم الموظف  : " + " " + Program.GlbV_EmpName;
            //this.barStaticItem6.Caption = "الوظيفه : " + " " + Program.GlbV_job;
        }

       
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            //Forms.BasicsCode.ClinicksFrm BscFrm = new Forms.BasicsCode.ClinicksFrm();
            //BscFrm.Show();
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            //Forms.BasicsCode.DoctorsFrm BscFrm = new Forms.BasicsCode.DoctorsFrm();
            //BscFrm.Show();
        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            //Forms.BasicsCode.PatientsFrm BscFrm = new Forms.BasicsCode.PatientsFrm();
            //BscFrm.Show();
        }

        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
            //Forms.BasicsCode.MedicalDiagnosisFrm BscFrm = new Forms.BasicsCode.MedicalDiagnosisFrm();
            //BscFrm.Show();
        }

        

        private void tileItem9_ItemClick(object sender, TileItemEventArgs e)
        {
            //Forms.BasicsCode.RaysFrm BscFrm = new Forms.BasicsCode.RaysFrm();
            //BscFrm.Show();
        }
         //***************************** Nav Bar
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.OurCompanyFrm BscFrm = new Forms.BasicsCode.OurCompanyFrm();
            BscFrm.Show();
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.ActivitiesFrm BscFrm = new Forms.BasicsCode.ActivitiesFrm();
            BscFrm.Show();
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.CustomersFrm BscFrm = new Forms.BasicsCode.CustomersFrm();
            BscFrm.Show();
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.DocumentTypeFrm BscFrm = new Forms.BasicsCode.DocumentTypeFrm();
            BscFrm.Show();
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.CompaniesType BscFrm = new Forms.BasicsCode.CompaniesType();
            BscFrm.Show();
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.CountryFrm BscFrm = new Forms.BasicsCode.CountryFrm();
            BscFrm.Show();
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.CurrencyFrm BscFrm = new Forms.BasicsCode.CurrencyFrm();
            BscFrm.Show();
        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.TaxTypeFrm BscFrm = new Forms.BasicsCode.TaxTypeFrm();
            BscFrm.Show();

        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //Forms.BasicsCode.Dosages BscFrm = new Forms.BasicsCode.Dosages();
            //BscFrm.Show();
        }

        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.PaymentMethodsFrm BscFrm = new Forms.BasicsCode.PaymentMethodsFrm();
            BscFrm.Show();
        }

        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //Forms.BasicsCode.Cities BscFrm = new Forms.BasicsCode.Cities();
            //BscFrm.Show();
        }

        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //Forms.BasicsCode.ServicesFrm BscFrm = new Forms.BasicsCode.ServicesFrm();
            //BscFrm.Show();
        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.UsersFrm SecFrm = new Forms.SecuritySystem.UsersFrm();
            SecFrm.Show();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            if (Program.GlbV_Language == "en")
            {
                LangTxtBox.Text = "en";
                //pictureBox1.ImageLocation = "E:\\DentalClinic\\DentalClinicAPP\\DentalClinicAPP\\Images\\enp.jpg";
                barStaticItem1.Caption = Program.GlbV_EmpName;
                simpleButton3_Click(sender, e);

            }
            else if (Program.GlbV_Language == "ar-EG")
            {
                LangTxtBox.Text = "ar-EG";
                //pictureBox1.ImageLocation = "E:\\DentalClinic\\DentalClinicAPP\\DentalClinicAPP\\Images\\arp.jpg";
                simpleButton4_Click(sender, e);
            }
            //accordionControl1.OptionsMinimizing.State = Minimized;
            ContorleMenu();
        }
        private void ContorleMenu()
        {
             //********************************Basic Data**********************************
            //1 - Our Company
            if (Program.SecurityFormsList.Contains(1) == true)
            {
                navBarItem1.Visible = true;
            }
            else
            {
                navBarItem1.Visible = false;
            }
            //navBarItem1.Enabled = Program.SecurityFormsList.Contains(1) ? true : false;
            //2 - Document Type
            if (Program.SecurityFormsList.Contains(2) == true)
            {
                navBarItem2.Visible = true;
            }
            else
            {
                navBarItem2.Visible = false;
            }

            //3 - Customers

            if (Program.SecurityFormsList.Contains(3) == true)
            {
                navBarItem3.Visible = true;
            }
            else
            {
                navBarItem3.Visible = false;
            }
            //*******Document Type
            if (Program.SecurityFormsList.Contains(4) == true)
            {
                navBarItem4.Visible = true;
            }
            else
            {
                navBarItem4.Visible = false;
            }
            //********Companies Type
            if (Program.SecurityFormsList.Contains(5) == true)
            {
                navBarItem5.Visible = true;
            }
            else
            {
                navBarItem5.Visible = false;
            }
            //********countries
            if (Program.SecurityFormsList.Contains(6) == true)
            {
                ieCountry.Visible = true;
            }
            else
            {
                ieCountry.Visible = false;
            }

            //********currencies
            if (Program.SecurityFormsList.Contains(7) == true)
            {
                navBarItem7.Visible = true;
            }
            else
            {
                navBarItem7.Visible = false;
            }
            //********tax of type
            if (Program.SecurityFormsList.Contains(8) == true)
            {
                navBarItem8.Visible = true;
            }
            else
            {
                navBarItem8.Visible = false;
            }

            //********type of unite
            if (Program.SecurityFormsList.Contains(9) == true)
            {
                navBarItem9.Visible = true;
            }
            else
            {
                navBarItem9.Visible = false;
            }
            //********Payment methodes
            if (Program.SecurityFormsList.Contains(10) == true)
            {
                navBarItem13.Visible = true;
            }
            else
            {
                navBarItem13.Visible = false;
            }

            //********Customer Type
            //if (Program.SecurityFormsList.Contains(11) == true)
            //{
            //    navBarItem6.Visible = true;
            //}
            //else
            //{
            //    navBarItem6.Visible = false;
            //}

            //********items
            //if (Program.SecurityFormsList.Contains(12) == true)
            //{
            //    navBarItem14.Visible = true;
            //}
            //else
            //{
            //    navBarItem14.Visible = false;
            //}

            //********accounting guid
            if (Program.SecurityFormsList.Contains(13) == true)
            {
                navBarItem15.Visible = true;
            }
            else
            {
                navBarItem15.Visible = false;
            }

            //********governorate
            if (Program.SecurityFormsList.Contains(14) == true)
            {
                navBarItem18.Visible = true;
            }
            else
            {
                navBarItem18.Visible = false;
            }
            //********cities
            if (Program.SecurityFormsList.Contains(15) == true)
            {
                navBarItem19.Visible = true;
            }
            else
            {
                navBarItem19.Visible = false;
            }

            //********tax of type
            if (Program.SecurityFormsList.Contains(16) == true)
            {
                navBarItem20.Visible = true;
            }
            else
            {
                navBarItem20.Visible = false;
            }
            //********unite type 
            if (Program.SecurityFormsList.Contains(17) == true)
            {
                navBarItem21.Visible = true;
            }
            else
            {
                navBarItem21.Visible = false;
            }
            //********sectors
            if (Program.SecurityFormsList.Contains(18) == true)
            {
                navBarItem22.Visible = true;
            }
            else
            {
                navBarItem22.Visible = false;
            }
            //********Companies and management
            if (Program.SecurityFormsList.Contains(19) == true)
            {
                navBarItem23.Visible = true;
            }
            else
            {
                navBarItem23.Visible = false;
            }
            //********internal items
            if (Program.SecurityFormsList.Contains(20) == true)
            {
                navBarItem10.Visible = true;
            }
            else
            {
                navBarItem10.Visible = false;
            }
            //********item of tax
            if (Program.SecurityFormsList.Contains(21) == true)
            {
                navBarItem24.Visible = true;
            }
            else
            {
                navBarItem24.Visible = false;
            }
            //************************************Security****************************************
            //********Users
            if (Program.SecurityFormsList.Contains(23) == true)
            {
                navBarItem16.Visible = true;
            }
            else
            {
                navBarItem16.Visible = false;
            }
            ////********Forms
            if (Program.SecurityFormsList.Contains(24) == true)
            {
                navBarItem25.Visible = true;
            }
            else
            {
                navBarItem25.Visible = false;
            }
            //********Unites Of Program
            if (Program.SecurityFormsList.Contains(25) == true)
            {
                navBarItem26.Visible = true;
            }
            else
            {
                navBarItem26.Visible = false;
            }
            //********Procedures
            if (Program.SecurityFormsList.Contains(26) == true)
            {
                navBarItem27.Visible = true;
            }
            else
            {
                navBarItem27.Visible = false;
            }
            //********Procedures Forms 
            if (Program.SecurityFormsList.Contains(27) == true)
            {
                navBarItem28.Visible = true;
            }
            else
            {
                navBarItem28.Visible = false;
            }
            //********Forms Unite Program
            if (Program.SecurityFormsList.Contains(28) == true)
            {
                navBarItem29.Visible = true;
            }
            else
            {
                navBarItem29.Visible = false;
            }
            //********User Type Forms 
            if (Program.SecurityFormsList.Contains(29) == true)
            {
                navBarItem30.Visible = true;
            }
            else
            {
                navBarItem30.Visible = false;
            }
            //********System Unites
            if (Program.SecurityFormsList.Contains(30) == true)
            {
                navBarItem31.Visible = true;
            }
            else
            {
                navBarItem31.Visible = false;
            }
            //********Employees
            if (Program.SecurityFormsList.Contains(31) == true)
            {
                navBarItem32.Visible = true;
            }
            else
            {
                navBarItem32.Visible = false;
            }
            //********Users Permissions
            if (Program.SecurityFormsList.Contains(32) == true)
            {
                navBarItem33.Visible = true;
            }
            else
            {
                navBarItem33.Visible = false;
            }
        }
        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            //Forms.BasicsCode.MedicinsFrm SecFrm = new Forms.BasicsCode.MedicinsFrm();
            //SecFrm.Show();
            
        }

        private void navBarItem15_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.AccountGuidFrm BscFrm = new Forms.BasicsCode.AccountGuidFrm();
            BscFrm.Show();
        }

        private void navBarItem18_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.GovernoratesFrm BscFrm = new Forms.BasicsCode.GovernoratesFrm();
            BscFrm.Show();
        }

        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.Cities BscFrm = new Forms.BasicsCode.Cities();
            BscFrm.Show();
        }

        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.TaxSubTypesFrm BscFrm = new Forms.BasicsCode.TaxSubTypesFrm();
            BscFrm.Show();
        }

        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.OurCompanyFrm BscFrm = new Forms.BasicsCode.OurCompanyFrm();
            BscFrm.Show();
        }

        private void navBarItem21_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.UnitesTypeFrm BscFrm = new Forms.BasicsCode.UnitesTypeFrm();
            BscFrm.Show();
        }

        private void navBarItem22_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.SectorsFrm BscFrm = new Forms.BasicsCode.SectorsFrm();
            BscFrm.Show();
        }

        private void navBarItem23_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.CompaniesFrm BscFrm = new Forms.BasicsCode.CompaniesFrm();
            BscFrm.Show();
        }

        private void navBarItem17_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            TaxI t = new TaxI();
            t.ShowDialog();
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.ItemsNFrm BscFrm = new Forms.BasicsCode.ItemsNFrm();
            BscFrm.Show();
        }

        private void navBarItem24_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.BasicsCode.ItemsFrm BscFrm = new Forms.BasicsCode.ItemsFrm();
            BscFrm.Show();
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem25_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.FormsFrm BscFrm = new Forms.SecuritySystem.FormsFrm();
            BscFrm.Show();
        }

        private void navBarItem26_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.MainMenuProgramFrm MnPFrm = new Forms.SecuritySystem.MainMenuProgramFrm();
            MnPFrm.Show();
        }

        private void navBarItem27_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.ProceduresFrm MnPFrm = new Forms.SecuritySystem.ProceduresFrm();
            MnPFrm.Show();
        }

        private void navBarItem28_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.ProceduresFormsFrm MnPFrm = new Forms.SecuritySystem.ProceduresFormsFrm();
            MnPFrm.Show();
        }

        private void navBarItem29_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.MenuProgUnit_SysUnites MnPFrm = new Forms.SecuritySystem.MenuProgUnit_SysUnites();
            MnPFrm.Show();

        }

        private void navBarItem30_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.FormsUserTypeUserFrm MnPFrm = new Forms.SecuritySystem.FormsUserTypeUserFrm();
            MnPFrm.Show();
        }

        private void navBarItem31_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.SystemUnitesFrm MnPFrm = new Forms.SecuritySystem.SystemUnitesFrm();
            MnPFrm.Show();
        }

        private void navBarItem32_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.EmployeeFrm MnPFrm = new Forms.SecuritySystem.EmployeeFrm();
            MnPFrm.Show();
        }

        private void navBarItem33_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Forms.SecuritySystem.UsersProcedureFormFrm MnPFrm = new Forms.SecuritySystem.UsersProcedureFormFrm();
            MnPFrm.Show();
        }

        

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
           TaxI MnPFrm = new TaxI();
            MnPFrm.Show();
        }

        private void navBarItem6_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            TaxI t = new TaxI();
            t.ShowDialog();
        }

        private void accordionControlElement10_Click(object sender, EventArgs e)
        {
            Receipt.ReciptFrm f = new Receipt.ReciptFrm();
            f.ShowDialog();
        }

        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            GetFrm g = new GetFrm();
            g.ShowDialog();
        }
    }
}