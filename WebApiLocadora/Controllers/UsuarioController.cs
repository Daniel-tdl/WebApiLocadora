using LocadoraFilmes.Dao;
using LocadoraFilmes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApiLocadora.Controllers
{
    public class UsuarioController : ApiController
    {
        public UsuarioController() { }

        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                return context.Usuario.ToList();
            }
        }

        [HttpGet]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(string nome)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Usuario user = context.Usuario.FirstOrDefault(x => x.Nome.Equals(nome));
                if (user == null)
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.Conflict,
                            "Não existe um usuário cadastrado com este nome."));
                else
                    return Ok(user);
            }
        }

        [HttpPost]
        public IHttpActionResult Incluir([FromBody]Usuario user)
        {
            var NewUsuario = new Usuario
            {
                UsuarioID = user.UsuarioID,
                Nome = user.Nome,
                Senha = user.Senha
            };

            using (LocadoraContex context = new LocadoraContex())
            {
                Usuario usuario = context.Usuario.FirstOrDefault(u => u.Nome.Equals(user.Nome));

                if (usuario != null)
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.Conflict,
                            "Já existe um usuário cadastrado com este nome."));
                else
                {
                    context.Usuario.Add(NewUsuario);
                    context.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
            }
        }

        [HttpPut]
        public IHttpActionResult Editar([FromBody]Usuario user)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Usuario usuario = context.Usuario.FirstOrDefault(u => u.UsuarioID == user.UsuarioID);
                if (usuario != null)
                {
                    usuario.Nome = user.Nome;
                    usuario.Senha = user.Senha;
                    context.SaveChanges();
                    return Ok(user);
                }
                else
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Usuário não localizado para alteração."));
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Usuario usuario = context.Usuario.FirstOrDefault(u => u.UsuarioID == id);

                if (usuario != null)
                {
                    context.Usuario.Remove(usuario);
                    context.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
                else
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "usuário não localizado para exclusão."));
            }
        }
    }
}
