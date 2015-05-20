using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float moving_impact = 1;
	public float jumping_impact = 5;
	private bool Jump=true;
	
	void OnCollisionEnter2D(){
		Jump = true;
		Debug.Log("Collision");
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
			Debug.Log("jump");
				}
	}

}
