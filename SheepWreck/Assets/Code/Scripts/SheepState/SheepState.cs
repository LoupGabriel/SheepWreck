using UnityEngine;

public class SheepState 
{

    protected SheepInstance sheepInstance;
    protected SheepStateMachine sheepStateMachine;
    public SheepState(SheepInstance sheep, SheepStateMachine sheepStateMachine) 
    { 
        this.sheepInstance = sheep;
        this.sheepStateMachine = sheepStateMachine;
    
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

}












