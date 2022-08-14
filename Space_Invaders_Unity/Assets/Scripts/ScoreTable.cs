using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    public TextMeshProUGUI Games_count;

    private void Awake()
    {
        entryContainer = transform.Find("ScoreEntryContainer");
        entryTemplate = entryContainer.Find("ScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 80f;
        for (int i = 0; i < 10; i++)
        {     
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            //Debug.Log("test4");

            int rank = i + 1;
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

            int score = Random.Range(0, 10000);
            entryTransform.Find("Text_Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = "AAA";
            entryTransform.Find("Text_Name").GetComponent<TextMeshProUGUI>().text = name;
        }

        
        int gamesCounter = Random.Range(0, 10000);
        Games_count.text = gamesCounter.ToString();
        //PlayerPrefs.SetInt("Games_count", number);
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
