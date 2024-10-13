using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DentalClinicAPP;
using DentalClinicAPP.Classes;
using System.Threading;
using System.Configuration;

namespace DentalClinicAPP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DentalClinicAPP.MainFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.AnalysisFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.DoctorsFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.MedicinUnitesFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.ClinicksFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.AnalysisFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.Governorates());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.Cities());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.ClinicksFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.DoctorsFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.PatientsFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.MedicinsFrm());
            //Application.Run(new DentalClinicAPP.Forms.BasicsCode.MedicalDiagnosisFrm());
            Application.Run(new DentalClinicAPP.Forms.SecuritySystem.LoginForm());
            //Application.Run(new DentalClinicAPP.Forms.SecuritySystem.UsersFrm());
            //Application.Run(new DentalClinicAPP.MedicalCheckUp.MedicalCheckUpFrm());
            //Application.Run(new DentalClinicAPP.Receipt.ReciptFrm());
            //Application.Run(new DentalClinicAPP.TaxI());
        }
        public static List<int> SecurityFormsList = new List<int>();
        public static List<FormProcedures> SecurityProceduresList = new List<FormProcedures>();
        public static string GlbV_Connection = ConfigurationManager.ConnectionStrings["DentalClinicAPP.Properties.Settings.EInvoiceConnectionString"].ConnectionString.ToString();
      
        public static string GlbV_Language = "";
        public static string GlbV_UserName = "";
        public static string GlbV_EmpName = "";
        public static string GlbV_SysUnite = "";
        public static string GlbV_Management = "";
        public static string GlbV_job = "";
        public static string GlbV_CPassword = "";
        public static string GlbV_UserType = "";
        public static string GlbV_DateTime = "";
        public static int GlbV_UserId = 0;
        public static int GlbV_UserTypeId = 0;
        public static int GlbV_SysUnite_ID = 0;


    }
}
