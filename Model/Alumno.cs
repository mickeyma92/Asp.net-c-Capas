namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.SqlClient;
    [Table("Alumno")]
    public partial class Alumno
    {
        public Alumno()
        {
            Cursos = new List<Curso>();
        }
        public ICollection<Curso> Cursos { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Required(ErrorMessage="Debe ingresar su nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "jaja no ha ingresado su apellido wey!")]
        [StringLength(50,ErrorMessage="Como maximo debe ingresar 50 caracteres ")]
        public string Apellido { get; set; }
        public List<Alumno> listar()
        {
            var alumnos = new List<Alumno>();
            try
            {
                using (var ctx = new NetFramWorkContext())
                {
                    alumnos = ctx.Alumno.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return alumnos;
        }
        public Alumno Obtener(int id)
        {
            var alumno = new Alumno();
            try {
                using (var ctx = new NetFramWorkContext())
                {
                    alumno = ctx.Alumno
                                        .Include("Cursos")
                                        .Where(x => x.id == id)
                                        .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return alumno;
        }
        public void Guardar()
        {
            try
            {
                using (var context = new NetFramWorkContext())
                {
                    if (this.id == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                    }
                    else
                    {
                        context.Database.ExecuteSqlCommand(
                            "DELETE FROM AlumnoCurso WHERE Alumno_id = @id",
                            new SqlParameter("id", this.id)
                        );

                        var cursoBK = this.Cursos;

                        this.Cursos = null;
                        context.Entry(this).State = EntityState.Modified;
                        this.Cursos = cursoBK;
                    }

                    foreach (var c in this.Cursos)
                        context.Entry(c).State = EntityState.Unchanged;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Eliminar(int id)
        {
            try
            {
                using (var ctx = new NetFramWorkContext())
                {
                    ctx.Entry(new Alumno { id = id }).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
