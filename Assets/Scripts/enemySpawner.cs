using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

	[SerializeField] private GameObject addPrefab;
	[SerializeField] private float addInterval = 10f;
	[SerializeField] private Transform leftSpawner;
	[SerializeField] private Transform rightSpawner;
	public BurtronEnemy burtronEnemy;
	private int spawnCount = 0;

	public void triggerBoss()
    {
        if (burtronEnemy.bossActivated == true)
        {
			StartCoroutine(spawnEnemy(addInterval, addPrefab));
        }		
	}
	public void defeatBoss()
    {
		StopCoroutine("spawnEnemy");
		//Debug.Log("defeat boss called");
	}
	void Start()
	{
		//StartCoroutine(spawnEnemy(addInterval, addPrefab));
	}

	public IEnumerator spawnEnemy(float interval, GameObject enemy)
	{
		if (spawnCount <= 7)
		{
			spawnCount++;
			GameObject newEnemy = Instantiate(enemy, leftSpawner.transform.position, leftSpawner.transform.rotation);
			GameObject newEnemy2 = Instantiate(enemy, rightSpawner.transform.position, rightSpawner.transform.rotation);
			yield return new WaitForSeconds(interval);
			if (burtronEnemy.bossActivated == true)
			{
				StartCoroutine(spawnEnemy(interval, enemy));
			}
		}
	}


}

