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
        if (flowerToDisplay != null)
        {
            // Instancie l'aperçu de la fleur dans l'interface
            Instantiate(flowerToDisplay.flowerPrefab, flowerPos);
        }
            // Recherche le BouquetGenerator
        bouquet = FindAnyObjectByType<BouquetGenerator>();

        if (bouquet == null)
        {
            Debug.LogError("BouquetGenerator non trouvé.");
            return;
        }

        // Vérifie si la fleur en question existe déjà dans le bouquet
        if (flowerToDisplay != null)
        {
            FlowerData existingFlower = bouquet.flowers.Find(f => f.flower == flowerToDisplay);

            if (existingFlower != null)
            {
                // Met à jour la quantité existante
                quantity = existingFlower.quantity;
            }

        }

        // Met à jour l'interface pour refléter la quantité actuelle
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
        // Vérifie si le bouquet existe
        if (bouquet == null)
        {
            Debug.LogError("BouquetGenerator non trouvé.");
            return;
        }

        // Vérifie si une fleur est sélectionnée
        if (flowerToDisplay == null)
        {
            Debug.LogWarning("Aucune fleur sélectionnée.");
            return;
        }

        // Cherche une fleur existante dans le bouquet
        FlowerData existingFlower = bouquet.flowers.Find(f => f.flower == flowerToDisplay);

        if (quantity == 0)
        {
            // Si la quantité est 0, supprime la fleur du bouquet
            if (existingFlower != null)
            {
                bouquet.flowers.Remove(existingFlower);
            }
        }
        else
        {
            // Si la fleur existe déjà, met à jour la quantité
            if (existingFlower != null)
            {
                existingFlower.quantity = quantity;
            }
            else
            {
                // Sinon, ajoute une nouvelle fleur
                bouquet.flowers.Add(new FlowerData(flowerToDisplay, quantity));
            }
        }

        // Recharge le bouquet
        bouquet.GenerateBouquet();
    }
}
