using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.kleberswf.lib.core;
using Facebook.MiniJSON;
using Facebook.Unity;

public class FacebookUser
{
    public string ID;
    public string Name;
    public string Gender;
    public string PictureURL;
    public string Location;
    public bool IsLocalUser;
    public DateTime BirthDay;
}

public class FacebookService : Singleton<FacebookService>
{
    public delegate void StringDelegate(string message);
    public delegate void FacebookUserDelegate(FacebookUser facebookUser);

    public delegate void VoidDelegate();

    public event StringDelegate OnLoginFailEvent;
    public event StringDelegate OnLoadLocalUserInfoFailEvent;
    public event VoidDelegate OnLoginSuccessEvent;
    public event VoidDelegate OnLogoutEvent;
    public event FacebookUserDelegate OnLoadLocalUserInfoSuccessEvent;


    private const string APP_IP = "958298417616617";
    //Read Permissions
    private static List<string> READ_PERMISSIONS = new List<string>
    {
        "public_profile",
        "user_friends",
        "user_games_activity",
        "user_location",
        "user_birthday"
    };

    private FacebookUser localUser;

    public bool IsLoggedIn
    {
        get { return FB.IsLoggedIn; }
    }


    protected override void Awake()
    {
        base.Awake();
        FB.Init(APP_IP);
    }


    public void Login()
    {
        FB.LogInWithReadPermissions(READ_PERMISSIONS, LoginCallback);
    }

    public void Logout()
    {
        FB.LogOut();
        DispatchOnLogoutEvent();
    }

    public void LoadLocalUserInfo()
    {
        FB.API("me?fields=id,name,gender,location,picture.height(128).width(128),birthday", HttpMethod.GET,
            LoadLocalUserInfoCallback);
    }

    private void LoadLocalUserInfoCallback(IGraphResult result)
    {
        if (!string.IsNullOrEmpty(result.Error))
        {
            DispatchOnLoadLocalUserInfoFailEvent(result.Error);
        }
        else
        {
            localUser = new FacebookUser()
            {
                IsLocalUser = true
            };


            IDictionary<string, object> resultDictionary =
                  (IDictionary<string, object>)Json.Deserialize(result.RawResult);

            resultDictionary.TryGetValue("id", out localUser.ID);
            resultDictionary.TryGetValue("name", out localUser.Name);
            resultDictionary.TryGetValue("location", out localUser.Location);
            resultDictionary.TryGetValue("gender", out localUser.Gender);
            

            string birthDay;
            resultDictionary.TryGetValue("birthday", out birthDay);

            if(!string.IsNullOrEmpty(birthDay))
                localUser.BirthDay = DateTime.Parse(birthDay);

            IDictionary<string, object> pictureResult;
            if (resultDictionary.TryGetValue("picture", out pictureResult))
            {
                IDictionary<string, object> dataResult;
                if (pictureResult.TryGetValue("data", out dataResult))
                    dataResult.TryGetValue("url", out localUser.PictureURL);
            }

            IDictionary<string, object> locationResult;
            if (resultDictionary.TryGetValue("location", out locationResult))
                locationResult.TryGetValue("name", out localUser.Location);



            DispatchOnLoadLocalUserInfoSuccessEvent();
        }
    }

    private void DispatchOnLoadLocalUserInfoSuccessEvent()
    {
        if (OnLoadLocalUserInfoSuccessEvent != null)
            OnLoadLocalUserInfoSuccessEvent(localUser);
    }

    private void DispatchOnLoadLocalUserInfoFailEvent(string error)
    {
        if (OnLoadLocalUserInfoFailEvent != null)
            OnLoadLocalUserInfoFailEvent(error);

    }


    private void LoginCallback(ILoginResult result)
    {
        if (!string.IsNullOrEmpty(result.Error))
        {
            DispatchOnLoginFailEvent(result.Error);
        }
        else
        {
            DispatchOnLoginSuccessEvent();
        }

    }
    private void DispatchOnLogoutEvent()
    {
        if (OnLogoutEvent != null)
            OnLogoutEvent();
    }

    private void DispatchOnLoginSuccessEvent()
    {
        if (OnLoginSuccessEvent != null)
            OnLoginSuccessEvent();
    }

    private void DispatchOnLoginFailEvent(string error)
    {
        if (OnLoginFailEvent != null)
            OnLoginFailEvent(error);
    }
}
