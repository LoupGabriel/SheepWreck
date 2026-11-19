using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrewManager : Singleton<CrewManager>
{
    [SerializeField] public List<SheepInstance> m_currentSheepOnBoard = new();
    [SerializeField] public Transform m_sheepSpawnPosition;

    private void Start()
    {
        CheatManager.Instance.OnKillAllSheep += KillAllSheep;
    }
    private void OnDestroy()
    {
        CheatManager.Instance.OnKillAllSheep -= KillAllSheep;
    }

    /// <summary>
    /// Add sheep to the list 
    /// </summary>
    /// <param name="sheep">Sheep instance to add</param>
    public void AddSheep(SheepInstance sheep)
    {
        m_currentSheepOnBoard.Add(sheep);

    }
    public void FillFood(int quantity)
    {
        if (RessourceSystem.Instance.RessourceDictionary[ERessourceType.FOOD] < quantity)
            return;
        foreach (SheepInstance sheep in m_currentSheepOnBoard)
        {
            sheep.ConsumeFood(quantity);
            RessourceSystem.Instance.GainRessource(-quantity, ERessourceType.FOOD);
            UiResourceAnimationManager.Instance?.PlayConsumeAnimation(sheep.transform, ERessourceType.FOOD, 50f, particleCount: 1);
        }

    }

    public void FillWater(int quantity)
    {
        if (RessourceSystem.Instance.RessourceDictionary[ERessourceType.WATER] < quantity)
            return;
        foreach (SheepInstance sheep in m_currentSheepOnBoard)
        {
            sheep.ConsumeWater(quantity);
            RessourceSystem.Instance.GainRessource(-quantity, ERessourceType.WATER);
            UiResourceAnimationManager.Instance?.PlayConsumeAnimation(sheep.transform, ERessourceType.WATER, 50f, particleCount: 1);
        }
    }

    public void PayAll()
    {
        RessourceSystem.Instance.GlobalPayConsumption(1, 1, 1);
    }
    public void PaySelected()
    {
        SheepInstance sheep = SelectionManager.Instance.LastSelectedSheep.GetComponent<SheepInstance>();
        int amount = sheep.GetPayCost();
        if (RessourceSystem.Instance.RessourceDictionary[ERessourceType.GOLD] < amount)
        {
            sheep.m_haBeenPaidThisWeek = false;
            return;
        }
        else
        {
            sheep.m_haBeenPaidThisWeek = true;
            RessourceSystem.Instance.GainRessource(-sheep.GetPayCost(),ERessourceType.GOLD);
        }
       
        UiResourceAnimationManager.Instance?.PlayConsumeAnimation(sheep.transform, ERessourceType.GOLD, 50f, particleCount: 1);
    }

    public void FillWaterSpecific(int quantity)
    {
        if (RessourceSystem.Instance.RessourceDictionary[ERessourceType.WATER] < quantity)
            return;
        SheepInstance sheep = SelectionManager.Instance.LastSelectedSheep.GetComponent<SheepInstance>();
        if (sheep == null) return;
       
        sheep.ConsumeWater(quantity);
        RessourceSystem.Instance.GainRessource(-quantity, ERessourceType.WATER);
        UiResourceAnimationManager.Instance?.PlayConsumeAnimation(sheep.transform, ERessourceType.WATER, 50f, particleCount: 1);

    }
    public void FillFoodSpecific(int quantity)
    {
        if (RessourceSystem.Instance.RessourceDictionary[ERessourceType.FOOD] < quantity)
            return;
        if(SelectionManager.Instance.LastSelectedSheep.GetComponent<SheepInstance>() != null)
        {
            SheepInstance sheep = SelectionManager.Instance.LastSelectedSheep.GetComponent<SheepInstance>();
            sheep.ConsumeFood(quantity);
            RessourceSystem.Instance.GainRessource(-quantity, ERessourceType.FOOD);
            UiResourceAnimationManager.Instance?.PlayConsumeAnimation(sheep.transform, ERessourceType.FOOD, 50f, particleCount: 1);
        }

    }
    /// <summary>
    /// Remove Sheep from the list
    /// </summary>
    /// <param name="sheep">Sheep to delete</param>
    public void RemoveSheep(SheepInstance sheep)
    {

        m_currentSheepOnBoard.Remove(sheep);
        CheckForEndGame();
    }

    /// <summary>
    /// Game over when the crew is empty
    /// </summary>
    private void CheckForEndGame()
    {
        if (m_currentSheepOnBoard.Count <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    /// <summary>
    ///  Cheat Manager button
    /// </summary>
    private void KillAllSheep()
    {
        foreach (SheepInstance sheep in m_currentSheepOnBoard)
        {
            Destroy(sheep.gameObject);
        }
    }



}
