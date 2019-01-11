using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpellDamages : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponentInParent<Opponent>())
                collision.gameObject.GetComponentInParent<Opponent>().setIsTouched(2);

            if (collision.gameObject.GetComponent<Target>())
                collision.gameObject.GetComponent<Target>().setIsTouched(2);
        }
    }
}
