using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LocadoraFilmes.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LocadoraFilmes.Dao
{
    public class Contex : DbContext
    {
        public Contex() : base("DBLocadora") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genero> Genero { get; set; }
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Locacao> Locacao { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}