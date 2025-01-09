using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FlowerSelector : MonoBehaviour
{
    public FlowerScriptableObject flowerToDisplay;
    [SerializeField] int quantity;

    [SerializeField] Transform flowerPos;
    private BouquetGenerator bouquet;
    public TMP_Text quantityText;

    private void Awake()
    {
        // Recherche le BouquetGenerator
        bouquet = FindAnyObjectByType<BouquetGenerator>();

        if (bouquet == null)
        {
            Debug.LogError("BouquetGenerator non trouv�.");
            return;
        }

        // V�rifie si la fleur en question existe d�j� dans le bouquet
        if (flowerToDisplay != null)
        {
            FlowerData existingFlower = bouquet.flowers.Find(f => f.flower == flowerToDisplay);

            if (existingFlower != null)
            {
                // Met � jour la quantit� existante
                quantity = existingFlower.quantity;
            }

            // Instancie l'aper�u de la fleur dans l'interface
            Instantiate(flowerToDisplay.flowerPrefab, flowerPos);
        }

        // Met � jour l'interface pour refl�ter la quantit� actuelle
        quantityText.text = quantity.ToString();
    }


    public void IncreaseQuantity()
    {
        quantity += 1;
        quantityText.text = quantity.ToString();
        ValidateSelection();

    }

    public void DecreaseQuantity()
    {
        quantity -= 1;
        if(quantity < 0)
            quantity=0;
        quantityText.text = quantity.ToString();
        ValidateSelection();
    }

    public void ResetQuantity()
    {
        quantity = 0;
        quantityText.text = quantity.ToString();
        ValidateSelection();
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

        // Recharge le bouquet
        bouquet.GenerateBouqet();
    }
}
