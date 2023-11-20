using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetHome.Models
{
    public class Usuario
    {
        public string nomUsuario { get; set; }
        public string contrasenia { get; set;}

        public Usuario()
        {
            nomUsuario = string.Empty;
            contrasenia = string.Empty;
        }
    }
}