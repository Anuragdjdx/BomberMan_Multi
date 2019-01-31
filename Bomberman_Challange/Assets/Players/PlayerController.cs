using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

	public float pVertical;
	public float pHorizontal;
	public Animator pAnimator;

	// Use this for initialization
	void Start ()
	{
		pAnimator = GetComponent<Animator>();			
	}
	
	// Update is called once per frame
	void Update ()
	{
		pVertical = Input.GetAxis("Vertical");
		pHorizontal = Input.GetAxis("Horizontal");
		pAnimator.SetFloat("Vertical", pVertical);
		pAnimator.SetFloat("Horizontal", pVertical);

	}
}
