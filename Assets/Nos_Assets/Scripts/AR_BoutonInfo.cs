using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AR_BoutonInfo : MonoBehaviour
{
    public FlowerScriptableObject flowerData;

    public void DisplayInfo(FlowerScriptableObject flowerData)
    {
        AR_DisplayInfos uIInfo = GameObject.FindAnyObjectByType<AR_DisplayInfos>();

        if (uIInfo != null && flowerData != null)
        {
            uIInfo.DisplayIndividualFlowerInfo(flowerData);
        }
    }

}
