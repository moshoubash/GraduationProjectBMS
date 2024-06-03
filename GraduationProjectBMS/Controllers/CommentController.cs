using GraduationProjectBMS.Models;
using GraduationProjectBMS.Services;
using GraduationProjectBMS.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class CommentController : ControllerBase
{
    private readonly MyDbContext _context;

    public CommentController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostComment([FromBody] CommentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var comment = new Comment
            {
                CommentContent = model.CommentContent,
                UserId = model.UserId,
                ArticleId = model.ArticleId,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
        return BadRequest(ModelState);
    }
}
