using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FishNetController : MonoBehaviour, ISelectable
{
  

    private Vector3 m_originalPos;

    [SerializeField] private Vector3 m_MaxPos;
   
    private void Start()
    {

        m_originalPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ShipwreckItem>() != null)
        {
            ShipwreckItem item = other.GetComponent<ShipwreckItem>();
            item.CatchItem();
        }
    }







}
