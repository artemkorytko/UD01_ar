using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FieldPositionController : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
    private ARAnchorManager _arAnchorManager;
    
    public bool IsActive { get; set; }

    public void Initialize(ARRaycastManager arRaycastManager, ARPlaneManager arPlaneManager, ARAnchorManager arAnchorManager)
    {
        _arAnchorManager = arAnchorManager;
        _arPlaneManager = arPlaneManager;
        _arRaycastManager = arRaycastManager;
    }

    private void Update()
    {
        if (_arRaycastManager == null || !IsActive) return;
        
        Vector2 screenCenterPosition = new Vector2(Screen.currentResolution.width * .5f, Screen.currentResolution.height * .5f);
        
        List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

        if (_arRaycastManager.Raycast(screenCenterPosition, arRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = arRaycastHits[0].pose;
            TrackableId hitId = arRaycastHits[0].trackableId;
            ARPlane arPlane = _arPlaneManager.GetPlane(hitId);
            ARAnchor arAnchor = _arAnchorManager.AttachAnchor(arPlane, pose);
            gameObject.transform.position = arAnchor.transform.position;
            gameObject.transform.rotation = pose.rotation;
        }
    }
}
