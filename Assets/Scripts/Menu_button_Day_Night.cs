using UnityEngine;
using System.Collections;

public class Menu_button_Day_Night : MonoBehaviour {
	public bool day = false;
	public Texture[] textures;
	public Texture main_menu_texture;

	// Use this for initialization
	void Start () {
		//присваиваем переменной ренд значение.
	}
	void OnMouseUp(){
		//задаю значение кнопкам день-ночь
		if(day==false){
			change_rend(1);
			day=true;
		}
		else{
			change_rend(0);
			day=false;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
	void change_rend(int index)
	{
		GameObject.Find("Cube").transform.GetComponent<Renderer> ().material.mainTexture=textures[index];
		GameObject.Find("Cube (1)").transform.GetComponent<Renderer> ().material.mainTexture=textures[index];
		GameObject.Find("Cube (2)").transform.GetComponent<Renderer> ().material.mainTexture=textures[index];
	}
}
