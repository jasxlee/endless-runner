using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore
    {
        public float[] highscorelist;

        public void initialise()
        {
            highscorelist = new float[5];
            for (int i = 0; i < 5; i++)
            {
                highscorelist[i] = 0;
            }
        }
    }