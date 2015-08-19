using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour
{
    private Vector3 vect=-Vector3.up;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == Vector3.zero)
            transform.position = vect;
        else
            vect = transform.position;
    }
}
