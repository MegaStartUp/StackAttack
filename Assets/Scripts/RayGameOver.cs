using UnityEngine;
using System.Collections;

public class RayGameOver : MonoBehaviour
{
    private int gm_ov_num;
    private GameObject General_Processor;

    // Use this for initialization
    void Start()
    {
        General_Processor = GameObject.Find("General_Processor");
        gm_ov_num = General_Processor.transform.GetComponent<CsGlobals>().sector_high_count;

    }

    // Update is called once per frame
    void Update()
    {
        int obj_num=0;
        RaycastHit2D[] hit_arr = Physics2D.RaycastAll(transform.position, Vector2.up);
        foreach (RaycastHit2D hit in hit_arr)
        {
            if (hit.transform.name == "Load" && hit.transform.parent != null)
                obj_num++;
        }
        if (obj_num >= gm_ov_num)
            General_Processor.transform.GetComponent<CsGlobals>().GmOv = true;
    }
}
