using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros.
        /// Data Source = Nome do Servidor
        /// inital catalog = Nome do banco de dados
        /// user id= sa; pwd= Chiba163 = Faz a autenticação com o SQL SERVER, passando Login e Senha.
        /// integrated security = Faz a autenticação com o usuario do sistema (Windows).
        /// </summary>
        private string stringConexao = "DATA SOURCE = DESKTOP-OAGJCNA\\SQLEXPRESS; initial catalog = T_Rental; user Id = SA; pwd = SENAI@132";
 
        public void AtualizarIdCorpo(AluguelDomain aluguelAtualizado)
        {
            if (aluguelAtualizado.idAluguel != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE ALUGUEL SET idCliente = @novoIdCliente, dataRetirada = @novoDataRet, dataDevolucao = @novoDataDev WHERE idAluguel = @idAluAtualizado";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@novoIdCliente", aluguelAtualizado.idCliente);
                        cmd.Parameters.AddWithValue("@novoDataRet", aluguelAtualizado.dataRetirada);
                        cmd.Parameters.AddWithValue("@novoDataDev", aluguelAtualizado.dataDevolucao);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public AluguelDomain BuscarPorId(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT * FROM ALUGUEL WHERE idAluguel = @idAluguel";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        AluguelDomain aluguelBuscado = new AluguelDomain
                        {
                            idAluguel = Convert.ToInt32(rdr[0]),
                            idCliente = Convert.ToInt32(rdr[1]),
                            dataRetirada = Convert.ToDateTime(rdr[2]),
                            dataDevolucao = Convert.ToDateTime(rdr[3]),
                        };
                        return aluguelBuscado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(AluguelDomain novoAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO ALUGUEL (idCliente, dataRetirada, dataDevolucao) VALUES (@idCliente, @dataRetirada, @dataDevolucao)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", novoAluguel.idCliente);
                    cmd.Parameters.AddWithValue("@dataRetirada", novoAluguel.dataRetirada);
                    cmd.Parameters.AddWithValue("@dataDevolucao", novoAluguel.dataDevolucao);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM ALUGUEL WHERE idAluguel = @idAluguel";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AluguelDomain> ListarTodos()
        {
            List<AluguelDomain> listaAluguel = new List<AluguelDomain>();
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectALL = "SELECT * FROM ALUGUEL;";
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(querySelectALL, con))
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        AluguelDomain aluguel = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(rdr[0]),
                            idCliente = Convert.ToInt32(rdr[1]),
                            dataRetirada = Convert.ToDateTime(rdr[2]),
                            dataDevolucao = Convert.ToDateTime(rdr[3]),
                        };
                        listaAluguel.Add(aluguel);
                    }
                }
            }
            return listaAluguel;
        }
    }
}
