using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] List<EnemyMovement> enemies;
    [SerializeField] List<CivMovement> civilians;

    private bool observeMode;

    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(instance.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        observeMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObserveMode(bool flag) {
        observeMode = flag;

        // if observe mode finished (false), reset all enemies to their starting position
        if (!observeMode)
        {
            foreach(EnemyMovement enemy in enemies)
            {
                enemy.ObserveFinished();
            }

            foreach(CivMovement civ in civilians)
            {
                civ.ObserveFinished();
            }
        }
    }

    public bool GetObserveMode() {
        return observeMode;
    }
}
