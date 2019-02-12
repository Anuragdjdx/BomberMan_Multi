using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlyer_pow : MonoBehaviour
{
	public static bool _range;
	public bool range
	{
		get
		{
			return _range;

		}

		set
		{
			_range = range;
		}
	}

	private void Start()
	{
		range = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Kuch to triggert hua " + collision.name);
		if (collision.name == "Bomberman-P1")
		{

			//player 1 can enter multiple bombs


			Invoke("Player1_PowerUp", 1);
			


			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}

		else if (collision.name == "Bomberman-P2")
		{
			//player 2 can enter multiple bombs

			Debug.Log("player 2 can enter multiple bombs");
			Invoke("Player1_PowerUp", 1);
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}



	public void Player1_PowerUp()
	{
		CheckPlyer_pow._range = true;
		Debug.Log("player 1 can enter multiple bombs range = " + _range);
	}

	void Player2_PowerUp()
	{
		
	}
}
