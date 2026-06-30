using UnityEngine;

public class SheepWorkingState : SheepState
{
    
    public SheepWorkingState(SheepInstance sheep, SheepStateMachine sheepStateMachine) : base(sheep, sheepStateMachine)
    {



    }
    public override void Enter(Animator animator) 
    {
        Debug.Log("Enter Working State");
        animator.SetTrigger("setWorking");
    
    }
    public override void Update() {  }
    public override void Exit() { }
}
