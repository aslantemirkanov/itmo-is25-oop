using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

internal class WorkerService : IWorkerService
{
    private readonly DatabaseContext _context;
    private Worker? _currentWorker;

    public WorkerService(DatabaseContext context)
    {
        _context = context;
        _currentWorker = null;
    }

    public WorkerDto CreateWorker(string login, string password, int status)
    {
        var worker = new Worker(
            login,
            password,
            Guid.NewGuid(),
            status,
            new List<Worker>(),
            new List<Account>());

        _context.Workers.Add(worker);
        _context.SaveChanges();

        return worker.AsDto();
    }

    public void AddSubworker(Worker manager, Worker subworker)
    {
        if (manager.Status != 1) return;
        if (manager.SubWorkers.Contains(subworker)) return;
        manager.SubWorkers.Add(subworker);
        _context.SaveChanges();
    }

    public WorkerDto? GetWorker(string login, string password)
    {
        var worker = _context.GetWorker(login, password);
        return worker != null ? worker.AsDto() : null;
    }

    public Worker? GetWorker(Guid id)
    {
        return _context.GetWorker(id);
    }

    public Worker? PickUpCurrentWorker(string login, string password)
    {
        _currentWorker = _context.GetWorker(login, password);
        return _currentWorker;
    }

    public void TakeOffCurrentWorker()
    {
        _currentWorker = null;
    }

    public Worker? GetCurrentWorker()
    {
        return _currentWorker;
    }
}