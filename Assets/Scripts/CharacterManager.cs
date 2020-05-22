using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject charPrefabs;

    public List<CharacterPathing> GetCharList()
    {
        var charPaths = new List<CharacterPathing>();

        foreach (Transform character in charPrefabs.transform)
        {
            charPaths.Add(character.GetComponent<CharacterPathing>());
            Debug.Log("Adding: " + character.name);
        }

        return charPaths;
    }
}
