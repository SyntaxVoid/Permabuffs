using System;
using System.Timers;
using TShockAPI;
using TShockAPI.Hooks;
using Terraria;
using TerrariaApi.Server;

namespace Permabuffs
{
  [ApiVersion(2, 1)]
  public class Permabuffs : TerrariaPlugin
  {
    public override string Author => "Myoni(SyntaxVoid)";
    public override string Description => "Store 30 of a potion in your "
                                        + "piggy bank to unlock a "
                                        + "permanent version of the buff!";
    public override string Name => "Permabuffs";
    public override Version Version => new Version(1, 1, 1, 0);
  
    private static bool ssc_enabled;
    private static Timer refresh_timer;


    public Permabuffs(Main game) : base(game)
    {
      base.Order = 1;
    }

    /// <Summary>
    /// Handles plugin initialization. 
    /// Fired when the server is started and the plugin is being loaded.
    /// You may register hooks, perform loading procedures, etc. here.
    /// </Summary>
    public override void Initialize()
    {
      ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
      ServerApi.Hooks.NetGreetPlayer.Register(this, OnGreet);
      AccountHooks.AccountDelete += OnAccountDelete;
      PlayerHooks.PlayerPostLogin += OnPostLogin;
      PlayerHooks.PlayerLogout += OnPostLogout;
    }

