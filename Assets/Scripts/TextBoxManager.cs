using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	/**public GameObject textBox;

	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public bool activeAtStart;

	public bool stopPlayerMovement;

	public Character character;

	private bool isTyping = false;
	private bool cancelTyping = false;

	public float typeSpeed;

	void Start () {

		character = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Character>();

		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;	
		}

		if (activeAtStart) {
			EnableTextBox();
		} 
		else {
			DisableTextBox();
		}  
	}

	void Update () {

		if (!activeAtStart) {
			return;
		} 

		if (Input.GetKeyDown(KeyCode.Return)) {

			if (!isTyping) {
				currentLine += 1;
				if (currentLine > endAtLine) {
					DisableTextBox();
				} 
				else {
					StartCoroutine(TextScroll(textLines[currentLine]));
				}
			} 
			else if (isTyping && !cancelTyping) {
				cancelTyping = true;
			}
		}
	}

	public void EnableTextBox() {
		textBox.SetActive (true);
		activeAtStart = true;
		StartCoroutine(TextScroll(textLines[currentLine]));
		if (stopPlayerMovement) {
			character.canMove = false;
		} 
	}

	public void DisableTextBox() {
		textBox.SetActive(false);
		activeAtStart = false;
		character.canMove = true;
	}

	public void ReloadScript(TextAsset theText) {
		if (theText != null) {
			textLines = new string[1];
			textLines = (theText.text.Split ('\n'));
		}
	}

	private IEnumerator TextScroll (string lineOfText) {
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) {
			theText.text += lineOfText [letter];
			letter += 1;
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}**/

    //Bens code from existing textbox code elsewhere o___o

    public struct Chat
    {
        public string[] messages;
        public Sprite avatar;
    };

    public Chat[] chats;

    public GameObject textBox;

    public bool repeated;

    private bool opened = false;
    private bool isOpen = false;
    private int chat = 0;
    private int chatMessage = 0;

    public void createChat(Chat[] chats)
    {
        GameObject.Find("Menu Canvas").SetActive(false);
        opened = true;
        GameObject toInstantiate = textBox;
        GameObject chatPane = Instantiate(toInstantiate, new Vector2(0, 0), Quaternion.identity) as GameObject;
        GameObject o = GameObject.Find("ChatImage");
        o.GetComponent<Image>().sprite = chats[0].avatar;
        o = GameObject.Find("TextBox");
        o.GetComponent<Button>().onClick.AddListener(incrementChat);
        StartCoroutine(updateAnimatedText(chats[0].messages[chatMessage]));
        isOpen = true;

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && (repeated || !opened))
        {
            createChat(chats);
        }
    }

    void incrementChat()
    {
        chatMessage++;
        if (chatMessage == chats[chat].messages.Length)
        {
            chat++;
            chatMessage = 0;
            GameObject pic = GameObject.Find("ChatImage");
            pic.GetComponent<Image>().sprite = chats[chat].avatar;
        }
        if(chat == chats.Length) { 
            Destroy(GameObject.FindGameObjectWithTag("TextCanvas"));
            isOpen = false;
            chat = 0;
            GameObject.Find("Menu Canvas").SetActive(true);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(updateAnimatedText(chats[chat].messages[chatMessage]));
        }
    }

    bool updateChat(string message)
    {
        GameObject o = GameObject.Find("TextBoxText");
        if (o != null)
        {
            o.GetComponent<Text>().text = message;
            return true;
        }
        return false;
    }

    IEnumerator updateAnimatedText(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            if (updateChat(str))
            {
                yield return new WaitForSeconds(0.04F);
            }
            else
            {
                yield break;
            }
        }
    }
}