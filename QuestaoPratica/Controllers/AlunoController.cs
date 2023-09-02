using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuestaoPratica.Models.Request;

namespace QuestaoPratica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : PrincipalController
    {
        public AlunoController()
        {
            _alunoCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "aluno.json");
        }

        private readonly string _alunoCaminhoArquivo;

        #region Metodos
        private List<AlunoViewModel> LerAlunosDoArquivo()
        {
            if (!System.IO.File.Exists(_alunoCaminhoArquivo))
            {
                return new List<AlunoViewModel>();
            }

            string json = System.IO.File.ReadAllText(_alunoCaminhoArquivo);
            if (string.IsNullOrEmpty(json))
            {
                return new List<AlunoViewModel>();
            }
            return JsonConvert.DeserializeObject<List<AlunoViewModel>>(json);
        }

        private int ObterProximoCodigoDisponivel()
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            if (alunos.Any())
            {
                return alunos.Max(p => p.Codigo) + 1;
            }
            else
            {
                return 1;
            }
        }

        private void EscreverAlunosNoArquivo(List<AlunoViewModel> alunos)
        {
            string json = JsonConvert.SerializeObject(alunos);
            System.IO.File.WriteAllText(_alunoCaminhoArquivo, json);
        }



        #endregion

        #region Operações CRUD

        [HttpGet]
        public IActionResult Get()
        {
            List<AlunoViewModel> alunos  = LerAlunosDoArquivo();
            return Ok(alunos);
        }

        [HttpGet("{codigo}")]
        public IActionResult Get(int codigo)
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            AlunoViewModel aluno = alunos.Find(a => a.Codigo == codigo);
            if(aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post([FromBody]AlunoViewModel aluno)
        {
            //if(aluno == null)
            //{
            //    return BadRequest();
            //}

            if (!ModelState.IsValid)
            {
                return ApiBadRequestResponse(ModelState, "Dados Inválidos");
            }

            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            int proximoCodigo = ObterProximoCodigoDisponivel();

            AlunoViewModel novoAluno = new AlunoViewModel()
            {
                Codigo = proximoCodigo,
                Nome = aluno.Nome,
                Ra = aluno.Ra,
                Email = aluno.Email,
                Cpf = aluno.Cpf,
                Ativo = aluno.Ativo,
            };

            alunos.Add(novoAluno);
            EscreverAlunosNoArquivo(alunos);

            return ApiResponse(novoAluno, "Aluno cadastrado com sucesso!");
            //return CreatedAtAction(nameof(Get), new { codigo = novoAluno.Codigo }, novoAluno);
        }

        [HttpPut("{codigo}")]
        public IActionResult Put(int codigo, [FromBody] EditaAlunoViewModel aluno)
        {
            if (aluno == null)
            {
                return BadRequest();
            }
           
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            int index = alunos.FindIndex(p => p.Codigo == codigo);

            if (index == -1)
            {
                return NotFound();
            }

            AlunoViewModel alunoEditado = new AlunoViewModel()
            {
                Codigo = codigo,
                Nome = aluno.Nome,
                Ra = aluno.Ra,
                Email = aluno.Email,
                Cpf = aluno.Cpf,
                Ativo = aluno.Ativo,
            };

            alunos[index] = alunoEditado;
            EscreverAlunosNoArquivo(alunos);
            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            AlunoViewModel aluno = alunos.Find(a => a.Codigo == codigo);
            if(aluno == null)
                return NotFound();

            alunos.Remove(aluno);
            EscreverAlunosNoArquivo(alunos);
            return NoContent();
        }

            #endregion
        
    }
}
