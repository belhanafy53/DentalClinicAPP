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
using Infragistics.Olap;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Ocsp;
using Infragistics.Win.UltraDataGridView;
//using System.Web.UI.WebControls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static DentalClinicAPP.CurrentCode;
using System.Reflection;
using Infragistics.Win.UltraWinPivotGrid;
using DevComponents.DotNetBar.Controls;
using DevExpress.ChartRangeControlClient.Core;
using DevExpress.Xpo;


namespace DentalClinicAPP
{
    public partial class GetFrm : Form
    {
        public GetFrm()
        {
            InitializeComponent();
            dataGridViewInvoices.CellContentClick += dataGridViewInvoices_CellContentClick;
        }

        private void GetFrm_Load(object sender, EventArgs e)
        {

        }
        
        
        private DataTable _dataTable;
        private int _pageSize = 10;
        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _totalRecords = 0;
        private List<InvoiceResponse> _invoices = new List<InvoiceResponse>();
        private async void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                marqueeProgressBarControl1.Visible = true;
                // تحقق من وجود التوكن، وإذا لم يكن موجوداً، احصل عليه
                if (string.IsNullOrEmpty(_accessToken))
                {
                    await GetAccessTokenAsync();
                }

                // إعدادات العميل
                var options = new RestClientOptions("https://api.invoicing.eta.gov.eg")
                {
                    MaxTimeout = -1,
                };
                int pageNumber = 1; // متغير لتتبع رقم الصفحة
                bool morePages = true; // علامة للتحقق من وجود المزيد من الصفحات
                // إعداد الطلب

                var client = new RestClient(options);
                var request = new RestRequest("/api/v1.0/documents/search?submissionDateFrom="+dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") +"&submissionDateTo=" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") +"&continuationToken=&pageSize=100&issueDateFrom=&issueDateTo=&direction=&status="+ comboBox1.Text+"&documentType="+comboBox2.Text+"&receiverType=&receiverId=&issuerType=&issuerId=&uuid=&internalID=", Method.Get);
                request.AddHeader("PageSize", "1000");
                request.AddHeader("PageNo", "5");

                // إضافة التوكن بشكل صحيح
                request.AddHeader("Authorization", $"{_tokenType} {_accessToken}");

                // تنفيذ الطلب
                RestResponse response = await client.ExecuteAsync(request);

