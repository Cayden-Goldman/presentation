using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb2d;
    Transform big;
    bool spinning;
    public GameObject rings;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = rb2d.GetComponent<SpriteRenderer>();
        big = GetComponent<Transform>();
    }

    void Update()
    {
        sr.color = Color.HSVToRGB(Time.time % 1 / 2 * 1.36f, 1, 1);
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 5, rb2d.velocity.y);
        if (Input.GetKey(KeyCode.O))
        {
            big.localScale = big.localScale * 2;
        }
        if (Input.GetKey(KeyCode.L))
        {
            big.localScale = big.localScale / 2;

        }
        if (Input.GetKeyDown(KeyCode.R) && !spinning)
        {
            StartCoroutine(Spin());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetKeyDown (KeyCode.Delete))
        {
            Application.Quit();
        }
    }

    IEnumerator Spin()
    {
        Vector3 startPos = transform.position + new Vector3(0, 1);
        spinning = true;
        yield return new WaitForEndOfFrame();
        while (!Input.GetKeyDown(KeyCode.R))
        {
            yield return null;
            transform.position = startPos + new Vector3(Mathf.Sin(Time.time * 10), Mathf.Cos(Time.time * 10)) * 2;
        }
        spinning = false;
    }

    IEnumerator Dash()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForEndOfFrame();
        for (float t = 0; t < Mathf.PI * 4; t += Time.deltaTime * 10)
        {
            yield return null;
            transform.position = startPos + new Vector3(t * 1.5f, Mathf.Sin(t));
        }
        rb2d.velocity = new Vector2(5, 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("onion"))
        {
            Instantiate(rings, collision.transform.position, new());
            Destroy(collision.gameObject);
        }
    }

}
