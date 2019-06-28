﻿// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using System;
using HeroKit.Scene.ActionField;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Change the rotation of an object by a specific amount.
    /// </summary>
    public class IncrementRotation : IHeroKitAction
    {
        // set up properties needed for all actions
        private HeroKitObject _heroKitObject;
        public HeroKitObject heroKitObject
        {
            get { return _heroKitObject; }
            set { _heroKitObject = value; }
        }
        private int _eventID;
        public int eventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }
        private bool _updateIsDone;
        public bool updateIsDone
        {
            get { return _updateIsDone; }
            set { _updateIsDone = value; }
        }

        // This is used by HeroKitCommon.GetAction() to add this action to the ActionDictionary. Don't delete!
        public static IncrementRotation Create()
        {
            IncrementRotation action = new IncrementRotation();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            // get field values
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            string childName = ChildObjectValue.GetValue(heroKitObject, 2, 3);
            bool runThis = (targetObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], childName);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Child (if used): " + childName + "\n" +
                                      "Euler Angles: " + degreesToChange;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        private Vector3 degreesToChange = new Vector3();
        public void ExecuteOnTarget(HeroKitObject targetObject, string childName)
        {
            Transform transform = null;
            if (childName == "")
                transform = targetObject.transform;
            else
                transform = targetObject.GetHeroChildComponent<Transform>("Transform", childName);

            if (transform != null)
            {
                Vector3 degrees = new Vector3();

                // get the target rotation
                bool changeX = BoolValue.GetValue(heroKitObject, 4);
                if (changeX)
                {
                    degrees.x = IntegerFieldValue.GetValueA(heroKitObject, 5);
                    degreesToChange.x = degrees.x;
                }
                bool changeY = BoolValue.GetValue(heroKitObject, 6);
                if (changeY)
                {
                    degrees.y = IntegerFieldValue.GetValueA(heroKitObject, 7);
                    degreesToChange.y = degrees.y;
                }
                bool changeZ = BoolValue.GetValue(heroKitObject, 8);
                if (changeZ)
                {
                    degrees.z = IntegerFieldValue.GetValueA(heroKitObject, 9);
                    degreesToChange.z = degrees.z;
                }

                // change the rotation
                transform.localEulerAngles = transform.localEulerAngles + degrees;
            }
        }

        // Not used
        public bool RemoveFromLongActions()
        {
            throw new NotImplementedException();
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}