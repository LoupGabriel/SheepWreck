using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static RoomData;

public class RoomInstance : MonoBehaviour, ISelectable
{
    //This is a Room instance 


    [SerializeField] public RoomData m_roomData;

    [SerializeField] public List<SheepInstance> m_assignedSheep = new();

    [SerializeField] public Transform m_sheepSlot;

    [SerializeField] private GameObject m_collectIcon;

    private Coroutine m_productionRoutine;
    public int m_currentStoredResources = 0;

    private MeshRenderer m_renderer;

    public bool m_isCurrentRoom = false;

    [SerializeField] private float m_roomProductionTime = 2f;
   



    public bool IsStorageFull => m_currentStoredResources >= m_roomData.m_capacity;

    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;
        ShipSystem.Instance.AddRoomToList(this);
        m_productionRoutine = StartCoroutine(ProductionRoutine());

        if(m_collectIcon != null)
        {
            m_collectIcon.SetActive(false);
        }
      
    }
    private void OnDestroy()
    {
        ShipSystem.Instance.RemoveRoomFromList(this);
    }

    private void OnDisable()
    {
        if (m_productionRoutine != null)
        {
            StopCoroutine(m_productionRoutine);
        }
    }
    public void AddSheepToRoom(SheepInstance sheepInstance)
    {
        if (maxSheepReach() != true)
        {
            m_assignedSheep.Add(sheepInstance);
        }



    }
    public void RemoveSheepFromRoom(SheepInstance sheepInstance)
    {

        m_assignedSheep.Remove(sheepInstance);

    }

    public void SetHover(bool isHovering)
    {

        m_isCurrentRoom = isHovering;

    }

    //Iselectable implementation
    public void Select()
    {
        if (m_currentStoredResources >= m_roomData.m_capacity && m_roomData.m_capacity != 0)
        {
            CollectRessource();
        }
           

       
      

    }


    // Produce X ressource per sheep each m_roomProductionTime
    private void Produce()
    {
      
        
        if (IsStorageFull)
            return;
        if (isEmptyRoom()) return;

        SetSheepsWorking();

        foreach (SheepInstance sheep in m_assignedSheep)
        {
         
          

            int amount = Mathf.RoundToInt(sheep.m_productionRate);
            int spaceLeft = m_roomData.m_capacity - m_currentStoredResources;

            if (spaceLeft <= 0)
                break;


            m_currentStoredResources += Mathf.Min(amount, spaceLeft);
            if(m_currentStoredResources >= m_roomData.m_capacity)
            {
                m_collectIcon.SetActive(true);
            }
        }


    }



    private void CollectRessource()
    {

        SfxManager.PlaySfx("Coin");
        if (m_currentStoredResources == 0)
            return;

        RessourceSystem.Instance.GainRessource(m_currentStoredResources, m_roomData.m_ressourceProduced);
        m_currentStoredResources = 0;
        m_collectIcon.SetActive(false);
    }

   
    private void SetSheepsWorking()
    {

      
        foreach (SheepInstance sheep in m_assignedSheep)
        {

            sheep.RequestWork();

        }


    }



    private bool isEmptyRoom()
    {

        return m_roomData.m_buildingType == EBuildingType.EMPTY;


    }


    public bool maxSheepReach()
    {

        return m_assignedSheep.Count >= m_roomData.m_maxSheepCapacity;

    }


    private IEnumerator ProductionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_roomProductionTime);

            if (m_assignedSheep.Count > 0 && !isEmptyRoom())
            {
                Produce();
            }
        }
    }

}
