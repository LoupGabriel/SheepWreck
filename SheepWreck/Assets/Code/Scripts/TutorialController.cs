using UnityEngine;


public enum ETutorialStep
{
    BuildFoodRoom,
    AssignSheep,
    BuildWaterRoom,
    BuildEnergyRoom,
    FirstResearch,
    FirstSail,
    AcceptFirstQuest,
    OpenBook,
    AcceptFirstQuestCompleted,
    Completed
}
public class TutorialController : MonoBehaviour
{
    [SerializeField] private UiInfo m_uiInfo;
    [SerializeField] private GameObject m_tutorialCompleted;
    private ETutorialStep m_currentStep;

    private RoomInstance m_foodRoom;
    private RoomInstance m_waterRoom;
    private RoomInstance m_energyRoom;

    private void OnEnable()
    {
        RoomInstance.OnFoodRoomBuilt += HandleFoodRoomBuilt;
        RoomInstance.OnSheepAssigned += HandleSheepAssigned;
        RoomInstance.OnWaterRoomBuilt += HandleWaterRoomBuilt;
        RoomInstance.OnEnergyRoomBuilt += HandleEnergyRoomBuilt;
        ResearchSystem.OnFirstResearch += HandleFirstResearch;
        TravelSystem.OnFirstSail += HandleFirstSail;
        QuestManager.OnAcceptQuest += HandleFirstQuest;
        UiPanelManager.OnOpenBook += HandleOpenBook;
        QuestManager.OnAcceptCompleted += HandleFirstQuestCompleted;
        UiInfo.OnCompleteTutorial += HandleCompleteTutorial;
    }
    private void OnDisable()
    {
        RoomInstance.OnFoodRoomBuilt -= HandleFoodRoomBuilt;
        RoomInstance.OnSheepAssigned -= HandleSheepAssigned;
        RoomInstance.OnWaterRoomBuilt -= HandleWaterRoomBuilt;
        RoomInstance.OnEnergyRoomBuilt -= HandleEnergyRoomBuilt;
        ResearchSystem.OnFirstResearch -= HandleFirstResearch;
        TravelSystem.OnFirstSail -= HandleFirstSail;
        QuestManager.OnAcceptQuest -= HandleFirstQuest;
        UiPanelManager.OnOpenBook -= HandleOpenBook;
        QuestManager.OnAcceptCompleted -= HandleFirstQuestCompleted;
        UiInfo.OnCompleteTutorial -= HandleCompleteTutorial;
    }

    private void Start()
    {
        m_tutorialCompleted.SetActive(false);
        m_currentStep = ETutorialStep.BuildFoodRoom;
        m_uiInfo.ShowInfo(0);
    }

    private void HandleFoodRoomBuilt(RoomInstance room)
    {
        if (m_currentStep != ETutorialStep.BuildFoodRoom)
            return;

        m_foodRoom = room;
        m_currentStep = ETutorialStep.AssignSheep;

        m_uiInfo.ShowInfo(1);
    }
    private void HandleWaterRoomBuilt(RoomInstance room)
    {
        if (m_currentStep != ETutorialStep.BuildWaterRoom)
            return;

        m_waterRoom = room;
        m_currentStep = ETutorialStep.BuildEnergyRoom;

        m_uiInfo.ShowInfo(3);
    }
    private void HandleEnergyRoomBuilt(RoomInstance room)
    {
        if (m_currentStep != ETutorialStep.BuildEnergyRoom)
            return;
        RessourceSystem.Instance.GainRessource(10, ERessourceType.RESEARCH);
        UiResourceAnimationManager.Instance.PlayCollectAnimation(room.transform, ERessourceType.RESEARCH);
        m_energyRoom = room;
        m_currentStep = ETutorialStep.FirstResearch;

        m_uiInfo.ShowInfo(4);
    }

    private void HandleSheepAssigned(RoomInstance room)
    {
        if (m_currentStep != ETutorialStep.AssignSheep)
            return;

        if (room != m_foodRoom)
            return;
        
        
        m_currentStep = ETutorialStep.BuildWaterRoom;
        m_uiInfo.ShowInfo(2);
        
    }
    private void HandleFirstResearch()
    {
        if (m_currentStep != ETutorialStep.FirstResearch)
            return;
        
        m_currentStep = ETutorialStep.FirstSail;
        m_uiInfo.ShowInfo(5);
        
    }
    private void HandleFirstSail()
    {
        if (m_currentStep != ETutorialStep.FirstSail)
            return;

        m_currentStep = ETutorialStep.AcceptFirstQuest;
        m_uiInfo.ShowInfo(6);
        
    }
    private void HandleFirstQuest()
    {
        if (m_currentStep != ETutorialStep.AcceptFirstQuest)
            return;

        m_currentStep = ETutorialStep.OpenBook;
        m_uiInfo.ShowInfo(7);
        
    }
    private void HandleOpenBook()
    {
        Debug.Log("open book tutorial");
        if (m_currentStep != ETutorialStep.OpenBook)
            return;

        m_currentStep = ETutorialStep.AcceptFirstQuestCompleted;
        m_uiInfo.ShowInfo(8);
        
    }
    private void HandleFirstQuestCompleted()
    {
        if (m_currentStep != ETutorialStep.AcceptFirstQuestCompleted)
            return;
        
        m_currentStep = ETutorialStep.Completed;
        m_uiInfo.ShowInfo(9);
        
    }
    private void HandleCompleteTutorial()
    {
        if (m_currentStep != ETutorialStep.Completed)
            return;
        
        RessourceSystem.Instance.GainRessource(100,ERessourceType.GOLD);
        UiResourceAnimationManager.Instance.PlayCollectAnimation(ShipSystem.Instance.transform,ERessourceType.GOLD,100);
        SfxManager.PlaySfx("Victory");
        m_tutorialCompleted.SetActive(true);
    }
}
