using UnityEngine;
using System.Collections;

public class StrippedScroller : MonoBehaviour {

    public float scrollSpeed;
    public float tileSizeY;

    private Renderer backgroundRenderer;

    private Vector2 savedOffset;
    private Vector3 startPosition;

    void Start()
    {
        backgroundRenderer = GetComponent<Renderer>();

        startPosition = transform.position;
        savedOffset = backgroundRenderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.timeSinceLevelLoad * scrollSpeed, tileSizeY * 4);
        x = x / tileSizeY;
        x = Mathf.Floor(x);
        x = x / 4;
        Vector2 offset = new Vector2(x, savedOffset.y);
        backgroundRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        float newPosition = Mathf.Repeat(Time.timeSinceLevelLoad * scrollSpeed, tileSizeY);
        transform.position = startPosition + Vector3.down * newPosition;
    }

    void OnDisable()
    {
        backgroundRenderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
