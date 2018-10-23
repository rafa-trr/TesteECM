using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using ECMAvaliacaoMVC.Models.Entidades;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ECMAvaliacaoMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string ApiBaseUrl = "http://localhost:51181/";
            string MetodoPath = "api/produtos/";
            List<Produto> resultado = new List<Produto>();
            List<Produto> model = new List<Produto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage resposta = await client.GetAsync(MetodoPath);

                if (resposta.IsSuccessStatusCode)
                {
                    var respostaConteudo = resposta.Content.ReadAsStringAsync().Result;

                    resultado = JsonConvert.DeserializeObject<List<Produto>>(respostaConteudo);

                    var query = resultado.GroupBy(e => e.Marca.Codigo)
                        .Select(e => new { e.Key, preco = e.Min(g => g.Preco) })
                        .OrderBy(p => p.preco)
                        .Take(10);

                    foreach (var item in query)
                    {
                        Produto prod = resultado.First(p => p.Preco == item.preco && p.Marca.Codigo == item.Key);
                        model.Add(prod);
                    }
                }
            }

            return View(model);            
        }
    }
}