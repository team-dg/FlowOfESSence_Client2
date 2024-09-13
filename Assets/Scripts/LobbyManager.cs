using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public GameObject[] Panel;
    public GameObject GameStartPanel;
    public Button GameStartBTN;

    public GameObject[] GameModePanel;

    public InputField[] inputFields;
    public GameObject ģ���߰�Panel;
    public InputField ģ���߰�Input;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickPanel(int num)
    {
        if(num==0)
        {
            GameStartPanel.SetActive(false);
            GameStartBTN.interactable = true;
        }
        else
        {
            GameStartPanel.SetActive(true);
            GameStartBTN.interactable = false;
        }
        for(int i=0; i<4; i++)
        {
            if(i==num)
            {
                Panel[i].SetActive(true);
            }
            else
            {
                Panel[i].SetActive(false);
            }
        }
        for(int i=0; i<3; i++)
        {
            inputFields[i].text = "";
        }
    }

    public void ClickGameMode(int num)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == num)
            {
                GameModePanel[i].SetActive(true);
            }
            else
            {
                GameModePanel[i].SetActive(false);
            }
        }
    }
    public void Openģ���߰�Panel(int num)
    {
        if(num==0)
        {
            ģ���߰�Panel.SetActive(true);
        }
        else
        {
            ģ���߰�Panel.SetActive(false);
            ģ���߰�Input.text = "";
        }
    }
}
