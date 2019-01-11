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
    public GameObject areaSpell;
    public GameObject popCube;
    private float timeTillCubeDisappear = 2;

    //For testing without VR
    public static bool shieldlaunched = false;
    public static bool areaSpelllaunched = false;

    public static bool telekinesisOn = false;
    VRGestureRig rig;
    IInput input;

    Transform playerHead;
    Transform playerHandL;
    Transform playerHandR;

    private GameObject target;
  
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
            case "FireBall":
                DoFire();
                break;
            case "Inferno":
                if (!areaSpelllaunched)
                    DoAreaSpell();
                break;
            case "EnergyShield":
                if (!shieldlaunched)
                    DoShield();
                break;
        }
    }

    void Update()
    {
        //For testing without VR
        if (Input.GetKeyDown(KeyCode.A))
            DoFire();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!shieldlaunched)
                DoShield();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!areaSpelllaunched)
                DoAreaSpell();
        }        
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
        shieldlaunched = true;
        GameObject shieldInstance = GameObject.Instantiate(shield, playerHandR.transform);
        shieldInstance.transform.localPosition = new Vector3(0.2f, 0, 0.4f);
        shieldInstance.transform.localEulerAngles = new Vector3(0, 90, 0);
        shieldInstance.transform.parent = null;
    }

    void DoAreaSpell()
    {
        areaSpelllaunched = true;
        GameObject areaSpellInstance = GameObject.Instantiate(areaSpell, playerHandR.transform.position + (playerHandR.transform.forward * 3), areaSpell.transform.rotation);
        areaSpellInstance.transform.parent = playerHandR.transform;
    }

    void PopCube()
    {
        GameObject cube = GameObject.Instantiate(popCube);
        StartCoroutine(WaitAndDestroy(timeTillCubeDisappear, cube));
    }

    IEnumerator WaitAndDestroy(float value, GameObject go)
    {
        yield return new WaitForSeconds(timeTillCubeDisappear);
        Destroy(go);
    }
}
