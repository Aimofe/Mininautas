using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPanelDifficulty : MonoBehaviour
{
    [Header("Data")]
    public List<dataDiffilcuty> _dataDifficulty;

    [Serializable]
    public struct dataDiffilcuty
    {
        [Tooltip("Possible geometry that can be target at this level")]
        public List<GameObject> possibleGeometry;
        [Tooltip("Possible geometry that can be target at this level")]
        public List<GameObject> targetGeometry;
        [Tooltip("The number of buttons to add or substract")]
        public int elementToAddSubs;
        [Tooltip("The number of pressed buttons")]
        public int pressedButtons;

    }

    /// <summary>
    /// Returns difficulty data to create the level
    /// </summary>
    /// <param name="levelDifficulty">Difficulty level</param>
    /// <returns>Returns the data in format <see cref="dataDiffilcuty"/> </returns>
    public dataDiffilcuty GenerateDataDifficulty(int levelDifficulty)
    {
        if (_dataDifficulty.Count < levelDifficulty)
        {
            return _dataDifficulty[0];
        }
        else
        {
            return _dataDifficulty[levelDifficulty - 1];
        }
    }
}
