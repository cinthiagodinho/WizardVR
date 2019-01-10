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
            int duration = 0;
            int intervalle = 0;

            if (isTouched == 1)
            {
                mesh.material.color = Color.red;
                damages = gameController.GetComponent<SpellsData>().getFireDamages();
                duration = gameController.GetComponent<SpellsData>().getFireDuration();
                intervalle = gameController.GetComponent<SpellsData>().getFireIntervalle();
            }
            StartCoroutine(Hurt(damages, duration, intervalle));
            isTouched = 0;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponentInChildren<FirePower>())
        {

        }
    }

    IEnumerator Hurt(int damage, int duration, int intervalle = 0)
    {
        int count = 0;
       
        while (duration > 0)
        {
            if (intervalle == 1)
            {
                health -= damage;
                duration--;
            }

            if (intervalle > 1)
            {
                duration--;
                count++;

                if (count == intervalle)
                {
                    health -= damage;
                    count = 0;
                }
            }
            yield return new WaitForSeconds(1);
        }

        mesh.material.color = baseMesh;
        Debug.Log("Health : " + health);
        StopCoroutine(Hurt(0, 0, 0));
    }
    public void setIsTouched(int value)
    {
        isTouched = value;
    }
}
