using NUnit.Framework;
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

    private int m_currentMorale;


    private float m_foodConsuptionTime = 10f;
    private float m_waterConsuptionTime = 2f;

    private float m_foodTimeSinceLastConsumption = 0;
    private float m_waterTimerSinceLastConsumption = 0;


    [SerializeField] private UIRessources m_ressourcesUI;


    private void Start()
    {
        Instance = this;
        InitializeDictionnary();
    }


    private void Update()
    {

        m_currentNumberOfSheep = CrewManager.Instance.m_currentSheepOnBoard.Count;
        GlobalFoodConsumption();
        GlobalWaterConsumption();
    }


    private void GlobalFoodConsumption()
    {


        m_foodTimeSinceLastConsumption += Time.deltaTime;


        if (m_foodTimeSinceLastConsumption >= m_foodConsuptionTime && m_ressourceDictionary[ERessourceType.FOOD] != 0)
        {
            // current food minus food consumption time number of sheep

            m_ressourceDictionary[ERessourceType.FOOD] -= m_foodConsumptionBySheep * m_currentNumberOfSheep;
            m_ressourceDictionary[ERessourceType.FOOD] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.FOOD], 0, m_maxFoodStock);

            m_foodTimeSinceLastConsumption = 0;

            //notify Hud

            m_ressourcesUI.NotifyRessourceChange(ERessourceType.FOOD);
           
            //call Consumefood for each sheep
            foreach (SheepInstance sheep in CrewManager.Instance.m_currentSheepOnBoard)
            {
                sheep.ConsumeFood(m_foodConsumptionBySheep);
            }
        }

    }
    private void GlobalWaterConsumption()
    {


        m_waterTimerSinceLastConsumption += Time.deltaTime;


        if (m_waterTimerSinceLastConsumption >= m_waterConsuptionTime && m_ressourceDictionary[ERessourceType.WATER] != 0)
        {
            // current water minus water consumption time number of sheep
           

            m_ressourceDictionary[ERessourceType.WATER] -= m_WaterConsumptionBySheep * m_currentNumberOfSheep;
            //clamp the value at 0 
            m_ressourceDictionary[ERessourceType.WATER] = Mathf.Clamp(m_ressourceDictionary[ERessourceType.WATER], 0, m_maxWaterStock);
           
            m_waterTimerSinceLastConsumption = 0;

            //notify Hud
            m_ressourcesUI.NotifyRessourceChange(ERessourceType.WATER);
            

            foreach (SheepInstance sheep in CrewManager.Instance.m_currentSheepOnBoard)
            {
                sheep.ConsumeWater(m_WaterConsumptionBySheep);
            }
        }

    }


    public void AddRessource(int ressourceAmount, RoomData roomData)
    {
        
      
        switch (roomData.m_ressourceProduced)
        {
            case RoomData.ERessourceProduced.food:


                m_ressourceDictionary[ERessourceType.FOOD] += ressourceAmount;
                break;

            case RoomData.ERessourceProduced.water:

                m_ressourceDictionary[ERessourceType.WATER] += ressourceAmount;
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
