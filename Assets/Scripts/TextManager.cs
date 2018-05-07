using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TextManager : NetworkBehaviour {

    public string[] chats;
    public Sprite[] avatars;
    public int[] chatPictures;
    public GameObject textBox;
    public bool repeated;
    public bool isTriggerable;

    private bool opened = false;
    private bool isOpen = false;
    private bool printing = false;
    private int chatMessage = 0;
    private GameObject uiCanvas;
    private GameObject textCanvas;
    private JoystickCharacter movement;

    public void Start() {
        uiCanvas = GameObject.Find("UI Canvas");
    }

    public void CreateChat(string[] chats, Sprite[] avatars, int[] chatPics)
    {
        uiCanvas.SetActive(false);
        opened = true;
        textCanvas = Instantiate(textBox, new Vector2(0, 0), Quaternion.identity);
        GameObject o = textCanvas.transform.Find("ChatImage").gameObject;
        o.GetComponent<Image>().sprite = avatars[chatPics[0]];
        o = textCanvas.transform.Find("TextBox").gameObject;
        o.GetComponent<Button>().onClick.AddListener(incrementChat);
        StartCoroutine(updateAnimatedText(chats[chatMessage]));
        isOpen = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (repeated || !opened) && !isOpen && isTriggerable)
        {
                movement = other.GetComponent<JoystickCharacter>();
                movement.joystick.inputVector = Vector3.zero;
                movement.joystick.joystickImage.rectTransform.anchoredPosition = Vector3.zero;
                movement.stickInput = Vector3.zero;
                movement.canMove = false;
            CreateChat(chats, avatars, chatPictures);
        }
    }

    private void incrementChat()
    {
        if (printing)
        {
            StopAllCoroutines();
            updateChat(chats[chatMessage]);
            printing = false;
        }
        else
        {
            chatMessage++;
            if (chatMessage == chats.Length)
            {
                Destroy(textCanvas);
                isOpen = false;
                chatMessage = 0;
                movement.canMove = true;
                movement = null;
                textCanvas = null;
                uiCanvas.SetActive(true);
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(updateAnimatedText(chats[chatMessage]));
                updatePicture(avatars[chatPictures[chatMessage]]);
            }
        }
    }
    private bool updatePicture(Sprite pic)
    {
       GameObject o = textCanvas.transform.Find("ChatImage").gameObject;
       if (o != null)
       {
           o.GetComponent<Image>().sprite = pic;
           return true;
       }
       return false;
    }

    private bool updateChat(string message)
    {
        GameObject o = textCanvas.transform.Find("TextBox/TextBoxText").gameObject;
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
            printing = true;
            float time = 0.04f;
            if (strComplete[i] == '.') time *= 2;
            str += strComplete[i];
            if (updateChat(str))
            {
                yield return new WaitForSeconds(time);
            }
            else
            {
                printing = false;
                yield break;
            }
            i++;
        }
        printing = false;
    }
}
