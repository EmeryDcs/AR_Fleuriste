using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AR_BoutonInfo : MonoBehaviour
{
    public FlowerScriptableObject flowerData;
    public TMP_Text uIInfoText;

    public void DisplayInfo()
    {
        GameObject uIInfo = GameObject.Find("UI Info Fleur");

        uIInfo.transform.GetChild(0).gameObject.SetActive(true);
        uIInfo.transform.GetChild(1).gameObject.SetActive(true);

        uIInfoText.text = flowerData.description;
    }
}
