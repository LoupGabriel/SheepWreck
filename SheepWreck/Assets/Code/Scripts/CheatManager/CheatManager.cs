using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GUI;

public class CheatManager : MonoBehaviour
{
    public static CheatManager Instance { get; private set; }


    private bool m_showWindow = false;
    private Rect m_winRect = new Rect(20, 20, 250, 500);
   
    public float m_currentTime;
    
    
    //ressource selection
    private int m_selectedRessourceIndex = 0;
    private string[] m_ressourceType = { "Gold", "Food", "Water", "Energy" };

    //Sheep Managment
    private int m_selectedSheep = 0;
    private string[] m_selectionType = { "All Sheep","Selected Sheep"};
    //time
    private Rect m_hRect = new Rect(0, 300, 250, 30);

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
        if (GUILayout.Button("Add Ressource"))
        {

        }

        if (GUILayout.Button("Remove Ressource"))
        {

        }

        m_selectedSheep = GUILayout.Toolbar(m_selectedSheep, m_selectionType);

        if (GUILayout.Button("Make it mad"))
        {

        }
        if (GUILayout.Button("Make it happy"))
        {

        }

        GUILayout.Label("Behavior");

        GUILayout.BeginArea(new Rect(0,300,250,100));
        if (GUILayout.Button("Idle"))
        {

        }
        if (GUILayout.Button("Working"))
        {

        }
        if (GUILayout.Button("WaitForWork"))
        {

        }
        if (GUILayout.Button("Resting"))
        {

        }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, 450, 250, 100));

        GUILayout.Label("Set Time Speed");

        m_currentTime = GUI.HorizontalSlider(m_hRect, m_currentTime, 0f, 10f);
        GUILayout.EndArea();

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Close"))
        {
            m_showWindow = false;
        }
        
    }

}
