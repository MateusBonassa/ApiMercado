using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrabalhoECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(ViewModels.Produto.UnidadeViewModel vm)
        {
          

            Models.Produto.Unidade u = new()
            {
                Id = vm.Id,
                Nome = vm.Nome
            };

            Services.UnidadeService us = new Services.UnidadeService();

            (bool sucesso, ValidationResult vr) = us.Salvar(u);

            if (sucesso)
                return Ok(ValidationResult.Sucesso("Deu certo"));
            else return BadRequest(vr);
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Excluir(int id)
        {
            Services.UnidadeService us = new Services.UnidadeService();

            (bool sucesso, ValidationResult vr) = us.Excluir(id);

            if (sucesso)
                return Ok(vr);
            return BadRequest(vr);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Obter(int id)
        {
            Services.UnidadeService us = new Services.UnidadeService();

            var unidade = us.Obter(id);

            if (unidade != null)
                return Ok(unidade);
            return BadRequest(ValidationResult.CampoInvalido("Unidade não encontrada."));
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Consultar(string termo)
        {
            Services.UnidadeService us = new Services.UnidadeService();

            var resultado = us.Consultar(termo);

            return Ok(resultado);
        }


    }
}
