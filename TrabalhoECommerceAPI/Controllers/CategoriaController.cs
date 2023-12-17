using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabalhoECommerceAPI.Models.Produto;
using TrabalhoECommerceAPI.Services;
using TrabalhoECommerceAPI.ViewModels.Produto;

namespace TrabalhoECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(CategoriaViewModel vm)
        {
            
            Categoria c = new()
            {
                Id = vm.Id,
                Nome = vm.Nome
            };
            CategoriaService cs = new CategoriaService();

            (bool sucesso, ValidationResult vr) = cs.Salvar(c);

            if (sucesso)
                return Ok(ValidationResult.Sucesso("Deu certo"));
            else return BadRequest(vr);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Obter(int id)
        {
            CategoriaService cs = new CategoriaService();
            Categoria c = cs.Obter(id);
            if (c == null)
                return BadRequest(ValidationResult.CampoInvalido("Não foi possivel encontrar uma categoria com o Id correspondente!"));
            else return Ok(c);
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Excluir(int id)
        {
            CategoriaService cs = new CategoriaService();
            (bool sucesso , ValidationResult vr)= cs.Excluir(id);
            if (sucesso)
                return Ok(vr);
            else return BadRequest(vr);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Consultar(String nome)
        {
            CategoriaService cs = new();
            List<Categoria> lista = cs.Consultar(nome);
            return Ok(lista);
        }
    }
}
