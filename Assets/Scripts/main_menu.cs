using UnityEngine;
using System.Collections;

public class main_menu : MonoBehaviour {
	public bool new_game=false;
	public bool option=false;
	public bool developer=false;
	public bool exit=false;
	public bool back=false;
	//переменная типа рендер
	public Renderer Rend; 
	//переменные типа: Камера.
	public Camera camera_1;
	public Camera camera_2;


	// Use this for initialization
	void Start () {
		//присваиваем переменной ренд значение.
		Rend = GetComponent<Renderer> ();
	}
	//функции взаимодействия с курсором
	void OnMouseEnter(){
		Rend.material.color = Color.red;
	}
	void OnMouseOver(){
		Rend.material.color -= new Color (0,1F, 0, 0) * Time.deltaTime;
	}
	void OnMouseExit(){
		Rend.material.color = Color.white;
	}
	void OnMouseUp(){
		//задаю значение кнопке "играть"
		if(new_game==true){
			camera_1.enabled=false;
			camera_2.enabled=true;
		}
		//задаю значение кнопке "назад" 
		if(back==true){
			camera_1.enabled=true;
			camera_2.enabled=false;
		}
		//задаю значение кнопке "Выход"
		if (exit == true) {
			Application.Quit ();
		}
	}
	// Update is called once per frame
	void Update () {

	}
}
