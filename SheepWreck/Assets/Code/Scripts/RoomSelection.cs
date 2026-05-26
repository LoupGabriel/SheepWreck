using System.Dynamic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomSelection : MonoBehaviour
{



    private Transform m_lastHoveredRoom;
    private MeshRenderer m_lastMeshRender;
    [SerializeField] private GameObject m_constructionModeImage;

    private bool m_constructionMode = false;

    
    private void Update()
    {
        if (m_constructionMode)
        {
            HoveringRoom();
            if (m_constructionModeImage != null)
            {
                m_constructionModeImage.SetActive(true);
            }
            

        }
        else
        {
            m_constructionModeImage.SetActive(false);
        }

    }


    private void HoveringRoom()
    {
        Ray rayfromCamera = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;




        if (Physics.Raycast(rayfromCamera, out hit))
        {


            Transform currentHoveredRoom = hit.collider.transform;

            if (hit.collider.CompareTag("Room"))
            {
                //If its a fresh new room
                if (m_lastHoveredRoom != null && m_lastHoveredRoom != currentHoveredRoom)
                {
                    ResetLastRoomMaterial();
                }


                //Store the last room to disable it when we leave
                if (m_lastMeshRender == null)
                {
                    m_lastHoveredRoom = currentHoveredRoom;
                    m_lastMeshRender = currentHoveredRoom.GetComponent<MeshRenderer>();

                    if (m_lastMeshRender != null)
                    {
                        m_lastMeshRender.enabled = true;
                    }
                }



                return;

            }



        }

        //If we touch nothing
        if (m_lastHoveredRoom != null)
        {

            ResetLastRoomMaterial();

        }


    }

    private void ResetLastRoomMaterial()
    {
        if (m_lastHoveredRoom != null)
        {
            m_lastMeshRender.enabled = false;

        }
        m_lastHoveredRoom = null;
        m_lastMeshRender = null;
    }



    public void NotifyConstructionMode()
    {
        m_constructionMode = !m_constructionMode;

    }


}
