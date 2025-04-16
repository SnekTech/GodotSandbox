using System;
using System.Collections.Generic;

namespace Sandbox.FSM;

public abstract class StateMachine<TState> where TState : class, IState
{
    protected readonly Dictionary<Type, TState> StateInstances = [];
    
    protected TState? CurrentState { get; private set; }

    protected abstract void InstantiateStateInstances();

    public bool IsAtState<T>() where T : TState
    {
        if (CurrentState == null) return false;

        return CurrentState == GetState<T>();
    }

    protected TState GetState<T>() => StateInstances[typeof(T)];

    public void SetInitState<T>() where T : TState
    {
        InstantiateStateInstances();

        if (StateInstances.Count == 0)
            throw new InvalidOperationException("cannot set initial state on a FSM with no state instances");

        CurrentState = GetState<T>();
        CurrentState.OnEnter();
    }

    public void ChangeState<T>() where T : TState
    {
        if (CurrentState == null)
            throw new InvalidOperationException("cannot change state from a null state");

        var newState = GetState<T>();
        if (newState == CurrentState)
            return;
        
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}