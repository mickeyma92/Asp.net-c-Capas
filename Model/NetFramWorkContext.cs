namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NetFramWorkContext : DbContext
    {
        public NetFramWorkContext()
            : base("name=NetFramWorkContext")
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        //public virtual DbSet<AlumnoCurso> AlumnoCurso { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
                .HasMany(e => e.Cursos)
                .WithMany(e => e.Alumno)
                .Map(m => m.ToTable("AlumnoCurso"));
        }
    }
}
