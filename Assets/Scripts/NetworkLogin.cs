using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
class UserData
{
    public string username;
    public string password;
    public string email;
    public string nickname;
    //string jsonBody = "{\"username\":\"temp1\",\"password\":\"Qwe1234@\",\"email\":\"temp1@naver.com\",\"nickname\":\"temp1\"}";
}

[System.Serializable]
public class LoginResponse
{
    public string nickname;
    public string profileImageUrl;
    public AccessToken accessToken;
    public RefreshToken refreshToken;
}

[System.Serializable]
public class AccessToken
{
    public string token;
    public string expiredAt;
}

[System.Serializable]
public class RefreshToken
{
    public string token;
    public string expiredAt;
}

public class NetworkLogin : MonoBehaviour
{
    [Header("�α���")]
    public InputField id;
    public InputField password;
    public GameObject loginBtn1;
    public GameObject loginBtn2;


    public GameObject SignUpBtn1;
    public GameObject SignUpBtn2;

    [Header("ȸ������")]
    public InputField signUpId;
    public InputField signUppassword;
    public InputField signUppassword2;
    public InputField signUpNickName;
    public InputField signUpEmail;
    public InputField signUppEmailNumber;

    [Header("Panel")]
    public GameObject idPanel;
    public GameObject emailPanel;
    public GameObject loginPanel;
    public GameObject �̿밡Panel;
    public GameObject lobbyPanel;
    public GameObject signUpPanel;

    [Header("User")]
    public Text NickName;
    public Image profileImage;
    private string userNickName;

    public Image �̿밡Image;

    UserData newplayer;
    
    void Start()
    {
        //string jsonData = JsonUtility.ToJson(newplayer);//json���� ��ȯ
        //UserData player=JsonUtility.FromJson<UserData>(jsonData);//Json������ UserData�� ��ȯ
        Screen.SetResolution(1366, 768, false);
    }

    //īī�� �α���
    public void StartKakaoLogin()
    {
        string kakaoAuthUrl = "https://kauth.kakao.com/oauth/authorize?client_id=24bc36a60f2edeaf5e54a9b7053f1861&redirect_uri=http%3A%2F%2Flocalhost%3A3000%2Flogin%2Foauth2%2Fcode%2Fkakao&response_type=code";
        Application.OpenURL(kakaoAuthUrl);
        StartCoroutine(GetAuthorizationCodeFromServer());
    }

    public IEnumerator GetAuthorizationCodeFromServer()//�� ���ü������� �ڵ� �޾ƿ�
    {
        string url = "http://localhost:3000/getAuthorizationCode";
        string authorizationCode = null;

        while (authorizationCode == null)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string temp = www.downloadHandler.text;
                if (!string.IsNullOrEmpty(temp))
                {
                    authorizationCode = temp;
                    Debug.Log("Authorization Code received from server: " + temp);

                    // Authorization Code�� Access Token ��û
                    UnityWebRequestPostKakaoLogin(authorizationCode);
                }
            }
            else
            {
                // HTTP 400 ������ �����ϰ� ��õ�
                if (www.responseCode == 400)
                {
                    Debug.Log("Authorization Code�� ���� �غ���� �ʾҽ��ϴ�. ��õ� ��...");
                }
                else
                {
                    Debug.LogError("Error: " + www.error);
                }
            }

            // 1�� ��� �� �ٽ� ��û
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void UnityWebRequestPostKakaoLogin(string authorizationCode)
    {
        string url = "http://60.253.18.199:5424/api/v1/auth/login/oauth2"; // OAuth �α��� API URL
        string jsonBody = $"{{\"socialType\":\"KAKAO\",\"code\":\"{authorizationCode}\"}}";
        StartCoroutine(SendPostRequest(url, jsonBody, "POST", authorizationCode));

        /*UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("�α��� ����: " + www.downloadHandler.text);

            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);

            // Access Token�� Refresh Token�� ����
            PlayerPrefs.SetString("accessToken", loginResponse.accessToken.token);
            PlayerPrefs.SetString("refreshToken", loginResponse.refreshToken.token);
            PlayerPrefs.Save(); // PlayerPrefs ����

            Debug.Log("Access Token �����: " + loginResponse.accessToken.token);
            Debug.Log("Access Token ������: " + loginResponse.accessToken.expiredAt);
            Debug.Log("Refresh Token �����: " + loginResponse.refreshToken.token);

            loginPanel.SetActive(false);
            userNickName = loginResponse.nickname;
            �̿밡Panel.SetActive(true);
            lobbyPanel.SetActive(true);

            Debug.Log("Full Response: " + www.downloadHandler.text);
            Debug.Log($"profileImageUrl: {loginResponse.profileImageUrl}");
            //StartCoroutine(LoadProfileImage(loginResponse.profileImageUrl));
            
            
            StartCoroutine(�̿밡ä��());
        }
        else
        {
            Debug.Log($"Error: {www.error}, Response Code: {www.responseCode}, Result: {www.result}");
            Debug.Log("���� ���� ����: " + www.downloadHandler.text);
        }*/
    }

