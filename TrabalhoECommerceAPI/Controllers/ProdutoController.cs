using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabalhoECommerceAPI.Models.Produto;
using TrabalhoECommerceAPI.Services;
using TrabalhoECommerceAPI.ViewModels.Produto;

namespace TrabalhoECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
     
        [HttpGet]
        [Route("[action]")]
        public IActionResult Consultar(String nome)
        {
            ProdutoService ps = new ProdutoService();
            List<Produto> p = ps.Consultar(nome);
            if (p == null)
                return BadRequest(ValidationResult.CampoInvalido("Não foi possivel encontrar uma produto com o Id correspondente!"));
            else return Ok(p);
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Obter(int id)
        {
            ProdutoService ps = new ProdutoService();
            Produto p = ps.Obter(id);
            if (p == null)
                return BadRequest(ValidationResult.CampoInvalido("Não foi possivel encontrar uma produto com o Id correspondente!"));
            else return Ok(p);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(ViewModels.Produto.ProdutoViewModel vm)
        {

            Produto p = new()
            {
                Id = vm.Id,
                Nome = vm.Nome,
                Ativo = vm.Ativo,
                Estoque = vm.Estoque,
                PrecoVenda = vm.PrecoVenda,
                Unidade = new()
                {
                    Id = vm.UnidadeId
                }
            };
            foreach (var id in vm.CategoriasIds)
            {
             

                Categoria c = new();
                c.Id = id;
                p.Categorias.Add(c);
            }
            ProdutoService ps = new();
            (bool sucesso, ValidationResult vr) = ps.Salvar(p);
            if (sucesso)
                return Ok(ValidationResult.Sucesso("Produto gravado com sucesso!"));
            else return BadRequest(vr);
          
        }
        [HttpDelete]
        [Route("[action]")]
        public IActionResult Excluir(int id)
        {
            ProdutoService ps = new ProdutoService();
            (bool sucesso, ValidationResult vr) = ps.Excluir(id);
            if (sucesso)
                return Ok(ValidationResult.Sucesso("Exclusão feita com sucesso!"));
            else return BadRequest(vr);
        }
       
    
    }
}
