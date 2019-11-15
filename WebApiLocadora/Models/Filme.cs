using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocadoraFilmes.Models
{
    public class Filme
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilmeID { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        [Required]
        public Char Ativo { get; set; }
        public int GeneroID { get; set; }

        [ForeignKey("GeneroID")]
        public virtual Genero Genero { get; set; }
    }
}