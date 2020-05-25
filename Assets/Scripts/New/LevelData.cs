using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : MonoBehaviour
{
    public int level;

    public LevelData(LevelManager lvl)
    {
        this.level = lvl.level;
    }
}
