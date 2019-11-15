using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocadoraFilmes.Models
{
    public class Filme
    {
        public int FilmeID { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public Char Ativo { get; set; }
        public int GeneroID { get; set; }

        [ForeignKey("GeneroID")]
        public virtual Genero Genero { get; set; }
    }
}