using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Panel_Geom : MonoBehaviour
{
   
    [Header("Game Objects")]
    public static GameObject itemDraging;
    public GameObject selectedObject;


    [Header("Positions")]
    Vector3 mousePosition;
    Vector3 startPosition;
    [Header("Parents")]

    Transform startParent;
    Transform dragParent;
    Transform dragParentPosition;

    [Header("Conditions")]
    private bool _collisionDetected;
    private bool _isReturning = false;
    public bool _canDrag = true;
    public bool _correctPosition = false;

    public Vector3 pos;
    public float returnVelocity = 3;
    public GameObject target; 

    //Si pinchas el que no es Correct = false y si no a true
   
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        target.GetComponent<DragParentPropieties>();
         pos = this.transform.position;
       // TiposEngra.AddRange(GameObject.FindGameObjectsWithTag("TagName3"));
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (GameObject gc in myListofGameObject)
        //{

        //    Debug.Log(gc.name);

            /** 

          * Calcula la posición del ratón en el mundo 
     
           */
            mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                        Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);


        
        /** 

        * Si se pulsa el ratón click izquierdo se mueve el objeto respecto a la posición del ratón

       */

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && targetObject != null  && targetObject.gameObject == gameObject && _canDrag)
        {
            Debug.Log("He entrado"); 
            if (targetObject)
            {
                
                itemDraging = targetObject.transform.gameObject;
                startPosition = transform.position;
                startParent = transform.parent;
                transform.SetParent(dragParent);
                
            }
        }
        if (Input.GetMouseButtonUp(0) && targetObject != null)
        {
            if (_collisionDetected && _correctPosition)
            {
                _canDrag = false;
                transform.position = dragParentPosition.position;
                itemDraging = null;
            }
            else
            {
                
                _isReturning = true;
                transform.SetParent(startParent);
                itemDraging = null;
                

            }
            Debug.Log(_isReturning);
        }
#endif


        /** 

       *Proceso para volver a su posición incial en caso de que no haya encontrado un parent

       */
        if (itemDraging)
        {
           
            itemDraging.transform.position = mousePosition;
        }

        if (_isReturning)
        {
            //transform.position = Vector2.Lerp(transform.position, startPosition, returnVelocity * Time.deltaTime);
            this.transform.position = pos;
            if(transform.position == pos)
            {
                _isReturning = false;
            }
        }


        targetObject = null;
    }

    /** 

   Se detectan colisiones, para saber si tenemos un objetivo al cual poner el target

    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DragParent")
        {
            dragParentPosition = collision.gameObject.transform;
            _collisionDetected = true;
            transform.position = dragParentPosition.position;
            print("Colisión: " + collision.gameObject.GetComponent<DragParentPropieties>().correct);
            if (collision.gameObject.GetComponent<DragParentPropieties>().correct==true)
            {
                _correctPosition = true;
                _isReturning = false;
                
            }
            else 
            {
                _correctPosition = false;
                Debug.Log("aaaaaaaaaaaaa" + _isReturning);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DragParent")
        {
            dragParentPosition = null;
            _collisionDetected = false;
        }
    }
}
