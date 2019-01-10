using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpell : MonoBehaviour
{
    Rigidbody rb;
    public float timeTillDeath;
    public float delayTillExplosion;
    private bool validated = false;

    public GameObject damages;
    Color opacity;
    void Start()
    {        
        opacity = gameObject.GetComponent<MeshRenderer>().material.color;
        opacity.a = 0.4f;
    }

    void FixedUpdate()
    {
        if (validated)
        {
            opacity.a = 1;
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(delayTillExplosion);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(timeTillDeath);
        Spells.areaSpelllaunched = false;
        Destroy(gameObject);
    }
}
