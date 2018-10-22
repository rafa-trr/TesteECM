using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
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

                    model = JsonConvert.DeserializeObject<List<Produto>>(respostaConteudo);
                }
            }

            return View(model);            
        }
    }
}