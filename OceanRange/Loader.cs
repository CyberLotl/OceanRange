using SRML;
using SRML.Utils;
using UnityEngine;
using SRML.SR;
using System.Reflection;
using SRML.SR.Translation;
using System.IO;
using System;

namespace OceanRange
{
    public class Loader : ModEntryPoint
    {
        // Called before GameContext.Awake
        // You want to register new things and enum values here, as well as do all your harmony patching
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.ROSA_PLORT);
            PediaRegistry.RegisterIdentifiableMapping(Id.ROSA_SLIME_ENTRY, Id.ROSA_SLIME);
            PediaRegistry.SetPediaCategory(Id.ROSA_SLIME_ENTRY, PediaRegistry.PediaCategory.SLIMES);

            new SlimePediaEntryTranslation(Id.ROSA_SLIME_ENTRY).SetTitleTranslation("Rosa Slime")
                .SetIntroTranslation("They say they come in different colors too!")
                .SetDietTranslation("Everything").SetFavoriteTranslation("(None)")
                .SetSlimeologyTranslation("The Rosa Slime is the most common slime in the Great Reef. Resembling the shared characteristics of the Earth animal, Axolotl. Just like its cousin the Pink Slime, it’s cheerful, docile, and will eat anything. Making it the easiest to take care of. These slime are very social. Enjoys the company of others including Ranchers!")
                .SetRisksTranslation("Not much to worry about! Except, for its mischievous nature. The Rosa Slime enjoys pulling pranks to other slimes including its own kind. Will steal and cause a little havoc just for fun. However, due to its social nature, when left alone, the Rosa Slime will become agitated. Ultimately, it will cause problems for attention. Another detail to note is that when hungry and surrounded by its own kind, it will take the ‘petals’ off other Rosa Slime for substance. Don’t worry though! Just like Axolotls, they can regenerate.")
                .SetPlortonomicsTranslation("Rosa Plorts or Pearls, are a recent discovery. Slime Scientists are still learning about their important properties that may be used on Earth.");

            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.LATERN_PLORT);
            PediaRegistry.RegisterIdentifiableMapping(Id.LATERN_SLIME_ENTRY, Id.LATERN_SLIME);
            PediaRegistry.SetPediaCategory(Id.LATERN_SLIME_ENTRY, PediaRegistry.PediaCategory.SLIMES);

            new SlimePediaEntryTranslation(Id.LATERN_SLIME_ENTRY).SetTitleTranslation("Lantern Slime")
                .SetIntroTranslation("It lights up your life")
                .SetDietTranslation("Meat").SetFavoriteTranslation("Rooster for now")
                .SetSlimeologyTranslation("The Lantern Slime is the distant cousin of the Angler Slime. Adapted to swim underwater, these nocturnal slimes are capable of swimming in great depths. Thanks to its “luminescent Organ,” it makes attracting prey very easy. Unfortunately, just like its other distant cousin Phospher Slimes, it will disappear after exposure to sunlight")
                .SetRisksTranslation("These Slimes don’t appear hostile to Ranchers. However, its “luminescent Organ,” may attract unwanted attention.")
                .SetPlortonomicsTranslation("Even though the discovery of Pearls are varly new, so far Slime Scientists have used the plorts for easy underwater welding. Its Pearls have a strong plasma core allowing it to remain warm even in cold underwater environments. This discovery may even help divers during underwater expeditions.");



            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.SAND_PLORT);
            PediaRegistry.RegisterIdentifiableMapping(Id.SAND_SLIME_ENTRY, Id.SAND_SLIME);
            PediaRegistry.SetPediaCategory(Id.SAND_SLIME_ENTRY, PediaRegistry.PediaCategory.SLIMES);

            new SlimePediaEntryTranslation(Id.SAND_SLIME_ENTRY).SetTitleTranslation("Sand Slime")
                .SetIntroTranslation("You can find it between your toes")
                .SetDietTranslation("Fruit for now").SetFavoriteTranslation("Mint Mango for now")
                .SetSlimeologyTranslation("The Sand Slimes are the distant cousin of the Fire and Puddle Slimes? Slimologists are still unsure on the slime's origins. So far what they know is that they are the bottom feeders of the slime world. Will eat anything that lands on the bottom of the sea. Recycling its content for resources.")
                .SetRisksTranslation("Just like the Puddle and Fire Slime, they are not hostile to a Rancher. They rather prefer exploring the ocean floor, eating water they can find.")
                .SetPlortonomicsTranslation("The discovery of Sand Pearls is still ongoing. So far Slime Scientists have found valuable resources stuck within the Pearls. The use of these valuable resources still remains a mystery.");



            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.MINE_PLORT);
            PediaRegistry.RegisterIdentifiableMapping(Id.MINE_SLIME_ENTRY, Id.MINE_SLIME);
            PediaRegistry.SetPediaCategory(Id.MINE_SLIME_ENTRY, PediaRegistry.PediaCategory.SLIMES);

            new SlimePediaEntryTranslation(Id.MINE_SLIME_ENTRY).SetTitleTranslation("Mine Slime")
                .SetIntroTranslation("[Insert boom emoji here]")
                .SetDietTranslation("Veggies for now").SetFavoriteTranslation("Carrots for now")
                .SetSlimeologyTranslation("The Mine Slime is the distant cousin of the Boom Slime. Capable of adapting to the Slime Sea. These Slimes prefer isolation. Deep in the ocean, the Mine Slimes will spend its days sleeping and rarely swims. But when disturbed, it will create an underwater explosion that affects anyone who dares to be in radius. You can say you should mine your own business!")
                .SetRisksTranslation("Just like the Boom Slimes, the Mine Slimes are hostile to Ranchers when getting too close. A single touch will activate them to explode, resulting in serious damage to Ranchers. Luckily, only agitated Mine Slime who haven’t received a yummy diet in days, will be the most dangerous. Otherwise, the Mine Slime loves to be booped resulting it to float around like a balloon!")
                .SetPlortonomicsTranslation("The discovery of Mine Pearls is the most mysterious. Due to the fact how delicate they are. Trying to study them may result in an explosion taking the Pearl with it! A word of caution to Ranchers who dares  to hold the Mine Pearl, ALWAYS USE YOUR VACPACK!");
        }


        // Called before GameContext.Start
        // Used for registering things that require a loaded gamecontext
        public override void Load()
        {

            

            (SlimeDefinition, GameObject) SlimeTuple = Custom_Creator.CreateSlime(Id.ROSA_SLIME, "Rosa Slime");
            Custom_Creator.CreateSpawner();
            (SlimeDefinition, GameObject) SlimeTupleLatern = Custom_Creator.CreateLaternSlime(Id.LATERN_SLIME, "Latern Slime");
            Custom_Creator.CreateLaternSpawner();
            (SlimeDefinition, GameObject) SlimeTupleSand = Custom_Creator.CreateSandSlime(Id.SAND_SLIME, "Sand Slime");
            Custom_Creator.CreateSandSpawner();
            (SlimeDefinition, GameObject) SlimeTupleMine = Custom_Creator.CreateMineSlime(Id.MINE_SLIME, "Mine Slime");
            Custom_Creator.CreateMineSpawner();
            Area01.PreLoad();
            Area01.PostLoad();
            Gordo_Creator.CreateGordo(Id.ROSA_GORDO, "gordoRosa");
            LargoGenerator.CraftLargo(Id.ROSA_PINK_LARGO, Identifiable.Id.PINK_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_ROCK_LARGO, Identifiable.Id.ROCK_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_TABBY_LARGO, Identifiable.Id.TABBY_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_HONEY_LARGO, Identifiable.Id.HONEY_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_PHOSPHOR_LARGO, Identifiable.Id.PHOSPHOR_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_HUNTER_LARGO, Identifiable.Id.HUNTER_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_QUANTUM_LARGO, Identifiable.Id.QUANTUM_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_DERVISH_LARGO, Identifiable.Id.DERVISH_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_TANGLE_LARGO, Identifiable.Id.TANGLE_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_RAD_LARGO, Identifiable.Id.RAD_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_BOOM_LARGO, Identifiable.Id.BOOM_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_CRYSTAL_LARGO, Identifiable.Id.CRYSTAL_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_MOSAIC_LARGO, Identifiable.Id.MOSAIC_SLIME, Id.ROSA_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_PINK_LARGO, Identifiable.Id.PINK_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_ROCK_LARGO, Identifiable.Id.ROCK_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_TABBY_LARGO, Identifiable.Id.TABBY_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_HONEY_LARGO, Identifiable.Id.HONEY_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_PHOSPHOR_LARGO, Identifiable.Id.PHOSPHOR_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_HUNTER_LARGO, Identifiable.Id.HUNTER_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_QUANTUM_LARGO, Identifiable.Id.QUANTUM_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_DERVISH_LARGO, Identifiable.Id.DERVISH_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_TANGLE_LARGO, Identifiable.Id.TANGLE_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_RAD_LARGO, Identifiable.Id.RAD_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_BOOM_LARGO, Identifiable.Id.BOOM_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_CRYSTAL_LARGO, Identifiable.Id.CRYSTAL_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.LATERN_MOSAIC_LARGO, Identifiable.Id.MOSAIC_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.ROSA_LATERN_LARGO, Id.ROSA_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_PINK_LARGO, Identifiable.Id.PINK_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_ROCK_LARGO, Identifiable.Id.ROCK_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_TABBY_LARGO, Identifiable.Id.TABBY_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_HONEY_LARGO, Identifiable.Id.HONEY_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_PHOSPHOR_LARGO, Identifiable.Id.PHOSPHOR_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_HUNTER_LARGO, Identifiable.Id.HUNTER_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_QUANTUM_LARGO, Identifiable.Id.QUANTUM_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_DERVISH_LARGO, Identifiable.Id.DERVISH_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_TANGLE_LARGO, Identifiable.Id.TANGLE_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_RAD_LARGO, Identifiable.Id.RAD_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_BOOM_LARGO, Identifiable.Id.BOOM_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_CRYSTAL_LARGO, Identifiable.Id.CRYSTAL_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_MOSAIC_LARGO, Identifiable.Id.MOSAIC_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_LATERN_LARGO, Id.MINE_SLIME, Id.LATERN_SLIME, SlimeRegistry.LargoProps.NONE);
            LargoGenerator.CraftLargo(Id.MINE_ROSA_LARGO, Id.ROSA_SLIME, Id.MINE_SLIME, SlimeRegistry.LargoProps.NONE);

            SlimeDefinition Rosa_Slime_Definition = SlimeTuple.Item1;

            GameObject Rosa_Slime_Object = SlimeTuple.Item2;


            LookupRegistry.RegisterIdentifiablePrefab(Rosa_Slime_Object);

            SlimeRegistry.RegisterSlimeDefinition(Rosa_Slime_Definition);





            PediaRegistry.RegisterIdEntry(Id.ROSA_SLIME_ENTRY, Custom_Creator.RosaSlimeAssetBundle.LoadAsset<Sprite>("rice_balls"));
            PediaRegistry.RegisterIdEntry(Id.LATERN_SLIME_ENTRY, Custom_Creator.RosaSlimeAssetBundle.LoadAsset<Sprite>("lantern_slime"));
            PediaRegistry.RegisterIdEntry(Id.SAND_SLIME_ENTRY, Custom_Creator.RosaSlimeAssetBundle.LoadAsset<Sprite>("sand_slime"));
            PediaRegistry.RegisterIdEntry(Id.MINE_SLIME_ENTRY, Custom_Creator.RosaSlimeAssetBundle.LoadAsset<Sprite>("mine_slime"));


            SlimeDefinition slimeByIdentifiableId = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.TARR_SLIME);
            
            slimeByIdentifiableId.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                eats = Id.ROSA_SLIME,
                becomesId = Identifiable.Id.TARR_SLIME,
                driver = SlimeEmotions.Emotion.FEAR,
                extraDrive = 999999f,
                producesId = Identifiable.Id.TARR_SLIME
            });
            
            

        }


        

        // Called after all mods Load's have been called
        // Used for editing existing assets in the game, not a registry step
        public override void PostLoad()
        {

        }

    }
}