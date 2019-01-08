using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingInput : MonoBehaviour {
	
	
	void Update () {
		
			Debug.Log("Button pressed - "+ OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch));
			if(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) > 0)
				Debug.Log("Trigger pressed");
			else{
				Debug.Log("Trigger released");
			}
		
	}
}
