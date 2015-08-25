using UnityEngine;
using System.Collections;

public class Player_status : MonoBehaviour {
	public GUISkin Skin;
	public bool enable=true;
	public float health=100;
	public float fury=100;
	public float health_max=100;
	public float fury_max=100;
	
	public float coord_x=10;
	public float coord_y=10;
	public float length=254;
	public float width=64;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI ()
	{
		if (enable) {
			float Health_rel_length=health/health_max;
			float Fury_rel_length=fury/fury_max;
			GUI.skin=Skin;
			GUI.Box(new Rect(coord_x,coord_y,length,width),"",GUI.skin.GetStyle("background"));
			GUI.Box(new Rect(coord_x,coord_y,length*Health_rel_length,width),"",GUI.skin.GetStyle("health"));
			GUI.Box(new Rect(coord_x,coord_y,length*Fury_rel_length,width),"",GUI.skin.GetStyle("fury"));
			GUI.Box(new Rect(coord_x,coord_y,length,width),"",GUI.skin.GetStyle("bar"));
        }
        GUI.Box(new Rect(Screen.width * 0.8f, Screen.height * 0.1f, Screen.width * 0.3f, Screen.height * 0.1f), "" + GameObject.Find("General_Processor").transform.GetComponent<CsGlobals>().score);
        if (GameObject.Find("General_Processor").transform.GetComponent<CsGlobals>().GmOv) GUI.Box(new Rect(Screen.width/2, Screen.height/2, Screen.width * 0.2f, Screen.height * 0.1f), "The Game Over");
	
    }
}
