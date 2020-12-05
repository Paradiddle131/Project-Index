using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public float boundaryX;
    public float boundaryY;

    public Transform SpawnPosition;
    public GameObject Tom;
    [SerializeField] public int numberOfToms;
    [SerializeField] public float radius;

    Dictionary<System.DateTime, GameObject> toms = new Dictionary<System.DateTime, GameObject>();

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
            System.Threading.Thread.Sleep(100);
            Vector2 randomPoint = GetRandomPoint();
            if (Physics2D.OverlapCircle(randomPoint, radius))
            {
                Debug.Log("COLLIDED ON: " + randomPoint);
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
            countSpawned++;
            //TurnTomToGreen(tom);
            TurnTomToGreen(go);
        } while (countSpawned != amount);
    }
    void Start()
    {
        //SpawnObject(Tom, numberOfToms);
    }

    void TurnTomToGreen(GameObject go)
    {
        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        renderer.color = new Color(0f, 1f, 0f, 1f);
    }

    void Update()
    {
        SpawnObject(Tom, 1);
    }
}
