using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

	[SerializeField] private GameObject addPrefab;
	[SerializeField] private float addInterval;
	[SerializeField] private Transform spawner;
	public BurtronEnemy burtronEnemy;

	public void triggerBoss()
    {
        if (burtronEnemy.bossActivated)
        {
			//StartCoroutine(spawnEnemy(addInterval, addPrefab));
        }
        else
        {
			//StopCoroutine(spawnEnemy);
        }
		
	}

	void Start()
	{
		//StartCoroutine(spawnEnemy(addInterval, addPrefab));
	}

	//private IEnumerator spawnEnemy(float interval, GameObject enemy)
	//{
	//	yield return new WaitForSeconds(interval);
	//	GameObject newEnemy = Instantiate(enemy, spawner.transform.position, spawner.transform.rotation);
	//	StartCoroutine(spawnEnemy(interval, enemy));
	//}


}

