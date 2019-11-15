using LocadoraFilmes.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using System.Data;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Configuration;
using LocadoraFilmes.Dao;
using System.Net.Http;
using System.Net;
using System.Linq;

namespace WebApiLocadora.Controllers
{
    public class LocacaoController : ApiController
    {
        [HttpGet] 
        public IEnumerable<Locacao> GetLocacoes()
        {
            using (SqlConnection conexao = new SqlConnection(
                ConfigurationManager.ConnectionStrings["LocadoraFilmes"].ConnectionString))
            {
                return conexao.Query<Locacao>(
                    "SELECT                                 " +
                    "   L.*,                                " +
                    "   G.NOME AS NOME_GENERO,              " +
                    "   F.FILMEID,                          " +
                    "   F.NOME AS NOME_FILME                " +
                    "FROM                                   " +
                    "    LOCACAO L                          " +
                    "INNER JOIN FILME F ON                  " +
                    "    F.LOCACAO_LOCACAOID = L.LOCACAOID  " +
                    "LEFT JOIN GENERO G ON                  " +
                    "    G.GENEROID = F.GENEROID            " +
                    "ORDER BY                               " +
                    "    L.LOCACAOID                        " );
            }
        }

        [HttpGet]
        public IEnumerable<Filme> GetLocacoesPorID(int id)
        {
            using (SqlConnection conexao = new SqlConnection(
                ConfigurationManager.ConnectionStrings["LocadoraFilmes"].ConnectionString))
            {
                return conexao.Query<Filme>(
                    "SELECT                                 " +
                    "   *                                   " +
                    "FROM                                   " +
                    "    FILME L                            " +
                    "WHERE                                  " +
                    "    L.Locacao_LocacaoID = @LocacaoID   ",
                    new { LocacaoID = id });
            }
        }

        [HttpPost]
        public IHttpActionResult Incluir([FromBody]Locacao locacao)
        {
            var NewLocacao = new Locacao
            {
                LocacaoID = locacao.LocacaoID,
                CPF = locacao.CPF,
                DataCriacao = locacao.DataCriacao
            };

            using (LocadoraContex context = new LocadoraContex())
            {
                Locacao locacaoDB = context.Locacao.FirstOrDefault(u => u.LocacaoID == locacao.LocacaoID);

                if (locacaoDB != null)
                    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.Conflict,
                            "Já existe uma locação cadastrada com este id."));
                else
                {
                    context.Locacao.Add(NewLocacao);
                    context.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
            }
        }
    }
}
