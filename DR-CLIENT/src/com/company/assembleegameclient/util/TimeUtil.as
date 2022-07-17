package com.company.assembleegameclient.util
{
import flash.utils.getTimer;

public class TimeUtil
   {

       public static const DAY_IN_MS:int = 86400000;
       public static const DAY_IN_S:int = 86400;
       public static const HOUR_IN_S:int = 3600;
       public static const MIN_IN_S:int = 60;


       public static function getTrueTime() : int {
           return getTimer();
       }
       public function TimeUtil()
      {
         super();
      }

      public static function secondsToDays(time:Number) : Number
      {
         return time / DAY_IN_S;
      }
   }
}
