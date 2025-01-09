using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AR_SwitchVase : MonoBehaviour
{
	public List<GameObject> prefabsVase = new List<GameObject>();
	public Button boutonGauche;
	public Button boutonDroite;

	private int i = 0;

	void Start()
	{
		boutonGauche.onClick.AddListener(ChangerVaseGauche);
		boutonDroite.onClick.AddListener(ChangerVaseDroite);
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
	}
}
