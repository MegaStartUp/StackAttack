using UnityEngine;
using System.Collections;

public class Respawn_crane : MonoBehaviour
{
    private float min_resp_time;
    private float max_resp_time;
    private float _achin_time_coef;

    private float init_time;
    private float var_time = 0.0f;

    private float min_speed;
    private float max_speed;

    private float left_end;
    private float right_end;
    private float high_crane;
    private float half_sector_width;

    private float load_anim_speed;
    private float fall_anim_speed;
    private float static_anim_speed;

    private GameObject crane_pref;
    private Crane scripte;
    private RuntimeAnimatorController[] Load_AnimatorController;
    private GameObject crane;
    private CsGlobals General_Processor;


    public bool debug = false;

    // Use this for initialization
    void Start()
    {
        General_Processor = GameObject.Find("General_Processor").transform.GetComponent<CsGlobals>();

        min_resp_time = General_Processor.min_resp_time;
        max_resp_time = General_Processor.max_resp_time;

        init_time = General_Processor.init_time;

        min_speed = General_Processor.min_speed;
        max_speed = General_Processor.max_speed;

        load_anim_speed = General_Processor.load_anim_speed;
        fall_anim_speed = General_Processor.fall_anim_speed;
        static_anim_speed = General_Processor.static_anim_speed;

        left_end = 0;
        right_end = General_Processor.width;
        high_crane = General_Processor.high - General_Processor.sector_high;
        half_sector_width = General_Processor.sector_width / 2;
        crane_pref = General_Processor.crane_pref;
        Load_AnimatorController = General_Processor.Load_AnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        respawn();

    }
    void respawn()
    {
        if (init_time <= var_time)
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                crane = Instantiate(crane_pref, new Vector2(left_end, high_crane), Quaternion.identity) as GameObject;
                scripte = crane.GetComponent<Crane>();
                scripte.forward = true;
            }
            else
            {
                crane = Instantiate(crane_pref, new Vector2(right_end, high_crane), Quaternion.identity) as GameObject;
                scripte = crane.GetComponent<Crane>();
                scripte.forward = false;
            }
            AnimatorController_random();
            scripte.speed = Random.Range(min_speed, max_speed);
            scripte.unload_point = half_sector_width + (int)Random.Range(left_end, right_end);
            _achin_time_coef = General_Processor.change_score_norm / (General_Processor.change_score_norm + General_Processor.full_score);
            init_time = Random.Range(min_resp_time * _achin_time_coef, max_resp_time * _achin_time_coef);
            var_time = 0;
        }
        else
            var_time += 1 * Time.deltaTime;

    }
    void AnimatorController_random()
    {
        if (Load_AnimatorController.Length == 0) return;
        Animator anim = crane.transform.Find("Load").gameObject.GetComponent<Animator>();
        anim.runtimeAnimatorController = Load_AnimatorController[(int)Random.Range(0, Load_AnimatorController.Length)];
        anim.SetBool("cube_load", true);
        anim.SetFloat("load_speed", load_anim_speed);
        anim.SetFloat("fall_speed", fall_anim_speed);
        anim.SetFloat("static_speed", static_anim_speed);
    }

}
