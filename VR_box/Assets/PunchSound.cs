using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSound : MonoBehaviour
{

    public AudioClip punh_block1;
    public AudioClip punh_block2;
    public AudioClip punh_block3;
    public AudioClip punh_block4;

    public AudioClip punh_body_general_1;
    public AudioClip punh_body_general_2;
    public AudioClip punh_body_general_3;
    public AudioClip punh_body_general_4;
    public AudioClip punh_body_general_5;
    public AudioClip punh_body_general_6;
    public AudioClip punh_body_general_7;
    public AudioClip punh_body_general_8;
    Random random;

    List<AudioClip> punch_sound;
    public GameObject head;
    public GameObject tors;
    private AudioSource source;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        random = new Random();
        punch_sound = new List<AudioClip> { punh_body_general_1, punh_body_general_2, punh_body_general_3, punh_body_general_4, punh_body_general_5, punh_body_general_6, punh_body_general_7, punh_body_general_8 };
        source = GetComponent<AudioSource>();
        player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject == head)
        {
            Debug.Log("HIT_head");
            int index = Random.Range(0, 7);
            source.PlayOneShot(punch_sound[index], 1f);
        }

        if (other.gameObject == tors)
        {
            Debug.Log("HIT_tors");
            int index = Random.Range(0, 7);
            source.PlayOneShot(punch_sound[index], 1f);
        }
    }
}
