using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public float m_currentTimeScale;


    private void Awake()
    {
        Instance = this;
        m_currentTimeScale = Time.timeScale;
        Time.timeScale = 1;
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
}
