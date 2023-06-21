using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApiTest.Core.Dtos;
using ReactApiTest.Core.Models;
using ReactApiTest.Data;

namespace ReactApiTest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            List<Person> people = await _context.People.ToListAsync();
            return Ok(people);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonById(int id)
        {
            Person? person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto request)
        {
            if (request.Name == null || request.Age == null) 
            { 
                return BadRequest();
            }
            Person person = new()
            {
                Name = request.Name,
                Age = request.Age
            };
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditPerson(int id, PersonDto person)
        {
            Person? dbPerson = await _context.People.FindAsync(id);

            if (dbPerson == null) return NotFound();

            if (person.Name == null || person.Age == null)
            {
                return BadRequest();
            }

            dbPerson.Name = person.Name;
            dbPerson.Age = person.Age;
            await _context.SaveChangesAsync();
            return Ok(person);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int id)
        {
            Person? dbPerson = await _context.People.FindAsync(id);

            if (dbPerson == null) return NotFound();

            _context.Remove(dbPerson);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
