using UnityEngine;
using UnityEngine.UI;
public class LoginManager : MonoBehaviour
{
    public InputField id;
    public InputField password;

    public GameObject loginBtn1;
    public GameObject loginBtn2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(1366, 768, false);
    }

    // Update is called once per frame
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
}
