using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private bool _isgameover = false;

    private void Update()
    {

    }

    public void GameOver()
    {
        _isgameover = true;
    }

    public void Restart()
    {
        if (_isgameover == true)
        {
            SceneManager.LoadScene(1); //Current Game Scene
        }
    }

    public void QuitApp()
    {
        if(_isgameover == true)
        {
            Application.Quit();
        }
    }
}
