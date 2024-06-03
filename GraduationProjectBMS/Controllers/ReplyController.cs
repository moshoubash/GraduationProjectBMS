using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;
using GraduationProjectBMS.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReplyController : ControllerBase
{
    private readonly MyDbContext _context;

    public ReplyController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostReply([FromBody] ReplyViewModel model)
    {
        if (ModelState.IsValid)
        {
            var reply = new Reply
            {
                ReplyContent = model.ReplyContent,
                UserId = model.UserId,
                CommentId = model.CommentId,
                CreatedAt = DateTime.Now
            };

            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();

            return Ok(reply);
        }
        return BadRequest(ModelState);
    }
}
