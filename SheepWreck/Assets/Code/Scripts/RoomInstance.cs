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
    [SerializeField] private int m_xpAmount = 1;
    public bool m_isGhost = false;

    private Coroutine m_productionRoutine;
    public int m_currentStoredResources = 0;

    private MeshRenderer m_renderer;

    private bool m_isCurrentRoom = false;

    [SerializeField] private float m_roomProductionTime = 2f;




    public bool IsStorageFull => m_currentStoredResources >= m_roomData.m_capacity;
    private bool m_storageUsed = false;
    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;
        ShipSystem.Instance.AddRoomToList(this);

        if (m_isGhost)
            return;

        if (m_collectIcon != null)
        {
            m_collectIcon.SetActive(false);
        }

        if (m_roomData.m_buildingType == EBuildingType.PRODUCE)
        {
            m_productionRoutine = StartCoroutine(ProductionRoutine());
        }
        else if (m_roomData.m_buildingType == EBuildingType.STOCK)
        {

            Stock(m_roomData.m_capacity, m_roomData.m_ressourceProduced);

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


        if (isEmptyRoom())
            return;

        SetSheepsWorking();

        float totalProduction = 0f;

        foreach (SheepInstance sheep in m_assignedSheep)
        {
            float roomProductionMultiplier = m_roomData.m_productionRate;
            float traitMultiplier = GetMultiplierByTrait(sheep);
            float specialityMultiplier = GetSpecialityMultiplier(sheep);
            float levelMultiplier = GetJobLevelMultipler(sheep);
            totalProduction += sheep.m_productionRate * traitMultiplier * specialityMultiplier * levelMultiplier * roomProductionMultiplier;

        }


        int amount = Mathf.RoundToInt(totalProduction);
        int spaceLeft = m_roomData.m_capacity - m_currentStoredResources;

        m_currentStoredResources += Mathf.Min(amount, spaceLeft);

        GiveXpToSheep();

        if (m_currentStoredResources >= m_roomData.m_capacity)
        {
            m_collectIcon.SetActive(true);
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


    private void Stock(int amount, ERessourceType ressource)
    {
        RessourceSystem.Instance.UpdateMaxCapacity(amount, ressource);
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

    public void SetIsGhost(bool isGhost)
    {
        m_isGhost = isGhost;
    }



    private void GiveXpToSheep()
    {
        ESheepJob job = GetRoomJob();

        foreach (SheepInstance sheep in m_assignedSheep)
        {
            sheep.AddJobXp(job, m_xpAmount);
        }
    }


    private ESheepJob GetRoomJob()
    {
        switch (m_roomData.m_ressourceProduced)
        {
            case ERessourceType.FOOD:
                {
                    return ESheepJob.Farmer;

                }
            case ERessourceType.WATER:
                {
                    return ESheepJob.Farmer;

                }
            case ERessourceType.ENERGY:
                {
                    return ESheepJob.Engineer;

                }

            case ERessourceType.RESEARCH:
                {
                    return ESheepJob.Scientist;
                }
            default: return ESheepJob.Sailor;

        }
    }
    private float GetMultiplierByTrait(SheepInstance sheep)
    {
        switch (sheep.Trait)
        {
            case ESheepTrait.HardWorker:
                return 1.25f;


            case ESheepTrait.Lazy:
                return 0.75f;

            case ESheepTrait.None:
                return 1;

            default: return 1;

        }
    }

    private float GetSpecialityMultiplier(SheepInstance sheep)
    {
        if (sheep.Speciality == ESheepSpeciality.Farmer &&
        m_roomData.m_ressourceProduced == ERessourceType.FOOD)
            return 1.5f;

        if (sheep.Speciality == ESheepSpeciality.Engineer &&
            m_roomData.m_ressourceProduced == ERessourceType.ENERGY)
            return 1.5f;

        if (sheep.Speciality == ESheepSpeciality.BookWorm &&
            m_roomData.m_ressourceProduced == ERessourceType.RESEARCH)
            return 1.5f;

        return 1f;
    }

    private float GetJobLevelMultipler(SheepInstance sheep)
    {
        ESheepJob job = GetRoomJob();
        int level = sheep.GetJobLevel(job);
        return 1 + ((level - 1) * 0.1f);
    }


}
