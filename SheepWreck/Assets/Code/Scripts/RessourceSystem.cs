
using System;
using System.Collections.Generic;
using UnityEngine;


public enum ERessourceType
{
    GOLD,
    FOOD,
    WATER,
    MORALE,
    ENERGY,
    RESEARCH

}

public class RessourceSystem : MonoBehaviour
{

    public static RessourceSystem Instance;

    public Dictionary<ERessourceType, int> m_ressourceDictionary = new Dictionary<ERessourceType, int>();

    public Action<ERessourceType> OnRessourceChange;
    public Action<ERessourceType, int> OnRessourceAdded;

    [SerializeField]
    private int m_maxGoldStock = 250;

    [SerializeField]
    private int m_maxFoodStock = 100;

    [SerializeField]
    private int m_maxWaterStock = 100;

    [SerializeField]
    private int m_maxEnergy = 100;

    [SerializeField]
    private int m_maxResearch = 100;

    private int m_currentNumberOfSheep = 15;
    [SerializeField]
    private int m_foodConsumptionBySheep = 2;
    private int m_WaterConsumptionBySheep = 5;

    private ShipSystem m_shipSystem;
    public int m_totalUpkeep;

    [SerializeField] private UIRessources m_ressourcesUI;

    public int GetRessource(ERessourceType type)
    {
        int amount = 0;
        switch (type)
        {
            case ERessourceType.GOLD:
                amount = m_maxGoldStock;

                break;

            case ERessourceType.FOOD:
                amount = m_maxFoodStock;
                break;

            case ERessourceType.WATER:
                amount = m_maxWaterStock;
                break;
            case ERessourceType.ENERGY:
                amount = m_maxEnergy;
                break;
            case ERessourceType.RESEARCH:
                amount = m_maxResearch;
                break;

        }
        return amount;
    }
    private void Awake()
    {
        Instance = this;
        InitializeDictionnary();
        m_shipSystem = ShipSystem.Instance;

    }

