using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
 
public static class Scoreboard {
    public delegate void ScoreCallback(Score score);
    public delegate void ScoresCallback(Score[] scores);

    // Change this to the score endpoint of the leaderboard server
    public static string serverURL = "http://6342f9d3.ngrok.io/score";
    
    public static IEnumerator PostScore(ScoreCallback callback, Score score) {
        // Using form to send because UnityWebRequest fails handling JSON
        WWWForm form = new WWWForm();

        form.AddField("name", score.name);
        form.AddField("level", score.level);
        form.AddField("time", score.time.ToString());

        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.LogError(www.error);
        } else {
            string jsonData = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 0, www.downloadHandler.data.Length);
            Score result = JsonUtility.FromJson<Score>(jsonData);
            callback(result);
        }
    }

    public static IEnumerator GetScores(ScoresCallback callback, string level, uint offset = 0, uint size = 10) {
        string query = "?level=" + level + "&offset=" + offset + "&size=" + size;

        using (UnityWebRequest www = UnityWebRequest.Get(serverURL + query)) {
            yield return www.SendWebRequest();

            string jsonData = "";
            if (www.isNetworkError || www.isHttpError) {
                Debug.LogError(www.error);
            } else {
                jsonData = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 0, www.downloadHandler.data.Length);
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
        public string level;
        public string name;
        public float time;
        public uint rank;


        public Score(string level, string name, float time) {
            this.name = name;
            this.time = time;
            this.level = level;
        }
    }
}