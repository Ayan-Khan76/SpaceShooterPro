using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private AudioSource _audioclip;
    // Start is called before the first frame update
    void Start()
    {
        _audioclip = GetComponent<AudioSource>();
        Destroy(this.gameObject, 3f);
        _audioclip.Play(); 
    }
}
