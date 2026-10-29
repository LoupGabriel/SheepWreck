using UnityEngine;
using static RoomData;

public class ShipwreckItem : MonoBehaviour
{
    [SerializeField] private ERessourceType m_ressourceProduced;
    [SerializeField] private float m_minSpeed = 2f;
    [SerializeField] private float m_maxSpeed = 10f;
    [SerializeField] private int m_amountMin = 1;
    [SerializeField] private int m_amountMax = 2;
    private float m_currentSpeed;
    
    

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
      
        int randAmount = Random.Range(m_amountMin, m_amountMax);
        RessourceSystem.Instance.GainRessource(randAmount, m_ressourceProduced);

        UiResourceAnimationManager.Instance.PlayCollectAnimation(transform,m_ressourceProduced);
    
    }
}
