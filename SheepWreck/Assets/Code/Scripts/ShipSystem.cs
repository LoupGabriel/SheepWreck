using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShipSystem : MonoBehaviour
{


    public static ShipSystem Instance;
    [SerializeField] public List<RoomInstance> m_shipCurrentRooms = new();
    [SerializeField] private Transform m_roomParent;
    [SerializeField] private Material m_ghostMaterial;
    [SerializeField] private SelectionManager m_selectionManager;
    private GameObject m_tempRoomPrefab;
    private GameObject m_ghostRoom;
    
    public Action<bool> OnRoomSwitch;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!GameManager.Instance.m_isConstructionModeActive || m_ghostRoom == null || m_selectionManager.m_currentHoveredRoom == null)
        {
            return;
        }


        
        GameObject hoveredTransform = m_selectionManager.m_currentHoveredRoom.gameObject;

        m_ghostRoom.transform.position = hoveredTransform.gameObject.transform.position;
        m_ghostRoom.transform.rotation = hoveredTransform.gameObject.transform.rotation;
    }
    public void AddRoomToList(RoomInstance room)
    {

        m_shipCurrentRooms.Add(room);
    }
    public void RemoveRoomFromList(RoomInstance room)
    {
        m_shipCurrentRooms.Remove(room);
    }


    /// <summary>
    /// Change the current hovered room with the select one
    /// </summary>
    /// <param name="currentRoom">current hovered room</param>
    public void SwitchRoom(RoomInstance currentRoom)
    {
        RoomInstance temp = currentRoom;
        Destroy(currentRoom.gameObject);
        GameObject room = Instantiate(m_tempRoomPrefab, temp.gameObject.transform.position, temp.gameObject.transform.rotation, m_roomParent);
        OnRoomSwitch?.Invoke(false);
        StopConstructionMode();
    }


    
    public void SetRoomPrefab(GameObject roomPrefab)
    {
        m_tempRoomPrefab = roomPrefab;


        if(m_ghostRoom != null)
        {
            Destroy(m_ghostRoom);
        }

        
        m_ghostRoom = Instantiate(roomPrefab);

        foreach(MeshRenderer renderer in m_ghostRoom.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = m_ghostMaterial;
        }

    }

    public void StopConstructionMode()
    {
        if(m_ghostRoom != null)
        {
            Destroy(m_ghostRoom);
            m_ghostRoom = null;
        }
    }
}
