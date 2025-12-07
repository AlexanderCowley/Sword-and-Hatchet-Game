using UnityEngine;
using System.Collections.Generic;
public abstract class AbstractStateMachine
{
    IState _currentState;
    public IState CurrentState => _currentState;
    protected List<IState> _states = new List<IState>();
    protected bool _InTransition { get; private set; }
    public abstract void CreateStates();
    public void AddState(IState newState) => _states.Add(newState);

    public void ChangeState<T>() where T : IState
    {
        T targetState = (T)_states.Find(x => x is T);

        if (targetState == null)
            return;

        InitiateCurrentState(targetState);
    }

    void InitiateCurrentState(IState state)
    {
        if (_currentState != state && !_InTransition)
            Transition(state);
    }

    void Transition(IState nextState)
    {
        _InTransition = true;

        _currentState?.OnStateExit();
        _currentState = nextState;
        _InTransition = false;

        _currentState.OnStateEntered();
    }

    public void UpdateState()
    {
        if (_currentState != null && !_InTransition)
            _currentState?.OnStateExecute();
    }
}
