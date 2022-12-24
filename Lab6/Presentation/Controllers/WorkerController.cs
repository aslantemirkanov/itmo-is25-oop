using Application.Dto;
using Application.Exceptions;
using Application.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private IWorkerService _service;
    private Worker? _currentWorker = null;

    public WorkerController(IWorkerService service)
    {
        _service = service;
    }

    [Route("/LogIn")]
    [HttpPost]
    public void LogIn([FromBody] CreateWorkerModel model)
    {
        _currentWorker = _service.PickUpCurrentWorker(model.login, model.password);
    }

    [Route("/LogOut")]
    [HttpGet]
    public void LogOut()
    {
        _service.TakeOffCurrentWorker();
        _currentWorker = null;
    }

    [Route("/CreateManager")]
    [HttpPost]
    public ActionResult<WorkerDto> CreateManager([FromBody] CreateWorkerModel model)
    {
        var worker = _service.CreateWorker(model.login, model.password, 1);
        return Ok(worker);
    }

    [Route("/CreateSimpleWorker")]
    [HttpPost]
    public ActionResult<WorkerDto> CreateSimpleWorker([FromBody] CreateWorkerModel model)
    {
        var worker = _service.CreateWorker(model.login, model.password, 0);
        return Ok(worker);
    }

    [Route("/AddSimpleWorkersToManager")]
    [HttpPost]
    public void AddSimpleWorkerToManager([FromBody] CreateWorkerModel model, Guid id)
    {
        LogIn(model);
        Worker? currentManager = _currentWorker;
        Worker? simpleWorker = _service.GetWorker(id);
        if (currentManager == null || simpleWorker == null)
        {
            throw new NotFoundException("entity doesn't exist");
        }

        _service.AddSubworker(currentManager, simpleWorker);
    }
}