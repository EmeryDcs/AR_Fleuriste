using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AR_SwitchVase : MonoBehaviour
{
	public List<GameObject> prefabsVase = new List<GameObject>();
	public Button boutonGauche;
	public Button boutonDroite;
		
	private int i = 0;

	void Awake()
	{
		//Listener sur les boutons
		boutonGauche.onClick.AddListener(ChangerVaseGauche);
		boutonDroite.onClick.AddListener(ChangerVaseDroite);

		//Chargement du vase
		string path = Application.persistentDataPath + "/vase.json";
		try
		{
			string iData = System.IO.File.ReadAllText(path);

			SetI(JsonUtility.FromJson<IntWrapper>(iData));
		}
		catch
		{
			Debug.Log("Le vase n'a jamais été sauvegardé");
		}
	}

	void ChangerVaseGauche()
	{
		if (i > 0)
		{
			i--;
			for (int j = 0; j < prefabsVase.Count; j++)
			{
				prefabsVase[j].SetActive(false);
				if (j == i)
				{
					prefabsVase[j].SetActive(true);
				}
			}
		}

		GetComponent<AR_SaveBouquet>().SaveVase();
	}

	void ChangerVaseDroite()
	{
		if (i < prefabsVase.Count-1)
		{
			i++;
			for (int j = 0; j < prefabsVase.Count; j++)
			{
				prefabsVase[j].SetActive(false);
				if (j == i)
				{
					prefabsVase[j].SetActive(true);
				}
			}
		}

		GetComponent<AR_SaveBouquet>().SaveVase();
	}
	public int GetI()
	{
		return i;
	}

	//Cette classe est nécessaire pour convertir un int en un objet convertissable en JSON.
	[System.Serializable]
	public class IntWrapper
	{
		public int value;

		public IntWrapper(int value)
		{
			this.value = value;
		}
	}

	public void SetI(IntWrapper intWrapped)
	{
		i = intWrapped.value;

		for (int j = 0; j < prefabsVase.Count; j++)
		{
			prefabsVase[j].SetActive(false);
			if (j == i)
			{
				prefabsVase[j].SetActive(true);
			}
		}
	}
}
