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
    public class ClientesController : ControllerBase
    {
        private IClienteRepository _ClienteRepository { get; set; }

        public ClientesController()
        {
            _ClienteRepository = new ClienteRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ClienteDomain> listaClientes = _ClienteRepository.ListarTodos();
            return Ok(listaClientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ClienteDomain clienteBuscado = _ClienteRepository.BuscarPorId(id);
            if (clienteBuscado == null)
            {
                return NotFound("Nenhum cliente encontrado!");
            }
            return Ok(clienteBuscado);
        }

        [HttpPost]
        public IActionResult Post(ClienteDomain novoCliente)
        {
            _ClienteRepository.Cadastrar(novoCliente);
            return StatusCode(201);
        }


        [HttpPut]
        public IActionResult PutBody(ClienteDomain clienteAtualizado)
        {
            if (clienteAtualizado.nomeCliente == null || clienteAtualizado.idCliente == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome do cliente ou id do cliente não foi informado!"
                    }
                );
            }
            ClienteDomain clienteBuscado = _ClienteRepository.BuscarPorId(clienteAtualizado.idCliente);
            if (clienteBuscado != null)
            {
                try
                {
                    _ClienteRepository.AtualizarIdCorpo(clienteAtualizado);
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
                        mensagemErro = "Cliente não encontrado!",
                        codErro = true
                    }
                );
        }

        /// ex: http://localhost:5000/api/clientes/excluir/7
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _ClienteRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}