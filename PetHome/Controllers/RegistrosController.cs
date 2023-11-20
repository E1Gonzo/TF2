using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetHome.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services.Description;

namespace PetHome.Controllers
{
    public class RegistrosController : Controller
    {
        string cadena = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        IEnumerable<Perro> perros()
        {
            List<Perro> temporal = new List<Perro>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("exec usp_perros", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                temporal.Add(new Perro()
                {
                    id_perro = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    edad = dr.GetInt32(2),
                    peso = dr.GetInt32(3),
                    raza = dr.GetString(4),
                    color = dr.GetString(5),
                    estadoSalud = dr.GetString(6),
                    lugarRescate = dr.GetString(7),
                    interesado = dr.GetString(8),
                });
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        IEnumerable<Interesado> interesados()
        {
            List<Interesado> temporal = new List<Interesado>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("usp_interesados", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                temporal.Add(new Interesado()
                {
                    id_Interesado = dr.GetInt32(0),
                    DNI_Interesado = dr.GetInt32(1),
                    nombre_Interesado = dr.GetString(2),
                    ApellidoPat = dr.GetString(3),
                    ApellidoMat = dr.GetString(4),
                    edad_Interesado = dr.GetInt32(5),
                    celular_Interesado = dr.GetInt32(6),
                    domicilio_Interesado = dr.GetString(7),
                    correo_Interesado = dr.GetString(8),
                    id_perro_Interesado = dr.GetInt32(9),

                });
            }
            dr.Close();
            cn.Close();
            return temporal;
        }
        /*---INTERESADOS---*/
        Interesado BuscarInteresado (int id)
        {
            Interesado reg;
            reg=interesados().Where(s=>s.id_Interesado==id).FirstOrDefault();
            return reg;
        }
        public ActionResult Crear_Interesados()
        {
            return View(new Interesado());
        }
        [HttpPost] public ActionResult Crear_Interesados(Interesado reg)
        {
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("USP_INSERTA_INTERESADOS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DNI", reg.DNI_Interesado);
                cmd.Parameters.AddWithValue("@Nombre", reg.nombre_Interesado);
                cmd.Parameters.AddWithValue("@apellidoPat", reg.ApellidoPat);
                if (string.IsNullOrEmpty(reg.ApellidoMat))
                {
                    cmd.Parameters.AddWithValue("@apellidoMat", "Indefinido");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@apellidoMat", reg.ApellidoMat);
                }
                cmd.Parameters.AddWithValue("@edad", reg.edad_Interesado);
                cmd.Parameters.AddWithValue("@celular", reg.celular_Interesado);
                cmd.Parameters.AddWithValue("@domicilio", reg.domicilio_Interesado);
                cmd.Parameters.AddWithValue("@correo", reg.correo_Interesado);
                cmd.Parameters.AddWithValue("@id_perro", reg.id_perro_Interesado);
                cmd.ExecuteNonQuery();
                ViewBag.mensaje = $"Se ha registrado exitosamente";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            finally
            { 
                cn.Close(); 
            }
            return View(reg);
        }
        public ActionResult ActualizarInteresados(int id)
        {
            Interesado reg = BuscarInteresado(id);
            return View(reg);
        }
        [HttpPost] public ActionResult ActualizarInteresados(Interesado reg)
        {
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("USP_ACTUALIZA_INTERESADOS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CODIGO", reg.id_Interesado);
                cmd.Parameters.AddWithValue("@DNI", reg.DNI_Interesado);
                cmd.Parameters.AddWithValue("@Nombre", reg.nombre_Interesado);
                cmd.Parameters.AddWithValue("@apellidoPat", reg.ApellidoPat);
                cmd.Parameters.AddWithValue("@apellidoMat", reg.ApellidoMat);
                cmd.Parameters.AddWithValue("@edad", reg.edad_Interesado);
                cmd.Parameters.AddWithValue("@celular", reg.celular_Interesado);
                cmd.Parameters.AddWithValue("@domicilio", reg.domicilio_Interesado);
                cmd.Parameters.AddWithValue("@correo", reg.correo_Interesado);
                cmd.Parameters.AddWithValue("@id_perro", reg.id_perro_Interesado);
                cmd.ExecuteNonQuery();
                ViewBag.mensaje = $"Se ha actualizado exitosamente";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            finally

            {
                cn.Close();
            }
            return View(reg);
        }
        public ActionResult DeleteInteresados(int id)
        {
            Interesado reg = BuscarInteresado(id);
            return View(reg);
        }
        [HttpPost] public ActionResult DeleteInteresadosConfirmed(Interesado reg)
        {
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("usp_elimina_interesados", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CODIGO", reg.id_Interesado);
                cmd.ExecuteNonQuery();
                return RedirectToAction("ListadoInteresados");
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View("DeleteInteresados", reg);
            }
            finally
            {
                cn.Close();
            }
        }
        /*---PERROS---*/
        Perro Buscar(int id)
        {
            Perro reg;
            reg = perros().Where(s => s.id_perro == id).FirstOrDefault();
            return reg;
        }
        public ActionResult Crear_Perros()
        {
            return View(new Perro());
        }
        [HttpPost] public ActionResult Crear_Perros(Perro reg)
        {
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_inserta_perros", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", reg.nombre);
                cmd.Parameters.AddWithValue("@edad", reg.edad);
                cmd.Parameters.AddWithValue("@peso", reg.peso);
                cmd.Parameters.AddWithValue("@raza", reg.raza);
                if (string.IsNullOrEmpty(reg.color))
                {
                    cmd.Parameters.AddWithValue("@color", "Indefinido");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@color", reg.color);
                }
                cmd.Parameters.AddWithValue("@estadoSalud", reg.estadoSalud);
                if (string.IsNullOrEmpty(reg.lugarRescate))
                {
                    cmd.Parameters.AddWithValue("@lugarRescate", "Indefinido");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@lugarRescate", reg.lugarRescate);
                }
                cmd.ExecuteNonQuery();
                ViewBag.mensaje = $"Se ha registrado 1 mascota";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return View(reg);

        }
        public ActionResult ActualizarPerro(int id) 
        {
            Perro reg2 = Buscar(id);
            return View(reg2); 
        }
        [HttpPost] public ActionResult ActualizarPerro(Perro reg)
        {
            SqlConnection cn = new SqlConnection(cadena); 
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_actualiza_perros", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", reg.id_perro);
                cmd.Parameters.AddWithValue("@nombre", reg.nombre);
                cmd.Parameters.AddWithValue("@edad", reg.edad);
                cmd.Parameters.AddWithValue("@peso", reg.peso);
                cmd.Parameters.AddWithValue("@raza", reg.raza);
                cmd.Parameters.AddWithValue("@color", reg.color);
                cmd.Parameters.AddWithValue("@estadoSalud", reg.estadoSalud);
                if (string.IsNullOrEmpty(reg.lugarRescate))
                {
                    cmd.Parameters.AddWithValue("@lugarRescate", "Indefinido");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@lugarRescate", reg.lugarRescate);
                }
                cmd.ExecuteNonQuery();
                ViewBag.mensaje = $"Se ha actualizado 1 mascota";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            finally
            { 
                cn.Close();
            }
            return View(reg);
        }
        public ActionResult DeletePerros(int id)
        {
            Perro reg = Buscar(id);
            return View(reg);
        }
        [HttpPost] public ActionResult DeletePerrosConfirmed(Perro reg)
        {
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("usp_elimina_perros", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod", reg.id_perro);
                cmd.ExecuteNonQuery();
                return RedirectToAction("MantenimientoListaPerros");
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View("DeletePerros", reg);
            }
            finally
            {
                cn.Close();
            }
        }
        //Views
        public ActionResult ListadoInteresados()
        {
            return View(interesados());
        }
        public ActionResult MantenimientoListaPerros()
        {
            return View(perros());
        }
        public ActionResult ListadoPerros()
        {
            return View(perros());
        }
        public ActionResult login()
        {
            return View(new Usuario());
        }
        [HttpPost] public ActionResult login(Usuario usu)
        {
            if(usu.nomUsuario == "U2023-17")
            {
                if(usu.contrasenia == "c0ntras3n1a")
                {
                    ViewBag.mensaje = "Bienvenido";
                    return RedirectToAction("menuMantenimiento");
                } else {
                    ViewBag.mensaje = "Contraseña incorrecta";
                }
            } else {
                ViewBag.mensaje = "Nombre de usuario incorrecto";
            }
            return View(usu);
        }
        public ActionResult menuMantenimiento()
        {
            return View();
        }
        /*---PDF---*/
        //Perros
        public ActionResult ListaPDFPerros()
        {
            return View(perros());
        }
        public ActionResult ListaPDFInteresados()
        {
            return View(interesados());
        }
        //Interesados
        public ActionResult PrintPerros()
        {
            return new Rotativa.ActionAsPdf("ListaPDFPerros")
            { FileName = "ListaPerros.pdf" };
        }
        public ActionResult PrintInteresados()
        {
            return new Rotativa.ActionAsPdf("ListaPDFInteresados")
            { FileName = "ListaInteresados.pdf" };
        }
    }
}