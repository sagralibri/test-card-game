using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class BattleHandler : MonoBehaviour
{
    
    private manager manager;
    public bool victorious;
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
    public Entity opponent;
    public int damageValue;
    public TMP_Text turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turn.text = "Ally";
        AddObjects();
        EntityObjectScript entityObjectScriptPlayer = playerObject.AddComponent<EntityObjectScript>();
        entityObjectScriptPlayer.assignment = Assignment.PLAYER;
        entityObjectScriptPlayer.creature = player;
        GameObject newOverlayPlayer = Instantiate(enemyOverlayPrefab, new Vector2(-9000,0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        EntityScript entityScriptPlayer = newOverlayPlayer.GetComponent<EntityScript>();
        entityScriptPlayer.creature = player;
        entityScriptPlayer.enemy = false;
        entityScriptPlayer.entityObject = playerObject;
        entityScriptPlayer.assignment = Assignment.PLAYER;
        entityScriptPlayer.player = player;
        entityScriptPlayer.isPlayer = true;
        

        Debug.Log(manager.enemyObjects.Count);
        Debug.Log(manager.allyObjects.Count);
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

        if (manager.GetEntityBH == null)
            manager.GetEntityBH = new UnityEvent<Entity, bool>();
        manager.GetEntityBH.AddListener(ProcessCurrentActor);

        if (manager.ReturnEntityBH2 == null)
            manager.ReturnEntityBH2 = new UnityEvent<Entity>();
        manager.ReturnEntityBH2.AddListener(SetEntity);

        if (manager.ReturnDamageValue == null)
            manager.ReturnDamageValue = new UnityEvent<int>();
        manager.ReturnDamageValue.AddListener(SetDamage);

        if (manager.EraseExcess == null)
            manager.EraseExcess = new UnityEvent();
        manager.EraseExcess.AddListener(DestroyExcess);



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
            Vector3 pos = cam.WorldToScreenPoint(convObj.transform.position);
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
                entityObjectScript.creature = enemy;
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
            Debug.Log(ally);
            Debug.Log(manager.allies[0]);
            entityScript.creature = ally;
            entityScript.enemy = false;
            entityScript.entityObject = convObj;
            entityScript.assignment = GetAllyAssignment(i);
            entityScript.player = player;
            if (dontAdd == false)
            {
                EntityObjectScript entityObjectScript = convObj.AddComponent<EntityObjectScript>();
                entityObjectScript.assignment = entityScript.assignment;
                entityObjectScript.creature = ally;
            }
            manager.allyObjects[i].SetActive(true); 
            toRemove2.Add(newOverlay);
            i++;
        }
        dontAdd = true;
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

    public void NextAction()
    {
        foreach(GameObject actor in validActors)
        {
            Debug.Log("Valid Actor at index" + validActors.IndexOf(actor) + ": " + actor);
        }
        if (IsAllDeadEnemy() == true)
        {
            Victory();
        }
        int actorCount = validActors.Count;
        int nextActor = validActors.IndexOf(currentActor) + 1;
        if (validActors.ElementAtOrDefault(nextActor) != null)
        {
            currentActor = validActors[nextActor];
            if (currentActor.GetComponent<EntityObjectScript>().unconscious == false)
            {
                Assignment assignment = currentActor.GetComponent<EntityObjectScript>().assignment;
                manager.canAct = false;
                Debug.Log("boop beep: " + currentActor);
                manager.ProcessAI.Invoke(assignment);
            }
            else
            {
                NextAction();
            }
        }
        else
        {
            currentActor = playerObject;
            manager.canAct = true;
        }
        // this does not work. i do not know why
        if (currentActor.GetComponent<EntityObjectScript>().enemy == true && currentActingSide == Turn.ALLY)
        {
            currentActingSide = Turn.ENEMY;
            turn.text = "Enemy";
        }
        else if (currentActor.GetComponent<EntityObjectScript>().enemy == false && currentActingSide == Turn.ENEMY)
        {
            currentActingSide = Turn.ALLY;
            turn.text = "Ally";
        }
    }

    public void OnRevive(GameObject revived)
    {
        // validActors.Add(revived);
        //validActors.Sort(validActorsInitial); how do i sort this
    }


    //

    // WARNING !!! NPC AI CODE FROM HERE ON OUT!! WILL BE SCUFFED

    //
    // i dont want to do this anymore


    // TODO: so much

    void ProcessCurrentActor(Entity actor, bool isEnemy)
    {
        Debug.Log(actor + " is now taking their turn");
        // preliminary declarations
        List<GameObject> validAllies = new List<GameObject>();
        List<GameObject> validEnemies = new List<GameObject>();
        validAllies = GetValidAllies(isEnemy);
        validEnemies = GetValidEnemies(isEnemy);

        bool AOE = false;
        bool healing = false;
        GameObject target = playerObject;
        int usedTechniqueInt = UnityEngine.Random.Range(0, actor.techniques.Count - 1);
        Technique usedTechnique = actor.techniques[usedTechniqueInt];

        switch (usedTechnique.techniqueType)
        {
            case TechniqueType.DAMAGE:
            {
                if (usedTechnique.targeting == Target.ONEENEMY)
                {
                    int targetInt = UnityEngine.Random.Range(0, validEnemies.Count - 1);
                    target = validEnemies[targetInt];
                    Debug.Log(target);
                }
                else
                {
                    AOE = true;
                } 
                break;
            }
            case TechniqueType.HEALING:
            {
                //there is no healing function currently
                break;
            }
        }

        if (healing == false)
        {
            if (AOE == false)
            {
                Assignment input = target.GetComponent<EntityObjectScript>().assignment;
                Assignment input2 = currentActor.GetComponent<EntityObjectScript>().assignment;
                Entity targetEntity = target.GetComponent<EntityObjectScript>().creature;
                Entity casterEntity = currentActor.GetComponent<EntityObjectScript>().creature;
                manager.UseTechnique.Invoke(input, usedTechnique, targetEntity, casterEntity);
                manager.GetMana.Invoke(input2, usedTechnique, targetEntity, casterEntity);
                Debug.Log("Enemy turn Complete");
            }
            else
            {
                Entity casterEntity = currentActor.GetComponent<EntityObjectScript>().creature;
                foreach (GameObject enemy in validEnemies)
                {
                Assignment input = enemy.GetComponent<EntityObjectScript>().assignment;
                Entity targetEntity = enemy.GetComponent<EntityObjectScript>().creature;
                manager.UseTechnique.Invoke(input, usedTechnique, targetEntity, casterEntity);
                }
                Debug.Log("Enemy turn Complete");
            }
        }

        manager.NextTurn.Invoke();
    }



    public List<GameObject> GetValidAllies(bool isEnemy)
    {
        List<GameObject> validAllies = new List<GameObject>();
        if (isEnemy == true)
        {
            switch (manager.enemies.Count)
            {
                case 0:
                    break;
                case 1:
                    validAllies.Add(e1);
                    break;
                case 2:
                    validAllies.Add(e1);
                    validAllies.Add(e2);
                    break;
                case 3:
                    validAllies.Add(e1);
                    validAllies.Add(e2);
                    validAllies.Add(e3);
                    break;
                case 4:
                    validAllies.Add(e1);
                    validAllies.Add(e2);
                    validAllies.Add(e3);
                    validAllies.Add(e4);
                    break;
            }
        }
        else
        {
            switch (manager.allies.Count)
            {
                case 0:
                    validAllies.Add(playerObject);
                    break;
                case 1:
                    validAllies.Add(playerObject);
                    validAllies.Add(a1);
                    break;
                case 2:
                    validAllies.Add(playerObject);
                    validAllies.Add(a1);
                    validAllies.Add(a2);
                    break;
                case 3:
                    validAllies.Add(playerObject);
                    validAllies.Add(a1);
                    validAllies.Add(a2);
                    validAllies.Add(a3);
                    break;
            }
        }
        return validAllies;
    }

    public List<GameObject> GetValidEnemies(bool isEnemy)
    {
        List<GameObject> validEnemies = new List<GameObject>();
        if (isEnemy == true)
        {
            switch (manager.allies.Count)
            {
                case 0:
                    validEnemies.Add(playerObject);
                    break;
                case 1:
                    validEnemies.Add(playerObject);
                    validEnemies.Add(a1);
                    break;
                case 2:
                    validEnemies.Add(playerObject);
                    validEnemies.Add(a1);
                    validEnemies.Add(a2);
                    break;
                case 3:
                    validEnemies.Add(playerObject);
                    validEnemies.Add(a1);
                    validEnemies.Add(a2);
                    validEnemies.Add(a3);
                    break;
            }
        }
        else
        {
            switch (manager.enemies.Count)
            {
                case 0:
                    break;
                case 1:
                    validEnemies.Add(e1);
                    break;
                case 2:
                    validEnemies.Add(e1);
                    validEnemies.Add(e2);
                    break;
                case 3:
                    validEnemies.Add(e1);
                    validEnemies.Add(e2);
                    validEnemies.Add(e3);
                    break;
                case 4:
                    validEnemies.Add(e1);
                    validEnemies.Add(e2);
                    validEnemies.Add(e3);
                    validEnemies.Add(e4);
                    break;
            }
        }
        return validEnemies;
    }

    public void SetEntity(Entity entity)
    {
        opponent = entity;
    }

    public void SetDamage(int damage)
    {
        damageValue = damage;
    }

    // end

    public bool IsAllDeadEnemy()
    {
        bool allDead = true;
        foreach (GameObject entity in validActors)
        {
            if (entity.GetComponent<EntityObjectScript>().enemy == true && entity.GetComponent<EntityObjectScript>().unconscious == false)
            {
                allDead = false;
            }
        }
        return allDead;
    }

    public void Victory()
    {
        if (victorious == false)
        {
            Debug.Log("VICTORY!");
            manager.difficultyValue += 1;
            manager.EraseExcess.Invoke();
            foreach (Treasure treasure in manager.treasures)
            {
                switch (treasure.ID)
                {
                    case 12:
                    {
                        manager.money += 4;
                        break;
                    }
                }
            }
            manager.ProceedStage();
            victorious = true;
        }
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void DestroyExcess()
    {
        foreach (GameObject objeect in manager.allyObjects)
        {
            removing.Add(objeect);
        } 
        for (int index = 0; index < removing.Count; index++)
        {
            manager.allyObjects.Remove(removing[index]);
            Destroy(removing[index]);
        }
        foreach (GameObject objeect in manager.enemyObjects)
        {
            removing.Add(objeect);
        } 
        for (int index = 0; index < removing.Count; index++)
        {
            manager.enemyObjects.Remove(removing[index]);
            Destroy(removing[index]);
        }
    }

}
