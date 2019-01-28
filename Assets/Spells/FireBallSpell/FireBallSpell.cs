using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;
using Edwon.VR.Input;

public class FireBallSpell : MonoBehaviour
{
    public OVRInput.Controller controller;
    public string buttonName;
    public GameObject core;
    public GameObject coreLightning;
    VRGestureRig rig;
    Transform playerHandR;
    public GameObject ActivatedGameObject;
    private bool validated = false;

    void Start()
    {
        rig = FindObjectOfType<VRGestureRig>();
        playerHandR = rig.handRight;
    }

    void Update()
    {
        if (Input.GetAxis(buttonName) == 1)
        {
            ActivateGO();
            validated = true;
        }

        if (!validated)
            gameObject.transform.position = playerHandR.position + (playerHandR.transform.forward * 1.1f);
        gameObject.transform.rotation = playerHandR.rotation;
    }

    void OnEnable()
    {
        ActivatedGameObject.SetActive(false);
    }

    void ActivateGO()
    {
        ActivatedGameObject.SetActive(true);
        core.SetActive(false);
        coreLightning.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponentInParent<Opponent>())
                collision.gameObject.GetComponentInParent<Opponent>().setIsTouched(1);

            if (collision.gameObject.GetComponent<Target>())
                collision.gameObject.GetComponent<Target>().setIsTouched(1);

            ActivateGO();
        }

        if (collision.gameObject.GetComponent<ShieldSpell>())
        {
            if (collision.gameObject.GetComponent<ShieldSpell>().getTimer() > collision.gameObject.GetComponent<ShieldSpell>().getCriticalParade())
            {
                ActivateGO();
            }
        }
        else
        {
            ActivateGO();
        }
    }
}

