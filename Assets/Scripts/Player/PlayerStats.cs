using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxHp;
    public float currentHp;
    public float armor;

    public float maxSprintEnergy;
    public float currentSprintEnergy;
    public float moveSpeed;
    public float sprintSpeedMultiplier;
    public float sprintUsageSpeed;

    private bool dead;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        dead = false;
        maxHp = 100;
        currentHp = maxHp;
    }

    public void takeDmg(float dmg)
    {
        if (dmg - armor < 1)
        {
            dmg = 1;
        }
        else
        {
            dmg -= armor;
        }

        currentHp -= dmg;

        if (currentHp <= 0)
        {
            dead = true;
        }
    }

    public void heal(float heal)
    {
        if (currentHp + heal > maxHp)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp += heal;
        }
    }
}
