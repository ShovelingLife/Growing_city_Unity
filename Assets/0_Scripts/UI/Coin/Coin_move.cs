using UnityEngine;
using System.Collections;

public class Coin_move : MonoBehaviour
{
    float speed = 2f;

    private void OnEnable()
    {
        transform.position = new Vector2(0.5f, 5.5f);
    }
    private void Update()
    {
        StartCoroutine("destroyCoin");
        this.transform.Translate(new Vector2(0, 4f) * speed * Time.deltaTime);
    }

    // 동전을 자동으로 소멸시켜주는 코루틴, 큐에다가 삽입
    IEnumerator destroyCoin()
    {
            yield return new WaitForSeconds(1f);
            Object_pooling_manager.instance.InsertQueue_Coin(gameObject);
    }
}