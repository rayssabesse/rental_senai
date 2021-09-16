using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros.
        /// Data Source = Nome do Servidor
        /// inital catalog = Nome do banco de dados
        /// user id= sa; pwd= Chiba163 = Faz a autenticação com o SQL SERVER, passando Login e Senha.
        /// integrated security = Faz a autenticação com o usuario do sistema (Windows).
        /// </summary>
        private string stringConexao = "DATA SOURCE = DESKTOP-OAGJCNA\\SQLEXPRESS; initial catalog = T_Rental; user Id = SA; pwd = SENAI@132";
        public void AtualizarIdCorpo(VeiculoDomain veiculoAtualizado)
        {
            if (veiculoAtualizado.placaVeiculo != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE VEICULO SET idEmpresa = @novoIdEmpresa, idModelo = @novoIdModelo, idAluguel = @novoIdAluguel, placaVeiculo = @novaPlacaVeiculo WHERE idVeiculo = @idVeiAtualizado";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@novoIdEmpresa", veiculoAtualizado.idEmpresa);
                        cmd.Parameters.AddWithValue("@novoIdModelo", veiculoAtualizado.idModelo);
                        cmd.Parameters.AddWithValue("@novoIdAluguel", veiculoAtualizado.idAluguel);
                        cmd.Parameters.AddWithValue("@novaPlacaVeiculo", veiculoAtualizado.placaVeiculo);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

    public VeiculoDomain BuscarPorId(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT * FROM VEICULO WHERE idVeiculo = @idVeiculo";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        VeiculoDomain veiculoBuscado = new VeiculoDomain
                        {
                            idVeiculo = Convert.ToInt32(rdr[0]),
                            idEmpresa = Convert.ToInt32(rdr[1]),
                            idModelo = Convert.ToInt32(rdr[2]),
                            idAluguel = Convert.ToInt32(rdr[3]),
                            placaVeiculo = rdr[4].ToString(),
                        };
                        return veiculoBuscado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO VEICULO (idEmpresa, idModelo, idAluguel, placaVeiculo) VALUES (@idEmpresa, @idModelo, @idAluguel, @placaVeiculo)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idEmpresa", novoVeiculo.idEmpresa);
                    cmd.Parameters.AddWithValue("@idModelo", novoVeiculo.idModelo);
                    cmd.Parameters.AddWithValue("@idAluguel", novoVeiculo.idAluguel);
                    cmd.Parameters.AddWithValue("@placaVeiculo", novoVeiculo.placaVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM VEICULO WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> ListarTodos()
        {
            List<VeiculoDomain> listaVeiculo = new List<VeiculoDomain>();
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT * FROM VEICULO;";
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(rdr[0]),
                            idEmpresa = Convert.ToInt32(rdr[1]),
                            idModelo = Convert.ToInt32(rdr[2]),
                            idAluguel = Convert.ToInt32(rdr[3]),
                            placaVeiculo = rdr[4].ToString(),
                        };
                        listaVeiculo.Add(veiculo);
                    }
                }
            }
            return listaVeiculo;
        }
    }
}
