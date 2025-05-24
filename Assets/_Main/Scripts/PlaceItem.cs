using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class PlaceItem : MonoBehaviour
{
    public Transform placeCircle;
    public ARRaycastManager arRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public bool isPlaced;

    private void Update()
    {
        if (isPlaced) return;

        if(arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            placeCircle.position = hitPose.position;
            if(!placeCircle.gameObject.activeSelf)
            {
                placeCircle.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (placeCircle.gameObject.activeSelf)
            {
                isPlaced = true;
                GameManager.Instance.Init(placeCircle.position);
                placeCircle.gameObject.SetActive(false);
            }
        }
    }
}
