using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_detect : MonoBehaviour
{
    float timer = 0;
    float timer_position = 0;
    public GameObject bishop;
    public GameObject image_target_piece;
    public GameObject board_image_targe;
    public int redcol;
    public int greencol;
    public int bluecol;
    public float y_piece;
    public bool is_selected = false;
    public bool position_target_selected = false;
    float timerSec = 0;
    public Color32 original_color;
    public string name_object;
    // Start is called before the first frame update
    void Start()
    {
        bishop = GameObject.Find(name_object);
        image_target_piece = GameObject.Find("piece");
        board_image_targe = GameObject.Find("ImageTarget");
        original_color = bishop.GetComponent<Renderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (is_selected)
        {
            bishop.GetComponent<Renderer>().material.color = new Color32((byte)redcol, (byte)greencol, (byte)bluecol, 255);
            if(timer > 4)
            {
                bishop.transform.SetParent(image_target_piece.transform);
                is_selected = false;
            }
            timer += Time.deltaTime;
            // Vector3 pos = bishop.transform.position;
            // pos.y = y_piece;
            // bishop.transform.position = pos;
        }
        else if (position_target_selected)
        {
            if (timer_position > 4)
            {
                Debug.Log("changing... finish");
                bishop.transform.SetParent(board_image_targe.transform);
                position_target_selected = false;
            }
            Debug.Log(timer_position);
            timer_position += Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "cube")
        {
            // Debug.Log("Collision detected");
            is_selected = true;
        }
        else if (collision.gameObject.name == "position")
        {
            position_target_selected = true;
            Debug.Log("Collide in target position");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Debug.Log("Finished collision");
        if (collision.gameObject.name == "cube")
        {
            is_selected = false;
            bishop.GetComponent<Renderer>().material.color = original_color;
            timer = 0;
        }
        else if (collision.gameObject.name == "position")
        {
            position_target_selected = false;
            Debug.Log("Exit collision");
            timer_position = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "cube")
        {
            //Debug.Log("Trigger eventtt");
        }
    }
}
