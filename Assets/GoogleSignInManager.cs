using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoogleSignInManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text txtBox;

    public void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services

            Debug.Log("work");

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();

            txtBox.text = name + "\n Success";
        }
        else
        {

            Debug.Log("NO work");
            txtBox.text = "nope";
            txtBox.text = status.ToString();
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
