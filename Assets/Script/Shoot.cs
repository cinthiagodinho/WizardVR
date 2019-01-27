using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject fire;  
    public float startingTime;

    void Start(){
        InvokeRepeating("DoFire", startingTime, 4f);
    }
    void Update()
    {
        
        /*if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire2"))
            DoFire();*/
    }
    void DoFire()
    {
        Vector3 oppForwardOnFloor = Vector3.ProjectOnPlane(gameObject.transform.forward, Vector3.up);
        // tir avec orientation libre
        Quaternion rotation = Quaternion.LookRotation(gameObject.transform.forward, Vector3.up);
        // tir toujours parallèle au sol
        //Quaternion rotation = Quaternion.LookRotation(oppForwardOnFloor, Vector3.up);
        Vector3 vect = gameObject.transform.position + gameObject.transform.forward * 0.5f;       
        GameObject fireInstance = GameObject.Instantiate(fire, vect, rotation) as GameObject;
        StartCoroutine(IEDoFire(fireInstance));
    }

    IEnumerator IEDoFire(GameObject fireInstance)
    {
        yield return new WaitForSeconds(.1f);
        fireInstance.GetComponent<Collider>().enabled = true;
    }

}