    public IEnumerator SendPostRequest(string url,string jsonBody,string getorpost,string authorizationCode)
    {
        UnityWebRequest www = new UnityWebRequest(url, getorpost);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("�α��� ����: " + www.downloadHandler.text);

            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);

            // Access Token�� Refresh Token�� ����
            PlayerPrefs.SetString("accessToken", loginResponse.accessToken.token);
            PlayerPrefs.SetString("refreshToken", loginResponse.refreshToken.token);
            PlayerPrefs.Save(); // PlayerPrefs ����

            Debug.Log("Access Token �����: " + loginResponse.accessToken.token);
            Debug.Log("Access Token ������: " + loginResponse.accessToken.expiredAt);
            Debug.Log("Refresh Token �����: " + loginResponse.refreshToken.token);

            loginPanel.SetActive(false);
            userNickName = loginResponse.nickname;
            �̿밡Panel.SetActive(true);
            lobbyPanel.SetActive(true);

            Debug.Log("Full Response: " + www.downloadHandler.text);
            Debug.Log($"profileImageUrl: {loginResponse.profileImageUrl}");
            //StartCoroutine(LoadProfileImage(loginResponse.profileImageUrl));


            StartCoroutine(�̿밡ä��());
        }
    }

    //�Ϲ� �α���, ȸ������
    public void Login()
    {
        UnityWebRequestPostLogin();
    }

    public void SignUp()
    {
        signUpPanel.SetActive(true);
    }

    public void UnityWebRequestPostLogin()
    {
        string url = "http://60.253.18.199:5424/api/v1/auth/login"; // �α��� API URL
        string jsonBody = $"{{\"username\":\"{id.text}\",\"password\":\"{password.text}\"}}";

        StartCoroutine(SendPostRequest(url, jsonBody, "POST", ""));
        /*
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("�α��� ����: " + www.downloadHandler.text);

            // ���� ������ �Ľ��Ͽ� ��ū ����
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);

            // Access Token�� Refresh Token�� ����
            PlayerPrefs.SetString("accessToken", loginResponse.accessToken.token);
            PlayerPrefs.SetString("refreshToken", loginResponse.refreshToken.token);
            PlayerPrefs.Save();  // PlayerPrefs ����

            Debug.Log("Access Token �����: " + loginResponse.accessToken.token);
            Debug.Log("expired  �����: " + loginResponse.accessToken.expiredAt);
            Debug.Log("Refresh Token �����: " + loginResponse.refreshToken.token);

            loginPanel.SetActive(false);
            userNickName = loginResponse.nickname;
            �̿밡Panel.SetActive(true);
            lobbyPanel.SetActive(true);
            StartCoroutine(�̿밡ä��());

        }
        else
        {
            Debug.Log($"Error: {www.error}, Response Code: {www.responseCode}, Result: {www.result}");
            Debug.Log("���� ���� ����: " + www.downloadHandler.text);
        }*/
    }

    public void UnityWebRequestPostSignup()
    {
        string url = "http://60.253.18.199:5424/api/v1/auth/signup";
        string jsonBody = $"{{\"username\":\"{newplayer.username}\",\"password\":\"{newplayer.password}\",\"email\":\"{newplayer.email}\",\"nickname\":\"{newplayer.nickname}\"}}";

        StartCoroutine(SendPostRequest(url, jsonBody, "POST", ""));

        /*UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("���� ����: " + www.downloadHandler.text);

            // ���� ������ �Ľ��Ͽ� ��ū ����
            LoginResponse tokenResponse = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);

            // ��ū�� PlayerPrefs�� ����
            PlayerPrefs.SetString("accessToken", tokenResponse.accessToken.token);
            PlayerPrefs.Save();  // PlayerPrefs ����
            loginPanel.SetActive(false);
            userNickName = tokenResponse.nickname;
            �̿밡Panel.SetActive(true);
            lobbyPanel.SetActive(true);
            StartCoroutine(�̿밡ä��());
            Debug.Log("��ū �����: " + tokenResponse.accessToken.token);
        }
        else
        {
            Debug.Log($"Error: {www.error}, Response Code: {www.responseCode}, Result: {www.result}");
            Debug.Log("���� ���� ����: " + www.downloadHandler.text);
        }*/
    }


    public void �α��μ���()
    {
        StartCoroutine(GetAuthorizationCodeFromServer());
    }

    public void EnableLoginButton()
    {
        if (id.text != "" && password.text != "")
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

    public void EnableSingUpButton()//ȸ�����Կ��� ������ ��ư�� �����ϴ� �Լ���.
    {
        if (idPanel.activeSelf)
        {
            if (signUpId.text != "" && signUppassword.text != "" && signUppassword.text != "" && signUppassword.text == signUppassword2.text)
            {
                SignUpBtn1.SetActive(false);
                SignUpBtn2.SetActive(true);
            }
            else
            {
                SignUpBtn1.SetActive(true);
                SignUpBtn2.SetActive(false);
            }
        }
        else if (emailPanel.activeSelf)
        {
            if (signUpNickName.text != "" && signUpEmail.text != "")
            {
                SignUpBtn1.SetActive(false);
                SignUpBtn2.SetActive(true);
            }
            else
            {
                SignUpBtn1.SetActive(true);
                SignUpBtn2.SetActive(false);
            }
        }
    }

    public IEnumerator �̿밡ä��()
    {
        // ���İ� 0���� 255���� ����
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime/3)//�ӵ��� ������.
        {
            Color newColor = �̿밡Image.color;
            newColor.a = alpha; // ���� �� ����
            �̿밡Image.color = newColor;
            yield return null; // ���� �����ӱ��� ���
        }

        // ���İ� 255���� 0���� ����
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime/3)
        {
            Color newColor = �̿밡Image.color;
            newColor.a = alpha; // ���� �� ����
            �̿밡Image.color = newColor;
            yield return null; // ���� �����ӱ��� ���
        }

        NickName.text = userNickName;
        �̿밡Panel.SetActive(false);
    }

    private IEnumerator LoadProfileImage(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            profileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            Debug.Log("������ �̹��� �ε� ����");
        }
        else
        {
            Debug.LogError("������ �̹��� �ε� ����: " + request.error);
        }
    }
    public void OpenLoginPage(string url)
    {
        Application.OpenURL(url);
    }



    /* public void EmailPanel()
    {
        if(idPanel.activeSelf)//���� id�г���Ȱ��ȭ �Ǿ��ִٸ�
        {
            idPanel.SetActive(false);
            emailPanel.SetActive(true);
            newplayer = new UserData();
            newplayer.username = signUpId.text;
            newplayer.password = signUppassword.text;
        }
        else//�׷��� �ʴٸ� �̸����г��� Ȱ��ȭ �Ǿ��ֱ⿡ ȸ��������.
        {
            newplayer.email = signUpEmail.text;
            newplayer.nickname = signUpNickName.text;
            //string jsonBody = JsonUtility.ToJson(newplayer);

            StartCoroutine(UnityWebRequestPostSignup());
        }
        
    }

    IEnumerator UnityWebRequestGet()
    {
        string url = "";
        UnityWebRequest www = UnityWebRequest.Get(url);
        //��û����
        //string jobId = "ad";
        //string JobGrowId = "�����";
        //string url = $"https://<jobId><jobGrowId>";

        yield return www.SendWebRequest();
        if(www.error==null)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }
    IEnumerator UnityWebRequestPOSTTEST()
    {
        string url = "POST ����� ����� ���� �ּҸ� �Է�";
        WWWForm form = new WWWForm();
        string id = "���̵�";
        string pw = "��й�ȣ";
        form.AddField("Username", id);
        form.AddField("Password", pw);
        UnityWebRequest www = UnityWebRequest.Post(url, form);  // ���� �ּҿ� ������ �Է�

        yield return www.SendWebRequest();  // ���� ���

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);    // ������ ���
        }
        else
        {
            Debug.Log("error");
        }
    }
    */
}
