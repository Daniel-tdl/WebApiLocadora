using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraFilmes.Models
{
    public class Genero
    {
        public int GeneroID { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public char Ativo { get; set; }
    }
}