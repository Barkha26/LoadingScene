using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{

    public Renderer background;
    public float speedBG;
    public Renderer midground;
    public float speedMG;
    public Renderer foreground;
    public float speedFG;

    Camera target;
    float startPosX;

    private void Start()
    {
        target = Camera.main;
        startPosX = target.transform.position.x;

        ApplyMaterialFromAssetBundle();
    }

    private void ApplyMaterialFromAssetBundle()
    {
        //background.material = LoadScene.instance.mat;
    }

    void Update()
    {
        var x = target.transform.position.x - startPosX;

        Vector2 offSet = new Vector2(speedBG * Time.time, 0);
        background.material.mainTextureOffset = offSet;

        if (midground != null)
            midground.material.mainTextureOffset = new Vector2(x * speedMG,
                midground.material.mainTextureOffset.y);

        if (foreground != null)
            foreground.material.mainTextureOffset = new Vector2(x * speedFG,
                foreground.material.mainTextureOffset.y);
    }
}
