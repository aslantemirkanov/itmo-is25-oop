using Application.Dto;
using DataAccess.Models;

namespace Application.Services;

public interface IWorkerService
{
    WorkerDto CreateWorker(string login, string password, int status);
    WorkerDto? GetWorker(string login, string password);
    Worker? GetWorker(Guid id);
    Worker? PickUpCurrentWorker(string login, string password);
    void TakeOffCurrentWorker();

    void AddSubworker(Worker manager, Worker subworker);

    Worker? GetCurrentWorker();
}