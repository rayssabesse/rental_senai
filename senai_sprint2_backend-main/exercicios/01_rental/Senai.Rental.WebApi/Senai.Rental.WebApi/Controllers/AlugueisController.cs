using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Controllers
{

    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class AlugueisController : ControllerBase
    {
        private IAluguelRepository _AluguelRepository { get; set; }

        public AlugueisController()
        {
            _AluguelRepository = new AluguelRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<AluguelDomain> listaAlugueis = _AluguelRepository.ListarTodos();
            return Ok(listaAlugueis);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AluguelDomain aluguelBuscado = _AluguelRepository.BuscarPorId(id);
            if (aluguelBuscado == null)
            {
                return NotFound("Nenhum aluguel encontrado!");
            }
            return Ok(aluguelBuscado);
        }

        [HttpPost]
        public IActionResult Post(AluguelDomain novoAluguel)
        {
            _AluguelRepository.Cadastrar(novoAluguel);
            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult PutBody(AluguelDomain aluguelAtualizado)
        {
            if (aluguelAtualizado.idCliente == null || aluguelAtualizado.idAluguel == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "ID do cliente ou id do aluguel não foi informado!"
                    }
                );
            }
            AluguelDomain aluguelBuscado = _AluguelRepository.BuscarPorId(aluguelAtualizado.idAluguel);
            if (aluguelBuscado != null)
            {
                try
                {
                    _AluguelRepository.AtualizarIdCorpo(aluguelAtualizado);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            return NotFound(
                    new
                    {
                        mensagemErro = "Aluguel não encontrado!",
                        codErro = true
                    }
                );
        }
        /// ex: http://localhost:5000/api/alugueis/excluir/7
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _AluguelRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
