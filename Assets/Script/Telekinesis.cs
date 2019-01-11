using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;
using Edwon.VR.Input;

public class Telekinesis : MonoBehaviour
{
    //Controller
    public OVRInput.Controller controller;
    public string buttonName;
    VRGestureRig rig;
    IInput input;
    Transform playerHead;
    Transform playerHandL;
    Transform playerHandR;

    //Variables for telekinesis
    private GameObject grabbedObject;
    private bool telekinesisOn = false;
    Vector3 controllerVelocity;
    Quaternion lastRotation;
    Quaternion currentRotation;
    private Color baseMeshColor;

    //Debug Line
    LineRenderer lineRenderer = null;

    void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.02f;
        }
    }
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
        Ray pointer = new Ray(playerHandL.position, playerHandL.forward);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, pointer.origin);
            lineRenderer.SetPosition(1, pointer.origin + pointer.direction * 500.0f);
        }

        RaycastHit hit;
        if (Physics.Raycast(pointer, out hit, 500.0f))
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }
        }

        if (!telekinesisOn && Input.GetAxis(buttonName) == 1)
        {
            GrabObject();
        }
        else if (telekinesisOn && Input.GetAxis(buttonName) < 1)
        {
            DropObject();
        }

        else if (telekinesisOn && grabbedObject != null)
        {
            controllerVelocity = OVRInput.GetLocalControllerVelocity(controller);
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;

            if (playerHandL.transform.localRotation.z > 0.30f)
                grabbedObject.transform.position += new Vector3(0, 0, 0.02f);

            if (playerHandL.transform.localRotation.z < -0.30f)
                grabbedObject.transform.position -= new Vector3(0, 0, 0.02f);
        }

    }

    void GrabObject()
    {
        telekinesisOn = true;
        RaycastHit hit;
        Vector3 fwd = playerHandL.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(playerHandL.transform.position, fwd, out hit, 30))
        {
            if (hit.transform.gameObject.tag == "Interactable")
            {
                telekinesisOn = true;
                grabbedObject = hit.transform.gameObject;
                grabbedObject.transform.parent = playerHandL.transform;
                baseMeshColor = grabbedObject.GetComponent<MeshRenderer>().material.color;
                grabbedObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

    }

    void DropObject()
    {
        telekinesisOn = false;
        if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().velocity = controllerVelocity * 5;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();
            grabbedObject.GetComponent<MeshRenderer>().material.color = baseMeshColor;
            grabbedObject = null;
        }
    }
    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
}
