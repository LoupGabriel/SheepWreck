using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomInstance : MonoBehaviour, ISelectable
{
    //This is a Room instance 


    [SerializeField] private RoomData m_roomData;

    [SerializeField] public List<SheepInstance> m_assignedSheep = new();

    [SerializeField] public Transform m_sheepSlot;


    private MeshRenderer m_renderer;

    public bool m_isCurrentRoom = false;

    
    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;
        ShipSystem.Instance.AddRoomToList(this);
    }

    public void AddSheepToRoom(SheepInstance sheepInstance)
    {

       m_assignedSheep.Add(sheepInstance);


    }

    public void SetHover(bool isHovering)
    {
        //Debug
        //m_renderer.enabled = isHovering;
        //m_renderer.material.color = new Color(1, 1, 1, 0.05f);


        m_isCurrentRoom = isHovering;




    }


    public void Select()
    {
        Debug.Log("RoomSelected");



    }
}
