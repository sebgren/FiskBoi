using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    public delegate void CharacterDamgeEvent(GameObject target);
    public delegate void CharacterDeathEvent(GameObject target, DeathReason reson);

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
            damageEvent.Invoke(gameObject);
        }

        if (this._health <= 0)
        {
            die(DeathReason.KILLED);
        }
    }

    /// <summary>
    /// Triggers death of the NPC
    /// </summary>
    public virtual void die(DeathReason reason)
    {
        if (deathEvent != null)
        {
            deathEvent.Invoke(gameObject, reason);
        }
    }

    /// <summary>
    /// Triggers death of the NPC
    /// </summary>
    public virtual void die()
    {
        if (deathEvent != null)
        {
            deathEvent.Invoke(gameObject, DeathReason.UNKNOWN);
        }
    }

    public virtual void reset()
    {
        _health = _healthMax;
    }
}

public enum DeathReason
{
    UNKNOWN,
    DROWNING,
    CHOKING,
    KILLED
}
