using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// UI管理
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject StringImages, TimeUp, Buttons, RankingView, Loading;
    public Image Fade, TimerBack;
    public Sprite sprite;
    public AudioSource Audio;
    public AudioClip StartSE, EndSE;
    public Text Score, hiScore, Timer;
    public Text[] Ranklist;

    // Start is called before the first frame update
    void Start()
    {
        //Starting();
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        Fade.DOFade(endValue: 0f, duration: 2f).OnComplete(() => 
        {
            Fade.enabled = false;
        });
    }

    public void Starting()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(StringImages.transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce));

        sequence.Play().OnComplete(() =>
        {
            
            Debug.Log("スタート！");
            StartCoroutine(Out());
        });
    }

    IEnumerator Out()
    {
        Audio.PlayOneShot(StartSE);
        yield return new WaitForSeconds(1f);
        StringImages.SetActive(false);
        GameManager.CookieSet();
    }

    public void GetScore(int score)
    {
        Score.text = "Score " + score.ToString();
    }
    public void GetHiScore(int hiscore)
    {
        hiScore.text = "HiScore " + hiscore.ToString();
    }

    public void GetTImer(float timer)
    {
        int number = (int)timer;
        Timer.text = number.ToString();
        if (10 >= number)
            Timer.color = new Color(255, 0, 0);

        if (timer < 0)
            TimerBack.sprite = sprite;
    }
    public void ButtonDisplay()
    {
        StartCoroutine(Result());
    }


    /// <summary>
    /// ランキングデータのセット
    /// </summary>
    /// <param name="Scores"></param>
    public void SetRank(List<string> Scores)
    {
        Debug.Log("ScoreView");
        Loading.SetActive(false);
        int rank = 1;

        for (int i = Scores.Count - 1; i >= 0; i--)
        {
            Debug.Log(rank + "位　" + Scores[i]);
            if (rank <= Scores.Count)
            {
                Ranklist[rank - 1].text = rank + "位　" + Scores[i];
            }
            else
            {
                Ranklist[rank - 1].text = rank + "位　NoName　0";
            }

            rank++;
            if (rank > 10)
                break;
        }
        RankingView.SetActive(true);
    }

    IEnumerator Result()
    {
        TimeUp.SetActive(true);
        Audio.PlayOneShot(EndSE);
        yield return new WaitForSeconds(2f);

        Buttons.SetActive(true);
    }
}
