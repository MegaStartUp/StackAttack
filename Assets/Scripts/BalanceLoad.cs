
using UnityEngine;
using System.Collections;


public class BalanceLoad : MonoBehaviour {
	public float shift_delay_time=2;
	private bool player_col=false;
	private float _shift_delay_time;

	public bool debug = false;
	
	void OnCollisionEnter2D(Collision2D col)
	{

		 if(col.gameObject.name == "Body")
		{
			player_col = true;
			_shift_delay_time=0;
			if(debug)Debug.Log("colcolcol"+col.gameObject.name);
		}
	}
		
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		 Balance ();
		
	}
	void Balance () 
	{
		if (!player_col)
		{
			float _shift=(float)(transform.position.x-0.5)%1;
			if(_shift>0.5)
			{
				if(debug)Debug.Log("Право");
				if(1-_shift<Time.deltaTime)
					transform.Translate(new Vector2(1-_shift,0));
				else
					transform.Translate(new Vector2(Time.deltaTime,0));

			}
			else
			{
				if(debug)Debug.Log("Лево");
				if(_shift<Time.deltaTime)
					transform.Translate(new Vector2(-_shift,0));
				else
					transform.Translate(new Vector2(-Time.deltaTime,0));
			}
		}
		else
			if(_shift_delay_time<shift_delay_time)
				_shift_delay_time+=Time.deltaTime;
			else
			player_col=false;


	}
}