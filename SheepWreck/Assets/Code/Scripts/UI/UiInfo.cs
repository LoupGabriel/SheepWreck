using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInfo : MonoBehaviour
{
    [SerializeField] private GameObject m_discutionPanel;
    [SerializeField] private Button m_button;
    [SerializeField] private TMP_Text m_text;

    private bool m_isActive = false;

    [SerializeField][TextArea] private string[] m_info;
    [SerializeField] private string m_inactiveText;
    [SerializeField] private Animator m_bubbleAnimator;
    private int m_textIndex = 0;
    private string m_currentInfo;

    private float m_elapse = 0;
    private Coroutine m_timerRoutine;
    private void Start()
    {
        m_currentInfo = m_info[0];
        m_timerRoutine = StartCoroutine(Timer());
    }

    
  
    public void OnBubbleInfo()
    {

        m_discutionPanel.SetActive(true);
        if(m_isActive)
        {
            m_text.text = m_currentInfo;
        }
        else
        {
            m_text.text = m_inactiveText;
        }
    }
    public void OnClickOK()
    {
        if (m_isActive)
        {
            if(m_textIndex != m_info.Length -1)
            {
                m_textIndex++;
            }
            else
            {
                m_isActive = !m_isActive;
                m_bubbleAnimator.SetTrigger("inactive");
            }
            
            m_currentInfo = m_info[m_textIndex];
        }
        
        m_discutionPanel.SetActive(!m_discutionPanel.activeSelf);

        
    }


    private IEnumerator Timer()
    {
        float elapse =0;

        while(elapse < 5f)
        {
            elapse += Time.deltaTime;
            yield return null;
        }
        m_isActive = true;
        m_bubbleAnimator.SetTrigger("active");
    }
}
