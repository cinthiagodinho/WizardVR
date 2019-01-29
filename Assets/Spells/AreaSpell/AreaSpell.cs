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
    public float speed;

    VRGestureRig rig;
    Transform playerHandR;

    void Start()
    {
        rig = FindObjectOfType<VRGestureRig>();
        playerHandR = rig.handRight;
    }
    void Update()
    {
        if (Input.GetAxis(buttonName) == 1)
            validated = true;

        if (validated)
        {
            StartCoroutine(DestroySelf());
            gameObject.transform.parent = null;
        }

        if (!validated)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -2.50f, gameObject.transform.position.z);
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

            if (playerHandR.gameObject.transform.localRotation.x > 0.20f)
                gameObject.transform.position += new Vector3(speed, 0, 0);

            else if (playerHandR.transform.localRotation.x < -0.20f)
            {
                gameObject.transform.position -= new Vector3(speed, 0, 0);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponentInParent<Opponent>())
                collision.gameObject.GetComponentInParent<Opponent>().setIsTouched(2);

            if (collision.gameObject.GetComponent<Target>())
                collision.gameObject.GetComponent<Target>().setIsTouched(2);
        }
    }

    IEnumerator DestroySelf()
    {
        //gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(timeTillDeath);
        Spells.areaSpellLaunched = false;
        Destroy(gameObject);
    }
}
