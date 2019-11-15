using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraFilmes.Models
{
    public class Locacao
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocacaoID { get; set; }
        [Required]
        public string CPF { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual ICollection<Filme> Filmes { get; set; }
    }
}