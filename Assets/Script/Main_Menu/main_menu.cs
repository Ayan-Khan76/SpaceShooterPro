using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_menu : MonoBehaviour
{
    //[SerializeField]
    //private Button _gameButton;

    public void LoadGame()
    {
            SceneManager.LoadScene(1);
    }

}
