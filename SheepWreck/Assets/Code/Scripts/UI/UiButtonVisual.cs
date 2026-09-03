using UnityEngine;
using UnityEngine.EventSystems;

public class UiButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private Vector3 m_offset = new Vector3(0, 30, 0);
    [SerializeField] private float m_scaleoffset = 1.2f;

    public void OnPointerClick(PointerEventData eventData)
    {
        SfxManager.PlaySfx("ValidateClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position += m_offset;
        transform.localScale *= m_scaleoffset;
        SfxManager.PlaySfx("Click");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position -= m_offset;
        transform.localScale /= m_scaleoffset;
    }
}
