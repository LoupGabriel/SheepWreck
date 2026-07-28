using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


public enum EEnvironment
{
    DAY,
    NIGHT,
    EVENING,

}
public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_windSwirl;
    [SerializeField] private ParticleSystem m_clouds;
    [SerializeField] private float m_transitionDuration = 5;
    [SerializeField] private Material m_runtimeSkybox;
    private static readonly int m_topColor = Shader.PropertyToID("_TopColor");
    private static readonly int m_bottomColor = Shader.PropertyToID("_BottomColor");
    private Coroutine m_coroutine;
    [SerializeField] private EEnvironment m_currentEnvironment;
    
    [SerializeField] private Color32[] m_topColorIndexes;
    [SerializeField] private Color32[] m_bottomColorIndexes;



    private void Awake()
    {
        m_runtimeSkybox = Instantiate(RenderSettings.skybox);
        RenderSettings.skybox = m_runtimeSkybox;
    }
    /// <summary>
    /// Change the speed of the particles systems
    /// </summary>
    public void SetSailSpeed(float newSpeed)
    {
        var velocitySwirl = m_windSwirl.velocityOverLifetime;
        velocitySwirl.speedModifier = newSpeed;

        var velocityCloud = m_clouds.velocityOverLifetime;
        velocityCloud.speedModifier = newSpeed;


    }

    /// <summary>
    /// Change the background color 
    /// </summary>
    /// <param name="currentEnvironment"></param>
    public void ChangeBackground(int currentEnvironment)
    {
        Color targetTop = Color.black;
        Color targetBot = Color.white;

        switch (currentEnvironment)
        {
            case (int)EEnvironment.DAY:
                targetTop = m_topColorIndexes[(int)EEnvironment.DAY];
                targetBot = m_bottomColorIndexes[(int)EEnvironment.DAY];
                break;

            case (int)EEnvironment.NIGHT:
                targetTop = m_topColorIndexes[(int)EEnvironment.NIGHT];
                targetBot = m_bottomColorIndexes[(int)EEnvironment.NIGHT];
                break;

            case (int)EEnvironment.EVENING:
                targetTop = m_topColorIndexes[(int)EEnvironment.EVENING];
                targetBot = m_bottomColorIndexes[(int)EEnvironment.EVENING];
                break;
        }

        m_coroutine= StartCoroutine(LerpSkybox(targetTop, targetBot, m_transitionDuration));
    }


    /// <summary>
    /// Lerp current background color with the target one
    /// </summary>
    private IEnumerator LerpSkybox(Color targetTop, Color targetBottom, float duration)
    {
        Color startTop = m_runtimeSkybox.GetColor(m_topColor);
        Color startBottom = m_runtimeSkybox.GetColor(m_bottomColor);

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            m_runtimeSkybox.SetColor(
                m_topColor,
                Color.Lerp(startTop, targetTop, t));

            m_runtimeSkybox.SetColor(
                m_bottomColor,
                Color.Lerp(startBottom, targetBottom, t));

            yield return null;
        }

        m_runtimeSkybox.SetColor(m_topColor, targetTop);
        m_runtimeSkybox.SetColor(m_bottomColor, targetBottom);
    }
}
