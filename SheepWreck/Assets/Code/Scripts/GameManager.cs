using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public float m_currentTimeScale;
   
    public bool m_isConstructionModeActive { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
        m_currentTimeScale = Time.timeScale;
        Time.timeScale = 1;
    }
    private void OnEnable()
    {
        ShipSystem.Instance.OnRoomSwitch += SetConstructionMode;
    }
    private void OnDisable()
    {
        ShipSystem.Instance.OnRoomSwitch -= SetConstructionMode;
    }
    public void SetTimePause()
    {

        Time.timeScale = 0f;
    }
    public void SetNormalTime()
    {
        Time.timeScale = 1f;
    }
    public void SetFasterTime(float timeSpeed)
    {
        Time.timeScale = timeSpeed;
    }




    public void SetConstructionMode(bool active)
    {
        m_isConstructionModeActive = active;
    }
}
