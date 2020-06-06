using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lean.Touch
{
    [RequireComponent(typeof(LeanPinchScale))]
    public class ObjectSelection : MonoBehaviour
    {
        public ObjectSelection[] OtherObj;
        public bool ThisIsSelected;

        private LeanPinchScale _myleanpinchscale;

        private void Start()
        {
            _myleanpinchscale = GetComponent<LeanPinchScale>();
        }

        private void Update()
        {
            _myleanpinchscale.enabled =ThisIsSelected;
        }

        public void DeselectAllOthers()
        {
            OtherObj = GameObject.FindObjectsOfType<ObjectSelection>();
            foreach(ObjectSelection ThisIsSelected in OtherObj)
            {
                ThisIsSelected.ThisIsSelected = false;
            }
            //this.ThisIsSelected = true;
            ThisIsSelected = true;
        }
    }
}
