using UnityEngine;

[CreateAssetMenu(menuName = "Events/Storm Event")]
public class StormEvent : GameEvent
{
    [SerializeField] private int m_foodLost;
    [SerializeField] private int m_waterLost;
    [SerializeField] private int m_goldLost;

    [SerializeField][TextArea] private string m_description;
    public override bool canTrigger(GameContext context)
    {
        return context.isTraveling;
    }

    public override void Trigger(GameContext context)
    {

        //PauseController.IsPaused(true);
        // GameManager.Instance.SetTimePause();
        //trigger Ui void showMessage
        //context.ui.ShowMessage(m_description + $"Food:{-m_foodLost}  Water:{-m_waterLost}  Gold:{-m_goldLost}");
        //trigger lost ressource

        //RessourceSystem.Instance.GainRessource(-m_foodLost, ERessourceType.FOOD);
        //RessourceSystem.Instance.GainRessource(-m_waterLost, ERessourceType.WATER);
        //trigger lost ship hp 

        GameManager.Instance.ChangeState(new SheepwreckState(GameManager.Instance));

    }



   


}
