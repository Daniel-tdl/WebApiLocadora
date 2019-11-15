using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocadoraFilmes.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        [Index(IsUnique = true)]
        [StringLength(200)]
        [Required]
        public string Nome { get; set; }
        [Required] 
        public string Senha { get; set; }   
    }
}