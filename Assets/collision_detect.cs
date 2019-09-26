using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_detect : MonoBehaviour
{
    public GameObject bishop;
    public int redcol;
    public int greencol;
    public int bluecol;
    public bool is_selected = false;
    public Color32 original_color;
    // Start is called before the first frame update
    void Start()
    {
        bishop = GameObject.Find("Bishop");
        original_color = bishop.GetComponent<Renderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (is_selected)
        {
            bishop.GetComponent<Renderer>().material.color = new Color32((byte)redcol, (byte)greencol, (byte)bluecol, 255);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "cube")
        {
            Debug.Log("Collision detected");
            is_selected = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Finished collision");
        is_selected = false;
        bishop.GetComponent<Renderer>().material.color = original_color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "cube")
        {
            Debug.Log("Trigger eventtt");
        }
    }
}
