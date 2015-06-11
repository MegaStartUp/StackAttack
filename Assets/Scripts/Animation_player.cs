using UnityEngine;
using System.Collections;

public class Animation_player : MonoBehaviour {
	public bool ismove= false;
	private Animator left;
	private Animator right;
	private Animator stop;
	private Animator anim; 

	void Start(){
		anim = this.GetComponent<Animator> ();

	}
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.D)) {
						anim.SetBool ("right", true);
						anim.SetBool ("left", false);
				}
		if (Input.GetKey (KeyCode.A)) {
			 			anim.SetBool("right",false);
						anim.SetBool ("left", true);
		}
			


	}
}
