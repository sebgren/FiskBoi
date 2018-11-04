using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    public delegate void CharacterDamgeEvent();
    public delegate void CharacterDeathEvent();

    [SerializeField]
    private int _health = 10;
    [SerializeField]
    private int _healthMax = 10;

    public event CharacterDamgeEvent damageEvent;
    public event CharacterDeathEvent deathEvent;


    /// <summary>
    /// Hurt the plyer/npc by the given damage
    /// </summary>
    /// <param name="dmg">Amount to damage by</param>
    public void hurt(int dmg)
    {
        this._health -= dmg;
        
        if (damageEvent != null)
        {
            damageEvent.Invoke();
        }

        if (this._health <= 0)
        {
            die();
        }
    }

    /// <summary>
    /// Triggers death of the NPC
    /// </summary>
    public virtual void die()
    {
        if (deathEvent != null)
        {
            deathEvent.Invoke();
        }
    }

    public virtual void reset()
    {
        _health = _healthMax;
    }
}
