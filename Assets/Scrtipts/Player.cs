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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    async void Update()
    {
        var newinput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if( newinput != Vector2.zero && rb.velocity.magnitude < 0.1f)
        {
            input = newinput;
            transform.up = input * -1f;
            source.PlayOneShot(jump);
        }
        rb.velocity = input * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(landingParticles, transform.position, Quaternion.identity);
            source.PlayOneShot(land);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            collision.GetComponent<PlaySound>().PlayS();
        }
    }
}
