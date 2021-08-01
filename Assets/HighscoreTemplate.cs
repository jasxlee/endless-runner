using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighscoreTemplate : MonoBehaviour
{
    private Transform highscoreTemplate;
    private Transform highscoreContainer;
    private Highscore savedhighscore;

    // Start is called before the first frame update
    void Start()
    {
        highscoreContainer = transform.Find("HighscoreContainer");
        highscoreTemplate = highscoreContainer.FindGameObjectByTag("HighscoreTemplate");      
        //if (File.Exists(Application.dataPath + "/safeFile.json"))
        //{
        //    string json = File.ReadAllText(Application.dataPath + "/safeFile.json");
        //    savedhighscore = JsonUtility.FromJson<Highscore>(json);
        //}
        //else
        //{
        //    //Create a size 5 array when the JSON file is absent
        //    savedhighscore = new Highscore();
        //    savedhighscore.initialise();
        //}

        float templateHeight = 30f;

        for (int i = 0; i<5; i++)
        {
            Transform entryTransform = Instantiate(highscoreTemplate, highscoreContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
