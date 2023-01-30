using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanelGeometry : MonoBehaviour
{
    [Header("Table Configuration")]
    public int _column;
    public int _row;
    public int _gapX;
    public int _gapY;
    public int _offsetX;
    public int _offsetY;

    int _count;

    public GameObject _canvas;
    [Header("Geometry")]
    public GameObject[] _geometryForms;
    public List<GameObject> _geometryList = new List<GameObject>();
    public List<GameObject> _targetList = new List<GameObject>();
    public List<GameObject> _allList = new List<GameObject>();
    private List<Geometry.Geometry_Type> _typeTargetGeometry = new List<Geometry.Geometry_Type>();

    //Game Configuration
    [SerializeField]
    private int _level;

    [SerializeField]
    CompleteThePanelDifficulty _completeThePanel;
    CompleteThePanelDifficulty.dataDiffilcuty _currentDataDifficulty;

    void Start()
    {
        _completeThePanel = GetComponent<CompleteThePanelDifficulty>();
        _currentDataDifficulty = _completeThePanel.GenerateDataDifficulty(_level);
        GeneratePanel();
    }

    /// <summary>
    /// Creates a panel with geometry.
    /// </summary>
    void GeneratePanel()
    {
        _geometryList.Clear();
        _targetList.Clear();
        _allList.Clear();
        GenerateTargets();
        GenerateNoTargetGeometry();

        //COLOCAR BOTONES EN POSICIÓN

        for (int x = 0; x < _column; x++)
        {
            for (int y = 0; y < _row; y++)
            {
                _allList[_count].GetComponent<Transform>().position = new Vector3((x + _offsetX) * _gapX, (y + _offsetY) * _gapY, 0);
                _count++;
            }
        }
        ServiceLocator.Instance.GetService<IGameTimeConfiguration>().StartGameTime();
    }

    /// <summary>
    /// Generates the normal geometry.
    /// </summary>
    private void GenerateNoTargetGeometry()
    {
        for (int i = 0; i < (_row * _column) - _currentDataDifficulty.numTargets; i++)
        {
            int geometryID = Random.Range(0, _currentDataDifficulty.possibleGeometry.Count);
            if (geometryID >= 7)
                geometryID = 6;

            GameObject newGeometry;
            newGeometry = Instantiate(_currentDataDifficulty.possibleGeometry[geometryID], new Vector3(0, 0, 0), Quaternion.identity);
            newGeometry.transform.SetParent(_canvas.transform, false);
            _geometryList.Add(newGeometry);
            _allList.Add(newGeometry);
        }

    }
    /// <summary>
    /// Generates the target geometry.
    /// </summary>
    private void GenerateTargets()
    {
        GenerateTypeTargetGeometry();
        do
        {
            for (int i = 0; i < _allList.Count; i++)
            {
                Destroy(_allList[i]);
            }
            _targetList.Clear();
            _allList.Clear();
            for (int i = 0; i < _currentDataDifficulty.numTargets; i++)
            {
                int idGeometry = Random.Range(0, _currentDataDifficulty.targetGeometry.Count);
                GameObject newGeometry = Instantiate(_currentDataDifficulty.targetGeometry[idGeometry], new Vector3(0, 0, 0), Quaternion.identity);
                newGeometry.GetComponent<ObjectPanel>()._placed = false;
                _targetList.Add(newGeometry);
                _allList.Add(newGeometry);
                newGeometry.transform.SetParent(_canvas.transform, false);
            }
        } while (!CheckTargets());
    }
    /// <summary>
    /// Generates the geometry type of the target.
    /// </summary>
    private void GenerateTypeTargetGeometry()
    {
        _typeTargetGeometry.Clear();
        for (int i = 0; i < _currentDataDifficulty.numGeometryTargets; i++)
        {
            bool isCorrect = false;
            int idGeometry = Random.Range(0, _currentDataDifficulty.targetGeometry.Count);
            do
            {
                isCorrect = false;
                Geometry.Geometry_Type newGeometry = _currentDataDifficulty.targetGeometry[idGeometry].GetComponent<Geometry>()._geometryType;
                if (!_typeTargetGeometry.Contains(newGeometry))
                {
                    _typeTargetGeometry.Add(newGeometry);
                    isCorrect = true;
                }
                else
                {
                    idGeometry++;
                    if (idGeometry >= _currentDataDifficulty.targetGeometry.Count)
                    {
                        idGeometry = 0;
                    }
                }
            } while (!isCorrect);
        }

    }
    /// <summary>
    /// Checks if the list of targets contains geometry repeated.
    /// </summary>
    private bool CheckTargets()
    {
        bool result = true;
        List<Geometry.Geometry_Type> geoTypeTarget = new List<Geometry.Geometry_Type>();
        foreach (GameObject currentTarget in _targetList)
        {
            geoTypeTarget.Add(currentTarget.GetComponent<Geometry>()._geometryType);
        }
        foreach (Geometry.Geometry_Type currentGeometryTpe in _typeTargetGeometry)
        {
            if (!geoTypeTarget.Contains(currentGeometryTpe))
            {
                result = false;
            }
        }
        return result;
    }
    // Update is called once per frame
    void Update()
    {
        if (_allList.Count == _row * _column)
        {
            if (ServiceLocator.Instance.GetService<IGameManager>().GetClientState() != IGameManager.GAME_STATE_CLIENT.playing)
            {
                for (int i = 0; i < _allList.Count; i++)
                {
                    _allList[i].GetComponent<Button>().interactable = false;
                }
            }
            else
            {
                for (int i = 0; i < _allList.Count; i++)
                {
                    _allList[i].GetComponent<Button>().interactable = true;
                }
            }
        }

    }

    /// <summary>
    /// Restarts the minigame.
    /// </summary>
    public void Restart()
    {
        foreach (GameObject geometry in _allList)
        {
            Destroy(geometry);
        }
        _count = 0;
        GeneratePanel();
    }
}
