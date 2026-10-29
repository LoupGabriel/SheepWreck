using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInfo : MonoBehaviour
{
    [SerializeField] private GameObject m_discutionPanel;
    [SerializeField] private Button m_button;
    [SerializeField] private TMP_Text m_text;
    [SerializeField] private Animator m_bubbleAnimator;

    [SerializeField][TextArea] private string[] m_info;

    private UiPanelManager m_panelManager;
    private string m_currentInfo;
    private bool m_isActive;
    public static event System.Action OnCompleteTutorial;
    private void Awake()
    {
        m_panelManager = GetComponent<UiPanelManager>();

        m_discutionPanel.SetActive(false);
        m_button.gameObject.SetActive(false);
    }

   
    public void ShowInfo(int index)
    {
        if (index < 0 || index >= m_info.Length)
        {
            Debug.LogWarning($"Invalid tutorial info index: {index}", this);
            return;
        }

        m_currentInfo = m_info[index];
        m_isActive = true;

        m_button.gameObject.SetActive(true);

        m_bubbleAnimator.ResetTrigger("inactive");
        m_bubbleAnimator.SetTrigger("active");
    }

    public void OnBubbleInfo()
    {
        if (!m_isActive)
            return;

        m_text.text = m_currentInfo;
        m_discutionPanel.SetActive(true);
    }

    public void OnClickOK()
    {
        OnCompleteTutorial?.Invoke();
        m_isActive = false;
        m_discutionPanel.SetActive(false);

        m_bubbleAnimator.ResetTrigger("active");
        m_bubbleAnimator.SetTrigger("inactive");

        m_panelManager.CloseCurrentPanel();
        PauseController.IsPaused(false);

        m_button.gameObject.SetActive(false);
    }
}