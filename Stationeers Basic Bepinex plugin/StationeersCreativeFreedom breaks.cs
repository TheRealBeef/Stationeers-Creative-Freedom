﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;
using JetBrains.Annotations;

using Assets.Scripts;
using Assets.Scripts.UI;
using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Structures;

using Assets.Scripts.GridSystem;
using Assets.Scripts.Networking;

using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Entities;

using Assets.Scripts.Inventory;

using Assets.Scripts.Util;
using Objects.Items;
using Assets.Scripts.Objects.Items;



/*
Thanks to guiding of the TurkeyKittin! 
And other guide of RoboPhred.
And inspiration from DevCo constructions.
And Inaki's exercises!
*/

namespace StationeersCreativeFreedom
{

    #region Structure

    [HarmonyPatch(typeof(Structure), "Awake")]
    internal class Structure_Awake_Patch
    {
     //   [UsedImplicitly]//dunno what is for, something for simpler replacing of the field values.
        private static void Postfix(Structure __instance)
        {
            if (WorldManager.Instance.GameMode == GameMode.Survival)
            {
                __instance.RotationAxis = RotationAxis.All; //thanks for Kamuchi for idea of using the named enumerator values
                __instance.AllowedRotations = AllowedRotations.All;
            }
            //TODO key switcher to change precisement of constructions
            //__instance.GridSize = 0.5f; 
            //__instance.PlacementType = PlacementSnap.Grid;
        }
    }

    [HarmonyPatch(typeof(Structure), "CanConstruct")]
    internal class Structure_CanConstruct_Patch
    {
        private static void Postfix(ref bool __result)
        {
            if (WorldManager.Instance.GameMode == GameMode.Creative)
            { __result = true; }
        }
    }


    #endregion Structure

    #region SmallGrid

    [HarmonyPatch(typeof(SmallGrid), "CanConstruct")]
    internal class SmallGrid_CanConstruct_Patch
    {
        private static void Postfix(ref bool __result)
        {
            if (WorldManager.Instance.GameMode == GameMode.Survival)
            { __result = true; }
        }
    }

    [HarmonyPatch(typeof(SmallGrid), "_IsCollision")]
    internal class SmallGrid_isCollision_Patch
    {
        private static bool Prefix()
        {
            if (WorldManager.Instance.GameMode == GameMode.Survival)
            { return false; }
            return true;
        }
    }

    [HarmonyPatch(typeof(SmallGrid), "HasFrameBelow")]
    internal class SmallGrid_HasFrameBelow_Patch
    {
        private static void Postfix(ref bool __result)
        {
            if (WorldManager.Instance.GameMode == GameMode.Survival)
            { __result = true; }
        }
    }

    [HarmonyPatch(typeof(SmallGrid), "HasVoxelBelow")]
    internal class SmallGrid_HasVoxelBelow_Patch
    {
        private static void Postfix(ref bool __result)
        {
            if (WorldManager.Instance.GameMode == GameMode.Survival)
            { __result = true; }
        }
    }

    [HarmonyPatch(typeof(SmallGrid), "CanMountOnWall")]
    internal class SmallGrid_CanMountOnWall_Patch
    {
        private static void Postfix(ref Structure.CanMountResult __result)
        {
            __result.result = Structure.WallMountResult.Valid;
        }
        //thanks to the TurkeyKittin.
    }

    [HarmonyPatch(typeof(MountedSmallGrid), "CanConstruct")]
    internal class MountedSmallGrid_CanConstruct_Patch
    {
        private static void Postfix(ref bool __result)
        {
            if (WorldManager.Instance.GameMode == GameMode.Survival)
            { __result = true; }
        }
    }
    #endregion SmallGrid
    
    #region Devices
    [HarmonyPatch(typeof(Airlock), "CanConstruct")]
    internal class Airlock_CanConstruct_Patch
    {
        private static void Postfix(ref bool __result)
        {
            if (WorldManager.Instance.GameMode == GameMode.Survival)
            { __result = true; } //evading of CheckSidesBlocked
        }
    }
    #endregion

    #region DynamicThing
 
    #endregion DynamicThing


    #region Items

    #endregion

  

    #region Mothership
    //[HarmonyPatch(typeof(Mothership), "FixedUpdateEachFrame")]
    //internal class Mothership_OnThreadUpdate_Patch
    //{
    //    [UsedImplicitly]
    //    private static void Prefix(Mothership __instance, ref bool ____isRotationDeviated)
    //    {
    //        ____isRotationDeviated = false;
    //    }
    //}
    #endregion

    #region Entity

    #endregion Entity
}