    private void Start()
    {
        if (TimeManager.Instance == null) return;
        TimeManager.Instance.OnDayPast += GlobalFoodConsumption;
        TimeManager.Instance.OnDayPast += GlobalWaterConsumption;
        TimeManager.Instance.OnWeekPast += UpkeepPayment;
        CheatManager.Instance.OnAddRessource += GainRessource;
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance == null) return;
        TimeManager.Instance.OnDayPast -= GlobalFoodConsumption;
        TimeManager.Instance.OnDayPast -= GlobalWaterConsumption;
        TimeManager.Instance.OnWeekPast -= UpkeepPayment;
        CheatManager.Instance.OnAddRessource -= GainRessource;

    }
    private void Update()
    {

        m_currentNumberOfSheep = CrewManager.Instance.m_currentSheepOnBoard.Count;


    }

    /// <summary>
    /// Consume food each days
    /// </summary>
    /// <param name="months">action param</param>
    /// <param name="weeks">action param</param>
    /// <param name="days">action param</param>
    private void GlobalFoodConsumption(int months, int weeks, int days)
    {
        if (m_ressourceDictionary[ERessourceType.FOOD] <= 0)
            return;


        int totalFoodConsumption = 0;


        foreach (SheepInstance sheep in CrewManager.Instance.m_currentSheepOnBoard)
        {
            float multiplier = GetConsumptionMultiplierByTrait(sheep);
            int amount = Mathf.RoundToInt(m_foodConsumptionBySheep * multiplier);
            totalFoodConsumption += amount;

            sheep.ConsumeFood(amount);
        }

        // current food minus food consumption time number of sheep

        m_ressourceDictionary[ERessourceType.FOOD] -= totalFoodConsumption;
        m_ressourceDictionary[ERessourceType.FOOD] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.FOOD], 0, m_maxFoodStock);


        //notify Hud

        OnRessourceChange?.Invoke(ERessourceType.FOOD);




    }

    /// <summary>
    /// Consume water each days
    /// </summary>
    /// <param name="months">action param</param>
    /// <param name="weeks">action param</param>
    /// <param name="days">action param</param>
    private void GlobalWaterConsumption(int months, int weeks, int days)
    {

        if (m_ressourceDictionary[ERessourceType.WATER] != 0)
        {
            // current water minus water consumption time number of sheep


            m_ressourceDictionary[ERessourceType.WATER] -= m_WaterConsumptionBySheep * m_currentNumberOfSheep;
            //clamp the value at 0 
            m_ressourceDictionary[ERessourceType.WATER] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.WATER], 0, m_maxWaterStock);



            //notify Hud
            OnRessourceChange?.Invoke(ERessourceType.WATER);


            //call ConsumeWater for each sheep
            foreach (SheepInstance sheep in CrewManager.Instance.m_currentSheepOnBoard)
            {
                sheep.ConsumeWater(m_WaterConsumptionBySheep);
            }
        }

    }


    /// <summary>
    /// Consume gold each week for each room
    /// </summary>
    /// <param name="months">action param</param>
    /// <param name="weeks">action param</param>
    /// <param name="days">action param</param>
    private void UpkeepPayment(int months, int weeks, int days)
    {


        foreach (RoomInstance room in m_shipSystem.m_shipCurrentRooms)
        {

            m_totalUpkeep += room.m_roomData.m_upkeepCost;
        }


        m_ressourceDictionary[ERessourceType.GOLD] -= m_totalUpkeep;
        m_ressourceDictionary[ERessourceType.GOLD] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.GOLD], 0, m_maxGoldStock);

        //notify Hud
        OnRessourceChange?.Invoke(ERessourceType.GOLD);

        //reset upkeep
        m_totalUpkeep = 0;



    }

    /// <summary>
    /// Add or remove Ressource from the dictionnary key
    /// </summary>
    /// <param name="ressourceAmount">amount</param>
    /// <param name="type">Key ( EressourceType</param>
    public void GainRessource(int ressourceAmount, ERessourceType type)
    {


        switch (type)
        {
            case ERessourceType.GOLD:

                type = ERessourceType.GOLD;
                m_ressourceDictionary[ERessourceType.GOLD] += ressourceAmount;
                m_ressourceDictionary[ERessourceType.GOLD] =
                Mathf.Clamp(m_ressourceDictionary[ERessourceType.GOLD], 0, m_maxGoldStock);
                break;

            case ERessourceType.FOOD:
                type = ERessourceType.FOOD;
                m_ressourceDictionary[ERessourceType.FOOD] += ressourceAmount;
                m_ressourceDictionary[ERessourceType.FOOD] =
                Mathf.Clamp(m_ressourceDictionary[ERessourceType.FOOD], 0, m_maxFoodStock);
                break;

            case ERessourceType.WATER:
                type = ERessourceType.WATER;
                m_ressourceDictionary[ERessourceType.WATER] += ressourceAmount;
                m_ressourceDictionary[ERessourceType.WATER] =
               Mathf.Clamp(m_ressourceDictionary[ERessourceType.WATER], 0, m_maxWaterStock);
                break;
            case ERessourceType.ENERGY:
                type = ERessourceType.ENERGY;
                m_ressourceDictionary[ERessourceType.ENERGY] += ressourceAmount;
                m_ressourceDictionary[ERessourceType.ENERGY] =
             Mathf.Clamp(m_ressourceDictionary[ERessourceType.ENERGY], 0, m_maxEnergy);
                break;
            case ERessourceType.RESEARCH:
                type = ERessourceType.RESEARCH;
                m_ressourceDictionary[ERessourceType.RESEARCH] += ressourceAmount;
                m_ressourceDictionary[ERessourceType.RESEARCH] =
             Mathf.Clamp(m_ressourceDictionary[ERessourceType.RESEARCH], 0, m_maxResearch);
                break;

        }
        OnRessourceChange?.Invoke(type);
        OnRessourceAdded?.Invoke(type, ressourceAmount);
    }



    private void InitializeDictionnary()
    {
        m_ressourceDictionary.Add(ERessourceType.GOLD, 250);
        m_ressourceDictionary.Add(ERessourceType.FOOD, 100);
        m_ressourceDictionary.Add(ERessourceType.WATER, 100);
        m_ressourceDictionary.Add(ERessourceType.ENERGY, 0);
        m_ressourceDictionary.Add(ERessourceType.MORALE, 0);
        m_ressourceDictionary.Add(ERessourceType.RESEARCH, 10);


    }


    public void UpdateMaxCapacity(int amount, ERessourceType type)
    {
        switch (type)
        {
            case ERessourceType.GOLD:

                type = ERessourceType.GOLD;
                m_maxGoldStock += amount;
                break;

            case ERessourceType.FOOD:
                type = ERessourceType.FOOD;
                m_maxFoodStock += amount;
                break;

            case ERessourceType.WATER:
                type = ERessourceType.WATER;
                m_maxWaterStock += amount;
                break;
            case ERessourceType.ENERGY:
                type = ERessourceType.ENERGY;
                m_maxEnergy += amount;
                break;

        }
    }


    private float GetConsumptionMultiplierByTrait(SheepInstance sheep)
    {
        switch (sheep.Trait)
        {
            case ESheepTrait.Glutton:
                return 1.5f;
            case ESheepTrait.Frugal:
                return 0.5f;

            default: return 1;
        }
    }


   
}
