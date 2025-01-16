using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Flower", order = 1)]
public class FlowerScriptableObject : ScriptableObject
{
    public string prefabName;
    public GameObject flowerPrefab;

    public string description;
    public float price;
}
