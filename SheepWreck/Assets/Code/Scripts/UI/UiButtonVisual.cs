using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Vector3 m_offset = new Vector3(0, 30, 0);
    [SerializeField] private float m_scaleoffset = 1.2f;
    private Image m_image;
    private Vector3 m_originalScale;
    private Vector3 m_originalPos;


    private void Start()
    {
        m_originalScale = transform.localScale;
        m_originalPos = transform.localPosition;
       m_image = GetComponent<Image>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SfxManager.PlaySfx("ValidateClick");
        transform.localScale = m_originalScale;
        transform.localPosition = m_originalPos;
        StopAllCoroutines();
        m_image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.position += m_offset;
        transform.localScale *= m_scaleoffset;
        SfxManager.PlaySfx("Click");
        CursorManager.Instance.SetCursorType(ECursorType.InteractUI);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.position -= m_offset;
        transform.localScale = m_originalScale;
        CursorManager.Instance.SetCursorType(ECursorType.Default);
    }
    public void StartBlinkingAnimation()
    {
        StartCoroutine(StartBlinking());
    }
    private IEnumerator StartBlinking()
    {
        
        while (true)
        {
            m_image.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            m_image.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            m_image.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            m_image.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
        
    }


}
