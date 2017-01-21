using UnityEngine;
using System.Collections;

public class fishSpawn : MonoBehaviour 
{
	public GameObject spawnPoint;
	public GameObject fishPrefab;

	bool fishSpawnActive;

	public float fishSpawnRate = 0.25f;
	float tmpTimer;

	void Start()
	{
		fishSpawnActive = true;
		Instantiate (fishPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
	}

	void Update()
	{
		tmpTimer += Time.deltaTime;
		if (fishSpawnActive) 
		{
			tmpTimer += Time.deltaTime;

			if (tmpTimer >= fishSpawnRate) 
			{
				Instantiate (fishPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
				tmpTimer = 0.0f;
			}
		}
	}
}
