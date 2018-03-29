﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class UnitParametersTemplate : Object {

    public UnitMovementSettings defaultMovementSettings { get; private set; } //movement settings are separates as they may depends on area or landscape
    public List<UnitWeaponTemplate> weapons;

    private Dictionary<string, List<EffectContainer>> triggeredEffects;
    public float maximumHealth { get; private set; }

    public bool isSelectable; //can be selected and inspected by the user
    public bool isControllable; //can be controlled by faction owner

    public UnitParametersTemplate() {
        defaultMovementSettings = new UnitMovementSettings();
        weapons = new List<UnitWeaponTemplate>();
        triggeredEffects = new Dictionary<string, List<EffectContainer>>();
        maximumHealth = 100;
    }

    public UnitParametersTemplate(UnitMovementSettings defaultMovementSettings, float maximumHealth, List<UnitWeaponTemplate> weapons = null, bool selectable = true, bool controllable = true) {
        triggeredEffects = new Dictionary<string, List<EffectContainer>>();
        this.maximumHealth = maximumHealth;
        this.defaultMovementSettings = defaultMovementSettings;
        if (weapons != null) {
            this.weapons = new List<UnitWeaponTemplate>(weapons);
        } else {
            this.weapons = new List<UnitWeaponTemplate>();
        }

        isSelectable = selectable;
        isControllable = controllable;
    }

    public UnitParametersTemplate Copy() {
        UnitParametersTemplate copy = new UnitParametersTemplate();
        copy.defaultMovementSettings = defaultMovementSettings.Copy();
        copy.weapons = weapons.ConvertAll(weapon => weapon.Copy());
        copy.triggeredEffects = triggeredEffects; //HMM HMM HMM...
        copy.maximumHealth = maximumHealth;
        copy.isSelectable = isSelectable;
        copy.isControllable = isControllable;

        return copy;
    }

    public void AddEffectForEvent(string eventName, EffectContainer effect) {
        List<EffectContainer> list;
        if (triggeredEffects.ContainsKey(eventName)) {
            list = triggeredEffects[eventName];
            list.Add(effect);
        } else {
            list = new List<EffectContainer>{ effect };
            triggeredEffects.Add(eventName, list);
        }
    }

    public List<EffectContainer> EffectsForEvent(string eventName) {
        if (!triggeredEffects.ContainsKey(eventName)) {
            return null;
        } else {
            return triggeredEffects[eventName];
        }
    }

}
