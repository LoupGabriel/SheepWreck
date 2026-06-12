using UnityEngine;

public class UiConstructionPanel : MonoBehaviour
{
    [SerializeField] private GameObject m_contructionPanel;




    public void ActiveConstructionPanel()
    {
        PauseController.IsPaused(true);
        m_contructionPanel.SetActive(!m_contructionPanel.activeSelf);
    }
}
