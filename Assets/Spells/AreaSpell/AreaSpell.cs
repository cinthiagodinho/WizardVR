using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;
using Edwon.VR.Input;

public class AreaSpell : MonoBehaviour
{
    public OVRInput.Controller controller;
    public string buttonName;
    Rigidbody rb;
    public float timeTillDeath;
    private bool validated = false;

    public GameObject damages;
    public Material matInProgress;
    public Material matValidated;
    public float speed;

    VRGestureRig rig;
    IInput input;
    Transform playerHandR;

    void Start()
    {
        rig = FindObjectOfType<VRGestureRig>();
        if (rig == null)
        {
            Debug.Log("there is no VRGestureRig in the scene, please add one");
        }

        playerHandR = rig.handRight;

        input = rig.GetInput(rig.mainHand);
    }
    void Update()
    {
        if (Input.GetAxis(buttonName) == 1)
            validated = true;

        if (validated)
        {
            gameObject.GetComponent<MeshRenderer>().material = matValidated;
            StartCoroutine(DestroySelf());
            gameObject.transform.parent = null;
        }

        if (!validated)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -2.50f, gameObject.transform.position.z);
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

            if (playerHandR.gameObject.transform.localRotation.z > 0.30f)
                gameObject.transform.position += new Vector3(0, 0, speed);

            else if (playerHandR.transform.localRotation.z < -0.30f)
                gameObject.transform.position -= new Vector3(0, 0, speed);
        }
    }
    IEnumerator DestroySelf()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(timeTillDeath);
        Spells.areaSpellLaunched = false;
        Destroy(gameObject);
    }
}
