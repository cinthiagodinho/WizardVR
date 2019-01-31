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
    private GameObject fireInstance;
    public GameObject shield;
    public GameObject areaSpell;
    public GameObject thunderbolt;
    public GameObject popCube;
    private float timeTillCubeDisappear = 2;

    public static bool fireBallLaunched = false;
    public static int fireBallCount = 1;
    public static bool shieldSpellLaunched = false;
    public static bool areaSpellLaunched = false;
    public static bool telekinesisOn = false;
    public static bool thunderBoltLaunched = false;
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
        if (Game.authorizeSpell)
        {
            switch (gestureName)
            {
                case "FireBall":
                 
                    if (!areaSpellLaunched && !fireBallLaunched && !shieldSpellLaunched)
                    {
                       
                        if (Cooldown.fireBall == 0)
                            DoFire();
                    }
                    break;

                case "FireCircle":
                   

                    if (!areaSpellLaunched && !fireBallLaunched && !shieldSpellLaunched)
                    {
                        
                        if (Cooldown.zoneAttack == 0)
                            DoAreaSpell();
                    }
                    break;

                case "Shield":
                   

                    if (!areaSpellLaunched && !fireBallLaunched && !shieldSpellLaunched)
                    {
                       
                        if (Cooldown.shield == 0)
                            DoShield();
                    }

                    break;

                case "ThunderBolt":
                    if (!thunderBoltLaunched)
                        DoThunderBolt();
                    break;
            }
        }
    }

    void Update()
    {
        //For testing without VR
        /* if (Input.GetKeyDown(KeyCode.A))
             DoFire();

         if (Input.GetKeyDown(KeyCode.Z))
         {
             if (!shieldSpellLaunched)
                 DoShield();
         }

         if (Input.GetKeyDown(KeyCode.E))
         {
             if (!areaSpellLaunched)
                 DoAreaSpell();
         }
         if (Input.GetKeyDown(KeyCode.R))
         {
             if (!thunderBoltLaunched)
                 DoThunderBolt();
         }*/

        if (fireBallCount == 2 && !fireBallLaunched)
            DoFire();

        if (fireBallCount == 3 && !fireBallLaunched)
            DoFire();

        if (fireBallCount == 4 && !fireBallLaunched)
        {
            fireBallCount = 1;
            Cooldown.fireBall = 1;
        }
    }

    void DoFire()
    {
        fireBallLaunched = true;
        fireInstance = GameObject.Instantiate(fire, playerHandR.position + (playerHandR.transform.forward * 1.1f), playerHandR.rotation);
    }

    void DoShield()
    {
        shieldSpellLaunched = true;
        GameObject shieldInstance = GameObject.Instantiate(shield, playerHandR.transform);
        shieldInstance.transform.localPosition = new Vector3(0, 0, 0.5f);
    }

    void DoAreaSpell()
    {
        areaSpellLaunched = true;
        GameObject areaSpellInstance = GameObject.Instantiate(areaSpell, playerHandR.transform.position + (playerHandR.transform.forward * 3), areaSpell.transform.rotation);
        areaSpellInstance.transform.parent = playerHandR.transform;
    }

    void DoThunderBolt()
    {
        thunderBoltLaunched = true;
        GameObject.Instantiate(thunderbolt, playerHandR.position, playerHandR.rotation);
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
