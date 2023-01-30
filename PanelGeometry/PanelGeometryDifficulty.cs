using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGeometryDifficulty : MonoBehaviour
{
    [Header("Data")]
    public List<dataDiffilcuty> _dataDifficulty;

    [Serializable]
    public struct dataDiffilcuty
    {
        [Tooltip("Column Lenght")]
        public int column;
        [Tooltip("Row Lenght")]
        public int row;
        [Tooltip("Possible geometry that can be target at this level")]
        public List<GameObject> possibleGeometry;
        [Tooltip("Possible geometry that can be target at this level")]
        public List<GameObject> targetGeometry;
        [Tooltip("Total number of different geometry targets at this level")]
        public int numGeometryTargets;

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
