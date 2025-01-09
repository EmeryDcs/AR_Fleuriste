using UnityEngine;
using UnityEngine.UI;

public class FlowerSelector : MonoBehaviour
{
    public FlowerScriptableObject flowerToDisplay;

    [SerializeField] Transform flowerPos;
    [SerializeField] int quantity;

    private BouquetGenerator bouquet;

    [Header("UI Config")]
    public Text quantityText;

    private void Awake()
    {
        bouquet = FindAnyObjectByType<BouquetGenerator>();

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
        if(quantity < 0)
            quantity=0;
        quantityText.text = quantity.ToString();
    }

    public void ResetQuantity()
    {
        quantity = 0;
        quantityText.text = quantity.ToString();
    }

    public void ValidateSelection()
    {
        // V�rifie si le bouquet existe
        if (bouquet == null)
        {
            Debug.LogError("BouquetGenerator non trouv�.");
            return;
        }

        // V�rifie si une fleur est s�lectionn�e
        if (flowerToDisplay == null)
        {
            Debug.LogWarning("Aucune fleur s�lectionn�e.");
            return;
        }

        if (quantity == 0)
            return;

        // Cherche une fleur existante dans le bouquet
        FlowerData existingFlower = bouquet.flowers.Find(f => f.flower == flowerToDisplay);

        if (existingFlower != null)
        {
            // Si la fleur existe d�j�, met � jour la quantit�
            existingFlower.quantity += quantity;
        }
        else
        {
            // Sinon, ajoute une nouvelle fleur
            bouquet.flowers.Add(new FlowerData(flowerToDisplay, quantity));
        }

        //Reinitialise la quantit� � ajouter;
        ResetQuantity();
        // Recharge le bouquet
        bouquet.GenerateBouqet();
    }
}
