using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Function: Monster
// Name: Myungsub Eum
// Number: 101168160
// Last Uopdate: 20201207
// Description: monster search the land and attack player.

public class Monster : MonoBehaviour
{
    public int nextMove;
    public GameObject playerObj;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;
    Vector2 startPosition;
    BoxCollider2D coll;
    bool canMove = true;
    Player player;
    float distancePlayer = 0.0f;
    bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        startPosition = transform.position;
        coll = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();
        isRight = true;
        anim.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        distancePlayer = playerObj.transform.position.x - transform.position.x;
        Vector2 frontVec;

        if (isRight)
            frontVec = new Vector2(rigid.position.x - 0.6f, rigid.position.y);
        else
            frontVec = new Vector2(rigid.position.x + 0.6f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down * 2, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            if (isRight)
                isRight = false;
            else
                isRight = true;
        }
        else if (rayHit.collider != null)
        {
            canMove = true;
        }
    }

    void FixedUpdate()
    {
        BehaviorControl();
    }

    void BehaviorControl()
    {
        if (isRight && canMove)
        {
            rigid.velocity = new Vector2(-1, rigid.velocity.y);
            anim.SetBool("IsWalking", true);
            sprite.flipX = false;
        }
        else if (!isRight && canMove)
        {
            rigid.velocity = new Vector2(1, rigid.velocity.y);
            anim.SetBool("IsWalking", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("IsWalking", false);
            canMove = false;
        }
    }

    public void OnDamaged()
    {
        sprite.color = new Color(1, 1, 1, 0.4f);
        sprite.flipY = true;
        coll.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("Death", 0.5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttack"))
        {
            OnDamaged();
            player.OnAttack();
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
    }
}
