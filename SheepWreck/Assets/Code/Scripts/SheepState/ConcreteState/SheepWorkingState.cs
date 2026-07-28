using UnityEngine;

public class SheepWorkingState : SheepState
{
    
    public SheepWorkingState(SheepInstance sheep, SheepStateMachine sheepStateMachine) : base(sheep, sheepStateMachine)
    {



    }
    public override void Enter(Animator animator) 
    {
       
        animator.SetTrigger("setWorking");
    
    }
    public override void Update() {  }
    public override void Exit() { }
}
