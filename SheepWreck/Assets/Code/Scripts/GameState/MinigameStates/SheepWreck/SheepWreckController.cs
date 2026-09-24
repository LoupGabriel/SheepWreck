using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SheepWreckController : MonoBehaviour
{
    [SerializeField] private GameObject m_fishNet;

    [SerializeField] private Transform m_startRope;
    [SerializeField] private Transform m_ropeEnd01;
    [SerializeField] private Transform m_ropeEnd02;

    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private LineRenderer m_lineRenderer02;
    [SerializeField] private LineRenderer m_lineRenderer03;

    [SerializeField] private List<ShipwreckItem> m_shipwreckItems;

    private List<ShipwreckItem> m_spawnedItem = new List<ShipwreckItem>();

    private Vector3 m_originalPos;

    [SerializeField] private Vector3 m_MaxPos;
    [SerializeField] private Image m_CraneControl;


    [SerializeField] private int m_maxItemSpawn;
    [SerializeField] private float m_moveSpeed = 2f;
    [SerializeField] private float m_eventDuration = 30f;
    [SerializeField] private Transform m_spawmPoint;
    private Vector3 m_direction = Vector3.zero;






    public Image Sprite => m_CraneControl;
    public Vector3 Direction => m_direction;
    public float Duration => m_eventDuration;


    private void Start()
    {

        m_lineRenderer.positionCount = 2;
        m_lineRenderer02.positionCount = 2;
        m_lineRenderer03.positionCount = 2;
        m_fishNet.gameObject.SetActive(false);
        m_lineRenderer.gameObject.SetActive(false);
        m_originalPos = transform.position;
        m_CraneControl.gameObject.SetActive(false);
    }



    public void EnableRope()
    {
        m_fishNet.gameObject.SetActive(true);
        m_lineRenderer.gameObject.SetActive(true);
    }

    public void ExitState()
    {
        m_CraneControl.gameObject.SetActive(false);
        m_fishNet.SetActive(false);
        m_lineRenderer.gameObject.SetActive(false);
        foreach (ShipwreckItem item in m_spawnedItem)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }

        }
        m_spawnedItem.Clear();
    }
    public void SetRope()
    {
        m_lineRenderer.SetPosition(0, m_startRope.position);
        m_lineRenderer.SetPosition(1, m_fishNet.transform.position);
        m_lineRenderer02.SetPosition(0, m_fishNet.transform.position);
        m_lineRenderer03.SetPosition(0, m_fishNet.transform.position);
        m_lineRenderer02.SetPosition(1, m_ropeEnd01.position);
        m_lineRenderer03.SetPosition(1, m_ropeEnd02.position);


    }
    public void MoveCrane(Vector3 direction)
    {
        m_fishNet.transform.Translate(direction * m_moveSpeed * Time.deltaTime);
        m_fishNet.transform.position = new Vector3
            (m_originalPos.x,
            Mathf.Clamp(m_fishNet.transform.position.y, m_originalPos.y, m_MaxPos.y),
            0);
    }
    public void UpdateSprite(Sprite newSprite)
    {
        m_CraneControl.sprite = newSprite;
    }
    public void ChangeDirection(float direction)
    {
        m_direction = new Vector3(0, direction, 0);
    }

    public void EnableControl(bool enable)
    {
        m_CraneControl.gameObject.SetActive(enable);
    }

    public void SpawnItem()
    {
        for (int i = 0; i < m_maxItemSpawn; i++)
        {
            int index = Random.Range(0, m_shipwreckItems.Count - 1);
            float y = Random.Range(-10, 10);
            Vector3 offset = new Vector3(0, y, 0);
            ShipwreckItem item = Instantiate(m_shipwreckItems[index], m_spawmPoint.position + offset, Quaternion.identity);
            m_spawnedItem.Add(item);
        }
    }
}
