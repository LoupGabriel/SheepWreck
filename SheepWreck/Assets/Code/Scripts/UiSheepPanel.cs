using TMPro;
using UnityEngine;

public class UiSheepPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_sheepName;

  
    public void EnableSheepPanel(string sheepName)
    {


        gameObject.SetActive(true);
        m_sheepName.text = sheepName;




    }

    public void CloseSheepPanel()
    {
        gameObject.SetActive(false);

    }
}
