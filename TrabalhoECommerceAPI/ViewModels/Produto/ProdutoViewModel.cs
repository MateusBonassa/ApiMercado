namespace TrabalhoECommerceAPI.ViewModels.Produto
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal Estoque { get; set; }
        public bool Ativo { get; set; }
        public int UnidadeId { get; set; }
        public List<int> CategoriasIds { get; set; }



    }
}
