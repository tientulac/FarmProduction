using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.ExportModels
{
    public class ProductExport
    {
        [Display(Name = "Tên sản phẩm")]
        public string? ProductName { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public string? Code { get; set; }
        [Display(Name = "Giá tiền (VNĐ)")]
        public decimal? Price { get; set; }
        [Display(Name = "Số lượng trong kho")]
        public int? Amount { get; set; }
        [Display(Name = "Đơn vị tính")]
        public string? Unit { get; set; }
        [Display(Name = "Giá trị")]
        public string? Value { get; set; }
        [Display(Name = "Ngày sản xuất")]
        public string? ManufactureDate { get; set; }
        [Display(Name = "Ngày hết hạn")]
        public string? ExpireDate { get; set; }
    }
}
