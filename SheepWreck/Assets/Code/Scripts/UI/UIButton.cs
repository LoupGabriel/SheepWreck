using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    [SerializeField] private string m_sceneName;

   
    public void LoadScene()
    {
        SceneManager.LoadScene(m_sceneName);
        PauseController.IsPaused(false);
    }


    public void Exit()
    {
       Application.Quit();
    }
}
