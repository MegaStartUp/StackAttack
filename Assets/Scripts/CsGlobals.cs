using UnityEngine;
using System.Collections;

public class CsGlobals : MonoBehaviour
{
    //Scale parametrs
    public bool pause = false;

    //Scale parametrs
    public int sector_width_count = 16;
    public int sector_high_count = 9;

    public float sector_width = 1.0f;
    public float sector_high = 1.0f;
    public float ray_coord_x = -1.0f;
    public float ray_coord_y = -1.0f;


    //Automatic calculate
    public float high;
    public float width;


    //Global builder parametrs
    public GameObject sector_pref;
    public GameObject destr_pref;
    public GameObject GOR_pref;
    public GameObject border_pref;
    public GameObject player_pref;


    //Respawn crane parametrs
    public float min_resp_time = 1.0f;
    public float max_resp_time = 5.0f;

    public float init_time = 1.0f;

    public float min_speed = 1.0f;
    public float max_speed = 5.0f;

    public float load_anim_speed = 2.0f;
    public float fall_anim_speed = 0.5f;
    public float static_anim_speed = 2.0f;

    public GameObject crane_pref;

    public RuntimeAnimatorController[] Load_AnimatorController;


    //Crane parametrs
    public float wait_time_for_crane = 0.01f;
    public float gravity_for_load = 0.5f;


    //Player parametrs
    public float moving_impact = 10;
    public float jumping_impact = 10;
    public float floor_dist = 0.1f;
    public float jump_col_radius = 0.5f;
    public float interact_dist = 0.5f;



    //Player variables
    public Vector2 orient_vect;
    public int move_horiz_button;
    public int move_vertic_button;
    public float acacceleration_coef;
    public bool jump_button;
    public bool catch_button;
    public bool wait_state;


    //Score
    private int Score = 0;
    private int Top_Score = 0;
    private int Last_Score = 0;
    public int score
    {
        get
        {
            return Score;
        }
        set
        {
            if(Count!=null)
                Count.score = Score = value;
            else
                Score = value;
        }
    }
    public int top_score
    {
        get
        {
            return Top_Score;
        }
        set
        {
            if (T_Count != null)
                T_Count.score = Top_Score = value;
            else
                Top_Score = value;
        }
    }
    public int last_score
    {
        get
        {
            return Last_Score;
        }
        set
        {
            if (L_Count != null)
                L_Count.score = Last_Score = value;
            else
                Last_Score = value;
        }
    }
    public bool GmOv = false;
    private bool _GmOv = true;
    public Sprite[] sprite;
    
    //Preferens
    public float sound;
    public string language;
    public string mode;

    //Game Over objects
    public GameObject Pause;
    public GameObject Im_P;
    public GameObject Im_G_O;
    public GameObject BackGr_and_Menu;
    private IControl IContr;
    private Indicator Count;
    private Indicator L_Count;
    private Indicator T_Count;
    // Use this for initialization
    void Awake()
    {
        high = sector_high_count * sector_high;
        width = sector_width_count * sector_width;
        Pause = GameObject.Find("Pause");
        Im_P = GameObject.Find("Im_Pause");
        Im_G_O = GameObject.Find("Im_GameOver");
        BackGr_and_Menu = GameObject.Find("Backgr_and_Menu");
        Count = GameObject.Find("Counter").GetComponent<Indicator>();
        L_Count = GameObject.Find("L_Counter").GetComponent<Indicator>();
        T_Count = GameObject.Find("T_Counter").GetComponent<Indicator>();
        Im_G_O.SetActive(false);
        Overwrite_param();
        IContr=new IUControl_Release();
    }

    // Update is called once per frame
    void Update()
    {

        sound = Store.Value_Sound();
        language = Store.Lang();
        mode = Store.Mode();
        if (GmOv) game_over();
        else
            game();
    }

    void pause_game()
    {
        Time.timeScale = 0;
    }

    void unpause_game()
    {
        Time.timeScale = 1;
    }

    void game_over()
    {
        if (_GmOv)
        {

            Time.timeScale = 0.1f;
            Pause.SetActive(false);
            Im_P.SetActive(false);
            Im_G_O.SetActive(true);
            BackGr_and_Menu.SetActive(true);
            BackGr_and_Menu.GetComponent<Control_Button>().Click_Back_to_Setting();
            Store.Save_Score(score);
            top_score = Store.Top_result();
            last_score = Store.Last_result();
            _GmOv = false;
        }
    }

    void game()
    {
        jump_button = IContr.Get_Jump();
        catch_button = IContr.Get_Catch();
        move_vertic_button = IContr.Vertical_Axis();
        move_horiz_button = IContr.Horizontal_Axis();
        IContr.Get_Pause();
        if (move_vertic_button == 0 && move_horiz_button == 0)
        {
            wait_state = true;
        }
        else
        {
            orient_vect.x = move_horiz_button;
            orient_vect.y = move_vertic_button;
            wait_state = false;
        }

        if (pause)
            pause_game();
        else
            unpause_game();
        if (wait_state)
            acacceleration_coef = 0;
        else
            if(acacceleration_coef<1.0f)
                acacceleration_coef+=Time.deltaTime*5;
            else
                acacceleration_coef=1.0f;

    }
    public void Overwrite_param()
    {
        top_score = Store.Top_result();
        last_score = Store.Last_result();
        sound = Store.Value_Sound();
        language = Store.Lang();
        mode = Store.Mode();
    }
}
