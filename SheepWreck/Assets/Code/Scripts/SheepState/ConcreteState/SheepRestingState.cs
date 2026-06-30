using UnityEngine;

public class SheepRestingState : SheepState
{
    public SheepRestingState(SheepInstance sheep, SheepStateMachine sheepStateMachine) : base(sheep, sheepStateMachine)
    {
    }

    public override void Enter(Animator animator)
    {
        Debug.Log("Enter Resting State");
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
