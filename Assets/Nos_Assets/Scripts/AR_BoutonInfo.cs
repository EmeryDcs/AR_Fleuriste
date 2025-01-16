using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AR_BoutonInfo : MonoBehaviour
{
    public FlowerScriptableObject flowerData;
    public GameObject uIInfo;
    public TMP_Text uIInfoText;

    public void DisplayInfo()
    {
        uIInfo.SetActive(true);
        uIInfoText.text = flowerData.description;
    }
}
