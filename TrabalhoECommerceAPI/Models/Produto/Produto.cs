namespace TrabalhoECommerceAPI.Models.Produto
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal Estoque { get; set; }
        public bool Ativo { get; set; }
        public Unidade Unidade { get; set; }

        public List<Categoria> Categorias { get; set; }
    
        public Produto()
        {
            Nome = "";
            Unidade = new Unidade();
            Categorias = new List<Categoria>();
        }
    }
}
