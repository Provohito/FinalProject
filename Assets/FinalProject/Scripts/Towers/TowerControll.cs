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
                isAttacking = true;  // Установка флага, что стрельба разрешена
                Debug.Log("1");
                Attack();
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
    public void Attack()
    {
        isAttacking = false;
        Debug.Log("!");

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
            Debug.Log("Destroy");
            Destroy(newProjectTile);
        }
        else
        {
            Debug.Log("Shoot");
            StartCoroutine(MoveProjectTile(newProjectTile)); // непосредственно сама стрельба(перемещение префаба)
        }
    }

    IEnumerator MoveProjectTile(ProjectTile projectTile)
    {
        while(GetTargetDistance(targetEnemy) < attackRadius && projectTile != null && targetEnemy != null) // 
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectTile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectTile.transform.localPosition = Vector2.MoveTowards(projectTile.transform.localPosition, targetEnemy.transform.localPosition, 5f* Time.deltaTime); // Поворачиваем и выпускаем наш снаряд
            yield return null;
        }

        if (projectTile != null || targetEnemy == null)
        {
            Destroy(projectTile);
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
