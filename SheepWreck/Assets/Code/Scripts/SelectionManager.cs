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
    public RoomInstance m_currentSelectedRoom = null;
    public RoomInstance m_currentHoveredRoom;
    private Ray m_rayFromCam;
    public SheepController m_lastSelectedSheep;
    public SheepController m_currentHoverSheeep;


    [SerializeField] private GameObject m_vfx;



    [SerializeField] private float m_doubleClickTime = 0.3f;
    private float m_lastClickTime;


    private FishNetController m_fishNetController;
    private FireObject m_currentfireObject;
    private void Start()
    {
        CheatManager.Instance.OnKillSheep += KillCurrentSheep;
    }

    private void OnDestroy()
    {
        CheatManager.Instance.OnKillSheep -= KillCurrentSheep;
    }
    private void Update()
    {

        m_rayFromCam = m_cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (PauseController.m_isPaused)
            return;
        HandleClick();
        if (!PauseController.m_isPaused)
        {
            HandleHover();
            HandleDrag();
        }
        HandleReleaseClick();




    }

    /// <summary>
    /// Handle when the mouse ray touch a collider
    /// </summary>
    private void HandleHover()
    {


        RoomInstance newHoveredRoom = null;

        SheepController newHoveredSheep = null;

        RaycastHit[] hits = Physics.RaycastAll(m_rayFromCam);


        foreach (RaycastHit hit in hits)
        {
            if (GameManager.Instance.CurrentGameState is SheepwreckState)
            {
                m_fishNetController = hit.collider.GetComponentInParent<FishNetController>();
                CursorManager.Instance.SetCursorType(ECursorType.SheepOver);
                Debug.Log("hover fishnet");
            }
            else if (GameManager.Instance.CurrentGameState is FireEventState)
            {
                m_currentfireObject = hit.collider.GetComponentInParent<FireObject>();
                if (m_currentfireObject != null)
                {
                    CursorManager.Instance.SetCursorType(ECursorType.Fire);
                }
                Debug.Log("hover fire");
            }

            SheepController sheep = hit.collider.GetComponentInParent<SheepController>();

            RoomInstance room = hit.collider.GetComponentInParent<RoomInstance>();

            if (newHoveredSheep == null && sheep != null)
            {
                newHoveredSheep = sheep;
            }
            if (newHoveredRoom == null && room != null)
            {
                newHoveredRoom = room;
            }

            if (newHoveredSheep != null && newHoveredRoom != null)
            {
                break;
            }
        }



        UpdateHoveredSheep(newHoveredSheep);
        UpdateHoveredRoom(newHoveredRoom);


    }

    private void UpdateHoveredSheep(SheepController newHoveredSheep)
    {
        if (newHoveredSheep == m_currentHoverSheeep)
        {
            return;
        }

        if (m_currentHoverSheeep != null)
        {
            m_currentHoverSheeep.SetHover(false);
            CursorManager.Instance.SetCursorType(ECursorType.Default);
        }

        m_currentHoverSheeep = newHoveredSheep;

        if (m_currentHoverSheeep != null)
        {
            m_currentHoverSheeep.SetHover(true);
            CursorManager.Instance.SetCursorType(ECursorType.SheepOver);
        }
    }

    private void UpdateHoveredRoom(RoomInstance newHoveredRoom)
    {
        if (newHoveredRoom == m_currentHoveredRoom)
        {
            return;
        }

        if (m_currentHoveredRoom != null)
        {
            m_currentHoveredRoom.SetHover(false);

        }

        m_currentHoveredRoom = newHoveredRoom;

        if (m_currentHoveredRoom != null)
        {
            m_currentHoveredRoom.SetHover(true);
            if (GameManager.Instance.m_isConstructionModeActive)
            {
                CursorManager.Instance.SetCursorType(ECursorType.Construction);
            }
            else
            {
                CursorManager.Instance.SetCursorType(ECursorType.Default);
            }
        }
    }





    /// <summary>
    /// Handle when the mouse click was pressed
    /// </summary>
    private void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {



            float timeSinceLastClick = Time.time - m_lastClickTime;
            if (GameManager.Instance.CurrentGameState is SheepwreckState)
            {
                if (m_fishNetController != null)
                {
                   // m_fishNetController.Select();
                }

            }
            else if (GameManager.Instance.CurrentGameState is FireEventState)
            {
                if (m_currentfireObject != null)
                {
                    m_currentfireObject.DestroyFire();
                }
            }

            //construction mode : click on a room switch the room 
            if (GameManager.Instance.m_isConstructionModeActive)
            {
                if (m_currentHoveredRoom != null)
                {
                    SfxManager.PlaySfx("Construction");
                    ShipSystem.Instance.SwitchRoom(m_currentHoveredRoom);
                    GameObject Vfx = Instantiate(m_vfx, m_currentHoveredRoom.transform.position, Quaternion.identity);
                }
            }
           


            RaycastHit[] hits = Physics.RaycastAll(m_rayFromCam);


            //calcul time between click
            if (m_currentSelectedSheep == m_lastSelectedSheep &&
                timeSinceLastClick <= m_doubleClickTime)
            {
                HandleDoubleClick();
            }
            if (timeSinceLastClick > m_doubleClickTime)
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
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Room"))
                {
                    m_currentSelectedRoom = hit.collider.gameObject.GetComponent<RoomInstance>();
                    m_currentSelectedRoom.Select();
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
            if (GameManager.Instance.CurrentGameState is SheepwreckState)
            {
                if (m_fishNetController != null)
                {
                 //   m_fishNetController.StartGrab();
                }

            }
            


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
            if (m_fishNetController != null)
            {
              //  m_fishNetController.Drop();
                m_fishNetController = null;
                CursorManager.Instance.SetCursorType(ECursorType.Default);

            }
            if (m_currentSelectedSheep != null)
            {
                if (m_currentHoveredRoom != null && !m_currentHoveredRoom.maxSheepReach())
                {
                    m_currentSelectedSheep.Drop(m_currentHoveredRoom.transform.position);

                    m_currentHoveredRoom.AddSheepToRoom(m_currentSelectedSheep.m_sheepData);
                    m_currentSelectedSheep.m_sheepData.SetSheepAssignedRoom(m_currentHoveredRoom);
                }
                else
                {
                    m_currentSelectedSheep.DropSheepNotValid();
                }


            }
            else
            {
                return;
            }


        }
    }


    /// <summary>
    /// Cheat Manager button
    /// </summary>
    private void KillCurrentSheep()
    {
        if (m_lastSelectedSheep != null)
        {
            Destroy(m_lastSelectedSheep.gameObject);
        }

    }










}
