using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Idle,
        Construction,
    }

    private GameState m_state = GameState.Idle;

    public void NotifyConstructionMode()
    {
        m_state = GameState.Construction;

    }
}
