using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBomb : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("player Kuch to collide hua " + collision.collider.name);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(" player Kuch to triggert hua " + collision.name);
	}
}
