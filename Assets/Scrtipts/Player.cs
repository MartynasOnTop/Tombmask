using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 input;
    public float moveSpeed = 20f;
    public GameObject landingParticles;
    AudioSource source;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip coinSound;
    public AudioClip bigCoinsSound;
    public AudioClip starSound;
    bool hasLanded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    async void Update()
    {
        var newinput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Mathf.Abs(newinput.x) > 0 && Mathf.Abs(newinput.y) > 0)
        {
            newinput.y = 0;
        }

        if (rb.velocity.magnitude < 0.1f && !hasLanded && newinput != input)
        {
            Instantiate(landingParticles, transform.position, Quaternion.identity);
            source.PlayOneShot(land);
            hasLanded = true;
        }

        if( newinput != Vector2.zero && rb.velocity.magnitude < 0.1f)
        {
            input = newinput;
            transform.up = input * -1f;
            hasLanded = false;
            source.PlayOneShot(jump);
        }
        rb.velocity = input * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Star"))
        {
            Destroy(collision.gameObject);
            AudioSystem.Play(starSound);
        }
        if (collision.gameObject.name.Contains("Money"))
        {
            Destroy(collision.gameObject);
            AudioSystem.Play(bigCoinsSound);
        }
        if (collision.gameObject.name.Contains("Coin"))
        {
            Destroy(collision.gameObject);
            AudioSystem.Play(coinSound);
        }
    }
}
