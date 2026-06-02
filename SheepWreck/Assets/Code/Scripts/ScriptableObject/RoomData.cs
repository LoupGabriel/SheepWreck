using UnityEngine;



[CreateAssetMenu(fileName ="RoomData",menuName = "ScriptableObjects/Room")]
public class RoomData : ScriptableObject
{


  

    public enum RessourceProduced
    {
        gold,
        morale,
        energy,
        food,
        water,
        nothing

    }

    [SerializeField] private string m_roomName;
    [SerializeField] private string m_description;
    [SerializeField] private Sprite m_roomIcon;
    


    [SerializeField] private int m_buildCost;
    [SerializeField] private int m_upkeepCost;
    [SerializeField] private int m_maxSheepCapacity;
    //
    [SerializeField] private float m_productionRate;
    [SerializeField] private float m_energyConsumption;

    [SerializeField] public RessourceProduced m_ressourceProduced = RessourceProduced.energy;

    [SerializeField] private int m_level;
    

   
        




}
