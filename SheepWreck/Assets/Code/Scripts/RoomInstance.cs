using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomInstance : MonoBehaviour, ISelectable
{
    //This is a Room instance 


    [SerializeField] public RoomData m_roomData;

    [SerializeField] public List<SheepInstance> m_assignedSheep = new();

    [SerializeField] public Transform m_sheepSlot;


    private MeshRenderer m_renderer;

    public bool m_isCurrentRoom = false;

    [SerializeField] private float m_roomProductionTime = 2f;
    private float m_roomTimeSinceLastProduce = 0;
    
    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;
        ShipSystem.Instance.AddRoomToList(this);

       
    }
    private void Update()
    {
        if(m_assignedSheep.Count <= 0)
        {
            return;
        }
        Produce();
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


    // Produce X ressource per sheep each m_roomProductionTime
    private void Produce()
    {

        m_roomTimeSinceLastProduce += Time.deltaTime;

        if(m_roomTimeSinceLastProduce >= m_roomProductionTime)
        {
            foreach (SheepInstance sheep in m_assignedSheep)
            {
                float amount = sheep.m_productionRate;
                RessourceSystem.Instance.AddRessource((int)(amount), m_roomData);
            }

            m_roomTimeSinceLastProduce = 0;

        }
       
        
    }

   
}
