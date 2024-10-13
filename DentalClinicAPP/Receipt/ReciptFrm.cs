using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Ocsp;
//using System.Web.UI.WebControls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Telerik.WinControls;
//*************************
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
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using ExcelDataReader;
using System.Configuration;
//using Chilkat;


namespace DentalClinicAPP.Receipt
{
    public partial class ReciptFrm : Form
    {
        public class ReceiptSubmission
        {
            public List<Receipt> Receipts { get; set; }
        }

        public class Receipt
        {
            public Header Header { get; set; }
            public DocumentType DocumentType { get; set; }
            public Seller Seller { get; set; }
            public Buyer Buyer { get; set; }
            public List<ItemData> ItemData { get; set; }
            public decimal TotalSales { get; set; }
            public decimal TotalCommercialDiscount { get; set; }
            public decimal TotalItemsDiscount { get; set; }
            public List<ExtraReceiptDiscountData> ExtraReceiptDiscountData { get; set; }
            public decimal NetAmount { get; set; }
            public decimal FeesAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public List<TaxTotal> TaxTotals { get; set; }
            public string PaymentMethod { get; set; }
            public decimal Adjustment { get; set; }
            public Contractor Contractor { get; set; }
            public Beneficiary Beneficiary { get; set; }
        }

        public class Header
        {
            public string DateTimeIssued { get; set; }
            public string ReceiptNumber { get; set; }
            public string Uuid { get; set; }
            public string PreviousUUID { get; set; }
            public string ReferenceOldUUID { get; set; }
            public string Currency { get; set; }
            public decimal ExchangeRate { get; set; }
            public string SOrderNameCode { get; set; }
            public string OrderDeliveryMode { get; set; }
            public decimal GrossWeight { get; set; }
            public decimal NetWeight { get; set; }
            public int? SendStatusId { get; set; }

            public string longId { get; set; }
            public string hashKey { get; set; }
            public string errorCode { get; set; }
            public string errorMessage { get; set; }
            public string errorTarget { get; set; }
            public string errorDetailsTarget { get; set; }
            public string errorDetailsMessage { get; set; }
            public DateTime? InsertDate { get; set; }
            public int? InsertedBy { get; set; }
            public DateTime? UpdateDate { get; set; }
            public int? UpdateBy { get; set; }
            //new
            public string OrderdeliveryMode { get; set; }
        }

        public class DocumentType
        {
            public string ReceiptType { get; set; }
            public string TypeVersion { get; set; }
        }

        public class Seller
        {
            public string Rin { get; set; }
            public string CompanyTradeName { get; set; }
            public string BranchCode { get; set; }
            public BranchAddress BranchAddress { get; set; }
            public string DeviceSerialNumber { get; set; }
            public string SyndicateLicenseNumber { get; set; }
            public string ActivityCode { get; set; }
        }

        public class BranchAddress
        {
            public string Country { get; set; }
            public string Governate { get; set; }
            public string RegionCity { get; set; }
            public string Street { get; set; }
            public string BuildingNumber { get; set; }
            public string PostalCode { get; set; }
            public string Floor { get; set; }
            public string Room { get; set; }
            public string Landmark { get; set; }
            public string AdditionalInformation { get; set; }
        }

        public class Buyer
        {
            public string Type { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string MobileNumber { get; set; }
            public string PaymentNumber { get; set; }
        }

        public class ItemData
        {
            public string InternalCode { get; set; }
            public string Description { get; set; }
            public string ItemType { get; set; }
            public string ItemCode { get; set; }
            public string UnitType { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal NetSale { get; set; }
            public decimal TotalSale { get; set; }
            public decimal Total { get; set; }
            public List<CommercialDiscountData> CommercialDiscountData { get; set; }
            public List<ItemDiscountData> ItemDiscountData { get; set; }
            public AdditionalCommercialDiscount AdditionalCommercialDiscount { get; set; }
            public AdditionalItemDiscount AdditionalItemDiscount { get; set; }
            public decimal ValueDifference { get; set; }
            public List<TaxableItem> TaxableItems { get; set; }
        }

        public class CommercialDiscountData
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public decimal Rate { get; set; }
            //new 
            public decimal? DiscountData { get; set; }
        }

        public class ItemDiscountData
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public decimal Rate { get; set; }
        }

        public class AdditionalCommercialDiscount
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public decimal Rate { get; set; }
        }

