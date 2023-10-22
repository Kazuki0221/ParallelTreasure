using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] int _price;
    public int Price => _price;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
            gameContoroller.AddTreasure(gameObject);
            gameObject.SetActive(false);
        }
    }
}
