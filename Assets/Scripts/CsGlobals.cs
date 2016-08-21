using UnityEngine;
using System.Collections;

public class CsGlobals : MonoBehaviour
{
    //pause parametrs
    private bool _pause = false;
    public bool pause
    {
        get
        {
            return _pause;
        }
        set
        {
            _pause = value;
            if (value)
                Time.timeScale = pause_t;
            else
                if (skill_s_a)
                    Time.timeScale = skills_t;
                else
                    Time.timeScale = now_t;
        }
    }

    //Time parametrs
    public float real_t = 1f;
    public float pause_t = 0;
    public float skills_t = 0.05f;
    public float gm_ov_t = 0.1f;
    public float slow_skill_t = 0.1f;
    public float now_t = 1f;
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

    //Skills parametrs
    public int score_start_first_skill = 50;
    public int score_start_second_skill = 150;
    public int score_start_third_skill = 250;
    public int score_start_fourth_skill = 600;
    private bool _skill_s_a = false;
    public bool skill_s_a
    {
        set
        {
            _skill_s_a = value;
            if (value)
                Time.timeScale = skills_t;
            else
                Time.timeScale = now_t;
        }
        get
        {
            return _skill_s_a;
        }
    }
    private bool _s_c_a = false;
    public bool Sec_Can_Activ
    {
        set
        {
            foreach (GameObject S_C in Sector_Canvases)
                S_C.SetActive(value);
            _s_c_a = value;
        }
        get
        {
            return _s_c_a;
        }
    }
    public GameObject[] Sector_Canvases;
    public Color[] Activ_Color;
    public Color[] Level_Color;
    public Sprite Activ_Level_Sprite;
    private int _Activ_Skill = 0;
    public int Activ_Skill
    {
        set
        {
            _Activ_Skill = value;
            Player_sc.Activ_Skill = value;
            Sec_Can_Activ = false;
        }
    }
    //public bool refresh_lvl
    //{
    //    set
    //    {
    //        Player_sc.Skill_Level = Skill_Level;
    //    }
    //}
    public int[] Skill_Level = { 0, 0, 0, 0};
    public float[][] Skill_Level_Exp_Score = new float[4][];
    public void init_s_l_e_s()
    {
        Skill_Level_Exp_Score[0] = new float[] { score_start_first_skill, 1.5f * score_start_first_skill, 1.5f * 1.5f * score_start_first_skill, 1.5f * 1.5f * 1.5f * score_start_first_skill, 1.5f * 1.5f * 1.5f * 1.5f * score_start_first_skill,99999999 };
        Skill_Level_Exp_Score[1] = new float[] { score_start_second_skill, 1.5f * score_start_second_skill, 1.5f * 1.5f * score_start_second_skill, 1.5f * 1.5f * 1.5f * score_start_second_skill, 1.5f * 1.5f * 1.5f * 1.5f * score_start_second_skill, 99999999 };
        Skill_Level_Exp_Score[2] = new float[] { score_start_third_skill, 1.5f * score_start_third_skill, 1.5f * 1.5f * score_start_third_skill, 1.5f * 1.5f * 1.5f * score_start_third_skill, 1.5f * 1.5f * 1.5f * 1.5f * score_start_third_skill, 99999999 };
        Skill_Level_Exp_Score[3] = new float[] { score_start_fourth_skill, 1.5f * score_start_fourth_skill, 1.5f * 1.5f * score_start_fourth_skill, 1.5f * 1.5f * 1.5f * score_start_fourth_skill, 1.5f * 1.5f * 1.5f * 1.5f * score_start_fourth_skill, 99999999 };
    }
    //public float[][] Skill_Level_Exp_Score ={{ 0, 0, 0, 0, 0 },
    //                                         { 0, 0, 0, 0, 0 },
    //                                         { 0, 0, 0, 0, 0 },
    //                                         { 0, 0, 0, 0, 0 },
    //                                         { 0, 0, 0, 0, 0 }};
    //p;ublic int this[int i]
    //{
    //    set
    //    {
    //        _Skill_Level[i] = value;
    //        Player_sc.Skill_Level = Skill_Level;
    //    }
    //}

