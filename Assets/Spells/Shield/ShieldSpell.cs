using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    Rigidbody rb;
    public float timeTillDeath;
    public int durability;
    public int criticalParade;
    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroySelf());
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (durability <= 0)
        {
            Spells.shieldlaunched = false;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<FirePower>())
        {
            if (timer < criticalParade)
            {
                float speed = col.gameObject.GetComponent<FirePower>().speed;
                Vector3 force = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y, speed);
                col.gameObject.GetComponent<Rigidbody>().AddRelativeForce(-force, ForceMode.Impulse);
                Destroy(gameObject);
            }
            else
            {
                durability--;
                Debug.Log(durability);
            }
        }
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeTillDeath);
        Spells.shieldlaunched = false;
        Destroy(gameObject);
    }
    public float getTimer()
    {
        return timer;
    }
     public float getCriticalParade()
    {
        return criticalParade;
    }
}
