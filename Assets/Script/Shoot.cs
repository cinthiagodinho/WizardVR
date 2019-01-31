using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject fire;
    public GameObject baguette;
    public float startingTime;
    public float shoot;
    

    void Start()
    {
        InvokeRepeating("DoFire", startingTime, shoot);
    }

    void DoFire()
    {
        Vector3 oppForwardOnFloor = Vector3.ProjectOnPlane(gameObject.transform.forward, Vector3.up);
        // tir avec orientation libre
        Quaternion rotation = Quaternion.LookRotation(gameObject.transform.forward, Vector3.up);
        // tir toujours parallèle au sol
        //Quaternion rotation = Quaternion.LookRotation(oppForwardOnFloor, Vector3.up);
        Vector3 vect = baguette.transform.position + baguette.transform.forward * 0.8f;
        GameObject fireInstance = GameObject.Instantiate(fire, vect, rotation) as GameObject;
       
    }
   
    void Update()
    {
        if (GetComponentInChildren<Target>().getHealth() <= 0)
            Destroy(gameObject);
    }
}
