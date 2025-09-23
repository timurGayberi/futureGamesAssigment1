using UnityEngine;
using EnemyScripts;
using scriptableObjects;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Type and Scoring")]
    public EnemyType enemyType;
    public int scoreValue;

    [Header("Base Stats")]
    public float maxHealth = 10f;
    public float damage = 5f;
    public float moveSpeed = 2f;
    
    [Header("Ranged Only")]
    public float attackCooldown = 1f;
    public float fireRange = 10f;
    public WeaponData weaponData;
}