using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float moving_impact = 1;
	public float jumping_impact = 5;
	public float floor_dist = 0.1f;
	public float jump_col_radius = 0.5f;
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
		Vector3 center =  new Vector3 (0, -jump_col_radius), right = new Vector3 (jump_col_radius, -jump_col_radius), left = new Vector3 (-jump_col_radius, -jump_col_radius);
		if (Physics2D.Raycast(transform.position+ center, -Vector2.up, floor_dist,3)) {
			Jump = true;
			if(debug)Debug.Log("Collisions by centre");
		}
		if (Physics2D.Raycast(transform.position + left, -Vector2.up, floor_dist,3)) {
			Jump = true;
			if(debug)Debug.Log("Collisions by left");
		}
		if (Physics2D.Raycast(transform.position + right, -Vector2.up, floor_dist,3)) {
			Jump = true;
			if(debug)Debug.Log("Collisions by right");
		}
		if(debug)Debug.Log("ready");
	}
	
}
