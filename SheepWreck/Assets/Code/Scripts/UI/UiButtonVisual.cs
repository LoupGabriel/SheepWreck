using UnityEngine;
using UnityEngine.EventSystems;

public class UiButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private Vector3 m_offset = new Vector3(0, 30, 0);
    [SerializeField] private float m_scaleoffset = 1.2f;
    private Vector3 m_originalScale;
    private Vector3 m_originalPos;


    private void Start()
    {
        m_originalScale= transform.localScale;
        m_originalPos= transform.localPosition;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SfxManager.PlaySfx("ValidateClick");
        transform.localScale = m_originalScale;
        transform.localPosition = m_originalPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position += m_offset;
        transform.localScale *= m_scaleoffset;
        SfxManager.PlaySfx("Click");
        CursorManager.Instance.SetCursorType(ECursorType.InteractUI);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position -= m_offset;
        transform.localScale /= m_scaleoffset;
        CursorManager.Instance.SetCursorType(ECursorType.Default);
    }
}
