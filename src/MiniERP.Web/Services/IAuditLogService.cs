namespace MiniERP.Web.Services;

public interface IAuditLogService
{
    void Log(string action, string entityName, int? entityId, string description);
}
