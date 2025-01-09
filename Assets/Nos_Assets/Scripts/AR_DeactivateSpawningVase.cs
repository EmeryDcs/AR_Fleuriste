using UnityEngine;
using UnityEngine.UI;

public class AR_DeactivateSpawningVase : MonoBehaviour
{
    public GameObject xROrigin;

	private void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(DeactivateSpawningVase);
	}

	private void DeactivateSpawningVase()
	{
		xROrigin.transform.GetComponent<AR_TapToPlace>().isPlacementValidated = true;
		transform.parent.gameObject.SetActive(false);
	}
}
