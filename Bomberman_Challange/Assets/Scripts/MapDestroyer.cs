using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class MapDestroyer : MonoBehaviour {

	public Tilemap tilemap;

	public Tile wallTile;
	public Tile destructibleTile;

	public GameObject explosionPrefab;
	private GameObject player1, player2;


	



	public void Explode(Vector2 worldPos)
	{
		Vector3Int P1_Pos, P2_Pos;

		Vector3Int originCell = tilemap.WorldToCell(worldPos);
		
		Debug.Log("Ye postion kya hai ? " + tilemap.WorldToCell(worldPos));

		ExplodeCell(originCell);

		if (ExplodeCell(originCell + new Vector3Int(1, 0, 0)))
		{
			ExplodeCell(originCell + new Vector3Int(1, 0, 0));			

		}
		
		if (ExplodeCell(originCell + new Vector3Int(0, 1, 0)))
		{
			ExplodeCell(originCell + new Vector3Int(0, 1, 0));
		}
		
		if (ExplodeCell(originCell + new Vector3Int(-1, 0, 0)))
		{
			ExplodeCell(originCell + new Vector3Int(-1, 0, 0));
		}
		
		if (ExplodeCell(originCell + new Vector3Int(0, -1, 0)))
		{
			ExplodeCell(originCell + new Vector3Int(0, -1, 0));
		}

	}

	bool ExplodeCell (Vector3Int cell)
	{
		Tile tile = tilemap.GetTile<Tile>(cell);
		player1 = GameObject.FindGameObjectWithTag("Player1");
		player2 = GameObject.FindGameObjectWithTag("Player2");


		//For Player 1
		float celly = cell.y;
		float cellx = cell.x;
		float player1y = player1.transform.position.y;
		float player1x = player1.transform.position.x;
		float dy = celly - player1y;
		dy = dy * dy;
		float dx = cellx - player1x;
		dx = dx * dx;		
		float distance = dy + dx;
		distance = Mathf.Sqrt(distance);
		int intDist = (int)distance;
		Debug.Log("Distance p1 kya " + intDist);



		//For Player 2
		float cellyp2 = cell.y;
		float cellxp2 = cell.x;
		float player2y = player2.transform.position.y;
		float player2x = player2.transform.position.x;
		float dyp2 = cellyp2 - player2y;
		dyp2 = dyp2 * dyp2;
		float dxp2 = cellxp2 - player2x;
		dxp2 = dxp2 * dxp2;
		float distancep2 = dyp2 + dxp2;
		distancep2 = Mathf.Sqrt(distancep2);
		int intDistp2 = (int)distancep2;
		Debug.Log("Distance  p2 kya " + intDistp2);

		if (intDist <= 0  )
		{
			player1.GetComponent<SpriteRenderer>().enabled = false;
			Debug.Log("Player2 Win");
			Invoke("player2win", 1.2f);
		}


		if (intDistp2 <= 0 )
		{
			player2.GetComponent<SpriteRenderer>().enabled = false;
			Debug.Log("Player1 Win");
			Invoke("player1win", 1.2f);
		}


		if (tile == wallTile)
		{
			return false;
		}

		if (tile == destructibleTile) 
		{
			tilemap.SetTile(cell, null);
			
			
		}



		

		Vector3 pos = tilemap.GetCellCenterWorld(cell);
		Instantiate(explosionPrefab, pos, Quaternion.identity);
		Invoke("DestroyExplorionPrefabs", 2);
		return true;
	}

	void player2win()
	{
		SceneManager.LoadScene("PLayer2_Win");
	}

	void player1win()
	{
		SceneManager.LoadScene("PLayer1_Win");
	}


	void DestroyExplorionPrefabs() // To delete the Explotion Clone objects from Heirarchy
	{
		GameObject[] Explosion = GameObject.FindGameObjectsWithTag("Explosion");

		foreach (var item in Explosion)
		{

			Debug.Log("Hello");


			Destroy(item);

		

		}
	}

}
