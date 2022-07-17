package com.company.assembleegameclient.util
{
import com.company.assembleegameclient.util.redrawers.GlowRedrawer;
import com.company.util.AssetLibrary;
   import com.company.util.PointUtil;
   import flash.display.BitmapData;
   import flash.filters.BitmapFilterQuality;
   import flash.filters.GlowFilter;
   import flash.geom.Matrix;
   
   public class ConditionEffect
   {
      
      public static const NOTHING:uint = 0;
      
      public static const DEAD:uint = 1;
      
      public static const QUIET:uint = 2;
      
      public static const WEAK:uint = 3;
      
      public static const SLOWED:uint = 4;
      
      public static const SICK:uint = 5;
      
      public static const DAZED:uint = 6;
      
      public static const STUNNED:uint = 7;
      
      public static const BLIND:uint = 8;
      
      public static const HALLUCINATING:uint = 9;
      
      public static const DRUNK:uint = 10;
      
      public static const CONFUSED:uint = 11;
      
      public static const STUN_IMMUNE:uint = 12;
      
      public static const INVISIBLE:uint = 13;
      
      public static const PARALYZED:uint = 14;
      
      public static const SPEEDY:uint = 15;
      
      public static const BLEEDING:uint = 16;
      
      public static const NOT_USED:uint = 17;
      
      public static const HEALING:uint = 18;
      
      public static const DAMAGING:uint = 19;
      
      public static const BERSERK:uint = 20;
      
      public static const PAUSED:uint = 21;
      
      public static const STASIS:uint = 22;
      
      public static const STASIS_IMMUNE:uint = 23;
      
      public static const INVINCIBLE:uint = 24;
      
      public static const INVULNERABLE:uint = 25;
      
      public static const ARMORED:uint = 26;
      
      public static const ARMORBROKEN:uint = 27;
      
      public static const HEXED:uint = 28;
      
      public static const NINJA_SPEEDY:uint = 29;

       public static const UNSTABLE:uint = 30;

       public static const EXPOSED:uint = 31;

       public static const CURSE:uint = 32;

       public static const INSPIRED:uint = 33;

       public static const DEAD_BIT:uint = 1 << DEAD - 1;
      
      public static const QUIET_BIT:uint = 1 << QUIET - 1;
      
      public static const WEAK_BIT:uint = 1 << WEAK - 1;
      
      public static const SLOWED_BIT:uint = 1 << SLOWED - 1;
      
      public static const SICK_BIT:uint = 1 << SICK - 1;
      
      public static const DAZED_BIT:uint = 1 << DAZED - 1;
      
      public static const STUNNED_BIT:uint = 1 << STUNNED - 1;
      
      public static const BLIND_BIT:uint = 1 << BLIND - 1;
      
      public static const HALLUCINATING_BIT:uint = 1 << HALLUCINATING - 1;
      
      public static const DRUNK_BIT:uint = 1 << DRUNK - 1;
      
      public static const CONFUSED_BIT:uint = 1 << CONFUSED - 1;
      
      public static const STUN_IMMUNE_BIT:uint = 1 << STUN_IMMUNE - 1;
      
      public static const INVISIBLE_BIT:uint = 1 << INVISIBLE - 1;
      
      public static const PARALYZED_BIT:uint = 1 << PARALYZED - 1;
      
      public static const SPEEDY_BIT:uint = 1 << SPEEDY - 1;
      
      public static const BLEEDING_BIT:uint = 1 << BLEEDING - 1;
      
      public static const NOT_USED_BIT:uint = 1 << NOT_USED - 1;
      
      public static const HEALING_BIT:uint = 1 << HEALING - 1;
      
      public static const DAMAGING_BIT:uint = 1 << DAMAGING - 1;
      
      public static const BERSERK_BIT:uint = 1 << BERSERK - 1;
      
      public static const PAUSED_BIT:uint = 1 << PAUSED - 1;
      
      public static const STASIS_BIT:uint = 1 << STASIS - 1;
      
      public static const STASIS_IMMUNE_BIT:uint = 1 << STASIS_IMMUNE - 1;
      
      public static const INVINCIBLE_BIT:uint = 1 << INVINCIBLE - 1;
      
      public static const INVULNERABLE_BIT:uint = 1 << INVULNERABLE - 1;
      
      public static const ARMORED_BIT:uint = 1 << ARMORED - 1;
      
      public static const ARMORBROKEN_BIT:uint = 1 << ARMORBROKEN - 1;

       public static const EXPOSED_BIT:uint = 1 << EXPOSED - 1;

       public static const HEXED_BIT:uint = 1 << HEXED - 1;
      
      public static const NINJA_SPEEDY_BIT:uint = 1 << NINJA_SPEEDY - 1;

       public static const UNSTABLE_BIT:uint = 1 << UNSTABLE - 1;

       public static const CURSE_BIT:uint = 1 << CURSE - 1;

       public static const INSPIRED_BIT:uint = 1 << INSPIRED - 1;



       public static const MAP_FILTER_BITMASK:uint = DRUNK_BIT | BLIND_BIT | PAUSED_BIT;
      public static var effects_:Vector.<ConditionEffect> = new <ConditionEffect>[new ConditionEffect("Nothing",0,null),new ConditionEffect("Dead",DEAD_BIT,null),new ConditionEffect("Quiet",QUIET_BIT,[32, 33]),new ConditionEffect("Weak",WEAK_BIT,[34,35,36,37]),new ConditionEffect("Slowed",SLOWED_BIT,[1]),new ConditionEffect("Sick",SICK_BIT,[39]),new ConditionEffect("Dazed",DAZED_BIT,[44]),new ConditionEffect("Stunned",STUNNED_BIT,[45]),new ConditionEffect("Blind",BLIND_BIT,[41]),new ConditionEffect("Hallucinating",HALLUCINATING_BIT,[42]),new ConditionEffect("Drunk",DRUNK_BIT,[43]),new ConditionEffect("Confused",CONFUSED_BIT,[2]),new ConditionEffect("Stun Immune",STUN_IMMUNE_BIT,null),new ConditionEffect("Invisible",INVISIBLE_BIT,null),new ConditionEffect("Paralyzed",PARALYZED_BIT,[53,54]),new ConditionEffect("Speedy",SPEEDY_BIT,[0]),new ConditionEffect("Bleeding",BLEEDING_BIT,[46]),new ConditionEffect("Not Used",NOT_USED_BIT,null),new ConditionEffect("Healing",HEALING_BIT,[47]),new ConditionEffect("Damaging",DAMAGING_BIT,[49]),new ConditionEffect("Berserk",BERSERK_BIT,[50]),new ConditionEffect("Paused",PAUSED_BIT,null),new ConditionEffect("Stasis",STASIS_BIT,null),new ConditionEffect("Stasis Immune",STASIS_IMMUNE_BIT,null),new ConditionEffect("Invincible",INVINCIBLE_BIT,null),new ConditionEffect("Invulnerable",INVULNERABLE_BIT,[17]),new ConditionEffect("Armored",ARMORED_BIT,[16]),new ConditionEffect("Armor Broken",ARMORBROKEN_BIT,[55]),new ConditionEffect("Hexed",HEXED_BIT,[42]), new ConditionEffect("Unstable",UNSTABLE_BIT,[56]),new ConditionEffect("Curse",CURSE_BIT,[57]),new ConditionEffect("Exposed",EXPOSED_BIT,[58]),new ConditionEffect("Inspired",INSPIRED_BIT,[59]), new ConditionEffect("Ninja Speedy",NINJA_SPEEDY_BIT,[0])];
      private static var conditionEffectFromName_:Object = null;
      private static var bitToIcon_:Object = null;
      public static const GLOW_FILTER:GlowFilter = new GlowFilter(0,0.3,6,6,2,BitmapFilterQuality.LOW,false,false);



      public var name_:String;
      
      public var bit_:uint;
      
      public var iconOffsets_:Array;
      
      public function ConditionEffect(name:String, bit:uint, iconOffsets:Array)
      {
         super();
         this.name_ = name;
         this.bit_ = bit;
         this.iconOffsets_ = iconOffsets;
      }
      
      public static function getConditionEffectFromName(name:String) : uint
      {
         var ce:uint = 0;
         if(conditionEffectFromName_ == null)
         {
            conditionEffectFromName_ = new Object();
            for(ce = 0; ce < effects_.length; ce++)
            {
               conditionEffectFromName_[effects_[ce].name_] = ce;
            }
         }
         return conditionEffectFromName_[name];
      }
      
      public static function getConditionEffectIcons(condition:uint, icons:Vector.<BitmapData>, index:int) : void
      {
         var newCondition:uint = 0;
         var bit:uint = 0;
         var iconList:Vector.<BitmapData> = null;
         while(condition != 0)
         {
            newCondition = condition & condition - 1;
            bit = condition ^ newCondition;
            iconList = getIconsFromBit(bit);
            if(iconList != null)
            {
               icons.push(iconList[index % iconList.length]);
            }
            condition = newCondition;
         }
      }
      
      private static function getIconsFromBit(bit:uint) : Vector.<BitmapData>
      {
         var drawMatrix:Matrix = null;
         var ce:uint = 0;
         var icons:Vector.<BitmapData> = null;
         var i:int = 0;
         var icon:BitmapData = null;
         if(bitToIcon_ == null)
         {
            bitToIcon_ = new Object();
            drawMatrix = new Matrix();
            drawMatrix.translate(4,4);
            for(ce = 0; ce < effects_.length; ce++)
            {
               icons = null;
               if(effects_[ce].iconOffsets_ != null)
               {
                  icons = new Vector.<BitmapData>();
                  for(i = 0; i < effects_[ce].iconOffsets_.length; i++)
                  {
                     icon = new BitmapData(16,16,true,0);
                     icon.draw(AssetLibrary.getImageFromSet("lofiInterface2",effects_[ce].iconOffsets_[i]),drawMatrix);
                     icon = GlowRedrawer.outlineGlow(icon,4294967295);
                     icon.applyFilter(icon,icon.rect,PointUtil.ORIGIN,GLOW_FILTER);
                     icons.push(icon);
                  }
               }
               bitToIcon_[effects_[ce].bit_] = icons;
            }
         }
         return bitToIcon_[bit];
      }
   }
}
