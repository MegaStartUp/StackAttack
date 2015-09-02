using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Last_score : MonoBehaviour {


    // Use this for initialization
    private CsGlobals Global;
    private Text t;
    void Start()
    {
        Global = GameObject.Find("General_Processor").transform.GetComponent<CsGlobals>();
        t = transform.GetComponent<Text>();
        t.text = "" + Global.last_score;

    }
    // Update is called once per frame
    void Update()
    {
        t.text = "" + Global.last_score;

    }
}
