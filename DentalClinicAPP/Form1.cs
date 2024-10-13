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
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using ExcelDataReader;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data.SqlClient;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.HighLevelAPI40;
using Net.Pkcs11Interop;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Ocsp;
//using System.Web.UI.WebControls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Telerik.WinControls;
//*************************

using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509.Store;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto.Operators;
using Chilkat;
using System.Diagnostics;

namespace DentalClinicAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        public static void SaveSerializedReceiptsToFile(string filePath, string serializedReceipts)
        {
            // قراءة الملف الأصلي
            string jsonContent = File.ReadAllText(filePath);

            // تحويل JSON إلى JObject
            JObject jsonObject = JObject.Parse(jsonContent);

            // استبدال المصفوفة القديمة داخل "receipts" بالمحتوى الجديد من TextBox
            jsonObject["receipts"] = JArray.Parse(serializedReceipts);

            // كتابة المحتوى المعدل إلى ملف جديد (أو استبدال الملف الأصلي)
            File.WriteAllText(filePath, jsonObject.ToString(Formatting.Indented));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string jsonFilePath = @"D:\555555555555555555555555555.json";

            // استدعاء الدالة لقراءة الملف وتسلسل بيانات receipts فقط
            string serializedReceipts = SerializeReceiptsOnlyFromFile(jsonFilePath);

            // إزالة "RECEIPTS""RECEIPTS" من البداية
            serializedReceipts = serializedReceipts.Replace("RECEIPTS", "");

            // عرض النتيجة المعدلة في TextBox
            textBox1.Text = serializedReceipts;

            // إنشاء كائن Chilkat StringBuilder
            Chilkat.StringBuilder sb = new Chilkat.StringBuilder();

            // تحميل المحتوى المعدل من السلسلة النصية بدلاً من ملف
            bool loadSuccess = sb.Append(serializedReceipts);
            if (loadSuccess == false)
            {
                Debug.WriteLine("Failed to load serialized content.");
                MessageBox.Show("Failed to load serialized content.");
                return;
            }

            // تحويل JSON إلى الصيغة المعيارية ITIDA
            bool canonicalizeSuccess = sb.Encode("itida", "utf-8");
            if (canonicalizeSuccess == false)
            {
                Debug.WriteLine("Failed to canonicalize JSON.");
                MessageBox.Show("Failed to canonicalize JSON.");
                return;
            }

            // الحصول على UUID باستخدام SHA256
            string uuid = sb.GetHash("sha256", "hex_lower", "utf-8");
            Debug.WriteLine("eInvoicing UUID = " + uuid);

            // عرض UUID في TextBox
            textBox2.Text = uuid;

            // عرض المحتوى المعياري (canonicalized) في TextBox
            textBox1.Text = sb.GetAsString();
        }

        private string SerializeReceiptsOnlyFromFile(string filePath)
        {
            // التحقق من وجود الملف
            if (!File.Exists(filePath))
            {
                MessageBox.Show("File not found.");
                return string.Empty;
            }

            // قراءة محتويات الملف
            string jsonContent = File.ReadAllText(filePath);

            // تحميل JSON إلى كائن JObject
            JObject jsonObject = JObject.Parse(jsonContent);

            // الوصول إلى بيانات receipts
            JToken receiptsToken = jsonObject["receipts"];
            if (receiptsToken == null)
            {
                MessageBox.Show("The receipts section is missing in the JSON file.");
                return string.Empty;
            }

            // تسلسل بيانات receipts فقط
            string serializedReceipts = receiptsToken.ToString();

            return serializedReceipts;
        }
    }
}
