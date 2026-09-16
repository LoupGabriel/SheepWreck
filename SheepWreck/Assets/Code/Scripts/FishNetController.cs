using UnityEngine;
using UnityEngine.InputSystem;

public class FishNetController : MonoBehaviour, ISelectable
{
    private static bool m_isGrab = false;
    private float m_timeSinceClick = 0;
    private float m_timeBeforeDrag = 0.25f;

    private Vector3 m_originalPos;
    private Rigidbody m_rb;
    private void Start()
    {
        m_rb= GetComponent<Rigidbody>();
        m_originalPos = transform.position;
    }
   
    public void Select()
    {

        if (m_isGrab)
        {
            return;
        }

         m_timeSinceClick = 0;



        
       


    }

    public void StartGrab()
    {
        Debug.Log("StartGrab");
        m_timeSinceClick += Time.deltaTime;
        
        //little delay before grabing 
        if (m_timeSinceClick >= m_timeBeforeDrag)
        {

            m_isGrab = true;
           // transform.SetParent(null, true);
            CursorManager.Instance.SetCursorType(ECursorType.SheepGrab);
            

            gameObject.transform.position = GetMousePosition();


        }

    }
    public void Drop()
    {


        Vector3 dropPos = transform.position;
       
        m_originalPos = dropPos;


        gameObject.transform.position = dropPos;

        m_isGrab = false;
        
        m_timeSinceClick = 0;

        CursorManager.Instance.SetCursorType(ECursorType.Default);
     
      
    }
    private Vector3 GetMousePosition()
    {
        Vector3 mousePos;

        mousePos = Mouse.current.position.ReadValue();

        mousePos.z = transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }


    private void UpdateRotation()
    {
        float angle = Mathf.Atan2(m_rb.linearVelocity.y, m_rb.angularVelocity.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }
}
