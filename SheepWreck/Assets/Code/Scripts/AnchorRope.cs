using UnityEngine;

public class AnchorRope : MonoBehaviour
{
    [SerializeField] private Transform m_start;
    [SerializeField] private Transform m_end;
    private LineRenderer m_lineRenderer;



    private void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 2;

    }

    private void Update()
    {
        m_lineRenderer.SetPosition(0, m_start.position);
        m_lineRenderer.SetPosition(1, m_end.position);

    }


}
