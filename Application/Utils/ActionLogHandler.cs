using Core.Entities;
using Core.Helpers;
using Infrastructure;
using System;
using System.Threading.Tasks;

namespace Application.Utils;
public interface IActionLogHandler
{
    Task<bool> Handle(string title);
}
public class ActionLogHandler : IActionLogHandler
{
    private readonly IUserHelper _helper;
    private readonly DBContext _context;

    public ActionLogHandler(IUserHelper helper, DBContext context)
    {
        _helper = helper;
        _context = context;
    }
    public async Task<bool> Handle(string title)
    {
        var user = _helper.GetUserFromToken();

        var actionLog = new ActionLog()
        {
            Action = title,
            RecordDate = DateTime.Now,
            UserId = user.Id,
        };
        _context.ActionLog.Add(actionLog);

        await _context.SaveChangesAsync();

        return true;
    }
}
