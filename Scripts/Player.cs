using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator anim;
    int dashDir = 0;
    public float dashMultiplier;
    public Collider2D platCol;
    public Collider2D col;
    public float gravity;
    public float paraGravity;
    public GameObject birdDeathEffect;
    public GameObject bouncyBirdDeathEffect;
    public float bounceSize;
    bool upDash = false;
    public float bounceSpeed;
    public int birdPoints = 2;
    public float screenShakeIntensity;
    public float screenShakeRotationIntensity;
    public float screenShakeLength;
    public float screenShakes;
    float time;
    bool shake = false;
    public GameObject cam;
    public AudioSource sound;

    void Awake()
    {
        if (PlayerPrefs.GetInt("faster") == 1)
        {
            speed *= 2;
        }
    }

    void Update()
    {
        if (transform.position.y > 2)
        {
            shake = true;
        }

        if (rb.IsTouchingLayers(LayerMask.GetMask(new string[] {"Ground"})))
        {
            if (shake && transform.position.y < 2)
            {
                sound.Play();
                shake = false;
                StartCoroutine(Shake());
            }
            rb.gravityScale = gravity;
            if (upDash && transform.position.y < bounceSize)
            {
                rb.velocity = new Vector2(0, bounceSpeed);
                platCol.enabled = false;
            }
            else
            {
                platCol.enabled = true;
                upDash = false;
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    platCol.enabled = false;
                }
                else
                {
                    platCol.enabled = true;
                    if (Input.GetKeyDown(KeyCode.Z) && PlayerPrefs.GetInt("dash") == 1)
                    {
                        dashDir = (int)(Input.GetAxisRaw("Horizontal") * speed * dashMultiplier);
                        if (Input.GetAxisRaw("Horizontal") == -1)
                        {
                            anim.SetInteger("State", 4);
                        }
                        else if (Input.GetAxisRaw("Horizontal") == 1)
                        {
                            anim.SetInteger("State", 5);
                        }
                        else
                        {
                            anim.SetInteger("State", 0);
                        }
                    }
                    else if (Input.GetKey(KeyCode.Z) && dashDir != 0 && PlayerPrefs.GetInt("dash") == 1)
                    {
                        rb.velocity = new Vector2(dashDir, 0);
                    }
                    else if (Input.GetAxisRaw("Horizontal") == 0)
                    {
                        rb.velocity = Vector2.zero;
                        anim.SetInteger("State", 0);
                    }
                    else
                    {
                        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);
                        anim.SetInteger("State", 1);
                    }
                }
            }
        }
        else
        {
            dashDir = 0;
            if (upDash)
            {
                if (transform.position.y < bounceSize)
                {
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(0, bounceSpeed);
                    platCol.enabled = false;
                }
                else
                {
                    upDash = false;
                    platCol.enabled = true;
                }
            }
            else
            {
                if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && PlayerPrefs.GetInt("parachute") == 1)
                {
                    rb.gravityScale = paraGravity;
                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);
                    anim.SetInteger("State", 3);
                }
                else
                {
                    rb.gravityScale = gravity;
                    anim.SetInteger("State", 2);
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Birds"))
        {
            Destroy(collision.gameObject);
            if (platCol.enabled == false)
            {
                Instantiate(birdDeathEffect, collision.transform.position, Quaternion.identity);
                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + birdPoints);
                if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                }
            }
            else
            {
                transform.position = collision.transform.position;
                anim.SetInteger("State", 6);
                upDash = true;
            }
        }
        else if (collision.CompareTag("BouncyBirds"))
        {
            Destroy(collision.gameObject);
            upDash = true;
            if (platCol.enabled == false)
            {
                Instantiate(bouncyBirdDeathEffect, collision.transform.position, Quaternion.identity);
                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + birdPoints);
                if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                }
                anim.SetInteger("State", 8);
                StartCoroutine(Shake());
            }
            else
            {
                transform.position = collision.transform.position;
                anim.SetInteger("State", 7);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Start"))
        {
            SceneManager.LoadScene("Infinite");
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("faster", 0);
            PlayerPrefs.SetInt("dash", 0);
            PlayerPrefs.SetInt("parachute", 0);
            PlayerPrefs.SetInt("BouncyBirds", 0);
            PlayerPrefs.SetInt("crown", 0);
        }
        else if (collision.transform.CompareTag("Continue"))
        {
            if (PlayerPrefs.GetInt("score") + PlayerPrefs.GetInt("faster") + PlayerPrefs.GetInt("dash") + PlayerPrefs.GetInt("parachute") + PlayerPrefs.GetInt("BouncyBirds") + PlayerPrefs.GetInt("crown") != 0)
            {
                SceneManager.LoadScene("Infinite");
            }
        }
    }

    IEnumerator Shake()
    {
        for (int i = 0; i < screenShakes; i++)
        {
            time = Time.time + screenShakeLength;
            cam.GetComponent<GlideToCamera>().shaking = true;
            cam.GetComponent<Camera>().orthographicSize = (float)Random.Range(45, 51)/10;
            yield return new WaitUntil(() => Time.time > time);
            cam.GetComponent<GlideToCamera>().shaking = false;
            cam.GetComponent<Camera>().orthographicSize = 5;
        }
    }
}