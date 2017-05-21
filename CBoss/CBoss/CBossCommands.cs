using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using TShockAPI;
using Terraria;

namespace CBoss
{
    public static class CBossCommands
    {
        public static void CBossCommand(CommandArgs args)
        {
            //used to spawn the boss at a given region (center point)
            Vector2 pointToSpawnBoss = new Vector2(0.0f, 0.0f);
            Rectangle regionBounds = new Rectangle(0, 0, 0, 0);
            int count = 1;
            Random random = new Random();
            int newLife = -1;

            if (args.Parameters.Count < 2 || args.Parameters.Count > 4)
            {
                //invalid parameters entered
                args.Player.SendErrorMessage("Invalid syntax; Use /cboss <npcID> <region> <count> <health>");
                args.Player.SendErrorMessage("I.E. /cboss 127 arena <-- first 2 params are required");
                args.Player.SendErrorMessage("(<count> is 1 by default)");
                args.Player.SendErrorMessage("(<health> is default by default)");
                return;
            }

            if (args.Parameters.Count > 1)
            {
                if (TShock.Regions.Regions.Exists(x => x.Name == args.Parameters[1]))
                {
                    //get region bounds and midpoint
                    regionBounds = TShock.Regions.GetRegionByName(args.Parameters[1]).Area;
                    pointToSpawnBoss = new Vector2(regionBounds.X + regionBounds.Width / 2.0f, regionBounds.Y + regionBounds.Height / 2.0f);
                }
            }

            if (args.Parameters.Count > 2)
            {
                count = Int32.Parse(args.Parameters[2]);
            }

            if(args.Parameters.Count > 3)
            {
                newLife = Int32.Parse(args.Parameters[3]);
            }

            switch (args.Parameters[0])
            {
                //twins
                case "125":
                    {
                        for (int i = 0; i < count; i++)
                        {
                            int index = NPC.NewNPC((int)(pointToSpawnBoss.X + random.Next(0,100)-50) * 16, (int)(pointToSpawnBoss.Y + random.Next(0, 100)-50)*16, 125, 0);
                            int index2 = NPC.NewNPC((int)(pointToSpawnBoss.X + random.Next(0, 100)-50) * 16, (int)(pointToSpawnBoss.Y + random.Next(0, 100)-50)*16, 126, 0);
                            if (index == 200 || index2 == 200)
                            {
                                Console.WriteLine("NPC spawn error");
                                return;
                            }

                            NPC npc = Main.npc[index];
                            if (newLife != -1)
                                npc.life = newLife;
                            NPC npc2 = Main.npc[index2];
                            if (newLife != -1)
                                npc2.life = newLife;
                            TSPlayer.Server.SendData(PacketTypes.NpcUpdate, "", index);
                            TSPlayer.Server.SendData(PacketTypes.NpcUpdate, "", index2);
                        }
                        break;
                    }
                case "126":
                    {
                        for (int i = 0; i < count; i++)
                        {
                            int index = NPC.NewNPC((int)(pointToSpawnBoss.X + random.Next(0, 100)-50) * 16, (int)(pointToSpawnBoss.Y + random.Next(0, 100)-50) * 16, 125, 0);
                            int index2 = NPC.NewNPC((int)(pointToSpawnBoss.X + random.Next(0, 100)-50) * 16, (int)(pointToSpawnBoss.Y + random.Next(0, 100)-50) * 16, 126, 0);
                            if (index == 200 || index2 == 200)
                            {
                                Console.WriteLine("NPC spawn error");
                                return;
                            }

                            NPC npc = Main.npc[index];
                            if (newLife != -1)
                                npc.life = newLife;
                            NPC npc2 = Main.npc[index2];
                            if (newLife != -1)
                                npc2.life = newLife;
                            TSPlayer.Server.SendData(PacketTypes.NpcUpdate, "", index);
                            TSPlayer.Server.SendData(PacketTypes.NpcUpdate, "", index2);
                        }
                        break;
                    }

                //for all other bosses
                default:
                    {
                        //npc ID
                        int npcid = Int32.Parse(args.Parameters[0]);

                        for (int i = 0; i < count; i++)
                        {
                            int index = NPC.NewNPC((int)(pointToSpawnBoss.X + random.Next(0,100)-50) * 16, (int)(pointToSpawnBoss.Y + random.Next(0, 100)-50) * 16, npcid, 0);
                            if (index == 200)
                            {
                                Console.WriteLine("NPC spawn error");
                                return;
                            }

                            NPC npc = Main.npc[index];
                            if (newLife != -1)
                                npc.life = newLife;

                            TSPlayer.Server.SendData(PacketTypes.NpcUpdate, "", index);
                        }
                        break;
                    }
            }
        }
    }
}