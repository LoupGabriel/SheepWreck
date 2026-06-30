using UnityEngine;

public class SheepWaitForWorkState : SheepState
{
    public SheepWaitForWorkState(SheepInstance sheep, SheepStateMachine sheepStateMachine) : base(sheep, sheepStateMachine)
    {
    }

    public override void Enter(Animator animator)
    {
        Debug.Log("Enter WaitForWork");
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
