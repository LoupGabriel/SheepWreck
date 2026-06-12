using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShipSystem : MonoBehaviour
{


    public static ShipSystem Instance;
    [SerializeField] public List<RoomInstance> m_shipCurrentRooms = new();
    [SerializeField] private Transform m_roomParent;
    private GameObject m_tempRoomPrefab;
    public Action<bool> OnRoomSwitch;
    private void Awake()
    {
        Instance = this;
    }


    public void AddRoomToList(RoomInstance room)
    {

        m_shipCurrentRooms.Add(room);
    }
    public void RemoveRoomFromList(RoomInstance room)
    {
        m_shipCurrentRooms.Remove(room);
    }



    public void SwitchRoom(RoomInstance currentRoom)
    {
        RoomInstance temp = currentRoom;
        Destroy(currentRoom.gameObject);
        GameObject room = Instantiate(m_tempRoomPrefab, temp.gameObject.transform.position, temp.gameObject.transform.rotation, m_roomParent);
        OnRoomSwitch?.Invoke(false);

    }

    public void SetRoomPrefab(GameObject roomPrefab)
    {
        m_tempRoomPrefab = roomPrefab;


    }
}
