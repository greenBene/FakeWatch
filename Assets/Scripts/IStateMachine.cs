public interface IStateMachine<T> {
    //void StateTransition();
    //void StateOnStay();
    //bool ChangeState(T newState);
    bool RequestStateChange(T newState);
}
