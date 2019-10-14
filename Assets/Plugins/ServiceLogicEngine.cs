using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServiceLogicEngine : MonoBehaviour
{
    // This field can be accesed through our singleton instance,
    // but it can't be set in the inspector, because we use lazy instantiation
    public int number;
    public string game_id;
    public string position_for_get_moves;
    public List<string> potential_moves = new List<string>();
    public bool in_progess_operation = false;

    // Static singleton instance
    private static ServiceLogicEngine instance;

    // Static singleton property
    public static ServiceLogicEngine Instance
    {
        // Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
        // otherwise we assign instance to a new component and return that
        get
        {
            return instance ?? (instance = new GameObject("ServiceLogicEngine").AddComponent<ServiceLogicEngine>());
        }
    }

    // Instance method, this method can be accesed through the singleton instance
    public void DoSomeAwesomeStuff()
    {
        Debug.Log("I'm doing awesome stuff");
    }

    public void start_game()
    {
        StartCoroutine(web_start_game());
    }
    IEnumerator web_start_game()
    {
        in_progess_operation = true;
        UnityWebRequest www = UnityWebRequest.Get("http://chess-api-chess.herokuapp.com/api/v1/chess/two");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var object_request = JSON.Parse(www.downloadHandler.text);
            if (game_id == null)
                game_id = object_request["game_id"].Value;
        }
        in_progess_operation = false;
    }

    public void get_possible_moves(string position)
    {
        position_for_get_moves = position;
        StartCoroutine(web_get_possible_moves());
    }

    IEnumerator web_get_possible_moves()
    {
        in_progess_operation = true;
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        string data_str = string.Format("game_id={0}&position={1}", game_id, position_for_get_moves);
        WWWForm form = new WWWForm();
        form.AddField("game_id", game_id);
        form.AddField("position", position_for_get_moves);

        UnityWebRequest www = UnityWebRequest.Post("http://chess-api-chess.herokuapp.com/api/v1/chess/two/moves", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Exisssst and error");
        }
        else
        {
            potential_moves.Clear();
            var object_request = JSON.Parse(www.downloadHandler.text);
            JSONArray arr = object_request["moves"].AsArray;
            Debug.Log("----Potential-------");
            Debug.Log(arr);
            Debug.Log("----Potential-------");
            for (int i = 0; i <= arr.Count; i++)
            {
                potential_moves.Add(arr[i]);
            }
            in_progess_operation = false;
        }
    }


    public void move_piece(string from_position, string to_position)
    {
        StartCoroutine(web_move_piece(from_position, to_position));
    }

    IEnumerator web_move_piece(string from_position, string to_position)
    {
        in_progess_operation = true;
        WWWForm form = new WWWForm();
        form.AddField("game_id", game_id);
        form.AddField("from", from_position);
        form.AddField("to", to_position);

        UnityWebRequest www = UnityWebRequest.Post("http://chess-api-chess.herokuapp.com/api/v1/chess/two/move", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Exisssst and error");
        }
        else
        {
            var object_request = JSON.Parse(www.downloadHandler.text);
            Debug.Log("Piece moooooooved");
            in_progess_operation = false;
        }
    }
}
