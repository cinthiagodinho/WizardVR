using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject player;
    private bool gameOver;
    private bool win;
    public GameObject winText;
    public GameObject gameOverText;
    public GameObject playButton;
    private bool allowButtonA = false;
    public int enemiesToDefeat;
    public static int enemiesDefeated;


    void Awake()
    {
        win = false;
        gameOver = false;
        enemiesDefeated = 0;
    }
    void Update()
    {
        if (player.GetComponent<Player>().lifePoints <= 0)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (var i = 0; i < enemies.Length; i++)
                Destroy(enemies[i]);

            gameOver = true;
        }

        if (enemiesDefeated == enemiesToDefeat)
            win = true;

        if (gameOver)
        {
            gameOverText.SetActive(true);
            InvokeRepeating("DisplayObject", 2, 0);
        }
        if (win)
        {
            winText.SetActive(true);
            InvokeRepeating("DisplayObject", 2, 0);
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (allowButtonA)
            {
                SceneManager.LoadScene("testenviromagie");
            }
        }
    }

    void DisplayObject()
    {
        playButton.SetActive(true);
        allowButtonA = true;
    }
}
