using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    

    public static CrewManager Instance;

    [SerializeField] public List<SheepInstance> m_currentSheepOnBoard = new();



    private void Awake()
    {
        Instance = this;
    }



    public void AddSheep(SheepInstance sheep)
    {
        m_currentSheepOnBoard.Add(sheep);


    }

    public void RemoveSheep(SheepInstance sheep)
    {

        m_currentSheepOnBoard.Remove(sheep);
    }
}
