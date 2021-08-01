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
    public float highscore;
    public bool saved;
    public bool checkings = true;


    // Start is called before the first frame update
    private void Start()
    {
        savedhighscore = new Highscore();
        saved = false;
        string json = File.ReadAllText(Application.dataPath + "/safeFile.json");
        savedhighscore = JsonUtility.FromJson<Highscore>(json);

        if (savedhighscore == null)
        {
            highscore = 0;
           // Debug.Log(savedhighscore);
            savedhighscore = new Highscore();
        }
        else
        {
            highscore = savedhighscore.savedscore;
            //Debug.Log(savedhighscore.savedscore);
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
               Debug.Log("Rewarding set to false")
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
            if (Score > highscore && saved == false)
            {
                saved = true;
                savedhighscore.savedscore = Score;
                string json = JsonUtility.ToJson(savedhighscore);
                File.WriteAllText(Application.dataPath + "/safeFile.json", json);
                Debug.Log(json);
            }
            deathMenu.ToggleEndMenu (Score);           
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

    public class Highscore
    {
        public float savedscore;
    }
}
