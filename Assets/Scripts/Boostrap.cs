using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates all management-level GameObjects required in a level
/// Does not initiate the player character, enemies or anything else position dependent.
/// </summary>
public class Boostrap : MonoBehaviour
{

    public static string PREFAB_GAME_MANAGER = "Prefabs/GameManager";
    public static string PREFAB_DEATH_MENU = "Prefabs/UI/DeathCanvas";
    public static string PREFAB_VICTORY_MENU = "Prefabs/UI/VictoryCanvas";
    public static string PREFAB_UI = "Prefabs/UI/Canvas";
    public static string MAIN_CAMERA = "Prefabs/MainCamera";

    public int nextLevelIndex = 0;

    public List<string> requiredGameobjects = new List<string>() { "Character", "Goal", "Ground", "Water" };

    // Start is called before the first frame update
    void Start()
    {
        CheckRequired();
        InitCamera();
        InitGameManager();
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitGameManager()
    {
        GameManager manager;
        try
        {
            manager = StaticReference.GameManager();
        }
        catch (StaticReference.GameObjectReferenceNotFound)
        {
            manager = (Instantiate(Resources.Load(PREFAB_GAME_MANAGER) as GameObject).GetComponent<GameManager>());
            StaticReference.GAME_MANAGER_INSTANCE = manager;
            manager.name = StaticReference.GAME_MANAGER_OBJECT_NAME;
        }

        try
        {
            manager.deathMenu = StaticReference.DeathMenu();
        }
        catch (StaticReference.GameObjectReferenceNotFound)
        {
            manager.deathMenu = (GameObject)Instantiate(Resources.Load(PREFAB_DEATH_MENU));
            manager.deathMenu.name = StaticReference.DEATH_MENU;
        }

        try
        {
            manager.victoryMenu = StaticReference.VictoryMenu();
        }
        catch (StaticReference.GameObjectReferenceNotFound)
        {
            manager.victoryMenu = (GameObject)Instantiate(Resources.Load(PREFAB_VICTORY_MENU));
            manager.victoryMenu.name = StaticReference.VICTORY_MENU;
        }
        manager.victoryMenu.GetComponentInChildren<LoadSceneOnClick>().indexOverride = nextLevelIndex;
    }

    private void InitCamera()
    {
        Instantiate(Resources.Load(MAIN_CAMERA));
    }

    private void InitUI()
    {
        try
        {
            StaticReference.UIIngame();
        }
        catch(StaticReference.GameObjectReferenceNotFound)
        {
            GameObject ui = (GameObject)Instantiate(Resources.Load(PREFAB_UI));
            ui.name = StaticReference.INGAME_UI;
        }
    }

    private void CheckRequired()
    {
        foreach(string name in requiredGameobjects)
        {
            GameObject obj = GameObject.Find(name);
            if (obj == null)
            {
                throw new MissingReferenceException(name);
            }
        }
    }

    public class MissingRequiredGameObjectException : UnityException
    {
        public MissingRequiredGameObjectException(string name) : base("Missing required GameObject named " + name) { }
    }
}
