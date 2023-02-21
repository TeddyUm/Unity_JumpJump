using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRender;
    Animator anim;
    BoxCollider2D coll;
    public Image[] UIHealth;
    bool canMove = true;
    public GameObject respawnPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        SoundManager.instance.Play("Stage");
    }

    void Update()
    {
        // animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("IsJumping") && canMove)
        {
            SoundManager.instance.Play("Jump");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", true);
        }
    }

    public void Death()
    {
        spriteRender.color = new Color(1, 1, 1, 0.4f);
        spriteRender.flipY = true;
        coll.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        canMove = false;

    }
    public void GameOver()
    {
        SoundManager.instance.Stop("Stage");
        GameManager.Instance.SceneChange("GameOver");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
                OnAttack();
            else
                OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            SoundManager.instance.Play("Coin");
            collision.gameObject.SetActive(false);
            GameManager.Instance.stagePoint += 100;
        }
        if (collision.tag == "Finish" && !GameManager.Instance.stageClear)
            GameManager.Instance.NextStage();
        if (collision.tag == "Respawn")
        {
            GameManager.Instance.playerHP--;
            if (GameManager.Instance.playerHP > 0)
            {
                SoundManager.instance.Play("Damage");
                transform.position = respawnPos.transform.position;
                UIHealth[GameManager.Instance.playerHP].color = new Color(1, 1, 1, 0.3f);
            }
            else
            {
                Invoke("GameOver", 0.3f);
            }
        }
    }

    public void OnAttack()
    {
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        GameManager.Instance.stagePoint += 100;
    }
    public void OnDamaged(Vector2 targetPos)
    {

        SoundManager.instance.Play("Damage");
        gameObject.layer = 11;
        spriteRender.color = new Color(1, 0, 0, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);

        GameManager.Instance.playerHP--;
        UIHealth[GameManager.Instance.playerHP].color = new Color(1, 1, 1, 0.3f);
        if (GameManager.Instance.playerHP <= 0)
            Death();

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRender.color = new Color(1, 1, 1, 1.0f);
    }
    private void FixedUpdate()
    {
        // stop speed
        if (Input.GetButtonUp("Horizontal") && canMove)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Move speed 
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // flip
        if (Input.GetAxisRaw("Horizontal") < 0)
            spriteRender.flipX = true;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            spriteRender.flipX = false;

        // Max speed
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxSpeed)
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }

        // Landing platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down * 2, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 2, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                    anim.SetBool("IsJumping", false);
            }
        }
    }
}
