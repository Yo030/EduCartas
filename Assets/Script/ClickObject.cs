using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public ObjectRotation _objectrotation;
    public Lean.Touch.ObjectSelection _objectselected;
    private Lean.Touch.ObjectSelection[] OtherObj;

    public float TimeForHold = 0.15f;                                                           //TIME IT TAKES FOR THE TOUCH TO BE TAKEN AS A HOLD
    private float OriginalTimeForHold;
    private bool Holding;                                                                       //CHECKS TO SEE IF HOLDING

    private void Start()
    {
        OriginalTimeForHold = TimeForHold;                                                      //SAVE "TimeForHold" IN A NEW VARIABLE IN ORDER TO RESET LATER
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))                                                             //IF THE LEFT MOUSE BUTTON IS PRESSED
        {
            TimeForHold -= Time.deltaTime;                                                      //WHILE HOLING THE TIMER IS SUBTRACTING
            if(0 > TimeForHold)                                                                 //IF "TimeForHold" GOES BELOW 0 
            {
                Holding = true;                                                                 //IT THE USER IS "Holing"
                RaycastHit hit;                                                                 //CREATES A RAYCAST
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                    //CAST THE RAYCAST FROM THE CAMERA
                if (Physics.Raycast(ray, out hit, 10000000f))                                   //IF THE RAYCAST COLIDES WITH SOMTHING
                {
                    if (Holding)                                                                //IF PLAYER IS HOLDING
                    {
                        _objectrotation.ActivateTouchpad();                                     //ACTIVATES THE ABILITY TO ROTATE THE SELECTED OBJECT
                    }
                }
            }
        }

        if(Input.GetMouseButtonUp(0))                                                           //WHEN THE LEFT MOUSE BUTTON IS RELEAS
        {
            Press();                                                                            //CALLS THE "Press"  FUNCTION
            TimeForHold = OriginalTimeForHold;                                                  //RESET THE "TimeForHold" TIME
            Holding = false;                                                                    //SETS "Holding" TO FALSE
            _objectrotation.DeactivateTouchpad();                                               //DEACTIVATES THE ABILITY TO ROTATE THE SELECTED OBJECT
        }
    }

    public void Press()
    {
        if(!Holding)                                                                            //WHEN NOT HOLING
        {
            RaycastHit hit;                                                                     //IT THE USER IS "Holing"
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                        //CREATES A RAYCAST
            if (Physics.Raycast(ray, out hit, 10000000f))                                       //CAST THE RAYCAST FROM THE CAMERA
            {
                if (hit.transform && hit.transform.gameObject.tag != "Wall")                    //WHEN THE RAYCAST HITS AN OBJECT AND THAT OBJECT IS NOT A WALL
                {
                    SetSelectedObject(hit.transform);                                           //SETS THE SELECTED OBJECT AS ROTATEABLE
                }

                if (hit.transform.gameObject.tag == "Wall")                                     //WHEN THE RAYCAST HITS AN OBJECT WITH THE TAG
                {
                    SetSelectedObject(null);                                                    //SETS THE SELECTED ROTATEABLE TO "null"
                    _objectrotation.DeactivateTouchpad();                                       //DEACTIVATES THE ABILITY TO ROTATE THE SELECTED OBJECT
                    DeselectAll();
                }
            }
        }
    }

    public void DeselectAll()
    {
        OtherObj = GameObject.FindObjectsOfType<Lean.Touch.ObjectSelection>();
        foreach (Lean.Touch.ObjectSelection ThisIsSelected in OtherObj)
        {
            ThisIsSelected.ThisIsSelected = false;
        }
    }

    public void SetSelectedObject(Transform _objecthit)
    {
        _objectrotation.RotatableH = _objecthit;                                                //ACCES THE "_objectrotation" AND SETS "RotatableH" AS THE TRANSFORM RECIEVED
        _objectrotation.RotatableV = _objecthit;                                                //ACCES THE "_objectrotation" AND SETS "Rotatablev" AS THE TRANSFORM RECIEVED
        if (_objecthit != null)
        {
            _objectselected = _objecthit.GetComponent<Lean.Touch.ObjectSelection>();
            _objectselected.DeselectAllOthers();
        }
        else
        {
            _objectselected = null;
        }
    }
}