                // التحقق من نجاح الطلب
                if (response.IsSuccessful)
                {
                    // تحليل JSON إلى قائمة من الفواتير
                    var invoices = JsonConvert.DeserializeObject<InvoiceResponse>(response.Content);
                   

                    // تعيين البيانات إلى DataGridView
                    

                    bindingSource1.DataSource = invoices.result;
                    marqueeProgressBarControl1.Visible = false;
                    bindingNavigator1.BindingSource = bindingSource1;
                    _dataTable = new DataTable();
                    _dataTable.Columns.Add("UUID", typeof(string));
                    _dataTable.Columns.Add("SubmissionUUID", typeof(string));
                    _dataTable.Columns.Add("LongId", typeof(string));
                    _dataTable.Columns.Add("InternalId", typeof(string));
                    _dataTable.Columns.Add("TypeName", typeof(string));
                    _dataTable.Columns.Add("DocumentTypeNamePrimaryLang", typeof(string));
                    _dataTable.Columns.Add("DocumentTypeNameSecondaryLang", typeof(string));
                    _dataTable.Columns.Add("TypeVersionName", typeof(string));
                    _dataTable.Columns.Add("IssuerId", typeof(string));
                    _dataTable.Columns.Add("IssuerName", typeof(string));
                    _dataTable.Columns.Add("IssuerType", typeof(string));
                    _dataTable.Columns.Add("ReceiverId", typeof(string));
                    _dataTable.Columns.Add("ReceiverName", typeof(string));
                    _dataTable.Columns.Add("ReceiverType", typeof(string));
                    _dataTable.Columns.Add("DateTimeIssued", typeof(string));
                    _dataTable.Columns.Add("DateTimeReceived", typeof(string));
                    _dataTable.Columns.Add("TotalSales", typeof(decimal));
                    _dataTable.Columns.Add("TotalDiscount", typeof(decimal));
                    _dataTable.Columns.Add("NetAmount", typeof(decimal));
                    _dataTable.Columns.Add("Total", typeof(decimal));
                    _dataTable.Columns.Add("CancelRequestDate", typeof(string));
                    _dataTable.Columns.Add("RejectRequestDate", typeof(string));
                    _dataTable.Columns.Add("Status", typeof(string));
                    _dataTable.Columns.Add("CreatedByUserId", typeof(string));
                    _dataTable.Columns.Add("FreezeStatus", typeof(FreezeStatus));
                    _dataTable.Columns.Add("DocumentStatusReason", typeof(string));
                    _dataTable.Columns.Add("LateSubmissionRequestNumber", typeof(string));
                    // أضف المزيد من الأعمدة حسب الحاجة

                    // ملء DataTable بالبيانات
                    foreach (var invoice in invoices.result)
                    {
                        _dataTable.Rows.Add(invoice.Uuid, invoice.SubmissionUUID, invoice.LongId, invoice.InternalId,
                            invoice.TypeName, invoice.DocumentTypeNamePrimaryLang, invoice.DocumentTypeNameSecondaryLang,
                            invoice.TypeVersionName, invoice.IssuerId, invoice.IssuerName, invoice.IssuerType,
                            invoice.ReceiverId, invoice.ReceiverName, invoice.ReceiverType, invoice.DateTimeIssued,
                            invoice.DateTimeReceived, invoice.TotalSales, invoice.TotalDiscount, invoice.NetAmount,
                            invoice.Total, invoice.CancelRequestDate, invoice.RejectRequestDate, invoice.Status,
                            invoice.CreatedByUserId, invoice.FreezeStatus, invoice.DocumentStatusReason,
                            invoice.LateSubmissionRequestNumber);
                    }

                    // حساب إجمالي الصفحات
                    //_totalPages = (int)Math.Ceiling(invoices.TotalRecords / _pageSize);

                    _totalRecords = invoices.result.Count > 0 ? (int)invoices.result[0].TotalRecords : 0;
                    _totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
                    labelPageNumber.Text = bindingNavigator1.CountItem.ToString();
                    bindingSource1.DataSource = invoices.result;
                    dataGridViewInvoices.DataSource = bindingSource1;

                    // تحديث أزرار التنقل
                    UpdatePaginationControls();

                    // تعيين البيانات إلى UltraPivotGrid

                }
                else
                {
                    MessageBox.Show("الحد الأقصى للبحث مده لا تتجاوز شهر " + response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }
        private void DisplayPage(int pageNumber)
        {
            // حساب العناصر التي يجب عرضها بناءً على الصفحة الحالية
            int start = (pageNumber - 1) * _pageSize;
            int end = Math.Min(start + _pageSize, _dataTable.Rows.Count);

            // نسخ الصفوف من DataTable للصفحة الحالية فقط
            DataTable pageTable = _dataTable.Clone(); // إنشاء نسخة من هيكل الجدول فقط
            for (int i = start; i < end; i++)
            {
                pageTable.ImportRow(_dataTable.Rows[i]);
            }

            // ربط البيانات بـ BindingSource
            bindingSource1.DataSource = pageTable;
            dataGridViewInvoices.DataSource = bindingSource1;

            // تحديث حالة الأزرار أو المؤشر إذا لزم الأمر
            UpdatePaginationControls();
        }

        private void UpdatePaginationControls()
        {
            buttonPrevious.Enabled = _currentPage > 1;
            buttonNext.Enabled = _currentPage < _totalPages;
        }
      
       

        public class InvoiceResponse
        {
            public List<result> result { get; set; }
            public double TotalRecords { get; set; }
        }

        public class result
        {
            //public string PublicURL { get; set; }
          
            public string Uuid { get; set; }
            public string SubmissionUUID { get; set; }
            public string LongId { get; set; }
            public string InternalId { get; set; }
            public string TypeName { get; set; }
            public string DocumentTypeNamePrimaryLang { get; set; }
            public string DocumentTypeNameSecondaryLang { get; set; }
            public string TypeVersionName { get; set; }
            public string IssuerId { get; set; }
            public string IssuerName { get; set; }
            public string IssuerType { get; set; }
            public string ReceiverId { get; set; }
            public string ReceiverName { get; set; }
            public string ReceiverType { get; set; }
            public string DateTimeIssued { get; set; }
            public string DateTimeReceived { get; set; }
            public decimal TotalSales { get; set; }
            public decimal TotalDiscount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal Total { get; set; }
            public string CancelRequestDate { get; set; }
            public string RejectRequestDate { get; set; }
            public string Status { get; set; }
            public string CreatedByUserId { get; set; }
            public FreezeStatus FreezeStatus { get; set; }
            public string DocumentStatusReason { get; set; }
            public string LateSubmissionRequestNumber { get; set; }
            public double TotalRecords { get; set; }
        }

        public class FreezeStatus
        {
            public bool Frozen { get; set; }
            public string Type { get; set; }
            public string Scope { get; set; }
            public string ActionDate { get; set; }
            public string AuCode { get; set; }
            public string AuName { get; set; }
        }
        private static string _accessToken;
        private static string _tokenType;
        //private DataTable ToDataTable(List<result> invoices)
        //{
        //    var table = new DataTable();
        //    table.Columns.Add("Uuid", typeof(string));
        //    table.Columns.Add("InternalId", typeof(string));
        //    table.Columns.Add("IssuerName", typeof(string));
        //    table.Columns.Add("ReceiverName", typeof(string));
        //    table.Columns.Add("DateTimeIssued", typeof(string));
        //    table.Columns.Add("Total", typeof(decimal));
        //    // أضف أعمدة إضافية بناءً على الحقول التي تريد عرضها

        //    foreach (var invoice in invoices)
        //    {
        //        table.Rows.Add(invoice.Uuid, invoice.InternalId, invoice.IssuerName, invoice.ReceiverName, invoice.DateTimeIssued, invoice.Total);
        //    }

        //    return table;
        //}

        public async Task GetAccessTokenAsync()
        {
            try
            {
                MyWebRequest myRequest = new MyWebRequest("https://id.eta.gov.eg/connect/token", "POST", "grant_type=client_credentials&Client_id=44627186-a9af-444c-8666-e87d0590584e&Client_secret=0d981678-80b0-4e51-b7b5-c97b9abe8fe6");
                dynamic objReturn = await myRequest.GetLoginAsync();
                _tokenType = objReturn.token_type;
                _accessToken = objReturn.access_token;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الحصول على التوكن: " + ex.Message);
            }
        }


        public async void getAccessToken()
        {
            MyWebRequest myRequest = new MyWebRequest("https://id.eta.gov.eg/connect/token", "POST", "grant_type=client_credentials&Client_id=44627186-a9af-444c-8666-e87d0590584e&Client_secret=0d981678-80b0-4e51-b7b5-c97b9abe8fe6");
            dynamic objReturn = await myRequest.GetLoginAsync(); // استخدام await للحصول على النتيجة الفعلية
            string tokenType = objReturn.token_type;
            string accessToken = objReturn.access_token;
        }
        private async void DownloadInvoice(string uuid)
        {
            try
            {
                // تحقق من وجود التوكن، وإذا لم يكن موجوداً، احصل عليه
                if (string.IsNullOrEmpty(_accessToken))
                {
                    await GetAccessTokenAsync();
                }

                var options = new RestClientOptions("https://api.invoicing.eta.gov.eg")
                {
                    MaxTimeout = -1,
                };

                var client = new RestClient(options);
                var request = new RestRequest($"/api/v1/documents/{uuid}/pdf", Method.Get);
                request.AddHeader("Authorization", $"{_tokenType} {_accessToken}");

                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "PDF Files|*.pdf",
                        FileName = $"{uuid}.pdf"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllBytes(saveFileDialog.FileName, response.RawBytes);
                        MessageBox.Show("تم تحميل الفاتورة بنجاح.");

                        // عرض الفاتورة بعد تحميلها
                        radPdfViewer1.LoadDocument(saveFileDialog.FileName);
                    }
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء تحميل الفاتورة: " + response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }


        private void dataGridViewInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0  )
            //{
                //var row = dataGridViewInvoices.Rows[e.RowIndex];
            
            //}
        }

