using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOver;

    [SerializeField] 
    private Text _totalscore;

    [SerializeField]
    private Image _liveImgs;

    [SerializeField]
    private Game_Manager game_manager;

    [SerializeField]
    private Sprite[] _liveSprite;

    private Player player;


    void Start()
    {
        _scoreText.text = "Score: " + 0;
        game_manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();

        player = GameObject.Find("Player").GetComponent<Player>();

        if(game_manager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }

    void Update()
    {

    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score : " + playerScore.ToString();
        _gameOver.gameObject.SetActive(false);
    }

    public void UpdateLives(int CurrentLive)
    {
        _liveImgs.sprite = _liveSprite[CurrentLive];

        if(CurrentLive == 0)
        {
            gameOverSequence();
        }
    }

    private void gameOverSequence()
    {
        _gameOver.gameObject.SetActive(true);
        StartCoroutine(GameOver_Flicker());
        game_manager.GameOver();
    }

    IEnumerator GameOver_Flicker()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ShowScore(int score)
    {
        _totalscore.text = "Score: " + score;
    }
}
