using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public RectTransform BlockLevel;
    public int howManyBlocks = 2;

    private float step = 720f;
    private bool sliding = false;
    private float smooth = 10f;
    private float newPosX = 0;

	void Update () {
        if (sliding)
        {
            float X = Mathf.Lerp(BlockLevel.anchoredPosition.x, newPosX, 
                smooth * Time.deltaTime);
            BlockLevel.anchoredPosition = new Vector2(X, BlockLevel.anchoredPosition.y);
            if(Mathf.Abs(BlockLevel.anchoredPosition.x - newPosX) < 10)
            {
                BlockLevel.anchoredPosition = new Vector2(newPosX,
                    BlockLevel.anchoredPosition.y);
                sliding = false;
            }
        }
	}

    public void Next()
    {
        if (!sliding)
        {
            newPosX -= step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);
            sliding = true;
        }
    }

    public void Pre()
    {
        if (!sliding)
        {
            newPosX += step;
            newPosX = Mathf.Clamp(newPosX, -step * (howManyBlocks - 1), 0);
            sliding = true;
        }
    }


}
