using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockspawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public int count;
    public GameObject[] rocks;
    public int i;
    public float tempPosition;
    public Transform playerPos;
    public float gameScore;
    public bool death;
    public bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;

        rocks = new GameObject[15];
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameScore = GameObject.Find("Player").GetComponent<score>().Score;
        death = GameObject.Find("Player").GetComponent<score>().checking;
        if (gameScore > 5 && spawned == false)
        {
            StartCoroutine(spawn());
            spawned = true;
        }

        if (death)
        {
            StopCoroutine(spawn());
            spawned = false;
        }
    }


    private IEnumerator spawn()
    {
        while (true)
        {
            for (i = 0; i < 10; i++)
            {
                if (rocks[i] == null)
                {
                    float randomX = UnityEngine.Random.Range(0.5f, 9.5f);
                    float timeRandom = UnityEngine.Random.Range(0.1f, 3f);

                    while (tempPosition == randomX)
                    {
                        randomX = UnityEngine.Random.Range(-4.5f, 4.5f);
                    }

                    Vector3 rockPosition = new Vector3(randomX, 1.6f, playerPos.position.z + 150f);
                    tempPosition = randomX;
                    rocks[i] = (GameObject)Instantiate(rockPrefab, rockPosition, Quaternion.identity);
                    Destroy(rocks[i], 6f);
                    yield return new WaitForSeconds(timeRandom);
                }
            }
        }
    }
}
