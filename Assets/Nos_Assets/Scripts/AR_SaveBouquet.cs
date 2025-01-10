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

	public void SaveFlowers(FlowerDataList listFlowers)
	{
		//Conversion de la liste flowers en string JSON
		string flowersToData = JsonUtility.ToJson(listFlowers);

		//Sauvegarde du bouquet
		string path = Application.persistentDataPath + "/flowers.json";
		System.IO.File.WriteAllText(path, flowersToData);
		Debug.Log("Flowers saved to " + path);
	}
}
