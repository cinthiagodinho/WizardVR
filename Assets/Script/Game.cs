using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject player;
    private bool gameOver;
    private bool win;
    public GameObject begin;
    public GameObject winText;
    public GameObject gameOverText;
    public GameObject playButton;
    public static bool authorizeSpell;
    private bool allowButtonA = false;
    public int enemiesToDefeat;
    public static int enemiesDefeated;


    void Awake()
    {
        win = false;
        gameOver = false;
        enemiesDefeated = 0;
        allowButtonA = true;
        authorizeSpell = false;
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
            authorizeSpell = false;

        }
        if (win)
        {
            winText.SetActive(true);
            InvokeRepeating("DisplayObject", 2, 0);
            authorizeSpell = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (allowButtonA)
            {
                if (win || gameOver)
                    SceneManager.LoadScene("testenviromagie");
                else if (!win && !gameOver)
                {
                    begin.SetActive(false);
                    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    for (var i = 0; i < enemies.Length; i++)
                        if (enemies[i].GetComponent<Shoot>())
                            enemies[i].GetComponent<Shoot>().enabled = true;

                    allowButtonA = false;
                    authorizeSpell = true;
                }
            }
        }
    }

    void DisplayObject()
    {
        playButton.SetActive(true);
        allowButtonA = true;
    }
}
