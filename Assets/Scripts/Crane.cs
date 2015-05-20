using UnityEngine;
using System.Collections;

public class Crane : MonoBehaviour {
	
	public bool forward = true;
	public float speed = 5;
	public float unload_point = 2;
	
	public float left_end = -5;
	public float right_end = 5;
	public float high_crane = 3;
	public float wait_time = 0.01f;
	
	private bool load = true;
	private bool corout= true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
				if (corout) driver ();
		}

	void driver () {
		float step = speed*Time.deltaTime;
		if (load&&(Mathf.Abs(transform.position.x-unload_point)<=step))
		{
			transform.position=new Vector2(unload_point,high_crane);
			load = false;
			StartCoroutine(Wait());
			return;
		}
		if (forward)
		{
			transform.Translate(new Vector2(step,0));
			if (transform.position.x>right_end)
			{
				Destroy(gameObject);
			}
		}
		else
		{
			transform.Translate(new Vector2(-step,0));
			if (transform.position.x<left_end)
			{
				Destroy(gameObject);
			}
		}
		
		
	}
	
	
	void catch_off () {

		GameObject load_obj = transform.Find("Load").gameObject;
		load_obj.transform.rigidbody2D.gravityScale = 1;
		load_obj.transform.collider2D.enabled = true;
		BalanceLoad scr = load_obj.GetComponent<BalanceLoad>();
		scr.enabled = false;
		load_obj.transform.parent = null;
}
	
	IEnumerator Wait() {
		corout=false;
		yield return new WaitForSeconds(wait_time);
		catch_off();
		Debug.Log("Unloading");
		yield return new WaitForSeconds(wait_time);
		corout=true;
	}
}
