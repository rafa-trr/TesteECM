using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECMWebApi.Models.Entidades
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public Marca Marca { get; set; }
    }
}