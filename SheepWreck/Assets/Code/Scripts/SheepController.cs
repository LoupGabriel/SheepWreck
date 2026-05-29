using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SheepController : MonoBehaviour,ISelectable
{






    private SheepInstance m_sheepData;
    private UiSheepPanel m_sheepPanel;

    private Animator m_animator;
    private static bool m_isGrab = false;

    private Vector3 m_originalPos;

    private float m_timeSinceClick = 0;
    private float m_timeBeforeDrag = 0.15f;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_originalPos = transform.localPosition;
        m_sheepData = GetComponent<SheepInstance>();
        m_sheepPanel = FindFirstObjectByType<UiSheepPanel>();
        m_animator.SetBool("isGrab", m_isGrab);

    }



 

 




    //Get the current mouse position in world space
    private Vector3 GetMousePosition()
    {
        Vector3 mousePos;

        mousePos = Mouse.current.position.ReadValue();

        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

   


   

    public void Select()
    {

        if (m_isGrab)
        {
            return;
        }

        m_timeSinceClick = 0;
        m_sheepPanel.EnableSheepPanel(m_sheepData.m_sheepName);


    }

    public void StartGrab()
    {

        m_timeSinceClick += Time.deltaTime;

        if (m_timeSinceClick >= m_timeBeforeDrag)
        {
            
            m_isGrab = true;
            m_animator.SetBool("isGrab", m_isGrab);

            gameObject.transform.position = GetMousePosition();


        }

    }

    public void Drop(Vector3 roomY)
    {
        Vector3 dropPos = transform.position;
        dropPos.y= roomY.y + 0.1f;


        gameObject.transform.position = dropPos ;


        m_isGrab = false;
        m_animator.SetBool("isGrab", m_isGrab);
        m_timeSinceClick = 0;


    }



}
