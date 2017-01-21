using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public bool towerPlacementActive;
	public GameObject transTowerPrefab;

	public int playerGold = 10000;

	public Text playerGoldText; 

	TowerPlacement tPlacement;

	int towerCost = 100;

	void Start () 
	{
		towerPlacementActive = false;
		tPlacement = gameObject.GetComponent<TowerPlacement> () as TowerPlacement;
	}

	void Update()
	{
		playerGoldText.text = playerGold.ToString();
	}

	public void PlaceTower()
	{
		if (!towerPlacementActive && playerGold > towerCost) 
		{
			playerGold -= towerCost;
			towerPlacementActive = true;
			GameObject tTower = Instantiate (transTowerPrefab);
			tPlacement.towerTransObj = tTower;
		}
	}
}
