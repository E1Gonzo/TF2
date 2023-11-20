using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PetHome.Models
{
    public class Perro
    {
        [Display(Name = "Código")] public int id_perro { get; set; }
        [Display(Name = "Nombre")] public string nombre { get; set; }
        [Display(Name = "Edad")] public int edad { get; set; }
        [Display(Name = "Peso")] public float peso { get; set; }
        [Display(Name = "Raza")] public string raza { get; set; }
        [Display(Name = "Color")] public string color { get; set; }
        [Display(Name = "Estado de salud")] public string estadoSalud { get; set; }
        [Display(Name = "Lugar de rescate")] public string lugarRescate { get; set; }
        [Display(Name = "Interesado")] public string interesado { get; set; }
    }
}