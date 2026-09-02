using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class CameraController : MonoBehaviour
{
    //Control the movement of the camera

    
    [SerializeField] private InputActionAsset m_actionAsset;
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_zoomSensitifity;

    private Vector3 m_sheepAnchor = new Vector3(0, 0.65f, -5f);//Camera offset at the sheep
    private Vector3 m_originalPos;

    //island vars
    [SerializeField] Vector3 m_islandView;
    [SerializeField] float m_islandTransitionDuration = 2f;   

    

    private InputAction m_moveCam;
    private InputAction m_zoom;
    private InputAction m_resetCam;
    
    private Vector2 m_movement;
    private Vector2 m_zoomAmount;
    private bool m_reset;
    public bool m_wasFocus = false;
   


    private void Start()
    {
        m_originalPos= transform.position;
        m_moveCam = m_actionAsset.FindAction("Move");
        m_zoom = m_actionAsset.FindAction("Zoom");
        m_resetCam = m_actionAsset.FindAction("Reset");
        
    }

    private void Update()
    {
        HandleInput();
        if (!PauseController.m_isPaused)
        {
            MoveCamera();
        }
       
        if (m_reset)
        {
            ResetCamera();
        }
     
    }
        
    private void HandleInput()
    {


        m_movement = m_moveCam.ReadValue<Vector2>();
        m_zoomAmount = m_zoom.ReadValue<Vector2>();
        m_reset = m_resetCam.WasPerformedThisFrame();

    }

    /// <summary>
    /// Move camera sideways with wasd
    /// </summary>
    private void MoveCamera()
    {

        float moveX = m_movement.x * m_movementSpeed * Time.deltaTime;
        float moveY = m_movement.y * m_movementSpeed * Time.deltaTime;
        float moveZ = m_zoomAmount.y * m_zoomSensitifity * Time.deltaTime;




        transform.Translate(moveX, moveY, moveZ);

     

    }
    /// <summary>
    /// Reset camera at start position
    /// </summary>
    public void ResetCamera()
    {

        transform.position = m_originalPos;
    }


    

    //notify focus bool
    public void NotifyFocus(Transform focusCameraPos)
    {
        m_wasFocus = true;
        transform.position = focusCameraPos.position + m_sheepAnchor;
    }
    


    public void StartIslandView()
    {
        transform.position = m_islandView;
    }


 
}
