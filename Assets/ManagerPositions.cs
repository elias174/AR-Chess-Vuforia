using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPositions : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject[] potential_moves;
    public static string current_position = "";
    public static bool disabled = false;
    public bool in_progess_operation = false;
    // Start is called before the first frame update
    void Start()
    {
        ServiceLogicEngine.Instance.start_game();
        positions = GameObject.FindGameObjectsWithTag("position");
    }

    void set_potential_moves(string current_position)
    {
        if(!ServiceLogicEngine.Instance.in_progess_operation && current_position != "")
        {
            ServiceLogicEngine.Instance.get_possible_moves(current_position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        set_potential_moves(current_position);
        foreach (GameObject position in positions)
        {
            if (disabled)
            {
                position.SetActive(false);
            }
            else
            {
                if(ServiceLogicEngine.Instance.potential_moves.Contains(position.name))
                {
                    position.SetActive(true);
                }
            }
        }
    }
}
