using MySql.Data.MySqlClient;
using TrabalhoECommerceAPI.Models.Produto;

namespace TrabalhoECommerceAPI.Repository
{
    public class CategoriaRepository
    {
        WrapperMySQL _mysql = new();

        public Categoria Obter(int id)
        {
            Categoria categoria = null;

            try {
                _mysql.Comando.CommandText = $@"select * from Categoria where CategoriaId = {id}";
               
                _mysql.Abrir();
                var result = _mysql.Comando.ExecuteReader();
                if(result.Read())
                {
                    categoria = new()
                    {
                        Id = Convert.ToInt32(result["CategoriaId"]),
                        Nome = result["Nome"].ToString()
                    };
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { _mysql.Fechar(); }

            return categoria;
        }

        public bool Salvar(Categoria categoria)
        {
            bool sucesso = false;
            try
            {
                _mysql.Abrir();
                if (categoria.Id == 0)
                    _mysql.Comando.CommandText = $@"insert into Categoria (Nome) values(@Nome)";
                else
                {
                    _mysql.Comando.CommandText = $@"update Categoria set Nome = @Nome where CategoriaId = @CategoriaId";
                    _mysql.Comando.Parameters.AddWithValue("@CategoriaId", categoria.Id);
                }
                _mysql.Comando.Parameters.AddWithValue("@Nome", categoria.Nome);
                _mysql.Abrir();
                int linhasafetadas = _mysql.Comando.ExecuteNonQuery();
                sucesso = linhasafetadas > 0;
                if (sucesso)
                    if (categoria.Id == 0)
                        categoria.Id = (int)_mysql.Comando.LastInsertedId;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { _mysql.Fechar(); }

            return sucesso;
        }

        public Categoria ObterPorNome(String nome)
        {
            Categoria categoria = null;
            try
            {
                _mysql.Comando.CommandText = $@"select * from Categoria where Nome = ""{nome}""";
                _mysql.Abrir();
                var result = _mysql.Comando.ExecuteReader();
                if(result.Read())
                {
                    categoria = new Categoria()
                    {
                        Id = Convert.ToInt32(result["CategoriaId"]),
                        Nome = result["Nome"].ToString()

                    };
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { _mysql.Fechar(); }

            return categoria;
        }

        public bool Excluir(int id)
        {
            bool sucesso = false;
            try
            {
                _mysql.Comando.CommandText = $@"delete from Categoria where CategoriaId = {id}";
                _mysql.Abrir();
                int linhasafetadas = _mysql.Comando.ExecuteNonQuery() ;
                sucesso = linhasafetadas > 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _mysql.Fechar();
            }
            return sucesso;
        }

        public List<Categoria> Consultar(String nome)
        {
            List<Categoria> categorias = new();
            try
            {
                _mysql.Comando.CommandText = $@"select * from Categoria where Nome like @Nome";
                _mysql.Comando.Parameters.AddWithValue("@Nome", nome +"%");
                _mysql.Abrir();
                var result = _mysql.Comando.ExecuteReader();
                while(result.Read())
                {
                    categorias.Add(new Categoria()
                    {
                        Id = Convert.ToInt32(result["CategoriaId"]),
                        Nome = result["Nome"].ToString()

                    });
                }
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            finally { _mysql.Fechar(); }

            return categorias;
        }
    }
}
