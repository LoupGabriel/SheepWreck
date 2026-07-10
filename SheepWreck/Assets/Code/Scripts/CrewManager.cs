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

    public void AddSheep(SheepInstance sheep)
    {
        m_currentSheepOnBoard.Add(sheep);

    }

    public void RemoveSheep(SheepInstance sheep)
    {

        m_currentSheepOnBoard.Remove(sheep);
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
