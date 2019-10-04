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
        UnityWebRequest www = UnityWebRequest.Get("http://chess-api-chess.herokuapp.com/api/v1/chess/two");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var object_request = JSON.Parse(www.downloadHandler.text);
            game_id = object_request["game_id"].Value;
            Debug.Log("---------------asfas");
            Debug.Log(game_id);
            Debug.Log("---------------asfas");
        }
    }

    public void get_possible_moves(string position)
    {
        Debug.Log(ServiceLogicEngine.Instance.game_id);
        position_for_get_moves = position;
        StartCoroutine(web_get_possible_moves());
    }

    IEnumerator web_get_possible_moves()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        string data_str = string.Format("game_id={0}&position={1}", game_id, position_for_get_moves);
        formData.Add(new MultipartFormDataSection(data_str));

        UnityWebRequest www = UnityWebRequest.Post("http://chess-api-chess.herokuapp.com/api/v1/chess/two/moves", formData);
        yield return www.SendWebRequest();

        Debug.Log(data_str);
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Exisssst and error");
        }
        else
        {
            var object_request = JSON.Parse(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
        }

    }
}
