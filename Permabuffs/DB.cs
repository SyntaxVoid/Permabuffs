using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using TShockAPI;
using TShockAPI.DB;

namespace Permabuffs
{
  public static class DB
  {
    private static IDbConnection db;
    public static Dictionary<int, bool> PermabuffStatus = new Dictionary<int, bool>();
    // PermabuffStatus is used for those who are logged in
    public static Dictionary<TSPlayer, bool> NotLoggedInPermabuffStatus = new Dictionary<TSPlayer, bool>();
    // NotLoggedInPermabuffStatus (phew) is used for those who are not logged in
    
    /// <Summary>
    /// Creates, connects, and establishes a proper database and table for use
    /// with the Permabuffs plugin. The database is stored in the tshock folder
    /// and is named Permabuffs.sqlite.
    /// </Summary>
    public static void Connect()
    {
      string sql = Path.Combine(TShock.SavePath, "Permabuffs.sqlite");
      db = new SqliteConnection(string.Format("uri=file://{0},Version=3", sql));
      
      SqlTableCreator sqlcreator 
        = new SqlTableCreator(db, db.GetSqlType() == 
            SqlType.Sqlite ? (IQueryBuilder)new SqliteQueryCreator() 
            : new MysqlQueryCreator());

      sqlcreator.EnsureTableStructure(
          new SqlTable("Permabuffs",
                       new SqlColumn("userID", MySqlDbType.Int32) {Primary = true, Unique = true, Length = 4},
                       new SqlColumn("PBEnabled", MySqlDbType.Int32) {Length = 1}));
    }

    /// <Summary>
    /// Gets the player's permabuff status. If the player is logged in, it will
    /// look in the PermabuffStatus dictionary and the database. If the player
    /// is not logged in, it will check the NotLoggedInPermabuffStatus
    /// dictionary. If no status can be found, one is created with the default
    /// value of true.
    /// </Summary>
    public static bool GetUserPBStatus(TSPlayer player)
    {
      bool status = true; // Default status
      if (player.IsLoggedIn) // Check the logged in related data
      {
        int userID = player.Account.ID;
        if (PermabuffStatus.ContainsKey(userID))
        {
          status = PermabuffStatus[userID];
        }
        else
        {
          using (QueryResult result = db.QueryReader("SELECT PBEnabled FROM Permabuffs WHERE userID=@0;", userID))
          {
            if (result.Read())
            {
              status = Convert.ToBoolean(result.Get<int>("PBEnabled"));
            }
            PermabuffStatus.Add(userID, status);
          }
        }
      }
      else // Check the not logged in related data
      {
        if (NotLoggedInPermabuffStatus.ContainsKey(player))
        {
          status = NotLoggedInPermabuffStatus[player];
        }
        else
        {
          NotLoggedInPermabuffStatus.Add(player, status);
        }
      }
      return status;
    }

    /// <Summary>
    /// Returns true if the player with the given userID is found in the
    /// databse. False otherwise.
    /// </Summary>
    public static bool UserInDB(int userID)
    /// Returns true if the user's ID is found in the database
    {
      using (QueryResult result = db.QueryReader("SELECT userID FROM Permabuffs WHERE userID=@0;", userID))
      {
        if (result.Read())
        {
          return true;
        }
        return false;
      }
    }

    /// <Summary>
    /// Creates a new entry in the Permabuffs table with the given userID and
    /// status.
    /// </Summary>
    public static void AddNewUser(int userID, bool status_bool = true)
    {
      int status_int = Convert.ToInt32(status_bool);
      db.Query("INSERT INTO Permabuffs (userID, PBEnabled) VALUES (@0,  @1);", userID, status_int);
      PermabuffStatus.Add(userID, status_bool);
      return;
    }

    /// <Summary>
    /// Deletes a user from the Permabuffs table, if they were even in it to
    /// begin with. Then removes their key from the PermabuffStatus dictionary.
    /// There is no association with the NotLoggedInPermabuffStatus dictionary
    /// and the database so there is no reason to check it.
    /// </Summary>
    public static void DeleteUser(int userID)
    {
      if (UserInDB(userID))
      {
        db.Query("DELETE FROM Permabuffs WHERE userID=@0", userID);
      }
      if (PermabuffStatus.ContainsKey(userID))
      {
        PermabuffStatus.Remove(userID);
      }
      return;
    }

    /// <Summary>
    /// Updates the player's status in the Permabuff table to the status given.
    /// </Summary>
    public static void UpdatePermabuffStatus(int userID, bool status)
    {
      if (!UserInDB(userID))
      {
        AddNewUser(userID, status); // I dont think this will ever be reached.
      }
      db.Query("UPDATE Permabuffs SET PBEnabled=@0 WHERE userID=@1;", Convert.ToInt32(status), userID);
      return;
    }
  }
}
