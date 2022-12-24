using Application.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private IReportService _reportService;
    private IWorkerService _workerService;
    private Worker? _currentWorker = null;

    public ReportController(IReportService reportService, IWorkerService workerService)
    {
        _reportService = reportService;
        _workerService = workerService;
    }

    [Route("/LogInReport")]
    [HttpPost]
    public void LogIn([FromBody] CreateWorkerModel model)
    {
        _currentWorker = _workerService.PickUpCurrentWorker(model.login, model.password);
    }

    [Route("/GetTodayReport")]
    [HttpPost]
    public List<Message> GetTodayMessages()
    {
        return _reportService.GetTodayMessages();
    }
}