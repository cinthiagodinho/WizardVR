using UnityEngine;
using System.Collections;

public class FirePower : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public float timeTillDeath;
    private bool deathBegan = false;
    public GameObject fireExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0, 0, speed);
        rb.AddRelativeForce(force, ForceMode.Impulse);
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!deathBegan)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (collision.gameObject.GetComponentInParent<Opponent>())
                    collision.gameObject.GetComponentInParent<Opponent>().setIsTouched(1);

                if (collision.gameObject.GetComponent<Target>())
                    collision.gameObject.GetComponent<Target>().setIsTouched(1);
                    
                StartCoroutine(DestroySelf(collision));
            }

            if (collision.gameObject.GetComponent<ShieldSpell>())
            {
                if (collision.gameObject.GetComponent<ShieldSpell>().getTimer() > collision.gameObject.GetComponent<ShieldSpell>().getCriticalParade())
                {
                    StartCoroutine(DestroySelf(collision));
                }
            }
        }

    }

    IEnumerator DestroySelf(Collision collision)
    {
        deathBegan = true;
        GameObject explosion = GameObject.Instantiate(fireExplosion, collision.contacts[0].point, Quaternion.identity);
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(gameObject);
    }

}
