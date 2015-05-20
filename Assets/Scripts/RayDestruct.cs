using UnityEngine;
using System.Collections;

public class RayDestruct : MonoBehaviour {
	public int destr_num=8;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D[] hit_arr = Physics2D.RaycastAll(transform.position,Vector2.right);
		if (hit_arr.Length != 0) {
			int destroy_count=0;
			foreach(RaycastHit2D hit in hit_arr)
			{
				if(hit.transform.name=="Load") destroy_count++;
			}

			if(destroy_count>destr_num)
			foreach(RaycastHit2D hit in hit_arr)
			{
				if(hit.transform.name=="Load") Destroy(hit.transform.gameObject);
			}

		}
	}
}
