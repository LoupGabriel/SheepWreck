using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_windSwirl;
    [SerializeField] private ParticleSystem m_clouds;



    public void SetSailSpeed(float newSpeed)
    {
        var velocitySwirl = m_windSwirl.velocityOverLifetime;
        velocitySwirl.speedModifier = newSpeed;

        var velocityCloud = m_clouds.velocityOverLifetime;
        velocityCloud.speedModifier = newSpeed;
    }

}
