using UnityEngine;
using System.Collections;

public class Load_ray : MonoBehaviour
{
    ~Load_ray()
    {
        if (General_Processor != null)
        {
            General_Processor.score++;
            General_Processor.full_score++;
        }
    }
    public bool find = true;
    private float min_depth;
    private float max_depth;
    private float sector_width;
    private float sector_high;
    private Animator anim;
    CsGlobals General_Processor;
    //GameObject ray_line;//debag
    // Use this for initialization
    void Start()
    {
        General_Processor = GameObject.Find("General_Processor").GetComponent<CsGlobals>();
        sector_width = General_Processor.sector_width;
        sector_high = General_Processor.sector_high;

        min_depth = this.transform.localScale.x / 1.42f;
        max_depth = min_depth * 1.1f;
        anim = this.GetComponent<Animator>();
        change_anim("cube_fall");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Destroy(ray_line);
        ray_line = new GameObject();
        LineRenderer _line = ray_line.AddComponent<LineRenderer>();
        _line.SetPosition(0, new Vector2(transform.position.x, transform.position.y - min_depth));
        _line.SetPosition(1, new Vector2(transform.position.x, transform.position.y - max_depth));
        _line.SetWidth(0.05f, 0.05f);*/
        //debag
        if (Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y - min_depth), new Vector2(transform.position.x, transform.position.y - max_depth), LayerMask.GetMask("Load", "Border")))
        {
            if (find)
            {
                Fixed_obj();
                find = false;
                change_anim("cube_static");
            }
        }
        else
        {
            if (!find)
            {
                unFixed_obj();
                change_anim("cube_fall");
                find = true;
            }
        }

    }
    //include obj in net
    void Fixed_obj()
    {
        int coord_x = (int)(this.transform.position.x / sector_width);
        int coord_y = (int)(this.transform.position.y / sector_high);
        GameObject GObj = GameObject.Find("Sectors_net/" + coord_x + " - " + coord_y);
        if (GObj != null)
        {
            this.transform.parent = GObj.transform;
            this.transform.GetComponent<Rigidbody2D>().isKinematic = true;
            this.transform.rotation = Quaternion.identity;
            this.transform.position = GObj.transform.position;
            //Debug.Log(coord_x + " - " + coord_y + " - Fixed_obj ");//debag
        }
    }
    //exclusion obj in net
    void unFixed_obj()
    {
        //int coord_x = (int)(this.transform.position.x / sector_width);//debag
        //int coord_y = (int)(this.transform.position.y / sector_high);//debag
        this.transform.parent = null;
        this.transform.GetComponent<Rigidbody2D>().isKinematic = false;
        //Debug
    }
    //Change animation
    void change_anim(string str)
    {
        anim.SetBool("cube_static", false);
        anim.SetBool("cube_load", false);
        anim.SetBool("cube_fall", false);
        anim.SetBool(str, true);
    }
}
