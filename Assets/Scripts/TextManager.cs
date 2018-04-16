using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public string[] chats;
    public Sprite[] avatars;
    public int[] chatPictures;
    public GameObject textBox;
    public bool repeated;
    public bool isTriggerable;

    private bool opened = false;
    private bool isOpen = false;
    private int chatMessage = 0;
    private GameObject uiCanvas;

    public void CreateChat(string[] chats, Sprite[] avatars, int[] chatPics)
    {
        uiCanvas = GameObject.Find("UI Canvas");
        uiCanvas.SetActive(false);
        opened = true;
        GameObject toInstantiate = textBox;
        GameObject chatPane = Instantiate(toInstantiate, new Vector2(0, 0), Quaternion.identity) as GameObject;
        GameObject o = GameObject.Find("ChatImage");
        o.GetComponent<Image>().sprite = avatars[chatPics[0]];
        o = GameObject.Find("TextBox");
        o.GetComponent<Button>().onClick.AddListener(incrementChat);
        StartCoroutine(updateAnimatedText(chats[chatMessage]));
        isOpen = true;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.tag == "Player" && (repeated || !opened) && !isOpen && isTriggerable)
        {
            CreateChat(chats, avatars, chatPictures);
        }
    }

    private void incrementChat()
    {
        chatMessage++;
        if (chatMessage == chats.Length)
        {
           Destroy(GameObject.FindGameObjectWithTag("TextCanvas"));
           isOpen = false;
           chatMessage = 0;
           uiCanvas.SetActive(true);
        }
        else
        {
           StopAllCoroutines();
           StartCoroutine(updateAnimatedText(chats[chatMessage]));
           updatePicture(avatars[chatPictures[chatMessage]]);
        }
    }
    private bool updatePicture(Sprite pic)
    {
       GameObject o = GameObject.Find("ChatImage");
       if (o != null)
       {
           o.GetComponent<Image>().sprite = pic;
           return true;
       }
       return false;
    }

    private bool updateChat(string message)
    {
        GameObject o = GameObject.Find("TextBoxText");
        if (o != null)
        {
            o.GetComponent<Text>().text = message;
            return true;
        }
        return false;
    }

    private IEnumerator updateAnimatedText(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            float time = 0.04f;
            if (strComplete[i] == '.') time *= 2;
            str += strComplete[i];
            if (updateChat(str))
            {
                yield return new WaitForSeconds(time);
            }
            else
            {
                yield break;
            }
            i++;
        }
    }
}
