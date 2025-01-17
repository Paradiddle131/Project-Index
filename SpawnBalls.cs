﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public float boundaryX;
    public float boundaryY;

    public Transform SpawnPosition;
    public GameObject Tom;
    [SerializeField] public int numberOfToms;
    [SerializeField] public float radius;

    public double score = 0;

    Dictionary<System.DateTime, GameObject> toms = new Dictionary<System.DateTime, GameObject>();
    System.DateTime timeTurnedGreen;

    Vector2 GetRandomPoint()
    {
        float randomX = Random.Range(-boundaryX, boundaryX);
        float randomY = Random.Range(-boundaryY, boundaryY);
        return new Vector2(randomX, randomY);
    }

    void SpawnObject(GameObject go, int amount)
    {
        if (go == null) return;
        int countSpawned = 0;
        int numIterated = 0;
        do
        {
            Vector2 randomPoint = GetRandomPoint();
            if (toms.Count > 0 || Physics2D.OverlapCircle(randomPoint, radius))
            {
                numIterated++;
                if (numIterated == amount * 2)
                {
                    break;
                }
                continue;
            }
            GameObject tom;
            tom = Instantiate(go);
            tom.transform.position = randomPoint;
            toms.Add(System.DateTime.Now, tom);
            System.Threading.Thread.Sleep(100);
            countSpawned++;
        } while (countSpawned != amount);
    }

    void Start()
    {
        initializeTomAsRed();
        //SpawnObject(Tom, numberOfToms);
    }

    void initializeTomAsRed()
    {
        SpriteRenderer renderer = Tom.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 0f, 0f, 1f);
    }

    void TurnTomToGreen(GameObject go)
    {
        Color colorGreen = new Color(0f, 1f, 0f, 1f);
        if (go.GetComponent<SpriteRenderer>().color != colorGreen)
        {
            SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
            renderer.color = colorGreen;
            timeTurnedGreen = System.DateTime.Now;
        }
    }

    void Update()
    {
        if (!Tom) Tom = new GameObject();
        SpawnObject(Tom, 1);
        double timeToDestroy = Random.Range(0.75f, 6.0f);
        if (System.DateTime.Now > toms.ElementAt(0).Key.AddSeconds(timeToDestroy))
        {
            TurnTomToGreen(toms.ElementAt(0).Value);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
            foreach (RaycastHit2D hit in hits)
            {
                GameObject clickedTom = hit.collider.gameObject;
                if (clickedTom.tag == "Tom" && clickedTom.GetComponent<SpriteRenderer>().color == new Color(0f, 1f, 0f, 1f))
                {
                    Destroy(clickedTom);
                    toms.Remove(toms.ElementAt(0).Key);
                    score += 1000 / (System.DateTime.Now - timeTurnedGreen).TotalMilliseconds;
                    Debug.Log("SCORE: " + score.ToString());
                }
            }
        }
    }
}
