using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.2f;
    private bool isMoving = false;
    private Vector2 input;
    private Vector3 targetPos;
    private Vector2 lookDirection = Vector2.down; // 기본 방향

    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                lookDirection = input;
                targetPos = transform.position + new Vector3(input.x, input.y, 0);
                StartCoroutine(Move());
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryAttack();
            }
        }
    }

    System.Collections.IEnumerator Move()
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    void TryAttack()
    {
        Vector2 attackPos = (Vector2)transform.position + lookDirection;

        Collider2D hit = Physics2D.OverlapCircle(attackPos, 0.1f);
        if (hit != null)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // 데미지는 1로 고정
            }
        }
    }
}