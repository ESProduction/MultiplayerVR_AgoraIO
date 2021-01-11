using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;
using Photon.Pun;

public class VRControllerTracking : MonoBehaviourPunCallbacks
{

	[SerializeField] UnityEngine.XR.XRNode whichNode = UnityEngine.XR.XRNode.LeftHand;

		// Use this for initialization
	void Start () {

		if (UnityEngine.XR.XRSettings.enabled == false) {
			Destroy (this);
		}

	}
	
	// Update is called once per frame
	void Update () {
        if (!photonView.IsMine) return;
		// For now, Only depend on parent node. So, it is ok if rotation of parent is not 0.
		transform.localPosition = UnityEngine.XR.InputTracking.GetLocalPosition (whichNode);
		transform.localRotation = UnityEngine.XR.InputTracking.GetLocalRotation (whichNode);
	}
}
