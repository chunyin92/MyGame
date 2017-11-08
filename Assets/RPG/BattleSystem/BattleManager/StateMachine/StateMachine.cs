public class StateMachine<T>
{
    public State<T> currentState { get; private set; }
    public State<T> previousState { get; private set; }
    public T Owner;

    public StateMachine (T o)
    {
        Owner = o;
        currentState = null;
    }

    public void ChangeState (State<T> newstate)
    {
        if (currentState != null)
        {
            currentState.ExitState (Owner);
            previousState = currentState;
        }

        currentState = newstate;
        currentState.EnterState (Owner);
    }

    public void ReturnToPreviousState ()
    {
        if (previousState != null)
            ChangeState (previousState);
    }

    public void Update ()
    {
        if (currentState != null)
            currentState.ExecuteState (Owner);
    }    
}

public abstract class State<T>
{
    public abstract void EnterState (T owner);
    public abstract void ExecuteState (T owner);
    public abstract void ExitState (T owner);    
}