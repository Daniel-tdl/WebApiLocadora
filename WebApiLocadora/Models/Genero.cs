using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocadoraFilmes.Models
{
    public class Genero
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeneroID { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public char Ativo { get; set; }
    }
}