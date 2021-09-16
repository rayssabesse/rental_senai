using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório AluguelRepository
    /// </summary>
    interface IAluguelRepository
    {
        /// <summary>
        /// Retorna todos os aluguéis
        /// </summary>
        /// <returns>Uma lista de aluguéis</returns>
        List<AluguelDomain> ListarTodos();

        /// <summary>
        /// Busca um aluguéç através do seu id
        /// </summary>
        /// <param name="idAluguel">id do aluguel que será buscado</param>
        /// <returns>Um objeto do tipo AluguelDomain que foi buscado</returns>
        AluguelDomain BuscarPorId(int idAluguel);

        /// <summary>
        /// Deleta um aluguel
        /// </summary>
        /// <param name="idAluguel">id do aluguel que será deletado</param>
        void Deletar(int idAluguel);

        /// <summary>
        /// Atualiza um aluguel existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="aluguelAtualizado">Objeto aluguelAtualizado com os novos dados</param>
        /// ex: http://localhost:5000/api/alugueis
        void AtualizarIdCorpo(AluguelDomain aluguelAtualizado);

        /// <summary>
        /// Cadastra um novo aluguel
        /// </summary>
        /// <param name="novoAluguel">Objeto novoAluguel com os dados que serão cadastrados</param>
        void Cadastrar(AluguelDomain novoAluguel);
    }
}

