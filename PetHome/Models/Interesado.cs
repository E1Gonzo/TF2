using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetHome.Models
{
    public class Interesado
    {
        [Display(Name = "Código")] [Required]public int id_Interesado { get; set; }
        [Display(Name = "DNI")][Required] public int DNI_Interesado { get; set; }
        [Display(Name = "Nombre")][Required] public string nombre_Interesado { get; set; }
        [Display(Name = "Apellido Paterno")][Required] public string ApellidoPat { get; set; }
        [Display(Name = "Apellido Materno")] public string ApellidoMat { get; set; }
        [Display(Name = "Edad")] public int edad_Interesado { get; set; }
        [Display(Name = "Celular")][Required] public int celular_Interesado { get; set; }
        [Display(Name = "Domicilio")] public string domicilio_Interesado { get; set; }
        [Display(Name = "Correo")][DataType(DataType.EmailAddress)][Required] public string correo_Interesado { get; set; }
        [Display(Name = "Perro de interés")] public int id_perro_Interesado { get; set; }
    }
}