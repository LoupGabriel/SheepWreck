using UnityEngine;

public class SheepIdleState : SheepState
{


    public SheepIdleState(SheepInstance sheep, SheepStateMachine sheepStateMachine):base(sheep, sheepStateMachine)
    { 
    
    
    
    }
    public override void Enter(Animator animator) 
    {
      
        animator.SetTrigger("setIdle");
    
    }
    public override void Update() { }
    public override void Exit() { }
}
