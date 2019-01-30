using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakDestroy : MonoBehaviour {
	public float timeTillDeath;
	 void Start()
    {     
        StartCoroutine(DestroySelf());       
    }
	
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeTillDeath);       
        Cooldown.shield = 1; 
        Destroy(gameObject);
    }
}
