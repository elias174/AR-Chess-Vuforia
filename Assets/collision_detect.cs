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
    public string current_position;
    public int redcol;
    public int greencol;
    public int bluecol;
    string last_target_position;
    public float y_piece;
    public bool is_selected = false;
    public bool position_target_selected = false;
    float timerSec = 0;
    public Color32 original_color;
    //public string name_object;
    // Start is called before the first frame update

    void Start()
    {
        bishop = GameObject.Find(this.name);
        image_target_piece = GameObject.Find("piece");
        board_image_targe = GameObject.Find("ImageTarget");
        original_color = bishop.GetComponent<Renderer>().material.GetColor("_Color");
        this.tag = current_position;
        ManagerPositions.disabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_selected)
        {
            bishop.GetComponent<Renderer>().material.color = new Color32((byte)redcol, (byte)greencol, (byte)bluecol, 255);
            if (timer > 4)
            {
                bishop.transform.SetParent(image_target_piece.transform);
                is_selected = false;
            }
            timer += Time.deltaTime;

        }
        else if (position_target_selected)
        {
            if (timer_position > 4)
            {
                bishop.transform.SetParent(board_image_targe.transform);
                y_piece = GameObject.Find("BoardGrid").transform.position.y;
                ServiceLogicEngine.Instance.move_piece(current_position, last_target_position);
                if (GameObject.FindGameObjectsWithTag(last_target_position).Length == 1)
                {
                    GameObject.FindGameObjectWithTag(last_target_position).SetActive(false);
                }
                current_position = last_target_position;
                ManagerPositions.disabled = false;
                ManagerPositions.current_position = current_position;
                GameObject position_object = GameObject.Find(current_position);
                bishop.transform.position = new Vector3(position_object.transform.position.x, position_object.transform.position.y, position_object.transform.position.z);
                this.tag = current_position;
                position_target_selected = false;
                //Debug.Log(last_target_position);

            }
            timer_position += Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "cube")
        {
            // Debug.Log("Collision detected");
            is_selected = true;
            ManagerPositions.disabled = false;
            ManagerPositions.current_position = current_position;
        }
        else if (collision.gameObject.tag == "position")
        {
            if(collision.gameObject.name == this.tag)
            {
                return;
            }
            position_target_selected = true;
            last_target_position = collision.gameObject.name;
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
            ManagerPositions.disabled = true;
            ManagerPositions.current_position = "";
            ServiceLogicEngine.Instance.potential_moves.Clear();
        }
        else if (collision.gameObject.tag == "position")
        {
            position_target_selected = false;
            timer_position = 0;
            last_target_position = "";
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
