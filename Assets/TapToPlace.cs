using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class TapToPlace : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn;
    [SerializeField]
    private TMP_Text debugMessage;
    [SerializeField]
    private ARPlaneManager planeManager;

    private Vector2 touchPosition;
    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();

    }

    void Update()
    {

        if (planeManager.trackables.count <= 0)
            return;

        debugMessage.text = "Tap on plane to place object";

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                touchPosition = touch.position;

            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                debugMessage.text = "Position dans plan : " + hitPose.position;
                Instantiate(prefabToSpawn, hitPose.position, hitPose.rotation);
            }
            else
                debugMessage.text = "Position hors plan : " + touch.position;
        }
    }
}
