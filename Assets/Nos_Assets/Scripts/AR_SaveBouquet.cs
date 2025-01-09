using UnityEngine;
using static AR_SwitchVase;

public class AR_SaveBouquet : MonoBehaviour
{
	public void SaveBouquet()
	{
		IntWrapper valueIWrapped = new IntWrapper(gameObject.GetComponent<AR_SwitchVase>().GetI());
		string vaseToData = JsonUtility.ToJson(valueIWrapped);
		string path = Application.persistentDataPath + "/vase.json";
		System.IO.File.WriteAllText(path, vaseToData);
		Debug.Log("Bouquet saved to " + path);
	}
}
