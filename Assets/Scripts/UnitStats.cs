using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStats : MonoBehaviour
{
    public int maxHP;
    public int damage;
    public float attackSpeed; 
    public int speed;
    public int lifeTime;
    public int cost;
    public int manacost;
    public int countOfUnits;

    public event Action OnDeath;

    private int currentHP;
    private Slider hpSlider;
    private void Start()
    {        
        hpSlider = GetComponentInChildren<Slider>();
        currentHP = maxHP;
        hpSlider.value = (float)currentHP / (float) maxHP;
    }
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        hpSlider.value = (float)currentHP / (float)maxHP;
        if(currentHP == 0)
        {
            OnDeath?.Invoke();
        }
        
    }
}
