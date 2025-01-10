using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BouquetGenerator : MonoBehaviour
{
    public List<FlowerData> flowers = new List<FlowerData>();
    int angle;

    public void GenerateBouqet()
    {
        CleanBouquet();
		foreach (FlowerData flowerType in flowers)
        {
            for (int i = 0; i < flowerType.quantity; i++)
            {
                int thresold = 2;

                if (i > thresold)
                {
                    thresold += 3;
                    angle += 3;
                    thresold = (int)(thresold + Mathf.Round(1.3f));

                }

                Instantiate(flowerType.flower.flowerPrefab, transform.position + GetRandomOffset(0.01f), GetRandomAngle(40), this.transform);
            }
        }

		//Save bouquet
		AR_SaveBouquet saveBouquet = FindAnyObjectByType<AR_SaveBouquet>();
		saveBouquet.SaveFlowers(flowers);
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
        Vector3 offset = new Vector3(Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset)*2, Random.Range(-maxOffset, maxOffset));
        return offset;
    }

    private void Awake()
	{
		string path = Application.persistentDataPath + "/flowers.json";
		try
		{
			string flowersData = System.IO.File.ReadAllText(path);
			flowers = JsonUtility.FromJson<List<FlowerData>>(flowersData);
            foreach (FlowerData flower in flowers)
			{
				Debug.Log(flower.flower.name + " : " + flower.quantity);
			}
			GenerateBouqet();
		}
		catch
		{
			Debug.Log("Les fleurs n'ont jamais été sauvegardées");
            flowers = new List<FlowerData>();
			GenerateBouqet();
		}

		Debug.Log("Fleurs : " + flowers);
	}

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            flowers[0].quantity += 1;
            GenerateBouqet();
        }
    }
}


[System.Serializable]
public class FlowerData
{
    public FlowerScriptableObject flower;
    public int quantity;

    public FlowerData(FlowerScriptableObject flower, int quantity)
    {
        this.flower = flower;
        this.quantity = quantity;
    }
}
