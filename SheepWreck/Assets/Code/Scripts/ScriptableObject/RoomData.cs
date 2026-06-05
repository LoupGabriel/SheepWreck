using UnityEngine;



[CreateAssetMenu(fileName ="RoomData",menuName = "ScriptableObjects/Room")]
public class RoomData : ScriptableObject
{


  

    public enum ERessourceProduced
    {
        gold,
        morale,
        energy,
        food,
        water,
        nothing

    }
    public enum EBuildingType
    {
        PRODUCE,
        STOCK,
        EMPTY
    }

    [SerializeField] private string m_roomName;
    [SerializeField] private string m_description;
    [SerializeField] private Sprite m_roomIcon;

    [SerializeField] public ERessourceProduced m_ressourceProduced = ERessourceProduced.energy;
    [SerializeField]
    public EBuildingType m_buildingType = EBuildingType.EMPTY;

    [SerializeField] private int m_buildCost;
    [SerializeField] private int m_upkeepCost;
    [SerializeField] private int m_maxSheepCapacity;
    //production
    [SerializeField] private float m_productionRate;
    [SerializeField] private float m_energyConsumption;

    //stock
    [SerializeField] private int m_capacity;


    [SerializeField] private int m_level;
    

   
        




}
