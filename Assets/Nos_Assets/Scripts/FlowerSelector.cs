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

    private float searchDelay = 0.5f; // Intervalle pour la recherche du bouquet
    private float lastSearchTime;    // Dernier moment où une recherche a été effectuée

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (flowerToDisplay != null)
        {
            UpdateFlowerPreview();
        }

        SearchBouquet();

        if (bouquet == null)
        {
            Debug.LogWarning("BouquetGenerator non trouvé lors de l'initialisation.");
            return;
        }

        UpdateQuantityFromBouquet();
        UpdateQuantityDisplay();
    }

    private void Update()
    {
        if (bouquet == null && Time.time - lastSearchTime > searchDelay)
        {
            SearchBouquet();
            if (bouquet != null)
            {
                Init();
            }
        }
    }

    private void SearchBouquet()
    {
        bouquet = FindAnyObjectByType<BouquetGenerator>();
        lastSearchTime = Time.time;
    }

    private void UpdateFlowerPreview()
    {
        // Détruit les anciens prefabs pour éviter les doublons
        foreach (Transform child in flowerPos)
        {
            Destroy(child.gameObject);
        }

        // Instancie le nouveau prefab
        Instantiate(flowerToDisplay.flowerPrefab, flowerPos);
    }

    private void UpdateQuantityFromBouquet()
    {
        if (bouquet != null && flowerToDisplay != null)
        {
            FlowerData existingFlower = bouquet.flowers.Find(f => f.flower == flowerToDisplay);
            quantity = existingFlower != null ? existingFlower.quantity : 0;
        }
    }

    private void UpdateQuantityDisplay()
    {
        quantityText.text = quantity.ToString();
    }

    public void IncreaseQuantity()
    {
        quantity++;
        UpdateQuantityDisplay();
        ValidateSelection();
    }

    public void DecreaseQuantity()
    {
        quantity = Mathf.Max(0, quantity - 1);
        UpdateQuantityDisplay();
        ValidateSelection();
    }

    public void ResetQuantity()
    {
        quantity = 0;
        UpdateQuantityDisplay();
        ValidateSelection();
    }

    public void ValidateSelection()
    {
        if (bouquet == null)
        {
            Debug.LogError("BouquetGenerator non trouvé.");
            return;
        }

        if (flowerToDisplay == null)
        {
            Debug.LogWarning("Aucune fleur sélectionnée.");
            return;
        }

        FlowerData existingFlower = bouquet.flowers.Find(f => f.flower == flowerToDisplay);

        if (quantity == 0)
        {
            if (existingFlower != null)
            {
                bouquet.flowers.Remove(existingFlower);
            }
        }
        else
        {
            if (existingFlower != null)
            {
                existingFlower.quantity = quantity;
            }
            else
            {
                bouquet.flowers.Add(new FlowerData(flowerToDisplay, quantity));
            }
        }

        bouquet.GenerateBouquet();
    }
}
