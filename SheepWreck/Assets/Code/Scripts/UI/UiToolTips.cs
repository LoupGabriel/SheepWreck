using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;




public enum ETooltipType
{
    Trait, Speciality, Ressource, Icon
}
public class UiToolTips : MonoBehaviour
{
    [SerializeField] private TMP_Text m_tooltipsText;
    [SerializeField] private GameObject m_tooltipPanel;
    [SerializeField] private SelectionManager m_selectionManager;

    
    private void Awake()
    {
        m_tooltipPanel.SetActive(false);
    }
    public void ShowTooltip(string text,ETooltipType tooltipType)
    {

        switch (tooltipType)
        {
            case ETooltipType.Trait:
                {
                    m_tooltipsText.text = GetTraitText(m_selectionManager.m_lastSelectedSheep.m_sheepData.Trait);
                    break;
                }

            case ETooltipType.Speciality:
                {
                    m_tooltipsText.text = GetSpeciality(m_selectionManager.m_lastSelectedSheep.m_sheepData.Speciality);
                    break;
                }
            
        }

        m_tooltipPanel.SetActive(true);
        m_tooltipPanel.transform.position = Mouse.current.position.ReadValue();

    }

    public void HideTooltip()
    {
        m_tooltipPanel.SetActive(false);
    }

    private string GetTraitText(ESheepTrait sheepTrait)
    {
        switch (sheepTrait)
        {
            case ESheepTrait.HardWorker:
                {
                    return "Work faster";
                }

            case ESheepTrait.Lazy:
                {
                    return "Work slower";
                }

            case ESheepTrait.Glutton:
                return "Eat more food";

            case ESheepTrait.Frugal:
                return "Eat less food";

            default: return string.Empty;
        }
    }


    private string GetSpeciality(ESheepSpeciality speciality)
    {
        switch (speciality)
        {
            case ESheepSpeciality.Farmer:
                return "more efficient in food production";
            case ESheepSpeciality.Engineer:
                return "more efficient in energy production";
            case ESheepSpeciality.BookWorm:
                return "more efficient in Research Production";
            case ESheepSpeciality.SeaWolf:
                return "Basic speciality";

            default: return string.Empty;
        }
    }


}
