using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private int speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;


    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;



    private spawn_Manager _spawn_Manager;

    
    [SerializeField]
    private GameObject _TripleShotprefab;

    [SerializeField]
    private GameObject _ShieldVisualizerPowerup;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private int _score;

    [SerializeField]
    private AudioClip _laserAudio;

    private AudioSource _audioSource;

    private int _totalscore;

    private UI_Script _UiManager;

    //boolean
    private bool triple_shot_enabled = false;
    private bool speedboost_enabled = false;
    private bool _shieldActive = false;



    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign it starting position
        transform.position = new Vector3(0, 0, 0);
        //x_pos = 12f;

        _spawn_Manager = GameObject.Find("spawn_Manager").GetComponent<spawn_Manager>();
        _UiManager = GameObject.Find("Canvas").GetComponent<UI_Script>();
        _audioSource = GetComponent<AudioSource>();

        if ( _spawn_Manager == null)
        {
            Debug.LogError("_spawn_Manager is NULL");
        }

        if (_UiManager == null)
        {
            Debug.LogError("_UiManager is NULL");
        }

        if(_audioSource == null)
        {
            Debug.LogError("_audioSource Is NULL");
        }
        else
        {
            _audioSource.clip = _laserAudio;
        }

    }

    // Update is called once per frame
    void Update()
    {
      CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) 
        { 
            Firelaser();
        }


      
    }
        void CalculateMovement()
        {
            //new vector3 moving on right with 5 metres per second.
            //deltaTime is used to convert frames into seconds.
            //(x,y,z)

            float horizontalInput = Input.GetAxis("Horizontal");
            float VerticalInput = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontalInput, VerticalInput, 0);

            transform.Translate(direction * _speed * Time.deltaTime);
        

            

             if (transform.position.y >= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }
            else if (transform.position.y <= -3.8f)
            {
                transform.position = new Vector3(transform.position.x, -3.8f, 0);
            }

          //A more efficient way
         //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

            if (transform.position.x > 12f)
            {
                transform.position = new Vector3(-12f, transform.position.y, 0);
            }
            else if (transform.position.x < -12f)
            {
                transform.position = new Vector3(12f, transform.position.y, 0);
            }

        }

        void Firelaser()
    {
          _canFire = Time.time + _fireRate;

          if (triple_shot_enabled)
        {
            Instantiate(_TripleShotprefab,transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }
    
        public void Damage()
    {
        //makes shieldActive For One Hit.
        if(_shieldActive == true)
        {
            _shieldActive = false;
            _ShieldVisualizerPowerup.SetActive(false);
            return;
            
        }

        _lives -= 1;
        _UiManager.UpdateLives(_lives);
        SpawnEngine();

        

        /*
        else
        {
            StartCoroutine(Shield_Timer());
        }*/

        if(_lives == 0)
        {
            _spawn_Manager.OnPlayerDeath();
            Destroy(this.gameObject);
            TotalScore();
            //spawn_Manager spawn_manager = gameObject.transform.GetComponent<spawn_Manager>();
            //Destroy(spawn_manager);
        }
    }

    public void TripleShot()
    {
        triple_shot_enabled = true;
        StartCoroutine(tripleShot_Timer());
    }

    public void Shield()
    {
        _shieldActive = true;
        _ShieldVisualizerPowerup.SetActive(true);
    }

    IEnumerator tripleShot_Timer()
    {
        while (triple_shot_enabled)
        {
            yield return new WaitForSeconds(5.0f);
            triple_shot_enabled = false;
        }
    }


    public void SpeedBoost()
    {
        speedboost_enabled = true;
        _speed *= speedMultiplier;
        StartCoroutine(Speedboost_Timer());
    }

    IEnumerator Speedboost_Timer()
    {
        if (speedboost_enabled)
        {
            yield return new WaitForSeconds(5.0f);
            speedboost_enabled = false;
            _speed /= speedMultiplier;
        } 
    }

    //gives a timer for shieldsActive
    /*IEnumerator Shield_Timer()
    {
        yield return new WaitForSeconds(15.0f);
        _shieldActive = false;
    }*/

    public void IncScore(int points)
    {
        _score += points;
        _UiManager.UpdateScore(_score);
    }

    public void TotalScore()
    {
            _totalscore = _score;
            _UiManager.ShowScore(_totalscore);
    }

    public void SpawnEngine()
    {
        if (_lives == 2 || _lives == 1)
        {
            int randomEngine = Random.Range(0, 2);

            if (_engines[randomEngine].activeSelf)
            {
                randomEngine = 1 - randomEngine;
            }
            _engines[randomEngine].SetActive(true);

        }
    }

    public void Heart()
    {
        if(_lives < 3)
        {
            _lives += 1;
        }
    }
}
