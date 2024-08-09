using System.Collections.Generic;
public static class DamageHandler{
    public static Dictionary<int,int> playerDamage;
    public static Dictionary<int,int> enemyDamage;
    static DamageHandler(){
        playerDamage = new Dictionary<int, int>();
        enemyDamage = new Dictionary<int, int>();
    }
    public static void SetPlayerDamage(int weapon, int dmg){playerDamage[weapon] = dmg;}
    public static void SetEnemyDamage(int hash, int dmg){enemyDamage[hash] = dmg;}
    public static int GetEnemyDamage(int hash){
        if(enemyDamage.ContainsKey(hash)) 
            return enemyDamage[hash];
        else{
            // 
            return 0;
        }
    }
    public static int GetPlayerDamage(int weapon){
        if(playerDamage.ContainsKey(weapon)) 
            return playerDamage[weapon];
        else{
            // 
            return 0;
        }
    }
    public static void ClearDictionaries(){
        playerDamage.Clear();
        enemyDamage.Clear();
    }
}
