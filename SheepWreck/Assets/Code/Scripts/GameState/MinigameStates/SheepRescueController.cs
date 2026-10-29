using UnityEngine;

public class SheepRescueController : MonoBehaviour
{
    [SerializeField] private GameObject m_sheepToRescue;
    [SerializeField] private Transform m_RescueSpawn;
    public GameObject SpawnRescueSheep()
    {
        GameObject sheep = Instantiate(m_sheepToRescue, m_RescueSpawn.transform.position, Quaternion.identity);
        return sheep;
    }

    public void RescueMovement(GameObject sheep)
    {
        sheep.transform.Translate(Vector3.right * 5f * Time.deltaTime);
    }


    public void DestroyIsland(GameObject island)
    {
        Destroy(island);
    }
}
