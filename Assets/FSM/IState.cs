public interface IState
{
    public void OnStateEntered();
    public void OnStateExecute();
    public void OnStateExit();
}