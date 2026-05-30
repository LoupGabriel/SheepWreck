using System.Collections.Generic;
using UnityEngine;

public class ShipSystem : MonoBehaviour
{


    public static ShipSystem Instance;
    [SerializeField] private List<RoomInstance> m_shipCurrentRooms = new();


    private void Awake()
    {
        Instance = this;
    }


    public void AddRoomToList(RoomInstance room)
    {

        m_shipCurrentRooms.Add(room);
    }

}
