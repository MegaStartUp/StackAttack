using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool debug = true;

    private float moving_impact;
    private float jumping_impact;
    private float jumping_impact_multipl=1;
    private float floor_dist;
    private float jump_col_radius;

    private bool Jump = true;
    private bool Jump1 = false;
    private CsGlobals General_Processor;
    private Transform Load;
    private float higt_load=1.1f;
    private bool catch_l_f = false;
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

    }

    // Update is called once per frame
    void Update()
    {
        player_transl();
        catch_or_separation_load();
        anim_contrl();
        game_over();
    }

    //function for player transport
    void player_transl()
    {
        if (Jump)
        {
            RB2D.AddForce(new Vector2(moving_impact * General_Processor.move_horiz_button* 1f* Time.deltaTime, 0), ForceMode2D.Impulse);
            if (General_Processor.wait_state && !jump_active) RB2D.velocity = -RB2D.velocity;
            else RB2D.drag = 3;
        }
        else
            RB2D.AddForce(new Vector2(moving_impact * General_Processor.move_horiz_button *1f * Time.deltaTime * 0.2f, 0), ForceMode2D.Impulse);

        if (Jump && (General_Processor.jump_button))
        {
            RB2D.AddForce(new Vector2(0, jumping_impact * jumping_impact_multipl), ForceMode2D.Impulse);
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

            if (debug) Debug.Log("Collisions");
        }
        else
        {
            jump_active = true;
            Jump1 = true;
        }
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
            if ((hit.collider != null) && !catch_l_f)
            {
                if (Vector2.up != General_Processor.orient_vect)
                {
                    RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.up, General_Processor.interact_dist, LayerMask.GetMask("Load"));
                    if (hit1.collider == null)
                    {
                        Load = hit.transform;
                        Load.parent = this.transform;
                        catch_l_f = true;
                        jumping_impact_multipl =2f;
                    }
                }
                else
                {
                    Load = hit.transform;
                    Load.parent = this.transform;
                    catch_l_f = true;
                }

            }
            else
            {
                catch_l_f = false;
                hit = Physics2D.Raycast(transform.position, General_Processor.orient_vect, General_Processor.interact_dist+0.1f, LayerMask.GetMask("Load", "Border"));
                if (Load != null)
                {
                    if (hit.collider == null)
                        Load.position = new Vector2(transform.position.x + General_Processor.interact_dist * General_Processor.orient_vect.x, transform.position.y + General_Processor.interact_dist * General_Processor.orient_vect.y);

                        Load.position = new Vector2((int)Load.position.x + half_sector_width, Load.position.y);
                        Load.parent = null;
                        Load = null;
                        jumping_impact_multipl = 1;
                }
            } 
        }
        if (catch_l_f)
        {
            if (Load != null)
                Load.position = new Vector2(this.transform.position.x, this.transform.position.y + higt_load);
            else
                catch_l_f = false;
        }
            
    }
    void anim_contrl()
    {
        if(catch_l_f)//box is load
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
                                General_Processor.GmOv = true;
                            }

                        }
                    }

                }
            }
        }
    }

}

