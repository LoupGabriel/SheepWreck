using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance;
    [SerializeField]
    private CameraController m_cameraController;
    private CinemachineCamera m_sheepCamera;
    [SerializeField]
    private CinemachineCamera m_gameplayCamera;

    [SerializeField]
    private SelectionManager selectionManager;

    private void Awake()
    {
        Instance = this;
    }

    public void FocusSheep(SheepController currentSheepInstance)
    {

        if (m_sheepCamera != null)
        {
            m_sheepCamera.Priority = 0;
        }



        m_sheepCamera = currentSheepInstance.GetComponentInChildren<CinemachineCamera>();

        if (m_sheepCamera == null)
        {
            return;
        }

        if (m_sheepCamera != null && m_gameplayCamera != null)
        {

            m_sheepCamera.Priority = 20;
            m_gameplayCamera.Priority = 0;
        }

        m_cameraController.NotifyFocus(currentSheepInstance.transform);
    }

    public void ExitFocus()
    {
        if (m_sheepCamera != null && m_gameplayCamera != null)
        {
            m_gameplayCamera.Priority = 20;
            m_sheepCamera.Priority = 0;
        }
    }
}

