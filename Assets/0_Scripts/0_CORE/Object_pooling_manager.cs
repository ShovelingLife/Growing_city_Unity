using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pooling_manager : Singleton_local<Object_pooling_manager>
{
    public GameObject coin_Prefab = null;
    //public GameObject xMas_Girl_Prefab;
    public Queue<GameObject> coin_Queue = new Queue<GameObject>();
    //public Queue<GameObject> xmas_Girl_Queue = new Queue<GameObject>();


    void Start()
    {
        Init_values();
    }

    // Variables initialization
    void Init_values()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject t_Coin = Instantiate(coin_Prefab, Vector2.zero, Quaternion.identity);
            coin_Queue.Enqueue(t_Coin);
            t_Coin.SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            //GameObject t_Girl = Instantiate(xMas_Girl_Prefab, Vector2.zero, Quaternion.identity);
            //xmas_Girl_Queue.Enqueue(t_Girl);
            //t_Girl.SetActive(false);
        }
    }

    // 동전 생성해주는 오브젝트를 큐에 삽입함
    public void Insert_queue_coin(GameObject _coin)
    {
        coin_Queue.Enqueue(_coin);
        _coin.SetActive(false);
    }  

    // 동전 생성해주는 오브젝트를 큐에서 꺼내옴
    public GameObject Get_queue_coin()
    {
        GameObject coin = coin_Queue.Dequeue();
        coin.SetActive(true);
        return coin;
    }
}