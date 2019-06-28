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
    /// Disable the canvas on a UI object.
    /// </summary>
    public class TurnCanvasOff : IHeroKitAction
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
        public static TurnCanvasOff Create()
        {
            TurnCanvasOff action = new TurnCanvasOff();
            return action;
        }

        // Execute the action
        public int Execute(HeroKitObject hko)
        {
            heroKitObject = hko;

            SceneObjectValueData objectData = SceneObjectValue.GetValue(heroKitObject, 0, 1, false);
            Canvas canvas = null;

            // object is hero object
            if (objectData.heroKitObject != null)
            {                
                canvas = objectData.heroKitObject[0].GetHeroComponent<Canvas>("Canvas");
            }

            // object is game object
            else if (objectData.gameObject != null)
            {
                canvas = heroKitObject.GetGameObjectComponent<Canvas>("Canvas", false, objectData.gameObject[0]);
            }

            if (canvas != null)
            {
                canvas.enabled = false;
            }
            else
            {
                Debug.LogError("Can't toggle canvas. There is no canvas attached to this game object.");
            }

            //------------------------------------
            // debug message
            //------------------------------------
            if (heroKitObject.debugHeroObject)
            {
                string strGO = (canvas != null) ? canvas.gameObject.name : "";
                string debugMessage = "Game Object: " + strGO;
                Debug.Log(HeroKitCommonRuntime.GetActionDebugInfo(heroKitObject, debugMessage));
            }

            return -99;
        }

        // not used
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