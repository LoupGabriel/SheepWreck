using UnityEngine;

public class SheepWorkingState : SheepState
{
    public SheepWorkingState(SheepInstance sheep, SheepStateMachine sheepStateMachine) : base(sheep, sheepStateMachine)
    {



    }
    public override void Enter() 
    {
        Debug.Log("Enter Working State");
    
    
    }
    public override void Update() { }
    public override void Exit() { }
}
