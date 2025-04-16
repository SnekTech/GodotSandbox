namespace Sandbox.FSM;

public interface IState
{
    void OnEnter();
    void OnExit();
}