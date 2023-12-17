namespace TrabalhoECommerceAPI.Services
{
    public class UnidadeService
    {
        Repository.UnidadeRepository _unidadeRepository = new();

        public (bool, ValidationResult) Salvar(Models.Produto.Unidade unidade)
        {
            ValidationResult vr = new ValidationResult();
            bool sucesso = false;
            bool continuar = true;

            if (string.IsNullOrEmpty(unidade.Nome))
            {
                continuar = false;
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Nome não foi fornecido.";
            }
            else if (unidade.Nome.Length > 6)
            {
                continuar = false;
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Nome ultrapassa 6 caracteres.";
            }
            else
            {

                var unidadeExistente = _unidadeRepository.ObterPorNome(unidade.Nome);

                if (unidadeExistente != null && unidade.Id==0)
                {
                    continuar = false;
                    vr.Tipo = ValidationResult.TpMensagem.Erro;
                    vr.Mensagem = "Unidade já existente.";
                }
            }

            if (continuar)
            {
                sucesso = _unidadeRepository.Salvar(unidade);

                if (!sucesso)
                {
                    vr.Tipo = ValidationResult.TpMensagem.Erro;
                    vr.Mensagem = "Erro desconhecido. Tente novamente.";
                }
            }

            return (sucesso, vr);
        }

        public (bool, ValidationResult) Excluir(int id)
        {
            var sucesso = _unidadeRepository.Excluir(id);

            ValidationResult vr = new ValidationResult();

            if (sucesso)
            {
                vr.Tipo = ValidationResult.TpMensagem.Sucesso;
                vr.Mensagem = "Excluído com sucesso.";
            }
            else
            {
                vr.Tipo = ValidationResult.TpMensagem.CampoInvalido;
                vr.Mensagem = "Não foi possível excluir.";
            }

            return (sucesso, vr);
        }

        public Models.Produto.Unidade Obter(int id)
        {
            return _unidadeRepository.Obter(id);
        }

        public List<Models.Produto.Unidade> Consultar(string nome)
        {
            return _unidadeRepository.Consultar(nome);
        }
    }
}
