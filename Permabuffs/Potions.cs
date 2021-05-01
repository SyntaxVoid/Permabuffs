using System.Collections.Generic;

namespace Permabuffs
{
  /// <Summary>
  /// I'm sure there's a better way to do this... but this is used to create
  /// a dictionary mapping all of the potion names to their respective buff IDs.
  /// </Summary>
  public class NameToBuffIDs
  {
    public static Dictionary<string, int> BuffIDs 
      = new Dictionary<string, int>();
    public static void PopulateBuffIDs()
    {
      // BUFF POTIONS
      BuffIDs.Add("Ammo Reservation Potion", 112);
      BuffIDs.Add("Archery Potion", 16);
      BuffIDs.Add("Battle Potion", 13);
      BuffIDs.Add("Builder Potion", 107);
      BuffIDs.Add("Calming Potion", 106);
      BuffIDs.Add("Crate Potion", 123);
      BuffIDs.Add("Dangersense Potion", 111);
      BuffIDs.Add("Endurance Potion", 114);
      BuffIDs.Add("Featherfall Potion", 8);
      BuffIDs.Add("Fishing Potion",  121);
      BuffIDs.Add("Flipper Potion", 109);
      BuffIDs.Add("Gills Potion", 4);
      BuffIDs.Add("Gravitation Potion", 18);
      BuffIDs.Add("Greater Luck Potion", 257);
      BuffIDs.Add("Heartreach Potion", 105);
      BuffIDs.Add("Hunter Potion", 17);
      BuffIDs.Add("Inferno Potion", 116);
      BuffIDs.Add("Invisibility Potion", 10);
      BuffIDs.Add("Ironskin Potion", 5);
      BuffIDs.Add("Lesser Luck Potion", 257);
      BuffIDs.Add("Lifeforce Potion", 113);
      BuffIDs.Add("Luck Potion", 257);
      BuffIDs.Add("Magic Power Potion", 7);
      BuffIDs.Add("Mana Regeneration Potion", 6);
      BuffIDs.Add("Mining Potion", 104);
      BuffIDs.Add("Night Owl Potion", 12);
      BuffIDs.Add("Obsidian Skin Potion", 1);
      BuffIDs.Add("Rage Potion", 115);
      BuffIDs.Add("Regeneration Potion", 2);
      BuffIDs.Add("Shine Potion", 11);
      BuffIDs.Add("Sonar Potion", 122);
      BuffIDs.Add("Spelunker Potion", 9);
      BuffIDs.Add("Stink Potion", 371);
      BuffIDs.Add("Summoning Potion", 110);
      BuffIDs.Add("Swiftness Potion",  3);
      BuffIDs.Add("Thorns Potion", 14);
      BuffIDs.Add("Titan Potion", 108);
      BuffIDs.Add("Warmth Potion", 124);
      BuffIDs.Add("Water Walking Potion", 15);
      BuffIDs.Add("Wrath Potion", 117);
      BuffIDs.Add("Flask of Cursed Flames", 73);
      BuffIDs.Add("Flask of Fire", 74);
      BuffIDs.Add("Flask of Gold", 75);
      BuffIDs.Add("Flask of Ichor", 76);
      BuffIDs.Add("Flask of Nanites", 77);
      BuffIDs.Add("Flask of Party", 78);
      BuffIDs.Add("Flask of Poison", 79);
      BuffIDs.Add("Flask of Venom", 71);

      // WELL FED
      BuffIDs.Add("Apple Juice", 26);
      BuffIDs.Add("Bloody Moscato", 26);
      BuffIDs.Add("Bunny Stew", 26);
      BuffIDs.Add("Cooked Fish", 26);
      BuffIDs.Add("Frozen Banana Daiquiri", 26);
      BuffIDs.Add("Fruit Juice", 26);
      BuffIDs.Add("Fruit Salad", 26);
      BuffIDs.Add("Grilled Squirrel", 26);
      BuffIDs.Add("Lemonade", 26);
      BuffIDs.Add("Peach Sangria", 26);
      // BuffIDs.Add("Pi%C3%B1a Colada", 26); // Check actual in game name
      BuffIDs.Add("Roasted Bird", 26);
      BuffIDs.Add("Smoothie of Darkness", 26);
      BuffIDs.Add("Tropical Smoothie", 26);
      BuffIDs.Add("Teacup", 26);
      BuffIDs.Add("Apple", 26);
      BuffIDs.Add("Apricot", 26);
      BuffIDs.Add("Banana", 26);
      BuffIDs.Add("Blackcurrant", 26);
      BuffIDs.Add("Blood Orange", 26);
      BuffIDs.Add("Cherry", 26);
      BuffIDs.Add("Coconut", 26);
      BuffIDs.Add("Elderberry", 26);
      BuffIDs.Add("Grapefruit", 26);
      BuffIDs.Add("Lemon", 26);
      BuffIDs.Add("Mango", 26);
      BuffIDs.Add("Peach", 26);
      BuffIDs.Add("Pineapple", 26);
      BuffIDs.Add("Plum", 26);
      BuffIDs.Add("Rambutan", 26);
      BuffIDs.Add("Carton of Milk", 26);
      BuffIDs.Add("Potato Chips", 26);
      BuffIDs.Add("Shucked Oyster", 26);
      BuffIDs.Add("Marshmallow", 26);

      // PLENTY SATISFIED
      BuffIDs.Add("Grub Soup", 206);
      BuffIDs.Add("Bowl of Soup", 206);
      BuffIDs.Add("Cooked Shrimp", 206);
      BuffIDs.Add("Pumpkin Pie", 206);
      BuffIDs.Add("Sashimi", 206);
      BuffIDs.Add("Escargot", 206);
      BuffIDs.Add("Lobster Tail", 206);
      BuffIDs.Add("Prismatic Punch", 206);
      BuffIDs.Add("Roasted Duck", 206);
      BuffIDs.Add("Sauteed Frog Legs", 206);
      BuffIDs.Add("Pho", 206);
      BuffIDs.Add("Pad Thai", 206);
      BuffIDs.Add("Dragon Fruit", 206);
      BuffIDs.Add("Star Fruit", 206);
      BuffIDs.Add("Banana Split", 206);
      BuffIDs.Add("Chicken Nugget", 206);
      BuffIDs.Add("Chocolate Chip Cookie", 206);
      BuffIDs.Add("Coffee", 206);
      BuffIDs.Add("Cream Soda", 206);
      BuffIDs.Add("Fried Egg", 206);
      BuffIDs.Add("Fries", 206);
      BuffIDs.Add("Grapes", 206);
      BuffIDs.Add("Hotdog", 206);
      BuffIDs.Add("Ice Cream", 206);
      BuffIDs.Add("Nachos", 206);
      // BuffIDs.Add("Shrimp Po%27 Boy", 206); // Check actual in game name

      // EXQUISITELY STUFFED
      BuffIDs.Add("Golden Delight", 207);
      BuffIDs.Add("Grape Juice", 207);
      BuffIDs.Add("Seafood Dinner", 207);
      BuffIDs.Add("Bacon", 207);
      BuffIDs.Add("Christmas Pudding", 207);
      BuffIDs.Add("Gingerbread Cookie", 207);
      BuffIDs.Add("Sugar Cookie", 207);
      BuffIDs.Add("Apple Pie", 207);
      BuffIDs.Add("BBQ Ribs", 207);
      BuffIDs.Add("Burger", 207);
      BuffIDs.Add("Milkshake", 207);
      BuffIDs.Add("Pizza", 207);
      BuffIDs.Add("Spaghetti", 207);
      BuffIDs.Add("Steak", 207);

      // TIPSY
      BuffIDs.Add("Sake", 25);
      BuffIDs.Add("Ale", 25);


      return;
    }

    
    
  }
}