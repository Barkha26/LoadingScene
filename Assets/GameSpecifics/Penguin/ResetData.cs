using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetData : MonoBehaviour {

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UnlockAll()
    {
        PlayerPrefs.SetInt("WorldReached", int.MaxValue);
        PlayerPrefs.SetInt("World1" + "HighestLevel", int.MaxValue);
        PlayerPrefs.SetInt("World2" + "HighestLevel", int.MaxValue);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	
}
