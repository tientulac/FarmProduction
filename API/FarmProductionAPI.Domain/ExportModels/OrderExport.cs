using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.ExportModels
{
    public class OrderExport
    {
        [Display(Name = "Mã hóa đơn")]
        public string? Code { get; set; }
        [Display(Name = "Trạng thái")]
        public string? Status { get; set; }
        [Display(Name = "Loại hóa đơn")]
        public string? Type { get; set; }
        [Display(Name = "Người mua")]
        public string? UserAccount { get; set; }
        [Display(Name = "Người bán")]
        public string? SellerAccount { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal? Total { get; set; }
        [Display(Name = "Phí ship")]
        public decimal? PaymentShip { get; set; }
        [Display(Name = "Loại thanh toán")]
        public string? PaymentType { get; set; }
        [Display(Name = "Ngày tạo")]
        public string? CreatedAt { get; set; }
        [Display(Name = "Ngày sửa")]
        public string? UpdatedAt { get; set; }
        [Display(Name = "Ngày hủy")]
        public string? DeletedAt { get; set; }
        [Display(Name = "Trạng thái hủy")]
        public bool? IsSoftDeleted { get; set; }
    }
}
