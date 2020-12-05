using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomPatrol : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    public float secondsToMaxDifficulty;
    public float minSpeed;
    public float maxSpeed;

    float speed;

    Vector2 targetPoisition;

    // Start is called before the first frame update
    void Start()
    {
        targetPoisition = GetRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position != targetPoisition)
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercent());
            transform.position = Vector2.MoveTowards(transform.position, targetPoisition, speed * Time.deltaTime);
        }
        else
        {
            targetPoisition = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Planet") // hit the planet
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.tag == "Boundary")
        {

        }
    }

    float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
