using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region ------ Panels ------   
    [Header("Game Panels")]
    public GameObject HomePanel;
    public GameObject GamePlay;
    public GameObject SettingPanel;
    public GameObject PausePanel;
    public GameObject SelectTeam_Panel;
    public GameObject Group_Map;
    public GameObject WinPanel;
    public GameObject LossPanel;
    #endregion
    #region ------ Team Flags ------
    public GameObject[] Flags;
    public Image[] QualifyRound;
    public Image[] FinalRound;
    #endregion
    #region ------ Private Variables ------
    private int currentFlags;
    private int playerTeam = 0;
    private int playerMatchBetween;
    private Dictionary<int, List<int>> ramdomNumber = new Dictionary<int, List<int>>();
    #endregion
    #region ------ UI ------
    public Button PreBtn;
    public Button NextBtn;
    public Text playerGoalCount;
    public Image playerTeamFlag;
    public Text counterTimer;
    #endregion
    #region ------ Setting UI ------
    public GameObject MusicOff;
    public GameObject SoundOff;
    #endregion
    #region ------ Character Sprites ------
    [Header("----- Charater Sprites -----")]
    public Sprite[] kickSprites;
    public Sprite[] idelSprites;
    public SpriteRenderer PlayerIdel;
    public SpriteRenderer PlayerKick;
    public GameObject CharacterContainer;
    public GameObject SecondPlayer;
    public Sprite[] RightSidekickSprites;
    public Sprite[] RightSideidelSprites;
    public GameObject Football;
    #endregion
    #region ------ Animation and Animator ------
    public Animator PlayerAimator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ramdomNumber.Add(0, new List<int>() {0,2,1,3 });
        ramdomNumber.Add(1, new List<int>() {1,2,0,3 });
        ramdomNumber.Add(2, new List<int>() {2,3,1,0});
        ramdomNumber.Add(3, new List<int>() {2,0,1,3 });
        MusicControl(PlayerPrefs.GetString("Music") == "ON");
        SoundControl(PlayerPrefs.GetString("Sound") == "ON");
        PanelsController(HomePanel.name);
        NextBtn.interactable = false;
    }

    public void onClickDownOnPlayer()
    {
        PlayerAimator.SetBool("kick", true);
    }
    public void onClickUpOnPlayer()
    {
        PlayerAimator.SetBool("kick", false);
    }
    public void TeamSelect(int index)
    {
        NextBtn.interactable = true;
        playerTeam = index;
        for(int i = 0; i < Flags.Length; i++)
        {
            Flags[i].gameObject.transform.GetChild(0).gameObject.SetActive(i == index);

        }
    }
    public void ScrollFlags(bool next)
    {
        if (next)
        {
            PreBtn.interactable = true;
            if (currentFlags < Flags.Length)
            {
                currentFlags++;
                Flags[currentFlags].gameObject.SetActive(true);
                Debug.Log(currentFlags);
                if (currentFlags >= Flags.Length - 1)
                {
                
                    NextBtn.interactable = false;
                }
            }
           

        }
        else
        {
            NextBtn.interactable = true;
            if (currentFlags > 0)
            {
                Flags[currentFlags].gameObject.SetActive(false);
                currentFlags--;
                if(currentFlags == 0)
                {
                    PreBtn.interactable = false;
                }
                
            }
            
          
        }
      
    
    }
    public void PanelsController(string name)
    {
        HomePanel.SetActive(name == HomePanel.name);
        GamePlay.SetActive(name == GamePlay.name);
        CharacterContainer.SetActive(name == GamePlay.name);
        SecondPlayer.SetActive(name == GamePlay.name);
        Football.SetActive(name == GamePlay.name);
        SettingPanel.SetActive(name == SettingPanel.name);
        PausePanel.SetActive(name == PausePanel.name);
        SelectTeam_Panel.SetActive(name == SelectTeam_Panel.name);
        Group_Map.SetActive(name == Group_Map.name);
        WinPanel.SetActive(name == WinPanel.name);
        LossPanel.SetActive(name == LossPanel.name);


    }

    public void MusicControl(bool on)
    {
        if (on)
        {
            PlayerPrefs.SetString("Music", "ON");
            MusicOff.SetActive(on);
        }
        else
        {
            PlayerPrefs.SetString("Music", "OFF");
            MusicOff.SetActive(on);
        }
    }
    public void SoundControl(bool on)
    {
        if (on)
        {
            PlayerPrefs.SetString("Sound", "ON");
            SoundOff.SetActive(on);
        }
        else
        {
            PlayerPrefs.SetString("Sound", "OFF");
            SoundOff.SetActive(on);
        }
    }

    public void TeamsQualifyRound()
    {
        int num = Random.Range(0, 3);

        for (int i = 0; i<ramdomNumber[num].Count; i++)
        {

            QualifyRound[i].sprite = Flags[ramdomNumber[num][i]].GetComponent<Image>().sprite;
            
            if(ramdomNumber[num][i] == playerTeam)
            {
                playerMatchBetween = ramdomNumber[num][i + 1];
                Debug.Log("dddd" + playerTeam + " , " + ramdomNumber[num][i + 1] + " , " + num);
                
            }

        }

        for(int i = 0; i < FinalRound.Length; i++)
        {
            FinalRound[i].sprite = null;
        }
        SecondPlayer.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = RightSideidelSprites[playerMatchBetween];
        SecondPlayer.transform.GetChild(0).transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = RightSidekickSprites[playerMatchBetween];
        playerTeamFlag.sprite = Flags[playerTeam].GetComponent<Image>().sprite;
        playerGoalCount.text = "0";
        PlayerIdel.sprite = idelSprites[playerTeam];
        PlayerKick.sprite = kickSprites[playerTeam];
       
        StartCoroutine(CounterTimer());
        Invoke(nameof(GoToGamePlay), 5);
      
    }

    private void GoToGamePlay()
    {
        PanelsController(GamePlay.name);
        CharacterContainer.SetActive(true);
        SecondPlayer.SetActive(true);
        Football.SetActive(true);
    }
    private IEnumerator CounterTimer()
    {
     
        yield return new WaitForSeconds(1);
        counterTimer.text = "3";
        yield return new WaitForSeconds(1);
        counterTimer.text = "2";
        yield return new WaitForSeconds(1);
        counterTimer.text = "1";
        yield return new WaitForSeconds(1);
        counterTimer.text = "0";
    }
}
