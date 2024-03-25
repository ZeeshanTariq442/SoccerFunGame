using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBallSecond : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    bool isPlayer;
    public Animator animatorPlayerOne;
    public Animator animatorPlayerTwo;
    public AudioSource footballAudio;
    public AudioSource goalAudio;
    private int ramdomNum;
    public Sprite[] RIdel;
    public Sprite[] RKick;
    public Sprite[] LIdel;
    public Sprite[] LKick;
    public SpriteRenderer PlayerOneIdel;
    public SpriteRenderer PlayerOneKick;
    public SpriteRenderer PlayerTwoIdel;
    public SpriteRenderer PlayerTwoKick;
    public void ResetValues()
    {
        transform.localPosition = Point1.transform.localPosition;
        ramdomNum = 0;
        isPlayer = false;
       
    }
    public void TeamSelect(int index)
    {
        PlayerOneIdel.sprite = RIdel[index];
        PlayerOneKick.sprite = RKick[index];
        PlayerTwoIdel.sprite = LIdel[index];
        PlayerTwoKick.sprite = LKick[index];


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "kick" && collision.gameObject.tag == "Player")
        {
            Invoke(nameof(playSound), 0.05f);
            isPlayer = true;
            animatorPlayerOne.SetBool("kick", false);

        }
        if (collision.gameObject.tag == "Player" && collision.gameObject.name == "point")
        {

            animatorPlayerOne.SetBool("kick", true);

            Debug.Log("Point Player");
        }

       

        if (collision.gameObject.name == "kick" && collision.gameObject.tag == "Player2")
        {
           
            isPlayer = false;
            animatorPlayerTwo.SetBool("kick", false);
        }
       
        if (collision.gameObject.tag == "kick" && collision.gameObject.name == "point")
        {
         
            Invoke(nameof(playSound), 0.7f);
            animatorPlayerTwo.SetBool("kick", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "point" && collision.gameObject.tag == "Player")
        {
          
            Debug.Log("Trigger");
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
                transform.localPosition = Vector3.Lerp(transform.position, Point2.transform.localPosition, 2 * Time.deltaTime);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.position, Point1.transform.localPosition, 2 * Time.deltaTime);
            }
            goalAudio.Play();
    }

}

