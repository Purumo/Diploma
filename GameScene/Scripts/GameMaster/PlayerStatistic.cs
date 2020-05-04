using UnityEngine;
using GameScene.GameMaster;using UnityEngine.UI;
using GameScene.EnemiesModule;

[System.Serializable]
public struct Stat
{
    [HideInInspector] public int score;
    public Text uiText;
}
//[System.Serializable]
//public struct VarStat
//{
//    [HideInInspector] public float score;
//    public GameObject panel;
//    public Text uiText;
//}
public class PlayerStatistic : MonoBehaviour
{
    private static PlayerStatistic instance;

    // private int scorePerRound = 0;
    private int currentKilledEnemyWaveIdx = 0;

    //private int enemiesPerRound = 0;

    private int pointsPerRound = 0;
    public Text pointsPerRoundText;
    public GameObject pointsPerRoundPanel;

    public GameObject nextWaveCountdownPanel;

    public Stat roundsPassed;
    public Stat pointsScored;

    void Start()
    {
        instance = this;

        roundsPassed.score = 0;
        pointsScored.score = 0;
    }

    public void KillEnemy(int score)
    {
        pointsPerRound += score;
        pointsPerRoundText.text = pointsPerRound.ToString();

        WaveSpawner.enemiesAlive--;

        currentKilledEnemyWaveIdx++;
        if (currentKilledEnemyWaveIdx == WaveSpawner.currentWaveCount)
        {
            currentKilledEnemyWaveIdx = 0;
            RoundPassed();
        }
    }
    public void RoundPassed()
    {
        pointsScored.score += pointsPerRound;
        pointsScored.uiText.text = pointsScored.score.ToString();
        roundsPassed.score++;
        roundsPassed.uiText.text = roundsPassed.score.ToString();

        pointsPerRound = 0;
        pointsPerRoundText.text = pointsPerRound.ToString();
        pointsPerRoundPanel.SetActive(false);

        //nextWaveCountdown.score = 0;//timeBetweenWaves
        //nextWaveCountdown.uiText.text = pointsScored.score.ToString();

        nextWaveCountdownPanel.SetActive(true);
    }

    public static PlayerStatistic GetInstance()
    {
        return instance;
    }
}
