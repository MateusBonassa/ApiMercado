using TrabalhoECommerceAPI.Models.Produto;
using TrabalhoECommerceAPI.Repository;

namespace TrabalhoECommerceAPI.Services
{
    public class ProdutoService
    {
        ProdutoRepository _produtoRepository = new();

        public (bool,ValidationResult) Salvar(Produto produto)
        {
            ValidationResult vr = new(); bool sucesso = false; bool continuar = true;
            if(produto.Nome==null)
            {
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Campo nome está nulo!";
                continuar = false;
            }
            else if (produto.Nome.Length>45)
            {
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Nome excede tamanho máximo de 45!";
                continuar = false;
            }
            else if (produto.PrecoVenda<=0)
            {
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Preco invalido!";
                continuar = false;
            }
            else if (produto.Estoque<0)
            {
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Quantidade de estoque invalida!";
                continuar = false;
            }
            else
            {
                continuar = _produtoRepository.ValidarCategorias(produto.Categorias);
               
                if (continuar)
                {
                    Produto p = _produtoRepository.ObterPorNome(produto.Nome);
                    if (p != null && produto.Id == 0)
                    {
                        continuar = false;
                        vr.Tipo = ValidationResult.TpMensagem.Erro;
                        vr.Mensagem = "Produto já existente!";
                    }
                }else
                {
                    vr.Tipo = ValidationResult.TpMensagem.Erro;
                    vr.Mensagem = "Categorias Inexistentes!";
                }

            }
            if(continuar)
            {
                sucesso = _produtoRepository.Salvar(produto);
                if(!sucesso)
                {
                    vr.Tipo = ValidationResult.TpMensagem.Erro;
                    vr.Mensagem = "Erro desconhecido. Tente Novamente!";
                }
            }

            return (sucesso, vr);
        }

        public Produto Obter(int id)
        {
            return _produtoRepository.ObterPorId(id);
        }
        public List<Produto> Consultar(String nome)
        {
            return _produtoRepository.Consultar(nome);
        }
     
        public (bool,ValidationResult) Excluir(int id)
        {
            if (_produtoRepository.Excluir(id))
                return (true, new ValidationResult(ValidationResult.TpMensagem.Sucesso, "Exclusão feita com sucesso!"));
            else
                return (false, new ValidationResult(ValidationResult.TpMensagem.CampoInvalido, "Não foi possivel efetuar a exclusão"));
        }
    }
}
