namespace TrabalhoECommerceAPI
{
    public class ValidationResult
    {
        public enum TpMensagem
        {
            CampoInvalido = 0,
            Erro = 1,
            Sucesso = 2,
        }

        public TpMensagem Tipo { get; set; }

        public string Mensagem { get; set; }

        public ValidationResult(TpMensagem tipo, string mensagem)
        {
            Tipo = tipo;
            Mensagem = mensagem;

        }

        public ValidationResult()
        {

            Tipo = TpMensagem.CampoInvalido;
            Mensagem = "";
        }


        public static ValidationResult Sucesso(string mensagem) {

            ValidationResult vr = new ValidationResult();
            vr.Tipo = TpMensagem.Sucesso;
            vr.Mensagem = mensagem;

            return vr;
        }

        public static ValidationResult CampoInvalido(string mensagem)
        {

            ValidationResult vr = new ValidationResult();
            vr.Tipo = TpMensagem.CampoInvalido;
            vr.Mensagem = mensagem;

            return vr;
        }
    }
}
