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
      return;
    }

    
    
  }
}