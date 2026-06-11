using UnityEngine;
using UnityEngine.InputSystem;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_menuPanel;


    private void Start()
    {
        m_menuPanel.SetActive(false);
    }


    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
           
            m_menuPanel.SetActive(!m_menuPanel.activeSelf);
        }
    }
}
