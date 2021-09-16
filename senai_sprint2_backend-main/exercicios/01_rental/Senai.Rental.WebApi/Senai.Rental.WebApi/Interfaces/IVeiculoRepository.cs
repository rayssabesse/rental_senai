using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório VeiculoRepository
    /// </summary>
    interface IVeiculoRepository
    {
        /// <summary>
        /// Retorna todos os veiculos
        /// </summary>
        /// <returns>Uma lista de veiculos</returns>
        List<VeiculoDomain> ListarTodos();

        /// <summary>
        /// Busca um veiculo através do seu id
        /// </summary>
        /// <param name="idVeiculo">id do veiculo que será buscado</param>
        /// <returns>Um objeto do tipo VeiculoDomain que foi buscado</returns>
        VeiculoDomain BuscarPorId(int idVeiculo);

        /// <summary>
        /// Deleta um veiculo
        /// </summary>
        /// <param name="idVeiculo">id do veiculo que será deletado</param>
        void Deletar(int idVeiculo);

        /// <summary>
        /// Atualiza um veiculo existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="veiculoAtualizado">Objeto veiculoAtualizado com os novos dados</param>
        /// ex: http://localhost:5000/api/veiculos
        void AtualizarIdCorpo(VeiculoDomain veiculoAtualizado);

        /// <summary>
        /// Cadastra um novo veiculo
        /// </summary>
        /// <param name="novoVeiculo">Objeto novoVeiculo com os dados que serão cadastrados</param>
        void Cadastrar(VeiculoDomain novoVeiculo);
    }
}
