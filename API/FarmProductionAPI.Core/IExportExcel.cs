using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.ExportModels;
using FarmProductionAPI.Domain.Models;
using NetTopologySuite.Index.HPRtree;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core
{
    public interface IExportExcel<TEntity>
    {
        public byte[] ExportToExcel(List<TEntity> data, string filePath);
    }

    public class ExportToExcel<TEntity> : IExportExcel<TEntity> where TEntity : class
    {
        byte[] IExportExcel<TEntity>.ExportToExcel(List<TEntity> data, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Danh sach");

            // Assuming T is a class with properties to be exported
            var properties = typeof(TEntity).GetProperties();

            using (var tieu_de = workSheet.Cells[1, 1, 1, properties.Length])
            {
                tieu_de.Value = "DANH SÁCH BÁO CÁO THỐNG KÊ";
                tieu_de.Merge = true;
                tieu_de.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                tieu_de.Style.Font.Bold = true;
                tieu_de.Style.Font.Name = "Times New Roman";
                tieu_de.Style.Font.Size = 13;
            }

            // Adding header row
            for (var i = 0; i < properties.Length; i++)
            {
                DisplayAttribute displayAttribute = properties[i].GetCustomAttribute<DisplayAttribute>();
                workSheet.Cells[3, i + 1].Value = displayAttribute != null ? displayAttribute.Name : "";
                workSheet.Cells[3, i + 1].Style.Font.Bold = true;
                workSheet.Cells[3, i + 1].Style.Font.Size = 13;
                workSheet.Column(i + 1).Width = 30;
                workSheet.Column(i + 1).Style.WrapText = true;
                workSheet.Cells[3, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, i + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            }

            // Adding data rows
            var rowIndex = 4;
            foreach (var item in data)
            {
                for (var i = 0; i < properties.Length; i++)
                {
                    workSheet.Cells[rowIndex, i + 1].Value = properties[i].GetValue(item);
                    workSheet.Cells[rowIndex, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[rowIndex, i + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[rowIndex, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[rowIndex, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Row(rowIndex + 1).Height = 17;
                    workSheet.Cells[1, rowIndex + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }
                rowIndex++;
            }

            // Save the file
            FileInfo newFile = new FileInfo(filePath);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(filePath);
            }
            var fileInfo = new FileInfo($"{filePath}.xlsx");
            package.SaveAs(fileInfo);
            return package.GetAsByteArray();
        }
    }
}
