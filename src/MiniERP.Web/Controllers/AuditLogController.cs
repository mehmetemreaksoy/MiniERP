using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Data;
using MiniERP.Web.Models;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AuditLogController : Controller
{
    private readonly AppDbContext _context;

    public AuditLogController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(
        string? searchText,
        string? userName,
        string? actionType,
        string? entityName,
        DateTime? startDate,
        DateTime? endDate,
        int page = 1)
    {
        const int pageSize = 20;

        searchText = searchText?.Trim();
        userName = userName?.Trim();
        actionType = actionType?.Trim();
        entityName = entityName?.Trim();
        page = page < 1 ? 1 : page;

        var auditLogs = _context.AuditLogs.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            auditLogs = auditLogs.Where(a =>
                (a.Description != null && a.Description.Contains(searchText)) ||
                a.UserName.Contains(searchText) ||
                a.EntityName.Contains(searchText));
        }

        if (!string.IsNullOrWhiteSpace(userName))
        {
            auditLogs = auditLogs.Where(a => a.UserName.Contains(userName));
        }

        if (!string.IsNullOrWhiteSpace(actionType))
        {
            auditLogs = auditLogs.Where(a => a.Action == actionType);
        }

        if (!string.IsNullOrWhiteSpace(entityName))
        {
            auditLogs = auditLogs.Where(a => a.EntityName == entityName);
        }

        if (startDate.HasValue)
        {
            auditLogs = auditLogs.Where(a => a.CreatedDate >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            var endDateExclusive = endDate.Value.Date.AddDays(1);
            auditLogs = auditLogs.Where(a => a.CreatedDate < endDateExclusive);
        }

        ViewBag.SearchText = searchText;
        ViewBag.UserName = userName;
        ViewBag.ActionType = actionType;
        ViewBag.EntityName = entityName;
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

        var totalCount = auditLogs.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        if (totalPages > 0 && page > totalPages)
        {
            page = totalPages;
        }

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.TotalCount = totalCount;

        List<AuditLog> model = auditLogs
            .OrderByDescending(a => a.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return View(model);
    }
}
