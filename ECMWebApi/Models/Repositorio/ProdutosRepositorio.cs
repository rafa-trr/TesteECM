using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECMWebApi.Models.Entidades;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace ECMWebApi.Models.Repositorio
{
    public class ProdutosRepositorio
    {
        private const string ENDERECO_URL_JSON_FILE = "http://www.json-generator.com/api/json/get/cehnqHymle?indent=0";
        private string jsonConteudo { get; set; }

        public ProdutosRepositorio()
        {
            jsonConteudo = new WebClient().DownloadString(ENDERECO_URL_JSON_FILE);
        }

        public IEnumerable<Produto> GetByTerm(string term)
        {
            List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(jsonConteudo);

            var resultado = produtos.Where(p => p.Nome.ToLower().Contains(term.ToLower()))
                .Select(p => p);

            return resultado;
        }
    }
}