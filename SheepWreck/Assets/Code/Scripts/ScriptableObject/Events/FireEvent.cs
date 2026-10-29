using UnityEngine;

[CreateAssetMenu(menuName = "Events/FireEvent")]
public class FireEvent : GameEvent
{
    public override bool canTrigger(GameContext context)
    {
        return context.isTraveling;
    }

    public override void Trigger(GameContext context)
    {
        GameManager.Instance.ChangeState(new FireEventState(GameManager.Instance));
    }

   
}
