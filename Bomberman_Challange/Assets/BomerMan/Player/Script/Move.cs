using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class Move : MonoBehaviour
{
	public static bool isInvincible = false;
	public static float timeSpentInvincible;
	public Texture2D lifeIconTexture;
	public static bool dead = false;
	public static int life = 100;

	public float speed_Player1, speed_Player2 = 3;

	public GameObject Player1, Player2;
	// NEED TO ADD
	public static Vector2 bombermanPosition, bombermanPositionRounded;
	public Tilemap tilemap;

	public GameObject bombPrefab;
	Animator anim_p1, anim_p2; //Player 1 and 2 Animator

	void Start()
	{
		anim_p1 = Player1.GetComponent<Animator>();
		anim_p2 = Player2.GetComponent<Animator>();
		dead = false;
		life = 100;
	}




	// Update is called once per frame
	public void BombSpwaner_Player1()
	{

		Vector3 worldPos = Player1.transform.position;
		Vector3Int cell = tilemap.WorldToCell(worldPos);
		Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
		Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);

	}


	public void BombSpwaner_Player2()
	{

		Vector3 worldPos = Player2.transform.position;
		Vector3Int cell = tilemap.WorldToCell(worldPos);
		Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

		Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);

	}

	private void Update()
	{

		GameObject[] Explosion = GameObject.FindGameObjectsWithTag("Explosion");

		foreach (var item in Explosion)
		{

		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			BombSpwaner_Player1();
		}

		if (Input.GetKeyDown(KeyCode.RightControl))
		{
			BombSpwaner_Player2();
		}


	}


	void FixedUpdate()
	{
		Vector2 dir_p1 = Vector2.zero;
		Vector2 dir_p2 = Vector2.zero;

		//For Player 1
		if (Input.GetKey(KeyCode.W))
		{
			anim_p1.SetInteger("Direction", 0);
			dir_p1.y = speed_Player1 * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			anim_p1.SetInteger("Direction", 1);
			dir_p1.x = speed_Player1 * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			anim_p1.SetInteger("Direction", 2);
			dir_p1.y = -speed_Player1 * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			anim_p1.SetInteger("Direction", 3);
			dir_p1.x = -speed_Player1 * Time.deltaTime;
		}

		else
		{
			anim_p1.SetInteger("Direction", 4);
		}

		//For Player 2

		if (Input.GetKey(KeyCode.UpArrow))
		{
			anim_p2.SetInteger("Direction", 0);
			dir_p2.y = speed_Player2 * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			anim_p2.SetInteger("Direction", 1);
			dir_p2.x = speed_Player2 * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			anim_p2.SetInteger("Direction", 2);
			dir_p2.y = -speed_Player2 * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			anim_p2.SetInteger("Direction", 3);
			dir_p2.x = -speed_Player2 * Time.deltaTime;
		}
		else
		{
			anim_p2.SetInteger("Direction", 4);
		}

		Player1.transform.Translate(dir_p1);
		Player2.transform.Translate(dir_p2);


		bombermanPosition = transform.position;
		float tempX = Mathf.Round(bombermanPosition.x);
		float tempY = Mathf.Round(bombermanPosition.y);
		//Debug.Log (bombermanPosition);
		bombermanPositionRounded = new Vector2(tempX, tempY);

		if (dead)
		{
			Application.LoadLevel("scene_win");
		}

		if (isInvincible)
		{
			timeSpentInvincible += Time.deltaTime;

			if (timeSpentInvincible < 3f)
			{
				float remainder = timeSpentInvincible % .3f;
				//renderer.enabled = remainder> .15f;
			}

			else
			{
				//renderer.enabled = true;
				isInvincible = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		life -= 10;
		if (life <= 0)
		{
			dead = true;
		}
		isInvincible = true;
		timeSpentInvincible = 0;
	}

	public static void BombExplosion()
	{
		life -= 20;
		if (life <= 0)
		{
			dead = true;
		}
		isInvincible = true;
		timeSpentInvincible = 0;
	}

	void DisplayLifeCount()
	{
		Rect lifeIconRect = new Rect(10, 10, 32, 32);
		GUI.DrawTexture(lifeIconRect, lifeIconTexture);

		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect(lifeIconRect.xMax + 10, lifeIconRect.y, 60, 32);
		GUI.Label(labelRect, life.ToString(), style);
	}

	void OnGUI()
	{
		DisplayLifeCount();
	}
}