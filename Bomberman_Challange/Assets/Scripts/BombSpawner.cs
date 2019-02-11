using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{

	public Tilemap tilemap;

	public GameObject bombPrefab;
	
	// Update is called once per frame
	public void BombSpwaner_Player1()
	{
		
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int cell = tilemap.WorldToCell(worldPos);
			Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

			Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
		
	}


}
