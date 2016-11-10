using System.ComponentModel.DataAnnotations;

namespace VL.ItsMe1110.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "用户名")]

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密码")]

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]

        [Compare(nameof(Password), ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

    }
}