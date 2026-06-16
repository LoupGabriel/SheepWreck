using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GUI;

public class CheatManager : MonoBehaviour
{
    public static CheatManager Instance { get; private set; }
    public Action<int,ERessourceType> OnAddRessource;
    public Action<float> OnChangeSpeed;

    public Action OnKillSheep;
    public Action OnKillAllSheep;

    private bool m_showWindow = false;
    private Rect m_winRect = new Rect(20, 20, 250, 500);
   
    public float m_currentTime;
    


    
    //ressource selection
    private int m_selectedRessourceIndex = 0;
    private string[] m_ressourceType = { "Gold", "Food", "Water", "Energy" };

    //Sheep Managment
    [SerializeField] private GameObject m_sheepPrefab;
    [SerializeField] private Transform m_newSheepParent;
    private string m_sheepPosX = "0";
    private string m_sheepPosY = "0";
    private string m_sheepPosZ = "0";
    private int m_selectedSheep = 0;
    private string[] m_selectionType = { "All Sheep","Selected Sheep"};
    //time
    private Rect m_hRect = new Rect(0, 300, 250, 30);

    private float m_currentSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.f1Key.wasPressedThisFrame)
        {
            m_showWindow = !m_showWindow;
        }
    }
    private void OnGUI()
    {
        if (m_showWindow)
        {


            m_winRect = GUI.Window(0, m_winRect, WindowFunction, "Cheat Manager");



        }

    }


    private void WindowFunction(int windowId)
    {
       

        
        GUILayout.Label("Ressource Managment");


        //ressource selection
        m_selectedRessourceIndex = GUILayout.SelectionGrid(m_selectedRessourceIndex, m_ressourceType,2);

        ERessourceType ressource = ERessourceType.GOLD;
        switch (m_selectedRessourceIndex)
        {
            case 0:
            //gold
            ressource = ERessourceType.GOLD;
                break;
            case 1:
                //food
                ressource = ERessourceType.FOOD;
                break;
            case 2:
                //water
                ressource = ERessourceType.WATER;
                break;
            case 3:
                //energy
                ressource = ERessourceType.ENERGY;
                break;

        }

        if (GUILayout.Button("Add Ressource"))
        {
            OnAddRessource?.Invoke(5,ressource);
            RessourceSystem.Instance.OnRessourceChange?.Invoke(ressource);
        }

        if (GUILayout.Button("Remove Ressource"))
        {
            OnAddRessource?.Invoke(-5, ressource);
            RessourceSystem.Instance.OnRessourceChange?.Invoke(ressource);
        }

        m_selectedSheep = GUILayout.Toolbar(m_selectedSheep, m_selectionType);

        if(m_selectedSheep == 0)
        {

            if (GUILayout.Button("Kill all sheep"))
            {
                OnKillAllSheep?.Invoke();
            }

        }

        if (m_selectedSheep == 1)
        {

            if (GUILayout.Button("Kill"))
            {
                OnKillSheep?.Invoke();
            }

        }
        GUILayout.Label("Spawn Position");
        GUILayout.BeginHorizontal();
        m_sheepPosX = GUILayout.TextField(m_sheepPosX);
        m_sheepPosY = GUILayout.TextField(m_sheepPosY);
        m_sheepPosZ = GUILayout.TextField(m_sheepPosZ);
        GUILayout.EndHorizontal();

        if(GUILayout.Button("Spawn new Sheep"))
        {

           GameObject spawnSheep = Instantiate(m_sheepPrefab, new Vector3( float.Parse(m_sheepPosX), float.Parse(m_sheepPosY), float.Parse(m_sheepPosZ)), Quaternion.identity, m_newSheepParent);

        }











        //GUILayout.Label("Behavior");

        //GUILayout.BeginArea(new Rect(0,300,250,100));
        //if (GUILayout.Button("Idle"))
        //{

        //}
        //if (GUILayout.Button("Working"))
        //{

        //}
        //if (GUILayout.Button("WaitForWork"))
        //{

        //}
        //if (GUILayout.Button("Resting"))
        //{

        //}
        //GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, 300, 250, 100));

        GUILayout.BeginHorizontal();
        GUILayout.Label("Set Time Speed");
        GUILayout.Label($"{m_currentSpeed}");
        GUILayout.EndHorizontal();

        m_currentSpeed = GUILayout.HorizontalSlider(m_currentSpeed, 0, 25);
        

        if (GUILayout.Button("Set new Speed"))
        {

            OnChangeSpeed?.Invoke(m_currentSpeed);
        }

       

        m_currentTime = GUI.HorizontalSlider(m_hRect, m_currentTime, 0f, 10f);
        GUILayout.EndArea();

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Close"))
        {
            m_showWindow = false;
        }
        
    }

}
