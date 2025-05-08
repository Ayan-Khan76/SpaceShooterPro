using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TripleShot_Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]    
    private int powerupID;

    [SerializeField]
    private AudioClip _powerupsound;





    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            Player player = collision.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_powerupsound, transform.position);

            switch (powerupID)
            {
                case 0:
                    if (player != null)
                    {
                        player.TripleShot();
                    }
                    break;
                case 1:
                    if (player != null)
                    {
                        player.SpeedBoost();
                    }
                    break;
                case 2:
                    if(player != null)
                    {
                        player.Shield();
                    }
                    break; 
                case 3:
                    if (player != null)
                    {
                        player.Heart();
                    }
                    break;
            }
            
            Destroy(this.gameObject);

        }
    }
}
