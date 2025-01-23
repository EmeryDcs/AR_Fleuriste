using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AR_ResetBouquet : MonoBehaviour
{
	public AR_TapToPlace tapToPlace;
	public GameObject UIPlacement;
	public GameObject xROrigin;
	public Material materialMesh;
	public Material materialLine;
	private BouquetGenerator bouquet;
	private AR_SaveBouquet saveBouquet;

	public void ResetBouquet()
	{
		bouquet = FindAnyObjectByType<BouquetGenerator>();
		saveBouquet = FindAnyObjectByType<AR_SaveBouquet>();

		//On réactive la possibilité de placer l'objet et on réaffiche l'UI de placement
		tapToPlace.isPlacementValidated = false;
		UIPlacement.SetActive(true);
		xROrigin.GetComponent<ARPlaneManager>().enabled = true;
		Debug.Log(xROrigin.GetComponent<ARPlaneManager>().enabled);
		//on cherche tous les objets qui ont été trackés en tant que ARPlane
		ARPlane[] gameObjects = FindObjectsByType<ARPlane>(FindObjectsSortMode.None);

		foreach (ARPlane plane in gameObjects)
		{
			//on remplace la ligne noire par un matériau transparent
			plane.gameObject.GetComponent<MeshRenderer>().material = materialMesh;
			//plane.gameObject.GetComponent<LineRenderer>().material = materialLine;
		}

		xROrigin.GetComponent<ARPlaneManager>().enabled = true;

		//On réinitialise le bouquet
		bouquet.CleanBouquet();
		bouquet.flowers.Clear();
		saveBouquet.SaveFlowers(new FlowerDataList(bouquet.flowers));

		//On détruit l'objet vase
		GameObject vase = GameObject.FindGameObjectWithTag("Vase");
		Destroy(vase);
	}
}
