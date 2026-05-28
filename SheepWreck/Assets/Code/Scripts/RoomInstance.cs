using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    [SerializeField] private  RoomData m_roomData;

    [SerializeField] public List<SheepInstance> m_assignedSheep = new();
}