    //blink skill
    public Player Player_sc;
    private Vector2 _bl_coor;
    public Vector2 bl_coor
    {
        set
        {
            _bl_coor = value;
            Player_sc.bl_coor = _bl_coor;
        }
    }
    public bool imb = false;


    //Player variables
    public Vector2 orient_vect;
    public int move_horiz_button;
    public int move_vertic_button;
    public float acacceleration_coef;
    private float _aclrtn_p;
    public bool jump_button;
    public bool catch_button;
    public bool skill_button;
    public bool wait_state;


    //Score
    private float Score = 200;
    private int Top_Score = 0;
    private int Last_Score = 0;
    public float full_score = 0;
    public float change_score_norm = 0;
    public float score
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
    public bool F_GmOv = true;
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
    public float GmOv_Time = 0.5f;
    private IControl IContr;
    private Indicator Count;
    private Indicator L_Count;
    private Indicator T_Count;
    // Use this for initialization
    void Awake()
    {
        init_s_l_e_s();
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
        if (GmOv && !imb)
            game_over();
        else
            game();
    }

    void game_over()
    {
        if (F_GmOv)
        {
            Time.timeScale = gm_ov_t;
            Pause.SetActive(false);
            Im_P.SetActive(false);
            Im_G_O.SetActive(true);
            BackGr_and_Menu.SetActive(true);
            BackGr_and_Menu.GetComponent<Control_Menu_Button>().Click_Back_to_Setting();
            Store.Save_Score((int)score);
            top_score = Store.Top_result();
            last_score = Store.Last_result();
            F_GmOv = false;
        }
    }

    void game()
    {
        IContr.Get_Pause();
        IContr.Get_Skill_Sets();
        up_first_level_of_skill();
        if (!_skill_s_a)
        {
            skill_button = IContr.Get_Skill();
            jump_button = IContr.Get_Jump();
            catch_button = IContr.Get_Catch();
            move_vertic_button = IContr.Vertical_Axis();
            move_horiz_button = IContr.Horizontal_Axis();
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
            if (wait_state)
                _aclrtn_p = 0;
            else
                if ((_aclrtn_p < 1.0f) && !jump_button)
                    //&& !jump_button
                    _aclrtn_p += Time.deltaTime * 5;
                else
                    _aclrtn_p = 1.0f;
            acacceleration_coef = _aclrtn_p * _aclrtn_p * _aclrtn_p * _aclrtn_p;
        }

    }
    private bool _count_change = true;
    private int _skill_numb = 0;
    private float _compare;
    public Skill_Sets_Bottom S_S_B_sc;

    public void up_first_level_of_skill()
    {
        if (_count_change && _skill_numb < 5)
            if (Skill_Level[_skill_numb] == 0)
            {
                switch (_skill_numb)
                {
                    //case 0:
                    //    if (Score >= _compare)
                    //    {
                    //        S_S_B_sc.Plus_First_Skill();
                    //        _level_numb++;
                    //        if (_level_numb >= 5) _level_numb = 4;
                    //    }
                    //    break;
                    case 0:
                        if (Score >= score_start_first_skill)
                        {
                            S_S_B_sc.Plus_First_Skill();
                            _skill_numb++;
                        }
                        break;
                    case 1:
                        if (Score >= score_start_second_skill)
                        {
                            S_S_B_sc.Plus_Second_Skill();
                            _skill_numb++;
                        }
                        break;
                    case 2:
                        if (Score >= score_start_third_skill)
                        {
                            S_S_B_sc.Plus_Third_Skill();
                            _skill_numb++;
                        }
                        break;
                    case 3:
                        if (Score >= score_start_fourth_skill)
                        {
                            S_S_B_sc.Plus_Fourth_Skill();
                            _skill_numb++;
                        }
                        break;
                    default:
                        break;

                }
            }
    }
    public void Overwrite_param()
    {
        top_score = Store.Top_result();
        last_score = Store.Last_result();
        sound = Store.Value_Sound();
        language = Store.Lang();
        mode = Store.Mode();
        if (Store.Is_Load_This_Start() == 1)
        {
            score = Store.Load_Now_Score_Game_Play_Poin();
            full_score = Store.Load_General_Score_Game_Play_Poin();
            Store.Load_Level_Game_Play_Poin(Skill_Level);
            Store.Dnt_Load_Next_Start();
        }
    }
}
