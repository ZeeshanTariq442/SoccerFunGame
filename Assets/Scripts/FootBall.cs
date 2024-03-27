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
    public AudioSource goalAudio;
    public Text scoreCount;
    private int counter;
    private int secondPlayerCounter;
    private Vector3 goalPoint = new Vector3(0, 0, -3);
    private int ramdomNum;
    public GameController gameController;
    bool isGoal;
    bool isGameFinished;
    public Text secondPlayerCounterScore;
    public void ResetValues()
    {
        transform.localPosition = Point1.transform.localPosition;
        scoreCount.text = "0";
        counter = 0;
        secondPlayerCounter = 0;
        secondPlayerCounterScore.text = "0";
        ramdomNum = 0;
        isGoal = false;
        isPlayer = false;
        isGameFinished = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.name == "kick" && collision.gameObject.tag == "Player")
            {
            ramdomNum = Random.Range(1, 25);
            Debug.Log("Player");
                if(ramdomNum != 11)
                {
                    counter++;
                    isPlayer = true;
                }
                else
                {
                    Invoke(nameof(GameLoss), 1);
                }

          
            if (counter > int.Parse(gameController.countScoure.text.ToString()))
            {
                if (!isGameFinished)
                {
                    counter--;
                    isGoal = true;
                    isPlayer = true;
                    goalAudio.Play();
                    Invoke(nameof(GameWin), 1);
                }

            }
            secondPlayerCounterScore.text = counter.ToString();
            Invoke(nameof(playSound), 0.05f);
               
               
        }
        if (collision.gameObject.tag == "Player" && collision.gameObject.name == "point")
        {
            
            
        }
            if (collision.gameObject.name == "kick" && collision.gameObject.tag == "Player2")
            {
                Debug.Log("NOt Player");

            ramdomNum = Random.Range(1, 25);
            gameController.PlayerAimator.SetBool("kick", false);
            animator.SetBool("kick", false);
            if (counter >= int.Parse(gameController.countScoure.text.ToString()))
            {

                if (!isGameFinished)
                {
                    counter--;
                    isGoal = true;
                    ramdomNum = 9;
                    goalAudio.Play();
                    Invoke(nameof(GameLoss), 1);
                }

            }
            if (ramdomNum != 11)
                {

                    counter++;
                    isPlayer = false;
                }
                else
                {
                    Invoke(nameof(GameWin), 1);
                }
                
                secondPlayerCounterScore.text = counter.ToString();
          
            Debug.Log("point");
            
        }



            if (collision.gameObject.tag == "kick" && collision.gameObject.name == "point")
            {
                 
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
        Debug.Log(ramdomNum + "  ,..,");
            if (!isGoal)
            {
                if (isPlayer)
                {
                    transform.localPosition = Vector3.Lerp(transform.position, Point2.transform.localPosition, 2 * Time.deltaTime);
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(transform.position, Point1.transform.localPosition, 2 * Time.deltaTime);
                }
            }

            if(isGoal)
            {

                if (isPlayer)
                {
                    Invoke(nameof(GameWin), 1);
                    transform.position = Vector3.Lerp(transform.position, goalPoint, 2 * Time.deltaTime);
                }
                else
                {
                    Invoke(nameof(GameLoss), 1);
                    transform.position = Vector3.Lerp(transform.position, goalPoint, 2 * Time.deltaTime);

                }
                goalAudio.Play();

            }
            
        
        Debug.Log("Player...." + isPlayer);
        

    }

    private void GameWin()
    {
        isGameFinished = true;
        if (gameController.isFinal)
        {
            gameController.WinPanel.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
        }
        gameController.PanelsController("WinPanel");
     
    }
    private void GameLoss()
    {
        isGameFinished = true;
        gameController.PanelsController("LossPanel");

    }
}
