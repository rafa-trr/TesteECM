﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ECMWebApi.Models.Entidades;
using ECMWebApi.Models.Repositorio;

namespace ECMWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:53894/", methods: "GET", headers: "*")]
    public class ProdutosController : ApiController
    {
        private ProdutosRepositorio _repositorio { get; set; }

        public ProdutosController()
        {
            if (_repositorio == null)
            {
                _repositorio = new ProdutosRepositorio();
            }
        }

        // GET api/produtos/xxx
        [Route("api/produtos/{term}")]
        public HttpResponseMessage Get(string term)
        {
            IEnumerable<Produto> produtos = new List<Produto>();
            try
            {
                produtos = _repositorio.GetByTerm(term);
                return Request.CreateResponse(HttpStatusCode.OK, produtos);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET api/produtos/
        [Route("api/produtos")]
        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Produto> produtos = _repositorio.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, produtos);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }
    }
}
