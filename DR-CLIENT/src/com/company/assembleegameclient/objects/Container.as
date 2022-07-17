package com.company.assembleegameclient.objects
{

import com.company.util.GraphicsUtil;
import com.company.util.PointUtil;
import flash.display.BitmapData;
import flash.display.GraphicsBitmapFill;
import flash.display.GraphicsPath;
import flash.geom.Matrix;
import com.company.assembleegameclient.map.Camera;
import flash.display.IGraphicsData;
   import com.company.assembleegameclient.game.GameSprite;
   import com.company.assembleegameclient.map.Map;
   import com.company.assembleegameclient.sound.SoundEffectLibrary;
   import com.company.assembleegameclient.ui.panels.Panel;
   import com.company.assembleegameclient.ui.panels.itemgrids.ContainerGrid;
   import com.company.util.PointUtil;
import flash.events.Event;
import flash.events.KeyboardEvent;
import flash.events.MouseEvent;
import flash.events.TimerEvent;
import com.company.assembleegameclient.parameters.Parameters;


   public class Container extends GameObject implements IInteractiveObject
   {
       public var mouseOver_:Boolean = false;

       private var lastEquips:Vector.<int> = new <int>[0, 0, 0, 0, 0, 0, 0, 0];


       public var isLoot_:Boolean;
      
      public var ownerId_:int;
      
      public function Container(objectXML:XML)
      {
         super(objectXML);
         isInteractive_ = true;
         this.isLoot_ = objectXML.hasOwnProperty("Loot");
         this.ownerId_ = -1;
      }
      
      public function setOwnerId(ownerId:int) : void
      {
         this.ownerId_ = ownerId;
         isInteractive_ = this.ownerId_ < 0 || this.isBoundToCurrentAccount();
      }
      
      public function isBoundToCurrentAccount() : Boolean
      {
         return map_.player_.accountId_ == this.ownerId_;
      }
      
      override public function addTo(map:Map, x:Number, y:Number) : Boolean
      {
         if(!super.addTo(map,x,y))
         {
            return false;
         }
         if(map_.player_ == null)
         {
            return true;
         }
         var dist:Number = PointUtil.distanceXY(map_.player_.x_,map_.player_.y_,x,y);
         if(this.isLoot_ && dist < 10)
         {
            SoundEffectLibrary.play("loot_appears");
         }
         return true;
      }

       override public function draw(graphicsData:Vector.<IGraphicsData>, camera:Camera, _arg_3:int):void
       {
           super.draw(graphicsData, camera, _arg_3);
           if (Parameters.data_.lootPreview && this.mouseOver_)
           {
               drawItems(graphicsData);
           }
       }

       public function updateItemSprites(bitmapData:Vector.<BitmapData>):void
       {
           for (var i:int = 0; i < this.equipment_.length; i++)
           {
               var slot:int = this.equipment_[i];
               if (slot != -1)
               {
                   var itemIcon:BitmapData = ObjectLibrary.getItemIcon(slot);
                   bitmapData.push(itemIcon);
               }
           }
       }

       public function drawItems(graphicsData:Vector.<IGraphicsData>):void
       {
           var num1:Number = NaN;
           var num2:Number = NaN;
           var int1:int = 0;
           if (this.icons_ == null)
           {
               this.icons_ = new Vector.<BitmapData>();
               this.iconFills_ = new Vector.<GraphicsBitmapFill>();
               this.iconPaths_ = new Vector.<GraphicsPath>();
               this.icons_.length = 0;
               updateItemSprites(this.icons_);
           }
           else
           {
               if (!vectorsAreEqual(equipment_))
               {
                   this.icons_.length = 0;
                   lastEquips[0] = equipment_[0];
                   lastEquips[1] = equipment_[1];
                   lastEquips[2] = equipment_[2];
                   lastEquips[3] = equipment_[3];
                   lastEquips[4] = equipment_[4];
                   lastEquips[5] = equipment_[5];
                   lastEquips[6] = equipment_[6];
                   lastEquips[7] = equipment_[7];
                   updateItemSprites(this.icons_);
               }
           }
           for (var i:int = 0; i < this.icons_.length; i++)
           {
               var icon:BitmapData = this.icons_[i];
               if (i >= this.iconFills_.length)
               {
                   this.iconFills_.push(new GraphicsBitmapFill(null, new Matrix(), false, false));
                   this.iconPaths_.push(new GraphicsPath(GraphicsUtil.QUAD_COMMANDS, new Vector.<Number>()));
               }
               var iconFill:GraphicsBitmapFill = this.iconFills_[i];
               var iconPath:GraphicsPath = this.iconPaths_[i];
               iconFill.bitmapData = icon;
               int1 = i * 0.25;
               num1 = ((posS_[3] - (icon.width * 2)) + ((i % 4) * icon.width));
               num2 = (((this.vS_[1] - (icon.height * 0.5)) + (int1 * (icon.height + 5))) - ((int1 * 5) + 20));
               iconPath.data.length = 0;
               (iconPath.data as Vector.<Number>).push(num1, num2, num1 + icon.width, num2, num1 + icon.width, num2 + icon.height, num1, num2 + icon.height);
               var matrix:Matrix = iconFill.matrix;
               matrix.identity();
               matrix.translate(num1, num2);
               graphicsData.push(iconFill);
               graphicsData.push(iconPath);
               graphicsData.push(GraphicsUtil.END_FILL);
           }
       }

       public function vectorsAreEqual(vector:Vector.<int>):Boolean
       {
           return vector[0] == lastEquips[0] && vector[1] == lastEquips[1] && vector[2] == lastEquips[2] && vector[3] == lastEquips[3] && vector[4] == lastEquips[4] && vector[5] == lastEquips[5] && vector[6] == lastEquips[6] && vector[7] == lastEquips[7];
       }


       public function getPanel(gameSprite:GameSprite):Panel
       {
           var player:Player = (gameSprite && gameSprite.map) ? gameSprite.map.player_ : null;
           return new ContainerGrid(this, player);
       }
       private function onMouseOver(event:MouseEvent) : void
       {
           this.mouseOver_ = true;
       }

       private function onMouseOut(event:MouseEvent) : void
       {
           this.mouseOver_ = false;
       }

   }
}
