using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.2f; // �̵� �ӵ�
    private bool isMoving = false; // �����̴� ���ΰ�?
    private Vector2 input;
    private Vector3 targetPos;
    private Vector2 lookDirection = Vector2.down; // �⺻ ����

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.W)) input = Vector2.up;
            else if (Input.GetKey(KeyCode.S)) input = Vector2.down;
            else if (Input.GetKey(KeyCode.A)) input = Vector2.left;
            else if (Input.GetKey(KeyCode.D)) input = Vector2.right;
            else input = Vector2.zero;

            // if (input.x != 0) input.y = 0; // �밢�� �̵� ����

            if (input != Vector2.zero) // �Է��� �־��ٸ� ��������
            {
                lookDirection = input; // ���� ���� = �Է¹���
                targetPos = transform.position + new Vector3(input.x * 0.5f, input.y * 0.5f, 0); //������ ����
                StartCoroutine(Move()); // �����δ�
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
        Vector2 attackPos = (Vector2)transform.position + (lookDirection/2);

        Collider2D hit = Physics2D.OverlapCircle(attackPos, 0.1f);
        if (hit != null)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // �������� 1�� ����
            }
        }
    }
}