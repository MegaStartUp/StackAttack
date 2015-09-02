using UnityEngine;
using System.Collections;

public class Color_control : MonoBehaviour
{
    private SpriteRenderer _SRend;
    public bool change_fl = false;
    public bool separation = false;
    // Use this for initialization
    void Start()
    {
        _SRend = transform.GetComponent<SpriteRenderer>();
        _SRend.color = new Color(1f, 1f, 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (separation)
        {
            if (change_fl)
            {
                _SRend.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                change_fl = false;
            }
            else
            {
                _SRend.color = new Color(1f, 1f, 1f, 1f);
                separation = false;
            }
            
        }
    }
}
