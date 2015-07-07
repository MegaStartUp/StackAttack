using UnityEngine;
using System.Collections;

public class CsGlobals : MonoBehaviour
{
    //Scale parametrs
    public int sector_width_count=16;
    public int sector_high_count=9;

    public float sector_width = 1.0f;
    public float sector_high = 1.0f;
    public float ray_coord_x = -1.0f;
    //Automatic calculate
    public float high;
    public float width;


    //Global builder parametrs
    public GameObject sector_pref;
    public GameObject destr_pref;
    public GameObject border_pref;
    public GameObject player_pref;


    //Respawn crane parametrs
    public float min_resp_time = 1.0f;
    public float max_resp_time = 5.0f;

    public float init_time = 1.0f;

    public float min_speed = 1.0f;
    public float max_speed = 5.0f;

    public GameObject crane_pref;

    public Texture[] textures;


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
    public float move_horiz_button;
    public float move_vertic_button;
    public float jump_button;
    public bool catch_button;



    // Use this for initialization
    void Awake()
    {
      
        high = sector_high_count * sector_high;
        width = sector_width_count * sector_width;
    }

    // Update is called once per frame
    void Update()
    {
        move_horiz_button = Input.GetAxis("Horizontal");
        move_vertic_button = Input.GetAxis("Vertical");
        jump_button = Input.GetAxis("Jump");
        catch_button = Input.GetKeyDown(KeyCode.E);//Catch key
        if ((0 != (int)move_horiz_button) || (0 != (int)move_vertic_button)) orient_vect = new Vector2((int)move_horiz_button, (int)move_vertic_button);
    }
}
