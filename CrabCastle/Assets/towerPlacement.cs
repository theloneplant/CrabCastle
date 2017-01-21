using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour 
{

	public GameObject towerTransObj;
	public GameObject towerPrefab;

	public Material notPlaceableMAT;
	public Material placeableMAT;

	GameManager gManager;

	void Start () 
	{
		gManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> () as GameManager;
	}
	
	void Update () 
	{
		if (gManager.towerPlacementActive) 
		{		
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				print (hit.transform.name);
				towerTransObj.transform.localPosition = hit.point;

				if (hit.transform.gameObject.layer != LayerMask.NameToLayer ("Foundation")) 
				{
					Renderer towerRend = towerTransObj.GetComponent<Renderer> () as Renderer;
					towerRend.sharedMaterial = notPlaceableMAT;
				} 
				else 
				{
					Renderer towerRend = towerTransObj.GetComponent<Renderer> () as Renderer;
					towerRend.sharedMaterial = placeableMAT;

					if (Input.GetMouseButton (0)) 
					{
						Instantiate (towerPrefab, towerTransObj.transform.position, towerTransObj.transform.rotation);
						gManager.towerPlacementActive = false;
						Destroy (towerTransObj);
					}
				}
			}
		}
	}
}
