using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public class Worm : MonoBehaviour
{
    public float speed = 0.02f;
	public static Vector2 roundedWormPos; 

    Animator anim;

    // Current State
    bool moving = false;

    // Wait time Remaining for idle
    int idle_wait = 0;

    // Current movement goal
    Vector2 goal;

	MapDestroyer mapDestroy;

	public List<int> res = new List<int>();

	void Start()
	{
        // Get Animation component, set default animation
        anim = GetComponent<Animator>();
        anim.SetInteger("Direction", 4);

        // Initialize movement variables
        goal = transform.position;
		mapDestroy = GetComponent<MapDestroyer>();
    }
	

    void FixedUpdate()
	{

        Vector2 pos = transform.position;

        if (moving) 
		{
            if (pos == goal)
			{
                // Reached goal. Back to Idle.
                moving = false;
                idle_wait = Random.Range(1, 100);
		    }

			else
			{
                // Walk a bit towards goal
                Vector2 v = Vector2.MoveTowards(pos,goal,speed);
                transform.position = v;
            }
        } 

		else 
		{
            // Idle
            if (idle_wait > 0) 
			{
                // Update the animation parameter
                anim.SetInteger("Direction", 4);

                // Wait a bit so it doesn't look nervous
                --idle_wait;
            }

			else
			{
                // Find valid Directions
                List<int> dirs = validDirections();

                // Select one of them randomly
                int dir = dirs[Random.Range(0, dirs.Count)];

                // Update animation parameter
                anim.SetInteger("Direction", dir);

                // Calculate next goal based on direction
                switch (dir) {
                    case 0: goal=pos+new Vector2(0, 1); break;
                    case 1: goal=pos+new Vector2(1, 0); break;
                    case 2: goal=pos+new Vector2(0, -1); break;
                    case 3: goal=pos+new Vector2(-1, 0); break;
                }

                // Change state to moving
                moving = true;
            }
        }
    }

    List<int> validDirections()
	{
       
        Vector2 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

		if (!Block.getBlockAt(pos.x, pos.y + 1)) // up
			res.Add(0);

		if (!Block.getBlockAt(pos.x + 1, pos.y)) // right
			res.Add(1);

		if (!Block.getBlockAt(pos.x, pos.y - 1)) // down
			res.Add(2);

		if (!Block.getBlockAt(pos.x - 1, pos.y)) // left
			res.Add(3);

		return res;
    }
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		
	
		Debug.Log("Naam bata " + col.name);

		if (col.name == "Bomberman-P1")
		{
			
			Invoke("player2win", 1);
			GameObject.Find("Bomberman-P1").GetComponent<SpriteRenderer>().enabled = false;
		}
			
		if	(col.name == "Bomberman-P2")
		{
			
			Invoke("player1win", 1);
			GameObject.Find("Bomberman-P2").GetComponent<SpriteRenderer>().enabled = false;
		}


	}

	 void player2win()
	{
		SceneManager.LoadScene("PLayer2_Win");
	}

	 void player1win()
	{
		SceneManager.LoadScene("PLayer1_Win");
	}


}
