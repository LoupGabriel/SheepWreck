using UnityEngine;
using UnityEngine.EventSystems;

public class UiOutsideCollider : MonoBehaviour,IPointerClickHandler
{

 
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        UiPanelManager.Instance.CloseCurrentPanel();
    }

}
