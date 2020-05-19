using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Config")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] float moveSpeed = 2f;
    //[SerializeField] GameObject characterPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] List<float> waitTimes;

    //public GameObject GetCharacterPrefab() { return characterPrefab; }   

    public List<Transform> GetWaypoints() 
    {
        var charWaypoints = new List<Transform>();

        // foreach (TYPE varname in VARIABLE)
        foreach (Transform child in pathPrefab.transform)
        {
            charWaypoints.Add(child);
        }

        return charWaypoints;
    }

    public List<float> GetWaitTimes() { return waitTimes; }

    public float GetMoveSpeed() { return moveSpeed; }
}
