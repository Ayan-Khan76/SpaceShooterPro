using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float enemy_fall = 4.0f;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _EnemyPrefab;

    private AudioSource _audioclip;

    private Player _player;

    Animator _animator;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioclip = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("player is null");
        }

        _animator = GetComponent<Animator>();

        if(_animator == null)
        {
            Debug.LogError("Animator is null");
        }

        if(_audioclip == null)
        {
            Debug.LogError("AudioClip is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * enemy_fall * Time.deltaTime);

        if(transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();

            if(player != null) {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            enemy_fall = 0;
            _audioclip.Play();
            Destroy(this.gameObject,2.4f);
        }
        if (other.tag == "Laser")
        {
            
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.IncScore(10);
            }

            _animator.SetTrigger("OnEnemyDeath");
            enemy_fall = 0;
            _audioclip.Play();
            Destroy(this.gameObject, 2.4f);
        }
    }
}
