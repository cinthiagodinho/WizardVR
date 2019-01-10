using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    Rigidbody rb;
    public float timeTillDeath;
    public int durability;
    private float timer;
    private bool successfulParade = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroySelf());
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (durability <= 0)
            Destroy(gameObject);

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<FirePower>())
        {
            if (timer <= 2)
            {
                float speed = col.gameObject.GetComponent<FirePower>().speed;
                Vector3 force = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y, speed);
                col.gameObject.GetComponent<Rigidbody>().AddRelativeForce(-force, ForceMode.Impulse);
                successfulParade = true;
            }
            else
            {
                durability--;
            }
        }
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(gameObject);
    }
    public bool getSuccessfulParade(){
        return successfulParade;
    }
}
