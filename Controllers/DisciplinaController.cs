using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiUniversidade.Context;
using apiUniversidade.Model;
using Microsoft.AspNetCore.Mvc;

namespace apiUniversidade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinaController : Controller
    {
        private readonly ILogger<DisciplinaController> _logger;
        private readonly apiUniversidadeContext _context;

        public DisciplinaController(ILogger<DisciplinaController> logger, apiUniversidadeContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet(Name = "disciplinas")]
        public ActionResult<IEnumerable<Disciplina>> Get(){
            var disciplinas = _context.Disciplinas.ToList();
            if (disciplinas is null)
                return NotFound();

            return disciplinas;
        }

        [HttpGet ("(id:int)", Name="GetDisciplina")]
        public ActionResult<Disciplina> Get(int id){
            var disciplina = _context.Disciplinas.FirstOrDefault(p => p.Id == id);
            if(disciplina is null)
                return NotFound("Disciplina não encontrada.");
            
            return disciplina;
        }

        [HttpPost]
        public ActionResult Post(Disciplina disciplina){
            _context.Disciplinas.Add(disciplina);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetDisciplina", new{id = disciplina.Id,}, disciplina);
        }

        [HttpPut ("{id:int}")]
        public ActionResult Put(int id, Disciplina disciplina){
            if(id != disciplina.Id)
                return BadRequest();

            _context.Entry(disciplina).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(disciplina);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var disciplina = _context.Disciplinas.FirstOrDefault(p => p.Id == id);
            if (disciplina is null)
                return NotFound();
            
            _context.Disciplinas.Remove(disciplina);
            _context.SaveChanges();

            return Ok(disciplina);
        }
    }
}