﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Edwon.VR;
using Edwon.VR.Gesture;
using Edwon.VR.Input;

public class Spells : MonoBehaviour
{
    public GameObject fire;
    public GameObject shield;
    public GameObject zoneAttack;

    public float shieldCooldown;
    private float _shieldCooldown;
    public Text shieldTextCooldown;
   //private bool shieldlaunched = false;

    public float zoneAttackCooldown;
    private float _zoneAttackCooldown;
    public Text zoneAttackTextCooldown;
    //private bool zoneAttacklaunched = false;

    VRGestureRig rig;
    IInput input;

    Transform playerHead;
    Transform playerHandL;
    Transform playerHandR;


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

        _shieldCooldown = shieldCooldown;
    }

    void OnEnable()
    {
        GestureRecognizer.GestureDetectedEvent += OnGestureDetected;
    }

    void OnDisable()
    {
        GestureRecognizer.GestureDetectedEvent -= OnGestureDetected;
    }

    void OnGestureDetected(string gestureName, double confidence, Handedness hand, bool isDouble)
    {
        string confidenceString = confidence.ToString().Substring(0, 4);
        //Debug.Log("detected gesture: " + gestureName + " with confidence: " + confidenceString);

        switch (gestureName)
        {
            case "Fire":
                DoFire();
                break;
            case "Earth":
                DoZoneAttack();
                break;
            case "Ice":
                DoShield();
                break;
        }
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
            DoFire();

         if (Input.GetKeyDown(KeyCode.Keypad2) && !shieldlaunched)
             DoShield();

         if (Input.GetKeyDown(KeyCode.Keypad3) && !zoneAttacklaunched)
             DoZoneAttack();*/

    }

    void DoFire()
    {
        GameObject fireInstance = GameObject.Instantiate(fire, playerHandR.position, playerHandR.rotation);

        StartCoroutine(IEDoFire(fireInstance));
    }

    IEnumerator IEDoFire(GameObject fireInstance)
    {
        yield return new WaitForSeconds(.1f);
        fireInstance.GetComponent<Collider>().enabled = true;
    }

    void DoShield()
    {
       // shieldlaunched = true;
        GameObject shieldInstance = GameObject.Instantiate(shield, playerHandR.transform);
        shieldInstance.transform.localPosition = new Vector3(0.2f, 0, 2);
        shieldInstance.transform.localEulerAngles = new Vector3(0, 90, 0);
        shieldInstance.transform.parent = null;

        StartCoroutine(IEDoShield(shieldInstance));
        StartCoroutine(ShieldCooldown());
    }

    IEnumerator IEDoShield(GameObject shieldInstance)
    {
        yield return new WaitForSeconds(.1f);
        shieldInstance.GetComponent<Collider>().enabled = true;
    }

    IEnumerator ShieldCooldown()
    {
        while (shieldCooldown > -1)
        {
            shieldTextCooldown.text = shieldCooldown.ToString();
            shieldCooldown--;
            yield return new WaitForSeconds(1f);
        }
        shieldTextCooldown.text = "";
        shieldCooldown = _shieldCooldown;
        //shieldlaunched = false;
    }


    void DoZoneAttack()
    {
       // zoneAttacklaunched = true;

        /* Vector3 pos = new Vector3(transform.position.x, -1.7f, transform.position.z);
        GameObject zoneAttackInstance = GameObject.Instantiate(zoneAttack, pos + transform.forward * 3f, zoneAttack.gameObject.transform.rotation);
*/
        GameObject zoneAttackInstance = GameObject.Instantiate(zoneAttack, playerHandR.transform);
        zoneAttackInstance.transform.localPosition = new Vector3(0.2f, 0, 2);
       // zoneAttackInstance.transform.localEulerAngles = new Vector3(0, 0, 0);
        zoneAttackInstance.transform.parent = null;

        StartCoroutine(IEDoZoneAttack(zoneAttackInstance));
        StartCoroutine(ZoneAttackCooldown());
    }

    IEnumerator IEDoZoneAttack(GameObject zoneAttackInstance)
    {
        yield return new WaitForSeconds(.1f);
        zoneAttackInstance.GetComponent<Collider>().enabled = true;
    }

    IEnumerator ZoneAttackCooldown()
    {
        while (zoneAttackCooldown > 0)
        {
            zoneAttackTextCooldown.text = zoneAttackCooldown.ToString();
            zoneAttackCooldown--;
            yield return new WaitForSeconds(1f);
        }
        zoneAttackTextCooldown.text = "";
        zoneAttackCooldown = _shieldCooldown;
        //zoneAttacklaunched = false;
    }
}
