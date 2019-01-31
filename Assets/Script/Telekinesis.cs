using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    float prevPosition = 0;
    float curPosition = 0;

    //Debug Line
    LineRenderer lineRenderer = null;
    public Text debug;

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
        if (Game.authorizeSpell)
        {
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

                /*  if (playerHandL.gameObject.transform.localPosition.z <= 0.9f)
                      grabbedObject.transform.position -= new Vector3(speed, 0, 0);

                  else if (playerHandL.transform.localPosition.z >= 1.2f)
                  {
                      grabbedObject.transform.position += new Vector3(speed, 0, 0);
                  }*/
            }
        }
    }

    void GrabObject()
    {
        telekinesisOn = true;
        RaycastHit hit;
        Vector3 fwd = playerHandL.transform.TransformDirection(Vector3.forward);
        Vector3 begin = playerHandL.transform.position + playerHandL.transform.forward * 0.3f;

        if (Physics.Raycast(begin, fwd, out hit, 30))
        {
            if (hit.transform.gameObject.tag == "Interactable")
            {
                telekinesisOn = true;
                grabbedObject = hit.transform.gameObject;
                grabbedObject.transform.parent = playerHandL.transform;
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
            grabbedObject = null;
        }
    }
    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
}
