using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Scene scene;
    public Player player;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        //Debug.Log("Active Scene is '" + scene.name + "'.");
    }

    void Update()
    {
        if (player.lifePoints <= 0)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (var i = 0; i < enemies.Length; i++)
                Destroy(enemies[i]);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }
}
