using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<ScoreEntry> scoreEntryList;
    private List<Transform> scoreEntryTransformList;

    public TextMeshProUGUI Games_count;

    // Init of the high score table, reading data
    private void Awake()
    {
        entryContainer = transform.Find("ScoreEntryContainer");
        entryTemplate = entryContainer.Find("ScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        /*
        scoreEntryList = new List<ScoreEntry>()
        { 
            new ScoreEntry{ score = 1, name = "AA1" },
            new ScoreEntry{ score = 2, name = "AA2" },
            new ScoreEntry{ score = 3, name = "AA3" },
            new ScoreEntry{ score = 20, name = "A20" },
            new ScoreEntry{ score = 5, name = "AA5" },
            new ScoreEntry{ score = 6, name = "AA6" },
            new ScoreEntry{ score = 7, name = "AA7" },
            new ScoreEntry{ score = 8, name = "AA8" },
        };
        */

        //AddNewEntry(13, "TO2");

        // Read saves
        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        /*
        // Sort entry list by Score
        for (int i = 0; i < highscores.scoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.scoreEntryList.Count; j++)
            {
                if(highscores.scoreEntryList[j].score > highscores.scoreEntryList[i].score)
                {
                    //Swap
                    ScoreEntry tmp = highscores.scoreEntryList[i];
                    highscores.scoreEntryList[i] = highscores.scoreEntryList[j];
                    highscores.scoreEntryList[j] = tmp;
                }
            } 
        }
        */

        scoreEntryTransformList = new List<Transform>();
        foreach (ScoreEntry scoreEntry in highscores.scoreEntryList)
        {
            CreateScoreEntryTransform(scoreEntry, entryContainer, scoreEntryTransformList);
        }


        //int gamesCounter = Random.Range(0, 10000);
        //int gamesCounter = 0;
        // add 1 to counter
        highscores.gamesCounter++;
        // Print no of games
        Games_count.text = highscores.gamesCounter.ToString();

        //PlayerPrefs.SetInt("Games_count", number);
        
        //Highscores highscores = new Highscores { scoreEntryList = scoreEntryList, gamesCounter = 0 };
        //highscores.gamesCounter = 0;
        // Save data
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetString("scoreTable"));
    }

    // Method drawing one row of high score table
    private void CreateScoreEntryTransform(ScoreEntry scoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 80f;
        
        // Prepare transformation of the template
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // Print rank
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("Text_Pos").GetComponent<TextMeshProUGUI>().text = rankString;
        // entryTransform.Find("Text_test").GetComponent<Text>().text = rankString;    

        // Print score
        int score = scoreEntry.score;
        entryTransform.Find("Text_Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

        // Print name
        string name = scoreEntry.name;
        entryTransform.Find("Text_Name").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

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
        for (int i = highscores.scoreEntryList.Count - 1; i > 0 ; i--)
        {
            if (highscores.scoreEntryList[i].score > highscores.scoreEntryList[i-1].score)
            {
                //Swap
                ScoreEntry tmp = highscores.scoreEntryList[i];
                highscores.scoreEntryList[i] = highscores.scoreEntryList[i-1];
                highscores.scoreEntryList[i-1] = tmp;
            }
        }

        // Remove 11th entry
        if(highscores.scoreEntryList.Count > 10)
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
