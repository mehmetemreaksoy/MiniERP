using MiniERP.Web.Data;
using MiniERP.Web.Models;

namespace MiniERP.Web.Services;

public class AuditLogService : IAuditLogService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditLogService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public void Log(string action, string entityName, int? entityId, string description)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var userName = httpContext?.User?.Identity?.IsAuthenticated == true
            ? httpContext.User.Identity.Name ?? "System"
            : "System";

        var auditLog = new AuditLog
        {
            UserName = userName,
            Action = action,
            EntityName = entityName,
            EntityId = entityId ?? 0,
            Description = description,
            IpAddress = httpContext?.Connection.RemoteIpAddress?.ToString()
        };

        _context.AuditLogs.Add(auditLog);
        _context.SaveChanges();
    }
}
