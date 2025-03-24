using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    apple,
    banana,
    cherry,
    kiwi,
    melon,
    orange,
    pineapple,
    strawberry
}
public class Fruit : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public FruitType myFruitType;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] fruitImage;
    private bool isCollecting;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!isCollecting)
            {
                AudioManager.instance.PlaySFX_Fruit();
                PlayerManager.instance.fruits++;
                anim.SetTrigger("Collect");
                isCollecting = true;
            }
        }
    }

    public void SetupFruit(int fruitIndex)
    {
        anim = GetComponent<Animator>();
        isCollecting = false;
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(fruitIndex, 1);
    }

    //private void OnValidate()
    //{
    //    sr.sprite = fruitImage[((int)myFruitType)];
    //}

    private void collectFruit()
    {
        Destroy(gameObject);
    }
}
