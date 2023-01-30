using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePuntuation : MonoBehaviour, ICalculatePoints
{
    public float _points;
    public float _success;
    public float _fails;
    public int _attempt=0;
    public List<float> _timeList = new List<float>();
    //Lista con el tiempo que tarda por cada intento
    public List<float> _timeAttemptList = new List<float>();
    private int _total;

    float _lastAtemptTime = 0;

    [Header("Average")]
   // public List<Average> _averages;
    public Average _average;
    [Serializable]
    public struct Average
    {
        public float averagePoints;
        public float averageTime;
        public float averageSuccess;
        public float averageFails;
    }
    void Start()
    {

    }
    /// <summary>Calculates the puntuation</summary>
    /// <param name="success">Number of success</param>
    /// <param name="fails">Number of fails</param>
    public void Puntuation(int success, int fails)
    {

        //ServiceLocator.Instance.GetService<GameManager>()._gameStateClient = GameManager.GAME_STATE_CLIENT.ranking;
        float finishTime = ServiceLocator.Instance.GetService<IGameTimeConfiguration>().GetFinishTime();
        float currentTime = ServiceLocator.Instance.GetService<IGameTimeConfiguration>().GetCurrentTime();
        if (_attempt>0)
        {
            if (finishTime - currentTime > 0)
            {
                _timeList.Add(currentTime - _lastAtemptTime);
            }           
        }
        else
        {
            _timeList.Add(currentTime- _lastAtemptTime);
        }
        
        _total = success + fails;
        _success += success;
        _fails += fails;
        
        if (success >0)
        {
            _points += success / _total;
        }
        _attempt++;

        CalculateAverage();

        if (_lastAtemptTime < currentTime)
        {
            _lastAtemptTime = currentTime;
        }
    }
    /// <summary>Calculates the average of the points , time, success fails</summary>
    public void CalculateAverage()
    {
        float finishTime = ServiceLocator.Instance.GetService<IGameTimeConfiguration>().GetFinishTime();

        

        float sumTime = 0;
        for (int i = 0; i < _timeList.Count; i++)
        {
            sumTime += _timeList[i];
        }
        _average.averageTime = sumTime / _attempt;

        if (_success > 0 || _fails > 0)
        {
            _average.averageSuccess = (_success / (_success + _fails)) * 100;
            _average.averageFails = (_fails / (_success + _fails)) * 100;
        }
        else
        {
            _average.averageSuccess = 0;
            _average.averageFails = 0;
        }
        if (_average.averageSuccess == _average.averageFails)
        {
            _average.averagePoints = 100;
        }
        else
        {
            _average.averagePoints = _points * 100.0f;
        }
        
    }

    public Average GetAverage()
    {
        return _average;
    }
}
