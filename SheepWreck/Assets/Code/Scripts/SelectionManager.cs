using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static RoomData;


public class SelectionManager : MonoBehaviour
{
    //Manager for all selectionMode


    [SerializeField] private Camera m_cam;
    public GameObject m_currentSelectedObject;
    public SheepController m_currentSelectedSheep = null;
    public RoomInstance m_currentHoveredRoom;
    private Ray m_rayFromCam;
    public SheepController m_lastSelectedSheep;


    
    [SerializeField] private float m_doubleClickTime = 0.3f;
    private float m_lastClickTime;

   
    private void Update()
    {
        m_rayFromCam = m_cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        HandleClick();
        if (!PauseController.m_isPaused)
        {
            HandleHover(); 
            HandleDrag();
        }
        HandleReleaseClick();




    }

    private void HandleHover()
    {


        RoomInstance newHoveredRoom = null;
        if (Physics.Raycast(m_rayFromCam, out RaycastHit hit))
        {
            newHoveredRoom = hit.collider.GetComponent<RoomInstance>();
        }


        if (newHoveredRoom != m_currentHoveredRoom)
        {

            if (m_currentHoveredRoom != null)
                m_currentHoveredRoom.SetHover(false);

            m_currentHoveredRoom = newHoveredRoom;

            if (m_currentHoveredRoom != null)
                m_currentHoveredRoom.SetHover(true);


        }




    }
    private void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {



            float timeSinceLastClick = Time.time - m_lastClickTime;

            //construction mode : click on a room switch the room 
            if (GameManager.Instance.m_isConstructionModeActive )
            {
                if(m_currentHoveredRoom != null)
                {

                ShipSystem.Instance.SwitchRoom(m_currentHoveredRoom);
                }
            }


            RaycastHit[] hits = Physics.RaycastAll(m_rayFromCam);


            //calcul time between click
            if (m_currentSelectedSheep == m_lastSelectedSheep &&
                timeSinceLastClick <= m_doubleClickTime)
            {
                HandleDoubleClick();
            }
            if(timeSinceLastClick > m_doubleClickTime)
            {
                m_currentSelectedSheep = null;
            }
            m_lastClickTime = Time.time;

            //look for sheep
            foreach (RaycastHit hit in hits)
            {
                m_currentSelectedObject = hit.collider.gameObject;

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Sheep"))
                {


                    m_currentSelectedSheep = hit.collider.gameObject.GetComponent<SheepController>();
                    m_lastSelectedSheep = m_currentSelectedSheep;
                    m_currentSelectedSheep.Select();
                    




                    return;

                }
                



            }









        }





    }

    private void HandleDoubleClick()
    {

        //focus on selected item
       

        if (m_lastSelectedSheep != null)
        {

            CameraController camControl = m_cam.GetComponent<CameraController>();
            camControl.NotifyFocus(m_currentSelectedObject.transform);

        }


    }

    private void HandleDrag()
    {
        if (Mouse.current.leftButton.isPressed)
        {

            if (m_currentSelectedSheep != null)
            {
                m_currentSelectedSheep.StartGrab();
                m_currentSelectedSheep.m_sheepData.m_assignedRoom.RemoveSheepFromRoom(m_currentSelectedSheep.m_sheepData);
            }



        }
    }
    private void HandleReleaseClick()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (m_currentSelectedSheep != null )
            {
                if (m_currentHoveredRoom != null)
                {
                    m_currentSelectedSheep.Drop(m_currentHoveredRoom.transform.position);
                    m_currentHoveredRoom.AddSheepToRoom(m_currentSelectedSheep.m_sheepData);
                    m_currentSelectedSheep.m_sheepData.SetSheepAssignedRoom(m_currentHoveredRoom);
                }
                else
                {
                    m_currentSelectedSheep.DropSheepInAir();
                }
                    

            }
            else
            {
                return;
            }


        }
    }


  







}
