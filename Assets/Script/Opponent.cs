using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    public Text lifeText;
    private int lifePoints = 100;
    public GameObject fire;
    Transform opp;

    void Start()
    {
        lifeText.text = lifePoints.ToString();
        opp = gameObject.transform;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FirePower>())
        {
            //Debug.Log("prout");
            StartCoroutine("Hurt");
        }

    }
    IEnumerator Hurt()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        lifePoints--;
        lifeText.text = lifePoints.ToString();
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<MeshRenderer>().material.color = Color.black;
        StopCoroutine("Hurt");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
            DoFire();
    }
    void DoFire()
    {
        Quaternion rotation = Quaternion.LookRotation(-opp.forward, Vector3.up);
        Vector3 vect = new Vector3(opp.position.x,opp.position.y, (opp.position.z - 0.5f) );
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
