using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class score : MonoBehaviour
{
    public float Score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 16;
    private int scoreToNextLevel = 16;
    public Text scoreText;
    public DeathMenu deathMenu;
    public bool checking = false;
    public Highscore savedhighscore;
    public bool saved;
    public bool checkings = true;


    // Start is called before the first frame update
    private void Start()
    {
        //Load the Highscore data, else create if the file cannot be found
        saved = false;

        if (File.Exists(Application.dataPath + "/safeFile.json"))
        {

            string json = File.ReadAllText(Application.dataPath + "/safeFile.json");
            savedhighscore = JsonUtility.FromJson<Highscore>(json);

        }
        else
        {
            //Create a size 5 array when the JSON file is absent
            savedhighscore = new Highscore();
            savedhighscore.initialise();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Score >= scoreToNextLevel))
        {
            LevelUp();
        }
        checkings = GetComponent<MovePlayer>().rewarding;
           switch(checkings)
           {
               case true:
               Score = Score + 15.0f;
               Debug.Log("SCORE ADDED");
               checkings = false;
               GetComponent<MovePlayer>().rewarding = false ;
                Debug.Log("Rewarding set to false");
               break;
              
               case false:
               checkings = false;
               break;

           }
           
       
        checking = GetComponent<MovePlayer>().isdead ;
        switch (checking)
        {
            case false:
                Score += Time.deltaTime * difficultyLevel;
                scoreText.text = ((int)Score).ToString();
                break;

            case true:
                deathMenu.ToggleEndMenu(Score);
                if (saved == false)
                {
                    saveHighscoreList(Score);
                }
                break;


        }
        /* if (GetComponent<PlayerController>().isdead==false)
         {
             Score += Time.deltaTime * difficultyLevel;
             scoreText.text = ((int)Score).ToString();

         }
        if (GetComponent<PlayerController>().isdead== true)
         {
             deathMenu.ToggleEndMenu (Score);
         }*/

    }
    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }
         scoreToNextLevel *= 2;
         difficultyLevel++;
         GetComponent<MovePlayer>().SetSpeed(difficultyLevel);
         Debug.Log (difficultyLevel);
     }

    void saveHighscoreList(float highscore)
    {
        int lowestscore = -1;
        float tempscore = -1;
        saved = true;

        //look for lowest value in array
        for (int i = 0; i < 5; i++)
        {
            if (tempscore == -1)//assign first loop value to temp
            {
                tempscore = savedhighscore.highscorelist[i];
                lowestscore = i;
            }
            else
            {
                if (tempscore > savedhighscore.highscorelist[i])//assign the lowest score to temp
                {
                    tempscore = savedhighscore.highscorelist[i];
                    lowestscore = i;
                }
            }
        }

        if (highscore > tempscore)
        {
            savedhighscore.highscorelist[lowestscore] = highscore;
            string json = JsonUtility.ToJson(savedhighscore);
            File.WriteAllText(Application.dataPath + "/safeFile.json", json);
        }
    }
}
