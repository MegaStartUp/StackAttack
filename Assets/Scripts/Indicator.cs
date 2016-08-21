using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    private bool _change_f = true;
    private float _score = 0;
    public float score
    {
        set
        {
            _score = value;
            _change_f = true;
        }
        get
        {
            return _score;
        }
    }
    private int i_first;
    private int i_second;
    private int i_third;
    private int i_fourth;
    private int i_fifth;
    private Image first;
    private Image second;
    private Image third;
    private Image fourth;
    private Image fifth;
    public Sprite[] sprite;
	void Awake() 
    {
        sprite = GameObject.Find("General_Processor").GetComponent<CsGlobals>().sprite;
        first = transform.Find("First_c/First").GetComponent<Image>();
        second = transform.Find("Second_c/Second").GetComponent<Image>();
        third = transform.Find("Third_c/Third").GetComponent<Image>();
        fourth = transform.Find("Fourth_c/Fourth").GetComponent<Image>();
        fifth = transform.Find("Fifth_c/Fifth").GetComponent<Image>();
	}

    void Update()
    {
        if (_change_f)
        {
            Parser();
            _change_f = false;
        }
    }
    void Parser()
    {
            first.sprite = sprite[(int)(_score % 10)];
            second.sprite = sprite[(int)((_score / 10) % 10)];
            third.sprite = sprite[(int)((_score / 100) % 10)];
            fourth.sprite = sprite[(int)((_score / 1000) % 10)];
            fifth.sprite = sprite[(int)((_score / 10000) % 10)];
    }

}

