using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    //Manager for all selectionMode


    [SerializeField] private Camera m_cam;

    private SheepController currentSelectedSheep = null;
    public RoomInstance m_currentHoveredRoom;
    private Ray m_rayFromCam;

    private void Update()
    {
        m_rayFromCam = m_cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        HandleHover();
        HandleClick();


    }

    private void HandleHover()
    {


        RoomInstance newHoveredRoom = null;
        if (Physics.Raycast(m_rayFromCam, out RaycastHit hit))
        {
            newHoveredRoom = hit.collider.GetComponent<RoomInstance>();
        } 


        if(newHoveredRoom != m_currentHoveredRoom )
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


            RaycastHit[] hits = Physics.RaycastAll(m_rayFromCam);

            //look for sheep
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Sheep"))
                {

                    currentSelectedSheep = hit.collider.gameObject.GetComponent<SheepController>();

                    currentSelectedSheep.Select();
                    currentSelectedSheep.StartGrab();



                    return;

                }
                


            }






        }

        if (Mouse.current.leftButton.isPressed)
        {

            if (currentSelectedSheep != null)
            {
                currentSelectedSheep.StartGrab();
            }



        }


        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (currentSelectedSheep != null)
            {
                currentSelectedSheep.Drop(m_currentHoveredRoom.transform.position);
                currentSelectedSheep = null;
            }


        }
    }


}
