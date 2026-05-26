using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_actionAsset;
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_zoomSensitifity;

    private Camera m_cam;
    [SerializeField] private float minZoom = 30f;
    [SerializeField] private float maxZoom = 70f;

    private float m_targetZoom;
    private InputAction m_moveCam;
    private InputAction m_zoom;
    
    private Vector2 m_movement;
    private Vector2 m_zoomAmount;
    
   


    private void Start()
    {
        m_cam = GetComponent<Camera>();
        m_moveCam = m_actionAsset.FindAction("Move");
        m_zoom = m_actionAsset.FindAction("Zoom");
        m_targetZoom = m_cam.fieldOfView;
    }

    private void Update()
    {
        HandleInput();
        MoveCamera();
        //ZoomCamera();
    }
        
    private void HandleInput()
    {


        m_movement = m_moveCam.ReadValue<Vector2>();
        m_zoomAmount = m_zoom.ReadValue<Vector2>();


    }
   
    
    private void MoveCamera()
    {

        float moveX = m_movement.x * m_movementSpeed * Time.deltaTime;
        float moveY = m_movement.y * m_movementSpeed * Time.deltaTime;
        float moveZ = m_zoomAmount.y * m_zoomSensitifity * Time.deltaTime;

       
        
        
        transform.Translate(moveX,moveY, moveZ);


    }
    private void ZoomCamera()
    {
        //use a small number because wheel make large number
        m_targetZoom -= m_zoomAmount.y * m_zoomSensitifity * 0.05f;
        m_targetZoom = Mathf.Clamp(m_targetZoom, minZoom, maxZoom);
        m_cam.fieldOfView = m_targetZoom;
    }
}
