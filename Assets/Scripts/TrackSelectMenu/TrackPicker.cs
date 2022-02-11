using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackPicker : MonoBehaviour {
    private SceneLoaderWithTransition _sceneLoaderWithTransition;
    //public void PlayScene(string sceneName) => SceneManager.LoadScene(sceneName);

    private void Start()
    {
        _sceneLoaderWithTransition = FindObjectOfType<SceneLoaderWithTransition>();
    }

    public void BackToMainMenu()
    {
        _sceneLoaderWithTransition.Load("MainMenu");
    }
}
