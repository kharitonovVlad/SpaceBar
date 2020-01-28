using System.IO;
using System;
using System.Collections.ObjectModel;
using Excel = Microsoft.Office.Interop.Excel;
using fckgg.Model;

namespace fckgg.Controllers
{
    internal class ReportController
    {
        public static void ExportOrderToExel(Order order)
        {
            int Id = 1;
            var path = Path.Combine("/ExcelTekst.xlsx");
            var ExcelApp = new Excel.Application();
            ExcelApp.Visible = true;
            var book = ExcelApp.Workbooks.Open(path);
            var sheet = (Excel.Worksheet)book.Worksheets[1];
            sheet.UsedRange.Replace("#date", order.DateTime);
            sheet.UsedRange.Replace("#total", order.Total);
            var range = sheet.UsedRange.Find("#tableRow");
            var row = range.Row;
            foreach (var item in order.names)
            {
                sheet.Cells[row, 2] = Id;
                sheet.Cells[row, 3] = item.Title;
                sheet.Cells[row, 6] = item.Count;
                row++;
                Id++;
            }
        }

        public static void ExportAllOrdersToExcel(ObservableCollection<Order> orders)
        {
            int Id = 1;
            int Total = 0;
            var path = Path.Combine("/ExcelAll.xlsx");
            var ExcelApp = new Excel.Application();
            ExcelApp.Visible = true;
            var book = ExcelApp.Workbooks.Open(path);
            var sheet = (Excel.Worksheet)book.Worksheets[1];
            sheet.UsedRange.Replace("#date", DateTime.Now);

            var range = sheet.UsedRange.Find("#tableRow");
            var row = range.Row;
            foreach (var item in orders)
            {
                sheet.Cells[row, 2] = Id;
                sheet.Cells[row, 3] = item.DateTime;
                sheet.Cells[row, 4] = item.Total;
                Total += item.Total;
                row++;
                Id++;
            }
            sheet.UsedRange.Replace("#total", Total);
        }
    }
}
