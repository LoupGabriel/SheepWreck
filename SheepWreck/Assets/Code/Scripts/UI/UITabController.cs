using UnityEngine;
using UnityEngine.UI;
public class UITabController : MonoBehaviour
{
    [SerializeField] private Image[] m_tabsImage;
    [SerializeField] private GameObject[] m_pages;


    private void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabsIndex)
    {


        for (int i = 0; i < m_pages.Length; i++)
        {
            m_pages[i].SetActive(false);
            m_tabsImage[i].color = Color.grey;

        }

        m_pages[tabsIndex].SetActive(true);
        m_tabsImage[tabsIndex].color = Color.white;
    }
}



