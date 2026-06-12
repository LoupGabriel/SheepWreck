using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool m_isPaused { get; private set; } = false;



    public static  void IsPaused(bool pause)
    {
        m_isPaused = pause;
    }
}
