using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SheepNames", menuName = "DataBase/SheepNames")]
public class SheepNamesDataBase : ScriptableObject
{
    public List<string> m_sheepNamesDataBase = new List<string>() 
    
    {
    "Captain Baa-beard",
    "Black Baa-rtholomew",
    "Wool Rogers",
    "Captain Woolbeard",
    "The Fleece Reaper",
    "Baa-nacle Bill",
    "Wool Turner",
    "Captain Shearheart",
    "Black Wool Jack",
    "Baa-bossa",
    "Long John Fleece",
    "Captain Rambeard",
    "The Wooly Buccaneer",
    "Sheepwreck Sam",
    "Captain Baa-nanza",
    "Wooliam Kidd",
    "The Grazing Ghost",
    "Baa-dluck Benny",
    "Captain Fluffhook",
    "The Bleating Marauder",
    "Admiral Woolstorm",
    "Fleece Flint",
    "Captain Shearlock",
    "Black Ram Roberts",
    "The Wool Raider",
    "Captain Baaaahamas",
    "Woolverine Jack",
    "The Barnacle Sheep",
    "Captain Bleatbeard",
    "One-Eyed Wooly",
    "The Fleece Kraken",
    "Rammy Bones",
    "Captain Woolfang",
    "Baa-rnaby the Cruel",
    "The Sheep Sea Scoundrel",
    "Captain Hornswool",
    "Woolbeard the Cursed",
    "Bleaty McGraw",
    "The Fuzzy Corsair",
    "Captain Eweblood",
    "The Wool Phantom",
    "Baa-rbarossa",
    "Captain Grazebeard",
    "The Bleating Devil",
    "Ram Raider Rick",
    "Captain Fluffbane",
    "Wool Hook Henry",
    "The Storm Shepherd",
    "Captain Sheepshot",
    "The Fleece Tyrant",
    "Wool Walker",
    "Captain Baa-lderdash",
    "The Salty Shepherd",
    "Ironhoof Woolson",
    "Captain Bleatstorm",
    "The Black Fleece",
    "Woolscar Jack",
    "Captain Ramrage",
    "The Woolen Warlord",
    "Sheepbeard the Savage",
    "Captain Ewe-Turn",
    "The Floating Fleece",
    "Baa-d Beard Bill",
    "Captain Hornhook",
    "Wool-Eye Walter",
    "The Bleating Bastard",
    "Captain Sea Shepherd",
    "Fleeceface Fred",
    "The Wool Marauder",
    "Captain Baa-tanic",
    "The Ram of the Caribbean",
    "Wooly D. Roger",
    "Captain Bleatwave",
    "The Cursed Shepherd",
    "Baa-ckstabber Pete",
    "Captain Fluffbeard",
    "The Wool Whisperer",
    "Sheepstorm Steve",
    "Captain Ewephoria",
    "The Fleece King",
    "Captain Ramhammer",
    "Black Sheep Jack",
    "The Bleating Kraken",
    "Captain Woolrider",
    "The Sea Rammer",
    "Wool Bones Morgan",
    "Captain Fleecefang",
    "The Wandering Ewe",
    "Captain Baa-listic",
    "Rambeard the Ruthless",
    "The Woolocalypse",
    "Captain Shear Terror",
    "The Fluffy Plunderer",
    "Wool Tide Willy",
    "Captain Eweniverse",
    "The Bleat Pirate",
    "Captain Hornsail",
    "Fleecebeard the Mad",
    "The Sheepyard Scavenger"
};


    public string GetRandomName()
    {
        int index = UnityEngine.Random.Range(0, m_sheepNamesDataBase.Count);

        return m_sheepNamesDataBase[index];

        
    }

}
