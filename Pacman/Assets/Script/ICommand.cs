public interface ICommand
{
    void Execute();
    float TimeStamp { get; }
}