        private  void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                string uuid = dataGridViewInvoices.CurrentRow.Cells[0].Value.ToString();// row.Cells[0].Value.ToString(); // التأكد من أن اسم العمود هو "UUID"

                // استدعاء الدالة لتحميل الفاتورة
                DownloadInvoice(uuid);
            }
            catch { }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            DisplayPage(_currentPage - 1);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //if (bindingSource1.Position < bindingSource1.Count - 1)
            //{
            //    bindingSource1.MoveNext();
            //}
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                DisplayPage(_currentPage);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            buttonX1_Click(sender, e);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            buttonX2_Click(sender, e);
            panelControl1.Visible = true ;
            simpleButton2.Enabled = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            panelControl1.Visible = false;
        }

        private void dataGridViewInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewInvoices.CurrentRow.Cells[0].Value.ToString() != null)
                {
                    simpleButton2.Enabled = true;
                }
                else
                {
                    simpleButton2.Enabled = false;
                }
            }
            catch { }
        }

        private async Task simpleButton4_ClickAsync(object sender, EventArgs e)
        {
            
        }

        private async void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                marqueeProgressBarControl1.Visible = true;
                // تحقق من وجود التوكن، وإذا لم يكن موجوداً، احصل عليه
                if (string.IsNullOrEmpty(_accessToken))
                {
                    await GetAccessTokenAsync();
                }

                // إعدادات العميل
                var options = new RestClientOptions("https://api.invoicing.eta.gov.eg")
                {
                    MaxTimeout = -1,
                };

                // إعداد الطلب
                var client = new RestClient(options);
                var request = new RestRequest("/api/v1.0/documents/recent?pageNo=1&pageSize=100&submissionDateFrom=" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "&submissionDateTo=" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "&issueDateFrom=&issueDateTo=&direction=&status=" + comboBox1.Text + "&documentType=" + comboBox2.Text + "&receiverType=&receiverId=&issuerType=&issuerId=", Method.Get);

               // var request = new RestRequest("/api/v1.0/documents/recent?submissionDateFrom=" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "&submissionDateTo=" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "&continuationToken=&pageSize=100&issueDateFrom=&issueDateTo=&direction=&status=" + comboBox1.Text + "&documentType=" + comboBox2.Text + "&receiverType=&receiverId=&issuerType=&issuerId=&uuid=&internalID=", Method.Get);
                request.AddHeader("PageSize", "1000");
                request.AddHeader("PageNo", "5");

                // إضافة التوكن بشكل صحيح
                request.AddHeader("Authorization", $"{_tokenType} {_accessToken}");

                // تنفيذ الطلب
                RestResponse response = await client.ExecuteAsync(request);

                // التحقق من نجاح الطلب
                if (response.IsSuccessful)
                {
                    // تحليل JSON إلى قائمة من الفواتير
                    var invoices = JsonConvert.DeserializeObject<InvoiceResponse>(response.Content);


                    // تعيين البيانات إلى DataGridView


                    bindingSource1.DataSource = invoices.result;
                    marqueeProgressBarControl1.Visible = false;
                    bindingNavigator1.BindingSource = bindingSource1;
                    _dataTable = new DataTable();
                    _dataTable.Columns.Add("UUID", typeof(string));
                    _dataTable.Columns.Add("SubmissionUUID", typeof(string));
                    _dataTable.Columns.Add("LongId", typeof(string));
                    _dataTable.Columns.Add("InternalId", typeof(string));
                    _dataTable.Columns.Add("TypeName", typeof(string));
                    _dataTable.Columns.Add("DocumentTypeNamePrimaryLang", typeof(string));
                    _dataTable.Columns.Add("DocumentTypeNameSecondaryLang", typeof(string));
                    _dataTable.Columns.Add("TypeVersionName", typeof(string));
                    _dataTable.Columns.Add("IssuerId", typeof(string));
                    _dataTable.Columns.Add("IssuerName", typeof(string));
                    _dataTable.Columns.Add("IssuerType", typeof(string));
                    _dataTable.Columns.Add("ReceiverId", typeof(string));
                    _dataTable.Columns.Add("ReceiverName", typeof(string));
                    _dataTable.Columns.Add("ReceiverType", typeof(string));
                    _dataTable.Columns.Add("DateTimeIssued", typeof(string));
                    _dataTable.Columns.Add("DateTimeReceived", typeof(string));
                    _dataTable.Columns.Add("TotalSales", typeof(decimal));
                    _dataTable.Columns.Add("TotalDiscount", typeof(decimal));
                    _dataTable.Columns.Add("NetAmount", typeof(decimal));
                    _dataTable.Columns.Add("Total", typeof(decimal));
                    _dataTable.Columns.Add("CancelRequestDate", typeof(string));
                    _dataTable.Columns.Add("RejectRequestDate", typeof(string));
                    _dataTable.Columns.Add("Status", typeof(string));
                    _dataTable.Columns.Add("CreatedByUserId", typeof(string));
                    _dataTable.Columns.Add("FreezeStatus", typeof(FreezeStatus));
                    _dataTable.Columns.Add("DocumentStatusReason", typeof(string));
                    _dataTable.Columns.Add("LateSubmissionRequestNumber", typeof(string));
                    // أضف المزيد من الأعمدة حسب الحاجة

                    // ملء DataTable بالبيانات
                    foreach (var invoice in invoices.result)
                    {
                        _dataTable.Rows.Add(invoice.Uuid, invoice.SubmissionUUID, invoice.LongId, invoice.InternalId,
                            invoice.TypeName, invoice.DocumentTypeNamePrimaryLang, invoice.DocumentTypeNameSecondaryLang,
                            invoice.TypeVersionName, invoice.IssuerId, invoice.IssuerName, invoice.IssuerType,
                            invoice.ReceiverId, invoice.ReceiverName, invoice.ReceiverType, invoice.DateTimeIssued,
                            invoice.DateTimeReceived, invoice.TotalSales, invoice.TotalDiscount, invoice.NetAmount,
                            invoice.Total, invoice.CancelRequestDate, invoice.RejectRequestDate, invoice.Status,
                            invoice.CreatedByUserId, invoice.FreezeStatus, invoice.DocumentStatusReason,
                            invoice.LateSubmissionRequestNumber);
                    }

                    // حساب إجمالي الصفحات
                    //_totalPages = (int)Math.Ceiling(invoices.TotalRecords / _pageSize);

                    _totalRecords = invoices.result.Count > 0 ? (int)invoices.result[0].TotalRecords : 0;
                    _totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
                    labelPageNumber.Text = bindingNavigator1.CountItem.ToString();
                    bindingSource1.DataSource = invoices.result;
                    dataGridViewInvoices.DataSource = bindingSource1;

                    // تحديث أزرار التنقل
                    UpdatePaginationControls();

                    // تعيين البيانات إلى UltraPivotGrid

                }
                else
                {
                    MessageBox.Show("الحد الأقصى للبحث مده لا تتجاوز شهر " + response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }
    }

   
}

