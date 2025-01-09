using UnityEngine;

public class AR_LookAtCamera : MonoBehaviour
{
	private Camera mainCamera;

	void Start()
	{
		// Trouver la cam�ra principale
		mainCamera = Camera.main;
	}

	void Update()
	{
		if (mainCamera != null)
		{
			// Faire en sorte que l'objet regarde toujours vers la cam�ra
			transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
		}
	}
}
