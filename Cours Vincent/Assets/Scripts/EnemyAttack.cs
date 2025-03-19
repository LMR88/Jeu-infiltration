using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int damage = 1;
    private Transform player;
    private Transform Enemy;

    public GameObject attackCube;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        attackCube.SetActive(true);
    }
}