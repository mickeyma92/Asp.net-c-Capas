using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_Project01.Controllers
{
    public class HomeController : Controller
    {

        private Alumno alumno = new Alumno();
        private Curso Curso = new Curso();
        public ActionResult Index()
        {
            return View(alumno.listar());
        }
        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }
        public ActionResult Crud(int id = 0)
        {
            ViewBag.Cursos = Curso.Todo();
            return View(
                id > 0 ? alumno.Obtener(id)
                        : alumno
            );
        }
        public ActionResult Guardar(Alumno model, int[] cursos = null)
        {
            if (cursos != null)
            {
                foreach (var c in cursos)
                    model.Cursos.Add(new Curso { id = c });
            }
            else
            {
                ModelState.AddModelError("CursosSeleccionados", "Debe seleccionar por lo menos un curso!");
            }
            if(ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Home/Crud/" + model.id);
            }
            else
            {
                ViewBag.Cursos = Curso.Todo();
                return View("~/Views/Home/Crud.cshtml", model);
            }
        }
        public ActionResult Eliminar(int id)
        {
            alumno.Eliminar(id);
            return Redirect("~/Home");
        }
    }
}
