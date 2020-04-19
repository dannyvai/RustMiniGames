using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Bear minigame", "DeBear", 0.1)]
    public class bearHavoc : RustPlugin
    {

        Dictionary<String, String> hitlist = new Dictionary<string, string>();
        Dictionary<String, int> killList = new Dictionary<string, int>();
        List<PlayerLogic> players = new List<PlayerLogic>();

        #region Items  

        static readonly Dictionary<int, string> _itemIdShortnameConversions = new Dictionary<int, string>
        {
            [-1461508848] = "rifle.ak",
            [2115555558] = "ammo.handmade.shell",
            [-533875561] = "ammo.pistol",
            [1621541165] = "ammo.pistol.fire",
            [-422893115] = "ammo.pistol.hv",
            [815896488] = "ammo.rifle",
            [805088543] = "ammo.rifle.explosive",
            [449771810] = "ammo.rifle.incendiary",
            [1152393492] = "ammo.rifle.hv",
            [1578894260] = "ammo.rocket.basic",
            [1436532208] = "ammo.rocket.fire",
            [542276424] = "ammo.rocket.hv",
            [1594947829] = "ammo.rocket.smoke",
            [-1035059994] = "ammo.shotgun",
            [1818890814] = "ammo.shotgun.fire",
            [1819281075] = "ammo.shotgun.slug",
            [1685058759] = "antiradpills",
            [93029210] = "apple",
            [-1565095136] = "apple.spoiled",
            [-1775362679] = "arrow.bone",
            [-1775249157] = "arrow.fire",
            [-1280058093] = "arrow.hv",
            [-420273765] = "arrow.wooden",
            [563023711] = "autoturret",
            [790921853] = "axe.salvaged",
            [-337261910] = "bandage",
            [498312426] = "barricade.concrete",
            [504904386] = "barricade.metal",
            [-1221200300] = "barricade.sandbags",
            [510887968] = "barricade.stone",
            [-814689390] = "barricade.wood",
            [1024486167] = "barricade.woodwire",
            [2021568998] = "battery.small",
            [97329] = "bbq",
            [1046072789] = "trap.bear",
            [97409] = "bed",
            [-1480119738] = "tool.binoculars",
            [1611480185] = "black.raspberries",
            [-1386464949] = "bleach",
            [93832698] = "blood",
            [-1063412582] = "blueberries",
            [-1887162396] = "blueprintbase",
            [-55660037] = "rifle.bolt",
            [919780768] = "bone.club",
            [-365801095] = "bone.fragments",
            [68998734] = "botabag",
            [-853695669] = "bow.hunting",
            [271534758] = "box.wooden.large",
            [-770311783] = "box.wooden",
            [-1192532973] = "bucket.water",
            [-307490664] = "building.planner",
            [707427396] = "burlap.shirt",
            [707432758] = "burlap.shoes",
            [-2079677721] = "cactusflesh",
            [-1342405573] = "tool.camera",
            [-139769801] = "campfire",
            [-1043746011] = "can.beans",
            [2080339268] = "can.beans.empty",
            [-171664558] = "can.tuna",
            [1050986417] = "can.tuna.empty",
            [-1693683664] = "candycaneclub",
            [523409530] = "candycane",
            [1300054961] = "cctv.camera",
            [-2095387015] = "ceilinglight",
            [1428021640] = "chainsaw",
            [94623429] = "chair",
            [1436001773] = "charcoal",
            [1711323399] = "chicken.burned",
            [1734319168] = "chicken.cooked",
            [-1658459025] = "chicken.raw",
            [-726947205] = "chicken.spoiled",
            [-341443994] = "chocholate",
            [1540879296] = "xmasdoorwreath",
            [94756378] = "cloth",
            [3059095] = "coal",
            [3059624] = "corn",
            [2045107609] = "clone.corn",
            [583366917] = "seed.corn",
            [2123300234] = "crossbow",
            [1983936587] = "crude.oil",
            [1257201758] = "cupboard.tool",
            [-1144743963] = "diving.fins",
            [-1144542967] = "diving.mask",
            [-1144334585] = "diving.tank",
            [1066729526] = "diving.wetsuit",
            [-1598790097] = "door.double.hinged.metal",
            [-933236257] = "door.double.hinged.toptier",
            [-1575287163] = "door.double.hinged.wood",
            [-2104481870] = "door.hinged.metal",
            [-1571725662] = "door.hinged.toptier",
            [1456441506] = "door.hinged.wood",
            [1200628767] = "door.key",
            [-778796102] = "door.closer",
            [1526866730] = "xmas.door.garland",
            [1925723260] = "dropbox",
            [1891056868] = "ducttape",
            [1295154089] = "explosive.satchel",
            [498591726] = "explosive.timed",
            [1755466030] = "explosives",
            [726730162] = "facialhair.style01",
            [-1034048911] = "fat.animal",
            [252529905] = "femalearmpithair.style01",
            [471582113] = "femaleeyebrow.style01",
            [-1138648591] = "femalepubichair.style01",
            [305916740] = "female_hairstyle_01",
            [305916742] = "female_hairstyle_03",
            [305916744] = "female_hairstyle_05",
            [1908328648] = "fireplace.stone",
            [-2078972355] = "fish.cooked",
            [-533484654] = "fish.raw",
            [1571660245] = "fishingrod.handmade",
            [1045869440] = "flamethrower",
            [1985408483] = "flameturret",
            [97513422] = "flare",
            [1496470781] = "flashlight.held",
            [1229879204] = "weapon.mod.flashlight",
            [-1722829188] = "floor.grill",
            [1849912854] = "floor.ladder.hatch",
            [-1266285051] = "fridge",
            [-1749787215] = "boots.frog",
            [28178745] = "lowgradefuel",
            [-505639592] = "furnace",
            [1598149413] = "furnace.large",
            [-1779401418] = "gates.external.high.stone",
            [-57285700] = "gates.external.high.wood",
            [98228420] = "gears",
            [1422845239] = "geiger.counter",
            [277631078] = "generator.wind.scrap",
            [115739308] = "burlap.gloves",
            [-522149009] = "gloweyes",
            [3175989] = "glue",
            [718197703] = "granolabar",
            [384204160] = "grenade.beancan",
            [-1308622549] = "grenade.f1",
            [-217113639] = "fun.guitar",
            [-1580059655] = "gunpowder",
            [-1832205789] = "male_hairstyle_01",
            [305916741] = "female_hairstyle_02",
            [936777834] = "attire.hide.helterneck",
            [-1224598842] = "hammer",
            [-1976561211] = "hammer.salvaged",
            [-1406876421] = "hat.beenie",
            [-1397343301] = "hat.boonie",
            [1260209393] = "bucket.helmet",
            [-1035315940] = "burlap.headwrap",
            [-1381682752] = "hat.candle",
            [696727039] = "hat.cap",
            [-2128719593] = "coffeecan.helmet",
            [-1178289187] = "deer.skull.mask",
            [1351172108] = "heavy.plate.helmet",
            [-450738836] = "hat.miner",
            [-966287254] = "attire.reindeer.headband",
            [340009023] = "riot.helmet",
            [124310981] = "hat.wolf",
            [1501403549] = "wood.armor.helmet",
            [698310895] = "hatchet",
            [523855532] = "hazmatsuit",
            [2045246801] = "clone.hemp",
            [583506109] = "seed.hemp",
            [-148163128] = "attire.hide.boots",
            [-132588262] = "attire.hide.skirt",
            [-1666761111] = "attire.hide.vest",
            [-465236267] = "weapon.mod.holosight",
            [-1211618504] = "hoodie",
            [2133577942] = "hq.metal.ore",
            [-1014825244] = "humanmeat.burned",
            [-991829475] = "humanmeat.cooked",
            [-642008142] = "humanmeat.raw",
            [661790782] = "humanmeat.spoiled",
            [-1440143841] = "icepick.salvaged",
            [569119686] = "bone.armor.suit",
            [1404466285] = "heavy.plate.jacket",
            [-1616887133] = "jacket.snow",
            [-1167640370] = "jacket",
            [-1284735799] = "jackolantern.angry",
            [-1278649848] = "jackolantern.happy",
            [776005741] = "knife.bone",
            [108061910] = "ladder.wooden.wall",
            [255101535] = "trap.landmine",
            [-51678842] = "lantern",
            [-789202811] = "largemedkit",
            [516382256] = "weapon.mod.lasersight",
            [50834473] = "leather",
            [-975723312] = "lock.code",
            [1908195100] = "lock.key",
            [-1097452776] = "locker",
            [146685185] = "longsword",
            [-1716193401] = "rifle.lr300",
            [193190034] = "lmg.m249",
            [371156815] = "pistol.m92",
            [3343606] = "mace",
            [825308669] = "machete",
            [830965940] = "mailbox",
            [1662628660] = "male.facialhair.style02",
            [1662628661] = "male.facialhair.style03",
            [1662628662] = "male.facialhair.style04",
            [-1832205788] = "male_hairstyle_02",
            [-1832205786] = "male_hairstyle_04",
            [1625090418] = "malearmpithair.style01",
            [-1269800768] = "maleeyebrow.style01",
            [429648208] = "malepubichair.style01",
            [-1832205787] = "male_hairstyle_03",
            [-1832205785] = "male_hairstyle_05",
            [107868] = "map",
            [997973965] = "mask.balaclava",
            [-46188931] = "mask.bandana",
            [-46848560] = "metal.facemask",
            [-2066726403] = "bearmeat.burned",
            [-2043730634] = "bearmeat.cooked",
            [1325935999] = "bearmeat",
            [-225234813] = "deermeat.burned",
            [-202239044] = "deermeat.cooked",
            [-322501005] = "deermeat.raw",
            [-1851058636] = "horsemeat.burned",
            [-1828062867] = "horsemeat.cooked",
            [-1966381470] = "horsemeat.raw",
            [968732481] = "meat.pork.burned",
            [991728250] = "meat.pork.cooked",
            [-253819519] = "meat.boar",
            [-1714986849] = "wolfmeat.burned",
            [-1691991080] = "wolfmeat.cooked",
            [179448791] = "wolfmeat.raw",
            [431617507] = "wolfmeat.spoiled",
            [688032252] = "metal.fragments",
            [-1059362949] = "metal.ore",
            [1265861812] = "metal.plate.torso",
            [374890416] = "metal.refined",
            [1567404401] = "metalblade",
            [-1057402571] = "metalpipe",
            [-758925787] = "mining.pumpjack",
            [-1411620422] = "mining.quarry",
            [88869913] = "fish.minnows",
            [-2094080303] = "smg.mp5",
            [843418712] = "mushroom",
            [-1569356508] = "weapon.mod.muzzleboost",
            [-1569280852] = "weapon.mod.muzzlebrake",
            [449769971] = "pistol.nailgun",
            [590532217] = "ammo.nailgun.nails",
            [3387378] = "note",
            [1767561705] = "burlap.trousers",
            [106433500] = "pants",
            [-1334615971] = "heavy.plate.pants",
            [-135651869] = "attire.hide.pants",
            [-1595790889] = "roadsign.kilt",
            [-459156023] = "pants.shorts",
            [106434956] = "paper",
            [-578028723] = "pickaxe",
            [-586116979] = "jar.pickle",
            [-1379225193] = "pistol.eoka",
            [-930579334] = "pistol.revolver",
            [548699316] = "pistol.semiauto",
            [142147109] = "planter.large",
            [148953073] = "planter.small",
            [102672084] = "attire.hide.poncho",
            [640562379] = "pookie.bear",
            [-1732316031] = "xmas.present.large",
            [-2130280721] = "xmas.present.medium",
            [-1725510067] = "xmas.present.small",
            [1974032895] = "propanetank",
            [-225085592] = "pumpkin",
            [509654999] = "clone.pumpkin",
            [466113771] = "seed.pumpkin",
            [2033918259] = "pistol.python",
            [2069925558] = "target.reactive",
            [-1026117678] = "box.repair.bench",
            [1987447227] = "research.table",
            [540154065] = "researchpaper",
            [1939428458] = "riflebody",
            [-288010497] = "roadsign.jacket",
            [-847065290] = "roadsigns",
            [3506021] = "rock",
            [649603450] = "rocket.launcher",
            [3506418] = "rope",
            [569935070] = "rug.bear",
            [113284] = "rug",
            [1916127949] = "water.salt",
            [-1775234707] = "salvaged.cleaver",
            [-388967316] = "salvaged.sword",
            [2007564590] = "santahat",
            [-1705696613] = "scarecrow",
            [670655301] = "hazmatsuit_scientist",
            [1148128486] = "hazmatsuit_scientist_peacekeeper",
            [-141135377] = "weapon.mod.small.scope",
            [109266897] = "scrap",
            [-527558546] = "searchlight",
            [-1745053053] = "rifle.semiauto",
            [1223860752] = "semibody",
            [-419069863] = "sewingkit",
            [-1617374968] = "sheetmetal",
            [2057749608] = "shelves",
            [24576628] = "shirt.collared",
            [-1659202509] = "shirt.tanktop",
            [2107229499] = "shoes.boots",
            [191795897] = "shotgun.double",
            [-1009492144] = "shotgun.pump",
            [2077983581] = "shotgun.waterpipe",
            [378365037] = "guntrap",
            [-529054135] = "shutter.metal.embrasure.a",
            [-529054134] = "shutter.metal.embrasure.b",
            [486166145] = "shutter.wood.a",
            [1628490888] = "sign.hanging.banner.large",
            [1498516223] = "sign.hanging",
            [-632459882] = "sign.hanging.ornate",
            [-626812403] = "sign.pictureframe.landscape",
            [385802761] = "sign.pictureframe.portrait",
            [2117976603] = "sign.pictureframe.tall",
            [1338515426] = "sign.pictureframe.xl",
            [-1455694274] = "sign.pictureframe.xxl",
            [1579245182] = "sign.pole.banner.large",
            [-587434450] = "sign.post.double",
            [-163742043] = "sign.post.single",
            [-1224714193] = "sign.post.town",
            [644359987] = "sign.post.town.roof",
            [-1962514734] = "sign.wooden.huge",
            [-705305612] = "sign.wooden.large",
            [-357728804] = "sign.wooden.medium",
            [-698499648] = "sign.wooden.small",
            [1213686767] = "weapon.mod.silencer",
            [386382445] = "weapon.mod.simplesight",
            [1859976884] = "skull_fire_pit",
            [960793436] = "skull.human",
            [1001265731] = "skull.wolf",
            [1253290621] = "sleepingbag",
            [470729623] = "small.oil.refinery",
            [1051155022] = "stash.small",
            [865679437] = "fish.troutsmall",
            [927253046] = "smallwaterbottle",
            [109552593] = "smg.2",
            [-2092529553] = "smgbody",
            [691633666] = "snowball",
            [-2055888649] = "snowman",
            [621575320] = "shotgun.spas12",
            [-2118132208] = "spear.stone",
            [-1127699509] = "spear.wooden",
            [-685265909] = "spikes.floor",
            [552706886] = "spinner.wheel",
            [1835797460] = "metalspring",
            [-892259869] = "sticks",
            [-1623330855] = "stocking.large",
            [-1616524891] = "stocking.small",
            [789892804] = "stone.pickaxe",
            [-1289478934] = "stonehatchet",
            [-892070738] = "stones",
            [-891243783] = "sulfur",
            [889398893] = "sulfur.ore",
            [-1625468793] = "supply.signal",
            [1293049486] = "surveycharge",
            [1369769822] = "fishtrap.small",
            [586484018] = "syringe.medical",
            [110115790] = "table",
            [1490499512] = "targeting.computer",
            [3552619] = "tarp",
            [1471284746] = "techparts",
            [456448245] = "smg.thompson",
            [110547964] = "torch",
            [1588977225] = "xmas.decoration.baubels",
            [918540912] = "xmas.decoration.candycanes",
            [-471874147] = "xmas.decoration.gingerbreadmen",
            [205978836] = "xmas.decoration.lights",
            [-1044400758] = "xmas.decoration.pinecone",
            [-2073307447] = "xmas.decoration.star",
            [435230680] = "xmas.decoration.tinsel",
            [-864578046] = "tshirt",
            [1660607208] = "tshirt.long",
            [260214178] = "tunalight",
            [-1847536522] = "vending.machine",
            [-496055048] = "wall.external.high.stone",
            [-1792066367] = "wall.external.high",
            [562888306] = "wall.frame.cell.gate",
            [-427925529] = "wall.frame.cell",
            [995306285] = "wall.frame.fence.gate",
            [-378017204] = "wall.frame.fence",
            [447918618] = "wall.frame.garagedoor",
            [313836902] = "wall.frame.netting",
            [1175970190] = "wall.frame.shopfront",
            [525244071] = "wall.frame.shopfront.metal",
            [-1021702157] = "wall.window.bars.metal",
            [-402507101] = "wall.window.bars.toptier",
            [-1556671423] = "wall.window.bars.wood",
            [61936445] = "wall.window.glass.reinforced",
            [112903447] = "water",
            [1817873886] = "water.catcher.large",
            [1824679850] = "water.catcher.small",
            [-1628526499] = "water.barrel",
            [547302405] = "waterjug",
            [1840561315] = "water.purifier",
            [-460592212] = "xmas.window.garland",
            [3655341] = "wood",
            [1554697726] = "wood.armor.jacket",
            [-1883959124] = "wood.armor.pants",
            [-481416622] = "workbench1",
            [-481416621] = "workbench2",
            [-481416620] = "workbench3",
            [-1151126752] = "xmas.lightstring",
            [-1926458555] = "xmas.tree"
        };

        static Item BuildItem(int itemid, int amount, ulong skin, int blueprintTarget)
        {
            if (amount < 1) amount = 1;
            Item item = CreateByItemID(itemid, amount, skin);
            if (blueprintTarget != 0)
                item.blueprintTarget = blueprintTarget;
            return item;
        }
        static Item BuildWeapon(int id, ulong skin, List<int> mods)
        {
            Item item = CreateByItemID(id, 1, skin);
            var weapon = item.GetHeldEntity() as BaseProjectile;
            if (weapon != null)
            {
                (item.GetHeldEntity() as BaseProjectile).primaryMagazine.contents = (item.GetHeldEntity() as BaseProjectile).primaryMagazine.capacity;
            }
            if (mods != null)
            {
                foreach (var mod in mods)
                {
                    item.contents.AddItem(BuildItem(mod, 1, 0, 0).info, 1);
                }
            }

            if (_itemIdShortnameConversions[id].StartsWith("ammo"))
            {
                item.amount = 1000;
            }

            return item;
        }

        static Item CreateByItemID(int itemID, int amount = 1, ulong skin = 0)
        {
            string shortName = "";
            if (_itemIdShortnameConversions.TryGetValue(itemID, out shortName))
            {
                return ItemManager.CreateByName(shortName, amount, skin);
            }
            else
            {
                return ItemManager.CreateByItemID(itemID, amount, skin);
            }
        }
        static bool GiveItem(PlayerInventory inv, Item item, ItemContainer container = null)
        {
            if (item == null) { return false; }
            int position = -1;
            return (((container != null) && item.MoveToContainer(container, position, true)) || (item.MoveToContainer(inv.containerMain, -1, true) || item.MoveToContainer(inv.containerBelt, -1, true)));
        }
        #endregion

        #region helpers
        double calcDistance(Vector3 x, Vector3 y)
        {
            return Vector3.Distance(x, y);
        }

        void SendMessage(BasePlayer player, string msg, params object[] args)
        {
            PrintToChat(player, msg, args);
        }

        void Broadcast(string msg, params object[] args)
        {
            PrintToChat(msg, args);
        }

        void Loaded()
        {
            //Broadcast("The plugin has been loaded");
        }

        void resetPlayers(BasePlayer player)
        {
            //If user doesn't exist add it to active players
            if (PlayerLogic.findPlayer(player, players) == null)
            {
                players.Add(new PlayerLogic(player));
            }
            else
            {
                PlayerLogic p = PlayerLogic.findPlayer(player, players);
                Puts(p.ToString());
                p.resetPlayer();
            }

            //Reset kills
            if (killList.ContainsKey(player.displayName))
            {
                killList[player.displayName] = 0;
            }
        }
        #endregion

        #region Hooks

        #region ChatCommands
        [ChatCommand("hey")]
        void HelloPlayer(BasePlayer player)
        {
            SendMessage(player, "Hey" + player.displayName);
        }

        [ChatCommand("highscore")]
        void HighScore(BasePlayer player)
        {

            var items = from pair in killList
                        orderby pair.Value ascending
                        select pair;

            int i = 0;
            // Display results.
            foreach (KeyValuePair<string, int> pair in items)
            {
                if (i > 10)
                {
                    break;
                }
                SendMessage(player, "{0}) {1} with {2} kills", i, pair.Key, pair.Value);
                i++;
            }
        }
        #endregion

        void OnPlayerRespawned(BasePlayer player)
        {
            resetPlayers(player);
        }
        object OnItemPickup(Item item, BasePlayer player)
        {
            item.amount *= 100;
            return null;
        }

        void OnPlayerAttack(BasePlayer attacker, HitInfo info)
        {
            Puts(attacker.ToString());
            Puts(info.ToString());

            if (info.HitEntity != null &&  info.DidHit)
            {
                string key = info.HitEntity.ToString();
                string value = attacker.displayName;
                if (hitlist.ContainsKey(key))
                {
                    hitlist[key] = value;
                }
                else
                {
                    hitlist.Add(key, value);
                }
                double attack_distance = calcDistance(attacker.transform.position, info.HitEntity.transform.position);
                string weapon = info.Weapon.ToString();
            }
        }

        void OnEntityKill(BaseNetworkable entity)
        {
            if (entity.GetType().ToString().Equals("Bear"))
            {
                string key = entity.ToString();
                if (hitlist.ContainsKey(key))
                {
                    Broadcast(entity.GetType().ToString());

                    string kill_key = hitlist[key]; ;
                    if (killList.ContainsKey(kill_key))
                    {
                        killList[kill_key] += 1;
                    }
                    else
                    {

                        killList.Add(kill_key, 1);
                        PlayerLogic.findPlayer(kill_key, players).addKill();
                    }
                    Broadcast("Bear was killed by {0}, He already killed {1} Bears", kill_key, killList[kill_key]);

                }

            }
        }
        #endregion


        class PlayerLogic
        {
            BasePlayer basePlayer;
            int kills;
            int rank;

            public static PlayerLogic findPlayer(BasePlayer player, List<PlayerLogic> players)
            {
                foreach (PlayerLogic p in players)
                {
                    if (player == p.basePlayer)
                    {
                        return p;
                    }
                }
                return null;
            }

            public static PlayerLogic findPlayer(string player_name, List<PlayerLogic> players)
            {
                foreach (PlayerLogic p in players)
                {
                    if (player_name.Equals(p.basePlayer.displayName))
                    {
                        return p;
                    }
                }
                return null;
            }

            public PlayerLogic(BasePlayer bp)
            {
                this.basePlayer = bp;
                resetPlayer();
            }

            public void addKill()
            {
                kills += 1;
                if (kills == 1)
                {
                    GiveItem(basePlayer.inventory, bearHavoc.BuildWeapon(-1716193401, 0, null));
                }
                else if (kills == 2)
                {
                    GiveItem(basePlayer.inventory, bearHavoc.BuildItem(586484018, 10, 0, 0));
                    GiveItem(basePlayer.inventory, bearHavoc.BuildWeapon(1152393492, 0, null));
                }
            }

            public void resetPlayer()
            {
                kills = 0;
                rank = 0;
                GiveItem(basePlayer.inventory, bearHavoc.BuildWeapon(-422893115, 0, null));
                GiveItem(basePlayer.inventory, bearHavoc.BuildWeapon(371156815, 0, null));
            }

        }

    }
}
