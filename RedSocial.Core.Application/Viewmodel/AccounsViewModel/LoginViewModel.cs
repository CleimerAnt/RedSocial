using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.AccounsViewModel
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre de Usuario es Requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La Contraseña es Requerida")]
        public string PassWord { get; set; }
        public bool HassError { get; set; }
        public string? Error { get; set; }
    }
}
