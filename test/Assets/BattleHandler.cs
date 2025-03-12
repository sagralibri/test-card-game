using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleHandler : MonoBehaviour
{
    
    private manager manager;
    public Camera cam;
    public Entity player;
    public GameObject e1;
    public GameObject e2;
    public GameObject e3;
    public GameObject e4;
    public GameObject playerObject;
    public GameObject a1;
    public GameObject a2;
    public GameObject a3;
    public GameObject currentActor;
    public Turn currentActingSide = Turn.ALLY;
    public int turnCount = 0;
    public GameObject lastActed;
    public GameObject enemyOverlayPrefab;
    public List<GameObject> validActors = new List<GameObject>();
    public List<GameObject> validActorsInitial = new List<GameObject>();
    public List<GameObject> toRemove1 = new List<GameObject>();
    public List<GameObject> toRemove2 = new List<GameObject>();
    List<GameObject> removing = new List<GameObject>();
    public bool dontAdd = false;
    public Entity TestOpponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager.enemies.Add(TestOpponent);
        AddObjects();
        EntityObjectScript entityObjectScriptPlayer = playerObject.AddComponent<EntityObjectScript>();
        entityObjectScriptPlayer.assignment = Assignment.PLAYER;


        foreach (GameObject enemy in manager.enemyObjects)
        {
            enemy.SetActive(false);
        }

        foreach (GameObject ally in manager.allyObjects)
        {
            ally.SetActive(false);
        }

        if (manager.NextTurn == null)
            manager.NextTurn = new UnityEvent();
        manager.NextTurn.AddListener(NextAction);


        SetValidActors();
        BattleStart();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BattleStart()
    {
        currentActor = playerObject;
        EntityInitializer();
    }

    // TODO - erase on restart
    public void EntityInitializer()
    {
        int i = 0;
        for (int j = 0; j < toRemove1.Count; j++)
        {
            toRemove1.Remove(toRemove1[j]);
            Destroy(toRemove1[j]);
        }
        for (int j = 0; j < toRemove2.Count; j++)
        {
            toRemove2.Remove(toRemove2[j]);
            Destroy(toRemove2[j]);
        }
        foreach (Entity enemy in manager.enemies)
        {
            
            GameObject convObj = manager.enemyObjects[i];
            Debug.Log("convObj Successful");
            Vector3 pos = cam.WorldToScreenPoint(convObj.transform.position);
            Debug.Log("cameraConv Successful");
            GameObject newOverlay = Instantiate(enemyOverlayPrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            EntityScript entityScript = newOverlay.GetComponent<EntityScript>();
            entityScript.creature = enemy;
            entityScript.enemy = true;
            entityScript.entityObject = convObj;
            entityScript.assignment = GetEnemyAssignment(i);
            entityScript.player = player;
            if (dontAdd == false)
            {
                EntityObjectScript entityObjectScript = convObj.AddComponent<EntityObjectScript>();
                entityObjectScript.assignment = entityScript.assignment;
            }
            manager.enemyObjects[i].SetActive(true);
            toRemove1.Add(newOverlay);
            i++;
        }
        i = 0;
        foreach (Entity ally in manager.allies)
        {
            GameObject convObj = manager.allyObjects[i];
            Vector3 pos = cam.WorldToScreenPoint(convObj.transform.position);
            GameObject newOverlay = Instantiate(enemyOverlayPrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            EntityScript entityScript = newOverlay.GetComponent<EntityScript>();
            entityScript.creature = ally;
            entityScript.enemy = false;
            entityScript.entityObject = convObj;
            entityScript.assignment = GetAllyAssignment(i);
            entityScript.player = player;
            if (dontAdd == false)
            {
                EntityObjectScript entityObjectScript = convObj.AddComponent<EntityObjectScript>();
                entityObjectScript.assignment = entityScript.assignment;
            }
            manager.allyObjects[i].SetActive(true); 
            toRemove2.Add(newOverlay);
            i++;
        }
        dontAdd = true;
        Debug.Log("Initialized");
    }

    public Assignment GetEnemyAssignment(int i)
    {
        Assignment obje = Assignment.E1;
        switch (i)
        {
            case 0:
                obje = Assignment.E1;
                break; 
            case 1:
                obje = Assignment.E2;
                break; 
            case 2:
                obje = Assignment.E3;
                break; 
            case 3:
                obje = Assignment.E4;
                break; 
        }
        return obje;
    }

    public Assignment GetAllyAssignment(int i)
    {
        Assignment obje = Assignment.A1;
        switch (i)
        {
            case 0:
                obje = Assignment.A1;
                break; 
            case 1:
                obje = Assignment.A2;
                break; 
            case 2:
                obje = Assignment.A3;
                break; 
        }
        return obje;
    }


    // im sorry
    public void AddObjects()
    {
        manager.enemyObjects.Add(e1);
        manager.enemyObjects.Add(e2);
        manager.enemyObjects.Add(e3);
        manager.enemyObjects.Add(e4);
        manager.allyObjects.Add(a1);
        manager.allyObjects.Add(a2);
        manager.allyObjects.Add(a3);
    }

    public void SetValidActors()
    {
        foreach (GameObject entity in validActors)
        {
            removing.Add(entity);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            validActors.Remove(removing[index]);
        }
        validActors.Add(playerObject);
        switch (manager.allies.Count)
        {
            case 0:
                break;
            case 1:
                validActors.Add(a1);
                break;
            case 2:
                validActors.Add(a1);
                validActors.Add(a2);
                break;
            case 3:
                validActors.Add(a1);
                validActors.Add(a2);
                validActors.Add(a3);
                break;
        }
        switch (manager.enemies.Count)
        {
            case 0:
                break;
            case 1:
                validActors.Add(e1);
                break;
            case 2:
                validActors.Add(e1);
                validActors.Add(e2);
                break;
            case 3:
                validActors.Add(e1);
                validActors.Add(e2);
                validActors.Add(e3);
                break;
            case 4:
                validActors.Add(e1);
                validActors.Add(e2);
                validActors.Add(e3);
                validActors.Add(e4);
                break;
        }
        foreach (GameObject actor in validActors)
        {
            validActorsInitial.Add(actor);
        }
    }

    // this is not working, issue at line 228
    public void NextAction()
    {
        foreach(GameObject actor in validActors)
        {
            Debug.Log("Valid Actor at index" + validActors.IndexOf(actor) + ": " + actor);
        }
        int actorCount = validActors.Count;
        int nextActor = validActors.IndexOf(currentActor) + 1;
        if (validActors.ElementAtOrDefault(nextActor) != null)
        {
            currentActor = validActors[nextActor];
            Assignment assignment = currentActor.GetComponent<EntityObjectScript>().assignment;
            manager.canAct = false;
        }
        else
        {
            currentActor = playerObject;
            manager.canAct = true;
        }
        // this does not work. i do not know why
        /* if (currentActor.GetComponent<EntityObjectScript>().enemy == true && currentActingSide == Turn.ALLY)
        {
            currentActingSide = Turn.ENEMY;
        }
        else if (currentActor.GetComponent<EntityObjectScript>().enemy == false && currentActingSide == Turn.ENEMY)
        {
            currentActingSide = Turn.ALLY;
        } */
        // this is currently bugged, so its functionally has been temporarily stripped
        // the bug relates to enemies always being marked as unconscious when being attacked
        if (currentActor.GetComponent<EntityObjectScript>().unconscious == true)
        {
            // NextAction();
        }
    }

    public void OnRevive(GameObject revived)
    {
        // validActors.Add(revived);
        //validActors.Sort(validActorsInitial); how do i sort this
    }

}
