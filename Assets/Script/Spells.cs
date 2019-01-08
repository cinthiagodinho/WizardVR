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

    //For testing without VR
    private bool shieldlaunched = false;
    private bool zoneAttacklaunched = false;

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
                DoZoneAttack();
                break;
            case "EnergyShield":
                DoShield();
                break;
            case "Telekinesis":
                if (!telekinesisOn)
                {
                    target = null;
                    DoTelekinesis();
                }
                break;
            case "StopTelekinesis":
                if (telekinesisOn)
                    StopTelekinesis(target);
                break;
        }
    }

    void Update()
    {
        //For testing without VR
        /* if (Input.GetKeyDown(KeyCode.A))
            DoFire();

        if (Input.GetKeyDown(KeyCode.Z) && !shieldlaunched)
            DoShield();

        if (Input.GetKeyDown(KeyCode.E) && !zoneAttacklaunched)
            DoZoneAttack();

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!telekinesisOn)
            {
                target = null;
                DoTelekinesis();
            }

        }
*/

        Debug.DrawRay(playerHandR.transform.position, playerHandR.transform.TransformDirection(Vector3.forward) * 10, Color.yellow);

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
        shieldInstance.transform.localPosition = new Vector3(0.2f, 0, 2);
        shieldInstance.transform.localEulerAngles = new Vector3(0, 90, 0);
        shieldInstance.transform.parent = null;

        StartCoroutine(IEDoShield(shieldInstance));
    }

    IEnumerator IEDoShield(GameObject shieldInstance)
    {
        yield return new WaitForSeconds(.1f);
        shieldInstance.GetComponent<Collider>().enabled = true;
    }

    void DoZoneAttack()
    {
        zoneAttacklaunched = true;
        /*GameObject zoneAttackInstance = GameObject.Instantiate(zoneAttack, playerHead.transform.position + (playerHead.transform.forward * 3), zoneAttack.transform.rotation);
     */
        GameObject zoneAttackInstance = GameObject.Instantiate(zoneAttack, playerHandR.transform.position + (playerHandR.transform.forward * 3), zoneAttack.transform.rotation);
        zoneAttackInstance.transform.position = new Vector3(zoneAttackInstance.transform.position.x, -2.50f, zoneAttackInstance.transform.position.z);
        StartCoroutine(IEDoZoneAttack(zoneAttackInstance));
    }

    IEnumerator IEDoZoneAttack(GameObject zoneAttackInstance)
    {
        yield return new WaitForSeconds(.1f);
        zoneAttackInstance.GetComponent<Collider>().enabled = true;
    }

    void DoTelekinesis()
    {
        telekinesisOn = true;

        RaycastHit hit;
        Vector3 fwd = playerHandR.transform.TransformDirection(Vector3.forward);
        int layerMask = 1 << 8;

        if (Physics.Raycast(playerHandR.transform.position, fwd, out hit, 10, layerMask))
        {
            target = hit.transform.gameObject;
            target.transform.parent = playerHandR.transform;
            target.AddComponent<TelekinesisSpell>();
        }
    }

    public static void StopTelekinesis(GameObject go)
    {
        Destroy(go.GetComponent<TelekinesisSpell>());
        telekinesisOn = false;
    }
}
