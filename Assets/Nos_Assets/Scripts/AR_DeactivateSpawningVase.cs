using UnityEngine;
using UnityEngine.UI;

public class AR_DeactivateSpawningVase : MonoBehaviour
{
    public GameObject xROrigin;
	public GameObject uIFlowers;

	private void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(DeactivateSpawningVase);
	}

	private void DeactivateSpawningVase()
	{
		xROrigin.transform.GetComponent<AR_TapToPlace>().isPlacementValidated = true;
		transform.parent.gameObject.SetActive(false);
		uIFlowers.SetActive(true);
	}
}
