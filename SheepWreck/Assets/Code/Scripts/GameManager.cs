using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState m_currentState;
    public float m_currentTimeScale;

    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private SheepRescueController m_rescueController;
    [SerializeField] private SheepWreckController m_sheepWreckController;
    [SerializeField] private FireEventController m_fireEventController;
    public bool m_isConstructionModeActive { get; private set; } = false;


    public GameState CurrentGameState => m_currentState;

    public FireEventController FireEventController => m_fireEventController;
    public SheepWreckController SheepWreckController => m_sheepWreckController;
    public SheepRescueController SheepRescueController => m_rescueController;
    private void Awake()
    {
        Instance = this;
        m_currentTimeScale = Time.timeScale;
        Time.timeScale = 1;
        m_gameOverPanel.SetActive(false);
        m_currentState = new GameManagementState(this);
       
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

    private void Update()
    {
        m_currentState.Update();
    }

    /// <summary>
    /// Set a custom Time float
    /// </summary>
    /// <param name="timeSpeed">new speed (Time.timescale)</param>
    public void SetCustomTime(float timeSpeed)
    {
        Time.timeScale = timeSpeed;
    }


    /// <summary>
    /// Set construction mode 
    /// </summary>
    /// <param name="active"></param>
    public void SetConstructionMode(bool active)
    {
        m_isConstructionModeActive = active;
    }

    //Open the game over menu
    public void GameOver()
    {
        PauseController.IsPaused(true);
        m_gameOverPanel.SetActive(true);
    }


    public void  ChangeState(GameState newState)
    {
        m_currentState?.Exit();
        m_currentState = newState;
        m_currentState.Enter();
    }
   
  
    
}

