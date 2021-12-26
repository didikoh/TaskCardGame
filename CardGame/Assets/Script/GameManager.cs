using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameManager : NetworkBehaviour
{
    public PlayerManager PlayerManager;

    public void OnClick()
    {
        if (isServer)
        {
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.CmdStart();
            DestroyMyself();
        }
        else
        {
            Debug.Log("you are not the server");
        }
    }

    [ClientRpc]
    public void DestroyMyself()
    {
        Destroy(this.gameObject);
    }
    /*GameObject start_but;
    int[] ran;
    public Transform[] card_position;
    public GameObject card_prefab;

    void Awake()
    {
        ran = new int[3];
        start_but = GameObject.Find("StartBut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        start_but.SetActive(false);
        ShowCard();
    }

    void ShowCard()
    {
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(1, 11);
            if (ran.Contains(rand))
            {
                i--;
            }
            else
            {
                ran[i] = rand;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            GameObject new_card = Instantiate(card_prefab, card_position[j].position, Quaternion.identity);
            new_card.GetComponentInChildren<TextMesh>().text = ran[j].ToString();
        }
    }*/
}
