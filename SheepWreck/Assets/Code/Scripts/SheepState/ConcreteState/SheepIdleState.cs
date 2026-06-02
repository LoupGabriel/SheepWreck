using UnityEngine;

public class SheepIdleState : SheepState
{


    public SheepIdleState(SheepInstance sheep, SheepStateMachine sheepStateMachine):base(sheep, sheepStateMachine)
    { 
    
    
    
    }
    public override void Enter() 
    {

        Debug.Log("Enter Idle State");
    
    }
    public override void Update() { }
    public override void Exit() { }
}
