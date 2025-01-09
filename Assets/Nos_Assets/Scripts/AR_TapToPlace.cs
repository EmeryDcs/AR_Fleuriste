using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class AR_TapToPlace : MonoBehaviour
{

    public GameObject objectToPlace;
	public bool isPlacementValidated = false;

	private GameObject spawnedObject;
    private ARRaycastManager arRaycastManager;
	private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

	void Awake()
    {
		arRaycastManager = GetComponent<ARRaycastManager>();
	}

	bool TryGetTouchPosition(out Vector2 touchPosition)
	{
		if (Input.touchCount > 0)
		{
			if (EventSystem.current.IsPointerOverGameObject()) { 
				touchPosition = default;
				return false;
			}

			touchPosition = Input.GetTouch(0).position;
			return true;
		}

		touchPosition = default;
		return false;
	}

	void Update()
    {
		if (isPlacementValidated || !TryGetTouchPosition(out Vector2 touchPosition))
			return;

		if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
		{
			var hitPose = hits[0].pose;

			if (spawnedObject == null)
			{
				spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
			}
			else
			{
				spawnedObject.transform.position = hitPose.position;
			}
		}

	}
}
