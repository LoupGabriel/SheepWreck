
using System.Collections.Generic;
using UnityEngine;

public class IslandInstance : MonoBehaviour
{
     public string m_islandName;
    [SerializeField] public Vector2Int m_gridPos;


    public List<Quest> m_availableQuest;

    private void Start()
    {
        m_islandName = $"{m_gridPos.x}/{m_gridPos.y}";
    }
}
