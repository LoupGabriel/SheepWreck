using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RessourceSystem : MonoBehaviour
{

    public static RessourceSystem Instance;


    public int m_currentFoodStock = 1000;
    private int m_maxFoodStock = 1000;

    public int m_currentWaterStock = 100;
    private int m_maxWaterStock = 1000;

    private int m_currentEnergyStock;
    private int m_maxEnergy;

    private int m_currentNumberOfSheep = 15;
    private int m_foodConsumptionBySheep = 1;
    private int m_WaterConsumptionBySheep = 1;

    private int m_currentMorale;


    private float m_foodConsuptionTime = 3f;
    private float m_waterConsuptionTime = 2f;

    private float m_foodTimeSinceLastConsumption = 0;
    private float m_waterTimerSinceLastConsumption = 0;


    [SerializeField] private UIRessources m_ressourcesUI;


    private void Start()
    {
        Instance = this;
    }


    private void Update()
    {
        GlobalFoodConsumption();
        GlobalWaterConsumption();
    }


    private void GlobalFoodConsumption()
    {


        m_foodTimeSinceLastConsumption += Time.deltaTime;


        if (m_foodTimeSinceLastConsumption >= m_foodConsuptionTime && m_currentFoodStock != 0)
        {
            // current food minus food consumption time number of sheep
            m_currentFoodStock -= m_foodConsumptionBySheep * m_currentNumberOfSheep;
            m_currentFoodStock = Mathf.Clamp(m_currentFoodStock, 0, m_maxFoodStock);
            m_foodTimeSinceLastConsumption = 0;

            //notify Hud

            m_ressourcesUI.NotifyFoodChange(m_currentFoodStock);
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


        if (m_waterTimerSinceLastConsumption >= m_waterConsuptionTime && m_currentWaterStock != 0)
        {
            // current water minus water consumption time number of sheep
            m_currentWaterStock -= m_WaterConsumptionBySheep * m_currentNumberOfSheep;
            m_currentWaterStock = Mathf.Clamp(m_currentWaterStock, 0, m_maxWaterStock);
            m_waterTimerSinceLastConsumption = 0;

            //notify Hud

            m_ressourcesUI.NotifyWaterChange(m_currentWaterStock);

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
            case RoomData.RessourceProduced.food:


                m_currentFoodStock += ressourceAmount;
                break;

            case RoomData.RessourceProduced.water:

                m_currentWaterStock += ressourceAmount;
                break;

        }


    }

}
