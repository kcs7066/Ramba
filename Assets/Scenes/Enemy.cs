using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 3;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"���� ü��: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("���Ͱ� �׾����ϴ�!");
        Destroy(gameObject);
    }
}
