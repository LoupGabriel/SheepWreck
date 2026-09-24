using UnityEngine;
using static RoomData;

public class ShipwreckItem : MonoBehaviour
{
    [SerializeField] private ERessourceType m_ressourceProduced;
    [SerializeField] private float m_minSpeed = 2f;
    [SerializeField] private float m_maxSpeed = 10f;

    private float m_currentSpeed;
    private Rigidbody m_rigidbody;
    private Collider m_collider;

    private void Start()
    {
       
        m_currentSpeed = Random.Range(m_minSpeed, m_maxSpeed);
        
    }

    private void Update()
    {
        transform.Translate(Vector3.right * m_currentSpeed * Time.deltaTime);
    }

    public void CatchItem()
    {
        SfxManager.PlaySfx("Click");
        CollectRessource();
        Destroy(gameObject);
    }

    private void CollectRessource()
    {

        SfxManager.PlaySfx("Coin");
      

        RessourceSystem.Instance.GainRessource(50, m_ressourceProduced);

        UiResourceAnimationManager.Instance.PlayCollectAnimation(transform,m_ressourceProduced);
    
    }
}
