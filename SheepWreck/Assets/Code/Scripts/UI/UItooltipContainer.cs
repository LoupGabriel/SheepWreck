using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UItooltipContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UiToolTips m_toolTip;

    [SerializeField] private TMP_Text m_text;
    [SerializeField] private ETooltipType m_tooltipType;
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_toolTip.ShowTooltip(m_text.text,m_tooltipType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_toolTip.HideTooltip();
    }
}
