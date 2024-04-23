namespace AuthenticationService.Shared.Persistence.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}