using Microsoft.AspNetCore.Mvc;
using APIConsumer.Data;
using APIConsumer.Models;

namespace APIConsumer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private readonly AppDbContext _context;

    public DataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var message = await _context.Mensagens.FindAsync(id);
        if (message == null)
            return NotFound();

        return Ok(message);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Message msg)
    {
        if (msg == null)
            return BadRequest();

        msg.ReceiptDate = DateTime.Now;
        _context.Mensagens.Add(msg);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = msg.Id }, msg);
    }
}