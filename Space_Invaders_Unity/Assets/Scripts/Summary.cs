using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Summary : MonoBehaviour
{
    public TextMeshProUGUI Last_pos;
    public TextMeshProUGUI Last_score;

    public void BackButton()
    {
        AddNewEntry(Interface.currentScore, "TE3");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(0); // Main menu
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Read saves
        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.scoreEntryList.Count; i++)
        {
            if (Interface.currentScore > highscores.scoreEntryList[i].score)
            {
                Last_pos.text = i.ToString();
                break;
            }
            else
            {
                Last_pos.text = (highscores.scoreEntryList.Count + 1).ToString();
            }
        }

        Last_score.text = Interface.currentScore.ToString();
    }

    // Copied from ScoreTable
    public void AddNewEntry(int score, string name)
    {
        // Create new score entry
        ScoreEntry newscoreEntry = new ScoreEntry { score = score, name = name };

        // Read saves
        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Add new entry do highscores
        highscores.scoreEntryList.Add(newscoreEntry);

        // Sort entry list by Score
        for (int i = highscores.scoreEntryList.Count - 1; i > 0; i--)
        {
            if (highscores.scoreEntryList[i].score > highscores.scoreEntryList[i - 1].score)
            {
                //Swap
                ScoreEntry tmp = highscores.scoreEntryList[i];
                highscores.scoreEntryList[i] = highscores.scoreEntryList[i - 1];
                highscores.scoreEntryList[i - 1] = tmp;
            }
        }

        // Remove 11th entry
        if (highscores.scoreEntryList.Count > 10)
        {
            highscores.scoreEntryList.RemoveAt(10);
        }

        // Save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
    }

    // Class to store all statistics data
    private class Highscores
    {
        public List<ScoreEntry> scoreEntryList;
        public int gamesCounter;
    }

    // Class to store single score entry
    [System.Serializable]
    private class ScoreEntry
    {
        public int score;
        public string name;
    }
}
