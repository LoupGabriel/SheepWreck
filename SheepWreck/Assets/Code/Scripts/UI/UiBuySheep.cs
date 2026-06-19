using UnityEngine;

public class UiBuySheep : MonoBehaviour
{

    [SerializeField] private GameObject m_sheep;
    [SerializeField] private Transform m_parent;
    [SerializeField] private int m_sheepCost = 150;
    public void OnBuySheep()
    {
        
        if (RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.GOLD] < m_sheepCost) { return; }
        //pay the gold amount
        RessourceSystem.Instance.GainRessource(m_sheepCost * -1, ERessourceType.GOLD);
        Instantiate(m_sheep, m_parent);
    }
}
