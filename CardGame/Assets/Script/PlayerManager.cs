using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    public GameObject card_prefab;
    List<int> ran = new List<int>();
    List<Transform> card_position = new List<Transform>();
    public override void OnStartClient()
    {
        base.OnStartClient();
        card_position.Add(GameObject.Find("CardPos1").transform);
        card_position.Add(GameObject.Find("CardPos2").transform);
        card_position.Add(GameObject.Find("CardPos3").transform);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }


    [Command]
    public void CmdStart()
    {
        Debug.Log("Start");
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(1, 11);
            if (ran.Contains(rand))
            {
                i--;
            }
            else
            {
                ran.Add(rand);
            }
        }
        for (int j = 0; j < 3; j++)
        {
            GameObject new_card = Instantiate(card_prefab, card_position[j].position, Quaternion.identity);
            NetworkServer.Spawn(new_card, connectionToClient);
            CardInfo(new_card, ran[j]);
            
        }

    }

    [ClientRpc]
    public void CardInfo(GameObject card,int number)
    {
        card.GetComponent<CardManager>().ShowText(number);
        if (!hasAuthority)
        {
            card.GetComponent<CardManager>().FliptoBack();
        }
    }

    [Command]
    public void ChoseThis(int num)
    {
        int n = num;
            ShowResult(n);
    }
        
    [ClientRpc]
    public void ShowResult(int num)
    {
            GameObject.Find("SelectedNum").GetComponent<Text>().text = "The client chose the card No" + num.ToString();
    }
}
