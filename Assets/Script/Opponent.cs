using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    public int health;
    public SkinnedMeshRenderer mesh;
    public SkinnedMeshRenderer joint;
    private Color baseMesh;
    private Color baseJoint;

    public float fireDuration;
    public float infernoDuration;
    public Transform opp;
    public int isTouched = 0;
    private GameObject gameController;

    void Start()
    {
        baseMesh = mesh.material.color;
        baseJoint = joint.material.color;
        gameController = GameObject.FindGameObjectWithTag("GameController");

    }

    void Update()
    {
        if (isTouched != 0)
        {
            int damages = 0;
            float duration = 0;
            float intervalle = 0;

            if (isTouched == 1)
            {
                mesh.material.color = Color.red;
                damages = gameController.GetComponent<SpellsData>().getFireDamages();
                duration = gameController.GetComponent<SpellsData>().getFireDuration();
                intervalle = gameController.GetComponent<SpellsData>().getFireIntervalle();
            }
            StartCoroutine(HurtFire(damages, duration, intervalle));
            isTouched = 0;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<AreaSpell>())
        {
            mesh.material.color = Color.red;
            int damages = gameController.GetComponent<SpellsData>().getAreaSpellDamages();
            float duration = gameController.GetComponent<SpellsData>().getAreaSpellDuration();
            float intervalle = gameController.GetComponent<SpellsData>().getAreaSpellIntervalle();
            float limit = gameController.GetComponent<SpellsData>().getAreaSpellLimit();
            StartCoroutine(HurtAreaSpell(damages, duration, intervalle, limit));

        }
    }

    IEnumerator HurtFire(int damage, float duration, float intervalle)
    {
        int count = 0;

        while (duration > 0)
        {
            duration--;
            count++;

            if (count == intervalle)
            {
                health -= damage;
                count = 0;
            }
            yield return new WaitForSeconds(1);
        }

        mesh.material.color = baseMesh;
        Debug.Log("Health : " + health);
        StopCoroutine(HurtFire(0, 0, 0));
    }

    IEnumerator HurtAreaSpell(int damage, float duration, float intervalle, float limit)
    {
        int count = 0;

        while (duration > 0)
        {
            duration--;
            count++;

            if (count == intervalle)
            {
                if (duration <= limit)
                    damage--;

                health -= damage;
                count = 0;
            }
            yield return new WaitForSeconds(1);
        }

        mesh.material.color = baseMesh;
        Debug.Log("Health : " + health);
        StopCoroutine(HurtAreaSpell(0, 0, 0, 0));
    }


    public void setIsTouched(int value)
    {
        isTouched = value;
    }
}
