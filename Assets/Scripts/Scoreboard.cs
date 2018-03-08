using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
 
public class Scoreboard : MonoBehaviour
{
    public string serverURL = "http://127.0.0.1:8090/score";
 
    void Start()
    {
        StartCoroutine(GetScores());
    }
 
    // REMEMBER TO CALL THIS AS COROUTINE
    IEnumerator PostScore(string name, string level, float time) {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("name=" + name + "&level=" + level + "&time=" + time));

        UnityWebRequest www = UnityWebRequest.Post(serverURL, formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
        } else {
            Debug.Log("Score submitted");
        }
    }
 
    // REMEMBER TO CALL IN COROUTINE
    IEnumerator GetScores(string level = null, int count = -1) {
        string query = "";
        if (level != null || count > 0) {
            query = "?";
            if (level != null) {
                query = query + "level=" + level;
            }
            if (level != null || count > 0) {
                query = query + "&";
            }
            if (count > 0) {
                query = query + "count=" + count;
            }
        }
        UnityWebRequest www = UnityWebRequest.Get(serverURL + query);
        yield return www.SendWebRequest();

        string jsonData = "";
        if (string.IsNullOrEmpty(www.error)) {
            jsonData = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);
            List<ScoreboardResult> result = JsonUtility.FromJson<List<ScoreboardResult>>(jsonData);
        }
    }

    [System.Serializable]
    public class ScoreboardResult {
        public string name;
        public float score;
    }
}