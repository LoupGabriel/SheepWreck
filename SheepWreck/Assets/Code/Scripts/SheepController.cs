using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SheepController : MonoBehaviour, ISelectable
{





    public SheepState m_sheepState;
    public SheepInstance m_sheepData;
    private UiSheepPanel m_sheepPanel;

    private SpriteRenderer m_sheepRenderer;

    private Animator m_animator;
    private static bool m_isGrab = false;

    private Vector3 m_originalPos;

    private float m_timeSinceClick = 0;
    private float m_timeBeforeDrag = 0.25f;

    private bool m_sfxPlayed = false;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_sheepPanel = FindFirstObjectByType<UiSheepPanel>(FindObjectsInactive.Include);
        m_sheepData = GetComponent<SheepInstance>();

    }


    private void Start()
    {
        m_sheepRenderer = GetComponent<SpriteRenderer>();
        m_animator.SetBool("isGrab", m_isGrab);
        SetParent();
        if (m_sheepData != null)
        {
            m_sheepState = m_sheepData.m_stateMachine.m_currentSheepState;
        }
      
        m_originalPos = transform.position;
       
    }



    /// <summary>
    /// Automaticly set sheep parent for cleaner project
    /// </summary>
    private void SetParent()
    {
        GameObject parent = GameObject.Find("Sheeps");
        gameObject.transform.SetParent(parent.transform);
    }




    //Get the current mouse position in world space
    private Vector3 GetMousePosition()
    {
        Vector3 mousePos;

        mousePos = Mouse.current.position.ReadValue();

        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }


    


    /// <summary>
    /// ISelectable implementation
    /// </summary>
    public void Select()
    {

        if (m_isGrab)
        {
            return;
        }

        if (m_sheepPanel == null || m_sheepData == null)
        {
            
            return;
        }
        m_timeSinceClick = 0;


        
        m_sheepPanel.EnableSheepPanel(m_sheepData.m_sheepName, m_sheepData);
        m_sheepRenderer.material.SetFloat("_oulineOn", 1);


    }

    public void StartGrab()
    {
        
        m_timeSinceClick += Time.deltaTime;
        m_sheepData.SetIdle();
        //little delay before grabing 
        if (m_timeSinceClick >= m_timeBeforeDrag)
        {

            m_isGrab = true;
            CursorManager.Instance.SetCursorType(ECursorType.SheepGrab);
            if (!m_sfxPlayed)
            {
                SfxManager.PlaySfx("Sheep");
                m_sfxPlayed = true;
            }

            m_animator.SetBool("isGrab", m_isGrab);

            gameObject.transform.position = GetMousePosition();


        }

    }


    /// <summary>
    /// Add sheep to the new room 
    /// </summary>
    /// <param name="room">new room</param>
    public void Drop(Vector3 room)
    {


        Vector3 dropPos = transform.position;
        dropPos.y = room.y + 0.1f;
        m_originalPos = dropPos;


        gameObject.transform.position = dropPos;

        m_isGrab = false;
        m_animator.SetBool("isGrab", m_isGrab);
        m_timeSinceClick = 0;
        m_sfxPlayed = false;

        m_sheepRenderer.material.SetFloat("_oulineOn", 0);
    }

    /// <summary>
    /// if the sheep is drop in an invalid position
    /// </summary>
    public void DropSheepNotValid()
    {
        SfxManager.PlaySfx("Error");
        m_isGrab = false;
        transform.position = m_originalPos;
        m_animator.SetBool("isGrab", m_isGrab);

    }


    public void SetHover(bool isHovered)
    {
       
            m_sheepRenderer.material.SetFloat("_oulineOn", isHovered ? 1:0);
        
    }





}
