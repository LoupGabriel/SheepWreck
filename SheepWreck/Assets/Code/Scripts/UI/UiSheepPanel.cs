using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UiSheepPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text m_sheepName;
    private SheepInstance m_currentSheep;



    //stats
    [SerializeField] private TMP_Text m_currentMoraleText;
    [SerializeField] private TMP_Text m_currentHungerText;
    [SerializeField] private TMP_Text m_curentThirstText;

    //personality
    [SerializeField] private TMP_Text m_traitText;
    [SerializeField] private TMP_Text m_specialityText;

    //lvl
    [SerializeField] private TMP_Text m_FarmerLvlText;
    [SerializeField] private TMP_Text m_EngineerLvlText;
    [SerializeField] private TMP_Text m_SailorLvlText;


    [SerializeField] private UnityEngine.UI.Image m_sprite;
    private Sprite m_currentSprite;


    private bool m_panelIsActive = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!m_panelIsActive)
        {
            return;
        }
        if(m_currentSheep == null)
        {
            CloseSheepPanel();
            return;
        }
       
            //update stats in reel time
            UpdateStats(m_currentSheep.GetHunger(), m_currentSheep.GetThirst(), m_currentSheep.GetMorale());
            UpdateSheepVisual();
        
    }
    public void EnableSheepPanel(string sheepName, SheepInstance sheep)
    {
        if(sheep == null)
        {
            return;
        }
        m_currentSheep = sheep;
        m_sheepName.text = sheepName;
        m_traitText.text = sheep.Trait.ToString();
        m_specialityText.text = sheep.Speciality.ToString();

        m_panelIsActive = true;
        gameObject.SetActive(true);
       
        
      
       


    }

    private void UpdateStats(int hunger, int thirst, float morale)
    {
        //hunger
        float hungerPercentage = Mathf.Clamp01((float)hunger / 100);

        if (hungerPercentage < 0.5)
        {
            m_currentHungerText.color = Color.Lerp(Color.red, Color.yellow, hungerPercentage);
        }
        else
        {
            m_currentHungerText.color = Color.Lerp(Color.yellow, Color.green, hungerPercentage);
        }

        m_currentHungerText.text = hunger.ToString() + "%";


        //thirst
        float thirstPercentage = Mathf.Clamp01((float)thirst / 100);
        if (thirstPercentage < 0.5)
        {
            m_curentThirstText.color = Color.Lerp(Color.red, Color.yellow, thirstPercentage);
        }
        else
        {
            m_curentThirstText.color = Color.Lerp(Color.yellow, Color.green, thirstPercentage);
        }
        m_curentThirstText.text = thirst.ToString() + "%";


        m_currentMoraleText.text = morale.ToString();


        //lvl

        m_FarmerLvlText.text = $"lvl : {m_currentSheep.GetJobLevel(ESheepJob.Farmer)}";
        m_EngineerLvlText.text = $"lvl : {m_currentSheep.GetJobLevel(ESheepJob.Engineer)}";

    }

    private void UpdateSheepVisual()
    {   
        m_sprite.sprite = m_currentSheep.m_spriteRenderer.sprite;
        m_sprite.color = m_currentSheep.m_spriteRenderer.color;
    }

    public void CloseSheepPanel()
    {
        m_panelIsActive = false;
        gameObject.SetActive(false);
        m_panelIsActive = false;
        
    }
}

   
