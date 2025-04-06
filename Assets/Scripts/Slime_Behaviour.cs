using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Slime_Behaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float Jumptimer;
    public float JumpTimeMax;
    int randomCount = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jumptimer = JumpTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        SlimeTimer();
           
    }
    private void SlimeTimer()
    {
        Jumptimer -= Time.deltaTime;
        if (Jumptimer < 0)
        {
            randomCount = Random.Range(0, 8);
            Debug.Log(randomCount);
            Jumptimer = JumpTimeMax;
            SlimeMove();
        }

    }
    private void SlimeMove()
    {
        Vector2 startPos = transform.position;
        Vector2 targetPos = startPos + (new Vector2(2f, 2f));
        float t = Mathf.Clamp01(SlimeTimer / 1f);
        transform.position = Vector3.Lerp(startPos, targetPos, 1f);
    }

}
