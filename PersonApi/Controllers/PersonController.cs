using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonApi.Data;
using PersonApi.Models;

namespace PersonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController(AppDbContext db) : ControllerBase
{
    // POST /api/person
    [HttpPost]
    public async Task<ActionResult<Person>> Create([FromBody] Person p)
    {
        db.Persons.Add(p);                 // Id sätts av databasen
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = p.Id }, p);
    }

    // GET /api/person/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Person>> GetById(int id)
        => await db.Persons.FindAsync(id) is { } p ? Ok(p) : NotFound();

    // GET /api/person
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        => Ok(await db.Persons.AsNoTracking().ToListAsync());

    // PUT /api/person/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Person input)
    {
        if (id != input.Id) return BadRequest("Id mismatch");
        if (!await db.Persons.AnyAsync(x => x.Id == id)) return NotFound();
        db.Entry(input).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/person/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await db.Persons.FindAsync(id);
        if (p is null) return NotFound();
        db.Persons.Remove(p);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
