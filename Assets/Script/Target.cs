using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health;
    public float fireDuration;
    protected int isTouched = 0;
    public MeshRenderer mesh;
    private Color baseMesh;
    private GameObject gameController;

    void Start()
    {
        baseMesh = mesh.material.color;
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
                StartCoroutine(HurtFire(damages, duration, intervalle));
            }
            else if (isTouched == 2)
            {
                float limit = 0;

                mesh.material.color = Color.red;
                damages = gameController.GetComponent<SpellsData>().getAreaSpellDamages();
                duration = gameController.GetComponent<SpellsData>().getAreaSpellDuration();
                intervalle = gameController.GetComponent<SpellsData>().getAreaSpellIntervalle();
                limit = gameController.GetComponent<SpellsData>().getAreaSpellLimit();
                StartCoroutine(HurtAreaSpell(damages, duration, intervalle, limit));
            }

            isTouched = 0;
        }
        if (health <= 0)
            Destroy(gameObject);
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
