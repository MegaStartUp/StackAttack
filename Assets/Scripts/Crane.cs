using UnityEngine;
using System.Collections;

public class Crane : MonoBehaviour
{

    public bool forward = true;
    public float speed = 5;
    public float unload_point = 2;

    private float left_end;
    private float right_end;
    private float high_crane;
    private float wait_time;
    private float gravity_for_load;

    private bool load = true;
    private bool corout = true;
    private float dif_time;



    // Use this for initialization
    void Start()
    {
        GameObject General_Processor = GameObject.Find("General_Processor");
        left_end = 0;
        right_end = General_Processor.transform.GetComponent<CsGlobals>().width;
        high_crane = General_Processor.transform.GetComponent<CsGlobals>().high - General_Processor.transform.GetComponent<CsGlobals>().sector_high;
        wait_time = General_Processor.transform.GetComponent<CsGlobals>().wait_time_for_crane;
        gravity_for_load = General_Processor.transform.GetComponent<CsGlobals>().gravity_for_load;
    }

    // Update is called once per frame
    void Update()
    {
        if (corout) 
            driver();
        else
            catch_off();
    }

    void driver()
    {
        float step = speed * Time.deltaTime;
        if (load && (Mathf.Abs(transform.position.x - unload_point) <= step))
        {
            transform.position = new Vector2(unload_point, high_crane);
            corout=load = false;
            dif_time = - wait_time;
            //StartCoroutine(Wait()); 
            return;
        }
        if (forward)
        {
            transform.Translate(new Vector2(step, 0));
            if (transform.position.x > right_end)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(new Vector2(-step, 0));
            if (transform.position.x < left_end)
            {
                Destroy(gameObject);
            }
        }


    }


    void catch_off()
    {
        if (dif_time < 0)
        {
            dif_time += Time.deltaTime;
            if (dif_time >= 0)
            {
                if (transform.Find("Load") != null)
                {
                    GameObject load_obj = transform.Find("Load").gameObject;
                    load_obj.transform.GetComponent<Rigidbody2D>().gravityScale = gravity_for_load;
                    load_obj.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                    load_obj.transform.GetComponent<Collider2D>().enabled = true;
                    load_obj.transform.GetComponent<Load_ray>().enabled = true;
                    //load_obj.transform.position = new Vector2(unload_point, high_crane);
                    load_obj.transform.parent = null;
                }
            }
        }
        else
        {
            dif_time += Time.deltaTime;
            if (dif_time >= wait_time)
                corout = true;
        }

    }
    /*
    IEnumerator Wait()
    {
       float w_time = Random.Range(0.01f,wait_time);
        corout = false;
        yield return new WaitForSeconds(w_time);
        catch_off();
        //Debug.Log("Unloading");
        yield return new WaitForSeconds(w_time);
        corout = true;
    }*/
}

