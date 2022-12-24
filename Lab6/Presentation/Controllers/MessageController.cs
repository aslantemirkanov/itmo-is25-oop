using Application.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IWorkerService _workerService;
    private Worker? _currentWorker = null;

    public MessageController(IMessageService messageService, IWorkerService workerService)
    {
        _workerService = workerService;
        _messageService = messageService;
    }

    [Route("/LogInMessenger")]
    [HttpPost]
    public void LogIn([FromBody] CreateAccountModel model)
    {
        _currentWorker = _workerService.PickUpCurrentWorker(model.login, model.password);
    }

    [Route("/SendMessage")]
    [HttpPost]
    public void SendMessage([FromBody] CreateMessageModel model)
    {
        _messageService.SendMessage(model.sender, model.receiver, model.message);
    }
}