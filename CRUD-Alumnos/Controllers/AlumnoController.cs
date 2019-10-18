using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                using (var db = new AlumnosContext()) //para no dejar la conexion de DB libre
                {
                    return View(db.Alumno.ToList());  //ME MUESTRA UNA LISTA DE ALUMNOS
                                                      //Un ejemplo: List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
               
            
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost] //PARA QUE RECIBA LA ENTIDAD ALUMNO
        [ValidateAntiForgeryToken] //GOOGLEAR, SON DETALLITOS
        public ActionResult Agregar(Alumno a)
        {
            
            if (!ModelState.IsValid)//si el nombre que entra no es válido...
                    return View();
            try
            {
                

                using (AlumnosContext db = new AlumnosContext()) ///Para abrir y cerrar la conexion
                {
                    a.FechaRegistro = DateTime.Now;

                    db.Alumno.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("","Error Registro de Alumno - " + ex.Message);
                return View();
            }           
        }
    }
}