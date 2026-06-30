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
        CheatManager.Instance.OnChangeSpeed += SetCustomTime;
    }
    private void Start()
    {
        SoundtrackManager.Instance.PlayMusic("MainMusic");
    }
    private void OnDisable()
    {
        ShipSystem.Instance.OnRoomSwitch -= SetConstructionMode;
        CheatManager.Instance.OnChangeSpeed -= SetCustomTime;
    }
    public void SetTimePause()
    {

        Time.timeScale = 0f;
    }
    public void SetNormalTime()
    {
        Time.timeScale = 1f;
    }

   
    /// <summary>
    /// Set a custom Time float
    /// </summary>
    /// <param name="timeSpeed">new speed (Time.timescale)</param>
    public void SetCustomTime(float timeSpeed)
    {
        Time.timeScale = timeSpeed;
    }



    public void SetConstructionMode(bool active)
    {
        m_isConstructionModeActive = active;
    }
}
