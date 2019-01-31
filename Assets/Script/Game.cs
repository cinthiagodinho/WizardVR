using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public GameObject player;
    private bool gameOver = false;
    public GameObject canvas;
    public GameObject playButton;
	private bool allowButtonA = false;

    void Start()
    {

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

        if (gameOver)
        {
            canvas.SetActive(true);
            InvokeRepeating("DisplayObject", 2, 0);
        }

		if(OVRInput.GetDown(OVRInput.Button.One)){
			if(allowButtonA){
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
