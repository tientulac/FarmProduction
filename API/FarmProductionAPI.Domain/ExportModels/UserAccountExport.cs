using FarmProductionAPI.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.ExportModels
{
    public class UserAccountExport
    {
        [Display(Name = "Tên đăng nhập")]
        public string? UserName { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }
        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
        [Display(Name = "Ngày tạo")]
        public string? CreatedAt { get; set; }
        [Display(Name = "Trạng thái xóa")]
        public bool? IsSoftDeleted { get; set; }
        [Display(Name = "Quyền")]
        public string? RoleName { get; set; }   
    }
}
