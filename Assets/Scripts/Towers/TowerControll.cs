using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControll : MonoBehaviour
{

    [SerializeField]
    float timeBetweenAttack;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    ProjectTile projectTile;
    Enemy targetEnemy = null;
    float attackCounter;

    

    bool isAttacking = false;
    void Update()
    {
        attackCounter -= Time.deltaTime;
        if (targetEnemy == null || targetEnemy.IsDead)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius)
            {
                targetEnemy = nearestEnemy;
            }
        }
        else
        {
            if (attackCounter <= 0)
            {
                isAttacking = true;

                attackCounter = timeBetweenAttack;
            }
            else
                isAttacking = false;

            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
            {
                targetEnemy = null;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isAttacking == true)
        {
            Attack();
        }
    }

    public void Attack()
    {
        isAttacking = false;

        ProjectTile newProjectTile = Instantiate(projectTile) as ProjectTile;

        newProjectTile.transform.localPosition = transform.localPosition;
        if (newProjectTile.PType == projectTileType.arrow)
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Arrow);
        }
        else if(newProjectTile.PType == projectTileType.fireball)
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.FireBall);

        }
        else if (newProjectTile.PType == projectTileType.rock)
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Rock);

        }

        if (targetEnemy == null)
        {
            Destroy(newProjectTile);
        }
        else
        {
            StartCoroutine(MoveProjectTile(newProjectTile));
        }
    }

    IEnumerator MoveProjectTile(ProjectTile projecttile)
    {
        while(GetTargetDistance(targetEnemy) > 0.20f && projecttile != null && targetEnemy != null)
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projecttile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projecttile.transform.localPosition = Vector2.MoveTowards(projecttile.transform.localPosition, targetEnemy.transform.localPosition, 5f* Time.deltaTime);
            yield return null;
        }

        if (projecttile != null || targetEnemy == null)
        {
            Destroy(projecttile);
        }
    }

    private float GetTargetDistance(Enemy thisEnemy)
    {
        if (thisEnemy == null)
        {
            thisEnemy = GetNearestEnemy();
            if (thisEnemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }
    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange;
    }

    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smallestDistence = float.PositiveInfinity; // Воспринимает на максимальном значении

        foreach (Enemy enemy in GetEnemiesInRange())
        {
            if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistence)
            {
                smallestDistence = Vector2.Distance(transform.position, enemy.transform.position);
                nearestEnemy = enemy;
            }

           
        }
        return nearestEnemy;
    }
}
