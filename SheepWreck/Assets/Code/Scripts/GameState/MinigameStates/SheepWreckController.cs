using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SheepWreckController : MonoBehaviour
{
    [SerializeField] private GameObject m_fishNet;

    [SerializeField] private Transform m_startRope;

    [SerializeField] private LineRenderer m_lineRenderer;



    private void Start()
    {
        
        m_lineRenderer.positionCount = 2;
        m_fishNet.gameObject.SetActive(false);
        m_lineRenderer.gameObject.SetActive(false);

    }

    public void EnableRope()
    {
        m_fishNet.gameObject.SetActive(true);
        m_lineRenderer.gameObject.SetActive(true);
    }

    public void ExitState()
    {
        m_fishNet.SetActive(false);
        m_lineRenderer.gameObject.SetActive(false);
    }
    public void SetRope()
    {
        m_lineRenderer.SetPosition(0, m_startRope.position);
        m_lineRenderer.SetPosition(1, m_fishNet.transform.position);

        

    }


 

}
