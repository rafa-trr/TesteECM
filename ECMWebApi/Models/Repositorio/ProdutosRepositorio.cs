﻿using System.Collections.Generic;
using System.Linq;
using ECMWebApi.Models.Entidades;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

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

        public Task<IEnumerable<Produto>> GetByTermAsync(string term)
        {
            return Task.Run(() =>
            {
                List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(jsonConteudo);

                var resultado = produtos.Where(p => p.Nome.ToLower().Contains(term.ToLower()))
                    .Select(p => p);

                return resultado;
            });
        }

        public Task<IEnumerable<Produto>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                IEnumerable<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(jsonConteudo);

                return produtos;
            });

        }
    }
}