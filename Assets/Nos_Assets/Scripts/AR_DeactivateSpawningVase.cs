using UnityEngine;
using UnityEngine.UI;

public class AR_DeactivateSpawningVase : MonoBehaviour
{
    public GameObject xROrigin;
	public GameObject uIBouquetAction;

	private void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(DeactivateSpawningVase);
	}

	private void DeactivateSpawningVase()
	{
		GameObject vaseSelector = GameObject.FindGameObjectWithTag("Vase Selector");
		if (vaseSelector != null)
		{
			vaseSelector.SetActive(false);

			xROrigin.transform.GetComponent<AR_TapToPlace>().isPlacementValidated = true;
			transform.parent.gameObject.SetActive(false);

			uIBouquetAction.SetActive(true);
		}
	}
}
