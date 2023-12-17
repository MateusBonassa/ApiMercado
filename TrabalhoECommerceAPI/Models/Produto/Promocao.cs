namespace TrabalhoECommerceAPI.Models.Produto
{
    public class Promocao
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public Produto Produto { get; set; }
        public Promocao()
        {
           Produto = new Produto();
        }
    }
}
