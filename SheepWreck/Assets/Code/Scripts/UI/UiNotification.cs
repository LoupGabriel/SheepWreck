using TMPro;
using UnityEngine;

public class UiNotification : MonoBehaviour
{
    [SerializeField] private Transform m_container;
    [SerializeField] private GameObject m_notificationEntry;

    public static UiNotification instance;

    private void Awake()
    {
        instance = this;

    }


    public void TriggerNotification(string notification)
    {
        foreach( Transform child in m_container)
        {
            Destroy(child.gameObject);
        }
        GameObject entry = Instantiate(m_notificationEntry, m_container);
        entry.GetComponentInChildren<TMP_Text>().text = notification;

    }


}



