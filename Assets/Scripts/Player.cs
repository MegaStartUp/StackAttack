using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private float moving_impact;
    private float jumping_impact;
    private float jumping_impact_multipl=1;
    private float floor_dist;
    private float jump_col_radius;

    private bool Jump = true;
    private bool Jump1 = false;
    private CsGlobals General_Processor;
    private Transform Load;
    private float higt_load = 0.9f;
    private bool catch_l_f = false;
    private bool Catch_l_f
    {
        set
        {
            catch_l_f = value;
            if (value)
            {
                transform.GetComponent<CircleCollider2D>().radius = 0.4f;
                transform.GetComponent<CircleCollider2D>().offset = new Vector2(0, -0.1f);
            }
            else
            {
                transform.GetComponent<CircleCollider2D>().radius = 0.5f;
                transform.GetComponent<CircleCollider2D>().offset = Vector2.zero;
            }

        }
        get
        {
            return catch_l_f;
         }
    }
    private float jump_time = 0.1f;
    private float jump_time_dif = 0.0f;

    private float half_sector_width;

    private Animator anim;
    private Vector2 last_state_vect = new Vector2();
    private Vector2 last_position;
    private Vector3 center;
    private Vector3 right;
    private Vector3 left;
    private Rigidbody2D RB2D;
    private bool jump_active;

    //private float* move_horiz_button;
    //private float* jump_button;
    //private float* catch_button;
    //private float* interact_dist;
    //private Vector2 orient_vect;

  //  GameObject ray_line;

    void OnCollisionEnter2D()
    {
    }
    // Use this for initialization
    void Start()
    {
        General_Processor = GameObject.Find("General_Processor").transform.GetComponent<CsGlobals>();
        moving_impact = General_Processor.moving_impact;
        jumping_impact = General_Processor.jumping_impact;
        floor_dist = General_Processor.floor_dist;
        jump_col_radius = General_Processor.jump_col_radius;
        half_sector_width = General_Processor.sector_width / 2;
        anim = this.GetComponent<Animator>();
        center = new Vector3(0, -jump_col_radius);
        right = new Vector3(jump_col_radius/1.5f, -jump_col_radius);
        left = new Vector3(-jump_col_radius/1.5f, -jump_col_radius);
        RB2D = transform.GetComponent<Rigidbody2D>();
        Skill_Level = General_Processor.Skill_Level;
        General_Processor.Player_sc = this;

    }
    // Update is called once per frame
    void Update()
    {
        player_transl();
        catch_or_separation_load();
        use_skill(Activ_Skill, Skill_Level[Activ_Skill]);
        child_behaviour();
        anim_contrl();
        game_over();
    }

    //function for player transport
    void player_transl()
    {
        if (Jump)
        {
            //RB2D.MovePosition(RB2D.position + new Vector2(moving_impact * General_Processor.move_horiz_button * General_Processor.acacceleration_coef * Time.deltaTime, 0));
            RB2D.AddForce(new Vector2(moving_impact * General_Processor.move_horiz_button * time_shift * General_Processor.acacceleration_coef * Time.deltaTime, 0), ForceMode2D.Impulse);
            if (General_Processor.wait_state && !jump_active) RB2D.velocity = -RB2D.velocity;
            else RB2D.drag = 3;
        }
        else
            RB2D.AddForce(new Vector2(moving_impact * General_Processor.move_horiz_button* General_Processor.acacceleration_coef * Time.deltaTime * 0.2f, 0), ForceMode2D.Impulse);
            //RB2D.MovePosition(RB2D.position + new Vector2(moving_impact * General_Processor.move_horiz_button * General_Processor.acacceleration_coef * Time.deltaTime * 0.2f, 0));
        if (Jump && (General_Processor.jump_button))
        {
            RB2D.AddForce(new Vector2(0, jumping_impact * jumping_impact_multipl), ForceMode2D.Impulse);
            //RB2D.MovePosition(RB2D.position + new Vector2(0, jumping_impact * jumping_impact_multipl * Time.deltaTime));
            Jump1=Jump = false;
            jump_time_dif = 0;
        }

        if (Physics2D.Raycast(transform.position + center, -Vector2.up, floor_dist) || Physics2D.Raycast(transform.position + left, -Vector2.up, floor_dist) || Physics2D.Raycast(transform.position + right, -Vector2.up, floor_dist))
        {
            jump_active = false;
            if (Jump1)
                Jump = true;
            else
            {
                jump_time_dif += Time.deltaTime;
                if (jump_time_dif > jump_time)
                {
                    Jump1 = true;
                }
            }
        }
        else
        {
            jump_active = true;
            Jump1 = true;
        }
    }
    void anim_contrl()
    {
        if(Catch_l_f)//box is load
        {
            if(!Jump)//Player jump
            {

                if (General_Processor.orient_vect == Vector2.right)//Player go right
                {
                    change_anim("up_load_right");
                }
                else
                {
                    change_anim("up_load_left");
                }
            }
            else
            {

                if (General_Processor.orient_vect == Vector2.right)//Player go right
                {
                    change_anim("load_right");
                }
                else
                {
                    change_anim("load_left");
                }
            }
        }
        else
        {
            if (!Jump)//Player jump
            {

                if (General_Processor.orient_vect == Vector2.right)//Player go right
                {
                    change_anim("up_right");
                }
                else
                {
                    change_anim("up_left");
                }
            }
            else
            {
                if (General_Processor.wait_state)//Player stay
                {
                    if (last_state_vect == Vector2.right)//last  orient vector state
                    {
                        change_anim("right_stop");
                    }
                    else
                    {
                        change_anim("left_stop");
                    }
                    return;
                }
                else
                {
                    if (General_Processor.orient_vect == Vector2.right)//Player go right
                    {
                        change_anim("move_right");
                    }
                    else
                    {
                        change_anim("move_left");
                    }
                }
            }

        }

        last_state_vect.x = General_Processor.orient_vect.x;
        last_state_vect.y = General_Processor.orient_vect.y;
    }
    void change_anim(string str)
    {
        anim.SetBool("left_stop", false);
        anim.SetBool("right_stop", false);
        anim.SetBool("move_left", false);
        anim.SetBool("move_right", false);
        anim.SetBool("load_left", false);
        anim.SetBool("load_right", false);
        anim.SetBool("up_left", false);
        anim.SetBool("up_right", false);
        anim.SetBool("up_load_left", false);
        anim.SetBool("up_load_right", false);
        anim.SetBool(str, true);
    }
    private bool _GmOv = false;
    private float _GmOv_Time = 0;
    void game_over()
    {
        RaycastHit2D[] hit_arr_t = Physics2D.RaycastAll(transform.position, Vector2.up, 2.0f * General_Processor.sector_high);
        foreach (RaycastHit2D hit in hit_arr_t)
        {
            if (hit.transform.name == "Load" && hit.transform.parent != null && hit.transform.parent != this.transform)
            {
                RaycastHit2D[] hit_arr_r = Physics2D.RaycastAll(transform.position, Vector2.right, General_Processor.sector_high);
                foreach (RaycastHit2D hit_r in hit_arr_r)
                {
                    if ((hit_r.transform.name == "Load" && hit_r.transform.parent != null && hit_r.transform.parent != this.transform) || hit_r.transform.name == "right_wall")
                    {
                        RaycastHit2D[] hit_arr_l = Physics2D.RaycastAll(transform.position, Vector2.left, General_Processor.sector_high);
                        foreach (RaycastHit2D hit_l in hit_arr_l)
                        {
                            if ((hit_l.transform.name == "Load" && hit_l.transform.parent != null && hit_l.transform.parent != this.transform) || hit_l.transform.name == "left_wall")
                            {
                                _GmOv = true;
                            }

                        }
                    }

                }
            }
        }
        if (_GmOv)
        {
            _GmOv_Time += Time.deltaTime;
            _GmOv = false;
            if (_GmOv_Time >= General_Processor.GmOv_Time) General_Processor.GmOv = true;
        }
        else
            _GmOv_Time = 0;
    }

    public int Activ_Skill=0;
    public int[] Skill_Level = { 0, 0, 0, 0, 0 };

    private float _score;
    void score_change()
    {
        if (_s_t_a || _imb)
            _score -= (_s_t_dec + _i_dec) * Time.deltaTime / Time.timeScale;
        if (_score <0)
        {
            all_unactive();
            General_Processor.score = _score= 0;
        }
        else
            General_Processor.score = _score;
    }

    void use_skill(int skill, int level)
    {
        _score = General_Processor.score;
        switch (skill)
        {
            case 0:
                blink(level);
                break;
            case 1:
                explosion(level);
                break;
            case 2:
                slow_time(level);
                break;
            case 3:
                imblnc(level);
                break;
            default:
                return;
        }
        if (General_Processor.F_GmOv && !General_Processor.pause) score_change();

    }
    //function for load catch
    void catch_or_separation_load()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, General_Processor.orient_vect, General_Processor.interact_dist, LayerMask.GetMask("Load"));
        if (hit.collider != null)
        {
            hit.transform.GetComponent<Color_control>().separation = true;
            hit.transform.GetComponent<Color_control>().change_fl = true;
        }
        if (General_Processor.catch_button)
        {
            /* Destroy(ray_line);
             ray_line = new GameObject();
             LineRenderer _line = ray_line.AddComponent<LineRenderer>();
             _line.SetPosition(0, transform.position);
             _line.SetPosition(1, new Vector2(transform.position.x + General_Processor.interact_dist*General_Processor.orient_vect.x, transform.position.y + General_Processor.interact_dist * General_Processor.orient_vect.y));
             _line.SetWidth(0.05f, 0.05f);*/
            if ((hit.collider != null) && !Catch_l_f)
            {
                if (Vector2.up != General_Processor.orient_vect)
                {
                    RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.up, General_Processor.interact_dist, LayerMask.GetMask("Load"));
                    if (hit1.collider == null)
                    {
                        Load = hit.transform;
                        Load.parent = this.transform;
                        Catch_l_f = true;
                        jumping_impact_multipl = 2f;
                    }
                }
                else
                {
                    Load = hit.transform;
                    Load.parent = this.transform;
                    Catch_l_f = true;
                }

            }
            else
            {
                hit = Physics2D.Raycast(transform.position, General_Processor.orient_vect, General_Processor.interact_dist + 0.1f, LayerMask.GetMask("Load", "Border"));
                if (Load != null)
                {
                    if (hit.collider == null)
                        Load.position = new Vector2(transform.position.x + General_Processor.interact_dist * General_Processor.orient_vect.x, transform.position.y + General_Processor.interact_dist * General_Processor.orient_vect.y);

                    Load.position = new Vector2((int)Load.position.x + half_sector_width, Load.position.y);
                    Load.parent = null;
                    Load = null;
                }
            }
        }

    }

    bool _b_a = false;
    Vector3 _bl_coor;
    public Vector3 bl_coor
    {
        set
        {
            _bl_coor = value;
            _b_a = true;
        }
        get
        {
            _b_a = false;
            return _bl_coor;
        }
    }
    private float _expense = 0;
    public float bl_n_exp = 5f;
    void blink(int level)
    {
        if (General_Processor.skill_button && !General_Processor.Sec_Can_Activ && level != 0)
        {
            General_Processor.Sec_Can_Activ = true;
        }
        else
        {
            if (_b_a)
            {
                _expense = (transform.position - bl_coor).magnitude * bl_n_exp / level;
                if (_expense <= _score)
                {
                    _score -= _expense;
                    transform.position = bl_coor;
                }
                General_Processor.Sec_Can_Activ = false;
            }
        }


    }

    private GameObject[] Loads;
    private float _radius_explosion = 0f;
    public float radius_explosion_coef = 1.5f;
    private void explosion(int level)
    {
        if (General_Processor.skill_button && level != 0)
        {
                _radius_explosion = level * radius_explosion_coef;
                _expense = _radius_explosion * radius_explosion_coef * 3.14f;
            if (_expense <= _score)
            {
                _radius_explosion = level * radius_explosion_coef;
                Loads = GameObject.FindGameObjectsWithTag("Load");
                foreach (GameObject load in Loads)
                {
                    if ((this.transform.position - load.transform.position).magnitude <= _radius_explosion)
                        Destroy(load);
                }
                _score -= _expense;
            }
        }
    }

    private bool _s_t_a = false;
    private float _s_t_n_dec = 5f;
    private float _s_t_dec = 0;
    private float time_shift = 1;
    private void slow_time(int level)
    {
        if (General_Processor.skill_button && level != 0)
        {
            if (_s_t_a)
            {
                _s_t_a = false;
                General_Processor.now_t = Time.timeScale = General_Processor.real_t;
                time_shift = 1f;
                _s_t_dec =0;
            }
            else
            {
                _s_t_a = true;
                General_Processor.now_t = Time.timeScale = General_Processor.slow_skill_t;
                time_shift = level;
                _s_t_dec = _s_t_n_dec / level;
            }
        }
    }
    private bool _imb = false;
    private float _i_n_dec = 5f;
    private float _i_dec = 0;
    private void imblnc(int level)
    {
        if (General_Processor.skill_button && level != 0)
        {
            if (_imb)
            {
                _imb = General_Processor.imb = false;
                General_Processor.GmOv = false;
                _i_dec = 0;
            }
            else
            {
                _imb = General_Processor.imb = true;
                _i_dec = _i_n_dec / level;
            }
        }
    }
    private void all_unactive()
    {
        //for blink
        General_Processor.Sec_Can_Activ = false;
        //for Slow Time
        _s_t_a = false;
        _s_t_dec = 0;
        if(!General_Processor.GmOv)
        General_Processor.now_t = Time.timeScale = General_Processor.real_t;
        //for imbalanc
        _imb = General_Processor.imb = false;
        General_Processor.GmOv = false;
        _i_dec = 0;

    }
    void child_behaviour()
    {
        if (Catch_l_f)
        {
            if (Load != null)
            {
                Load.position = new Vector2(this.transform.position.x, this.transform.position.y + higt_load);
            }
            else
            {
                jumping_impact_multipl = 1;
                Catch_l_f = false;
            }
        }

    }
}

