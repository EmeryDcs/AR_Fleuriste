using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AR_DeactivateBorderPlaneDetected : MonoBehaviour
{
    public Material materialTransparent;
    public GameObject xROrigin;

    public void DeactivateBorderPlaneDetected()
	{
		//on cherche tous les objets qui ont été trackés en tant que ARPlane
		ARPlane[] gameObjects = FindObjectsByType<ARPlane>(FindObjectsSortMode.None);

        foreach (ARPlane plane in gameObjects)
        {
            //on remplace la ligne noire par un matériau transparent
            plane.gameObject.GetComponent<MeshRenderer>().material = materialTransparent;
            //plane.gameObject.GetComponent<LineRenderer>().material = materialTransparent;
		}

		xROrigin.GetComponent<ARPlaneManager>().enabled = false;
		Debug.Log("ARPlaneManager enabled :" + xROrigin.GetComponent<ARPlaneManager>().enabled);
	}
}
