using UnityEngine;
using System.Collections;

public class Global_builder : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject General_Processor = GameObject.Find("General_Processor");

        //Init var
        int sector_width_count = General_Processor.transform.GetComponent<CsGlobals>().sector_width_count;
        int sector_high_count = General_Processor.transform.GetComponent<CsGlobals>().sector_high_count;

        float sector_width = General_Processor.transform.GetComponent<CsGlobals>().sector_width;
        float sector_high = General_Processor.transform.GetComponent<CsGlobals>().sector_high;

        float high = General_Processor.transform.GetComponent<CsGlobals>().high;
        float width = General_Processor.transform.GetComponent<CsGlobals>().width;
        float ray_coord_x = General_Processor.transform.GetComponent<CsGlobals>().ray_coord_x;
        float ray_coord_y = General_Processor.transform.GetComponent<CsGlobals>().ray_coord_y;

        //Init gameobject templates 
        GameObject sector_pref = General_Processor.transform.GetComponent<CsGlobals>().sector_pref;
        GameObject destr_pref = General_Processor.transform.GetComponent<CsGlobals>().destr_pref;
        GameObject GOR_pref = General_Processor.transform.GetComponent<CsGlobals>().GOR_pref;
        GameObject border_pref = General_Processor.transform.GetComponent<CsGlobals>().border_pref;
        GameObject player_pref = General_Processor.transform.GetComponent<CsGlobals>().player_pref;
        //Gameobject var 
        GameObject sectors_net;
        GameObject destr_ray_net;
        GameObject GOR_pref_net;


        //Build rays net and sectors net 
        this.transform.position = new Vector3(width / 2, high / 2, -1);
        sectors_net = new GameObject();
        sectors_net.name = "Sectors_net";
        destr_ray_net = new GameObject();
        destr_ray_net.name = "Destr_ray_net";
        GOR_pref_net = new GameObject();
        GOR_pref_net.name = "Go_ray_net";
        create_sectors_rays(sectors_net.transform, sector_pref, destr_ray_net.transform, destr_pref, ray_coord_x, GOR_pref_net.transform, GOR_pref, ray_coord_y, new Vector2(sector_width, sector_high), new Vector2(sector_width_count, sector_high_count));
        
        //Build borders
        GameObject border = Instantiate(border_pref, new Vector3(width / 2, high / 2, 1), Quaternion.identity) as GameObject;
        border.transform.localScale = new Vector3(width, high, 1);
        border.name = "Border";

        //Build player
        GameObject player = Instantiate(player_pref, new Vector2(width / 2, sector_high), Quaternion.identity) as GameObject;
        player.name = "Player";

    }
    //Function build rays net and sectors net 
    void create_sectors_rays(Transform s_net, GameObject sector_pref, Transform d_net, GameObject destr_pref, float ray_coord_x, Transform g_net, GameObject GOR_pref, float ray_coord_y, Vector2 vect_scale, Vector2 vect_count)
    {

        float coord_x = vect_scale.x / 2, coord_y;
        for (int i = 0; i < vect_count.x; i++)
        {
            GameObject GmOv_ray = Instantiate(GOR_pref, new Vector2(coord_x, ray_coord_y), Quaternion.identity) as GameObject;
            GmOv_ray.name = "Game Over ray " + i;
            GmOv_ray.transform.parent = g_net;

            coord_y = vect_scale.y/2;

            for (int j = 0; j < vect_count.y; j++)
            {
                if (i == 0)
                {
                    GameObject destr_ray = Instantiate(destr_pref, new Vector2(ray_coord_x, coord_y), Quaternion.identity) as GameObject;
                    destr_ray.name = "Destroy ray "+j;
                    destr_ray.transform.parent = d_net;
                }
                GameObject sector = Instantiate(sector_pref, new Vector2(coord_x, coord_y), Quaternion.identity) as GameObject;
                sector.name = i+" - "+ j;
                sector.transform.parent = s_net;
                coord_y += vect_scale.y;
            }
            coord_x += vect_scale.x;

        }
    }
}
