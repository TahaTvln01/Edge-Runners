using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Transform[] fruitPositions;
    [SerializeField] private GameObject fruitPrefab;
    
    void Start()
    {
        fruitPositions = GetComponentsInChildren<Transform>();
        
        for (int i = 1; i < fruitPositions.Length; i++)
        {
            GameObject newFruit = Instantiate(fruitPrefab, fruitPositions[i]);

            int rand = Random.Range(0, 8);
            newFruit.GetComponent<Fruit>().SetupFruit(rand);

            fruitPositions[i].GetComponent<SpriteRenderer>().sprite = null;
        }

        GameManager.instance.fruitAmount = fruitPositions.Length-1;
    }
}
