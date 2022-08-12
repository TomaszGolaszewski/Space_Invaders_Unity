using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public int no_of_games;
    public int[] table_of_scores;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
        SaveStats();
    }

    public void SaveStats()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadStats()
    {
        StatsData data = SaveSystem.LoadData();

        no_of_games = data.no_of_games;
        table_of_scores = data.table_of_scores; //.Copy();
    }

    public void Reset()
    {
        no_of_games = 0;
        for (int i = 0; i < 10; i++) table_of_scores[i] = 0;
    }
}

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

 /*   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
