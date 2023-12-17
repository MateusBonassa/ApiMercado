using System.ComponentModel;
using TrabalhoECommerceAPI.Models.Produto;

namespace TrabalhoECommerceAPI.Repository
{
    public class PromocaoRepository
    {
        WrapperMySQL _mysql = new ();

        public bool ValidarProduto(int id)
        {
            bool sucesso = true;
            try
            {
                _mysql.Abrir();
                 _mysql.Comando.CommandText = $@"select count(*) from Produto where ProdutoId = {id}";
                 Int64 existe = (Int64)_mysql.Comando.ExecuteScalar();
                 sucesso = existe>0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { _mysql.Fechar(); }

            return sucesso;
        }

        public bool Salvar(Promocao promocao)
        {
            bool sucesso = false;
            
            try
            {
                if(promocao.Id == 0)
                {
                    _mysql.Comando.CommandText = $@"insert into Promocao(Preco,ProdutoId) values (@Preco , @ProdutoId)";
                    _mysql.Comando.Parameters.AddWithValue("@ProdutoId", promocao.Produto.Id);
                }
                else
                {
                    _mysql.Comando.CommandText = $@"update Promocao set Preco = @Preco  where PromocaoId = @PromocaoId";
                    _mysql.Comando.Parameters.AddWithValue("@PromocaoId", promocao.Id);
                }
                
                _mysql.Comando.Parameters.AddWithValue("@Preco", promocao.Preco);
                _mysql.Abrir();
                int linhasafetadas = _mysql.Comando.ExecuteNonQuery();
                sucesso = linhasafetadas > 0;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { _mysql.Fechar(); }
            return sucesso;
        }

        public Promocao Obter(int id)
        {
            Promocao p  = null;
            try
            {
                _mysql.Comando.CommandText = $@"select r.*,p.*,u.Nome as UnidadeNome, c.Nome as CategoriaNome , c.CategoriaId from Promocao r inner join Produto p on p.ProdutoId = r.ProdutoId inner join Unidade u on u.UnidadeId = p.UnidadeId 
inner join CategoriaProduto cp on cp.ProdutoId = p.ProdutoId inner join Categoria c on c.CategoriaId = cp.CategoriaId
 where PromocaoId = {id};";
                _mysql.Abrir();
                var result = _mysql.Comando.ExecuteReader();
                if(result.Read())
                {
                    p = new Promocao()
                    {
                        Id =(int) result["PromocaoId"],
                        Preco = (decimal)result["Preco"],
                        Produto = new()
                        {
                            Id = (int)result["ProdutoId"],
                            Nome = result["Nome"].ToString(),
                            PrecoVenda = (Decimal)result["PrecoVenda"],
                            Estoque = (int)result["Estoque"],
                            Ativo = (bool)result["Ativo"],
                            Unidade = new()
                            {
                                Nome = result["UnidadeNome"].ToString(),
                                Id = (int)result["UnidadeId"]
                            },
                            Categorias = new()
                        }
                    };
                    p.Produto.Categorias.Add(new()
                    {
                        Nome = result["CategoriaNome"].ToString(),
                        Id = (int)result["CategoriaId"]
                    });
                    while (result.Read())
                    {
                        p.Produto.Categorias.Add(new()
                        {
                            Nome = result["CategoriaNome"].ToString(),
                            Id = (int)result["CategoriaId"]
                        });
                    }
                }
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            finally { _mysql.Fechar(); }
            return p;
        }
        public bool Excluir(int id)
        {
            bool sucesso = false;
            try
            {
                _mysql.Comando.CommandText = $@"delete from Promocao where PromocaoId = {id}";
                _mysql.Abrir();
                int linhasafetadas = _mysql.Comando.ExecuteNonQuery();
                sucesso = linhasafetadas > 0;
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            finally { _mysql.Fechar(); }
            return sucesso;
        }

      
    }
}