    /// <Summary>
    /// Handles plugin disposal logic.
    /// *Supposed* to fire when the server shuts down.
    /// You should deregister hooks and free all resources here.
    /// </Summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
        ServerApi.Hooks.NetGreetPlayer.Deregister(this, OnGreet);
        AccountHooks.AccountDelete -= OnAccountDelete;
        PlayerHooks.PlayerPostLogin -= OnPostLogin;
        PlayerHooks.PlayerLogout -= OnPostLogout;
      }
      base.Dispose(disposing);
    }

    /// <Summary>
    /// Performs the initialization procedures and connects to the database
    /// containing the Permabuff info. Schedules the rebuff method to run every
    /// 30 seconds. Lasatly, creates the player commands to control permabuffs.
    /// </Summary>
    public void OnInitialize(EventArgs args)
    {
      NameToBuffIDs.PopulateBuffIDs();
      Version cur = TShock.VersionNum;
      Version old = new Version("4.5"); 
      if (cur.CompareTo(old) <= 0)
      {
        Console.WriteLine("[PermaBuffs]: Error - You are using an outdated TShock server.");
        Console.WriteLine("[PermaBuffs]: To fix, either upgrade TShock or try the 1.0.0.0");
        Console.WriteLine("[Permabuffs]: version of this plugin.");
      }
      ssc_enabled = TShock.ServerSideCharacterConfig.Settings.Enabled;
      
      DB.Connect();
      
      refresh_timer = new Timer
        {Interval = 30000, AutoReset = true, Enabled = true};
      refresh_timer.Elapsed += BuffRefresher;
      
      Commands.ChatCommands.Add(new Command("pb.use", PBenable, "pbenable")
      {
        AllowServer=false,
        HelpText = "Applies permanent buffs if you have 30 of that potion "
                  +"in your Piggy Bank."
      });
      
      Commands.ChatCommands.Add(new Command("pb.use", PBdisable, "pbdisable")
      {
        AllowServer=false,
        HelpText = "Clears permanent buffs and disables them from refreshing."
      });
      
    }

    /// <Summary>
    /// This method is called shortly after a player connects and spawns into 
    /// the world. It notifies the player of their current permabuff status and
    /// then (if they have permission) tells them how to change it.
    /// </Summary>
    public void SendInitialPBMessage(TSPlayer player)
    {
      bool users_status = DB.GetUserPBStatus(player);
      if (users_status)
      {
        player.SendInfoMessage("[Permabuffs] Your permabuffs are enabled!");
        if (player.HasPermission("pb.use"))
        {
          player.SendInfoMessage(string.Format("               To disable, type {0}pbdisable.", Commands.Specifier));
        }
      }
      else
      {
        player.SendInfoMessage("[Permabuffs] Your permabuffs are disbled!");
        if (player.HasPermission("pb.use"))
        {
          player.SendInfoMessage(string.Format("               To enable, type {0}pbenable.", Commands.Specifier));
        }
      }
      return;
    }

    /// <Summary>
    /// This hook is called very shortly after a player spawns into the world.
    /// Sets their Permabuff status to true if they have the pb.use permission.
    /// If SSC is enabled, there is no point in displaying the initial
    /// permabuff message or doing anything else so we just return. Otherwise,
    /// the initial permabuff message is displayed and the player's buffs are
    /// refreshed, if they have permabuffs enabled.
    /// </Summary>
    public void OnGreet(GreetPlayerEventArgs args)
    {
      TSPlayer player = TShock.Players[args.Who];
      DB.NotLoggedInPermabuffStatus[player] = player.HasPermission("pb.use");
      if (ssc_enabled)
      {
        return;
      }
      if (player == null)
      {
        return;
      }
      SendInitialPBMessage(player);
      RefreshPlayerBuffs(player);
      return;
    }

    /// <Summary>
    /// This hook is called very shortly after a player logs in. This is NOT
    /// the same as when a player joins the world. This hook is called
    /// exclusively when a player successfuly executes /login or when the
    /// server automatically logs a player in. An entry is searched in the
    /// Permabuff database for the player. If none is found, one is created.
    /// The player's permabuffs are then refreshed, if they have permabuffs
    /// enabled.
    /// </Summary>
    public void OnPostLogin(PlayerPostLoginEventArgs args)
    {
      DB.NotLoggedInPermabuffStatus.Remove(args.Player);
      int userID = args.Player.Account.ID;
      if (!DB.UserInDB(userID))
      {
        DB.AddNewUser(userID, args.Player.HasPermission("pb.use"));
      }
      SendInitialPBMessage(args.Player);
      RefreshPlayerBuffs(args.Player);
      return;
    }

    /// <Summary>
    /// Despite the name, this hook can be called in two ways: when the player
    /// logs out via /logout and when the player exits the game. It will only
    /// be called once, even if the player is "logged in" while closing the 
    /// client. This method will remove the information in the appropriate
    /// dictionaries and will add the player's current status to the database
    /// so it can be remembered next time they log in.
    /// </Summary>
    public void OnPostLogout(PlayerLogoutEventArgs args)
    {
      TSPlayer player = args.Player;
      if (player == null)
      {
        return;
      }
      if (args.Player.IsLoggedIn)
      {
        int userID = player.Account.ID;
        if (DB.PermabuffStatus.ContainsKey(userID))
        {
          bool status = DB.PermabuffStatus[userID];
          DB.UpdatePermabuffStatus(userID, status);
          DB.PermabuffStatus.Remove(userID);
        }
      }
      else
      {
        // Remove from the not logged in dict. No need to save.
        DB.NotLoggedInPermabuffStatus.Remove(player);
      }
      return;
    }

    /// <Summary>
    /// Called when an account is deleted. Purges the user's info from the
    /// database.
    /// </Summary>
    public void OnAccountDelete(AccountDeleteEventArgs args)
    {
      DB.DeleteUser(args.Account.ID);
    }

    /// <Summary>
    /// Refreshes a single players buff for as many ticks as stated. Terraria
    /// runs at a speed of 60 TPS (ticks per second). The player's buffs will
    /// be refreshed for 4 minutes (every 30 seconds). A buff is considered
    /// "permanent" if the player has 30 of that potion in their piggybank.
    /// </Summary>
    private void RefreshPlayerBuffs(TSPlayer player, int ticks = 4*60*60)
    {      
      if ((player == null) || (!DB.GetUserPBStatus(player)) )
      {
        return;
      }
      if ((!player.IsLoggedIn) && ssc_enabled)
      {
        return;
      }
      foreach (Item item in player.TPlayer.bank.item) // Piggy Bank
      {
        if ((item.stack == 30) && (NameToBuffIDs.BuffIDs.ContainsKey(item.Name)))
        {
          int buffID = NameToBuffIDs.BuffIDs[item.Name];
          player.SetBuff(buffID, ticks);
        }
      }
    }

    /// <Summary>
    /// Calls RefreshPlayerBuffs on each current player.
    /// </Summary>
    private void BuffRefresher(object sender, ElapsedEventArgs args)
    {
      // Remove broadcasts after testing
      foreach (TSPlayer player in TShock.Players)
      {
        RefreshPlayerBuffs(player);
      }
      return;
    }

    /// <Summary>
    /// Called when a player executes the pbenable command with the pb.use
    /// permission. Enables their permabuffs and immediately refreshes them.
    /// </Summary>
    private void PBenable(CommandArgs args)
    {
      args.Player.SendInfoMessage("Permabuffs enabled.");
      if (args.Player.IsLoggedIn)
      {
        DB.PermabuffStatus[args.Player.Account.ID] = true;
      }
      else
      {
        DB.NotLoggedInPermabuffStatus[args.Player] = true;
      }
      RefreshPlayerBuffs(args.Player);
      return;
    }

    /// <Summary>
    /// Called when a player executes the pbdisable command with the pb.use
    /// permission. Disables their permabuffs.
    /// </Summary>
    private void PBdisable(CommandArgs args)
    {
      args.Player.SendInfoMessage("Permabuffs disabled.");
      if (args.Player.IsLoggedIn)
      {
        DB.PermabuffStatus[args.Player.Account.ID] = false;
      }
      else
      {
        DB.NotLoggedInPermabuffStatus[args.Player] = false;
      }
      return;
    }
  }
}