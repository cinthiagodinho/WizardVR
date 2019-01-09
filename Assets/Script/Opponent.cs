using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    //public Text lifeText;
    public int health;
    public GameObject fire;
    public float fireDuration;
    public float infernoDuration;
    Transform opp;

    void Start()
    {
        //lifeText.text = lifePoints.ToString();
        opp = gameObject.transform;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FirePower>())
        {
            gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
            //StartCoroutine(Hurt(1, fireDuration));
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<ZoneAttackSpell>())
            StartCoroutine(Hurt(3, infernoDuration));
    }

    IEnumerator Hurt(int damage, float duration)
    {
        while (duration > 0)
        {
            health--;
            duration--;
            yield return new WaitForSeconds(1);
        }

        gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.black;
        StopCoroutine(Hurt(0, 0));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
            DoFire();
    }
    void DoFire()
    {
        Quaternion rotation = Quaternion.LookRotation(-opp.forward, Vector3.up);
        Vector3 vect = new Vector3(opp.position.x, opp.position.y, (opp.position.z - 0.5f));
        Debug.Log(vect);
        GameObject fireInstance = GameObject.Instantiate(fire, vect, rotation) as GameObject;
        StartCoroutine(IEDoFire(fireInstance));
    }

    IEnumerator IEDoFire(GameObject fireInstance)
    {
        yield return new WaitForSeconds(.1f);
        fireInstance.GetComponent<Collider>().enabled = true;
    }

}
