using LocadoraFilmes.Dao;
using LocadoraFilmes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApiLocadora.Controllers
{
    public class GeneroController : ApiController
    {
        [HttpGet]
        public IEnumerable<Genero> GetGeneros()
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                return context.Genero.ToList();
            }
        }

        [HttpGet]
        [ResponseType(typeof(Genero))]
        public IHttpActionResult GetGenero(string nome)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Genero genero = context.Genero.FirstOrDefault(x => x.Nome.Equals(nome));
                if (genero == null)
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.Conflict,
                            "Não existe um Genero cadastrado com este nome."));
                else
                    return Ok(genero);
            }
        }

        [HttpPost]
        public IHttpActionResult Incluir([FromBody]Genero genero)
        {
            var NewGenero = new Genero
            {
                GeneroID = genero.GeneroID,
                Nome = genero.Nome,
                DataCriacao = genero.DataCriacao,
                Ativo = genero.Ativo
            };

            using (LocadoraContex context = new LocadoraContex())
            {
                Genero generoDB = context.Genero.FirstOrDefault(u => u.Nome.Equals(genero.Nome));

                if (generoDB != null)
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.Conflict,
                            "Já existe um Genero cadastrado com este nome."));
                else
                {
                    context.Genero.Add(NewGenero);
                    context.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
            }
        }

        [HttpPut]
        public IHttpActionResult Editar([FromBody]Genero genero)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Genero GeneroDB = context.Genero.FirstOrDefault(u => u.GeneroID == genero.GeneroID);
                if (GeneroDB != null)
                {
                    GeneroDB.Nome = genero.Nome;
                    GeneroDB.DataCriacao = genero.DataCriacao;
                    GeneroDB.GeneroID = genero.GeneroID;
                    GeneroDB.Ativo = genero.Ativo;
                    context.SaveChanges();
                    return Ok(GeneroDB);
                }
                else
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Genero não localizado para alteração."));
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Genero GeneroDB = context.Genero.FirstOrDefault(u => u.GeneroID == id);

                if (GeneroDB != null)
                {
                    context.Genero.Remove(GeneroDB);
                    context.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
                else
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Genero não localizado para exclusão."));
            }
        }
    }
}
