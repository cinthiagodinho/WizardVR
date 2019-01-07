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
    public GameObject zoneAttack;

    public float shieldCooldown;
    private float _shieldCooldown;
    public Text shieldTextCooldown;
    private bool shieldlaunched = false;

    public float zoneAttackCooldown;
    private float _zoneAttackCooldown;
    public Text zoneAttackTextCooldown;
    private bool zoneAttacklaunched = false;

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
        if (Input.GetKeyDown(KeyCode.A))
            DoFire();

        /* if (Input.GetKeyDown(KeyCode.Keypad2) && !shieldlaunched)
             DoShield();

         if (Input.GetKeyDown(KeyCode.Keypad3) && !zoneAttacklaunched)
             DoZoneAttack();*/

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

       /* Quaternion rotation = Quaternion.LookRotation(playerHandR.forward, Vector3.up);
        Vector3 betweenHandsPos = (playerHandL.position + playerHandR.position) / 2;
        Vector3 pos = new Vector3(betweenHandsPos.x, betweenHandsPos.y, betweenHandsPos.z);
        GameObject shieldInstance = GameObject.Instantiate(shield, pos + transform.forward * 2f, transform.rotation);
*/
        //Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
       
        Vector3 pos = new Vector3(playerHandR.position.x, 0, playerHandR.position.z) + transform.forward * 2f;
        GameObject shieldInstance = GameObject.Instantiate(shield, pos, transform.rotation);

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
        shieldlaunched = false;
    }


    void DoZoneAttack()
    {
        zoneAttacklaunched = true;

        /* Vector3 pos = new Vector3(transform.position.x, -1.7f, transform.position.z);
        GameObject zoneAttackInstance = GameObject.Instantiate(zoneAttack, pos + transform.forward * 3f, zoneAttack.gameObject.transform.rotation);
*/
        Vector3 pos = new Vector3(playerHandR.position.x, -1.7f, playerHandR.position.z) + transform.forward * 2f;
        GameObject zoneAttackInstance = GameObject.Instantiate(zoneAttack, pos, transform.rotation);


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
        zoneAttacklaunched = false;
    }
}
