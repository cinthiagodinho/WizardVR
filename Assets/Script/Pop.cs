using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;
using Edwon.VR.Input;

public class Pop : MonoBehaviour
{
    public GameObject obj;
    VRGestureRig rig;
    IInput input;

    Transform playerHead;
    Transform playerHandL;
    Transform playerHandR;

    private float speed = 2.0f;

    void Start()
    {
        rig = FindObjectOfType<VRGestureRig>();
        if (rig == null)
        {
            Debug.Log("there is no VRGestureRig in the scene, please add one");
        }

        playerHead = rig.head;
        playerHandR = rig.handRight;
        playerHandL = rig.handLeft;

        input = rig.GetInput(rig.mainHand);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            PopObject();
    }
    void FixedUpdate()
    {
        float h = speed * Input.GetAxis("Horizontal");
        float v = -speed * Input.GetAxis("Vertical");
        transform.Rotate(v, h, 0);

    }
    void PopObject()
    {
        Quaternion rotation = Quaternion.LookRotation(playerHandR.forward, Vector3.up);
        Vector3 betweenHandsPos = (playerHandL.position + playerHandR.position) / 2;
        Vector3 firePos = new Vector3(betweenHandsPos.x, betweenHandsPos.y, (betweenHandsPos.z + 2f));
        GameObject fireInstance = GameObject.Instantiate(obj, firePos, rotation) as GameObject;
    }
}
