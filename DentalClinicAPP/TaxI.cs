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

    public partial class TaxI : Form
    {

        private void ColorizeJson(string json, RichTextBox richTextBox)
        {
            richTextBox.Clear();

            var stringColor = Color.Brown;
            var numberColor = Color.Blue;
            var booleanColor = Color.Purple;
            var nullColor = Color.Red;
            var bracketsColor = Color.White;
            var keyColor = Color.Green;

            bool inQuotes = false;
            bool isKey = true;

            for (int i = 0; i < json.Length; i++)
            {
                char currentChar = json[i];

                // Check if we're inside quotes
                if (currentChar == '\"')
                {
                    inQuotes = !inQuotes;
                    richTextBox.SelectionColor = stringColor;
                }

                if (inQuotes)
                {
                    richTextBox.AppendText(currentChar.ToString());
                    continue;
                }

                if (char.IsWhiteSpace(currentChar))
                {
                    richTextBox.AppendText(currentChar.ToString());
                    continue;
                }

                switch (currentChar)
                {
                    case '{':
                    case '}':
                    case '[':
                    case ']':
                    case ',':
                    case ':':
                        richTextBox.SelectionColor = bracketsColor;
                        richTextBox.AppendText(currentChar.ToString());
                        if (currentChar == ':' && !inQuotes)
                        {
                            isKey = false;
                        }
                        else if (currentChar == ',' && !inQuotes)
                        {
                            isKey = true;
                        }
                        break;

                    default:
                        int length = 0;
                        if (char.IsDigit(currentChar) || currentChar == '-')
                        {
                            while (i + length < json.Length && (char.IsDigit(json[i + length]) || json[i + length] == '.' || json[i + length] == '-'))
                            {
                                length++;
                            }
                            richTextBox.SelectionColor = numberColor;
                            richTextBox.AppendText(json.Substring(i, length));
                            i += length - 1;
                        }
                        else if (json.Substring(i, 4).ToLower() == "true" || json.Substring(i, 5).ToLower() == "false")
                        {
                            string boolValue = json.Substring(i, json[i + 4] == 'e' ? 4 : 5);
                            richTextBox.SelectionColor = booleanColor;
                            richTextBox.AppendText(boolValue);
                            i += boolValue.Length - 1;
                        }
                        else if (json.Substring(i, 4).ToLower() == "null")
                        {
                            richTextBox.SelectionColor = nullColor;
                            richTextBox.AppendText("null");
                            i += 3;
                        }
                        else if (currentChar == '\"')
                        {
                            length = 1;
                            while (i + length < json.Length && json[i + length] != '\"')
                            {
                                length++;
                            }
                            richTextBox.SelectionColor = isKey ? keyColor : stringColor;
                            richTextBox.AppendText(json.Substring(i, length + 1));
                            i += length;
                        }
                        else
                        {
                            richTextBox.AppendText(currentChar.ToString());
                        }
                        break;
                }
            }
        }

        private string pin = "58866042"; // استبدل YOUR_PIN بالـ PIN الخاص بك
        private string libraryPath = "eps2003csp11.dll"; // مسار مكتبة التوكين
        public string sendinv { get; set; }
        public string notSendinv { get; set; }
        public string SendDtatusid { get; set; }
        public string UrlStr { get; set; }


        private readonly ApiFunctions _apiFunctions = new ApiFunctions();
        private string _privateKey = "<YOUR_PRIVATE_KEY_HERE>";
        public TaxI()
        {
            InitializeComponent();
        }

        private void TaxI_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'excell.InvoiceHeader1' table. You can move, or remove it, as needed.
            this.invoiceHeader1TableAdapter.FillNot(this.excell.InvoiceHeader1);
            // TODO: This line of code loads data into the 'excell1.InvoiceHeader' table. You can move, or remove it, as needed.
            //this.invoiceHeaderTableAdapter.FillNot(this.excell1.InvoiceHeader);
            this.invoiceHeaderTableAdapter.FillByOK(this.excell1.InvoiceHeader);
            // TODO: This line of code loads data into the 'company.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.company.DataTable1);
            // TODO: This line of code loads data into the 'excell.ExcelTable' table. You can move, or remove it, as needed.
            //this.excelTableTableAdapter.Fill(this.excell.ExcelTable);
            getToken_Pin();
            sendinv = "0";
            notSendinv = "0";
            SendDtatusid = "0";
            Lblsendinv.Text = sendinv.ToString();
            LblNotSendInv.Text = notSendinv.ToString();
            lbltotalinv.Text = (sendinv + notSendinv).ToString();
            int workid = 123;
            Worker WorkerObj = new Worker();
            WorkerDB Workdb = new WorkerDB();
            WorkerObj = Workdb.GetRecord(workid);
            UrlStr = "";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //using (var openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string filePath = openFileDialog.FileName;
            //        string fileExt = Path.GetExtension(filePath);
            UploadFromDatabase();
            //        DataTable dtExcel = ReadExcel(filePath, fileExt);
            //        //List<InvoiceHeader> invoiceHeaders = SaveInviceHeader(dtExcel);
            //        dataGridView1.DataSource = dtExcel;
            //        dataGridViewX1.DataSource = dtExcel;
            //        // Save DataTable to JSON file
            //        //uploadFomExcel(fileExt, fileExt);
            //    }


            //}
        }
        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();

            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", con);
                    oleAdpt.Fill(dtexcel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading Excel file: " + ex.Message);
                }
            }

            return dtexcel;
        }
        //*****************************************************


        //public void uploadFomExcel(string extension, string filePath)
        //{
        //    string conString = string.Empty;
        //    switch (extension)
        //    {
        //        case ".xls": //Excel 97-03.
        //            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
        //            break;
        //        case ".xlsx": //Excel 07 and above.
        //            conString = ConfigurationManager.ConnectionStrings["Excel16ConString"].ConnectionString;
        //            break;
        //    }

        //    DataTable dt = new DataTable();
        //    dt = ReadExcel(filePath);
        //    conString = ConfigurationManager.ConnectionStrings["InvoiceEF"].ConnectionString;

        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        Label1.Text = "Upload status: Load From Excel To Sql Server ";
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "Delete from dbo.ExcelTable";
        //        cmd.Connection = con;
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //        {
        //            //Set the database table name.
        //            sqlBulkCopy.DestinationTableName = "ExcelTable";


        //            for (int i = 0; i < dt.Columns.Count; i++)
        //            {
        //                sqlBulkCopy.ColumnMappings.Add(i, i);
        //            }
        //            con.Open();
        //            sqlBulkCopy.WriteToServer(dt);
        //            con.Close();
        //            List<InvoiceHeader> InvoiceHeaderList = SaveInviceHeader(dt);
        //            dataGridViewX1.DataSource=dt;
        //            if (InvoiceHeaderList.Count > 0)
        //            {
        //                SendDtatusid = "1";
        //                //BindGried(1);
        //                label2.Text = "Upload Done";
        //            }
        //            else
        //                label2.Text = "البيانات داخلة مسبقا";
        //        }
        //    }



        //}
        private bool IsInternalIdExists(string internalID, string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM InvoiceHeader WHERE internalID = @Internal_ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Internal_ID", internalID);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count > 0;
            }
        }
        public void UploadFromDatabase()
        {

            string conString = ConfigurationManager.ConnectionStrings["InvoiceEF"].ConnectionString;


            DataTable dt = new DataTable();

            string selectQuery = "SELECT * FROM dbo.ExcelTable";

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con);
                    adapter.Fill(dt);

                    con.Close();
                }
                foreach (DataRow row in dt.Rows)
                {
                    string internalId = row["internalID"].ToString();
                    if (!IsInternalIdExists(internalId, conString))
                    {
                        List<InvoiceHeader> invoiceHeaderList = SaveInviceHeader(dt);



                        if (invoiceHeaderList.Count > 0)
                        {
                            SendDtatusid = "1";
                            label2.Text = "Upload Done";
                        }
                        else
                        {
                            label2.Text = "البيانات داخلة مسبقا";
                        }

                        Label1.Text = "Upload status: Load From Database To Grid Completed";
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message;
            }
        }

        //private List<InvoiceHeader> SaveInviceHeader(DataTable dt)
        //{
        //    string currentDateTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        //    List<InvoiceHeader> InvoiceHeaderList = new List<InvoiceHeader>();
        //    InvoiceHeaderDB InvoiceHeaderDBobj = new InvoiceHeaderDB();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        InvoiceHeader InvoiceHeaderObj = new InvoiceHeader();
        //        // Document
        //        InvoiceHeaderObj.dateTimeIssued = currentDateTime.ToString();
        //        InvoiceHeaderObj.documentType = "I";
        //        InvoiceHeaderObj.documentTypeVersion = "1.0";
        //        if (dt.Rows[i]["extraDiscountAmount"].ToString() != "")
        //            InvoiceHeaderObj.extraDiscountAmount = Convert.ToDecimal(dt.Rows[i]["extraDiscountAmount"].ToString());
        //        InvoiceHeaderObj.internalID = dt.Rows[i]["internalID"].ToString();
        //        InvoiceHeaderObj.taxpayerActivityCode = dt.Rows[i]["taxpayerActivityCode"].ToString();
        //        if (dt.Rows[i]["netAmount"].ToString() != "")
        //            InvoiceHeaderObj.netAmount = Convert.ToDecimal(dt.Rows[i]["netAmount"].ToString());
        //        if (dt.Rows[i]["totalAmount"].ToString() != "")
        //            InvoiceHeaderObj.totalAmount = Convert.ToDecimal(dt.Rows[i]["totalAmount"].ToString());
        //        if (dt.Rows[i]["totalDiscountAmount"].ToString() != "")
        //            InvoiceHeaderObj.totalDiscountAmount = Convert.ToDecimal(dt.Rows[i]["totalDiscountAmount"].ToString());
        //        if (dt.Rows[i]["totalItemsDiscountAmount"].ToString() != "")
        //            InvoiceHeaderObj.totalItemsDiscountAmount = Convert.ToDecimal(dt.Rows[i]["totalItemsDiscountAmount"].ToString());
        //        if (dt.Rows[i]["totalSalesAmount"].ToString() != "")
        //            InvoiceHeaderObj.totalSalesAmount = Convert.ToDecimal(dt.Rows[i]["totalSalesAmount"].ToString());

        //        // taxTotals
        //        if (dt.Rows[i]["Taxtotalamout"].ToString() != "")
        //            InvoiceHeaderObj.taxTotals__amount = Convert.ToDecimal(dt.Rows[i]["Taxtotalamout"].ToString());
        //        InvoiceHeaderObj.taxTotals__taxType = dt.Rows[i]["Taxtype1"].ToString();

        //        // issuer
        //        InvoiceHeaderObj.issuer_id = dt.Rows[i]["IssuerId"].ToString();
        //        InvoiceHeaderObj.issuer_name = dt.Rows[i]["issuername"].ToString();
        //        InvoiceHeaderObj.issuer_type = dt.Rows[i]["IssuerType"].ToString();
        //        InvoiceHeaderObj.issuer_branchID = dt.Rows[i]["issuerbranchID"].ToString();
        //        InvoiceHeaderObj.issuer_buildingNumber = dt.Rows[i]["issuerbuildingNumber"].ToString();
        //        InvoiceHeaderObj.issuer_country = dt.Rows[i]["issuercountry"].ToString();
        //        InvoiceHeaderObj.issuer_governate = dt.Rows[i]["issuergovernate"].ToString();
        //        InvoiceHeaderObj.issuer_regionCity = dt.Rows[i]["issuerregionCity"].ToString();
        //        InvoiceHeaderObj.issuer_street = dt.Rows[i]["issuerstreet"].ToString();
        //        //receiver
        //        InvoiceHeaderObj.receiver_buildingNumber = dt.Rows[i]["receiverbuildingNumber"].ToString();
        //        InvoiceHeaderObj.receiver_country = dt.Rows[i]["receivercountry"].ToString();
        //        InvoiceHeaderObj.receiver_governate = dt.Rows[i]["receivergovernate"].ToString();
        //        InvoiceHeaderObj.receiver_regionCity = dt.Rows[i]["receiverregionCity"].ToString();
        //        InvoiceHeaderObj.receiver_street = dt.Rows[i]["receiverstreet"].ToString();
        //        InvoiceHeaderObj.receiver_id = dt.Rows[i]["receiverid"].ToString();
        //        InvoiceHeaderObj.receiver_name = dt.Rows[i]["receivername"].ToString();
        //        InvoiceHeaderObj.receiver_type = dt.Rows[i]["receivertype"].ToString();
        //        InvoiceHeaderObj.SendStatusId = 1;
        //        InvoiceHeaderObj.signatureType = "I";

        //        InvoiceHeader invoiceheaderId = InvoiceHeaderDBobj.GetRecordByInternalID(InvoiceHeaderObj.internalID);
        //        if (invoiceheaderId != null)
        //        {
        //            if (invoiceheaderId.SendStatusId != 2)
        //            {
        //                InvoiceHeaderObj.SendStatusId = 1;
        //                InvoiceHeaderObj.ID = invoiceheaderId.ID;
        //                InvoiceHeaderObj.UpdateDate = DateTime.Now;
        //                InvoiceHeaderObj.UpdateBy = 0; // User ID
        //                bool updateflag = InvoiceHeaderDBobj.UpdateRecord(InvoiceHeaderObj);
        //                InsertInvoiceLine( invoiceheaderId.ID, i); // Pass the current row index
        //                InvoiceHeaderList.Add(InvoiceHeaderObj);
        //            }
        //        }
        //        else
        //        {
        //            InvoiceHeaderObj.InsertDate = DateTime.Now;
        //            InvoiceHeaderObj.InsertedBy = 0; // User ID
        //            int InsertHeaderId = InvoiceHeaderDBobj.SaveRecord(InvoiceHeaderObj);
        //            if (InsertHeaderId > 0)
        //            {
        //                InsertInvoiceLine(dt, InsertHeaderId, i); // Pass the current row index
        //                InvoiceHeaderList.Add(InvoiceHeaderObj);
        //            }
        //        }
        //    }
        //    return InvoiceHeaderList;
        //}

        //private void InsertInvoiceLine(DataTable dt, int InvoiceHeaderId, int rowIndex)
        //{
        //    InvoiceLinedDB InvoiceLinedDBObj = new InvoiceLinedDB();
        //    InvoiceLined InvoiceLinedObj = new InvoiceLined
        //    {

        //        description = dt.Rows[rowIndex]["invoiceLinesdescription"].ToString(),
        //        //if (dt.Rows[i]["amount"] != null)
        //        //    OracleInvoiceObj.invoiceLines__discount__amount = Convert.ToDecimal(dt.Rows[i]["amount"].ToString());
        //        //if (dt.Rows[i]["invoiceLines__discount__rate"].ToString() != "")
        //        //    OracleInvoiceObj.invoiceLines__discount__rate = Convert.ToDecimal(dt.Rows[i]["invoiceLines_discount__rate"].ToString());
        //        internalCode = dt.Rows[rowIndex]["LineInternalcode"].ToString(),
        //        itemCode = dt.Rows[rowIndex]["invoiceLinesitemCode"].ToString(),
        //            //if (dt.Rows[rowIndex]["Itemsdiscount"].ToString() != "")
        //      itemsDiscount = Convert.ToDecimal(dt.Rows[rowIndex]["Itemsdiscount"].ToString()),
        //    itemType = dt.Rows[rowIndex]["InvoicelinesItemtype"].ToString(),

        //       netTotal = Convert.ToDecimal(dt.Rows[rowIndex]["netTotal"].ToString()),

        //       quantity = Convert.ToDecimal(dt.Rows[rowIndex]["InvoicelinesQuantity"].ToString()),

        //        salesTotal = Convert.ToDecimal(dt.Rows[rowIndex]["salesTotal"].ToString()),

        //      taxableItems_amount = Convert.ToDecimal(dt.Rows[rowIndex]["Amount"].ToString()),

        // taxableItems_rate = Convert.ToDecimal(dt.Rows[rowIndex]["rate"].ToString()),
        //    taxableItems_subType = dt.Rows[rowIndex]["subType"].ToString(),
        //    taxableItems_taxType = dt.Rows[rowIndex]["taxType"].ToString(),

        //       total = Convert.ToDecimal(dt.Rows[rowIndex]["total"].ToString()),

        //       totalTaxableFees = Convert.ToDecimal(dt.Rows[rowIndex]["totalTaxableFees"].ToString()),
        //   unitType = dt.Rows[rowIndex]["InvoicelinesUnittype"].ToString(),

        //        unitValue_amountEGP = Convert.ToDecimal(dt.Rows[rowIndex]["InvoicelinesAmountegp"].ToString()),

        //      unitValue_amountSold = Convert.ToDecimal(dt.Rows[rowIndex]["InvoicelinesAmountsold"].ToString()),

        //       unitValue_currencyExchangeRate = Convert.ToDecimal(dt.Rows[rowIndex]["Currencyexchangerate"].ToString()),
        //    unitValue_currencySold = dt.Rows[rowIndex]["InvoicelinesCurrencysold"].ToString(),

        //        valueDifference = Convert.ToDecimal(dt.Rows[rowIndex]["valueDifference"].ToString()),

        //        discount_amount = Convert.ToDecimal(dt.Rows[rowIndex]["DISCOUNT_AMOUNT"].ToString()),
        //    InvoiceHeaderID = InvoiceHeaderId

        //};

        //    InvoiceLined existingInvoiceLined = InvoiceLinedDBObj.GetRecordByLineInternal(InvoiceLinedObj.internalCode);
        //    if (existingInvoiceLined == null)
        //    {
        //        InvoiceLinedDBObj.SaveRecord(InvoiceLinedObj);
        //    }
        //    else
        //    {
        //        InvoiceLinedObj.ID = existingInvoiceLined.ID;
        //        InvoiceLinedDBObj.UpdateRecord(InvoiceLinedObj);
        //    }
        //}
        private List<InvoiceHeader> SaveInviceHeader(DataTable dt)
        {
            string currentDateTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            List<InvoiceHeader> InvoiceHeaderList = new List<InvoiceHeader>();
            InvoiceHeaderDB InvoiceHeaderDBobj = new InvoiceHeaderDB();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                InvoiceHeader InvoiceHeaderObj = new InvoiceHeader();
                // Document details
                InvoiceHeaderObj.dateTimeIssued = currentDateTime;
                InvoiceHeaderObj.documentType = "I";
                InvoiceHeaderObj.documentTypeVersion = "1.0";
                InvoiceHeaderObj.extraDiscountAmount = string.IsNullOrEmpty(dt.Rows[i]["extraDiscountAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["extraDiscountAmount"]);
                InvoiceHeaderObj.internalID = dt.Rows[i]["internalID"].ToString();
                InvoiceHeaderObj.taxpayerActivityCode = dt.Rows[i]["taxpayerActivityCode"].ToString();
                InvoiceHeaderObj.netAmount = string.IsNullOrEmpty(dt.Rows[i]["netAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["netAmount"]);
                InvoiceHeaderObj.totalAmount = string.IsNullOrEmpty(dt.Rows[i]["totalAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["totalAmount"]);
                InvoiceHeaderObj.totalDiscountAmount = string.IsNullOrEmpty(dt.Rows[i]["totalDiscountAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["totalDiscountAmount"]);
                InvoiceHeaderObj.totalItemsDiscountAmount = string.IsNullOrEmpty(dt.Rows[i]["totalItemsDiscountAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["totalItemsDiscountAmount"]);
                InvoiceHeaderObj.totalSalesAmount = string.IsNullOrEmpty(dt.Rows[i]["totalSalesAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["totalSalesAmount"]);

                // Tax Totals
                InvoiceHeaderObj.taxTotals__amount = string.IsNullOrEmpty(dt.Rows[i]["Taxtotalamout"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["Taxtotalamout"]);
                InvoiceHeaderObj.taxTotals__taxType = dt.Rows[i]["Taxtype1"].ToString();

                // Issuer details
                InvoiceHeaderObj.issuer_id = dt.Rows[i]["IssuerId"].ToString();
                InvoiceHeaderObj.issuer_name = dt.Rows[i]["issuername"].ToString();
                InvoiceHeaderObj.issuer_type = dt.Rows[i]["IssuerType"].ToString();
                InvoiceHeaderObj.issuer_branchID = dt.Rows[i]["issuerbranchID"].ToString();
                InvoiceHeaderObj.issuer_buildingNumber = "1";
                InvoiceHeaderObj.issuer_country = dt.Rows[i]["issuercountry"].ToString();
                InvoiceHeaderObj.issuer_governate = dt.Rows[i]["issuergovernate"].ToString();
                InvoiceHeaderObj.issuer_regionCity = dt.Rows[i]["issuerregionCity"].ToString();
                InvoiceHeaderObj.issuer_street = dt.Rows[i]["issuerstreet"].ToString();

                // Receiver details
                InvoiceHeaderObj.receiver_buildingNumber = "1";
                InvoiceHeaderObj.receiver_country = dt.Rows[i]["receivercountry"].ToString();
                InvoiceHeaderObj.receiver_governate = dt.Rows[i]["receivergovernate"].ToString();
                InvoiceHeaderObj.receiver_regionCity = dt.Rows[i]["receiverregionCity"].ToString();
                InvoiceHeaderObj.receiver_street = dt.Rows[i]["receiverstreet"].ToString();
                InvoiceHeaderObj.receiver_id = dt.Rows[i]["receiverid"].ToString();
                InvoiceHeaderObj.receiver_name = dt.Rows[i]["receivername"].ToString();
                InvoiceHeaderObj.receiver_type = dt.Rows[i]["receivertype"].ToString();
                InvoiceHeaderObj.SendStatusId = 1;
                InvoiceHeaderObj.signatureType = "I";

                // Save or Update the Invoice Header
                InvoiceHeader existingHeader = InvoiceHeaderDBobj.GetRecordByInternalID(InvoiceHeaderObj.internalID);
                if (existingHeader != null && existingHeader.SendStatusId != 2)
                {
                    // Update existing header
                    InvoiceHeaderObj.SendStatusId = 1;
                    InvoiceHeaderObj.ID = existingHeader.ID;
                    InvoiceHeaderObj.UpdateDate = DateTime.Now;
                    InvoiceHeaderObj.UpdateBy = 0; // Set user ID if available
                    bool updateSuccess = InvoiceHeaderDBobj.UpdateRecord(InvoiceHeaderObj);
                    InsertInvoiceLineId(dt.Rows[i], existingHeader.ID);
                }
                else
                {
                    // Save new header
                    InvoiceHeaderObj.InsertDate = DateTime.Now;
                    InvoiceHeaderObj.InsertedBy = 0; // Set user ID if available
                    int newHeaderId = InvoiceHeaderDBobj.SaveRecord(InvoiceHeaderObj);
                    if (newHeaderId > 0)
                    {
                        InsertInvoiceLineId(dt.Rows[i], newHeaderId);
                        InvoiceHeaderList.Add(InvoiceHeaderObj);
                    }
                }
            }
            return InvoiceHeaderList;
        }




        //////private List<InvoiceHeader> SaveInviceHeader(DataTable dt)
        //////{

        //////    string currentDateTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        //////    List<InvoiceHeader> InvoiceHeaderList = new List<InvoiceHeader>();
        //////    InvoiceHeaderDB InvoiceHeaderDBobj = new InvoiceHeaderDB();
        //////    for (int i = 0; i < dt.Rows.Count; i++)
        //////    {

        //////        InvoiceHeader InvoiceHeaderObj = new InvoiceHeader();
        //////        // Document
        //////        InvoiceHeaderObj.dateTimeIssued = currentDateTime.ToString();
        //////        InvoiceHeaderObj.documentType ="I";
        //////        InvoiceHeaderObj.documentTypeVersion = "1.0";
        //////        if (dt.Rows[i]["extraDiscountAmount"].ToString() != "")
        //////            InvoiceHeaderObj.extraDiscountAmount = Convert.ToDecimal(dt.Rows[i]["extraDiscountAmount"].ToString());
        //////        InvoiceHeaderObj.internalID = dt.Rows[i]["internalID"].ToString();
        //////        InvoiceHeaderObj.taxpayerActivityCode = dt.Rows[i]["taxpayerActivityCode"].ToString();
        //////        if (dt.Rows[i]["netAmount"].ToString() != "")
        //////            InvoiceHeaderObj.netAmount = Convert.ToDecimal(dt.Rows[i]["netAmount"].ToString());
        //////        if (dt.Rows[i]["totalAmount"].ToString() != "")
        //////            InvoiceHeaderObj.totalAmount = Convert.ToDecimal(dt.Rows[i]["totalAmount"].ToString());
        //////        if (dt.Rows[i]["totalDiscountAmount"].ToString() != "")
        //////            InvoiceHeaderObj.totalDiscountAmount = Convert.ToDecimal(dt.Rows[i]["totalDiscountAmount"].ToString());
        //////        if (dt.Rows[i]["totalItemsDiscountAmount"].ToString() != "")
        //////            InvoiceHeaderObj.totalItemsDiscountAmount = Convert.ToDecimal(dt.Rows[i]["totalItemsDiscountAmount"].ToString());
        //////        if (dt.Rows[i]["totalSalesAmount"].ToString() != "")
        //////            InvoiceHeaderObj.totalSalesAmount = Convert.ToDecimal(dt.Rows[i]["totalSalesAmount"].ToString());

        //////        //taxTotals
        //////        if (dt.Rows[i]["Taxtotalamout"].ToString() != "")
        //////            InvoiceHeaderObj.taxTotals__amount = Convert.ToDecimal(dt.Rows[i]["Taxtotalamout"].ToString());
        //////        InvoiceHeaderObj.taxTotals__taxType = dt.Rows[i]["Taxtype1"].ToString();


        //////        //issuer
        //////        InvoiceHeaderObj.issuer_id = dt.Rows[i]["IssuerId"].ToString();
        //////        InvoiceHeaderObj.issuer_name = dt.Rows[i]["issuername"].ToString();
        //////        InvoiceHeaderObj.issuer_type = dt.Rows[i]["IssuerType"].ToString();
        //////        InvoiceHeaderObj.issuer_branchID = dt.Rows[i]["issuerbranchID"].ToString();
        //////        InvoiceHeaderObj.issuer_buildingNumber = dt.Rows[i]["issuerbuildingNumber"].ToString();
        //////        InvoiceHeaderObj.issuer_country = dt.Rows[i]["issuercountry"].ToString();
        //////        InvoiceHeaderObj.issuer_governate = dt.Rows[i]["issuergovernate"].ToString();
        //////        InvoiceHeaderObj.issuer_regionCity = dt.Rows[i]["issuerregionCity"].ToString();
        //////        InvoiceHeaderObj.issuer_street = dt.Rows[i]["issuerstreet"].ToString();
        //////        //receiver
        //////        InvoiceHeaderObj.receiver_buildingNumber = dt.Rows[i]["receiverbuildingNumber"].ToString();
        //////        InvoiceHeaderObj.receiver_country = dt.Rows[i]["receivercountry"].ToString();
        //////        InvoiceHeaderObj.receiver_governate = dt.Rows[i]["receivergovernate"].ToString();
        //////        InvoiceHeaderObj.receiver_regionCity = dt.Rows[i]["receiverregionCity"].ToString();
        //////        InvoiceHeaderObj.receiver_street = dt.Rows[i]["receiverstreet"].ToString();
        //////        InvoiceHeaderObj.receiver_id = dt.Rows[i]["receiverid"].ToString();
        //////        InvoiceHeaderObj.receiver_name = dt.Rows[i]["receivername"].ToString();
        //////        InvoiceHeaderObj.receiver_type = dt.Rows[i]["receivertype"].ToString();
        //////        InvoiceHeaderObj.SendStatusId = 1;
        //////        InvoiceHeaderObj.signatureType = "I";

        //////        InvoiceHeader invoiceheaderId = new InvoiceHeader();
        //////        int InsertHeaderId = 0;
        //////        invoiceheaderId = InvoiceHeaderDBobj.GetRecordByInternalID(InvoiceHeaderObj.internalID);
        //////        if (invoiceheaderId != null)
        //////        {
        //////            if (invoiceheaderId.SendStatusId != 2)
        //////            {
        //////                InvoiceHeaderObj.SendStatusId = 1;
        //////                InvoiceHeaderObj.ID = invoiceheaderId.ID;
        //////                InvoiceHeaderObj.UpdateDate = DateTime.Now;
        //////                InvoiceHeaderObj.UpdateBy = 0;//User ID
        //////                bool updateflag = InvoiceHeaderDBobj.UpdateRecord(InvoiceHeaderObj);
        //////                InsertInvoiceLineId(dt.Rows[i], invoiceheaderId.ID);
        //////                InvoiceHeaderList.Add(InvoiceHeaderObj);
        //////            }
        //////        }
        //////        else
        //////        {
        //////            InvoiceHeaderObj.InsertDate = DateTime.Now;
        //////            InvoiceHeaderObj.InsertedBy = 0;//User ID
        //////            InsertHeaderId = InvoiceHeaderDBobj.SaveRecord(InvoiceHeaderObj);
        //////            if (InsertHeaderId > 0)
        //////            {

        //////                InsertInvoiceLineId(dt.Rows[i], InsertHeaderId);
        //////                InvoiceHeaderList.Add(InvoiceHeaderObj);
        //////            }
        //////        }

        //////    }
        //////    return InvoiceHeaderList;
        //////}
        private void InsertInvoiceLineId(DataRow dr, int InvoiceHeaderId)
        {
            InvoiceLined InvoiceLinedObj = new InvoiceLined();
            InvoiceLined InvoiceLinedObj2 = new InvoiceLined();
            InvoiceLinedDB InvoiceLinedDBObj = new InvoiceLinedDB();
            //invoiceLines
            InvoiceLinedObj.description = dr["invoiceLinesdescription"].ToString();
            //if (dt.Rows[i]["amount"] != null)
            //    OracleInvoiceObj.invoiceLines__discount__amount = Convert.ToDecimal(dt.Rows[i]["amount"].ToString());
            //if (dt.Rows[i]["invoiceLines__discount__rate"].ToString() != "")
            //    OracleInvoiceObj.invoiceLines__discount__rate = Convert.ToDecimal(dt.Rows[i]["invoiceLines_discount__rate"].ToString());
            InvoiceLinedObj.internalCode = dr["LineInternalcode"].ToString();
            InvoiceLinedObj.itemCode = dr["invoiceLinesitemCode"].ToString();
            if (dr["Itemsdiscount"].ToString() != "")
                InvoiceLinedObj.itemsDiscount = Convert.ToDecimal(dr["Itemsdiscount"].ToString());
            InvoiceLinedObj.itemType = dr["InvoicelinesItemtype"].ToString();
            if (dr["Nettotal"].ToString() != "")
                InvoiceLinedObj.netTotal = Convert.ToDecimal(dr["netTotal"].ToString());
            if (dr["InvoicelinesQuantity"].ToString() != "")
                InvoiceLinedObj.quantity = Convert.ToDecimal(dr["InvoicelinesQuantity"].ToString());
            if (dr["salesTotal"].ToString() != "")
                InvoiceLinedObj.salesTotal = Convert.ToDecimal(dr["salesTotal"].ToString());
            if (dr["Amount"].ToString() != "")
                InvoiceLinedObj.taxableItems_amount = Convert.ToDecimal(dr["Amount"].ToString());
           
                InvoiceLinedObj.taxableItems_rate = Convert.ToDecimal(0.00000);
            InvoiceLinedObj.taxableItems_subType = dr["subType"].ToString();
            InvoiceLinedObj.taxableItems_taxType = dr["taxType"].ToString();
            if (dr["total"].ToString() != "")
                InvoiceLinedObj.total = Convert.ToDecimal(dr["total"].ToString());
            if (dr["totalTaxableFees"].ToString() != "")
                InvoiceLinedObj.totalTaxableFees = Convert.ToDecimal(dr["totalTaxableFees"].ToString());
            InvoiceLinedObj.unitType = "IE";
            if (dr["InvoicelinesAmountegp"].ToString() != "")
                InvoiceLinedObj.unitValue_amountEGP = Convert.ToDecimal(dr["InvoicelinesAmountegp"].ToString());
          
                InvoiceLinedObj.unitValue_amountSold = Convert.ToDecimal(0.00000);
           
                InvoiceLinedObj.unitValue_currencyExchangeRate = Convert.ToDecimal(0.00000);
            InvoiceLinedObj.unitValue_currencySold = dr["InvoicelinesCurrencysold"].ToString();
            
                InvoiceLinedObj.valueDifference = Convert.ToDecimal(0.00000);
           
                InvoiceLinedObj.discount_amount =Convert.ToDecimal( 0.00000);
            InvoiceLinedObj.InvoiceHeaderID = InvoiceHeaderId;
            InvoiceLinedObj2 = InvoiceLinedDBObj.GetRecordByLineInternal(InvoiceLinedObj.internalCode);
            int id;
            bool updateflag;
            //if (InvoiceLinedObj2 == null)
            id = InvoiceLinedDBObj.SaveRecord(InvoiceLinedObj);
            //else
            //{
            //    InvoiceLinedObj.ID = InvoiceLinedObj2.ID;
            //    updateflag = InvoiceLinedDBObj.UpdateRecord(InvoiceLinedObj);
            //}
        }
        //private void InsertInvoiceLineId(DataRow dr, int InvoiceHeaderId)
        //{
        //    InvoiceLined InvoiceLinedObj = new InvoiceLined();
        //    InvoiceLined InvoiceLinedObj2 = new InvoiceLined();
        //    InvoiceLinedDB InvoiceLinedDBObj = new InvoiceLinedDB();
        //    //invoiceLines
        //    InvoiceLinedObj.description = dr["invoiceLinesdescription"].ToString();
        //    //if (dt.Rows[i]["amount"] != null)
        //    //    OracleInvoiceObj.invoiceLines__discount__amount = Convert.ToDecimal(dt.Rows[i]["amount"].ToString());
        //    //if (dt.Rows[i]["invoiceLines__discount__rate"].ToString() != "")
        //    //    OracleInvoiceObj.invoiceLines__discount__rate = Convert.ToDecimal(dt.Rows[i]["invoiceLines_discount__rate"].ToString());
        //    InvoiceLinedObj.internalCode = dr["LineInternalcode"].ToString();
        //    InvoiceLinedObj.itemCode = dr["invoiceLinesitemCode"].ToString();
        //    if (dr["Itemsdiscount"].ToString() != "")
        //        InvoiceLinedObj.itemsDiscount = Convert.ToDecimal(dr["Itemsdiscount"].ToString());
        //    InvoiceLinedObj.itemType = dr["InvoicelinesItemtype"].ToString();
        //    if (dr["Nettotal"].ToString() != "")
        //        InvoiceLinedObj.netTotal = Convert.ToDecimal(dr["netTotal"].ToString());
        //    if (dr["InvoicelinesQuantity"].ToString() != "")
        //        InvoiceLinedObj.quantity = Convert.ToDecimal(dr["InvoicelinesQuantity"].ToString());
        //    if (dr["salesTotal"].ToString() != "")
        //        InvoiceLinedObj.salesTotal = Convert.ToDecimal(dr["salesTotal"].ToString());
        //    if (dr["Amount"].ToString() != "")
        //        InvoiceLinedObj.taxableItems_amount = Convert.ToDecimal(dr["Amount"].ToString());
        //    if (dr["rate"].ToString() != "")
        //        InvoiceLinedObj.taxableItems_rate = Convert.ToDecimal(dr["rate"].ToString());
        //    InvoiceLinedObj.taxableItems_subType = dr["subType"].ToString();
        //    InvoiceLinedObj.taxableItems_taxType = dr["taxType"].ToString();
        //    if (dr["total"].ToString() != "")
        //        InvoiceLinedObj.total = Convert.ToDecimal(dr["total"].ToString());
        //    if (dr["totalTaxableFees"].ToString() != "")
        //        InvoiceLinedObj.totalTaxableFees = Convert.ToDecimal(dr["totalTaxableFees"].ToString());
        //    InvoiceLinedObj.unitType = dr["InvoicelinesUnittype"].ToString();
        //    if (dr["InvoicelinesAmountegp"].ToString() != "")
        //        InvoiceLinedObj.unitValue_amountEGP = Convert.ToDecimal(dr["InvoicelinesAmountegp"].ToString());
        //    if (dr["InvoicelinesAmountsold"].ToString() != "")
        //        InvoiceLinedObj.unitValue_amountSold = Convert.ToDecimal(dr["InvoicelinesAmountsold"].ToString());
        //    if (dr["Currencyexchangerate"].ToString() != "")
        //        InvoiceLinedObj.unitValue_currencyExchangeRate = Convert.ToDecimal(dr["Currencyexchangerate"].ToString());
        //    InvoiceLinedObj.unitValue_currencySold = dr["InvoicelinesCurrencysold"].ToString();
        //    if (dr["valueDifference"].ToString() != "")
        //        InvoiceLinedObj.valueDifference = Convert.ToDecimal(dr["valueDifference"].ToString());
        //    if (dr["DISCOUNT_AMOUNT"].ToString() != "")
        //        InvoiceLinedObj.discount_amount = Convert.ToDecimal(dr["DISCOUNT_AMOUNT"].ToString());
        //    InvoiceLinedObj.InvoiceHeaderID = InvoiceHeaderId;
        //    InvoiceLinedObj2 = InvoiceLinedDBObj.GetRecordByLineInternal(InvoiceLinedObj.internalCode);
        //    int id;
        //    bool updateflag;
        //    if (InvoiceLinedObj2 == null)
        //        id = InvoiceLinedDBObj.SaveRecord(InvoiceLinedObj);
        //    else
        //    {
        //        InvoiceLinedObj.ID = InvoiceLinedObj2.ID;
        //        updateflag = InvoiceLinedDBObj.UpdateRecord(InvoiceLinedObj);
        //    }
        //}
        private void getToken_Pin()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvoiceEF"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandType = CommandType.Text;
                    com.Connection = con;
                    com.CommandText = "SELECT *  FROM dbo.USB_Token ";

                    // Clear parameters to avoid adding multiple parameters on multiple calls
                    com.Parameters.Clear();

                    // Add parameter


                    con.Open();
                    using (SqlDataReader red = com.ExecuteReader())
                    {
                        if (red.Read())
                        {
                            textBox1.Text = red.GetValue(1).ToString();


                        }
                        red.Close();
                        con.Close();
                    }
                }
            }
        }
        private void updateSendStatuse()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvoiceEF"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandType = CommandType.Text;
                    com.Connection = con;
                    com.CommandText = "Update InvoiceHeader  set SendStatusId=1 ,erroeMessage=NULL where SendStatusId=3";

                    // Clear parameters to avoid adding multiple parameters on multiple calls
                    com.Parameters.Clear();

                    // Add parameter


                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        protected async void SendToETA_Click(object sender, EventArgs e)
        {

            //marqueeProgressBarControl1.Visible = true;
            CreatJason CreatJasonObj = new CreatJason();
            InvoiceHeaderDB _InvoiceHeaderDB = new InvoiceHeaderDB();

            List<InvoiceHeader> InvoiceHeaderList = _InvoiceHeaderDB.GetAllNotSend();

            if (InvoiceHeaderList.Count > 0)
            {
                lbltotalinv.Text = InvoiceHeaderList.Count.ToString();

                sendinv = "0";
                notSendinv = "0";

                List<List<InvoiceHeader>> InvoiceHeaderList2 = SplitList(InvoiceHeaderList, 20);

                foreach (List<InvoiceHeader> InvoiceHeaderList3 in InvoiceHeaderList2)
                {
                    List<JObject> JsonList = new List<JObject>();

                    foreach (InvoiceHeader InvoiceHeaderObj in InvoiceHeaderList3)
                    {
                        //try
                        //{
                        string jsonObj = CreatJasonObj.createJsonObj(InvoiceHeaderObj);

                        JObject jsonSigned = ApiFunctions.SignatureJson(textBox1.Text, InvoiceHeaderObj);

                        JsonList.Add(jsonSigned);
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show($"Error processing invoice {InvoiceHeaderObj.ID }: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    continue;
                        //}
                    }

                    string json = JsonConvert.SerializeObject(new { documents = JsonList }, Formatting.Indented);

                    MyWebRequest myRequest = new MyWebRequest("https://id.eta.gov.eg/connect/token", "POST", "grant_type=client_credentials&Client_id=44627186-a9af-444c-8666-e87d0590584e&Client_secret=d8fc20eb-a5b2-4ec4-b13e-37930cd1b567");

                    //try
                    //{
                    dynamic objReturn = await myRequest.GetLoginAsync(); // استخدام await للحصول على النتيجة الفعلية
                    string tokenType = objReturn.token_type;
                    string accessToken = objReturn.access_token;
                    textBox19.Text = accessToken;
                    List<CustomResponse> customResponseList = await PostData(accessToken, tokenType, json);

                    // حفظ ملف JSON
                    //using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    //{
                    //    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    //    saveFileDialog.Title = "Save JSON File";
                    //    saveFileDialog.FileName = "InvoiceData.json";

                    //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    //    {
                    //        string filePath = saveFileDialog.FileName;
                    //        File.WriteAllText(filePath, json);
                    //        MessageBox.Show($"JSON file saved to: {filePath}", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //}
                    //}
                    //catch (ArgumentOutOfRangeException ex)
                    //{
                    //    Console.WriteLine($"Argument out of range: {ex.ParamName}, {ex.ActualValue}");
                    //    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
            //marqueeProgressBarControl1.Visible = false;
            MessageBox.Show("تم ارسال الفواتير");
        }
        public async Task<List<CustomResponse>> PostDataDirect(string TokenKey, string token_type, string filePath)
        {
            marqueeProgressBarControl1.Visible = true;
            string JsonInvData = File.ReadAllText(filePath);
            var options = new RestClientOptions("https://api.invoicing.eta.gov.eg")
            {
                MaxTimeout = -1,
            };


            var client = new RestClient(options);
            var request = new RestRequest("/api/v1.0/documentsubmissions", Method.Post);

            request.AddHeader("Content-Type", "application/json");

            request.AddHeader("Authorization", token_type + " " + TokenKey);
            request.AddParameter("application/json", JsonInvData, ParameterType.RequestBody);

            RestResponse response = await client.ExecuteAsync(request);

            List<CustomResponse> customResponses = new List<CustomResponse>();

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                label5.Text = "Request Accepted: " + ((int)response.StatusCode).ToString() + " " + response.StatusDescription;
                MessageBox.Show("Done");
                customResponses = JsonConvert.DeserializeObject<List<CustomResponse>>(response.Content);
            }
            else
            {
                label5.Text = "Request Rejected: " + ((int)response.StatusCode).ToString() + " " + response.StatusDescription;
                richTextBox2.Text = "Error: " + response.Content;
            }

            return customResponses;
            marqueeProgressBarControl1.Visible = false;
        }

        public async Task<List<CustomResponse>> PostData(string TokenKey, string token_type, string JsonInvData)
        {
            marqueeProgressBarControl1.Visible = true;
            InvoiceHeaderDB Invoiceheaderdb = new InvoiceHeaderDB();
            var options = new RestClientOptions("https://api.invoicing.eta.gov.eg")
            {
                MaxTimeout = -1,
            };


            var client = new RestClient(options);
            var request = new RestRequest("/api/v1.0/documentsubmissions", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            request.AddHeader("Authorization", token_type + " " + TokenKey);
            request.AddParameter("application/json", JsonInvData, ParameterType.RequestBody);
            //request.AddParameter("application/json", JsonInvData, ParameterType.RequestBody);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //var response = client.Execute(request);
            RestResponse response = await client.ExecuteAsync(request);
            EtaReponce EtaReponceObj = new EtaReponce();
            EtaReponceDB etaresponcedb = new EtaReponceDB();
            label5.Text = "Request Accepted: " + ((int)response.StatusCode).ToString() + " " + response.StatusDescription;

            //Convert respone to json
            //Convert respone to json
            Outputs json = JsonConvert.DeserializeObject<Outputs>(response.Content);
            //       var json2 = JsonConvert.DeserializeObject<CustomResponse>(response.Content);
            // int i = json
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                label5.Text = "Request Accepted: " + ((int)response.StatusCode).ToString() + " " + response.StatusDescription;
                MessageBox.Show("تم الارسال");
            }
            else
            {
                label5.Text = "Request Rejected: " + ((int)response.StatusCode).ToString() + " " + response.StatusDescription;
                MessageBox.Show("Request Rejected: " + ((int)response.StatusCode).ToString() + " " + response.StatusDescription);
            }
            CustomResponse customResponse = new CustomResponse();
            List<CustomResponse> CustomResponseList = new List<CustomResponse>();
            List<ErrorDetail> ErrorDetaillist = new List<ErrorDetail>();
            if (json?.acceptedDocuments != null && json.acceptedDocuments.Any())
            {
                foreach (AcceptedDocuments AcceptedDocumentsobj in json.acceptedDocuments)
                {

                    InvoiceHeader InvoiceHeaderobj = new InvoiceHeader();
                    InvoiceHeaderobj = Invoiceheaderdb.GetRecordByInternalID(AcceptedDocumentsobj.internalId);
                    if (InvoiceHeaderobj != null)
                    {
                        InvoiceHeaderobj.uuid = AcceptedDocumentsobj.uuid;
                        InvoiceHeaderobj.submissionID = json.submissionUUID;
                        InvoiceHeaderobj.longId = AcceptedDocumentsobj.longId;
                        InvoiceHeaderobj.SendStatusId = 2;
                        bool updatef = Invoiceheaderdb.UpdateRecord(InvoiceHeaderobj);
                        customResponse.longId = AcceptedDocumentsobj.longId;
                        customResponse.uuid = AcceptedDocumentsobj.uuid;
                        customResponse.internalId = AcceptedDocumentsobj.internalId;
                        CustomResponseList.Add(customResponse);
                        sendinv = sendinv + 1;
                    }

                }
            }
            if (json?.rejectedDocuments != null && json.rejectedDocuments.Any())
            {
                foreach (DocumentRejected DocumentRejectedobj in json.rejectedDocuments)
                {
                    InvoiceHeader InvoiceHeaderobj = new InvoiceHeader();
                    InvoiceHeaderobj = Invoiceheaderdb.GetRecordByInternalID(DocumentRejectedobj.internalId);
                    if (InvoiceHeaderobj != null)
                    {
                        InvoiceHeaderobj.erroeMessage = DocumentRejectedobj.error.message;
                        ErrorDetailDB ErrorDetaildb = new ErrorDetailDB();
                        bool update = ErrorDetaildb.updateRecord(DocumentRejectedobj.internalId);
                        for (int i = 0; i < DocumentRejectedobj.error.details.Count(); i++)
                        {
                            ErrorDetail ErrorDetailObj = new ErrorDetail();
                            ErrorDetailObj.InvoiceHeaderId = int.Parse(InvoiceHeaderobj.ID.ToString());
                            ErrorDetailObj.MessageDetail = DocumentRejectedobj.error.details[i].message;
                            ErrorDetailObj.target = DocumentRejectedobj.error.details[i].target;
                            ErrorDetailObj.Code = DocumentRejectedobj.error.details[i].code;
                            ErrorDetailObj.InternalId = DocumentRejectedobj.internalId;
                            ErrorDetailObj.Insertby = 0;
                            ErrorDetailObj.insertDate = DateTime.Now;
                            int inserte = ErrorDetaildb.SaveRecord(ErrorDetailObj);
                            //if (inserte > 0)
                            //{
                            //    ErrorDetaillist.Add(ErrorDetailObj);

                            //}
                        }

                        InvoiceHeaderobj.SendStatusId = 3;
                        notSendinv = notSendinv + 1;
                        bool updatef = Invoiceheaderdb.UpdateRecord(InvoiceHeaderobj);
                    }
                }
            }

            marqueeProgressBarControl1.Visible = false;

            // string prettyJson = JsonConvert.SerializeObject(
            //JsonConvert.DeserializeObject(response.Request.Body.Value?.ToString()),
            //Formatting.Indented);



            Lblsendinv.Text = sendinv.ToString();
            LblNotSendInv.Text = notSendinv.ToString();
            lbltotalinv.Text = (sendinv + notSendinv).ToString();
            BindGriederror();
            return CustomResponseList;

        }
        private void BindGriederror()
        {
            ErrorDetailDB _ErrorDetailDB = new ErrorDetailDB();
            List<ErrorDetail> ErrorDetaillist = new List<ErrorDetail>();
            ErrorDetaillist = _ErrorDetailDB.GetAllCurrantErrordetail();
            //if (ErrorDetaillist.Count > 0)
            //{
            //    DontSendGrid.DataKeyNames = new string[] { "ID" };
            //    DontSendGrid.DataSource = ErrorDetaillist;
            //    DontSendGrid.DataBind();
            //}

        }
        public CustomResponse PostDataObj(string TokenKey, string token_type, string JsonInvData)
        {
            InvoiceHeaderDB Invoiceheaderdb = new InvoiceHeaderDB();
            string UrlStr = "https://api.invoicing.eta.gov.eg";
            string url = UrlStr + "/api/v1.0/documentsubmissions";
            //var client = new RestClient(url);
            var options = new RestClientOptions(url)
            {
                Timeout = TimeSpan.FromMinutes(-1)
            };

            var client = new RestClient(options);
            var request = new RestRequest
            {
                Method = Method.Post
            };
            // var client = new RestClient("https://api.invoicing.eta.gov.eg/api/v1.0/documentsubmissions");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token_type + " " + TokenKey);
            request.AddParameter("application/json", JsonInvData, ParameterType.RequestBody);
            var response = client.Execute(request);
            EtaReponce EtaReponceObj = new EtaReponce();
            EtaReponceDB etaresponcedb = new EtaReponceDB();



            dynamic json = JsonConvert.DeserializeObject(response.Content);
            CustomResponse customResponse = new CustomResponse();
            if (json["submissionId"] != null)
            {
                customResponse.submissionID = json["submissionId"];
                customResponse.uuid = json["acceptedDocuments"][0]["uuid"];
                customResponse.longId = json["acceptedDocuments"][0]["longId"];
                customResponse.hashKey = json["acceptedDocuments"][0]["hashKey"];
            }
            else
            {
                customResponse.errorCode = json["rejectedDocuments"][0]["error"]["code"];
                customResponse.erroeMessage = json["rejectedDocuments"][0]["error"]["message"];
                customResponse.errorTarget = json["rejectedDocuments"][0]["error"]["target"];
            }
            return customResponse;
        }
        static int countOccurences(string str, string word)
        {
            string[] a = str.Split('\"');

            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {

                if (word.Equals(a[i]))
                    count++;
            }

            return count;
        }

        private void ShowStatus(List<CustomResponse> customResponseList)
        {
            InvoiceHeaderDB _InvoiceHeaderDB = new InvoiceHeaderDB();
            System.Text.StringBuilder sbAccept = new System.Text.StringBuilder();
            //StringBuilder sberror = new StringBuilder();
            sbAccept.Append("<TABLE  class=\"table table - striped table - bordered table - hover\">\n");
            sbAccept.Append("<th>internalId</th>");
            sbAccept.Append("<th>submissionId</th>");
            sbAccept.Append("<th>uuid</th>");
            //sbAccept.Append("<th>longId</th>");
            //sbAccept.Append("<th>hashKey</th>");
            sbAccept.Append("</tr>\n");

            //sberror.Append("<TABLE class=\"table table - striped table - bordered table - hover\">\n");

            //sberror.Append("<tr class=\"thead -light\">\n");
            //sberror.Append("<th>errorCode</th>");
            //sberror.Append("<th>erroeMessage</th>");
            //sberror.Append("<th>errorTarget</th>");
            //sberror.Append("<th>errordetailsmessage</th>");
            //sberror.Append("<th>errordetailsTargt</th>");
            //sberror.Append("</tr>\n");
            foreach (CustomResponse customResponseObj in customResponseList)
            {

                if (customResponseObj.uuid != null)
                {

                    sbAccept.Append("<tr>\n");
                    sbAccept.Append("<td>" + customResponseObj.internalId + "</td>");
                    sbAccept.Append("<td>" + customResponseObj.submissionID + "</td>");
                    sbAccept.Append("<td>" + customResponseObj.uuid + "</td>");
                    //sbAccept.Append("<td>" + customResponseObj.longId + "</td>");
                    //sbAccept.Append("<td>" + customResponseObj.hashKey + "</td>");
                    sbAccept.Append("<\tr>\n");
                    //   _InvoiceHeaderDB.UpdateRecordByInternalId(customResponseObj.internalId, 2, customResponseObj);
                }
                //else
                //{
                //    sberror.Append("<tr>\n");
                //    sberror.Append("<td>" + customResponseObj.errorCode + "</td>");
                //    sberror.Append("<td>" + customResponseObj.erroeMessage + "</td>");
                //    sberror.Append("<td>" + customResponseObj.errorTarget + "</td>");
                //    sberror.Append("<td>" + customResponseObj.errordetailsmessage + "</td>");
                //    sberror.Append("<td>" + customResponseObj.errordetailsTargt + "</td>");
                //    sberror.Append("</tr>\n");

                //    _InvoiceHeaderDB.UpdateRecordByInternalId(customResponseObj.errorTarget, 3, customResponseObj);
                //}
            }

            sbAccept.Append("</TABLE>\n");
            //sberror.Append("</TABLE>\n");

            //  diverror.InnerHtml = sberror.ToString();
        }


        public static List<List<InvoiceHeader>> SplitList(List<InvoiceHeader> locations, int nSize)
        {
            var list = new List<List<InvoiceHeader>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }
        DataTableCollection tableCoolection;
        public DataTable ReadExcel(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            DataTable dt = new DataTable();

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, System.IO.FileAccess.Read))
                {
                    IExcelDataReader excelReader;

                    string fileExtension = Path.GetExtension(filePath).ToLower();
                    if (fileExtension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        throw new NotSupportedException("File format is not supported.");
                    }

                    var config = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    DataSet result = excelReader.AsDataSet(config);
                    if (result.Tables.Count > 0)
                    {
                        dt = result.Tables[0];
                    }
                    else
                    {
                        throw new Exception("No tables found in the Excel file.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while reading the Excel file.", ex);
            }

            return dt;
        }


        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //marqueeProgressBarControl1.Visible=true;
            SendToETA_Click(sender, e);
            this.invoiceHeader1TableAdapter.FillNot(this.excell.InvoiceHeader1);
            this.invoiceHeaderTableAdapter.FillByOK(this.excell1.InvoiceHeader);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DeleteDD1();
                    DeleteDD();
                    string filePath = openFileDialog.FileName;
                    string connectionString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString.ToString();


                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


                    using (var stream = File.Open(filePath, FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet();
                            DataTable sheet1 = result.Tables[0];
                            DataTable sheet2 = result.Tables[1];
                           
                            SaveDocumentsToDatabase(sheet1, connectionString);
                            SaveItemsToDatabase(sheet2, connectionString);
                        }
                    }
                }


            }

            DeleteExcelltb();
           
            InsertData();
            this.excelTableTableAdapter.Fill(this.excell.ExcelTable);
            
            UploadFromDatabase();
            updateSendStatuse();

        }
        public void DeleteDD()
        {

            string conString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString;




           

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {

                    con.Open();

                    SqlCommand com = new SqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = "delete FROM dbo.Documents";
                    com.ExecuteNonQuery();

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message;
            }
        }
        public void DeleteDD1()
        {

            string conString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString;






            //try
            //{
                using (SqlConnection con = new SqlConnection(conString))
                {

                    con.Open();

                    SqlCommand com = new SqlCommand();
                com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = "delete FROM dbo.Items";
                    com.ExecuteNonQuery();

                    con.Close();
                }


            //}
            //catch (Exception ex)
            //{
            //    Label1.Text = "Error: " + ex.Message;
            //}
        }
        public void DeleteExcelltb()
        {

            string conString = ConfigurationManager.ConnectionStrings["InvoiceEF"].ConnectionString;




            string selectQuery = "delete FROM dbo.ExcelTable";

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {

                    con.Open();

                    SqlCommand adapter = new SqlCommand(selectQuery, con);
                    adapter.ExecuteNonQuery();

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message;
            }
        }
        //static void SaveDocumentsToDatabase(DataTable table, string connectionString)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        foreach (DataRow row in table.Rows)
        //        {
        //            if (row[0].ToString() == "DocumentId")
        //            {
        //                continue;
        //            }
        //            SqlCommand cmd = new SqlCommand(
        //                @"INSERT INTO Documents (
        //                DocumentId, UUID, TypeOfDocument_Id, TypeOfDocumentVersion_Id, dateTimeIssued, 
        //                taxpayerActivityCode, Internal_ID, Id_Suppliers_Clint, Suppliers_Clint_Branch_Id, 
        //                ReceiverTo_RegistrationNumber, branchID, country, governate, regionCity, street, 
        //                buildingNumber, postalCode, floor, room, landmark, additionalInformation, 
        //                purchaseOrderReference, purchaseOrderDescription, salesOrderReference, 
        //                salesOrderDescription, proformaInvoiceNumber, bankName, bankAddress, bankAccountNo, 
        //                bankAccountIBAN, swiftCode, terms, approach, packaging, dateValidity, exportPort, 
        //                grossWeight, netWeight, delivery_terms, totalDiscountAmount, totalSalesAmount, 
        //                netAmount, taxTotals, totalAmount, extraDiscountAmount, totalItemsDiscountAmount
        //            ) VALUES (
        //                @DocumentId, @UUID, @TypeOfDocument_Id, @TypeOfDocumentVersion_Id, @dateTimeIssued, 
        //                @taxpayerActivityCode, @Internal_ID, @Id_Suppliers_Clint, @Suppliers_Clint_Branch_Id, 
        //                @ReceiverTo_RegistrationNumber, @branchID, @country, @governate, @regionCity, @street, 
        //                @buildingNumber, @postalCode, @floor, @room, @landmark, @additionalInformation, 
        //                @purchaseOrderReference, @purchaseOrderDescription, @salesOrderReference, 
        //                @salesOrderDescription, @proformaInvoiceNumber, @bankName, @bankAddress, @bankAccountNo, 
        //                @bankAccountIBAN, @swiftCode, @terms, @approach, @packaging, @dateValidity, @exportPort, 
        //                @grossWeight, @netWeight, @delivery_terms, @totalDiscountAmount, @totalSalesAmount, 
        //                @netAmount, @taxTotals, @totalAmount, @extraDiscountAmount, @totalItemsDiscountAmount
        //            )", conn);

        //            cmd.Parameters.AddWithValue("@DocumentId", row[0]);
        //            cmd.Parameters.AddWithValue("@UUID", row[1]);
        //            cmd.Parameters.AddWithValue("@TypeOfDocument_Id", row[2]);
        //            cmd.Parameters.AddWithValue("@TypeOfDocumentVersion_Id", row[3]);
        //            cmd.Parameters.AddWithValue("@dateTimeIssued", DateTime.Now.ToShortDateString());
        //            cmd.Parameters.AddWithValue("@taxpayerActivityCode", row[5]);
        //            cmd.Parameters.AddWithValue("@Internal_ID", row[6]);
        //            cmd.Parameters.AddWithValue("@Id_Suppliers_Clint", row[7]);
        //            cmd.Parameters.AddWithValue("@Suppliers_Clint_Branch_Id", row[8]);
        //            cmd.Parameters.AddWithValue("@ReceiverTo_RegistrationNumber", row[9]);
        //            cmd.Parameters.AddWithValue("@branchID", row[10]);
        //            cmd.Parameters.AddWithValue("@country", row[11]);
        //            cmd.Parameters.AddWithValue("@governate", row[12]);
        //            cmd.Parameters.AddWithValue("@regionCity", row[13]);
        //            cmd.Parameters.AddWithValue("@street", row[14]);
        //            cmd.Parameters.AddWithValue("@buildingNumber", row[15]);
        //            cmd.Parameters.AddWithValue("@postalCode", row[16]);
        //            cmd.Parameters.AddWithValue("@floor", row[17]);
        //            cmd.Parameters.AddWithValue("@room", row[18]);
        //            cmd.Parameters.AddWithValue("@landmark", row[19]);
        //            cmd.Parameters.AddWithValue("@additionalInformation", row[20]);
        //            cmd.Parameters.AddWithValue("@purchaseOrderReference", row[21]);
        //            cmd.Parameters.AddWithValue("@purchaseOrderDescription", row[22]);
        //            cmd.Parameters.AddWithValue("@salesOrderReference", row[23]);
        //            cmd.Parameters.AddWithValue("@salesOrderDescription", row[24]);
        //            cmd.Parameters.AddWithValue("@proformaInvoiceNumber", row[25]);
        //            cmd.Parameters.AddWithValue("@bankName", row[26]);
        //            cmd.Parameters.AddWithValue("@bankAddress", row[27]);
        //            cmd.Parameters.AddWithValue("@bankAccountNo", row[28]);
        //            cmd.Parameters.AddWithValue("@bankAccountIBAN", row[29]);
        //            cmd.Parameters.AddWithValue("@swiftCode", row[30]);
        //            cmd.Parameters.AddWithValue("@terms", row[31]);
        //            cmd.Parameters.AddWithValue("@approach", row[32]);
        //            cmd.Parameters.AddWithValue("@packaging", row[33]);
        //            cmd.Parameters.AddWithValue("@dateValidity", DateTime.Now.ToShortDateString());
        //            cmd.Parameters.AddWithValue("@exportPort", row[35]);
        //            cmd.Parameters.AddWithValue("@grossWeight", row[36]);
        //            cmd.Parameters.AddWithValue("@netWeight", row[37]);
        //            cmd.Parameters.AddWithValue("@delivery_terms", row[38]);
        //            cmd.Parameters.AddWithValue("@totalDiscountAmount", row[39]);
        //            cmd.Parameters.AddWithValue("@totalSalesAmount", row[40]);
        //            cmd.Parameters.AddWithValue("@netAmount", row[41]);
        //            cmd.Parameters.AddWithValue("@taxTotals", row[42]);
        //            cmd.Parameters.AddWithValue("@totalAmount", row[43]);
        //            cmd.Parameters.AddWithValue("@extraDiscountAmount", row[44]);
        //            cmd.Parameters.AddWithValue("@totalItemsDiscountAmount", row[45]);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        static void SaveDocumentsToDatabase(DataTable table, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataRow row in table.Rows)
                {
                    if (row[0].ToString() == "DocumentId")
                    {
                        continue;
                    }

                    // Check if DocumentId already exists
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Documents WHERE DocumentId = @DocumentId", conn);
                    checkCmd.Parameters.AddWithValue("@DocumentId", row[0]);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        // DocumentId already exists, skip the insertion
                        continue;
                    }

                    // Proceed with insertion if DocumentId does not exist
                    SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO Documents (
                DocumentId, UUID, TypeOfDocument_Id, TypeOfDocumentVersion_Id, dateTimeIssued, 
                taxpayerActivityCode, Internal_ID, Id_Suppliers_Clint, Suppliers_Clint_Branch_Id, 
                ReceiverTo_RegistrationNumber, branchID, country, governate, regionCity, street, 
                buildingNumber, postalCode, floor, room, landmark, additionalInformation, 
                purchaseOrderReference, purchaseOrderDescription, salesOrderReference, 
                salesOrderDescription, proformaInvoiceNumber, bankName, bankAddress, bankAccountNo, 
                bankAccountIBAN, swiftCode, terms, approach, packaging, dateValidity, exportPort, 
                grossWeight, netWeight, delivery_terms, totalDiscountAmount, totalSalesAmount, 
                netAmount, taxTotals, totalAmount, extraDiscountAmount, totalItemsDiscountAmount
            ) VALUES (
                @DocumentId, @UUID, @TypeOfDocument_Id, @TypeOfDocumentVersion_Id, @dateTimeIssued, 
                @taxpayerActivityCode, @Internal_ID, @Id_Suppliers_Clint, @Suppliers_Clint_Branch_Id, 
                @ReceiverTo_RegistrationNumber, @branchID, @country, @governate, @regionCity, @street, 
                @buildingNumber, @postalCode, @floor, @room, @landmark, @additionalInformation, 
                @purchaseOrderReference, @purchaseOrderDescription, @salesOrderReference, 
                @salesOrderDescription, @proformaInvoiceNumber, @bankName, @bankAddress, @bankAccountNo, 
                @bankAccountIBAN, @swiftCode, @terms, @approach, @packaging, @dateValidity, @exportPort, 
                @grossWeight, @netWeight, @delivery_terms, @totalDiscountAmount, @totalSalesAmount, 
                @netAmount, @taxTotals, @totalAmount, @extraDiscountAmount, @totalItemsDiscountAmount
            )", conn);

                    cmd.Parameters.AddWithValue("@DocumentId", row[0]);
                    cmd.Parameters.AddWithValue("@UUID", row[1]);
                    cmd.Parameters.AddWithValue("@TypeOfDocument_Id", row[2]);
                    cmd.Parameters.AddWithValue("@TypeOfDocumentVersion_Id", row[3]);
                    cmd.Parameters.AddWithValue("@dateTimeIssued", DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@taxpayerActivityCode", row[5]);
                    cmd.Parameters.AddWithValue("@Internal_ID", row[6]);
                    cmd.Parameters.AddWithValue("@Id_Suppliers_Clint", row[7]);
                    cmd.Parameters.AddWithValue("@Suppliers_Clint_Branch_Id", row[8]);
                    cmd.Parameters.AddWithValue("@ReceiverTo_RegistrationNumber", row[9]);
                    cmd.Parameters.AddWithValue("@branchID", row[10]);
                    cmd.Parameters.AddWithValue("@country", row[11]);
                    cmd.Parameters.AddWithValue("@governate", row[12]);
                    cmd.Parameters.AddWithValue("@regionCity", row[13]);
                    cmd.Parameters.AddWithValue("@street", row[14]);
                    cmd.Parameters.AddWithValue("@buildingNumber", row[15]);
                    cmd.Parameters.AddWithValue("@postalCode", row[16]);
                    cmd.Parameters.AddWithValue("@floor", row[17]);
                    cmd.Parameters.AddWithValue("@room", row[18]);
                    cmd.Parameters.AddWithValue("@landmark", row[19]);
                    cmd.Parameters.AddWithValue("@additionalInformation", row[20]);
                    cmd.Parameters.AddWithValue("@purchaseOrderReference", row[21]);
                    cmd.Parameters.AddWithValue("@purchaseOrderDescription", row[22]);
                    cmd.Parameters.AddWithValue("@salesOrderReference", row[23]);
                    cmd.Parameters.AddWithValue("@salesOrderDescription", row[24]);
                    cmd.Parameters.AddWithValue("@proformaInvoiceNumber", row[25]);
                    cmd.Parameters.AddWithValue("@bankName", row[26]);
                    cmd.Parameters.AddWithValue("@bankAddress", row[27]);
                    cmd.Parameters.AddWithValue("@bankAccountNo", row[28]);
                    cmd.Parameters.AddWithValue("@bankAccountIBAN", row[29]);
                    cmd.Parameters.AddWithValue("@swiftCode", row[30]);
                    cmd.Parameters.AddWithValue("@terms", row[31]);
                    cmd.Parameters.AddWithValue("@approach", row[32]);
                    cmd.Parameters.AddWithValue("@packaging", row[33]);
                    cmd.Parameters.AddWithValue("@dateValidity", DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@exportPort", row[35]);
                    cmd.Parameters.AddWithValue("@grossWeight", row[36]);
                    cmd.Parameters.AddWithValue("@netWeight", row[37]);
                    cmd.Parameters.AddWithValue("@delivery_terms", row[38]);
                    cmd.Parameters.AddWithValue("@totalDiscountAmount", row[39]);
                    cmd.Parameters.AddWithValue("@totalSalesAmount", row[40]);
                    cmd.Parameters.AddWithValue("@netAmount", row[41]);
                    cmd.Parameters.AddWithValue("@taxTotals", row[42]);
                    cmd.Parameters.AddWithValue("@totalAmount", row[43]);
                    cmd.Parameters.AddWithValue("@extraDiscountAmount", row[44]);
                    cmd.Parameters.AddWithValue("@totalItemsDiscountAmount", row[45]);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        static void SaveItemsToDatabase(DataTable table, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataRow row in table.Rows)
                {
                    if (row[0].ToString() == "DocumentId")
                    {
                        continue;
                    }

                    // Check if Items_Id already exists
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Items WHERE DocumentId = @DocumentId and total <> total", conn);
                    checkCmd.Parameters.AddWithValue("@DocumentId", row[0]);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        // Items_Id already exists, skip the insertion
                        continue;
                    }

                    // Proceed with insertion if Items_Id does not exist
                    SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO Items (
                Items_Id, DocumentId, itemCode, itemType, Unit_Id, 
                quantity, amountEGP, currencySold, internalCode, 
                description, rate, amount, salesTotal, totalTaxableFees, 
                itemsDiscount, valueDifference, netTotal, total
            ) VALUES (
                @Items_Id, @DocumentId, @itemCode, @itemType, @Unit_Id, 
                @quantity, @amountEGP, @currencySold, @internalCode, 
                @description, @rate, @amount, @salesTotal, @totalTaxableFees, 
                @itemsDiscount, @valueDifference, @netTotal, @total
            )", conn);

                    cmd.Parameters.AddWithValue("@Items_Id", row[1]);
                    cmd.Parameters.AddWithValue("@DocumentId", row[0]);
                    cmd.Parameters.AddWithValue("@itemCode", row[3]);
                    cmd.Parameters.AddWithValue("@itemType", row[4]);
                    cmd.Parameters.AddWithValue("@Unit_Id", row[5]);
                    cmd.Parameters.AddWithValue("@quantity", row[6]);
                    cmd.Parameters.AddWithValue("@amountEGP", row[7]);
                    cmd.Parameters.AddWithValue("@currencySold", row[8]);
                    cmd.Parameters.AddWithValue("@internalCode", row[9]);
                    cmd.Parameters.AddWithValue("@description", row[10]);
                    cmd.Parameters.AddWithValue("@rate", row[11]);
                    cmd.Parameters.AddWithValue("@amount", row[12]);
                    cmd.Parameters.AddWithValue("@salesTotal", row[13]);
                    cmd.Parameters.AddWithValue("@totalTaxableFees", row[14]);
                    cmd.Parameters.AddWithValue("@itemsDiscount", row[15]);
                    cmd.Parameters.AddWithValue("@valueDifference", row[16]);
                    cmd.Parameters.AddWithValue("@netTotal", row[17]);
                    cmd.Parameters.AddWithValue("@total", row[18]);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //static void SaveItemsToDatabase(DataTable table, string connectionString)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        foreach (DataRow row in table.Rows)
        //        {
        //            if (row[0].ToString() == "DocumentId")
        //            {
        //                continue;
        //            }
        //            SqlCommand cmd = new SqlCommand(
        //                @"INSERT INTO Items (
        //                Items_Id, DocumentId, itemCode, itemType, Unit_Id, 
        //                quantity, amountEGP, currencySold, internalCode, 
        //                description, rate, amount, salesTotal, totalTaxableFees, 
        //                itemsDiscount, valueDifference, netTotal, total
        //            ) VALUES (
        //                @Items_Id, @DocumentId, @itemCode, @itemType, @Unit_Id, 
        //                @quantity, @amountEGP, @currencySold, @internalCode, 
        //                @description, @rate, @amount, @salesTotal, @totalTaxableFees, 
        //                @itemsDiscount, @valueDifference, @netTotal, @total
        //            )", conn);

        //            cmd.Parameters.AddWithValue("@Items_Id", row[1]);
        //            cmd.Parameters.AddWithValue("@DocumentId", row[0]);
        //            cmd.Parameters.AddWithValue("@itemCode", row[3]);
        //            cmd.Parameters.AddWithValue("@itemType", row[4]);
        //            cmd.Parameters.AddWithValue("@Unit_Id", row[5]);
        //            cmd.Parameters.AddWithValue("@quantity", row[6]);
        //            cmd.Parameters.AddWithValue("@amountEGP", row[7]);
        //            cmd.Parameters.AddWithValue("@currencySold", row[8]);
        //            cmd.Parameters.AddWithValue("@internalCode", row[9]);
        //            cmd.Parameters.AddWithValue("@description", row[10]);
        //            cmd.Parameters.AddWithValue("@rate", row[11]);
        //            cmd.Parameters.AddWithValue("@amount", row[12]);
        //            cmd.Parameters.AddWithValue("@salesTotal", row[13]);
        //            cmd.Parameters.AddWithValue("@totalTaxableFees", row[14]);
        //            cmd.Parameters.AddWithValue("@itemsDiscount", row[15]);
        //            cmd.Parameters.AddWithValue("@valueDifference", row[16]);
        //            cmd.Parameters.AddWithValue("@netTotal", row[17]);
        //            cmd.Parameters.AddWithValue("@total", row[18]);


        //            cmd.ExecuteNonQuery();
        //        }
        //    }

        //}
        private void insertToExcellTbl()
        {
            string connectionString = "YourConnectionStringHere";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    string fetchInvoiceQuery = @"
                    SELECT         d.DocumentId, d.UUID, d.TypeOfDocument_Id, d.TypeOfDocumentVersion_Id, d.dateTimeIssued, d.taxpayerActivityCode, d.Internal_ID, d.Id_Suppliers_Clint, d.Suppliers_Clint_Branch_Id, 
                         d.ReceiverTo_RegistrationNumber, d.branchID, d.country, d.governate, d.regionCity, d.street, d.buildingNumber, d.postalCode, d.floor, d.room, d.landmark, d.additionalInformation, d.purchaseOrderReference, 
                         d.purchaseOrderDescription, d.salesOrderReference, d.salesOrderDescription, d.proformaInvoiceNumber, d.bankName, d.bankAddress, d.bankAccountNo, d.bankAccountIBAN, d.swiftCode, d.terms, d.approach, d.packaging, 
                         d.dateValidity, d.exportPort, d.grossWeight, d.netWeight, d.delivery_terms, d.totalDiscountAmount, d.totalSalesAmount, d.netAmount, d.taxTotals, d.totalAmount, d.extraDiscountAmount, d.totalItemsDiscountAmount, i.Items_Id, 
                         i.itemCode, i.itemType, i.Unit_Id, i.quantity, i.amountEGP, i.currencySold, i.internalCode, i.description, i.rate, i.amount, i.salesTotal, i.totalTaxableFees, i.itemsDiscount, i.valueDifference, i.netTotal, i.total, i.description AS Expr1, 
                         dbo.Tbl_ItemsOfTax.CodeName as CodeName
FROM            dbo.Documents AS d INNER JOIN
                         dbo.Items AS i ON d.DocumentId = i.DocumentId INNER JOIN
                         dbo.Tbl_ItemsOfTax ON i.itemCode = dbo.Tbl_ItemsOfTax.UniqueNumber
ORDER BY d.ReceiverTo_RegistrationNumber";

                    using (SqlCommand fetchCmd = new SqlCommand(fetchInvoiceQuery, connection, transaction))
                    {

                        fetchCmd.Parameters.AddWithValue("@DocumentId", "documentId");
                        using (SqlDataReader reader = fetchCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string insertQuery = @"
                                INSERT INTO ExcelTable (
                                    IssuerType, IssuerId, IssuerName, IssuerBranchid, IssuerCountry, IssuerGovernate, IssuerRegioncity,
                                    IssuerStreet, IssuerBuildingnumber, ReceiverType, ReceiverId, ReceiverName, ReceiverCountry, 
                                    ReceiverGovernate, ReceiverRegioncity, ReceiverStreet, ReceiverBuildingnumber, Documenttype,
                                    Documenttypeversion, Datetimeissued, Taxpayeractivitycode, Internalid, InvoicelinesDescription, 
                                    InvoicelinesItemtype, InvoicelinesItemcode, InvoicelinesUnittype, InvoicelinesQuantity, 
                                    InvoicelinesCurrencysold, InvoicelinesAmountegp, InvoicelinesAmountsold, Currencyexchangerate,
                                    Salestotal, Total, Valuedifference, Totaltaxablefees, Nettotal, Itemsdiscount, DISCOUNT_AMOUNT, 
                                    Taxtype, Amount, Subtype, Rate, LineInternalcode, Totalsalesamount, Totaldiscountamount, Netamount, 
                                    Taxtype1, Taxtotalamout, Extradiscountamount, Totalitemsdiscountamount, Totalamount, 
                                    PURCHASEORDERREFERENCE, PURCHASEORDERDESCRIPTION, SALESORDERREFERENCE, SALESORDERDESCRIPTION
                                )
                                VALUES (
                                    @IssuerType, @IssuerId, @IssuerName, @IssuerBranchid, @IssuerCountry, @IssuerGovernate, @IssuerRegioncity,
                                    @IssuerStreet, @IssuerBuildingnumber, @ReceiverType, @ReceiverId, @ReceiverName, @ReceiverCountry, 
                                    @ReceiverGovernate, @ReceiverRegioncity, @ReceiverStreet, @ReceiverBuildingnumber, @Documenttype,
                                    @Documenttypeversion, @Datetimeissued, @Taxpayeractivitycode, @Internalid, @InvoicelinesDescription, 
                                    @InvoicelinesItemtype, @InvoicelinesItemcode, @InvoicelinesUnittype, @InvoicelinesQuantity, 
                                    @InvoicelinesCurrencysold, @InvoicelinesAmountegp, @InvoicelinesAmountsold, @Currencyexchangerate,
                                    @Salestotal, @Total, @Valuedifference, @Totaltaxablefees, @Nettotal, @Itemsdiscount, @DISCOUNT_AMOUNT, 
                                    @Taxtype, @Amount, @Subtype, @Rate, @LineInternalcode, @Totalsalesamount, @Totaldiscountamount, @Netamount, 
                                    @Taxtype1, @Taxtotalamout, @Extradiscountamount, @Totalitemsdiscountamount, @Totalamount, 
                                    @PURCHASEORDERREFERENCE, @PURCHASEORDERDESCRIPTION, @SALESORDERREFERENCE, @SALESORDERDESCRIPTION
                                )";

                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                                {
                                    insertCmd.Parameters.AddWithValue("@IssuerType", reader["IssuerType"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerId", reader["IssuerId"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerName", reader["IssuerName"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerBranchid", reader["IssuerBranchid"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerCountry", reader["IssuerCountry"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerGovernate", reader["IssuerGovernate"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerRegioncity", reader["IssuerRegioncity"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerStreet", reader["IssuerStreet"]);
                                    insertCmd.Parameters.AddWithValue("@IssuerBuildingnumber", reader["IssuerBuildingnumber"]);
                                    insertCmd.Parameters.AddWithValue("@ReceiverType", reader["ReceiverType"]);
                                    insertCmd.Parameters.AddWithValue("@ReceiverId", reader["ReceiverId"]);
                                    insertCmd.Parameters.AddWithValue("@ReceiverName", reader["ReceiverName"]);
                                    insertCmd.Parameters.AddWithValue("@ReceiverCountry", reader["ReceiverCountry"]);
                                    insertCmd.Parameters.AddWithValue("@ReceiverGovernate", reader["ReceiverGovernate"]);
                                    insertCmd.Parameters.AddWithValue("@ReceiverRegioncity", reader["ReceiverRegioncity"]);
                                    insertCmd.Parameters.AddWithValue("@ReceiverStreet", reader["ReceiverStreet"]);
                                    if (reader["ReceiverBuildingnumber"] == null)
                                    {
                                        insertCmd.Parameters.AddWithValue("@ReceiverBuildingnumber", "1");
                                    }
                                    else
                                    {
                                        insertCmd.Parameters.AddWithValue("@ReceiverBuildingnumber", reader["ReceiverBuildingnumber"]);
                                    }
                                    insertCmd.Parameters.AddWithValue("@Documenttype", reader["TypeOfDocument_Id"]);
                                    insertCmd.Parameters.AddWithValue("@Documenttypeversion", reader["TypeOfDocumentVersion_Id"]);
                                    insertCmd.Parameters.AddWithValue("@Datetimeissued", reader["dateTimeIssued"]);
                                    insertCmd.Parameters.AddWithValue("@Taxpayeractivitycode", reader["taxpayerActivityCode"]);
                                    insertCmd.Parameters.AddWithValue("@Internalid", reader["Internal_ID"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesDescription", reader["CodeName"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesItemtype", reader["itemType"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesItemcode", reader["itemCode"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesUnittype", reader["Unit_Id"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesQuantity", reader["quantity"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesCurrencysold", reader["currencySold"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesAmountegp", reader["amountEGP"]);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesAmountsold", reader["amount"]);
                                    insertCmd.Parameters.AddWithValue("@Currencyexchangerate", 1); // ضع القيمة المناسبة إذا كانت متاحة
                                    insertCmd.Parameters.AddWithValue("@Salestotal", reader["salesTotal"]);
                                    insertCmd.Parameters.AddWithValue("@Total", reader["total"]);
                                    insertCmd.Parameters.AddWithValue("@Valuedifference", reader["valueDifference"]);
                                    insertCmd.Parameters.AddWithValue("@Totaltaxablefees", reader["totalTaxableFees"]);
                                    insertCmd.Parameters.AddWithValue("@Nettotal", reader["netTotal"]);
                                    insertCmd.Parameters.AddWithValue("@Itemsdiscount", reader["itemsDiscount"]);
                                    insertCmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", reader["extraDiscountAmount"]);
                                    insertCmd.Parameters.AddWithValue("@Taxtype", "VAT"); // ضع نوع الضريبة المناسب إذا كان متاح
                                    insertCmd.Parameters.AddWithValue("@Amount", reader["amount"]);
                                    insertCmd.Parameters.AddWithValue("@Subtype", "Standard"); // ضع القيمة المناسبة إذا كانت متاحة
                                    insertCmd.Parameters.AddWithValue("@Rate", reader["rate"]);
                                    insertCmd.Parameters.AddWithValue("@LineInternalcode", reader["internalCode"]);
                                    insertCmd.Parameters.AddWithValue("@Totalsalesamount", reader["totalSalesAmount"]);
                                    insertCmd.Parameters.AddWithValue("@Totaldiscountamount", reader["totalDiscountAmount"]);
                                    insertCmd.Parameters.AddWithValue("@Netamount", reader["netAmount"]);
                                    insertCmd.Parameters.AddWithValue("@Taxtype1", "VAT");
                                    insertCmd.Parameters.AddWithValue("@Taxtotalamout", reader["taxTotals"]);
                                    insertCmd.Parameters.AddWithValue("@Extradiscountamount", reader["extraDiscountAmount"]);
                                    insertCmd.Parameters.AddWithValue("@Totalitemsdiscountamount", reader["totalItemsDiscountAmount"]);
                                    insertCmd.Parameters.AddWithValue("@Totalamount", reader["totalAmount"]);
                                    insertCmd.Parameters.AddWithValue("@PURCHASEORDERREFERENCE", "0.000000");
                                    insertCmd.Parameters.AddWithValue("@PURCHASEORDERDESCRIPTION", "0.000000");
                                    insertCmd.Parameters.AddWithValue("@SALESORDERREFERENCE", "0.000000");
                                    insertCmd.Parameters.AddWithValue("@SALESORDERDESCRIPTION", "0.000000");

                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    // تنفيذ العملية بنجاح
                    transaction.Commit();
                    Console.WriteLine("تم إدراج الفاتورة وتفاصيلها بنجاح في جدول ExcelTable.");
                }
                catch (Exception ex)
                {
                    // في حالة حدوث خطأ، يتم التراجع عن العملية
                    transaction.Rollback();
                    Console.WriteLine("حدث خطأ أثناء الإدراج: " + ex.Message);
                }
            }
        }
        string OurCompanyName_A;
        string TaxRegisterNo;
        string CompanyType;
        string Governerate;
        string Cities;
        string Street;
        string BuildingNumber;
        string CountryCode;
        string ActivityCode;

        string ReceiverType;
        string ReceiverId;
        string ReceiverName;
        string ReceiverCountry;
        string ReceiverGovernate;
        string ReceiverRegioncity;
        string ReceiverStreet;
        string ReceiverBuildingnumber;
        string TaxRecordNo;
        private void GetIssureInfo()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString.ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string IssuerData = @"SELECT        dbo.Tbl_OurCompany.Name_Ar, dbo.Tbl_OurCompany.TaxRegisterNo, dbo.Tbl_OurCompany.CompanyType, dbo.Tbl_Governerate.Name_Ar AS Governate, dbo.Tbl_Cities.Name_E AS City, dbo.Tbl_OurCompany.Street, 
                         dbo.Tbl_OurCompany.BuildingNumber, dbo.Tbl_OurCompany.CountryCode, dbo.Tbl_OurCompActivity.ActivityCode
FROM            dbo.Tbl_Governerate INNER JOIN
                         dbo.Tbl_Cities ON dbo.Tbl_Governerate.ID = dbo.Tbl_Cities.GovernorateID INNER JOIN
                         dbo.Tbl_OurCompany ON dbo.Tbl_Cities.ID = dbo.Tbl_OurCompany.CityID INNER JOIN
                         dbo.Tbl_OurCompActivity ON dbo.Tbl_OurCompany.ID = dbo.Tbl_OurCompActivity.OurCompanyRef";
                connection.Open();
                using (SqlCommand GetIsuureCMD = new SqlCommand(IssuerData, connection))
                {
                    using (SqlDataReader reader = GetIsuureCMD.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            OurCompanyName_A = reader.GetValue(0).ToString();
                            TaxRegisterNo = reader.GetValue(1).ToString();
                            CompanyType = reader.GetValue(2).ToString();
                            Governerate = reader.GetValue(3).ToString();
                            Cities = reader.GetValue(4).ToString();
                            Street = reader.GetValue(5).ToString();
                            BuildingNumber = reader.GetValue(6).ToString();
                            CountryCode = reader.GetValue(7).ToString();
                            ActivityCode = reader.GetValue(8).ToString();

                        }
                        //reader.Close();
                        connection.Close();
                    }
                }
            }
        }
        private void GetReceiverInfo()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString.ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string IssuerData = @"SELECT  dbo.Tbl_Governerate.Name_Ar AS Governate, dbo.Tbl_Cities.Name_E AS City, dbo.Tbl_Customer.CompanyType, dbo.Tbl_Customer.TaxRecordNo, dbo.Tbl_Customer.CustomerName, dbo.Tbl_Customer.CountryCode,   dbo.Tbl_Customer.Address, dbo.Tbl_Customer.BuildingNo
FROM            dbo.Tbl_Governerate INNER JOIN
                         dbo.Tbl_Cities ON dbo.Tbl_Governerate.ID = dbo.Tbl_Cities.GovernorateID INNER JOIN
                         dbo.Tbl_Customer ON dbo.Tbl_Governerate.ID = dbo.Tbl_Customer.ID INNER JOIN
                         dbo.Tbl_CompanyType ON dbo.Tbl_Governerate.ID = dbo.Tbl_CompanyType.ID
WHERE        (dbo.Tbl_Customer.TaxRecordNo = @N)";
                connection.Open();
                using (SqlCommand GetIsuureCMD = new SqlCommand(IssuerData, connection))
                {
                    GetIsuureCMD.Parameters.Add("@N", SqlDbType.NVarChar).Value = ReceiverTo_RegistrationNumber;
                    using (SqlDataReader reader = GetIsuureCMD.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            ReceiverGovernate = reader.GetValue(0).ToString();
                            ReceiverRegioncity = reader.GetValue(1).ToString();
                            ReceiverType = reader.GetValue(2).ToString();
                            ReceiverId = reader.GetValue(3).ToString();
                            //TaxRecordNo = reader.GetValue(4).ToString();
                            ReceiverName = reader.GetValue(4).ToString();
                            ReceiverCountry = reader.GetValue(5).ToString();
                            ReceiverStreet = reader.GetValue(6).ToString();

                            ReceiverBuildingnumber = reader.GetValue(7).ToString();
                        }
                        //reader.Close();
                        connection.Close();
                    }
                }
            }
        }
        string ReceiverTo_RegistrationNumber;
        public void InsertExcellTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString + ";MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                GetIssureInfo();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        List<dynamic> invoices = new List<dynamic>();

                        string fetchInvoicesQuery = "SELECT * FROM Documents";
                        using (SqlCommand fetchCmd = new SqlCommand(fetchInvoicesQuery, connection, transaction))
                        {
                            using (SqlDataReader reader = fetchCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    invoices.Add(new
                                    {
                                        DocumentId = reader["DocumentId"].ToString(),
                                        ReceiverTo_RegistrationNumber = reader["ReceiverTo_RegistrationNumber"].ToString(),
                                        Datetimeissued = reader["dateTimeIssued"],
                                        Internalid = reader["Internal_ID"],
                                    });
                                }
                            }
                        }

                        foreach (var invoice in invoices)
                        {
                            ReceiverTo_RegistrationNumber = invoice.ReceiverTo_RegistrationNumber;
                            GetReceiverInfo();

                            List<dynamic> items = new List<dynamic>();

                            string fetchItemsQuery = "SELECT * FROM Items WHERE DocumentId = @DocumentId";

                            using (SqlCommand fetchItemsCmd = new SqlCommand(fetchItemsQuery, connection, transaction))
                            {
                                fetchItemsCmd.Parameters.AddWithValue("@DocumentId", invoice.DocumentId);
                                using (SqlDataReader itemsReader = fetchItemsCmd.ExecuteReader())
                                {
                                    MessageBox.Show(itemsReader.FieldCount.ToString());
                                    MessageBox.Show("DocumentId: " + invoice.DocumentId);
                                    while (itemsReader.Read())
                                    {
                                        string Description = itemsReader.GetValue(0).ToString();
                                        items.Add(new
                                        {
                                            Description = itemsReader.GetValue(0).ToString(),
                                            ItemType = itemsReader["itemType"],
                                            ItemCode = itemsReader["itemCode"],
                                            UnitType = itemsReader["Unit_Id"],
                                            Quantity = itemsReader["quantity"],
                                            CurrencySold = itemsReader["currencySold"],
                                            AmountEgp = itemsReader["amountEGP"],
                                            AmountSold = itemsReader["amount"],
                                            SalesTotal = itemsReader["salesTotal"],
                                            Total = itemsReader["total"],
                                            ValueDifference = itemsReader["valueDifference"],
                                            TotalTaxableFees = itemsReader["totalTaxableFees"],
                                            NetTotal = itemsReader["netTotal"],
                                            ItemsDiscount = itemsReader["itemsDiscount"],
                                            DiscountAmount = itemsReader["discountAmount"],
                                            TaxType = itemsReader["taxType"],
                                            SubType = itemsReader["subtype"],
                                            Rate = itemsReader["rate"],
                                            InternalCode = itemsReader["internalCode"],
                                            TotalSalesAmount = itemsReader["totalSalesAmount"],
                                            TotalDiscountAmount = itemsReader["totalDiscountAmount"],
                                            NetAmount = itemsReader["netAmount"],
                                            TaxType1 = itemsReader["taxType1"],
                                            TaxTotalAmount = itemsReader["taxTotalAmount"],
                                            ExtraDiscountAmount = itemsReader["extraDiscountAmount"],
                                            TotalItemsDiscountAmount = itemsReader["totalItemsDiscountAmount"],
                                            TotalAmount = itemsReader["totalAmount"],
                                            PurchaseOrderReference = itemsReader["purchaseOrderReference"],
                                            PurchaseOrderDescription = itemsReader["purchaseOrderDescription"],
                                            SalesOrderReference = itemsReader["salesOrderReference"],
                                            SalesOrderDescription = itemsReader["salesOrderDescription"]
                                        });
                                    }
                                }
                            }

                            foreach (var item in items)
                            {
                                string insertQuery = @"INSERT INTO ExcelTable 
                        (IssuerType, IssuerId, IssuerName, IssuerBranchid, IssuerCountry, IssuerGovernate, 
                        IssuerRegioncity, IssuerStreet, IssuerBuildingnumber, ReceiverType, ReceiverId, 
                        ReceiverName, ReceiverCountry, ReceiverGovernate, ReceiverRegioncity, ReceiverStreet, 
                        ReceiverBuildingnumber, Documenttype, Documenttypeversion, Datetimeissued, 
                        Taxpayeractivitycode, Internalid, InvoicelinesDescription, InvoicelinesItemtype, 
                        InvoicelinesItemcode, InvoicelinesUnittype, InvoicelinesQuantity, InvoicelinesCurrencysold, 
                        InvoicelinesAmountegp, InvoicelinesAmountsold, Currencyexchangerate, Salestotal, Total, 
                        Valuedifference, Totaltaxablefees, Nettotal, Itemsdiscount, DISCOUNT_AMOUNT, Taxtype, 
                        Amount, Subtype, Rate, LineInternalcode, Totalsalesamount, Totaldiscountamount, 
                        Netamount, Taxtype1, Taxtotalamout, Extradiscountamount, Totalitemsdiscountamount, 
                        Totalamount, PURCHASEORDERREFERENCE, PURCHASEORDERDESCRIPTION, SALESORDERREFERENCE, 
                        SALESORDERDESCRIPTION) 
                        VALUES 
                        (@IssuerType, @IssuerId, @IssuerName, @IssuerBranchid, @IssuerCountry, @IssuerGovernate, 
                        @IssuerRegioncity, @IssuerStreet, @IssuerBuildingnumber, @ReceiverType, @ReceiverId, 
                        @ReceiverName, @ReceiverCountry, @ReceiverGovernate, @ReceiverRegioncity, @ReceiverStreet, 
                        @ReceiverBuildingnumber, @Documenttype, @Documenttypeversion, @Datetimeissued, 
                        @Taxpayeractivitycode, @Internalid, @InvoicelinesDescription, @InvoicelinesItemtype, 
                        @InvoicelinesItemcode, @InvoicelinesUnittype, @InvoicelinesQuantity, @InvoicelinesCurrencysold, 
                        @InvoicelinesAmountegp, @InvoicelinesAmountsold, @Currencyexchangerate, @Salestotal, @Total, 
                        @Valuedifference, @Totaltaxablefees, @Nettotal, @Itemsdiscount, @DISCOUNT_AMOUNT, @Taxtype, 
                        @Amount, @Subtype, @Rate, @LineInternalcode, @Totalsalesamount, @Totaldiscountamount, 
                        @Netamount, @Taxtype1, @Taxtotalamout, @Extradiscountamount, @Totalitemsdiscountamount, 
                        @Totalamount, @PURCHASEORDERREFERENCE, @PURCHASEORDERDESCRIPTION, @SALESORDERREFERENCE, 
                        @SALESORDERDESCRIPTION)";

                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                                {
                                    insertCmd.Parameters.AddWithValue("@IssuerType", CompanyType);
                                    insertCmd.Parameters.AddWithValue("@IssuerId", TaxRegisterNo);
                                    insertCmd.Parameters.AddWithValue("@IssuerName", OurCompanyName_A);
                                    insertCmd.Parameters.AddWithValue("@IssuerBranchid", "0");
                                    insertCmd.Parameters.AddWithValue("@IssuerCountry", CountryCode);
                                    insertCmd.Parameters.AddWithValue("@IssuerGovernate", Governerate);
                                    insertCmd.Parameters.AddWithValue("@IssuerRegioncity", Cities);
                                    insertCmd.Parameters.AddWithValue("@IssuerStreet", Street);
                                    insertCmd.Parameters.AddWithValue("@IssuerBuildingnumber", BuildingNumber);
                                    insertCmd.Parameters.AddWithValue("@ReceiverType", ReceiverType);
                                    insertCmd.Parameters.AddWithValue("@ReceiverId", ReceiverId);
                                    insertCmd.Parameters.AddWithValue("@ReceiverName", ReceiverName);
                                    insertCmd.Parameters.AddWithValue("@ReceiverCountry", ReceiverCountry);
                                    insertCmd.Parameters.AddWithValue("@ReceiverGovernate", ReceiverGovernate);
                                    insertCmd.Parameters.AddWithValue("@ReceiverRegioncity", ReceiverRegioncity);
                                    insertCmd.Parameters.AddWithValue("@ReceiverStreet", ReceiverStreet);
                                    insertCmd.Parameters.AddWithValue("@ReceiverBuildingnumber", ReceiverBuildingnumber);
                                    insertCmd.Parameters.AddWithValue("@Documenttype", "I");
                                    insertCmd.Parameters.AddWithValue("@Documenttypeversion", "1.0");
                                    insertCmd.Parameters.AddWithValue("@Datetimeissued", invoice.Datetimeissued);
                                    insertCmd.Parameters.AddWithValue("@Taxpayeractivitycode", ActivityCode);
                                    insertCmd.Parameters.AddWithValue("@Internalid", invoice.Internalid);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesDescription", item.Description);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesItemtype", item.ItemType);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesItemcode", item.ItemCode);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesUnittype", item.UnitType);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesQuantity", item.Quantity);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesCurrencysold", item.CurrencySold);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesAmountegp", item.AmountEgp);
                                    insertCmd.Parameters.AddWithValue("@InvoicelinesAmountsold", item.AmountSold);
                                    insertCmd.Parameters.AddWithValue("@Currencyexchangerate", 1); // القيمة الافتراضية
                                    insertCmd.Parameters.AddWithValue("@Salestotal", item.SalesTotal);
                                    insertCmd.Parameters.AddWithValue("@Total", item.Total);
                                    insertCmd.Parameters.AddWithValue("@Valuedifference", item.ValueDifference);
                                    insertCmd.Parameters.AddWithValue("@Totaltaxablefees", item.TotalTaxableFees);
                                    insertCmd.Parameters.AddWithValue("@Nettotal", item.NetTotal);
                                    insertCmd.Parameters.AddWithValue("@Itemsdiscount", item.ItemsDiscount);
                                    insertCmd.Parameters.AddWithValue("@DISCOUNT_AMOUNT", item.DiscountAmount);
                                    insertCmd.Parameters.AddWithValue("@Taxtype", item.TaxType);
                                    insertCmd.Parameters.AddWithValue("@Amount", item.AmountSold);
                                    insertCmd.Parameters.AddWithValue("@Subtype", item.SubType);
                                    insertCmd.Parameters.AddWithValue("@Rate", item.Rate);
                                    insertCmd.Parameters.AddWithValue("@LineInternalcode", item.InternalCode);
                                    insertCmd.Parameters.AddWithValue("@Totalsalesamount", item.TotalSalesAmount);
                                    insertCmd.Parameters.AddWithValue("@Totaldiscountamount", item.TotalDiscountAmount);
                                    insertCmd.Parameters.AddWithValue("@Netamount", item.NetAmount);
                                    insertCmd.Parameters.AddWithValue("@Taxtype1", item.TaxType1);
                                    insertCmd.Parameters.AddWithValue("@Taxtotalamout", item.TaxTotalAmount);
                                    insertCmd.Parameters.AddWithValue("@Extradiscountamount", item.ExtraDiscountAmount);
                                    insertCmd.Parameters.AddWithValue("@Totalitemsdiscountamount", item.TotalItemsDiscountAmount);
                                    insertCmd.Parameters.AddWithValue("@Totalamount", item.TotalAmount);
                                    insertCmd.Parameters.AddWithValue("@PURCHASEORDERREFERENCE", item.PurchaseOrderReference);
                                    insertCmd.Parameters.AddWithValue("@PURCHASEORDERDESCRIPTION", item.PurchaseOrderDescription);
                                    insertCmd.Parameters.AddWithValue("@SALESORDERREFERENCE", item.SalesOrderReference);
                                    insertCmd.Parameters.AddWithValue("@SALESORDERDESCRIPTION", item.SalesOrderDescription);

                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("حدث خطأ أثناء العملية: " + ex.Message);
                    }
                }
            }
        }



        private void buttonX2_Click(object sender, EventArgs e)
        {
            InsertExcellTable();


        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }
        private void InsertData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString + ";MultipleActiveResultSets=true";
            string connectionStringInsert = ConfigurationManager.ConnectionStrings["InvoiceEF"].ConnectionString + ";MultipleActiveResultSets=true";

            string selectQuery = @"SELECT        TOP (100) PERCENT dbo.Tbl_Customer.TaxRecordNo AS ReceiverId, MAX(dbo.Tbl_Customer.CustomerName) AS ReceiverName, dbo.Tbl_Customer.BuildingNo AS ReceiverBuildingnumber, dbo.Items.valueDifference, 
                         dbo.Items.totalTaxableFees, dbo.Items.netTotal, dbo.Items.itemsDiscount, dbo.Items.amount, dbo.Items.rate, dbo.Items.internalCode AS LineInternalcode, dbo.Documents.totalSalesAmount, dbo.Documents.totalDiscountAmount, 
                         dbo.Documents.netAmount, dbo.Documents.totalAmount, dbo.Documents.extraDiscountAmount, dbo.Documents.totalItemsDiscountAmount, dbo.Documents.taxTotals AS Taxtotalamout, dbo.Documents.purchaseOrderReference, 
                         dbo.Documents.purchaseOrderDescription, dbo.Documents.salesOrderReference, dbo.Documents.salesOrderDescription, dbo.Items.total, dbo.Items.itemType AS InvoicelinesItemtype, 
                         dbo.Items.itemCode AS InvoicelinesItemcode, dbo.Items.quantity AS InvoicelinesQuantity, dbo.Items.currencySold AS InvoicelinesCurrencysold, dbo.Items.amountEGP AS InvoicelinesAmountegp, 
                         dbo.Documents.taxpayerActivityCode AS Taxpayeractivitycode, dbo.Items.salesTotal AS Salestotal, dbo.Tbl_ItemsOfTax.CodeName AS InvoicelinesDescription, dbo.Documents.DocumentId, dbo.Tbl_Customer.TaxRecordNo, 
                         dbo.Documents.Id_Suppliers_Clint, dbo.Tbl_Customer.AccountGuidID, dbo.Tbl_Customer.CompanyType AS ReceiverType, dbo.Documents.governate AS ReceiverGovernate, dbo.Documents.regionCity AS ReceiverRegioncity, 
                         dbo.Documents.street AS ReceiverStreet, dbo.Tbl_Customer.CountryCode AS ReceiverCountry
FROM            dbo.Documents INNER JOIN
                         dbo.Items ON dbo.Documents.DocumentId = dbo.Items.DocumentId INNER JOIN
                         dbo.Tbl_ItemsOfTax ON dbo.Items.itemCode = dbo.Tbl_ItemsOfTax.UniqueNumber INNER JOIN
                         dbo.Tbl_Customer ON dbo.Documents.ReceiverTo_RegistrationNumber = dbo.Tbl_Customer.TaxRecordNo AND dbo.Documents.Id_Suppliers_Clint = dbo.Tbl_Customer.AccountNO
GROUP BY dbo.Tbl_Customer.TaxRecordNo, dbo.Tbl_Customer.BuildingNo, dbo.Items.valueDifference, dbo.Items.totalTaxableFees, dbo.Items.netTotal, dbo.Items.itemsDiscount, dbo.Items.amount, dbo.Items.rate, dbo.Items.internalCode, 
                         dbo.Documents.totalSalesAmount, dbo.Documents.totalDiscountAmount, dbo.Documents.netAmount, dbo.Documents.totalAmount, dbo.Documents.extraDiscountAmount, dbo.Documents.totalItemsDiscountAmount, 
                         dbo.Documents.taxTotals, dbo.Documents.purchaseOrderReference, dbo.Documents.purchaseOrderDescription, dbo.Documents.salesOrderReference, dbo.Documents.salesOrderDescription, dbo.Items.total, 
                         dbo.Items.itemType, dbo.Items.itemCode, dbo.Items.quantity, dbo.Items.currencySold, dbo.Items.amountEGP, dbo.Documents.taxpayerActivityCode, dbo.Items.salesTotal, dbo.Tbl_ItemsOfTax.CodeName, 
                         dbo.Documents.DocumentId, dbo.Documents.Id_Suppliers_Clint, dbo.Tbl_Customer.AccountGuidID, dbo.Tbl_Customer.CompanyType, dbo.Documents.country, dbo.Documents.governate, dbo.Documents.regionCity, 
                         dbo.Documents.street, dbo.Tbl_Customer.CountryCode
ORDER BY dbo.Documents.DocumentId";

            string insertQuery = @"INSERT INTO ExcelTable 
                           (IssuerType,IssuerId,IssuerName,IssuerBranchid,IssuerCountry,IssuerGovernate,IssuerRegioncity,IssuerStreet,IssuerBuildingnumber, ReceiverType, ReceiverId, ReceiverName, ReceiverCountry, ReceiverGovernate, ReceiverRegioncity, 
                            ReceiverStreet, ReceiverBuildingnumber, Valuedifference, Totaltaxablefees, Nettotal, Itemsdiscount, 
                            Amount, Rate, LineInternalcode, Totalsalesamount, Totaldiscountamount, Netamount, Totalamount, 
                            Extradiscountamount, Totalitemsdiscountamount, Taxtotalamout, PURCHASEORDERREFERENCE, 
                            PURCHASEORDERDESCRIPTION, SALESORDERREFERENCE, SALESORDERDESCRIPTION, Total, 
                            InvoicelinesItemtype, InvoicelinesItemcode, InvoicelinesQuantity, InvoicelinesCurrencysold, 
                            InvoicelinesAmountegp, Taxpayeractivitycode, Salestotal,Documenttype,Documenttypeversion,Datetimeissued,Internalid
,Taxtype,Subtype,Taxtype1,InvoicelinesDescription) 
                           VALUES 
                           (@IssuerType,@IssuerId,@IssuerName,@IssuerBranchid,@IssuerCountry,@IssuerGovernate,@IssuerRegioncity,@IssuerStreet,@IssuerBuildingnumber,@ReceiverType, @ReceiverId, @ReceiverName, @ReceiverCountry, @ReceiverGovernate, @ReceiverRegioncity, 
                            @ReceiverStreet, @ReceiverBuildingnumber, @Valuedifference, @Totaltaxablefees, @Nettotal, @Itemsdiscount, 
                            @Amount, @Rate, @LineInternalcode, @Totalsalesamount, @Totaldiscountamount, @Netamount, @Totalamount, 
                            @Extradiscountamount, @Totalitemsdiscountamount, @Taxtotalamout, @PURCHASEORDERREFERENCE, 
                            @PURCHASEORDERDESCRIPTION, @SALESORDERREFERENCE, @SALESORDERDESCRIPTION, @Total, 
                            @InvoicelinesItemtype, @InvoicelinesItemcode, @InvoicelinesQuantity, @InvoicelinesCurrencysold, 
                            @InvoicelinesAmountegp, @Taxpayeractivitycode, @Salestotal,@Documenttype,@Documenttypeversion,@Datetimeissued,@Internalid
,@Taxtype,@Subtype,@Taxtype1,@InvoicelinesDescription)";

            //try
            //{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = selectCommand.ExecuteReader();
                SqlConnection connectionInsert = new SqlConnection(connectionStringInsert);
                connectionInsert.Open();
                while (reader.Read())
                {

                    SqlCommand insertCommand = new SqlCommand(insertQuery, connectionInsert);
                    insertCommand.Parameters.AddWithValue("@IssuerType", textBox10.Text.Trim());
                    insertCommand.Parameters.AddWithValue("@IssuerId", textBox4.Text);
                    insertCommand.Parameters.AddWithValue("@IssuerName", textBox3.Text);
                    insertCommand.Parameters.AddWithValue("@IssuerBranchid", textBox16.Text);
                    insertCommand.Parameters.AddWithValue("@IssuerCountry", textBox5.Text);
                    insertCommand.Parameters.AddWithValue("@IssuerGovernate", textBox15.Text);
                    insertCommand.Parameters.AddWithValue("@IssuerRegioncity", textBox14.Text);
                    insertCommand.Parameters.AddWithValue("@IssuerStreet", textBox6.Text);
                    insertCommand.Parameters.AddWithValue("@IssuerBuildingnumber", textBox7.Text);

                    insertCommand.Parameters.AddWithValue("@ReceiverType", reader["ReceiverType"]);
                    insertCommand.Parameters.AddWithValue("@ReceiverId", reader["ReceiverId"]);
                    insertCommand.Parameters.AddWithValue("@ReceiverName", reader["ReceiverName"]);
                    insertCommand.Parameters.AddWithValue("@ReceiverCountry", reader["ReceiverCountry"]);
                    insertCommand.Parameters.AddWithValue("@ReceiverGovernate", reader["ReceiverGovernate"]);
                    insertCommand.Parameters.AddWithValue("@ReceiverRegioncity", reader["ReceiverRegioncity"]);
                    insertCommand.Parameters.AddWithValue("@ReceiverStreet", reader["ReceiverStreet"]);
                    insertCommand.Parameters.AddWithValue("@ReceiverBuildingnumber", reader["ReceiverBuildingnumber"]);
                    insertCommand.Parameters.AddWithValue("@Valuedifference", reader["valueDifference"]);
                    insertCommand.Parameters.AddWithValue("@Totaltaxablefees", reader["totalTaxableFees"]);
                    insertCommand.Parameters.AddWithValue("@Nettotal", reader["netTotal"]);
                    insertCommand.Parameters.AddWithValue("@Itemsdiscount", reader["itemsDiscount"]);
                    insertCommand.Parameters.AddWithValue("@Amount", reader["amount"]);
                    insertCommand.Parameters.AddWithValue("@Rate", reader["rate"]);
                    insertCommand.Parameters.AddWithValue("@LineInternalcode", reader["LineInternalcode"]);
                    insertCommand.Parameters.AddWithValue("@Totalsalesamount", reader["totalSalesAmount"]);
                    insertCommand.Parameters.AddWithValue("@Totaldiscountamount", reader["totalDiscountAmount"]);
                    insertCommand.Parameters.AddWithValue("@Netamount", reader["netAmount"]);
                    insertCommand.Parameters.AddWithValue("@Totalamount", reader["totalAmount"]);
                    insertCommand.Parameters.AddWithValue("@Extradiscountamount", reader["extraDiscountAmount"]);
                    insertCommand.Parameters.AddWithValue("@Totalitemsdiscountamount", reader["totalItemsDiscountAmount"]);
                    insertCommand.Parameters.AddWithValue("@Taxtotalamout", reader["Taxtotalamout"]);
                    insertCommand.Parameters.AddWithValue("@PURCHASEORDERREFERENCE", "0.00000");
                    insertCommand.Parameters.AddWithValue("@PURCHASEORDERDESCRIPTION", "0.00000");
                    insertCommand.Parameters.AddWithValue("@SALESORDERREFERENCE", "0.00000");
                    insertCommand.Parameters.AddWithValue("@SALESORDERDESCRIPTION", "0.00000");
                    insertCommand.Parameters.AddWithValue("@Total", reader["total"]);
                    insertCommand.Parameters.AddWithValue("@InvoicelinesItemtype", reader["InvoicelinesItemtype"]);
                    insertCommand.Parameters.AddWithValue("@InvoicelinesItemcode", reader["InvoicelinesItemcode"]);
                    insertCommand.Parameters.AddWithValue("@InvoicelinesQuantity", reader["InvoicelinesQuantity"]);
                    insertCommand.Parameters.AddWithValue("@InvoicelinesCurrencysold", reader["InvoicelinesCurrencysold"]);
                    insertCommand.Parameters.AddWithValue("@InvoicelinesAmountegp", reader["InvoicelinesAmountegp"]);
                    insertCommand.Parameters.AddWithValue("@Taxpayeractivitycode", textBox11.Text);
                    insertCommand.Parameters.AddWithValue("@Salestotal", reader["Salestotal"]);
                    insertCommand.Parameters.AddWithValue("@InvoicelinesDescription", reader["InvoicelinesDescription"]);
                    insertCommand.Parameters.AddWithValue("@Documenttype", comboBox2.Text);
                    insertCommand.Parameters.AddWithValue("@Documenttypeversion", "1.0");
                    insertCommand.Parameters.AddWithValue("@Datetimeissued", DateTime.Now.ToShortDateString());
                    insertCommand.Parameters.AddWithValue("@Internalid", reader["DocumentId"]);
                    insertCommand.Parameters.AddWithValue("@Taxtype", textBox12.Text);
                    insertCommand.Parameters.AddWithValue("@Subtype", textBox13.Text);
                    insertCommand.Parameters.AddWithValue("@Taxtype1", textBox12.Text);
                    // تنفيذ عملية الإدراج
                    insertCommand.ExecuteNonQuery();
                }

                reader.Close();
            }

            MessageBox.Show("Data inserted successfully.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }


        private void button1_Click(object sender, EventArgs e)
        {
            InsertData();
        }
        // Function Updatetd
        private void button2_Click(object sender, EventArgs e)
        {
            string receiptJson = @"{
    ""receipts"": [
        {
            ""header"": {
                ""dateTimeIssued"": ""2024-09-14T00:34:00Z"",
                ""receiptNumber"": ""123458"",
                ""uuid"": """",
                ""previousUUID"": """",
                ""referenceOldUUID"": """",
                ""currency"": ""EGP"",
                ""exchangeRate"": 0,
                ""sOrderNameCode"": ""sOrderNameCode"",
                ""orderdeliveryMode"": """",
                ""grossWeight"": 0,
                ""netWeight"": 0
              
            },
            ""documentType"": {
                ""receiptType"": ""S"",
                ""typeVersion"": ""1.2""
            },
            ""seller"": {
                ""rin"": ""588660248"",
                ""companyTradeName"": ""شركة الصوٝى"",
                ""branchCode"": ""0"",
                ""branchAddress"": {
                    ""country"": ""EG"",
                    ""governate"": ""cairo"",
                    ""regionCity"": ""city center"",
                    ""street"": ""16 street"",
                    ""buildingNumber"": ""14BN"",
                    ""postalCode"": """",
                    ""floor"": ""1F"",
                    ""room"": ""3R"",
                    ""landmark"": ""tahrir square"",
                    ""additionalInformation"": ""talaat harb street""
                },
                ""deviceSerialNumber"": ""2UA3220J04"",
                ""syndicateLicenseNumber"": """",
                ""activityCode"": ""8690""
            },
            ""buyer"": {
                ""type"": ""f"",
                ""id"": ""313717919"",
                ""name"": ""taxpayer 1"",
                ""mobileNumber"": ""+201020567462"",
                ""paymentNumber"": ""987654""
            },
            ""itemData"": [
                {
                    ""internalCode"": ""880609"",
                    ""description"": ""Samsung A02 32GB_LTE_BLACK_DS_SM-A022FZKDMEB_A022 _ A022_SM-A022FZKDMEB"",
                    ""itemType"": ""EGS"",
                    ""itemCode"": ""EG-588660248-ACF009"",
                    ""unitType"": ""IE"",
                    ""quantity"": 35,
                    ""unitPrice"":  247.96000 ,
                    ""netSale"":  7810.74000 ,
                    ""totalSale"":  8678.60000 ,
                    ""total"":  8887.04360 ,
                    ""commercialDiscountData"": [
                         {
                             ""amount"": 867.86000, 
                             ""description"": ""XYZ"",
                             ""rate"":2.3
                         }
                    ],
                    ""itemDiscountData"": [
                        {
                            ""amount"": 10,
                            ""description"":""ABC"",
                             ""rate"":2.3
                        },
                        {
                            ""amount"": 10,
                            ""description"": ""XYZ"",
                             ""rate"":4.0
                        }
                    ],
                     ""additionalCommercialDiscount"": {
                            ""amount"": 9456.1404,
                            ""description"": ""ABC"",
                            ""rate"": 10.0
                        },
                        ""additionalItemDiscount"": {
                            ""amount"": 9456.1404,
                            ""description"": ""XYZ"",
                            ""rate"": 10.0
                        },
                    ""valueDifference"": 20,
                    ""taxableItems"": [
                        {
                                ""taxType"": ""T1"",
                                ""amount"":  1096.30360 ,
                                ""subType"": ""V009"",
                                ""rate"": 14
                        }
                    ]
                }
            ],
            ""totalSales"": 8678.60000,
            ""totalCommercialDiscount"": 867.86000,
            ""totalItemsDiscount"": 20,
            ""extraReceiptDiscountData"": [
               {
                   ""amount"": 0,
                   ""description"": ""ABC"",
                    ""rate"":10
               }
            ],
            ""netAmount"": 7810.74000,
            ""feesAmount"": 0,
            ""totalAmount"": 8887.04360,
            ""taxTotals"": [
                    {
                        ""taxType"": ""T1"",
                        ""amount"": 1096.30360
                    }
            ],
            ""paymentMethod"": ""C"",
            ""adjustment"": 0,
            ""contractor"": {
                ""name"": ""contractor1"",
                ""amount"": 2.563,
                ""rate"": 2.3
            },
            ""beneficiary"": {
                ""amount"": 20.569,
                ""rate"": 2.147
            }
        }
    ]
    
}";
            Chilkat.StringBuilder sb = new Chilkat.StringBuilder();

            bool success = sb.LoadFile("D:/555555555555555555555555555", "utf-8");
            if (success == false)
            {
                Debug.WriteLine("Failed to load input file.");
                return;
            }

            sb.Encode("itida", "utf-8");

            string uuid = sb.GetHash("sha256", "hex_lower", "utf-8");
            Debug.WriteLine("eInvoicing UUID = " + uuid);

            string content = receiptJson;




            JObject receiptObject = JObject.Parse(receiptJson);
            textBox19.Text = CustomSerializeJson(receiptObject);
            textBox18.Text = uuid;

        }
        private static string GenerateUuid(string formattedJson)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(formattedJson);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        public static void UpdateReceiptWithUuid(JObject receiptObject, string uuid)
        {
            var header = receiptObject["HEADER"];
            header["uuid"] = uuid;

        }

        public static string GenerateUuid(JObject receiptObject)
        {
            string serializedReceipt = JsonConvert.SerializeObject(receiptObject, Formatting.None);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(serializedReceipt));

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
        public static void EnsureReceiptFields(JObject receiptObject)
        {
            var header = receiptObject["HEADER"];


            if (header["UUID"] == null || string.IsNullOrEmpty(header["UUID"].ToString()))
            {
                header["UUID"] = "";
            }


            if (header["PREVIOUSUUID"] == null)
            {
                header["PREVIOUSUUID"] = "";
            }
            if (header["REFERENCEOLDUUID"] == null)
            {
                header["REFERENCEOLDUUID"] = "";
            }

        }

        public static string CustomSerializeJson(JObject jsonObject)
        {
            // نسلسل الـ JSON بشكل عادي
            string serializedJson = JsonConvert.SerializeObject(jsonObject, Formatting.None);



            serializedJson = serializedJson
                .Replace("receipts", null)

                .Replace("{", "")
                .Replace("}", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace(",", "")
                .Replace("\"", "\"")
                .Replace(":", "")
                 .Replace("header", "HEADER")
        .Replace("dateTimeIssued", "DATETIMEISSUED")
        .Replace("receiptNumber", "RECEIPTNUMBER")
        .Replace("uuid", "UUID")
        .Replace("previousUUID", "PREVIOUSUUID")
        .Replace("referenceOldUUID", "REFERENCEOLDUUID")
        .Replace("currency", "CURRENCY")
        .Replace("exchangeRate", "EXCHANGERATE")
        .Replace("sOrderNameCode", "SORDERNAMECODE")
        .Replace("orderdeliveryMode", "ORDERDELIVERYMODE")
        .Replace("grossWeight", "GROSSWEIGHT")
        .Replace("netWeight", "NETWEIGHT")
        .Replace("documentType", "DOCUMENTTYPE")
        .Replace("receiptType", "RECEIPTTYPE")
        .Replace("typeVersion", "TYPEVERSION")
        .Replace("seller", "SELLER")
        .Replace("rin", "RIN")
        .Replace("companyTradeName", "COMPANYTRADENAME")
        .Replace("branchCode", "BRANCHCODE")
        .Replace("branchAddress", "BRANCHADDRESS")
        .Replace("country", "COUNTRY")
        .Replace("governate", "GOVERNATE")
        .Replace("regionCity", "REGIONCITY")
        .Replace("street", "STREET")
        .Replace("buildingNumber", "BUILDINGNUMBER")
        .Replace("postalCode", "POSTALCODE")
        .Replace("floor", "FLOOR")
        .Replace("room", "ROOM")
        .Replace("landmark", "LANDMARK")
        .Replace("additionalInformation", "ADDITIONALINFORMATION")
        .Replace("deviceSerialNumber", "DEVICESERIALNUMBER")
        .Replace("syndicateLicenseNumber", "SYNDICATELICENSENUMBER")
        .Replace("activityCode", "ACTIVITYCODE")
        .Replace("buyer", "BUYER")
        .Replace("type", "TYPE")
        .Replace("id", "ID")
        .Replace("name", "NAME")
        .Replace("mobileNumber", "MOBILENUMBER")
        .Replace("paymentNumber", "PAYMENTNUMBER")
        .Replace("itemData", "ITEMDATA")
        .Replace("internalCode", "INTERNALCODE")
        .Replace("description", "DESCRIPTION")
        .Replace("itemType", "ITEMTYPE")
        .Replace("itemCode", "ITEMCODE")
        .Replace("unitType", "UNITTYPE")
        .Replace("quantity", "QUANTITY")
        .Replace("unitPrice", "UNITPRICE")
        .Replace("netSale", "NETSALE")
        .Replace("totalSale", "TOTALSALE")
        .Replace("total", "TOTAL")
        .Replace("commercialDiscountData", "COMMERCIALDISCOUNTDATA")
        .Replace("itemDiscountData", "ITEMDISCOUNTDATA")
        .Replace("valueDifference", "VALUEDIFFERENCE")
        .Replace("taxableItems", "TAXABLEITEMS")
        .Replace("taxType", "TAXTYPE")
        .Replace("amount", "AMOUNT")
        .Replace("subType", "SUBTYPE")
        .Replace("rate", "RATE")
        .Replace("totalSales", "TOTALSALES")
        .Replace("totalCommercialDiscount", "TOTALCOMMERCIALDISCOUNT")
        .Replace("totalItemsDiscount", "TOTALITEMSDISCOUNT")
        .Replace("extraReceiptDiscountData", "EXTRARECEIPTDISCOUNTDATA")
        .Replace("netAmount", "NETAMOUNT")
        .Replace("feesAmount", "FEESAMOUNT")
        .Replace("totalAmount", "TOTALAMOUNT")
        .Replace("taxTotals", "TAXTOTALS")
        .Replace("paymentMethod", "PAYMENTMETHOD")
        .Replace("adjustment", "ADJUSTMENT")
        .Replace("contractor", "CONTRACTOR")
        .Replace("beneficiary", "BENEFICIARY");


            serializedJson = serializedJson.Replace("2024-09-14T003400Z", "2024-09-14T00:34:00Z");

            return serializedJson;
        }
        public class BaseResponseDto
        {
            public string CorrelationId { get; set; }
            public string Target { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
            public object Details { get; set; }
        }

        public class GenerateUuidResponseDto : BaseResponseDto
        {
            public string Uuid { get; set; } = null;
            public JToken UpdatedReceiptJson { get; set; } = null;
        }



        private static string MinifyJson(string jsonContent)
        {
            try
            {

                var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonContent);


                string minifiedJson = JsonConvert.SerializeObject(jsonObject, Formatting.None);

                return minifiedJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error minifying JSON: " + ex.Message);
                return null;
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {


        }



        static string NormalizeJsonContent(string jsonContent)
        {

            return jsonContent.Replace("\n", "").Replace("\r", "").Replace(" ", "");
        }
        private static string FormatAsUuid(string hash)
        {

            return $"{hash.Substring(0, 8)}-{hash.Substring(8, 4)}-{hash.Substring(12, 4)}-{hash.Substring(16, 4)}-{hash.Substring(20, 12)}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Chilkat.Http http = new Chilkat.Http();
            bool success;

            Chilkat.HttpRequest req = new Chilkat.HttpRequest();
            req.AddParam("grant_type", "client_credentials");
            // Use your actual client ID and client secret...
            req.AddParam("client_id", "d0394a9f-0607-40de-a978-2d3eb8375b04");
            req.AddParam("client_secret", "6d62315e-d65a-4e41-9112-4195ea834edf");

            req.AddHeader("posserial", "1234567899");
            req.AddHeader("pososversion", "os");
            req.AddHeader("posmodelframework", "1");
            req.AddHeader("presharedkey", "03ac674216f3e1...");
        }
        public static string GenerateUuidFromContent(string content)
        {

            Crypt2 crypt = new Crypt2();


            bool success = crypt.UnlockComponent("YourUnlockCode");
            if (!success)
            {
                Console.WriteLine(crypt.LastErrorText);
                return null;
            }


            crypt.HashAlgorithm = "sha256";


            byte[] contentBytes = Encoding.UTF8.GetBytes(content);


            crypt.EncodingMode = "hex";
            string hashHex = crypt.HashStringENC(content);

            return hashHex.ToLower();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string connectionString = ConfigurationManager.ConnectionStrings["DentalClinicConnectionString1"].ConnectionString.ToString();


                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


                    using (var stream = File.Open(filePath, FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            simpleButton3.Enabled = true;
                            //var result = reader.AsDataSet();
                            //DataTable sheet1 = result.Tables[0];
                            //DataTable sheet2 = result.Tables[1];

                            //SaveDocumentsToDatabase(sheet1, connectionString);
                            //SaveItemsToDatabase(sheet2, connectionString);
                        }
                    }
                }
            }
        }
    }
}


