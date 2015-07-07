using UnityEngine;
using System.Collections;

public class Player_catch : MonoBehaviour {
	public bool debag=true;
	private Vector2 Vect;
	private RaycastHit2D hit;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		switch ((int)Input.GetAxis ("Vertical")) {
		case -1:
			 Vect = Vector2.up;
			break;
		case 1:
			 Vect = -Vector2.up;
			break;
		default:
			switch ((int)Input.GetAxis ("Horizontal")) {
			case -1:
				 Vect = -Vector2.right;
				break;
			case 1:
				Vect = Vector2.right;
				break;
			default:
				return;
			}
			break;
		}
		if (0f != Input.GetAxis ("Catch")) 
		if (hit.collider == null) {
			hit = Physics2D.Raycast (transform.position, Vect,1,2);
			if(debag) Debug.Log ("catch"+hit.transform.name);
						if (hit.transform.name == "Load") {
								hit.transform.GetComponent<Rigidbody2D>().isKinematic = true;	
								hit.transform.parent = transform;
						}
		} else 
			if (hit.transform.name == "Load") {
				hit.transform.GetComponent<Rigidbody2D>().isKinematic = false;	
				hit.transform.parent = null;
			}
			
		
		}
		
	

}
