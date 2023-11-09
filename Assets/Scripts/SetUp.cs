using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SetUp : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn;
    [SerializeField]
    private TMP_Text debugMessage;
    [SerializeField]
    private GameObject collectible;
    [SerializeField]
    private ARPlaneManager planeManager;
    [SerializeField]
    private ARRaycastManager _arRaycastManager;

    private ARPlane LockedPlane;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool hasSpawn = false;


    void Update()
    {
       
        if (planeManager.trackables.count <= 0 || hasSpawn)
            return;

        debugMessage.text = "Tap on plane to place player";

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if(SpawnPlayer(touch.position))
                {
                    ARPlane plane = planeManager.GetPlane(hits[0].trackableId);
                    LockPlane(plane);
                    SpawnCollectibles();
                    GetComponent<Timer>().StartTimer();
                }
            }
        }
    }

    private bool SpawnPlayer(Vector2 touchPosition)
    {
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(prefabToSpawn, hitPose.position, hitPose.rotation);
            hasSpawn = true;
        }
        else
            debugMessage.text = "You taped outside the plane, try again";

        return hasSpawn;
    }

    public void LockPlane(ARPlane keepPlane)
    {
        foreach (var plane in planeManager.trackables)
        {
            if (plane != keepPlane)
            {
                plane.gameObject.SetActive(false);
            }
        }

        LockedPlane = keepPlane;
        _arRaycastManager.enabled = false;
        planeManager.enabled = false;
    }

    void SpawnCollectibles()
    {
        ReadCSV csvreader = GetComponent<ReadCSV>();
        if (csvreader.positions != null)
        {
            foreach (var pos in csvreader.positions)
            {
                pos.pos.y = LockedPlane.center.y;
                Instantiate(collectible, pos.pos, Quaternion.identity);
            }
            debugMessage.text = "Collectible spawn ! Go get them";

            if(csvreader.positions.Count <= 0)
                debugMessage.text = "Error spawning collectibles";
        }

    }


}
