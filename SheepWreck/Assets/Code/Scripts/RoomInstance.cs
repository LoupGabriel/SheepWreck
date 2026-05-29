using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomInstance : MonoBehaviour,ISelectable
{
    [SerializeField] private RoomData m_roomData;

    [SerializeField] public List<SheepInstance> m_assignedSheep = new();

    [SerializeField] public Transform m_sheepSlot;


    private MeshRenderer m_renderer;

    public bool m_isCurrentRoom = false;


    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;

    }



    public void SetHover(bool isHovering)
    {

        m_renderer.enabled = isHovering;

        if (isHovering)
        {
            m_renderer.material.color = new Color(1, 1, 1, 0.05f);
            m_isCurrentRoom = isHovering;

        }


    }
   
 
    public void Select()
    {
        Debug.Log("RoomSelected");



    }
}
