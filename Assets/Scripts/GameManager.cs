using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    public Text playerScore;

    public void Score(int points)
    {
        score += points;
        playerScore.text = score.ToString();
    }
}
