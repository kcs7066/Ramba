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
        Debug.Log($"몬스터 체력: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("몬스터가 죽었습니다!");
        Destroy(gameObject);
    }
}
