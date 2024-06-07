using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isFriendly;
    [HideInInspector] public Vector3 moveDirection;

    private UnitState state = UnitState.Moving;
    private UnitStats stats;
    private Animator animator;
    private Rigidbody2D rb;
    private float attackTimer;
    private Collider2D collider;
    private HashSet<Unit> availableTargets = new HashSet<Unit>(); 
    private void Awake()
    {
        moveDirection = isFriendly ? Vector3.right : Vector3.left;
        if(moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        stats = GetComponent<UnitStats>();
    }
    private void OnEnable()
    {
        stats.OnDeath += Death;
    }
    private void OnDisable()
    {
        stats.OnDeath -= Death;
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        attackTimer += Time.deltaTime;
    }
    private void Move()
    {
        if (state == UnitState.Dying)
            return;
        state = availableTargets.Count > 0?UnitState.Attacking : UnitState.Moving;
        if (state != UnitState.Moving)
            return;
        rb.MovePosition(transform.position + moveDirection * stats.speed * Time.fixedDeltaTime);
    }
    private void Death()
    {
        state = UnitState.Dying;
        animator.SetTrigger("Death");
        collider.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit))
        {
            if (isFriendly != unit.isFriendly)
            {
                availableTargets.Add(unit);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit))
        {
            if (isFriendly != unit.isFriendly)
            {
                availableTargets.Remove(unit);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit))
        {
            if (isFriendly == unit.isFriendly)
                return;
            if(attackTimer >= stats.attackSpeed && state == UnitState.Attacking)
            {
                attackTimer = 0;
                animator.SetTrigger("Attack");
                unit.GetComponent<UnitStats>().TakeDamage(stats.damage);
            }
        }
    }
    
}
