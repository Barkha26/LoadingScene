using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas canvas;
    private void Start()
    {
        //canvas.sortingLayerName = "BG";
    }

    public void Play()
    {
        //LoadScene.instance.LoadMaterial();
        LoadScene.instance.PlayBtn("Level1");
        //SceneManager.LoadScene("Level 1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
