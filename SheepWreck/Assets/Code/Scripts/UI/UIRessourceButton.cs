using TMPro;
using UnityEngine;

public class UIRessourceButton : MonoBehaviour
{
    [SerializeField] private TMP_Text m_foodConsumptionText;
    [SerializeField] private TMP_Text m_waterConsumptionText;
    [SerializeField] private TMP_Text m_goldConsumptionText;

    private void Start()
    {
        m_foodConsumptionText.color = Color.green;
        m_waterConsumptionText.color = Color.green;
        m_goldConsumptionText.color = Color.green;
        RessourceSystem.OnFoodConsumptionChange += changeFoodTextColor;
        RessourceSystem.OnWaterConsumptionChange += changeWaterTextColor;
        RessourceSystem.OnGoldConsumptionChange += changeGoldTextColor;

    }
    private void OnDestroy()
    {
        RessourceSystem.OnFoodConsumptionChange -= changeFoodTextColor;
        RessourceSystem.OnWaterConsumptionChange -= changeWaterTextColor;
        RessourceSystem.OnGoldConsumptionChange -= changeGoldTextColor;
    }
    public void changeFoodTextColor(bool state)
    {
        if (state)
        {
            m_foodConsumptionText.color = Color.green;
        }
        else
        {
            m_foodConsumptionText.color = Color.red;
        }
    }
    public void changeWaterTextColor(bool state)
    {
        if (state)
        {
            m_waterConsumptionText.color = Color.green;
        }
        else
        {
            m_waterConsumptionText.color = Color.red;
        }
    }
    public void changeGoldTextColor(bool state)
    {
        if (state)
        {
            m_goldConsumptionText.color = Color.green;
        }
        else
        {
            m_goldConsumptionText.color = Color.red;
        }
    }

    public void openMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }
}
