using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;



namespace DentalClinicAPP
{
    public static class ExcelToJsonConverter
    {
        public static string ConvertExcelToJson(string filePath)
        {
            var dataTable = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // تحقق من وجود أوراق عمل
                if (package.Workbook.Worksheets.Count == 0)
                {
                    throw new InvalidOperationException("الملف لا يحتوي على أي أوراق عمل.");
                }

                // استخدام ورقة العمل الأولى
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                // تحقق من أن ورقة العمل تحتوي على بيانات
                if (worksheet.Dimension == null)
                {
                    throw new InvalidOperationException("ورقة العمل فارغة.");
                }

                var columns = worksheet.Dimension.End.Column;
                var rows = worksheet.Dimension.End.Row;

                // إضافة الأعمدة إلى DataTable
                for (int col = 1; col <= columns; col++)
                {
                    dataTable.Columns.Add(worksheet.Cells[1, col].Text);
                }

                // إضافة الصفوف إلى DataTable
                for (int row = 2; row <= rows; row++)
                {
                    var dataRow = dataTable.NewRow();
                    for (int col = 1; col <= columns; col++)
                    {
                        dataRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            // تحويل DataTable إلى JSON
            return JsonConvert.SerializeObject(dataTable);
        }
    }
    }
