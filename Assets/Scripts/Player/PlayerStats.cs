using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public delegate void PlayerBreathEvent(GameObject target);

    [SerializeField]
    private int _breathMaxOffset = 10;
    [SerializeField]
    private float _breath = 0;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public event CharacterDeathEvent deathEvent;
    public event PlayerBreathEvent breathEvent;

    /// <summary>
    /// Adds to the breath, kills the player if breath is above max
    /// </summary>
    /// <param name="val">Breath to add</param>
    public void addBreathOrDie(float val)
    {
        _breath += val;

        if (_breath > _breathMaxOffset)
        {
            die(DeathReason.CHOKING);
        }
        invokeBreathNotNull();
    }

    /// <summary>
    /// Removes from the breath, kills the player if breath is below min
    /// </summary>
    /// <param name="val">Breath to remove</param>
    public void subBreathOrDie(float val)
    {
        _breath -= val;

        if (_breath <= (_breathMaxOffset * -1))
        {
            die(DeathReason.DROWNING);
        }
        invokeBreathNotNull();
    }

    /// <summary>
    /// Moves breath towards zero by the given value
    /// </summary>
    /// <param name="val">Amount to add/remove towards zero</param>
    public void moveBreathTowardsZero(float val)
    {
        if (_breath < 0)
        {
            _breath += val;
            if (_breath > 0)
            {
                _breath = 0;
            }
        } else if (_breath > 0)
        {
            _breath -= val;
            if (_breath < 0)
            {
                _breath = 0;
            }
        }
        invokeBreathNotNull();
    }

    /// <summary>
    /// Reset breath to zero
    /// </summary>
    public void resetBreath()
    {
        _breath = 0;
    }


    /// <summary>
    /// Triggers death of the player
    /// </summary>
    public override void die()
    {
        reset();
        if (deathEvent != null)
        {
            deathEvent.Invoke(gameObject, DeathReason.UNKNOWN);
        }
    }

    /// <summary>
    /// Triggers death of the player
    /// </summary>
    public override void die(DeathReason reason)
    {
        reset();
        if (deathEvent != null)
        {
            deathEvent.Invoke(gameObject, reason);
        }
    }

    public override void reset()
    {
        base.reset();
        _breath = 0;
    }

    private void invokeBreathNotNull()
    {
        if (breathEvent != null)
        {
            breathEvent.Invoke(gameObject);
        }
    }

}
