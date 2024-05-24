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
    private void Start()
    {
        moveDirection = isFriendly ? Vector3.right : Vector3.left;
        if(moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        rb = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        stats = GetComponent<UnitStats>();
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
        if (state != UnitState.Moving)
            return;
        rb.MovePosition(transform.position + moveDirection * stats.speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit))
        {
            if (isFriendly != unit.isFriendly)
            {
                state = UnitState.Attacking;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit))
        {
            if (isFriendly == unit.isFriendly)
                return;
            if(attackTimer >= stats.attackSpeed)
            {
                attackTimer = 0;
                animator.SetTrigger("Attack");
                unit.GetComponent<UnitStats>().TakeDamage(stats.damage);
            }
        }
    }
    
}
