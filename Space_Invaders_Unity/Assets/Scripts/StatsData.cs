using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData : MonoBehaviour
{
    public int no_of_games;
    public int[] table_of_scores;

    public StatsData (Stats stats)
    {
        no_of_games = stats.no_of_games;
        table_of_scores = stats.table_of_scores; //.Copy();
    }
}
