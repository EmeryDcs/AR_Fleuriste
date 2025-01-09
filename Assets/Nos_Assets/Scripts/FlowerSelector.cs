using UnityEngine;
using UnityEngine.UI;

public class FlowerSelector : MonoBehaviour
{
    public FlowerScriptableObject flowerToDisplay;

    [SerializeField] Transform flowerPos;
    [SerializeField] int quantity;

    [Header("UI Config")]
    public Text quantityText;

    private void Awake()
    {
        if (flowerToDisplay)
        {
            Instantiate(flowerToDisplay.flowerPrefab, flowerPos);

        }
        quantityText.text = quantity.ToString();
    }


    public void IncreaseQuantity()
    {
        quantity += 1;
        quantityText.text = quantity.ToString();
    }

    public void DecreaseQuantity()
    {
        quantity -= 1;
        quantityText.text = quantity.ToString();
    }
}
