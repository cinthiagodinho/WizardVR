using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    public GameObject SpawnAfterDead;
    public int durability;
    public int criticalParade;
    private float timer = 0;

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (durability <= 0)
        {
            Spells.shieldSpellLaunched = false;
            GameObject.Destroy(this.gameObject);
            Spells.shieldSpellLaunched = false;
            GameObject.Instantiate(SpawnAfterDead, this.transform.position, SpawnAfterDead.transform.rotation);
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
                GameObject.Destroy(this.gameObject);
                Spells.shieldSpellLaunched = false;
                GameObject.Instantiate(SpawnAfterDead, this.transform.position, SpawnAfterDead.transform.rotation);               
            }
            else
            {
                durability--;
                Debug.Log(durability);
            }
        }
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
