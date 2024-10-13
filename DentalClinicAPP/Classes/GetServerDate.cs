 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using DentalClinicAPP.Classes;
using DevComponents.DotNetBar.Controls;
using DentalClinicAPP.DataBase.Model;
using DentalClinicAPP.DataBase.ModelEvents;
using DentalClinicAPP.DataBase.ModelSecurity;
 

namespace DentalClinicAPP.Classes
{
    class GetServerDate
    {
       

        public static DateTime Cs_GetServerDate()
        {
            DateTime Result_ValueBefore = DateTime.Now;
            //**********************
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DentalClinicAPP.Properties.Settings.EInvoiceConnectionString"].ConnectionString.ToString();
            SqlCommand com = new SqlCommand();
            SqlCommand com1 = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com1.Connection = con;
            com1.CommandType = CommandType.Text;


            com.Parameters.Clear();
            //row.Cells[5].Value = MAXID.Text;
            //com.CommandText = "SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))";
            com.CommandText = "SELECT  GETDATE() AS CurrentDateTime";
            SqlDataReader red;
            con.Open();
            red = com.ExecuteReader();
            while (red.Read())
            {
                Result_ValueBefore = Convert.ToDateTime(red.GetValue(0).ToString());
            }
            red.Close();
            con.Close();

            //*****************
            return Result_ValueBefore;

        }
    }
}
