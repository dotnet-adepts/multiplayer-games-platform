using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Obecne hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Obecne hasło")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nowe hasło jest wymagane")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$", ErrorMessage = "Hasło musi zawierać minimum jedną małą literę, jedną dużą literę, jedną cyfrę oraz jeden znak specjalny")]
        [StringLength(100, ErrorMessage = "{0} musi mieć od {2} do {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Powtórzenie hasła jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("NewPassword", ErrorMessage = "Podane hasła są różne")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
