using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class displayUserInfo : MonoBehaviour {

    //UI
    public Text playerName;
    public Image avatarImage;

	void Start () {

        if (!SteamManager.Initialized) {
            return;
        }

        //Username
        playerName.text = SteamFriends.GetPersonaName();


        //Avatar
        StartCoroutine(_FetchAvatar());
	}

    int avatarInt;
    uint width, height;
    Texture2D downloadedAvatar;
    Rect rect = new Rect(0, 0, 184, 184);
    Vector2 pivot = new Vector2(0.5f, 0.5f);

    IEnumerator _FetchAvatar()
    {
        avatarInt = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());

        while(avatarInt == -1) {
            yield return null;
        }

        if(avatarInt > 0) {
            SteamUtils.GetImageSize(avatarInt, out width, out height);
            if(width > 0 && height > 0) {
                byte[] avatarStream = new byte[4 * (int)width * (int)height];
                SteamUtils.GetImageRGBA(avatarInt, avatarStream, 4 * (int)width * (int)height);

                downloadedAvatar = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                downloadedAvatar.LoadRawTextureData(avatarStream);
                downloadedAvatar.Apply();

                avatarImage.sprite = Sprite.Create(downloadedAvatar, rect, pivot);
            }
        }
    }

}
