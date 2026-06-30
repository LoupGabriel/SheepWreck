using UnityEngine;



[CreateAssetMenu(fileName = "RoomData", menuName = "ScriptableObjects/Room")]
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

    public string m_roomName;
    public string m_description;
    public Sprite m_roomIcon;

    public ERessourceType m_ressourceProduced = ERessourceType.ENERGY;
   // public ERessourceProduced m_ressourceProduced = ERessourceProduced.energy;

    public EBuildingType m_buildingType = EBuildingType.EMPTY;

    public int m_buildCost;
    public int m_upkeepCost;
    public int m_maxSheepCapacity;
    //production
    public float m_productionRate;
    public float m_energyConsumption;

    //stock
    public int m_capacity;


    public int m_level;








}
