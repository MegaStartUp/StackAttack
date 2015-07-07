using UnityEngine;
using System.Collections;

public class Animation_player : MonoBehaviour {
	public bool ismove= false;
	private float mytimer=1.0F;
	/*private float seconds=0.0F; 
	private int minutes=0;*/
	private Animator left;
	private Animator right; 
	private Animator right_stop;
	private Animator left_stop;
	private Animator stop;
	private Animator anim; 
	private bool left_flag=false;
	private bool right_flag=false;
    public RuntimeAnimatorController[] runtimeAnimatorController;

	void Start(){
		anim = this.GetComponent<Animator> ();
        anim.runtimeAnimatorController = runtimeAnimatorController[0];

	}
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            anim.runtimeAnimatorController = runtimeAnimatorController[0];
        if (Input.GetKeyDown(KeyCode.R))
            anim.runtimeAnimatorController = runtimeAnimatorController[1];
        if (Input.GetKeyDown(KeyCode.T))
            anim.runtimeAnimatorController = runtimeAnimatorController[2];
        if (Input.GetKeyDown(KeyCode.Y))
            anim.runtimeAnimatorController = runtimeAnimatorController[3];
		if(!(Input.GetKey (KeyCode.D)&&Input.GetKey (KeyCode.A))){

																 
		if (Input.GetKey (KeyCode.D)) {
						anim.SetBool ("right", true);
						anim.SetBool ("right_stop", false);
						anim.SetBool ("left", false);
						anim.SetBool ("left_stop",false);
			            right_flag=true;
						left_flag=false;
				}
		else{
			if(right_flag==true){
									right_flag=false;
									anim.SetBool ("right_stop", true);
									anim.SetBool ("right", false);
									anim.SetBool ("left", false);
									anim.SetBool ("left_stop",false);
								}

			}
		if (Input.GetKey (KeyCode.A)) {
			 			anim.SetBool("right",false);
						anim.SetBool ("left", true);
						anim.SetBool ("right_stop", false);
						anim.SetBool ("left_stop",false);
						right_flag=false;
						left_flag=true;
		}
			
		else{
			if(left_flag==true){
									left_flag=false;
									anim.SetBool ("left_stop", true);
									anim.SetBool ("right", false);
									anim.SetBool ("left", false);
									anim.SetBool ("right_stop",false);
			}

	}
		}
		//делаю таймер.
		if (!(Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)|| Input.GetKey (KeyCode.Space))) {
													mytimer+=Time.deltaTime;
													//Debug.Log("время:"+mytimer);
				}

}
			
}
