using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    Rigidbody rb;
    public float timeTillDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody>();     
       // StartCoroutine(DestroySelf());
    }

    void FixedUpdate()
    {

    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(gameObject);
    }
}
