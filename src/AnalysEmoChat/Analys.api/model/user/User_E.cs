using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Analys.api.model.user
{
    public class User_E
    {
        [Key] // مشخص می‌کند این پراپرتی کلید اصلی است
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-Increment
        public int Id { get; set; }

        [Required] // الزامی بودن فیلد
        [MaxLength(50)] // حداکثر طول رشته
        [Column("username")] // نام ستون در دیتابیس
        public string Username { get; set; }

        [Required]
        [EmailAddress] // اعتبارسنجی فرمت ایمیل
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
