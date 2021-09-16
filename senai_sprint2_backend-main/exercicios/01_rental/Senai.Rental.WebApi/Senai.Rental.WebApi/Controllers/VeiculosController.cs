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
    public class VeiculosController : ControllerBase
    {

        private IVeiculoRepository _VeiculoRepository { get; set; }

        public VeiculosController()
        {
            _VeiculoRepository = new VeiculoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<VeiculoDomain> listaVeiculos = _VeiculoRepository.ListarTodos();
            return Ok(listaVeiculos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            VeiculoDomain veiculoBuscado = _VeiculoRepository.BuscarPorId(id);
            if (veiculoBuscado == null)
            {
                return NotFound("Nenhum veiculo encontrado!");
            }
            return Ok(veiculoBuscado);
        }

        [HttpPost]
        public IActionResult Post(VeiculoDomain novoVeiculo)
        {
            _VeiculoRepository.Cadastrar(novoVeiculo);
            return StatusCode(201);
        }


        [HttpPut]
        public IActionResult PutBody(VeiculoDomain veiculoAtualizado)
        {
            if (veiculoAtualizado.placaVeiculo == null || veiculoAtualizado.idVeiculo == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Placa do veiculo ou id do veiculo não foi informado!"
                    }
                );
            }
            VeiculoDomain veiculoBuscado = _VeiculoRepository.BuscarPorId(veiculoAtualizado.idVeiculo);
            if (veiculoBuscado != null)
            {
                try
                {
                    _VeiculoRepository.AtualizarIdCorpo(veiculoAtualizado);
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
                        mensagemErro = "Veiculo não encontrado!",
                        codErro = true
                    }
                );
        }

        /// ex: http://localhost:5000/api/veiculos/excluir/7
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _VeiculoRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
