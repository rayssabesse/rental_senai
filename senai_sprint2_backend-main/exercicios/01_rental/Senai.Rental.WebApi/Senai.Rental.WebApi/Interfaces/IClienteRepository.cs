using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório ClienteRepository
    /// </summary>
    interface IClienteRepository
    {
        /// <summary>
        /// Retorna todos os clientes
        /// </summary>
        /// <returns>Uma lista de clientes</returns>
        List<ClienteDomain> ListarTodos();

        /// <summary>
        /// Busca um cliente através do seu id
        /// </summary>
        /// <param name="idCliente">id do cliente que será buscado</param>
        /// <returns>Um objeto do tipo ClienteDomain que foi buscado</returns>
        ClienteDomain BuscarPorId(int idCliente);

        /// <summary>
        /// Deleta um cliente
        /// </summary>
        /// <param name="idCliente">id do cliente que será deletado</param>
        void Deletar(int idCliente);

        /// <summary>
        /// Atualiza um cliente existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="clienteAtualizado">Objeto clienteAtualizado com os novos dados</param>
        /// ex: http://localhost:5000/api/clientes
        void AtualizarIdCorpo(ClienteDomain clienteAtualizado);

        /// <summary>
        /// Cadastra um novo cliente
        /// </summary>
        /// <param name="novoCliente>Objeto novoCliente com os dados que serão cadastrados</param>
        void Cadastrar(ClienteDomain novoCliente);
    }
}
