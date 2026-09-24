using UnityEngine;

public class FireObject : MonoBehaviour
{

    [SerializeField] private GameObject m_vfx;


    public void DestroyFire()
    {
        GameObject vfx = Instantiate(m_vfx, transform.position, Quaternion.identity);
        SfxManager.PlaySfx("FireBurnOut");
        Destroy(gameObject);
        GameManager.Instance.FireEventController.AllFire.Remove(this);
    }
}
