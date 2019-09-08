using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeZoomOnLevel : MonoBehaviour {

    //Start and finish of the zone where we need the zoom to change
    [Space]
    [Header("Transform Targets")]
    [SerializeField]
    private Transform leftTrasform;
    [SerializeField]
    private Transform rightTrasform;
    //Zoom we want as finished.
    [Space]
    [SerializeField]
    private float targetZoom;

    //Reference to the camera.
    private CinemachineVirtualCamera camera;

    //X position of the left transform.
    private float leftPosX;
    //X position of the right transform
    private float rightPosX;

    //Initial zoom of the camera.
    private float startZoom;

    //Gets the camera component
    private void Awake() {
        camera = GetComponent<CinemachineVirtualCamera>();

        leftPosX = leftTrasform.position.x;
        rightPosX = rightTrasform.position.x;

        startZoom = camera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update () {

        float relativePositionNormalized = Mathf.InverseLerp(leftPosX, rightPosX, transform.position.x);

        camera.m_Lens.OrthographicSize = Mathf.SmoothStep(startZoom, targetZoom, relativePositionNormalized);
	}
}
