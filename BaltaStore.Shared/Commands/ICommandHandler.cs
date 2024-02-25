namespace BaltaStore.Shared.Commands
{
    public interface ICommandHandler<T>
    {
        ICommandResult Handler(T command);
    }
}