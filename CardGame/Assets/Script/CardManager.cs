using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class CardManager : NetworkBehaviour
{
    public GameObject card_back;
    public GameObject card_front;
    public TextMesh card_text;
    public int selected_int;
    public PlayerManager PlayerManager;
    //public Text selected_text;
    // Start is called before the first frame update

    public void ShowText(int num)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        card_text.text = num.ToString();
        selected_int = num;
        //selected_text = GameObject.Find("SelectedNum").GetComponent<Text>()
    }

    public void FliptoBack()
    {
        card_front.SetActive(false);
        card_back.SetActive(true);
    }

    public void OnMouseDown()
    {
        Debug.Log("clicked");
        if (!isServer)
        { 
            card_front.SetActive(true);
            card_back.SetActive(false);
            PlayerManager.ChoseThis(selected_int);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
