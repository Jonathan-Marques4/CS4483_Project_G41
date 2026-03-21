using UnityEngine;

public class PlayerStats: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxHp;
    public float currentHp;
    public float armor;

    public float moveSpeed;
    public float sprintSpeed;
    public float maxSprintEnergy;
    public float currentSprintEnergy;

    private bool dead;
    void Start()
    {
        currentHp = maxHp;
        dead = false;
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
            Debug.Log("Dead");
        }
    }

    public float getCurrentHp()
    {
        return currentHp;
    }

    public void setCurrentHp(float hp)
    {
        currentHp = hp;
    }

    public float getMaxHp()
    {
        return maxHp;
    }

    public void setMaxHp(float hp)
    {
        maxHp = hp;
    }

    public float getArmor()
    {
        return armor;
    }

    public void setArmor(float armor)
    {
        this.armor = armor;
    }
}
