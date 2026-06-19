using UnityEngine;


[CreateAssetMenu(menuName = "Events/Shipwreck Event")]
public class Shipwreck : GameEvent
{


    [SerializeField] private int m_foodGain;
    [SerializeField] private int m_waterGain;
    [SerializeField] private int m_goldGain;

    [SerializeField] private bool m_foundSheep;
    [SerializeField] private GameObject m_sheep;
    [SerializeField] private Transform m_parent;
    [SerializeField][TextArea] private string m_description;
    

    public override bool canTrigger(GameContext context)
    {
        return context.isTraveling;
    }

    public override void Trigger(GameContext context)
    {

       PauseController.IsPaused(true);
       // GameManager.Instance.SetTimePause();
        //trigger Ui void showMessage
        context.ui.ShowMessage(m_description + $"Food:{m_foodGain}  Water:{m_waterGain}  Gold:{m_goldGain}");
        //trigger lost ressource
        if (m_foundSheep)
        {
            Instantiate(m_sheep);
        }
        RessourceSystem.Instance.GainRessource(m_foodGain, ERessourceType.FOOD);
        RessourceSystem.Instance.GainRessource(m_waterGain, ERessourceType.WATER);
        RessourceSystem.Instance.GainRessource(m_goldGain, ERessourceType.WATER);
    }
}
