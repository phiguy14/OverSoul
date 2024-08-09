using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHUD
{
    VisualElement root;
    Label arrow;
    ProgressBar healthBar;
    ProgressBar staminaBar;
    public UIHUD(UIDocument uIDocument)
    {
        root = uIDocument.rootVisualElement;
        arrow = root.Q<Label>("ArrowCount");
        healthBar = root.Q<ProgressBar>("HealthBar");
        staminaBar = root.Q<ProgressBar>("StaminaBar");
        staminaBar.highValue = Stats.maxStats[Stat.SP];
        staminaBar.lowValue = 0;
        healthBar.highValue = Stats.maxStats[Stat.HP];
        healthBar.lowValue = 0;
        arrow.text = Stats.playerStats[Stat.Arrow].ToString();
        Stats.playerStats.CollectionChanged += changeStatDisplay;
        Stats.onLVChange += LevelUp;
    }
    public void Unregister()
    {
        Stats.playerStats.CollectionChanged -= changeStatDisplay;
        Stats.onLVChange -= LevelUp;
    }
    public void LevelUp()
    {
        healthBar.highValue = Stats.maxStats[Stat.HP];
        staminaBar.highValue = Stats.maxStats[Stat.SP];
    }

    public void changeStatDisplay(object sender, EventArgs e)
    {
        var statEventArgs = (StatEventArgs)e;
        switch (statEventArgs.stat)
        {
            case Stat.Arrow:
                arrow.text = Stats.playerStats[Stat.Arrow].ToString();
                break;
            case Stat.HP:
                healthBar.value = Stats.playerStats[Stat.HP];
                break;
            case Stat.SP:
                staminaBar.value = Stats.playerStats[Stat.SP];
                break;
            default:
                break;
        }
    }
}