        public class AdditionalItemDiscount
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public decimal Rate { get; set; }
        }

        public class TaxableItem
        {
            public string TaxType { get; set; }
            public decimal Amount { get; set; }
            public string SubType { get; set; }
            public decimal Rate { get; set; }
        }

        public class ExtraReceiptDiscountData
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public decimal Rate { get; set; }
        }

        public class TaxTotal
        {
            public string TaxType { get; set; }
            public decimal Amount { get; set; }
        }

        public class Contractor
        {
            public string Name { get; set; }
            public decimal Amount { get; set; }
            public decimal Rate { get; set; }
        }

        public class Beneficiary
        {
            public decimal Amount { get; set; }
            public decimal Rate { get; set; }
        }

        static async Task Main(string[] args)
        {
            var submission = await GetReceiptSubmissionFromDatabase();
            string jsonPayload = JsonConvert.SerializeObject(submission, Formatting.Indented);
            MessageBox.Show(jsonPayload);
            await SendReceiptsInBatches(submission);
        }

        public ReciptFrm()
        {
            InitializeComponent();
        }
        static async Task<ReceiptSubmission> GetReceiptSubmissionFromDatabase()
        {
            var submission = new ReceiptSubmission { Receipts = new List<Receipt>() };
            string connectionString = "YourConnectionStringHere"; // تعديل بيانات الاتصال بقاعدة البيانات

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // جلب بيانات الهيدر من جدول Documents
                string headerQuery = "SELECT * FROM Documents WHERE DocumentId = @DocumentId";
                using (SqlCommand command = new SqlCommand(headerQuery, connection))
                {
                    command.Parameters.AddWithValue("@DocumentId", 1); // تعديل حسب الحاجة
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            var header = new Header
                            {
                                DateTimeIssued = reader["DateTimeIssued"].ToString(),
                                ReceiptNumber = reader["ReceiptNumber"].ToString(),
                                Uuid = GenerateUuid(), // توليد UUID جديد
                                PreviousUUID = reader["PreviousUUID"].ToString(),
                                ReferenceOldUUID = reader["ReferenceOldUUID"].ToString(),
                                Currency = reader["Currency"].ToString(),
                                ExchangeRate = Convert.ToDecimal(reader["ExchangeRate"]),
                                SOrderNameCode = reader["SOrderNameCode"].ToString(),
                                OrderDeliveryMode = reader["OrderDeliveryMode"].ToString(),
                                GrossWeight = Convert.ToDecimal(reader["GrossWeight"]),
                                NetWeight = Convert.ToDecimal(reader["NetWeight"]),
                                SendStatusId = null // سيتم تحديثه لاحقًا
                            };

                            var receipt = new Receipt
                            {
                                Header = header,
                                DocumentType = new DocumentType
                                {
                                    ReceiptType = reader["ReceiptType"].ToString(),
                                    TypeVersion = reader["TypeVersion"].ToString()
                                },
                                Seller = new Seller
                                {
                                    Rin = reader["Rin"].ToString(),
                                    CompanyTradeName = reader["CompanyTradeName"].ToString(),
                                    BranchCode = reader["BranchCode"].ToString(),
                                    BranchAddress = new BranchAddress
                                    {
                                        Country = reader["Country"].ToString(),
                                        Governate = reader["Governate"].ToString(),
                                        RegionCity = reader["RegionCity"].ToString(),
                                        Street = reader["Street"].ToString(),
                                        BuildingNumber = reader["BuildingNumber"].ToString(),
                                        PostalCode = reader["PostalCode"].ToString(),
                                        Floor = reader["Floor"].ToString(),
                                        Room = reader["Room"].ToString(),
                                        Landmark = reader["Landmark"].ToString(),
                                        AdditionalInformation = reader["AdditionalInformation"].ToString()
                                    },
                                    DeviceSerialNumber = reader["DeviceSerialNumber"].ToString(),
                                    SyndicateLicenseNumber = reader["SyndicateLicenseNumber"].ToString(),
                                    ActivityCode = reader["ActivityCode"].ToString()
                                },
                                Buyer = new Buyer
                                {
                                    Type = reader["BuyerType"].ToString(),
                                    Id = reader["BuyerId"].ToString(),
                                    Name = reader["BuyerName"].ToString(),
                                    MobileNumber = reader["BuyerMobileNumber"].ToString(),
                                    PaymentNumber = reader["BuyerPaymentNumber"].ToString()
                                },
                                // Continue to populate other fields similarly
                            };

                            submission.Receipts.Add(receipt);
                        }
                    }
                }
            }

            return submission;
        }

        static string GenerateUuid()
        {
            return Guid.NewGuid().ToString();
        }

        static async Task UpdateReceiptStatusAsync(List<Receipt> receipts, bool isSuccess, string errorMessage = null, string previousUUID = null)
        {
            string connectionString = "YourConnectionStringHere"; // تعديل بيانات الاتصال بقاعدة البيانات

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                foreach (var receipt in receipts)
                {
                    string updateQuery = @"
                UPDATE Documents
                SET SendStatusId = @SendStatusId,
                    ErrorMessage = @ErrorMessage,
                    PreviousUUID = @PreviousUUID
                WHERE ReceiptNumber = @ReceiptNumber";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SendStatusId", isSuccess ? 2 : 1); // 2 إذا كان الإرسال ناجح، 1 إذا كان هناك خطأ
                        command.Parameters.AddWithValue("@ErrorMessage", isSuccess ? (object)DBNull.Value : errorMessage);
                        command.Parameters.AddWithValue("@PreviousUUID", previousUUID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ReceiptNumber", receipt.Header.ReceiptNumber);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        static async Task SendReceipt(string jsonPayload, List<Receipt> receipts)
        {
            string baseUrl = "https://api.sit.invoicing.eta.gov.eg";
            var options = new RestClientOptions(baseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/v1/receipts/submission", Method.Post);
            request.AddHeader("Authorization", "Bearer YOUR_BEARER_TOKEN");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(jsonPayload);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // تحديث حالة الإرسال إلى ناجح
                await UpdateReceiptStatusAsync(receipts, true);
                MessageBox.Show("Receipt submitted successfully.");
            }
            else
            {
                // تحديث حالة الإرسال إلى فشل مع رسالة الخطأ
                await UpdateReceiptStatusAsync(receipts, false, response.Content);
                MessageBox.Show($"Error: {response.StatusCode}, {response.Content}");
            }
        }

        static async Task SendReceiptsInBatches(ReceiptSubmission submission)
        {
            // تقسيم البيانات إلى دفعات بحد أقصى 100 إيصال لكل دفعة
            var batches = SplitIntoBatches(submission.Receipts, 20);

            foreach (var batch in batches)
            {
                // تحويل كل دفعة إلى JSON
                string jsonPayload = JsonConvert.SerializeObject(new ReceiptSubmission { Receipts = batch }, Formatting.Indented);

                // إرسال الدفعة إلى API
                await SendReceipt(jsonPayload, batch);
            }
        }

        static List<List<T>> SplitIntoBatches<T>(List<T> items, int batchSize)
        {
            var batches = new List<List<T>>();
            for (int i = 0; i < items.Count; i += batchSize)
            {
                batches.Add(items.GetRange(i, Math.Min(batchSize, items.Count - i)));
            }
            return batches;
        }
        private void ReciptFrm_Load(object sender, EventArgs e)
        {

        }

       

        private async Task button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {

                var submission = await GetReceiptSubmissionFromDatabase();

                // إرسال الإيصالات في دفعات
                await SendReceiptsInBatches(submission);



            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
           await  button1_ClickAsync(sender, e);
        }
       
        static string GetSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        static string GenerateReceiptUuid(Receipt receipt)
        {
            // Serialize and normalize the receipt object
            string serializedReceipt = JsonConvert.SerializeObject(receipt, Formatting.None);

            // Flatten all properties into one line
            string normalizedText = serializedReceipt.Replace("\n", "").Replace("\r", "").Replace(" ", "");

            // Create SHA-256 hash value
            string sha256Hash = GetSha256Hash(normalizedText);

            // Return the hexadecimal string as the receipt UUID
            return sha256Hash;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //string filePath = "D/555555555555555555555555555.json"; // قم بتحديث المسار إلى ملف JSON الخاص بك
            //string uuid = GenerateUuidFromFile(filePath);
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
        static string GenerateUuidFromFile(string filePath)
        {
            // قراءة محتوى ملف JSON
            string jsonContent = File.ReadAllText(filePath);

            // تسلسل وتطبيع النص
            string normalizedText = NormalizeJsonContent(jsonContent);

            // إنشاء تجزئة SHA-256
            string sha256Hash = GetSha256Hash(normalizedText);

            return sha256Hash;
        }

        static string NormalizeJsonContent(string jsonContent)
        {
            // تحويل النص إلى JSON Object ثم تسلسله بطريقة موحدة
            // للتأكد من أن كل التنسيقات هي نفسها
            // يمكن استخدام مكتبة مثل Newtonsoft.Json للقيام بذلك
            // هنا نحن فقط سنزيل المسافات والتباعد للحفاظ على نص موحد
            return jsonContent.Replace("\n", "").Replace("\r", "").Replace(" ", "");
        }

        //***********************************
        //***********************************
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

        private void buttonX1_Click(object sender, EventArgs e)
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
                            var result = reader.AsDataSet();
                            DataTable sheet1 = result.Tables[0];
                            DataTable sheet2 = result.Tables[1];

                            //SaveDocumentsToDatabase(sheet1, connectionString);
                            //SaveItemsToDatabase(sheet2, connectionString);
                        }
                    }
                }


            }
            MessageBox.Show("البيانات غير متوافقة مع بنية الايصال الخاص بمصلحة الضرائب المصرية");
        }
    }
}
