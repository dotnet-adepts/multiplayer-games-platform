using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Użytkownik")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Podano niepoprawy adres e-mail")]
        public string Email { get; set; }

        public string StatusMessage { get; set; }
    }
}
