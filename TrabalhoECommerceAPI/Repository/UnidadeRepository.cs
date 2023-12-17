using Microsoft.AspNetCore.Authentication;
using MySql.Data.MySqlClient;
namespace TrabalhoECommerceAPI.Repository
{
    public class UnidadeRepository
    {
        WrapperMySQL _mysql = new WrapperMySQL();

        public bool Salvar(Models.Produto.Unidade unidade)
        {
            bool sucesso = false;
            
            try
            {
            
                if (unidade.Id == 0)
                {
                    _mysql.Comando.CommandText = $@"insert into 
                                                     Unidade (Nome) 
                                                     values (""{unidade.Nome}"")";
                    
                }
                else
                {
                    _mysql.Comando.CommandText = @$"update Unidade 
                                                     set Nome = @Nome
                                                     where UnidadeId = {unidade.Id}";

                   
                }
               

           
                _mysql.Abrir();
              
                int linhasAfetadas = _mysql.Comando.ExecuteNonQuery();

                sucesso = linhasAfetadas > 0;

                if (sucesso)
                {
                    if (unidade.Id == 0)
                    {
                        unidade.Id = (int)_mysql.Comando.LastInsertedId;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                _mysql.Fechar();
            }

            return sucesso;

        }

        public bool Excluir(int id)
        {
            bool sucesso = false;
 
            try
            {


                _mysql.Comando.CommandText = @$"delete from Unidade 
                                         where UnidadeId = {id}";

                _mysql.Abrir();
                int linhasAfetadas = _mysql.Comando.ExecuteNonQuery();

                sucesso = linhasAfetadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _mysql.Fechar();
            }

            return sucesso;

        }

        public Models.Produto.Unidade Obter(int id)
        {
            Models.Produto.Unidade unidade = null;
     

            try
            {


                _mysql.Comando.CommandText = $@"select * 
                                     from Unidade
                                     where UnidadeId = {id}";

                _mysql.Abrir();
                var dr = _mysql.Comando.ExecuteReader();

                if(dr.Read())
                {
                    unidade = new Models.Produto.Unidade();
                    unidade.Id = Convert.ToInt32(dr["UnidadeId"]);
                    unidade.Nome = dr["Nome"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _mysql.Fechar();
            }

            return unidade;

        }

        public Models.Produto.Unidade ObterPorNome(string nome)
        {
            Models.Produto.Unidade unidade = null;
  

            try
            {


                _mysql.Comando.CommandText = $@"select * 
                                     from Unidade
                                     where Nome = @Nome";

                _mysql.Abrir();
                _mysql.Comando.Parameters.AddWithValue("@Nome", nome);
                var dr = _mysql.Comando.ExecuteReader();

                if (dr.Read())
                {
                    unidade = new Models.Produto.Unidade();
                    unidade.Id = Convert.ToInt32(dr["UnidadeId"]);
                    unidade.Nome = dr["Nome"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _mysql.Fechar();
            }

            return unidade;

        }

        public List<Models.Produto.Unidade> Consultar(string nome)
        {
            List<Models.Produto.Unidade> unidades = new List<Models.Produto.Unidade>(); 

            try
            {
                _mysql.Comando.CommandText = $@"select * 
                                     from Unidade
                                     where Nome like @Nome";

                _mysql.Comando.Parameters.AddWithValue("@Nome", nome + "%");

                _mysql.Abrir();
                var dr = _mysql.Comando.ExecuteReader();

                while (dr.Read())
                {
                    Models.Produto.Unidade u = new Models.Produto.Unidade();
                    u.Id = Convert.ToInt32(dr["UnidadeId"]);
                    u.Nome = dr["Nome"].ToString();

                    unidades.Add(u);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _mysql.Fechar();
            }

            return unidades;

        }


    }
}
