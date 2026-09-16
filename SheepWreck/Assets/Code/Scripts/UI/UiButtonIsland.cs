
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UiButtonIsland : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] string m_panelName;
    
    private Button m_button;

    private void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        UiIslandRecruit recruit = UiPanelManager.Instance.GetComponent<UiIslandRecruit>();
        UiIslandQuest quest = UiPanelManager.Instance.GetComponent<UiIslandQuest>();


        if(m_panelName == "QuestPanel")
        {
            quest.ActiveQuestPanel();
        }
        else if (m_panelName == "RecruitAtIslandPanel")
        {
            recruit.ActiveRecruitPanel();
        }
       
        
        
        UiPanelManager.Instance.OpenPanelByName(m_panelName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursorType(ECursorType.InteractUI);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursorType(ECursorType.Default);
    }
}
