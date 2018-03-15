// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class TextManager : MonoBehaviour {
// public struct Chat
//     {
//         public string[] messages;
//         public Sprite avatar;
//     };

//     public string[] chats;
// 	public Sprite[] avatars;
// 	public int[] chatPictures;
//     public GameObject textBox;
//     public bool repeated;

//     private bool opened = false;
//     private bool isOpen = false;
//     private int chat = 0;
//     private int chatMessage = 0;

//     public void createChat(Chat[] chats)
//     {
//         GameObject.Find("Menu Canvas").SetActive(false);
//         opened = true;
//         GameObject toInstantiate = textBox;
//         GameObject chatPane = Instantiate(toInstantiate, new Vector2(0, 0), Quaternion.identity) as GameObject;
//         GameObject o = GameObject.Find("ChatImage");
//         o.GetComponent<Image>().sprite = chats[0].avatar;
//         o = GameObject.Find("TextBox");
//         o.GetComponent<Button>().onClick.AddListener(incrementChat);
//         StartCoroutine(updateAnimatedText(chats[0].messages[chatMessage]));
//         isOpen = true;

//     }


//     void OnTriggerEnter(Collider other)
//     {
//         if (other.tag == "Player" && (repeated || !opened))
//         {
//             createChat(chats);
//         }
//     }

//     void incrementChat()
//     {
//         chatMessage++;
//         if (chatMessage == chats[chat].messages.Length)
//         {
//             chat++;
//             chatMessage = 0;
//             GameObject pic = GameObject.Find("ChatImage");
//             pic.GetComponent<Image>().sprite = chats[chat].avatar;
//         }
//         if(chat == chats.Length) { 
//             Destroy(GameObject.FindGameObjectWithTag("TextCanvas"));
//             isOpen = false;
//             chat = 0;
//             GameObject.Find("Menu Canvas").SetActive(true);
//         }
//         else
//         {
//             StopAllCoroutines();
//             StartCoroutine(updateAnimatedText(chats[chat].messages[chatMessage]));
//         }
//     }

//     bool updateChat(string message)
//     {
//         GameObject o = GameObject.Find("TextBoxText");
//         if (o != null)
//         {
//             o.GetComponent<Text>().text = message;
//             return true;
//         }
//         return false;
//     }

//     IEnumerator updateAnimatedText(string strComplete)
//     {
//         int i = 0;
//         string str = "";
//         while (i < strComplete.Length)
//         {
//             str += strComplete[i++];
//             if (updateChat(str))
//             {
//                 yield return new WaitForSeconds(0.04F);
//             }
//             else
//             {
//                 yield break;
//             }
//         }
//     }
// }