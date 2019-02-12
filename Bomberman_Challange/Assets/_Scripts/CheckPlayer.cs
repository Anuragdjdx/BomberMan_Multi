using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPlayer : MonoBehaviour
{
	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Kuch to triggert hua " + collision.name);
		if (collision.name == "Bomberman-P1" )
		{
			GameObject.Find("Bomberman-P1").GetComponent<SpriteRenderer>().enabled = false;
			Invoke("player2win", .4f);

		}

		else if (collision.name == "Bomberman-P2")
		{
			GameObject.Find("Bomberman-P2").GetComponent<SpriteRenderer>().enabled = false;
			Invoke("player1win", .4f);
		}

		else if (true)
		{
			Destroy(GameObject.Find("Worm1"));
			
		}
	}

	public void player2win()
	{
		SceneManager.LoadScene("PLayer2_Win");
	}

	public 	void player1win()
	{
		SceneManager.LoadScene("PLayer1_Win");
	}
}
