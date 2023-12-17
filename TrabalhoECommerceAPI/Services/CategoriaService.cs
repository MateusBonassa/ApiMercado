using TrabalhoECommerceAPI.Models.Produto;
using TrabalhoECommerceAPI.Repository;

namespace TrabalhoECommerceAPI.Services
{
    public class CategoriaService
    {
        CategoriaRepository _categoriaRepository = new();

        public (bool, ValidationResult) Salvar(Categoria categoria)
        {
            ValidationResult vr = new ValidationResult();
            bool sucesso = false;
            bool continuar = true;
            if(categoria.Nome== null)
            {
                continuar = false;
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Nome não fornecido";
            }
            else if (categoria.Nome.Length > 45)
            {
                continuar = false;
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Nome excede tamanho permitido";
            }
            else
            {
                var catexiste = _categoriaRepository.ObterPorNome(categoria.Nome);
                if(catexiste!=null && categoria.Id==0)
                {
                    continuar = false;
                    vr.Tipo = ValidationResult.TpMensagem.Erro;
                    vr.Mensagem = "Categoria já existente";
                }
            }
            if(continuar)
            {
                sucesso = _categoriaRepository.Salvar(categoria);
                if(!sucesso)
                {
                    vr.Tipo = ValidationResult.TpMensagem.Erro;
                    vr.Mensagem = "Erro desconhecido. Tente novamente!";
                }
            }
            return (sucesso, vr);
        }

        public Categoria Obter(int id)
        {
            return _categoriaRepository.Obter(id);
        }
        
        public (bool,ValidationResult) Excluir(int id)
        {
            bool sucesso = _categoriaRepository.Excluir(id);
            if (sucesso)
                return (sucesso, new ValidationResult(ValidationResult.TpMensagem.Sucesso, "Categoria excluida com sucesso"));
            else
                return (sucesso, new ValidationResult(ValidationResult.TpMensagem.CampoInvalido, "Não foi possivel efetuar a exclusão"));
        }


        public List<Categoria> Consultar(String nome)
        {
            return _categoriaRepository.Consultar(nome);
        }
    }
}
