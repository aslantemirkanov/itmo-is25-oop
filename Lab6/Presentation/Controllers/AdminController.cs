using Application.Dto;
using Application.Services;
using DataAccess.Models;
using DataAccess.Models.Sources;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private IAdminService _service;

    public AdminController(IAdminService service)
    {
        _service = service;
    }

    [Route("/CreateAccount")]
    [HttpPost]
    public ActionResult<AccountDto> CreateAccount([FromBody] CreateAccountModel model)
    {
        var account = _service.CreateAccount(model.login, model.password);
        return account;
    }

    [Route("/AddMailSourceType")]
    [HttpPost]
    public void AddMailSourceType([FromBody] AddAccountModel model)
    {
        _service.AddSource(SourceType.Mail, model.id, model.message);
    }

    [Route("/AddMessengerSourceType")]
    [HttpPost]
    public void AddMessengerSourceType([FromBody] AddAccountModel model)
    {
        _service.AddSource(SourceType.Messenger, model.id, model.message);
    }

    [Route("/AddPhoneSourceType")]
    [HttpPost]
    public void AddPhoneSourceType([FromBody] AddAccountModel model)
    {
        _service.AddSource(SourceType.Phone, model.id, model.message);
    }

    [Route("/AddWorkerToAccount")]
    [HttpPost]
    public void AddWorkerToAccount(Guid accountId, Guid workerId)
    {
        _service.AddWorkerToAccount(accountId, workerId);
    }
}