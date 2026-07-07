using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_windSwirl;
    [SerializeField] private ParticleSystem m_clouds;
    [SerializeField] private ParticleSystem m_foam;



    private void Start()
    {
        var emission = m_foam.emission;

        emission.enabled = false;
       
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

        var emission = m_foam.emission;

        emission.enabled = true;

    }

}
