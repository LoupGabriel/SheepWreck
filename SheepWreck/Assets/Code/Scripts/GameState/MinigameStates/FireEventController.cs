
using System.Collections.Generic;
using UnityEngine;

public class FireEventController : MonoBehaviour
{
    [SerializeField] private FireObject m_firePrefab;
    [SerializeField] private List<Transform> m_spawnPoint = new List<Transform>();

    private List<FireObject> m_allFire = new List<FireObject>();

    [SerializeField] private float m_eventDuration = 10f;

    public float Duration => m_eventDuration;
    public List<FireObject> AllFire => m_allFire;
    public void SpawnFire()
    {
        foreach(RoomInstance room in ShipSystem.Instance.m_shipCurrentRooms)
        {
            //take a random spawn point of the room
            //spawn the fire
            m_spawnPoint.Add(room.transform);
        }
        foreach(Transform t in m_spawnPoint)
        {
            FireObject fire = Instantiate(m_firePrefab, t.position,Quaternion.identity);
            m_allFire.Add(fire);
        }
    }
}
