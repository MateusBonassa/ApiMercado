using TrabalhoECommerceAPI.Models.Produto;
using TrabalhoECommerceAPI.Repository;

namespace TrabalhoECommerceAPI.Services
{
    public class PromocaoService
    {
        PromocaoRepository _promocaorepository = new();

        public (bool,ValidationResult) Salvar(Promocao promocao)
        {
            bool sucesso = false, continuar = true;
            ValidationResult vr = new();
            if(promocao.Preco <=0 )
            {
                continuar = false;
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Preco invalido!";
            }
            else
            {
                continuar = _promocaorepository.ValidarProduto(promocao.Produto.Id);
                if (continuar)
                {
                    sucesso = _promocaorepository.Salvar(promocao);
                    if (!sucesso)
                    {
                        vr.Tipo = ValidationResult.TpMensagem.Erro;
                        vr.Mensagem = "Erro desconhecido. Tente Novamente!";
                    }
                }
                else
                {
                    vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                    vr.Mensagem = "Produto inexistente!";
                }
            }
          
            return (sucesso, vr);
        }

        public (bool ,ValidationResult) Excluir(int id)
        {
            bool sucesso = false;
            sucesso = _promocaorepository.Excluir(id);
            if (sucesso)
                return (true, new ValidationResult(ValidationResult.TpMensagem.Sucesso, "Exclusão feita com sucesso!"));
            else
                return (false, new ValidationResult(ValidationResult.TpMensagem.CampoInvalido, "Não foi possivel efetuar a exclusão"));
        }

        public Promocao Obter(int id)
        {
            return _promocaorepository.Obter(id);
        }
    }
}
