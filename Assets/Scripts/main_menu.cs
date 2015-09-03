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
	public Camera camera_3;
	//для смены дневного и ночного режима
    public bool day = false;
    public bool night = false;

    //для анимации
    private Animator anim;
	private Animator Animation_2;
	private Animator Animation_1;
   
	
	void Start () {
		//присваиваем переменной ренд значение.
		Rend = GetComponent<Renderer> ();
        // для анимации кнопок 
        anim = this.GetComponent<Animator>();
        
	}
	//функции взаимодействия с курсором
	void OnMouseEnter(){
		Rend.material.color = Color.red;
        anim.SetBool("Animation_2", true);
        anim.SetBool("Animation_1", false);
        

	}
	void OnMouseOver(){
		Rend.material.color -= new Color (0,1F, 0, 0) * Time.deltaTime;
		anim.SetBool("Animation_2", true);
		anim.SetBool("Animation_1", false);
	}
	void OnMouseExit(){
		Rend.material.color = Color.white;
		anim.SetBool("Animation_2", false);
		anim.SetBool("Animation_1", true);

	}
	void OnMouseUp(){
		//задаю значение кнопке "играть"
		if(new_game==true){
            //back=false; 
            //option=false;
            //exit=false;
            //camera_2.enabled=true;
            //camera_1.enabled=false;
            //camera_3.enabled=false;
            Application.LoadLevel("Game");
		}
		//задаю значение кнопке "назад" 
		if(back==true){
			new_game=false;
			option=false;
			exit=false;
			camera_1.enabled=true;
			camera_2.enabled=false;
			camera_3.enabled=false;

		}
		// Задаю значение кнопке "настройки"
		if(option==true) {
			new_game=false;
			exit=false;
			back=false;
			camera_3.enabled = true;
			camera_2.enabled=false;
			camera_1.enabled = false;
		}
		//задаю значение кнопке "Выход"
		if(exit == true) {
			new_game=false;
			option=false;
			back=false;
			Application.Quit ();
		}

	}
	// Update is called once per frame
	void Update () {

	}
}
