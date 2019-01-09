using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpell : MonoBehaviour
{
    Rigidbody rb;
    public float timeTillDeath;
    public float delayTillExplosion;
   
    public GameObject damages;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroySelf());       
    }

    void FixedUpdate()
    {

    }

    IEnumerator DestroySelf()
    {    
        yield return new WaitForSeconds(delayTillExplosion);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(gameObject);
    }
}
