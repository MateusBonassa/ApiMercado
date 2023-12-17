namespace TrabalhoECommerceAPI.Models.Produto
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Categoria()
        {
            Nome = "";
        }
    }
}
