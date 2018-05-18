using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public enum WeaponType
{
  SMG, SNIPER_RIFLE, ASSAULT_RIFLE, LMG, HMG, SHOTGUN, PISTOL, RPG, GRENADE_LAUNCHER,
  BOW, CROSSBOW, NET_LAUNCHER, POLEARM, GAUNTLETS, DAGGER, THROWING_KNIVES, SHIELD, BLADE
}

public enum WeaponComponentType{
  STOCK,
  MAGAZINE,
  SIGHT,
  BODY,
  MUZZLE,
  UNDERBARREL
}

public enum WeaponComponentModel{
  FULL,
  SHORT,
  SILENCER,
  COMPENSATOR,
  MUZZLEBREAK,
  FORWARDGRIP,
  ACOG,
  REFLEX,
  HOLOSIGHT,
  EXTENDED,
  QUICKDRAW
}

public struct WeaponComponent{
  public WeaponComponentType socket;
  public WeaponComponentModel model;
}

public struct AttributeModifier{
  public string attr;
  public float lowerLim;
  public float upperLim;
  
  public AttributeModifier (string tar, float lower, float upper){
    attr = tar;
    lowerLim = lower;
    upperLim = upper;
  }
}

[Serializable]
public class Weapon_Class
{
  [FoldoutGroup("Base Attributes")]
  public string weaponName;

  [FoldoutGroup("Base Attributes")]
  public WeaponType wepType;

  [FoldoutGroup("Base Attributes")]
  public float damage;

  public static Dictionary<WeaponComponentType, WeaponComponentModel> ComponentDictionary;
  public static Dictionary<WeaponComponentModel, AttributeModifier> AttributeDictionary;

  public Weapon_Class()
  {
    if (ComponentDictionary == null){
      //SetupDictionary();
    }
    weaponName = "N/A";
    wepType = WeaponType.SMG;
    damage = 0;
  }

  public Weapon_Class(string name){
    weaponName = name;
  }

  //public static void SetupDictionary(){
  //  ComponentDictionary = new Dictionary<WeaponComponentType, WeaponComponentModel>();
  //  AttributeDictionary = new Dictionary<WeaponComponentModel, AttributeModifier>();
    
  //  //Stock Models
  //  ComponentDictionary.Add(WeaponComponentType.STOCK, WeaponComponentModel.FULL);
  //  ComponentDictionary.Add(WeaponComponentType.STOCK, WeaponComponentModel.SHORT);
    
  //  //Magazine Models
  //  ComponentDictionary.Add(WeaponComponentType.MAGAZINE, WeaponComponentModel.EXTENDED);
  //  ComponentDictionary.Add(WeaponComponentType.MAGAZINE, WeaponComponentModel.QUICKDRAW);

  //  //Limits
  //  AttributeDictionary.Add(WeaponComponentModel.EXTENDED, new AttributeModifier("MaxAmmoCount", 35, 55));
  //}
}

[Serializable]
public class SMG : Weapon_Class
{
  int MaxAmmoCount;
  public SMG() : base()
  {
    weaponName = "Submachine Gun";
    wepType = WeaponType.SMG;
    damage = 1;
    MaxAmmoCount = 30;
  }

  public SMG(string name, List<KeyValuePair<WeaponComponentType, WeaponComponentModel>> componentList) : base(name){
    
  }
}

[Serializable]
public class SniperRifle : Weapon_Class
{
  public SniperRifle() : base()
  {
    weaponName = "Sniper Rifle";
    wepType = WeaponType.SNIPER_RIFLE;
    damage = 1;
  }
}