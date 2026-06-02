using UnityEngine;

public class SheepEatingState : SheepState
{
    public SheepEatingState(SheepInstance sheep, SheepStateMachine sheepStateMachine) : base(sheep, sheepStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter Eating State");
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
