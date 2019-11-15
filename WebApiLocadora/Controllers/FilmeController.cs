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
    public class FilmeController : ApiController
    {
        [HttpGet]
        public IEnumerable<Filme> GetFilmes()
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                return context.Filme.ToList();
            }
        }

        [HttpGet]
        [ResponseType(typeof(Filme))]
        public IHttpActionResult GetFilme(string nome)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Filme filme = context.Filme.FirstOrDefault(x => x.Nome.Equals(nome));
                if (filme == null)
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.Conflict,
                            "Não existe um filme cadastrado com este nome."));
                else
                    return Ok(filme);
            }
        }

        [HttpPost]
        public IHttpActionResult Incluir([FromBody]Filme filme)
        {
            var NewFilme = new Filme
            {
                FilmeID = filme.FilmeID,
                Nome = filme.Nome,
                DataCriacao = filme.DataCriacao,
                GeneroID = filme.GeneroID
            };

            using (LocadoraContex context = new LocadoraContex())
            {
                Filme Filme = context.Filme.FirstOrDefault(u => u.Nome.Equals(filme.Nome));

                if (Filme != null)
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.Conflict,
                            "Já existe um filme cadastrado com este nome."));
                else
                {
                    context.Filme.Add(NewFilme);
                    context.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
            }
        }

        [HttpPut]
        public IHttpActionResult Editar([FromBody]Filme filme)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Filme Filme = context.Filme.FirstOrDefault(u => u.FilmeID == filme.FilmeID);
                if (Filme != null)
                {
                    Filme.Nome = filme.Nome;
                    Filme.DataCriacao = filme.DataCriacao;
                    Filme.GeneroID = filme.GeneroID;
                    Filme.Ativo = filme.Ativo;
                    context.SaveChanges();
                    return Ok(filme);
                }
                else
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Filme não localizado para alteração."));
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (LocadoraContex context = new LocadoraContex())
            {
                Filme Filme = context.Filme.FirstOrDefault(u => u.FilmeID == id);

                if (Filme != null)
                {
                    context.Filme.Remove(Filme);
                    context.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
                else
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Filme não localizado para exclusão."));
            }
        }
    }
}

