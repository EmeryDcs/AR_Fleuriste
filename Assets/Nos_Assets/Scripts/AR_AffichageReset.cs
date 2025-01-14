using UnityEngine;
using UnityEngine.UI;

public class AR_AffichageReset : MonoBehaviour
{
    private GameObject resetButton;

    void Start()
    {
		resetButton = GameObject.FindGameObjectWithTag("Reset Button");
        resetButton.transform.GetChild(0).gameObject.SetActive(true);
	}

	private void OnDestroy()
	{
		resetButton.transform.GetChild(0).gameObject.SetActive(false);
	}
}
