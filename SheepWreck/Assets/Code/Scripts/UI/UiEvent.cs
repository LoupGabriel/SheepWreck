using System.Collections;
using TMPro;
using UnityEngine;

public class UiEvent : MonoBehaviour
{
    [SerializeField] private GameObject m_eventPanel;
    [SerializeField] private TMP_Text m_message;
    [SerializeField] private float m_typeSpeed = 0.3f;

    private Coroutine m_TypeRoutine;

    public static UiEvent Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowMessage(string message)
    {
        //active panel
      
        UiPanelManager.Instance.OpenPanel(m_eventPanel);

        //show message
        if(m_TypeRoutine != null)
        {
            StopCoroutine(m_TypeRoutine);
        }
        m_TypeRoutine = StartCoroutine(TypeTextRoutine(message));
    }

    private IEnumerator TypeTextRoutine(string message)
    {
        
        m_message.text = "";

        foreach (char c in message)
        {
            m_message.text += c;
            SfxManager.PlaySfx("Error");
            yield return new WaitForSeconds(m_typeSpeed);
        }
    }
}
