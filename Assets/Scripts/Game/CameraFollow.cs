using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour {

    CinemachineVirtualCamera vCamera;

    // Use this for initialization
    void Awake () {
        vCamera = GetComponent<CinemachineVirtualCamera>();
        
	}

    public void setFollow (Transform target) {
        vCamera.Follow = target;
    }
	
}
