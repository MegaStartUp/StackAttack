using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool debug = true;

    private float moving_impact;
    private float jumping_impact;
    private float floor_dist;
    private float jump_col_radius;

    private bool Jump = true;
    private GameObject General_Processor;
    private Transform Load;
    private float higt_load=1.3f;
    private bool catch_l_f = false;

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
        General_Processor = GameObject.Find("General_Processor");

        moving_impact = General_Processor.transform.GetComponent<CsGlobals>().moving_impact;
        jumping_impact = General_Processor.transform.GetComponent<CsGlobals>().jumping_impact;
        floor_dist = General_Processor.transform.GetComponent<CsGlobals>().floor_dist;
        jump_col_radius = General_Processor.transform.GetComponent<CsGlobals>().jump_col_radius;

    }

    // Update is called once per frame
    void Update()
    {
        player_transl();
        catch_load();
    }

    //function for player transport
    void player_transl()
    {

        if (Jump)
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(moving_impact * General_Processor.transform.GetComponent<CsGlobals>().move_horiz_button * Time.deltaTime, 0), ForceMode2D.Impulse);
        else
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(moving_impact * General_Processor.transform.GetComponent<CsGlobals>().move_horiz_button * Time.deltaTime / 5, 0), ForceMode2D.Impulse);

        if (Jump && (General_Processor.transform.GetComponent<CsGlobals>().jump_button == 1))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumping_impact), ForceMode2D.Impulse);
            Jump = false;
        }
        Vector3 center = new Vector3(0, -jump_col_radius), right = new Vector3(jump_col_radius, -jump_col_radius), left = new Vector3(-jump_col_radius, -jump_col_radius);
        if (Physics2D.Raycast(transform.position + center, -Vector2.up, floor_dist))
        {
            Jump = true;
            if (debug) Debug.Log("Collisions by centre");
        }
        if (Physics2D.Raycast(transform.position + left, -Vector2.up, floor_dist))
        {
            Jump = true;
            if (debug) Debug.Log("Collisions by left");
        }
        if (Physics2D.Raycast(transform.position + right, -Vector2.up, floor_dist))
        {
            Jump = true;
            if (debug) Debug.Log("Collisions by right");
        }
        if (debug) Debug.Log("ready");
    }
    //function for load catch
    void catch_load()
    {
        if (General_Processor.transform.GetComponent<CsGlobals>().catch_button)
        {
           /* Destroy(ray_line);
            ray_line = new GameObject();
            LineRenderer _line = ray_line.AddComponent<LineRenderer>();
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, new Vector2(transform.position.x + General_Processor.transform.GetComponent<CsGlobals>().interact_dist*General_Processor.transform.GetComponent<CsGlobals>().orient_vect.x, transform.position.y + General_Processor.transform.GetComponent<CsGlobals>().interact_dist * General_Processor.transform.GetComponent<CsGlobals>().orient_vect.y));
            _line.SetWidth(0.05f, 0.05f);*/
            RaycastHit2D hit = Physics2D.Raycast(transform.position, General_Processor.transform.GetComponent<CsGlobals>().orient_vect, General_Processor.transform.GetComponent<CsGlobals>().interact_dist, LayerMask.GetMask("Load"));
            if ((hit.collider != null) && !catch_l_f)
            {
                if (Vector2.up != General_Processor.transform.GetComponent<CsGlobals>().orient_vect)
                {
                    RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.up, General_Processor.transform.GetComponent<CsGlobals>().interact_dist, LayerMask.GetMask("Load"));
                    if (hit1.collider == null)
                    {
                        Load = hit.transform;
                        Load.parent = this.transform;
                        catch_l_f = true;
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
                hit = Physics2D.Raycast(transform.position, General_Processor.transform.GetComponent<CsGlobals>().orient_vect, General_Processor.transform.GetComponent<CsGlobals>().interact_dist, LayerMask.GetMask("Load", "Border"));
                if (Load != null)
                {
                    if (hit.collider == null)
                        Load.position = new Vector2(transform.position.x + General_Processor.transform.GetComponent<CsGlobals>().interact_dist * General_Processor.transform.GetComponent<CsGlobals>().orient_vect.x, transform.position.y + General_Processor.transform.GetComponent<CsGlobals>().interact_dist * General_Processor.transform.GetComponent<CsGlobals>().orient_vect.y);
                    Load.parent = null;
                    Load = null;
                }
            }
        }
        if (catch_l_f)
        {
            Load.position = new Vector2(this.transform.position.x, this.transform.position.y + higt_load);
        }
    }

}
