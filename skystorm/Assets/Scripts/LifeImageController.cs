using UnityEngine;
using System.Collections;

public class LifeImageController : MonoBehaviour {


    private GUITexture lifeImg1;

    void Start () {
        lifeImg1 = GetComponent<GUITexture>();
	}
	
	void Update () {
        lifeImg1.pixelInset = new Rect(0f,0f,16f,16f);
	}
}
