using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For UI elements

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    public Text scoreText; // Reference to the UI Text element
    public int scorePerRangeUpgrade = 10; // Cost to upgrade range

    // Method to add points
    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Method to spend points for upgrades
    public bool SpendPoints(int points)
    {
        if (score >= points)
        {
            score -= points;
            UpdateScoreText();
            return true; // Successful spend
        }
        return false; // Not enough points
    }

    // Method to update the score display
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the text
        }
    }

    // Optional: Method to get current score
    public int GetScore()
    {
        return score;
    }
}