using UnityEngine;

public class AR_ResetBouquet : MonoBehaviour
{
	public AR_TapToPlace tapToPlace;
	public GameObject UIPlacement;
	private BouquetGenerator bouquet;
	private AR_SaveBouquet saveBouquet;

	public void ResetBouquet()
	{
		bouquet = FindAnyObjectByType<BouquetGenerator>();
		saveBouquet = FindAnyObjectByType<AR_SaveBouquet>();

		//On réactive la possibilité de placer l'objet et on réaffiche l'UI de placement
		tapToPlace.isPlacementValidated = false;
		UIPlacement.SetActive(true);

		//On réinitialise le bouquet
		bouquet.CleanBouquet();
		bouquet.flowers.Clear();
		saveBouquet.SaveFlowers(new FlowerDataList(bouquet.flowers));

		//On détruit l'objet vase
		GameObject vase = GameObject.FindGameObjectWithTag("Vase");
		Destroy(vase);
	}
}
