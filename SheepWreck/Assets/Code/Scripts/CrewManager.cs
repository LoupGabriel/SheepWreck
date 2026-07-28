using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    

    public static CrewManager Instance;

    [SerializeField] public List<SheepInstance> m_currentSheepOnBoard = new();
    [SerializeField] public Transform m_sheepSpawnPosition;



    private void Awake()
    {
        Instance = this;


    }

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
        if(m_currentSheepOnBoard.Count <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    /// <summary>
    ///  Cheat Manager button
    /// </summary>
    private void KillAllSheep()
    {
        foreach(SheepInstance sheep in m_currentSheepOnBoard)
        {
            Destroy(sheep.gameObject);
        }
    }
}
