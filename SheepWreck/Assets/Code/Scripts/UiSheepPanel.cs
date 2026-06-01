using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UiSheepPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text m_sheepName;
    private SheepInstance m_currentSheep;
    [SerializeField] private TMP_Text m_currentMoraleText;
    [SerializeField] private TMP_Text m_currentHungerText;
    [SerializeField] private TMP_Text m_curentThirstText;
    [SerializeField] private UnityEngine.UI.Image m_sprite;

    private bool m_panelIsActive = false;

    private void Update()
    {
        if (m_panelIsActive)
        {
            //update stats in reel time
            UpdateStats(m_currentSheep.GetHunger(), m_currentSheep.GetThirst(),m_currentSheep.GetMorale());
            UpdateSheepVisual();
        }
    }
    public void EnableSheepPanel(string sheepName,SheepInstance sheep)
    {

        m_panelIsActive=true;
        gameObject.SetActive(true);
        m_sheepName.text = sheepName;
        m_currentSheep = sheep;
        

    }

    private void UpdateStats(int hunger, int thirst, float morale)
    {
        //hunger
        float hungerPercentage = Mathf.Clamp01((float)hunger /100 );

        if(hungerPercentage < 0.5)
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
        if (hungerPercentage < 0.5)
        {
            m_curentThirstText.color = Color.Lerp(Color.red, Color.yellow, thirstPercentage);
        }
        else
        {
            m_curentThirstText.color = Color.Lerp(Color.yellow, Color.green, thirstPercentage);
        }
        m_curentThirstText.text = thirst.ToString() + "%";


        m_currentMoraleText.text = morale.ToString();


    }

    private void UpdateSheepVisual()
    {
        m_sprite.sprite = m_currentSheep.m_spriteRenderer.sprite;
        m_sprite.color = m_currentSheep.m_spriteRenderer.color;
    }

    public void CloseSheepPanel()
    {
        gameObject.SetActive(false);
        m_panelIsActive = false;
    }


  
}
