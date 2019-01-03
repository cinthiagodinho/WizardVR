using System.Collections;
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
    public float shieldCooldown;
    private float _shieldCooldown;
    public Text textShieldCooldown;
    private bool shieldlaunched = false;

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


    /*void OnEnable()
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
        }
    }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            DoFire();

        if (Input.GetKeyDown(KeyCode.Keypad2) && !shieldlaunched)
            DoShield();

    }

    void DoFire()
    {
        Quaternion rotation = Quaternion.LookRotation(playerHandR.forward, Vector3.up);
        Vector3 betweenHandsPos = (playerHandL.position + playerHandR.position) / 2;
        Vector3 firePos = new Vector3(betweenHandsPos.x, betweenHandsPos.y, (betweenHandsPos.z + 0.5f));
        GameObject fireInstance = GameObject.Instantiate(fire, firePos, rotation) as GameObject;
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
        Quaternion rotation = Quaternion.LookRotation(playerHandR.forward, Vector3.up);
        Vector3 betweenHandsPos = (playerHandL.position + playerHandR.position) / 2;
        Vector3 shieldPos = new Vector3(betweenHandsPos.x, betweenHandsPos.y, (betweenHandsPos.z + 1));
        GameObject shieldInstance = GameObject.Instantiate(shield, shieldPos, rotation);
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
        textShieldCooldown.text = shieldCooldown.ToString();

        while (shieldCooldown > -1)
        {
            yield return new WaitForSeconds(1f);
            shieldCooldown--;
            textShieldCooldown.text = shieldCooldown.ToString();
        }       
        textShieldCooldown.text = "";
        shieldCooldown = _shieldCooldown;
        shieldlaunched = false;
        Debug.Log("coroutine arrêté");      
    }
}
