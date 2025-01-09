using System.Collections.Generic;
using UnityEngine;

public class BouquetGenerator : MonoBehaviour
{
    public List<FlowerData> flowers = new List<FlowerData>();

    [Header("Bouquet Settings")]
    public float baseRadius = 0.1f;  // Rayon de base pour le placement des fleurs
    public float heightVariance = 0.05f; // Variation verticale pour simuler un effet de bouquet
    public float randomOffset = 0.02f; // Offset aléatoire pour chaque fleur

    public void GenerateBouquet()
    {
        CleanBouquet();

        foreach (FlowerData flowerType in flowers)
        {
            if (flowerType.quantity <= 0) continue;

            // Calcule le rayon spécifique pour ce type de fleur
            float flowerRadius = baseRadius + flowerType.quantity * 0.0001f;

            for (int i = 0; i < flowerType.quantity; i++)
            {
                // Calcule une position circulaire autour du point central
                float angle = (360f / flowerType.quantity) * i; // Divise uniformément les fleurs en cercle
                Vector3 position = GetPositionOnCircle(angle, flowerRadius);

                // Applique une variation aléatoire à la hauteur et à la position
                position.y += Random.Range(-heightVariance, heightVariance);
                position += GetRandomOffset(randomOffset);

                // Instancie la fleur avec une rotation aléatoire
                Instantiate(
                    flowerType.flower.flowerPrefab,
                    transform.position + position,
                    GetRandomAngle(25),
                    this.transform
                );
            }
        }
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
            Random.Range(-maxOffset, maxOffset),
            Random.Range(-maxOffset, maxOffset)
        );
    }

    private Vector3 GetPositionOnCircle(float angle, float radius)
    {
        // Convertit l'angle en radians et calcule la position sur un cercle
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
    }

    private void Start()
    {
        GenerateBouquet();
    }

    private void OnDrawGizmos()
    {
        // Couleur pour le rayon de base
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, baseRadius);

        if (flowers == null || flowers.Count == 0) return;

        foreach (FlowerData flowerType in flowers)
        {
            if (flowerType.quantity <= 0) continue;

            // Calcule le rayon spécifique pour ce type de fleur
            float flowerRadius = baseRadius + flowerType.quantity * 0.01f;

            for (int i = 0; i < flowerType.quantity; i++)
            {
                // Calcule une position circulaire autour du point central
                float angle = (360f / flowerType.quantity) * i;
                Vector3 position = GetPositionOnCircle(angle, flowerRadius);

                // Applique une variation de hauteur
                position.y += Random.Range(-heightVariance, heightVariance);

                // Calcule la position globale dans le monde
                Vector3 worldPosition = transform.position + position;

                // Dessine une sphère pour représenter la position de la fleur
                Gizmos.color = flowerType.flower != null ? Color.cyan : Color.red;
                Gizmos.DrawSphere(worldPosition, 0.02f);
            }
        }

        // Dessine un repère central pour l'origine du bouquet
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.05f);
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
