using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Holds static methods that return a reference to different important gameplay objects
/// WARNING: These are all VERY EXPENSIVE to call. OMLY USE FOR INIT.
/// 
/// Do not instantiate anything from here, this class should only contain methods to easily and predticably grab references to important game objects.
/// 
/// The GameManager is cached because it should NEVER be destroyed,
/// Player is not safe to cache so don't. Shouldn't matter anyhow.
/// </summary>
public class StaticReference
{
    public static string GAME_MANAGER_OBJECT_NAME = "GameManager";
    public static string PLAYER_OBJECT_NAME = "Character";
    public static string DEATH_MENU = "DeathMenu";
    public static string VICTORY_MENU = "VictoryMenu";
    public static string INGAME_UI = "Canvas";
    public static string TILEMAP_GROUND_NAME = "Ground";
    public static string TILEMAP_WATER_NAME = "Water";

    public static GameManager GAME_MANAGER_INSTANCE = null;

    public static GameManager GameManager()
    {
        GameManager manager = GAME_MANAGER_INSTANCE == null ? GetTargetComponentByName<GameManager>(GAME_MANAGER_OBJECT_NAME) : GAME_MANAGER_INSTANCE;
        GAME_MANAGER_INSTANCE = manager;
        return manager;
    }

    public static PlayerController PlayerController()
    {
        return GetTargetComponentByName<PlayerController>(PLAYER_OBJECT_NAME);
    }

    public static PlayerStats PlayerStats()
    {
        return GetTargetComponentByName<PlayerStats>(PLAYER_OBJECT_NAME);
    }

    public static Character PlayerCharacter()
    {
        return GetTargetComponentByName<Character>(PLAYER_OBJECT_NAME);
    }

    public static GameObject DeathMenu()
    {
        return GetTargetComponentByName<Canvas>(DEATH_MENU).gameObject;
    }

    public static GameObject UIIngame()
    {
        return GetTargetComponentByName<Canvas>(INGAME_UI).gameObject;
    }

    public static GameObject VictoryMenu()
    {
        return GetTargetComponentByName<Canvas>(VICTORY_MENU).gameObject;
    }

    public static Tilemap Ground()
    {
        return GetTargetComponentByName<Tilemap>(TILEMAP_GROUND_NAME);
    }

    public static Tilemap Water()
    {
        return GetTargetComponentByName<Tilemap>(TILEMAP_WATER_NAME);
    }


    /// <summary>
    /// Gets a component by gameobject name. Throws exception if it is missing
    /// </summary>
    /// <typeparam name="T">The Component class</typeparam>
    /// <param name="referenceName">The name of the target gameobject</param>
    /// <exception cref="GameObjectReferenceNotFound">Thrown if no GameObject by that name excist</exception>
    /// <exception cref="ComponentReferenceNotFound">Thrown if a component is missing</exception>
    /// <returns>A component of class T</returns>
    private static T GetTargetComponentByName<T>(string referenceName)
    {
        GameObject target = GameObject.Find(referenceName);
        if (target == null)
        {
            throw new GameObjectReferenceNotFound(referenceName);
        }

        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new ComponentReferenceNotFound(referenceName, typeof(T).ToString());
        }

        return component;
    }

    public class GameObjectReferenceNotFound : UnityException
    {
        public GameObjectReferenceNotFound(string refrenceName) : base("GameObject does not excist in the scene: " + refrenceName) {}
    }

    public class ComponentReferenceNotFound : UnityException
    {
        public ComponentReferenceNotFound(string refrenceName, string className) : base("GameObject " + refrenceName + " missing component of class " + className) { }
    }
}
