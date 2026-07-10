
using System;
using System.Collections.Generic;
using UnityEngine;

public class IslandInstance : MonoBehaviour
{
    public string m_islandName;
    [SerializeField] public Vector2Int m_gridPos;
    [SerializeField]
    private SheepNamesDataBase m_nameData;
    public List<Quest> m_availableQuest;
    public List<SheepRecruitData> m_availableRecruits = new();
    [SerializeField] private int m_recruitCount = 3;
    private void Start()
    {
        m_islandName = $"{m_gridPos.x}/{m_gridPos.y}";
        GenerateRecruits();
    }


    private void GenerateRecruits()
    {
        m_availableRecruits.Clear();

        for (int i = 0; i < m_recruitCount; i++)
        {
            SheepRecruitData recruitData = new SheepRecruitData
            {
                name = m_nameData.GetRandomName(),
                trait = GenerateRandomTrait(),
                speciality = GenerateRandomSpeciality(),
                farmerXp = UnityEngine.Random.Range(0, 50),
                engineerXp = UnityEngine.Random.Range(0, 50),
                sailorXp = UnityEngine.Random.Range(0, 50),
                cost = UnityEngine.Random.Range(20, 101),
            };
            m_availableRecruits.Add(recruitData);

        }
    }


    private ESheepTrait GenerateRandomTrait()
    {
        Array traits = Enum.GetValues(typeof(ESheepTrait));

        return (ESheepTrait)traits.GetValue(UnityEngine.Random.Range(1, traits.Length));

    }
    private ESheepSpeciality GenerateRandomSpeciality()
    {

        Array speciality = Enum.GetValues(typeof(ESheepSpeciality));
        return (ESheepSpeciality)speciality.GetValue(UnityEngine.Random.Range(1, speciality.Length));
    }
}
