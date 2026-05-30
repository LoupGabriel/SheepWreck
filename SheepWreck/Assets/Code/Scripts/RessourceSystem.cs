using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RessourceSystem : MonoBehaviour
{
    public int m_currentFoodStock = 1000;
    private int m_maxFoodStock = 1000;

    private int m_currentWaterStock;
    private int m_maxWaterStock;

    private int m_currentEnergyStock;
    private int m_maxEnergy;

    private int m_currentNumberOfSheep = 15;
    private int m_foodConsumptionBySheep = 1;

    private int m_currentMorale;


    private float m_foodConsuptionTime = 3f;
    private float m_waterConsuptionTime = 2f;

    private float m_foodTimeSinceLastConsumption = 0;
    private float m_waterTimerSinceLastConsumption = 0;

   


   
    private void Update()
    {
        m_foodTimeSinceLastConsumption += Time.deltaTime;
       

        if(m_foodTimeSinceLastConsumption >= m_foodConsuptionTime)
        {
            // current food minus food consumption time number of sheep
            m_currentFoodStock -= m_foodConsumptionBySheep * m_currentNumberOfSheep;
            m_foodTimeSinceLastConsumption = 0;

            //call Consumefood for each sheep
            foreach(SheepInstance sheep in CrewManager.Instance.m_currentSheepOnBoard)
            {
                sheep.ConsumeFood(m_foodConsumptionBySheep);
            }
        }
    }

}
