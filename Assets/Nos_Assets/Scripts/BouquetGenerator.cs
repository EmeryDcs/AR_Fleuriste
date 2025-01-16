using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class BouquetGenerator : MonoBehaviour
{
    public List<FlowerData> flowers = new List<FlowerData>();
    public FlowerDataList listFlowers;
    public float totalPrice; 
	int angle;

    [Header("Bouquet Settings")]
    public float baseRadius = 0.1f;  // Rayon de base pour le placement des fleurs
    public float heightVariance = 0.05f; // Variation verticale pour simuler un effet de bouquet
    public float randomOffset = 0.02f; // Offset al�atoire pour chaque fleur

    public void GenerateBouquet()
    {
        CleanBouquet();

        foreach (FlowerData flowerType in flowers)
        {
            if (flowerType.quantity <= 0) continue;

            // Calcule le rayon sp�cifique pour ce type de fleur
            float flowerRadius = baseRadius + flowerType.quantity * 0.0001f;

            for (int i = 0; i < flowerType.quantity; i++)
            {
                // Calcule une position circulaire autour du point central
                float angle = (360f / flowerType.quantity) * i; // Divise uniform�ment les fleurs en cercle
                Vector3 position = GetPositionOnCircle(angle, flowerRadius);

                // Applique une variation al�atoire � la hauteur et � la position
                position.y += Random.Range(-heightVariance, heightVariance);
                position += GetRandomOffset(randomOffset);

				// Instancie la fleur avec une rotation al�atoire
				Instantiate(
					flowerType.flower.flowerPrefab,
					transform.position + position,
					GetRandomAngle(25),
					this.transform
				);
			}
		}

        CalculateTotalPrice();

		//Save bouquet
		AR_SaveBouquet saveBouquet = FindAnyObjectByType<AR_SaveBouquet>();
		listFlowers.flowers = flowers;
		saveBouquet.SaveFlowers(listFlowers);
	}

    private void CalculateTotalPrice()
    {
        totalPrice = 0f;

        foreach (FlowerData flowerType in flowers)
        {
            if (flowerType.flower != null)
            {
                totalPrice += flowerType.flower.price * flowerType.quantity;
            }
        }

        Debug.Log($"Prix total du bouquet : {totalPrice} €");
    }

    public void CleanBouquet()
    {
        if (this.transform.childCount == 0) return;

        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    private Quaternion GetRandomAngle(int maxAngle)
    {
        return Quaternion.Euler(
            Random.Range(-maxAngle, maxAngle),
            Random.Range(0, 359),
            Random.Range(-maxAngle, maxAngle)
        );
    }

    private Vector3 GetRandomOffset(float maxOffset)
    {
        return new Vector3(
            Random.Range(-maxOffset, maxOffset),
            0,
            Random.Range(-maxOffset, maxOffset)
        );
    }

    private Vector3 GetPositionOnCircle(float angle, float radius)
    {
        // Convertit l'angle en radians et calcule la position sur un cercle
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
    }

    private void Awake()
	{
		string path = Application.persistentDataPath + "/flowers.json";
		try
		{
			string flowersData = System.IO.File.ReadAllText(path);
			listFlowers = JsonUtility.FromJson<FlowerDataList>(flowersData);
			flowers = listFlowers.flowers;
			GenerateBouquet();
		}
		catch
		{
            flowers = new List<FlowerData>();
			GenerateBouquet();
		}
	}
}

[System.Serializable]
public class FlowerData
{
    public string name;
    public FlowerScriptableObject flower;
    public int quantity;

    public FlowerData(FlowerScriptableObject flower, int quantity)
    {
        this.name = flower.name;
        this.flower = flower;
        this.quantity = quantity;
    }
}

[System.Serializable]
public class FlowerDataList
{
	public List<FlowerData> flowers;

    public FlowerDataList(List<FlowerData> flowers)
	{
		this.flowers = flowers;
	}
}