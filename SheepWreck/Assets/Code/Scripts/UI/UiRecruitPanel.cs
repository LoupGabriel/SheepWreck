
using TMPro;

using UnityEngine;

using UnityEngine.UI;

[System.Serializable]
public class SheepRecruitData
{
    public string name;
    public ESheepTrait trait;
    public ESheepSpeciality speciality;
    public int farmerXp;
    public int engineerXp; 
    public int sailorXp;
    public int cost;

   
}
public class UiRecruitPanel : MonoBehaviour
{

    
    public SheepRecruitData m_currentRecruit;
   
    [SerializeField] private TMP_Text m_name;
    [SerializeField] private TMP_Text m_trait;
    [SerializeField] private TMP_Text m_speciality;
    [SerializeField] private TMP_Text m_goldCost;
    [SerializeField] private GameObject m_sheepPrefab;
  
    private Transform m_spawnPosition;




    private void Start()
    {
        ShowRecruit(m_currentRecruit);
        Transform spawnPos = CrewManager.Instance.m_sheepSpawnPosition;
        m_spawnPosition = spawnPos;
    }

    public void ShowRecruit(SheepRecruitData recruit)
    {
        m_currentRecruit = recruit;
        
        m_name.text= m_currentRecruit.name;
        m_trait.text = m_currentRecruit.trait.ToString();
        m_speciality.text = m_currentRecruit.speciality.ToString();
        m_goldCost.text = m_currentRecruit.cost.ToString();
    }


    public void Recruit()
    {
       

        if (m_currentRecruit == null) return;

        //if doesnt have enought gold
        if (RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.GOLD] < m_currentRecruit.cost)
            return;

        RessourceSystem.Instance.GainRessource(-m_currentRecruit.cost, ERessourceType.GOLD);

        GameObject sheepObject = Instantiate(m_sheepPrefab, m_spawnPosition.position,Quaternion.identity);
        SheepInstance sheep = sheepObject.GetComponent<SheepInstance>();
        sheep.InitializeFromRecruitData(m_currentRecruit);


        
        gameObject.SetActive(false);
        

    }
}
