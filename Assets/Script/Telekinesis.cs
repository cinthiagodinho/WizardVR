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
    Transform playerHandL;

    //Variables for telekinesis
    private GameObject grabbedObject;
    private bool telekinesisOn = false;
    Vector3 controllerVelocity;
    Quaternion lastRotation;
    Quaternion currentRotation;
    public float speed;
    private Color baseMeshColor;

    //Debug Line
    LineRenderer lineRenderer = null;

    void Awake()
    {
        /* if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.02f;
        }*/
    }
    void Start()
    {
        rig = FindObjectOfType<VRGestureRig>();
        if (rig == null)
        {
            Debug.Log("there is no VRGestureRig in the scene, please add one");
        }

        playerHandL = rig.handLeft;

        input = rig.GetInput(rig.mainHand);
    }
    void Update()
    {
        //For testing
        /* Vector3 begin = playerHandL.transform.position + playerHandL.transform.forward * 1f;
          Ray pointer = new Ray(begin, playerHandL.forward);

          if (lineRenderer != null)
          {
              lineRenderer.SetPosition(0, pointer.origin);
              lineRenderer.SetPosition(1, pointer.origin + pointer.direction * 20);
          }

          RaycastHit hit;

          if (Physics.Raycast(pointer, out hit, 20))
          {
              if (lineRenderer != null)
              {
                  lineRenderer.SetPosition(1, hit.point);
              }
          }*/

        if (!telekinesisOn && Input.GetAxis(buttonName) == 1)
            GrabObject();

        else if (telekinesisOn && Input.GetAxis(buttonName) < 1)
        {
            DropObject();
        }

        else if (telekinesisOn && grabbedObject != null)
        {
            controllerVelocity = OVRInput.GetLocalControllerVelocity(controller);
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;

            if (playerHandL.gameObject.transform.localRotation.x > 0.30f)
                grabbedObject.transform.position += new Vector3(speed, 0, 0);

            else if (playerHandL.transform.localRotation.x < -0.30f)
            {
                grabbedObject.transform.position -= new Vector3(speed, 0, 0);
            }

            else if (playerHandL.transform.localRotation.x == 0)
            {
                grabbedObject.transform.position = Vector3.MoveTowards(grabbedObject.transform.position, playerHandL.transform.position + (playerHandL.transform.forward * 3), 2 * Time.deltaTime);
            }
        }
    }

    void GrabObject()
    {
        telekinesisOn = true;
        RaycastHit hit;
        Vector3 fwd = playerHandL.transform.TransformDirection(Vector3.forward);
        Vector3 begin = playerHandL.transform.position + playerHandL.transform.forward * 0.9f;

        if (Physics.Raycast(begin, fwd, out hit, 30))
        {
            if (hit.transform.gameObject.tag == "Interactable")
            {
                telekinesisOn = true;
                grabbedObject = hit.transform.gameObject;
                grabbedObject.transform.parent = playerHandL.transform;
                //baseMeshColor = grabbedObject.GetComponent<MeshRenderer>().material.color;
                //grabbedObject.GetComponent<MeshRenderer>().material.color = Color.blue;
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
            //grabbedObject.GetComponent<MeshRenderer>().material.color = baseMeshColor;
            grabbedObject = null;
        }
    }
    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
}
