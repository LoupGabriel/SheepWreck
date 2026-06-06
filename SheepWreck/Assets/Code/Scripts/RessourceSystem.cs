
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
  
}

public class RessourceSystem : MonoBehaviour
{
  
    public static RessourceSystem Instance;
    public Dictionary<ERessourceType, int> m_ressourceDictionary = new Dictionary<ERessourceType, int>();

    public Action<ERessourceType> OnRessourceChange;


    [SerializeField]
    private int m_maxGoldStock = 1000;

    [SerializeField]
    private int m_maxFoodStock = 100;

    [SerializeField]
    private int m_maxWaterStock = 100;

    [SerializeField]
    private int m_maxEnergy = 100;

    private int m_currentNumberOfSheep = 15;
    private int m_foodConsumptionBySheep = 1;
    private int m_WaterConsumptionBySheep = 1;

    private ShipSystem m_shipSystem;
    public int m_totalUpkeep;

    [SerializeField] private UIRessources m_ressourcesUI;

  
    private void Awake()
    {
        Instance = this;
        InitializeDictionnary();
        m_shipSystem = ShipSystem.Instance;



       

    }
    
    private void OnEnable()
    {
        TimeManager.Instance.OnDayPast += GlobalFoodConsumption;
        TimeManager.Instance.OnDayPast += GlobalWaterConsumption;

        TimeManager.Instance.OnWeekPast += UpkeepPayment;

    }
    private void OnDisable()
    {
        TimeManager.Instance.OnDayPast -= GlobalFoodConsumption;
        TimeManager.Instance.OnWeekPast -= UpkeepPayment;

    }
    private void Update()
    {

        m_currentNumberOfSheep = CrewManager.Instance.m_currentSheepOnBoard.Count;
       
        
    }


    private void GlobalFoodConsumption()
    {


        if ( m_ressourceDictionary[ERessourceType.FOOD] != 0)
        {
            // current food minus food consumption time number of sheep

            m_ressourceDictionary[ERessourceType.FOOD] -= m_foodConsumptionBySheep * m_currentNumberOfSheep;
            m_ressourceDictionary[ERessourceType.FOOD] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.FOOD], 0, m_maxFoodStock);


            //notify Hud

            OnRessourceChange?.Invoke(ERessourceType.FOOD);
           
           
            //call Consumefood for each sheep
            foreach (SheepInstance sheep in CrewManager.Instance.m_currentSheepOnBoard)
            {
                sheep.ConsumeFood(m_foodConsumptionBySheep);
            }
        }

    }
    private void GlobalWaterConsumption()
    {

        if ( m_ressourceDictionary[ERessourceType.WATER] != 0)
        {
            // current water minus water consumption time number of sheep
           

            m_ressourceDictionary[ERessourceType.WATER] -= m_WaterConsumptionBySheep * m_currentNumberOfSheep;
            //clamp the value at 0 
            m_ressourceDictionary[ERessourceType.WATER] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.WATER], 0, m_maxWaterStock);
           
        

            //notify Hud
            OnRessourceChange?.Invoke(ERessourceType.WATER);
            
            

            foreach (SheepInstance sheep in CrewManager.Instance.m_currentSheepOnBoard)
            {
                sheep.ConsumeWater(m_WaterConsumptionBySheep);
            }
        }

    }

    private void UpkeepPayment()
    {




        foreach (RoomInstance room in m_shipSystem.m_shipCurrentRooms)
        {

            m_totalUpkeep += room.m_roomData.m_upkeepCost;
        }


        m_ressourceDictionary[ERessourceType.GOLD] -= m_totalUpkeep;
        m_ressourceDictionary[ERessourceType.GOLD] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.GOLD], 0, m_maxWaterStock);

        //notify Hud
        OnRessourceChange?.Invoke(ERessourceType.GOLD);

        m_totalUpkeep = 0;



    }

    public void AddRessource(int ressourceAmount, RoomData roomData)
    {
        
      
        switch (roomData.m_ressourceProduced)
        {
            case RoomData.ERessourceProduced.food:


                m_ressourceDictionary[ERessourceType.FOOD] += ressourceAmount;
                OnRessourceChange?.Invoke(ERessourceType.FOOD);
                break;

            case RoomData.ERessourceProduced.water:

                m_ressourceDictionary[ERessourceType.WATER] += ressourceAmount;
                OnRessourceChange?.Invoke(ERessourceType.WATER);
                break;

        }


    }


  

    private void InitializeDictionnary()
    {
        m_ressourceDictionary.Add(ERessourceType.GOLD, 100);
        m_ressourceDictionary.Add(ERessourceType.FOOD, 100);
        m_ressourceDictionary.Add(ERessourceType.WATER, 100);
        

    }

}
