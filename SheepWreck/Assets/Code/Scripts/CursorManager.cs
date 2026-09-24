using UnityEngine;


public enum ECursorType
{
    Default,
    SheepOver,
    SheepGrab,
    InteractUI,
    Fire,
    Construction
}
public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D m_default;
    [SerializeField] private Texture2D m_sheepOver;
    [SerializeField] private Texture2D m_SheepGrab;
    [SerializeField] private Texture2D m_Interact;
    [SerializeField] private Texture2D m_construction;
    [SerializeField] private Texture2D m_fire;


    [SerializeField] private Vector2 clickPosition = Vector2.zero;


    public static CursorManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Cursor.SetCursor(m_default, clickPosition, CursorMode.Auto);
    }


    public void SetCursorType(ECursorType type)
    {
        switch (type)
        {
            case ECursorType.Default:
                Cursor.SetCursor(m_default, clickPosition, CursorMode.Auto);
                break;
            case ECursorType.SheepOver:
                Cursor.SetCursor(m_sheepOver, clickPosition, CursorMode.Auto);
                break;
            case ECursorType.SheepGrab:
                Cursor.SetCursor(m_SheepGrab, clickPosition, CursorMode.Auto);
                break;
            case ECursorType.InteractUI:
                Cursor.SetCursor(m_Interact, clickPosition, CursorMode.Auto);
                break;
            case ECursorType.Construction:
                Cursor.SetCursor(m_construction, clickPosition, CursorMode.Auto);
                break;
            case ECursorType.Fire:
                Cursor.SetCursor(m_fire, clickPosition, CursorMode.Auto);
                break;


        }

    }
}
