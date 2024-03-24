using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FootBall : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    bool isPlayer;
    public Animator animator;
    public AudioSource footballAudio;
    private bool isPlayerKick;
    public Text scoreCount;
    private int counter;
    private void Start()
    {
        Debug.Log("Point1" + Point1.transform.localPosition);
        Debug.Log("Point2" + Point2.transform.localPosition);
    }

    public void ScoreToPlayer()
    {
        if (isPlayerKick)
        {
            counter++;
            scoreCount.text = counter.ToString();
            isPlayerKick = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "kick" && collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            isPlayerKick = true;
            Invoke(nameof(playSound), 0.05f);
            isPlayer = true;
        }
        else if (collision.gameObject.name == "kick" && collision.gameObject.tag == "Player2")
        {
            Debug.Log("NOt Player");
            isPlayer = false;
            isPlayerKick = false;
            animator.SetBool("kick", false);
        }
        Debug.Log(collision.gameObject.name + "   " + collision.gameObject.tag);
        if (collision.gameObject.tag == "kick" && collision.gameObject.name == "point")
        {
            Debug.Log("point");
            isPlayerKick = false;
            Invoke(nameof(playSound), 0.7f);
            animator.SetBool("kick", true);
        }
    }
    private void playSound()
    {
        footballAudio.Play();
    }
    private void Update()
    {
        
        if (isPlayer)
        {
            transform.localPosition = Vector3.Lerp(transform.position, Point2.transform.localPosition, 1 * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.position, Point1.transform.localPosition, 1 * Time.deltaTime);
        }
     
    }
}
