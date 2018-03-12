using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
 
public static class Scoreboard {
    public delegate void Callback(Score[] scores);
    public static string serverURL = "http://127.0.0.1:8090/score";

    public static IEnumerator PostScore(Score score) {
        string jsonData = JsonUtility.ToJson(score);

        UnityWebRequest www = UnityWebRequest.Post(serverURL, jsonData);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
        } else {
            Debug.Log("Score submitted");
        }
    }

    public static IEnumerator GetScores(Callback callback, string level = null, int count = -1) {
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

        using (UnityWebRequest www = UnityWebRequest.Get(serverURL + query)) {
            yield return www.SendWebRequest();

            string jsonData = "";
            if (string.IsNullOrEmpty(www.error)) {
                jsonData = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);
                Score[] result = JsonHelper.getJsonArray<Score>(jsonData);
                callback(result);
            }
        }
    }

    private static class JsonHelper {
        public static T[] getJsonArray<T>(string json) {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }

    [System.Serializable]
    public class Score {
        public string name;
        public string level;
        public float time;

        public Score(string name, float time, string level) {
            this.name = name;
            this.time = time;
            this.level = level;
        }
    }
}