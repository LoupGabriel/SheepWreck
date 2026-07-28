using UnityEngine;

public class SheepRestingState : SheepState
{
    public SheepRestingState(SheepInstance sheep, SheepStateMachine sheepStateMachine) : base(sheep, sheepStateMachine)
    {
    }

    public override void Enter(Animator animator)
    {
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
