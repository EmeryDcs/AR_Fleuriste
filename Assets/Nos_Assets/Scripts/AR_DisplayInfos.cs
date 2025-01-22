using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class AR_DisplayInfos : MonoBehaviour
{
    [Tooltip("Individual Info")]
    [SerializeField] private Transform individualInfoPanel;
    public TMP_Text individualFlowerDescription;
    public TMP_Text individualFlowerPrice;
    public TMP_Text individualFlowerName;

    [Tooltip("Bouquet Info")]
    [SerializeField] private Transform bouquetInfoPanel;
    public TMP_Text bouquetFlowerList;
    public TMP_Text bouquetFinalPrice;

    private BouquetGenerator bouquetGenerator;

    private void Start()
    {
        ResetInfos();

        
    }

    public void DisplayIndividualFlowerInfo(FlowerScriptableObject flowerData)
    {
        if (flowerData == null)
        {
            return;
        }

        individualInfoPanel.gameObject.SetActive(true);

        individualFlowerDescription.text = flowerData.description;
        float price = flowerData.price;
        individualFlowerPrice.text = price.ToString() + "€";
        individualFlowerName.text = flowerData.prefabName;
    }

    public void DisplayBouquetDetails()
    {
        // Initialize the bouquet generator reference
        if(bouquetGenerator == null)
        {
            bouquetGenerator = FindFirstObjectByType<BouquetGenerator>();
            if (bouquetGenerator == null)
            {
                Debug.LogError("BouquetGenerator is not found in the scene.");
            }
        }
        
        if (bouquetGenerator == null || bouquetGenerator.flowers == null)
        {
            Debug.LogError("Cannot display bouquet details: BouquetGenerator or flower list is null.");
            return;
        }

        bouquetInfoPanel.gameObject.SetActive(true);

        StringBuilder flowerListBuilder = new StringBuilder();
        float totalBouquetPrice = 0f;

        foreach (var flowerData in bouquetGenerator.flowers)
        {
            if (flowerData.flower != null && flowerData.quantity > 0)
            {
                flowerListBuilder.AppendLine($"{flowerData.flower.prefabName} (x{flowerData.quantity}) - {flowerData.flower.price}€");
                totalBouquetPrice += flowerData.flower.price * flowerData.quantity;
            }
        }

        bouquetFlowerList.text = flowerListBuilder.ToString();
        bouquetFinalPrice.text = $"Prix total : {totalBouquetPrice:F2}€";
    }

    public void ResetInfos()
    {
        individualFlowerDescription.text = "";
        individualFlowerPrice.text = "";
        individualFlowerName.text = "";
        bouquetFlowerList.text = "";
        bouquetFinalPrice.text = "";
    }

    public void CloseIndividualInfo()
    {
        individualInfoPanel.gameObject.SetActive(false);
        ResetIndividualInfo();
    }

    public void CloseBouquetInfo()
    {
        bouquetInfoPanel.gameObject.SetActive(false);
        ResetBouquetInfo();
    }

    private void ResetIndividualInfo()
    {
        individualFlowerDescription.text = "";
        individualFlowerPrice.text = "";
        individualFlowerName.text = "";
    }

    private void ResetBouquetInfo()
    {
        bouquetFlowerList.text = "";
        bouquetFinalPrice.text = "";
    }
}
