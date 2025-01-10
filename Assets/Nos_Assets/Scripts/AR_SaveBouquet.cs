using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static AR_SwitchVase;

public class AR_SaveBouquet : MonoBehaviour
{
	public void SaveVase()
	{
		//Conversion du numéro du prefab sélectionner pour être sauvegardé en JSON
		IntWrapper valueIWrapped = new IntWrapper(gameObject.GetComponent<AR_SwitchVase>().GetI());
		string vaseToData = JsonUtility.ToJson(valueIWrapped);

		//Sauvegarde du vase
		string path = Application.persistentDataPath + "/vase.json";
		System.IO.File.WriteAllText(path, vaseToData);
		Debug.Log("Vase saved to " + path);
	}

	public void SaveFlowers(List<FlowerData> flowers)
	{
		string strFlower = "";

		foreach (FlowerData flower in flowers)
		{
			strFlower += flower.ToString();
		}
		Debug.Log(strFlower);

		//Conversion de la liste flowers en string JSON
		string flowersToData = "";
		foreach (FlowerData flower in flowers)
		{
			flowersToData += JsonUtility.ToJson(flower);
		}
		Debug.Log("Flowers in JSON : " + flowersToData);

		//Sauvegarde du bouquet
		string path = Application.persistentDataPath + "/flowers.json";
		System.IO.File.WriteAllText(path, flowersToData);
		Debug.Log("Flowers saved to " + path);
	}
}
