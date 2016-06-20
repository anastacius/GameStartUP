using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField]
    private Button loginButton;

    private Text loginButtonLabel;

    private void Awake()
    {
        loginButtonLabel = loginButton.GetComponentInChildren<Text>();
        loginButton.onClick.AddListener(OnClickLoginButton);
        FacebookService.Instance.OnLoginSuccessEvent += UpdateButtonLabel;
        FacebookService.Instance.OnLogoutEvent += UpdateButtonLabel;
    }

    private void OnDestroy()
    {
        loginButton.onClick.RemoveAllListeners();
        FacebookService.Instance.OnLoginSuccessEvent -= UpdateButtonLabel;
        FacebookService.Instance.OnLogoutEvent -= UpdateButtonLabel;
    }

    private void UpdateButtonLabel()
    {
        if (FacebookService.Instance.IsLoggedIn)
            loginButtonLabel.text = "Logout";
        else
        {
            loginButtonLabel.text = "Login";
        }

    }

    private void OnClickLoginButton()
    {
        if (FacebookService.Instance.IsLoggedIn)
            FacebookService.Instance.Logout();
        else
            FacebookService.Instance.Login();
    }
}
