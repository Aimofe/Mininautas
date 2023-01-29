using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Number of geometry target")]
    public int _nButton;
    

    [Header("Number of geometry pressed")]
    public int _buttonCounter;
    
    int _totalGeometry;
    int _goodGeometry;
    int _badGeometry;
    public Text _mission;

    public CreatePanelAddSubs _createPanel;
    [SerializeField]
    private CalculatePuntuation _calculatePuntuation;
    [SerializeField]
    private int _level;

    [SerializeField]
    AddPanelDifficulty _completeThePanel;
    AddPanelDifficulty.dataDiffilcuty _currentDataDifficulty;

    GameObject checkButton;

    [Header("Animations")]
    public GameObject _spedometer;
    public GameObject _spedometer2;
    public GameObject _bar;

    void Start()
    {
        //_completeThePanel = GetComponent<AddPanelDifficulty>();
        _currentDataDifficulty = _completeThePanel.GenerateDataDifficulty(_level);
        checkButton = GameObject.FindGameObjectWithTag("CheckButton");

    }
    public string GetTextGame()
    {
        string message = (GeometryNumberText(_createPanel._orderButtons) );
        return  message;
    }
    // Update is called once per frame
    void Update()
    {
        if (_buttonCounter>0)
        {
            checkButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            checkButton.GetComponent<Button>().interactable = false;
        }
    }
   
    /// <summary>Show the geometry name in plural or singular.</summary> 

    /// <param name="nGeometry">The quantity of a geometry</param> 
    /// <param name="geometryName">The name of the geometry</param>

    /// <returns>Empty or the name of the geometry in singular or plural</returns> 
    public string GeometryNumberText(int nGeometry)
    {
        if (nGeometry == 0)
        {
            return "";
        }
        else if (nGeometry > 1)
        {
            return "Apaga o enciende botones para tener "+nGeometry + " botones encendidos";
        }
        else
        {
            return "Apaga o enciende botones para tener " + nGeometry + " botones encendidos";
        }
    }

    /// <summary>Check the quantity of success.</summary> 
    public void Compare()
    {
       // ServiceLocator.Instance.GetService<IGameManager>().SetClientState (IGameManager.GAME_STATE_CLIENT.ranking);
        CheckGeometry(_createPanel._orderButtons, _buttonCounter);

        if (_badGeometry > 0)
            ServiceLocator.Instance.GetService<IError>().GenerateError();
        else
            ServiceLocator.Instance.GetService<IPositive>().GenerateFeedback(Vector2.zero);

        ServiceLocator.Instance.GetService<ICalculatePoints>().Puntuation(_goodGeometry, _badGeometry);

        _bar.GetComponent<Animator>().Play("Bar_Animation");
        _spedometer.GetComponent<Animator>().Play("Speedometer_01_Animation");
        _spedometer2.GetComponent<Animator>().Play("Speedometer_02_Animation");

        _calculatePuntuation.Puntuation(_goodGeometry, _badGeometry);

        _goodGeometry = 0;
        _badGeometry = 0;
        _nButton = 0;
        _buttonCounter = 0;

        _createPanel.Restart();
    }
    /// <summary>Check how much geometry is ok.</summary> 
    /// <param name="nGeometry">The quantity of a geometry</param> 
    /// <param name="counter">The geometry of the player</param>
    public void CheckGeometry(int nGeometry, int counter)
    {
        if (nGeometry > 0)
        {
           
            if (nGeometry == counter)
            {
                _goodGeometry += 1;
            }
            else
            {
                _badGeometry += 1;
            }
        }

    }
    #region Button Counters
    public void CounterButton(GameObject button)
    {
        _buttonCounter = Counter(button, _buttonCounter);
    }
   
    public int Counter(GameObject button, int counter)
    {
        if (button.GetComponent<ObjectPanel>()._pressed == false)
        {
            counter++;
            button.GetComponent<Image>().sprite = button.GetComponent<ObjectPanel>()._pressedSprite;
            button.GetComponent<ObjectPanel>()._pressed = true;
            //button.GetComponent<GeometryButton>()._isPresed = true;
        }
        else
        {
            counter--;
            button.GetComponent<Image>().sprite = button.GetComponent<ObjectPanel>()._restSprite;
            button.GetComponent<ObjectPanel>()._pressed = false;
            //button.GetComponent<GeometryButton>()._isPresed = false;
        }
        return counter;
    }
    #endregion
}

