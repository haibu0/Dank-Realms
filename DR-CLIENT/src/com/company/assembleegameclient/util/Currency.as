package com.company.assembleegameclient.util
{
   public class Currency
   {
      
      public static const INVALID:int = -1;

      public static const GOLD:int = 0;

      public static const FAME:int = 1;
      
      public static const GUILD_FAME:int = 2;

       public static const SILVER:int = 3;



       public function Currency()
      {
         super();
      }
      
      public static function typeToName(type:int) : String
      {
         switch(type)
         {
             case SILVER:
                 return "Silver";
            case GOLD:
               return "Gold";
            case FAME:
               return "Fame";
            case GUILD_FAME:
               return "Guild Fame";
            default:
               return "";
         }
      }
   }
}
