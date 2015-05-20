using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float moving_impact = 1;
	public float jumping_impact = 5;
	public float floor_dist = 0.5f;
	private bool Jump=true;
	
	public bool debug = true;
	
	void OnCollisionEnter2D(){
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		float transV = -playerspeed * Input.GetAxis ("Vertical") * Time.deltaTime;
		float transH = -playerspeed * Input.GetAxis ("Horizontal") * Time.deltaTime;
		transform.Translate (new Vector3 (transH, transV,0));
	*/
		if (Jump) transform.rigidbody2D.AddForce (new Vector2 (moving_impact * Input.GetAxis ("Horizontal") * Time.deltaTime, 0), ForceMode2D.Impulse);
		else transform.rigidbody2D.AddForce (new Vector2 (moving_impact * Input.GetAxis ("Horizontal") * Time.deltaTime/5, 0), ForceMode2D.Impulse);

		if (Jump&&(Input.GetAxis ("Jump")==1)) {
			transform.rigidbody2D.AddForce (new Vector2 (0,jumping_impact), ForceMode2D.Impulse);
						Jump = false;
		}
		if (Physics2D.Raycast(transform.position, -Vector2.up, floor_dist,3)) {
			Jump = true;
			if(debug)Debug.Log("Collision");
		}
		if(debug)Debug.Log("ready");
	}

}
