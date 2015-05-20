using UnityEngine;
using System.Collections;

public class Respawn_crane : MonoBehaviour {
	public float min_resp_time = 1.0f;
	public float max_resp_time = 5.0f;
	
	public float init_time = 1.0f;
	private float var_time = 0.0f;

	public float min_speed = 0.0f;
	public float max_speed = 5.0f;

	public float left_end = -5;
	public float right_end = 5;
	public float high_crane = 2.5f;

	public GameObject crane_pref;
	private Crane scripte;

	public bool debug = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		respawn ();
		
	}
	void respawn () {
		if(init_time<=var_time)
		{
			if(Random.Range(0.0f,1.0f)<0.5f)
			{
				 GameObject crane=Instantiate(crane_pref,new Vector2(left_end,high_crane),Quaternion.identity) as GameObject;
				 scripte=crane.GetComponent<Crane>();
				 scripte.forward=true;
				if(debug)Debug.Log("С лева");
			}
			else
			{
				GameObject crane=Instantiate(crane_pref,new Vector2(right_end,high_crane),Quaternion.identity) as GameObject;
				 scripte=crane.GetComponent<Crane>();
				scripte.forward=false;
				if(debug)Debug.Log("С права");
			}
			scripte.speed=Random.Range(min_speed,max_speed);
			scripte.unload_point=Random.Range(left_end,right_end);
			init_time=Random.Range(min_resp_time,max_resp_time);
			var_time=0;
		}
		else
		var_time+=1*Time.deltaTime;
		
	}

}
