using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;

    [SerializeField]
    private TextMeshProUGUI scoreUI;
    [SerializeField]
    private TextMeshProUGUI debugMessage;
    [SerializeField]
    private AudioClip victorySound;


    private int score = 0;
    private int nbCollect = 0;
    private AudioSource source;
    private ReadCSV csvreader;

    public static ScoreManager Instance { get => instance; }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);    

        instance = this;
    }

    private void Start()
    {
        csvreader = this.GetComponent<ReadCSV>();
        source = this.GetComponent<AudioSource>();
    }

    public void AddScore(int point)
    {
        nbCollect++;
        score += point;
        UpdateScoreUI();
        CheckIfEnd();
    }

    private void UpdateScoreUI()
    {
        scoreUI.text = "Score : "+score;
    }

    private void CheckIfEnd()
    {
        if (nbCollect == csvreader.positions.Count)
        {
            this.GetComponent<Timer>().EndTimer();
            debugMessage.text = "Congratulations, you collected all " + nbCollect + "collectibles";
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AnimEnd();
            source.PlayOneShot(victorySound);
        }
    }
}
