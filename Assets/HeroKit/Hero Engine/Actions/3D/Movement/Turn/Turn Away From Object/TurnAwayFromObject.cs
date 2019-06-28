﻿// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;
using HeroKit.Scene.ActionField;
using HeroKit.Scene.Scripts;

namespace HeroKit.Scene.Actions
{

    /// <summary>
    /// Turn an object away from another object.
    /// </summary>
    public class TurnAwayFromObject : IHeroKitAction
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
        public static TurnAwayFromObject Create()
        {
            TurnAwayFromObject action = new TurnAwayFromObject();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;
            HeroKitObject[] targetObject = HeroObjectFieldValue.GetValueE(heroKitObject, 0, 1);
            HeroKitObject avoidObject = HeroObjectFieldValue.GetValueA(heroKitObject, 2)[0];
            bool runThis = (targetObject != null && avoidObject != null);

            // execute action for all objects in list
            for (int i = 0; runThis && i < targetObject.Length; i++)
                ExecuteOnTarget(targetObject[i], avoidObject);

            // set up the long action
            eventID = heroKitObject.heroStateData.eventBlock;
            heroKitObject.heroState.heroEvent[eventID].waiting = true;
            updateIsDone = false;
            heroKitObject.longActions.Add(this);

            // debug message
            if (heroKitObject.debugHeroObject)
            {
                string debugMessage = "Turn away from this object: " + avoidObject;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        public void ExecuteOnTarget(HeroKitObject targetObject, HeroKitObject avoidObject)
        {
            // get the movement script
            moveObject = targetObject.GetHeroComponent<HeroMove3D>("HeroMove3D", true);
            moveObject.target = avoidObject.transform;
            moveObject.Move = moveObject.TurnAwayFromObject;
            moveObject.settings.turnObject = true;
            moveObject.Initialize();
        }

        // ---------------------------------------
        // Long Update Data
        // ---------------------------------------
        private HeroMove3D moveObject;

        // has action completed?
        public bool RemoveFromLongActions()
        {
            return HeroActionCommonRuntime.RemoveFromLongActions(updateIsDone, eventID, heroKitObject);
        }

        // update the action
        public void Update()
        {
            if (moveObject == null || !moveObject.enabled)
                updateIsDone = true;
        }
    }
}