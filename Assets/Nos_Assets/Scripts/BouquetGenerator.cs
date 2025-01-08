using System.Collections.Generic;
using UnityEngine;

public class BouquetGenerator : MonoBehaviour
{
    [SerializeField] List<Flower> flowers = new List<Flower>();
    [SerializeField] List<GameObject> folliage = new List<GameObject>();

    int angle;


    public void GenerateBouqet()
    {
        CleanBouquet();
        foreach (Flower flower in flowers)
        {
            for (int i = 0; i < flower.quantity; i++)
            {
                int thresold = 2;

                if (i > thresold)
                {
                    angle += 3;
                    thresold = (int)(thresold + Mathf.Round(1.3f));

                }

                Instantiate(flower.flowerPrefab, transform.position + GetRandomOffset(0.01f), GetRandomAngle(40), this.transform);
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
        Quaternion rot = Quaternion.Euler(Random.Range(-maxAngle, maxAngle), Random.Range(0, 359), Random.Range(-maxAngle, maxAngle) );

        return rot;
    }

    private Vector3 GetRandomOffset(float maxOffset)
    {
        Vector3 offset = new Vector3(Random.Range(-maxOffset, maxOffset), 0, Random.Range(-maxOffset, maxOffset));
        return offset;
    }

    private void Start()
    {
        GenerateBouqet();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            flowers[1].quantity += 1;
            GenerateBouqet();
        }
    }
}


[System.Serializable]
public class Flower
{
    public string name;
    public GameObject flowerPrefab;
    public int quantity;
}
