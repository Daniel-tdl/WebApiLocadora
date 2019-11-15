using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraFilmes.Models
{
    public class Locacao
    {
        public int LocacaoID { get; set; }
        public string CPF { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual ICollection<Filme> Filmes { get; set; }
    }
}