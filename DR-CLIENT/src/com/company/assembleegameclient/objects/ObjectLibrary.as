package com.company.assembleegameclient.objects
{
   import com.company.assembleegameclient.objects.animation.AnimationsData;
import com.company.util.AssetLibrary;
   import com.company.util.ConversionUtil;
   import flash.utils.Dictionary;
   import flash.utils.getDefinitionByName;
   import kabam.rotmg.constants.GeneralConstants;
   import kabam.rotmg.constants.ItemConstants;
   import kabam.rotmg.messaging.impl.data.StatData;
import flash.geom.Matrix;
import flash.display.BitmapData;
import com.company.assembleegameclient.util.TextureRedrawer;
import com.company.assembleegameclient.util.redrawers.GlowRedrawer;
import com.company.util.PointUtil;
import com.company.assembleegameclient.util.ConditionEffect;




public class ObjectLibrary
   {
      public static var playerChars_:Vector.<XML> = new Vector.<XML>();
      public static var hexTransforms_:Vector.<XML> = new Vector.<XML>();
      public static var playerClassAbbr_:Dictionary = new Dictionary();
      public static const propsLibrary_:Dictionary = new Dictionary();
      public static const xmlLibrary_:Dictionary = new Dictionary();
      public static const idToType_:Dictionary = new Dictionary();
      public static const typeToDisplayId_:Dictionary = new Dictionary();
      public static const typeToTextureData_:Dictionary = new Dictionary();
      public static const typeToTopTextureData_:Dictionary = new Dictionary();
      public static const typeToAnimationsData_:Dictionary = new Dictionary();
      public static const defaultProps_:ObjectProperties = new ObjectProperties(null);
      
      public static const TYPE_MAP:Object = {
         "CaveWall":CaveWall,
         "Character":Character,
         "CharacterChanger":CharacterChanger,
         "ClosedVaultChest":ClosedVaultChest,
         "ConnectedWall":ConnectedWall,
         "Container":Container,
         "GameObject":GameObject,
         "GuildBoard":GuildBoard,
         "GuildChronicle":GuildChronicle,
         "GuildHallPortal":GuildHallPortal,
         "GuildMerchant":GuildMerchant,
         "GuildRegister":GuildRegister,
         "Merchant":Merchant,
         "MoneyChanger":MoneyChanger,
         "NameChanger":NameChanger,
         "ReskinVendor":ReskinVendor,
         "OneWayContainer":OneWayContainer,
         "Player":Player,
         "Portal":Portal,
         "Projectile":Projectile,
         "Sign":Sign,
         "SpiderWeb":SpiderWeb,
         "Stalagmite":Stalagmite,
         "Wall":Wall
      };
       
      
      public function ObjectLibrary()
      {
         super();
      }
      
      public static function parseFromXML(xml:XML) : void
      {
         var objectXML:XML = null;
         var id:String = null;
         var displayId:String = null;
         var objectType:int = 0;
         var found:Boolean = false;
         var i:int = 0;
         for each(objectXML in xml.Object)
         {
            id = String(objectXML.@id);
            displayId = id;
            if(objectXML.hasOwnProperty("DisplayId"))
            {
               displayId = objectXML.DisplayId;
            }
            if(objectXML.hasOwnProperty("Group"))
            {
               if(objectXML.Group == "Hexable")
               {
                  hexTransforms_.push(objectXML);
               }
            }
            objectType = int(objectXML.@type);
            propsLibrary_[objectType] = new ObjectProperties(objectXML);
            xmlLibrary_[objectType] = objectXML;
            idToType_[id] = objectType;
            typeToDisplayId_[objectType] = displayId;
            if(String(objectXML.Class) == "Player")
            {
               playerClassAbbr_[objectType] = String(objectXML.@id).substr(0,2);
               found = false;
               for(i = 0; i < playerChars_.length; i++)
               {
                  if(int(playerChars_[i].@type) == objectType)
                  {
                     playerChars_[i] = objectXML;
                     found = true;
                  }
               }
               if(!found)
               {
                  playerChars_.push(objectXML);
               }
            }
            typeToTextureData_[objectType] = new TextureData(objectXML);
            if(objectXML.hasOwnProperty("Top"))
            {
               typeToTopTextureData_[objectType] = new TextureData(XML(objectXML.Top));
            }
            if(objectXML.hasOwnProperty("Animation"))
            {
               typeToAnimationsData_[objectType] = new AnimationsData(objectXML);
            }
         }
      }
      
      public static function getIdFromType(type:int) : String
      {
         var objectXML:XML = xmlLibrary_[type];
         if(objectXML == null)
         {
            return null;
         }
         return String(objectXML.@id);
      }
      
      public static function getPropsFromId(id:String) : ObjectProperties
      {
         var objectType:int = idToType_[id];
         return propsLibrary_[objectType];
      }
      
      public static function getXMLfromId(id:String) : XML
      {
         var objectType:int = idToType_[id];
         return xmlLibrary_[objectType];
      }
      
      public static function getObjectFromType(objectType:int) : GameObject
      {
         var objectXML:XML = xmlLibrary_[objectType];
         var typeReference:String = objectXML.Class;
         var typeClass:Class = TYPE_MAP[typeReference] || makeClass(typeReference);
         return new typeClass(objectXML);
      }
      
      private static function makeClass(typeReference:String) : Class
      {
         var typeName:String = "com.company.assembleegameclient.objects." + typeReference;
         return getDefinitionByName(typeName) as Class;
      }
      
      public static function getTextureFromType(objectType:int) : BitmapData
      {
         var textureData:TextureData = typeToTextureData_[objectType];
         if(textureData == null)
         {
            return null;
         }
         return textureData.getTexture();
      }

       public static function getRedrawnTextureFromType(objectType:int, size:int, includeBottom:Boolean, useCaching:Boolean = true, scaleValue:int = 5) : BitmapData
       {
           var textureData:TextureData = typeToTextureData_[objectType];
           var texture:BitmapData = Boolean(textureData)?textureData.getTexture():null;
           if(texture == null)
           {
               texture = AssetLibrary.getImageFromSet("lofiObj3",255);
           }
           var mask:BitmapData = Boolean(textureData)?textureData.mask_:null;
           if(mask == null)
           {
               return TextureRedrawer.redraw(texture,size,includeBottom,0,useCaching,scaleValue);
           }
           var objectXML:XML = xmlLibrary_[objectType];
           if(texture == null && objectXML.hasOwnProperty("Mistake"))
           {
               return TextureRedrawer.redraw(texture,size,includeBottom,14603786,useCaching,scaleValue);
           }
           var tex1:int = Boolean(objectXML.hasOwnProperty("Tex1"))?int(int(objectXML.Tex1)):int(0);
           var tex2:int = Boolean(objectXML.hasOwnProperty("Tex2"))?int(int(objectXML.Tex2)):int(0);
           texture = TextureRedrawer.resize(texture,mask,size,includeBottom,tex1,tex2);
           texture = GlowRedrawer.outlineGlow(texture,0);
           return texture;
       }
      public static function getSizeFromType(objectType:int) : int
      {
         var objectXML:XML = xmlLibrary_[objectType];
         if(!objectXML.hasOwnProperty("Size"))
         {
            return 100;
         }
         return int(objectXML.Size);
      }
      
      public static function getSlotTypeFromType(objectType:int) : int
      {
         var objectXML:XML = xmlLibrary_[objectType];
         if(!objectXML.hasOwnProperty("SlotType"))
         {
            return -1;
         }
         return int(objectXML.SlotType);
      }
      
      public static function isEquippableByPlayer(objectType:int, player:Player) : Boolean
      {
         if(objectType == ItemConstants.NO_ITEM)
         {
            return false;
         }
         var objectXML:XML = xmlLibrary_[objectType];
         var slotType:int = int(objectXML.SlotType.toString());
         for(var i:uint = 0; i < GeneralConstants.NUM_EQUIPMENT_SLOTS; i++)
         {
            if(player.slotTypes_[i] == slotType)
            {
               return true;
            }
         }
         return false;
      }
      
      public static function getMatchingSlotIndex(objectType:int, player:Player) : int
      {
         var objectXML:XML = null;
         var slotType:int = 0;
         var i:uint = 0;
         if(objectType != ItemConstants.NO_ITEM)
         {
            objectXML = xmlLibrary_[objectType];
            slotType = int(objectXML.SlotType);
            for(i = 0; i < GeneralConstants.NUM_EQUIPMENT_SLOTS; i++)
            {
               if(player.slotTypes_[i] == slotType)
               {
                  return i;
               }
            }
         }
         return -1;
      }
      
      public static function isUsableByPlayer(objectType:int, player:Player) : Boolean
      {
         if(player == null)
         {
            return true;
         }
         var objectXML:XML = xmlLibrary_[objectType];
         if(objectXML == null || !objectXML.hasOwnProperty("SlotType"))
         {
            return false;
         }
         var slotType:int = objectXML.SlotType;
         if(slotType == ItemConstants.POTION_TYPE)
         {
            return true;
         }
         for(var i:int = 0; i < player.slotTypes_.length; i++)
         {
            if(player.slotTypes_[i] == slotType)
            {
               return true;
            }
         }
         return false;
      }
      
      public static function isSoulbound(objectType:int) : Boolean
      {
         var objectXML:XML = xmlLibrary_[objectType];
         return objectXML != null && objectXML.hasOwnProperty("Soulbound");
      }
      
      public static function usableBy(objectType:int) : Vector.<String>
      {
         var playerXML:XML = null;
         var slotTypes:Vector.<int> = null;
         var i:int = 0;
         var objectXML:XML = xmlLibrary_[objectType];
         if(objectXML == null || !objectXML.hasOwnProperty("SlotType"))
         {
            return null;
         }
         var slotType:int = objectXML.SlotType;
         if(slotType == ItemConstants.POTION_TYPE || slotType == ItemConstants.RING_TYPE)
         {
            return null;
         }
         var usable:Vector.<String> = new Vector.<String>();
         for each(playerXML in playerChars_)
         {
            slotTypes = ConversionUtil.toIntVector(playerXML.SlotTypes);
            for(i = 0; i < slotTypes.length; i++)
            {
               if(slotTypes[i] == slotType)
               {
                  usable.push(typeToDisplayId_[int(playerXML.@type)]);
                  break;
               }
            }
         }
         return usable;
      }
      
      public static function playerMeetsRequirements(objectType:int, player:Player) : Boolean
      {
         var reqXML:XML = null;
         if(player == null)
         {
            return true;
         }
         var objectXML:XML = xmlLibrary_[objectType];
         for each(reqXML in objectXML.EquipRequirement)
         {
            if(!playerMeetsRequirement(reqXML,player))
            {
               return false;
            }
         }
         return true;
      }
      
      public static function playerMeetsRequirement(reqXML:XML, p:Player) : Boolean
      {
         var val:int = 0;
         if(reqXML.toString() == "Stat")
         {
            val = int(reqXML.@value);
            switch(int(reqXML.@stat))
            {
               case StatData.MAX_HP_STAT:
                  return p.maxHP_ >= val;
               case StatData.MAX_MP_STAT:
                  return p.maxMP_ >= val;
               case StatData.LEVEL_STAT:
                  return p.level_ >= val;
               case StatData.ATTACK_STAT:
                  return p.attack_ >= val;
               case StatData.DEFENSE_STAT:
                  return p.defense_ >= val;
               case StatData.SPEED_STAT:
                  return p.speed_ >= val;
               case StatData.VITALITY_STAT:
                  return p.vitality_ >= val;
               case StatData.WISDOM_STAT:
                  return p.wisdom_ >= val;
               case StatData.DEXTERITY_STAT:
                  return p.dexterity_ >= val;
            }
         }
         return false;
      }

       public static function getItemIcon(param1:int) : BitmapData
       {
           var _loc7_:* = null;
           var _loc3_:* = null;
           var _loc2_:* = null;
           var _loc8_:* = null;
           var _loc10_:* = null;
           var _loc6_:int = 0;
           var _loc9_:int = 0;
           var _loc4_:* = null;
           var _loc5_:Matrix = new Matrix();
           if(param1 == -1)
           {
               _loc7_ = scaleBitmapData(AssetLibrary.getImageFromSet("lofiInterface",7),2);
               _loc5_.translate(4,4);
               _loc3_ = new BitmapData(22,22,true,0);
               _loc3_.draw(_loc7_,_loc5_);
               return _loc3_;
           }
           _loc2_ = xmlLibrary_[param1];
           _loc8_ = typeToTextureData_[param1];
           _loc10_ = !!_loc8_?_loc8_.mask_:null;
           _loc6_ = "Tex1" in _loc2_?int(_loc2_.Tex1):0;
           _loc9_ = "Tex2" in _loc2_?int(_loc2_.Tex2):0;
           _loc4_ = getTextureFromType(param1);
           if(_loc6_ > 0 || _loc9_ > 0)
           {
               _loc4_ = TextureRedrawer.retexture(_loc4_,_loc10_,_loc6_,_loc9_);
               _loc5_.scale(0.2,0.2);
           }
           _loc7_ = scaleBitmapData(_loc4_,_loc4_.rect.width == 16?1:2);
           _loc5_.translate(4,4);
           _loc3_ = new BitmapData(22,22,true,0);
           _loc3_.draw(_loc7_,_loc5_);
           _loc3_ = GlowRedrawer.outlineGlow(_loc3_,0);
           _loc3_.applyFilter(_loc3_,_loc3_.rect,PointUtil.ORIGIN,ConditionEffect.GLOW_FILTER);
           return _loc3_;
       }

       public static function scaleBitmapData(_arg_1:BitmapData, _arg_2:Number):BitmapData
       {
           _arg_2 = Math.abs(_arg_2);
           var _local_4:int = ((_arg_1.width * _arg_2) || (1));
           var _local_6:int = ((_arg_1.height * _arg_2) || (1));
           var _local_3:BitmapData = new BitmapData(_local_4, _local_6, true, 0);
           var _local_5:Matrix = new Matrix();
           _local_5.scale(_arg_2, _arg_2);
           _local_3.draw(_arg_1, _local_5);
           return (_local_3);
       }
   }
}
