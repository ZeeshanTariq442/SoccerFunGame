using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBall : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    bool isPlayer;
    public Animator animator;
    private void Start()
    {
        Debug.Log("Point1" + Point1.transform.localPosition);
        Debug.Log("Point2" + Point2.transform.localPosition);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "kick" && collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            isPlayer = true;
        }
        else if (collision.gameObject.name == "kick" && collision.gameObject.tag == "Player2")
        {
            Debug.Log("NOt Player");
            isPlayer = false;
            animator.SetBool("kick", false);
        }
        Debug.Log(collision.gameObject.name + "   " + collision.gameObject.tag);
        if (collision.gameObject.tag == "kick" && collision.gameObject.name == "point")
        {
            Debug.Log("point");
            animator.SetBool("kick", true);
        }
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
