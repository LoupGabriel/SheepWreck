using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;


public class UiPanelManager : MonoBehaviour
{
    public static UiPanelManager Instance { get; private set; }
    
    [SerializeField] 
    private GameObject m_constructionPanel;
    [SerializeField]
    private GameObject m_panelOutside;
    private GameObject m_currentPanel;


    [SerializeField] GameObject[] m_allPanels;
    [SerializeField] private GameObject m_sheepPanel;
    [SerializeField]
    private GameObject m_constructionModeEffect;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance.m_isConstructionModeActive)
        {
            m_constructionModeEffect.SetActive(true);
        }
        else
        {
            m_constructionModeEffect.SetActive(false);
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseCurrentPanel();
            GameManager.Instance.SetConstructionMode(false);
            PauseController.IsPaused(false);
        }
    }
    public void OpenPanel(GameObject panel)
    {
        //if panel equal construction panel
        if(panel == m_constructionPanel)
        {
            GameManager.Instance.SetConstructionMode(!GameManager.Instance.m_isConstructionModeActive);
        }

        //if we click on the same button 
        if (m_currentPanel == panel)
        {
           
            panel.SetActive(false);
            m_panelOutside.SetActive(false);
            GameManager.Instance.SetConstructionMode(false);
            PauseController.IsPaused(false);
            m_currentPanel = null;
            return;
        }
        //if there already a panel open
        if (m_currentPanel != null)
        {
            m_currentPanel.SetActive(false);
            PauseController.IsPaused(false);
            if (panel != m_constructionPanel)
            {
                GameManager.Instance.SetConstructionMode(false);
            }
        }
        PauseController.IsPaused(true);
      
        panel.SetActive(true);
        m_panelOutside.SetActive(true);
        m_currentPanel = panel;

    }
    public void OpenPanelByName(string panel)
    {
        foreach(GameObject p in m_allPanels)
        {
            if(p.name == panel)
            {
                OpenPanel(p);
                return;
            }
        }

       

    }

    public void CloseCurrentPanel()
    {
        PauseController.IsPaused(false);
        
        if (m_currentPanel != null)
        {
            m_currentPanel.SetActive(false);
            m_panelOutside.SetActive(false );
            m_currentPanel = null;
            if (m_currentPanel != m_constructionPanel)
            {
                GameManager.Instance.SetConstructionMode(false);
            }
        }
    }
    public void closeSheepPanel()
    {
        m_sheepPanel.SetActive(false);
        
    }

    public void closeConstructionPanel()
    {
       m_constructionPanel.SetActive(false);
    }
}
