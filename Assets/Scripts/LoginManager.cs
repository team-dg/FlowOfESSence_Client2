using UnityEngine;
using UnityEngine.UI;
public class LoginManager : MonoBehaviour
{
    public InputField id;
    public InputField password;

    public GameObject loginBtn1;
    public GameObject loginBtn2;

    public GameObject loginPanel;
    public GameObject lobbyPanel;

    void Start()
    {
        Screen.SetResolution(1366, 768, false);
    }

    void Update()
    {
        
    }

    public void EnableLoginButton()
    {
        if(id.text!=""&&password.text!="")
        {
            loginBtn1.SetActive(false);
            loginBtn2.SetActive(true);
        }
        else
        {
            loginBtn1.SetActive(true);
            loginBtn2.SetActive(false);
        }
    }
    public void Login()
    {
        //만약 로그인에 성공한다면
        loginPanel.SetActive(false);
        lobbyPanel.SetActive(true);

    }
    public void OpenLoginPage(string url)
    {
        Application.OpenURL(url);
    }
}
