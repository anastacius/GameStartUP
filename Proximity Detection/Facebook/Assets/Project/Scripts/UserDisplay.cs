using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserDisplay : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;

    [SerializeField]
    private Text userInfo;


    private void Awake()
    {
        FacebookService.Instance.OnLoadLocalUserInfoSuccessEvent += OnLoadLocalUserInfoSuccessEvent;
        FacebookService.Instance.OnLoginSuccessEvent += OnLoginSuccessEvent;
    }

    private void OnDestroy()
    {
        FacebookService.Instance.OnLoadLocalUserInfoSuccessEvent -= OnLoadLocalUserInfoSuccessEvent;
        FacebookService.Instance.OnLoginSuccessEvent -= OnLoginSuccessEvent;
    }

    private void OnLoginSuccessEvent()
    {
        FacebookService.Instance.LoadLocalUserInfo();
    }

    private void OnLoadLocalUserInfoSuccessEvent(FacebookUser facebookUser)
    {
        userInfo.text = string.Format("{0}\n", facebookUser.Name);
        userInfo.text += string.Format("{0}\n", facebookUser.Location);
        userInfo.text += string.Format("{0}\n", facebookUser.Gender);
        userInfo.text += string.Format("{0}\n", facebookUser.BirthDay.Year);


        StartCoroutine(LoadProfilePictureEnumerator(facebookUser));
    }

    private IEnumerator LoadProfilePictureEnumerator(FacebookUser facebookUser)
    {
        WWW pictureLoading = new WWW(facebookUser.PictureURL);

        yield return pictureLoading;

        if (!string.IsNullOrEmpty(pictureLoading.error))
        {
            yield break;
        }
        rawImage.texture = pictureLoading.texture;
    }
}
