﻿using HarmonyLib;
using Lamb.UI.PauseMenu;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WeaponSelector;

[HarmonyPatch]
internal class ScreenPatch
{
    [HarmonyPatch(typeof(UIPauseMenuController), nameof(UIPauseMenuController.Start))]
    [HarmonyPostfix]
    static void CreateScreen(UIPauseMenuController __instance)
    {
        Transform parentMenu = __instance.gameObject.transform.Find("PauseMenuContainer");

        GameObject screen = new GameObject();
        screen.name = "WeaponMenuContainer";
        screen.layer = LayerMask.NameToLayer("UI");
        screen.transform.SetParent(parentMenu);
        screen.transform.localPosition = new Vector3(650, 300, 0);
        screen.transform.localScale = Vector3.one;

        TextMeshProUGUI UItextMesh = parentMenu.Find("Left").Find("Transform").Find("MenuContainer").Find("Settings").Find("Text").GetComponent<TextMeshProUGUI>();

        // if this script is attached to Pause Menu(Clone)
        Canvas canvas = __instance.gameObject.GetComponent<Canvas>();

        WeaponMenu menu = screen.AddComponent<WeaponMenu>();
        menu.Parent = screen;
        menu.UIText = UItextMesh;
        menu.canvas = canvas;
    }
}
