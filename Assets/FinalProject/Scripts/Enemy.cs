using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    Transform exit;
    List<Transform> wayPoints = new List<Transform>(); 
    [SerializeField]
    float navigation;
    [SerializeField]
    int health;
    [SerializeField]
    int rewardAmount;


    int target = 0;
    Transform enemy;
    Collider2D enemyCollider;
    Animator anim;
    float navigationTime = 0;
    bool isDead = false;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    void Start()
    {
        GameObject[] countMoving = GameObject.FindGameObjectsWithTag("MovingPoint");
        exit = GameObject.FindGameObjectWithTag("Finish").transform;
        for (int i = 0; i < countMoving.Length; i++)
        {
            wayPoints.Add(countMoving[i].transform);
        }
        enemy = GetComponent<Transform>();
        GameManager.Instance.RegisterEnemy(this);
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
       
    }

    void Update()
    {
        if (wayPoints != null && isDead == false)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigation)
            {
                if (target < wayPoints.Count)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MovingPoint")
        {
            target += 1;
        }
        else if (collision.tag == "Finish")
        {
            GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.UnregisterEnemy(this);
            GameManager.Instance.IsWaveOver();
        }
        else if (collision.tag == "ProjectTile")
        {
            ProjectTile newP = collision.gameObject.GetComponent<ProjectTile>();
            EnemyHit(newP.AttackDamage);
            Destroy(collision.gameObject);
        }
    }

    public void EnemyHit(int hitPoints)
    {
        if (health - hitPoints > 0)
        {
            health -= hitPoints;
            //hurt
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
            anim.Play("Hurt");
        }
        else
        {
            anim.SetTrigger("didDie");
            Die();
        }
        
    }
    public void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
        GameManager.Instance.TotalKilled += 1;
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
        GameManager.Instance.addMoney(rewardAmount);
        GameManager.Instance.IsWaveOver();
    }
}
