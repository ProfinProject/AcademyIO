using AcademyIO.Payments.API.Data;
using AcademyIO.Payments.API.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademyIO.WebAPI.Core.Controllers;
using AcademyIO.WebAPI.Core.User;

namespace AcademyIO.Payments.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : MainController
{
    private readonly PaymentsContext _context;
    private readonly IAspNetUser _aspNetUser;

    public PaymentsController(PaymentsContext context, IAspNetUser aspNetUser)
    {
        _context = context;
        _aspNetUser = aspNetUser;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
    {
        var payments = await _context.Set<Payment>().Where(p => !p.Deleted).ToListAsync();
        return Ok(payments);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Payment>> GetById(Guid id)
    {
        var payment = await _context.Set<Payment>().FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
        if (payment == null) return NotFound();
        return Ok(payment);
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> Create(Payment payment)
    {
        _context.Set<Payment>().Add(payment);
        await _context.Commit();
        return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var payment = await _context.Set<Payment>().FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
        if (payment == null) return NotFound();

        _context.Set<Payment>().Remove(payment);
        await _context.Commit();
        return NoContent();
    }


    [HttpGet("exists")]
    public async Task<ActionResult<bool>> PaymentExists(Guid courseId, Guid studentId)
    {
        var exists = await _context.Set<Payment>().AnyAsync(p => p.StudentId == studentId && p.CourseId == courseId && !p.Deleted);
        return Ok(exists);
    }

}