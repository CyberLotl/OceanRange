using System;
using System.Reflection;
using SRML.Utils;
using UnityEngine;
using System.IO;
using System.Collections;
using SRML.SR;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AssetsLib;
using Object = UnityEngine.Object;

namespace OceanRange
{

    
    class Custom_Creator
    {



        public static AssetBundle RosaSlimeAssetBundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("OceanRange.rosa_slime_assetbundle"));



        public static (SlimeDefinition, GameObject) CreateSlime(Identifiable.Id ROSA_SLIME, String RosaSlime)
        {
            
            SlimeDefinition pinkSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME);
            SlimeDefinition slimeDefinition = (SlimeDefinition)PrefabUtils.DeepCopyObject(pinkSlimeDefinition);
            slimeDefinition.AppearancesDefault = new SlimeAppearance[1];
            slimeDefinition.Diet.Produces = new Identifiable.Id[1]
            {
                Id.ROSA_PLORT
            };

            slimeDefinition.Diet.MajorFoodGroups = new SlimeEat.FoodGroup[4]
            {
                SlimeEat.FoodGroup.FRUIT
                ,SlimeEat.FoodGroup.VEGGIES
                ,SlimeEat.FoodGroup.MEAT
                ,SlimeEat.FoodGroup.GINGER
            };

            slimeDefinition.Diet.AdditionalFoods = new Identifiable.Id[0];

            slimeDefinition.Diet.Favorites = new Identifiable.Id[0];

            slimeDefinition.Diet.EatMap?.Clear();
            slimeDefinition.CanLargofy = false;
            slimeDefinition.FavoriteToys = new Identifiable.Id[1]
            {
                Identifiable.Id.RUBBER_DUCKY_TOY
            };
            slimeDefinition.Name = RosaSlime;
            slimeDefinition.IdentifiableId = ROSA_SLIME;

            GameObject slimeObject = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_SLIME));
            slimeObject.name = "RosaSlime";
            slimeObject.GetComponent<PlayWithToys>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeAppearanceApplicator>().SlimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeEat>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<Identifiable>().id = ROSA_SLIME;
            UnityEngine.Object.Destroy(slimeObject.GetComponent<PinkSlimeFoodTypeTracker>());


            SlimeAppearance slimeAppearance = (SlimeAppearance)PrefabUtils.DeepCopyObject(pinkSlimeDefinition.AppearancesDefault[0]);
            slimeDefinition.AppearancesDefault[0] = slimeAppearance;
            slimeAppearance.Structures = new SlimeAppearanceStructure[]
            {
              slimeAppearance.Structures[0],
              new SlimeAppearanceStructure(slimeAppearance.Structures[0]),
              new SlimeAppearanceStructure(slimeAppearance.Structures[0])
            };
            Material material = slimeAppearance.Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(230, 199, 210, 255));
            material.SetColor("_MiddleColor", new Color32(230, 199, 210, 255));
            material.SetColor("_BottomColor", new Color32(230, 199, 210, 255));
            material.SetColor("_SpecColor", new Color32(230, 199, 210, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            slimeAppearance.Structures[0].DefaultMaterials = new Material[] { material };
            slimeAppearance.Structures[1].DefaultMaterials = new Material[] { material };
            material = slimeAppearance.Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(178, 62, 101, 255));
            material.SetColor("_MiddleColor", new Color32(178, 62, 101, 255));
            material.SetColor("_BottomColor", new Color32(178, 62, 101, 255));
            material.SetColor("_SpecColor", new Color32(178, 62, 101, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            slimeAppearance.Structures[2].DefaultMaterials = new Material[] { material };
            SlimeExpressionFace[] expressionFaces = slimeAppearance.Face.ExpressionFaces;
            for (int k = 0; k < expressionFaces.Length; k++)
            {
                SlimeExpressionFace slimeExpressionFace = expressionFaces[k];
                if ((bool)slimeExpressionFace.Mouth)
                {
                    slimeExpressionFace.Mouth.SetColor("_MouthBot", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthMid", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthTop", new Color32(0, 0, 0, 255));
                }
                if ((bool)slimeExpressionFace.Eyes)
                {
                    slimeExpressionFace.Eyes.SetColor("_EyeRed", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeGreen", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeBlue", new Color32(0, 0, 0, 255));
                }
            }
            slimeAppearance.Face.OnEnable();
            slimeAppearance.ColorPalette = new SlimeAppearance.Palette
            {
                Top = new Color32(249, 229, 240, 255),
                Middle = new Color32(249, 229, 240, 255),
                Bottom = new Color32(249, 229, 240, 255)
            };
            slimeObject.GetComponent<SlimeAppearanceApplicator>().Appearance = slimeAppearance;


            if (RosaSlimeAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
            }

            
            
            var pinkBodyApp = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME).AppearancesDefault[0].Structures[0].Element.Prefabs[0];
            slimeAppearance.Structures[0].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[0].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.gameObject.CreatePrefabCopy().GetComponent<SlimeAppearanceObject>() };
            slimeAppearance.Structures[0].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_rosa.obj");
            if (slimeAppearance.Structures[0].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
            {
                Debug.Log("Couldnt load rosa mesh");
            }
            slimeAppearance.Structures[1].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[1].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.CreatePrefab() };
            slimeAppearance.Structures[1].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_tendrals.obj");
            slimeAppearance.Structures[1].SupportsFaces = false;
            if (slimeAppearance.Structures[1].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
                Debug.Log("Couldnt load rosa tendral mesh");
            slimeAppearance.Structures[2].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[2].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.CreatePrefab() };
            slimeAppearance.Structures[2].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_frills.obj");
            slimeAppearance.Structures[2].SupportsFaces = false;
            if (slimeAppearance.Structures[2].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
                Debug.Log("Couldnt load rosa frill mesh");
            var dummyBody = pinkSlimeDefinition.AppearancesDefault[0].Structures[0].Element.Prefabs[0].CreatePrefab();
            dummyBody.GetComponent<SkinnedMeshRenderer>().sharedMesh = Object.Instantiate(dummyBody.GetComponent<SkinnedMeshRenderer>().sharedMesh);
            MeshUtils.GenerateBoneData(slimeObject.GetComponent<SlimeAppearanceApplicator>(), dummyBody, 0.25f, 1,
                slimeAppearance.Structures[0].Element.Prefabs[0],
                slimeAppearance.Structures[1].Element.Prefabs[0],
            
                slimeAppearance.Structures[2].Element.Prefabs[0]);

            RosaSlimeAssetBundle.LoadAsset<Sprite>("rice_balls");
            
            GameObject GameObject2 = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_PLORT)); //It can be any plort, but pink works the best. 
            GameObject2.name = "RosaPlort";

            GameObject2.GetComponent<Identifiable>().id = Id.ROSA_PLORT;
            GameObject2.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;

            GameObject2.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate<Material>(GameObject2.GetComponent<MeshRenderer>().material);
            Color PureWhite = new Color32(255, 255, 255, byte.MaxValue); // RGB   
            Color White = Color.white;
            //Pretty self explanatory. These change the color of the plort. You can set the colors to whatever you want.    
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_TopColor", White);
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", PureWhite);
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", White);

            LookupRegistry.RegisterIdentifiablePrefab(slimeObject);
            SlimeRegistry.RegisterSlimeDefinition(slimeDefinition);
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.ROSA_SLIME));
            Sprite icon2 = RosaSlimeAssetBundle.LoadAsset<Sprite>("rice_balls");
            slimeAppearance.Icon = RosaSlimeAssetBundle.LoadAsset<Sprite>("rice_balls");
            LookupRegistry.RegisterVacEntry(Id.ROSA_SLIME, new Color32(80, 0, 0, byte.MaxValue), icon2);
            slimeAppearance.ColorPalette.Ammo = new Color32(80, 0, 0, byte.MaxValue);
            SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.ROSA_SLIME).GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            TranslationPatcher.AddActorTranslation("l.rosa_slime", "Rosa Slime");

            LookupRegistry.RegisterIdentifiablePrefab(GameObject2);

            

            TranslationPatcher.AddActorTranslation("l.rosa_plort", "Rosa Plort");
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.ROSA_PLORT);
            Identifiable.PLORT_CLASS.Add(Id.ROSA_PLORT);
            Identifiable.NON_SLIMES_CLASS.Add(Id.ROSA_PLORT);

            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, GameObject2);
            // Icon that is below is just a placeholder. You can change it to anything or add your own!

            Sprite PlortIcon = RosaSlimeAssetBundle.LoadAsset<Sprite>("RosaPearl"); 
            LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(Id.ROSA_PLORT, PureWhite, PlortIcon));
            AmmoRegistry.RegisterSiloAmmo((System.Predicate<SiloStorage.StorageType>)(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.PLORT), Id.ROSA_PLORT);

            float price = 95f; //Starting price for plort   
            float saturation = 5f; //Can be anything. The higher it is, the higher the plort price changes every day. I'd recommend making it small so you don't destroy the economy lol.   
            PlortRegistry.AddEconomyEntry(Id.ROSA_PLORT, price, saturation); //Allows it to be sold while the one below this adds it to plort market.   
            PlortRegistry.AddPlortEntry(Id.ROSA_PLORT); //PlortRegistry.AddPlortEntry(YourCustomEnum, new ProgressDirector.ProgressType[1]{ProgressDirector.ProgressType.NONE});   
            DroneRegistry.RegisterBasicTarget(Id.ROSA_PLORT);
            AmmoRegistry.RegisterRefineryResource(Id.ROSA_PLORT);  //For if you want to make a gadget that uses it  


            CreateSpawner();


            return (slimeDefinition, slimeObject);

             
        }

        public static void CreateSpawner()
        {
            SRCallbacks.PreSaveGameLoad += delegate (SceneContext s)
            {
                foreach (DirectedSlimeSpawner directedSlimeSpawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>().Where(delegate (DirectedSlimeSpawner ss)
                {
                    ZoneDirector.Zone zoneId = ss.GetComponentInParent<Region>(true).GetZoneId();
                    return zoneId == ZoneDirector.Zone.REEF;
                }))
                {
                    foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                    {
                        List<SlimeSet.Member> list = new List<SlimeSet.Member>(spawnConstraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.ROSA_SLIME)
                                ,weight = 0.5f
                            }
                        };
                        spawnConstraint.slimeset.members = list.ToArray();
                    }
                }
            };

            
        }
        public static (SlimeDefinition, GameObject) CreateLaternSlime(Identifiable.Id LATERN_SLIME, String LaternSlime)
        {
            SlimeDefinition pinkSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME);
            SlimeDefinition slimeDefinition = (SlimeDefinition)PrefabUtils.DeepCopyObject(pinkSlimeDefinition);
            slimeDefinition.AppearancesDefault = new SlimeAppearance[1];
            slimeDefinition.Diet.Produces = new Identifiable.Id[1]
            {
                Id.LATERN_PLORT
            };

            slimeDefinition.Diet.MajorFoodGroups = new SlimeEat.FoodGroup[1]
            {
                SlimeEat.FoodGroup.MEAT
            };

            slimeDefinition.Diet.AdditionalFoods = new Identifiable.Id[0];

            slimeDefinition.Diet.Favorites = new Identifiable.Id[1]
            {
                Identifiable.Id.ROOSTER
            };

            slimeDefinition.Diet.EatMap?.Clear();
            slimeDefinition.CanLargofy = false;
            slimeDefinition.FavoriteToys = new Identifiable.Id[1]
            {
                Identifiable.Id.NIGHT_LIGHT_TOY
            };
            slimeDefinition.Name = LaternSlime;
            slimeDefinition.IdentifiableId = LATERN_SLIME;

            GameObject slimeObject = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_SLIME));
            slimeObject.name = "LaternSlime";
            slimeObject.GetComponent<PlayWithToys>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeAppearanceApplicator>().SlimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeEat>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<Identifiable>().id = LATERN_SLIME;
            UnityEngine.Object.Destroy(slimeObject.GetComponent<PinkSlimeFoodTypeTracker>());


            SlimeAppearance slimeAppearance = (SlimeAppearance)PrefabUtils.DeepCopyObject(pinkSlimeDefinition.AppearancesDefault[0]);
            slimeDefinition.AppearancesDefault[0] = slimeAppearance;
            slimeAppearance.Structures = new SlimeAppearanceStructure[]
            {
              slimeAppearance.Structures[0],
              new SlimeAppearanceStructure(slimeAppearance.Structures[0]),
              new SlimeAppearanceStructure(slimeAppearance.Structures[0]),
              new SlimeAppearanceStructure(slimeAppearance.Structures[0])
            };
            Material material = slimeAppearance.Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(117, 44, 134, 255));
            material.SetColor("_MiddleColor", new Color32(117, 44, 134, 255));
            material.SetColor("_BottomColor", new Color32(117, 44, 134, 255));
            material.SetColor("_SpecColor", new Color32(117, 44, 134, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            slimeAppearance.Structures[0].DefaultMaterials = new Material[] { material };
            material = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.TABBY_SLIME).AppearancesDefault[0].Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(117, 44, 134, 255));
            material.SetColor("_MiddleColor", new Color32(117, 44, 134, 255));
            material.SetColor("_BottomColor", new Color32(177, 94, 200, 255));
            material.SetColor("_SpecColor", new Color32(117, 44, 134, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            material.SetTexture("_StripeTexture", RosaSlimeAssetBundle.LoadAsset<Texture2D>("assets/latern_fin.png"));
            slimeAppearance.Structures[1].DefaultMaterials = new Material[] { material };
            material = slimeAppearance.Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(235, 219, 106, 255));
            material.SetColor("_MiddleColor", new Color32(235, 219, 106, 255));
            material.SetColor("_BottomColor", new Color32(235, 219, 106, 255));
            material.SetColor("_SpecColor", new Color32(235, 219, 106, 255));
            material.SetFloat("_Shininess", 5f);
            material.SetFloat("_Gloss", 1f);
            slimeAppearance.Structures[2].DefaultMaterials = new Material[] { material };
            material = slimeAppearance.Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(117, 44, 134, 255));
            material.SetColor("_MiddleColor", new Color32(117, 44, 134, 255));
            material.SetColor("_BottomColor", new Color32(117, 44, 134, 255));
            material.SetColor("_SpecColor", new Color32(117, 44, 134, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            slimeAppearance.Structures[3].DefaultMaterials = new Material[] { material };

            SlimeExpressionFace[] expressionFaces = slimeAppearance.Face.ExpressionFaces;
            for (int k = 0; k < expressionFaces.Length; k++)
            {
                SlimeExpressionFace slimeExpressionFace = expressionFaces[k];
                if ((bool)slimeExpressionFace.Mouth)
                {
                    slimeExpressionFace.Mouth.SetColor("_MouthBot", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthMid", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthTop", new Color32(0, 0, 0, 255));
                }
                if ((bool)slimeExpressionFace.Eyes)
                {
                    slimeExpressionFace.Eyes.SetColor("_EyeRed", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeGreen", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeBlue", new Color32(0, 0, 0, 255));
                }
            }
            slimeAppearance.Face.OnEnable();
            slimeAppearance.ColorPalette = new SlimeAppearance.Palette
            {
                Top = new Color32(117, 44, 134, 255),
                Middle = new Color32(117, 44, 134, 255),
                Bottom = new Color32(117, 44, 134, 255)
            };
            slimeObject.GetComponent<SlimeAppearanceApplicator>().Appearance = slimeAppearance;


            if (RosaSlimeAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
            }


            var pinkBodyApp = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME).AppearancesDefault[0].Structures[0].Element.Prefabs[0];
            slimeAppearance.Structures[0].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[0].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.gameObject.CreatePrefabCopy().GetComponent<SlimeAppearanceObject>() };
            var skin = slimeAppearance.Structures[0].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>();
            skin.sharedMesh = Object.Instantiate(skin.sharedMesh);
            if (slimeAppearance.Structures[0].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
            {
                Debug.Log("Couldnt load latern mesh");
            }
            slimeAppearance.Structures[1].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[1].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.CreatePrefab() };
            slimeAppearance.Structures[1].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_latern_fins.obj");
            slimeAppearance.Structures[1].SupportsFaces = false;
            if (slimeAppearance.Structures[1].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
                Debug.Log("Couldnt load latern fins mesh");
            slimeAppearance.Structures[2].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[2].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.CreatePrefab() };
            slimeAppearance.Structures[2].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_latern_lure.obj");
            slimeAppearance.Structures[2].SupportsFaces = false;
            if (slimeAppearance.Structures[2].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
                Debug.Log("Couldnt load lantern lure mesh");
            slimeAppearance.Structures[3].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[3].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.CreatePrefab() };
            slimeAppearance.Structures[3].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_latern_spine.obj");
            slimeAppearance.Structures[3].SupportsFaces = false;
            if (slimeAppearance.Structures[2].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
                Debug.Log("Couldnt load lantern spine mesh");
            MeshUtils.GenerateBoneData(slimeObject.GetComponent<SlimeAppearanceApplicator>(), slimeAppearance.Structures[0].Element.Prefabs[0], 0.25f, 1,
                slimeAppearance.Structures[1].Element.Prefabs[0],
                slimeAppearance.Structures[2].Element.Prefabs[0],
                slimeAppearance.Structures[3].Element.Prefabs[0]);

            RosaSlimeAssetBundle.LoadAsset<Sprite>("Lantern_Plort");
            
            GameObject GameObject2 = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_PLORT)); //It can be any plort, but pink works the best. 
            GameObject2.name = "LaternPlort";

            GameObject2.GetComponent<Identifiable>().id = Id.LATERN_PLORT;
            GameObject2.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;

            GameObject2.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate<Material>(GameObject2.GetComponent<MeshRenderer>().material);
            Color PureWhite = new Color32(255, 255, 255, byte.MaxValue); // RGB   
            Color White = Color.white;
            //Pretty self explanatory. These change the color of the plort. You can set the colors to whatever you want.    
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_TopColor", new Color32(117, 44, 134, 255));
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", new Color32(117, 44, 134, 255));
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", new Color32(117, 44, 134, 255));

            LookupRegistry.RegisterIdentifiablePrefab(GameObject2);

            

            TranslationPatcher.AddActorTranslation("l.latern_plort", "Latern Plort");
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.LATERN_PLORT);
            Identifiable.PLORT_CLASS.Add(Id.LATERN_PLORT);
            Identifiable.NON_SLIMES_CLASS.Add(Id.LATERN_PLORT);

            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, GameObject2);
            // Icon that is below is just a placeholder. You can change it to anything or add your own!

            Sprite PlortIcon = RosaSlimeAssetBundle.LoadAsset<Sprite>("Lantern_Plort"); 
            LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(Id.LATERN_PLORT, PureWhite, PlortIcon));
            AmmoRegistry.RegisterSiloAmmo((System.Predicate<SiloStorage.StorageType>)(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.PLORT), Id.LATERN_PLORT);

            float price = 150f; //Starting price for plort   
            float saturation = 5f; //Can be anything. The higher it is, the higher the plort price changes every day. I'd recommend making it small so you don't destroy the economy lol.   
            PlortRegistry.AddEconomyEntry(Id.LATERN_PLORT, price, saturation); //Allows it to be sold while the one below this adds it to plort market.   
            PlortRegistry.AddPlortEntry(Id.LATERN_PLORT); //PlortRegistry.AddPlortEntry(YourCustomEnum, new ProgressDirector.ProgressType[1]{ProgressDirector.ProgressType.NONE});   
            DroneRegistry.RegisterBasicTarget(Id.LATERN_PLORT);
            AmmoRegistry.RegisterRefineryResource(Id.LATERN_PLORT);  //For if you want to make a gadget that uses it  


            LookupRegistry.RegisterIdentifiablePrefab(slimeObject);
            SlimeRegistry.RegisterSlimeDefinition(slimeDefinition);
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.LATERN_SLIME));
            Sprite icon2 = RosaSlimeAssetBundle.LoadAsset<Sprite>("lantern_slime");
            slimeAppearance.Icon = RosaSlimeAssetBundle.LoadAsset<Sprite>("lantern_slime");
            LookupRegistry.RegisterVacEntry(Id.LATERN_SLIME, new Color32(80, 0, 0, byte.MaxValue), icon2);
            slimeAppearance.ColorPalette.Ammo = new Color32(80, 0, 0, byte.MaxValue);
            TranslationPatcher.AddActorTranslation("l.latern_slime", "Lantern Slime");
            SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.LATERN_SLIME).GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            


            CreateLaternSpawner();


            return (slimeDefinition, slimeObject);

             
        }

        public static void CreateLaternSpawner()
        {
            SRCallbacks.PreSaveGameLoad += delegate (SceneContext s)
            {
                foreach (DirectedSlimeSpawner directedSlimeSpawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>().Where(delegate (DirectedSlimeSpawner ss)
                {
                    ZoneDirector.Zone zoneId = ss.GetComponentInParent<Region>(true).GetZoneId();
                    return zoneId == ZoneDirector.Zone.QUARRY;
                }))
                {
                    foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                    {
                        List<SlimeSet.Member> list = new List<SlimeSet.Member>(spawnConstraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.LATERN_SLIME)
                                ,weight = 0.5f
                            }
                        };
                        spawnConstraint.slimeset.members = list.ToArray();
                    }
                }
            };

            
        }

        public static (SlimeDefinition, GameObject) CreateSandSlime(Identifiable.Id SAND_SLIME, String SandSlime)
        {
            SlimeDefinition PuddleSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PUDDLE_SLIME);
            SlimeDefinition slimeDefinition = (SlimeDefinition)PrefabUtils.DeepCopyObject(PuddleSlimeDefinition);
            slimeDefinition.AppearancesDefault = new SlimeAppearance[1];
            slimeDefinition.Diet.Produces = new Identifiable.Id[1]
            {
                Id.SAND_PLORT
            };

            slimeDefinition.Diet.MajorFoodGroups = new SlimeEat.FoodGroup[1]
            {
                SlimeEat.FoodGroup.FRUIT
            };

            slimeDefinition.Diet.AdditionalFoods = new Identifiable.Id[0];

            slimeDefinition.Diet.Favorites = new Identifiable.Id[1]
            {
                Identifiable.Id.MANGO_FRUIT
            };

            slimeDefinition.Diet.EatMap?.Clear();
            slimeDefinition.CanLargofy = false;
            slimeDefinition.FavoriteToys = new Identifiable.Id[1]
            {
                Identifiable.Id.CHARCOAL_BRICK_TOY
            };
            slimeDefinition.Name = SandSlime;
            slimeDefinition.IdentifiableId = SAND_SLIME;

            GameObject slimeObject = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_SLIME));
            slimeObject.name = "SandSlime";
            slimeObject.GetComponent<PlayWithToys>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeAppearanceApplicator>().SlimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeEat>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<Identifiable>().id = SAND_SLIME;
            UnityEngine.Object.Destroy(slimeObject.GetComponent<PinkSlimeFoodTypeTracker>());


            SlimeAppearance slimeAppearance = (SlimeAppearance)PrefabUtils.DeepCopyObject(PuddleSlimeDefinition.AppearancesDefault[0]);
            slimeDefinition.AppearancesDefault[0] = slimeAppearance;
            SlimeAppearanceStructure[] structures = slimeAppearance.Structures;
            foreach (SlimeAppearanceStructure slimeAppearanceStructure in structures)
            {
                Material[] defaultMaterials = slimeAppearanceStructure.DefaultMaterials;
                if (defaultMaterials != null && defaultMaterials.Length != 0)
                {
                    Material material = UnityEngine.Object.Instantiate(SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PUDDLE_SLIME).AppearancesDefault[0].Structures[0].DefaultMaterials[0]);
                    material.SetColor("_TopColor", new Color32(237, 169, 96, 255));
                    material.SetColor("_MiddleColor", new Color32(237, 169, 96, 255));
                    material.SetColor("_BottomColor", new Color32(237, 169, 96, 255));
                    material.SetColor("_SpecColor", new Color32(237, 169, 96, 255));
                    material.SetFloat("_Shininess", 1f);
                    material.SetFloat("_Gloss", 1f);
                    slimeAppearanceStructure.DefaultMaterials[0] = material;
                }
            }
            SlimeExpressionFace[] expressionFaces = slimeAppearance.Face.ExpressionFaces;
            for (int k = 0; k < expressionFaces.Length; k++)
            {
                SlimeExpressionFace slimeExpressionFace = expressionFaces[k];
                if ((bool)slimeExpressionFace.Mouth)
                {
                    slimeExpressionFace.Mouth.SetColor("_MouthBot", new Color32(237, 169, 96, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthMid", new Color32(237, 169, 96, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthTop", new Color32(237, 169, 96, 255));
                }
                if ((bool)slimeExpressionFace.Eyes)
                {
                    slimeExpressionFace.Eyes.SetColor("_EyeRed", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeGreen", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeBlue", new Color32(0, 0, 0, 255));
                }
            }
            slimeAppearance.Face.OnEnable();
            slimeAppearance.ColorPalette = new SlimeAppearance.Palette
            {
                Top = new Color32(237, 169, 96, 255),
                Middle = new Color32(237, 169, 96, 255),
                Bottom = new Color32(237, 169, 96, 255)
            };
            slimeObject.GetComponent<SlimeAppearanceApplicator>().Appearance = slimeAppearance;


            if (RosaSlimeAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
            }


            RosaSlimeAssetBundle.LoadAsset<Sprite>("Lantern_Plort");

            GameObject GameObject2 = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_PLORT)); //It can be any plort, but pink works the best. 
            GameObject2.name = "LaternPlort";

            GameObject2.GetComponent<Identifiable>().id = Id.SAND_PLORT;
            GameObject2.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;

            GameObject2.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate<Material>(GameObject2.GetComponent<MeshRenderer>().material);
            Color PureWhite = new Color32(255, 255, 255, byte.MaxValue); // RGB   
            Color White = Color.white;
            //Pretty self explanatory. These change the color of the plort. You can set the colors to whatever you want.    
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_TopColor", new Color32(237, 169, 96, 255));
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", new Color32(237, 169, 96, 255));
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", new Color32(237, 169, 96, 255));

            LookupRegistry.RegisterIdentifiablePrefab(GameObject2);



            TranslationPatcher.AddActorTranslation("l.sand_plort", "Sand Plort");
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.SAND_PLORT);
            Identifiable.PLORT_CLASS.Add(Id.SAND_PLORT);
            Identifiable.NON_SLIMES_CLASS.Add(Id.SAND_PLORT);
            
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, GameObject2);
            // Icon that is below is just a placeholder. You can change it to anything or add your own!

            Sprite PlortIcon = RosaSlimeAssetBundle.LoadAsset<Sprite>("Sand_Plort");
            LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(Id.SAND_PLORT, PureWhite, PlortIcon));
            AmmoRegistry.RegisterSiloAmmo((System.Predicate<SiloStorage.StorageType>)(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.PLORT), Id.LATERN_PLORT);

            float price = 175f; //Starting price for plort   
            float saturation = 5f; //Can be anything. The higher it is, the higher the plort price changes every day. I'd recommend making it small so you don't destroy the economy lol.   
            PlortRegistry.AddEconomyEntry(Id.SAND_PLORT, price, saturation); //Allows it to be sold while the one below this adds it to plort market.   
            PlortRegistry.AddPlortEntry(Id.SAND_PLORT); //PlortRegistry.AddPlortEntry(YourCustomEnum, new ProgressDirector.ProgressType[1]{ProgressDirector.ProgressType.NONE});   
            DroneRegistry.RegisterBasicTarget(Id.SAND_PLORT);
            AmmoRegistry.RegisterRefineryResource(Id.SAND_PLORT);  //For if you want to make a gadget that uses it  


            LookupRegistry.RegisterIdentifiablePrefab(slimeObject);
            SlimeRegistry.RegisterSlimeDefinition(slimeDefinition);
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.SAND_SLIME));
            Sprite icon2 = RosaSlimeAssetBundle.LoadAsset<Sprite>("sand_slime");
            slimeAppearance.Icon = RosaSlimeAssetBundle.LoadAsset<Sprite>("sand_slime");
            LookupRegistry.RegisterVacEntry(Id.SAND_SLIME, new Color32(80, 0, 0, byte.MaxValue), icon2);
            slimeAppearance.ColorPalette.Ammo = new Color32(80, 0, 0, byte.MaxValue);
            TranslationPatcher.AddActorTranslation("l.sand_slime", "Sand Slime");
            SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.SAND_SLIME).GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;



            CreateSandSpawner();


            return (slimeDefinition, slimeObject);


        }

        public static void CreateSandSpawner()
        {
            SRCallbacks.PreSaveGameLoad += delegate (SceneContext s)
            {
                foreach (DirectedSlimeSpawner directedSlimeSpawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>().Where(delegate (DirectedSlimeSpawner ss)
                {
                    ZoneDirector.Zone zoneId = ss.GetComponentInParent<Region>(true).GetZoneId();
                    return zoneId == ZoneDirector.Zone.DESERT;
                }))
                {
                    foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                    {
                        List<SlimeSet.Member> list = new List<SlimeSet.Member>(spawnConstraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.SAND_SLIME)
                                ,weight = 0.5f
                            }
                        };
                        spawnConstraint.slimeset.members = list.ToArray();
                    }
                }
            };


        }



        public static (SlimeDefinition, GameObject) CreateMineSlime(Identifiable.Id MINE_SLIME, String MineSlime)
        {

            SlimeDefinition pinkSlimeDefinition = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME);
            SlimeDefinition slimeDefinition = (SlimeDefinition)PrefabUtils.DeepCopyObject(pinkSlimeDefinition);
            slimeDefinition.AppearancesDefault = new SlimeAppearance[1];
            slimeDefinition.Diet.Produces = new Identifiable.Id[1]
            {
                Id.MINE_PLORT
            };

            slimeDefinition.Diet.MajorFoodGroups = new SlimeEat.FoodGroup[1]
            {
                SlimeEat.FoodGroup.VEGGIES
            };

            slimeDefinition.Diet.AdditionalFoods = new Identifiable.Id[0];

            slimeDefinition.Diet.Favorites = new Identifiable.Id[1]
            {
                Identifiable.Id.CARROT_VEGGIE
            };

            slimeDefinition.Diet.EatMap?.Clear();
            slimeDefinition.CanLargofy = false;
            slimeDefinition.FavoriteToys = new Identifiable.Id[1]
            {
                Identifiable.Id.BOMB_BALL_TOY
            };
            slimeDefinition.Name = MineSlime;
            slimeDefinition.IdentifiableId = MINE_SLIME;

            GameObject slimeObject = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.BOOM_SLIME));
            slimeObject.name = "MineSlime";
            slimeObject.GetComponent<PlayWithToys>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeAppearanceApplicator>().SlimeDefinition = slimeDefinition;
            slimeObject.GetComponent<SlimeEat>().slimeDefinition = slimeDefinition;
            slimeObject.GetComponent<Identifiable>().id = MINE_SLIME;
            UnityEngine.Object.Destroy(slimeObject.GetComponent<PinkSlimeFoodTypeTracker>());


            SlimeAppearance slimeAppearance = (SlimeAppearance)PrefabUtils.DeepCopyObject(pinkSlimeDefinition.AppearancesDefault[0]);
            slimeDefinition.AppearancesDefault[0] = slimeAppearance;
            slimeAppearance.Structures = new SlimeAppearanceStructure[]
            {
              slimeAppearance.Structures[0],
              new SlimeAppearanceStructure(slimeAppearance.Structures[0]),
              new SlimeAppearanceStructure(slimeAppearance.Structures[0])
            };
            Material material = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.TABBY_SLIME).AppearancesDefault[0].Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(158, 161, 111, 255));
            material.SetColor("_MiddleColor", new Color32(158, 161, 111, 255));
            material.SetColor("_BottomColor", new Color32(68, 86, 96, 255));
            material.SetColor("_SpecColor", new Color32(158, 161, 111, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            material.SetTexture("_StripeTexture", RosaSlimeAssetBundle.LoadAsset<Texture2D>("assets/mine_pattern.png"));
            slimeAppearance.Structures[0].DefaultMaterials = new Material[] { material };
            material = slimeAppearance.Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(68, 86, 96, 255));
            material.SetColor("_MiddleColor", new Color32(68, 86, 96, 255));
            material.SetColor("_BottomColor", new Color32(68, 86, 96, 255));
            material.SetColor("_SpecColor", new Color32(68, 86, 96, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            slimeAppearance.Structures[1].DefaultMaterials = new Material[] { material };
            material = slimeAppearance.Structures[0].DefaultMaterials[0].Clone();
            material.SetColor("_TopColor", new Color32(68, 86, 96, 255));
            material.SetColor("_MiddleColor", new Color32(68, 86, 96, 255));
            material.SetColor("_BottomColor", new Color32(68, 86, 96, 255));
            material.SetColor("_SpecColor", new Color32(68, 86, 96, 255));
            material.SetFloat("_Shininess", 1f);
            material.SetFloat("_Gloss", 1f);
            slimeAppearance.Structures[2].DefaultMaterials = new Material[] { material };
            SlimeExpressionFace[] expressionFaces = slimeAppearance.Face.ExpressionFaces;
            for (int k = 0; k < expressionFaces.Length; k++)
            {
                SlimeExpressionFace slimeExpressionFace = expressionFaces[k];
                if ((bool)slimeExpressionFace.Mouth)
                {
                    slimeExpressionFace.Mouth.SetColor("_MouthBot", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthMid", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Mouth.SetColor("_MouthTop", new Color32(0, 0, 0, 255));
                }
                if ((bool)slimeExpressionFace.Eyes)
                {
                    slimeExpressionFace.Eyes.SetColor("_EyeRed", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeGreen", new Color32(0, 0, 0, 255));
                    slimeExpressionFace.Eyes.SetColor("_EyeBlue", new Color32(0, 0, 0, 255));
                }
            }
            slimeAppearance.Face.OnEnable();
            slimeAppearance.ColorPalette = new SlimeAppearance.Palette
            {
                Top = new Color32(68, 86, 96, 255),
                Middle = new Color32(68, 86, 96, 255),
                Bottom = new Color32(68, 86, 96, 255)
            };
            slimeObject.GetComponent<SlimeAppearanceApplicator>().Appearance = slimeAppearance;


            if (RosaSlimeAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
            }

            slimeAppearance.ExplosionAppearance = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.BOOM_SLIME).AppearancesDefault[0].ExplosionAppearance;

            var pinkBodyApp = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME).AppearancesDefault[0].Structures[0].Element.Prefabs[0];
            slimeAppearance.Structures[0].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[0].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.gameObject.CreatePrefabCopy().GetComponent<SlimeAppearanceObject>() };
            var skin = slimeAppearance.Structures[0].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>();
            skin.sharedMesh = Object.Instantiate(skin.sharedMesh);
            if (slimeAppearance.Structures[0].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
            {
                Debug.Log("Couldnt load mine mesh");
            }
            slimeAppearance.Structures[1].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[1].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.CreatePrefab() };
            slimeAppearance.Structures[1].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_mine_ring.obj");
            slimeAppearance.Structures[1].SupportsFaces = false;
            if (slimeAppearance.Structures[1].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
                Debug.Log("Couldnt load mine ring mesh");
            slimeAppearance.Structures[2].Element = ScriptableObject.CreateInstance<SlimeAppearanceElement>();
            slimeAppearance.Structures[2].Element.Prefabs = new SlimeAppearanceObject[] { pinkBodyApp.CreatePrefab() };
            slimeAppearance.Structures[2].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh = RosaSlimeAssetBundle.LoadAsset<Mesh>("assets/slime_mine_triggers.obj");
            slimeAppearance.Structures[2].SupportsFaces = false;
            if (slimeAppearance.Structures[2].Element.Prefabs[0].GetComponent<SkinnedMeshRenderer>().sharedMesh == null)
                Debug.Log("Couldnt load mine triggers mesh");
            MeshUtils.GenerateBoneData(slimeObject.GetComponent<SlimeAppearanceApplicator>(), slimeAppearance.Structures[0].Element.Prefabs[0], 0.125f, 1,
                slimeAppearance.Structures[1].Element.Prefabs[0],
                slimeAppearance.Structures[2].Element.Prefabs[0]);


            RosaSlimeAssetBundle.LoadAsset<Sprite>("Mine");

            GameObject GameObject2 = PrefabUtils.CopyPrefab(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_PLORT)); //It can be any plort, but pink works the best. 
            GameObject2.name = "MinePlort";

            GameObject2.GetComponent<Identifiable>().id = Id.MINE_PLORT;
            GameObject2.GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;

            GameObject2.GetComponent<MeshRenderer>().material = UnityEngine.Object.Instantiate<Material>(GameObject2.GetComponent<MeshRenderer>().material);
            Color PureWhite = new Color32(255, 255, 255, byte.MaxValue); // RGB   
            Color White = Color.white;
            //Pretty self explanatory. These change the color of the plort. You can set the colors to whatever you want.    
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_TopColor", new Color32(68, 86, 96, 255));
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", new Color32(68, 86, 96, 255));
            GameObject2.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", new Color32(68, 86, 96, 255));

            LookupRegistry.RegisterIdentifiablePrefab(slimeObject);
            SlimeRegistry.RegisterSlimeDefinition(slimeDefinition);
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.MINE_SLIME));
            Sprite icon2 = RosaSlimeAssetBundle.LoadAsset<Sprite>("mine_slime");
            slimeAppearance.Icon = RosaSlimeAssetBundle.LoadAsset<Sprite>("mine_slime");
            LookupRegistry.RegisterVacEntry(Id.MINE_SLIME, new Color32(80, 0, 0, byte.MaxValue), icon2);
            slimeAppearance.ColorPalette.Ammo = new Color32(80, 0, 0, byte.MaxValue);
            SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.MINE_SLIME).GetComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            TranslationPatcher.AddActorTranslation("l.mine_slime", "Mine Slime");

            LookupRegistry.RegisterIdentifiablePrefab(GameObject2);



            TranslationPatcher.AddActorTranslation("l.mine_plort", "Mine Plort");
            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.MINE_PLORT);
            Identifiable.PLORT_CLASS.Add(Id.MINE_PLORT);
            Identifiable.NON_SLIMES_CLASS.Add(Id.MINE_PLORT);

            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT, GameObject2);
            // Icon that is below is just a placeholder. You can change it to anything or add your own!

            Sprite PlortIcon = RosaSlimeAssetBundle.LoadAsset<Sprite>("Mine");
            LookupRegistry.RegisterVacEntry(VacItemDefinition.CreateVacItemDefinition(Id.MINE_PLORT, PureWhite, PlortIcon));
            AmmoRegistry.RegisterSiloAmmo((System.Predicate<SiloStorage.StorageType>)(x => x == SiloStorage.StorageType.NON_SLIMES || x == SiloStorage.StorageType.PLORT), Id.MINE_PLORT);

            float price = 95f; //Starting price for plort   
            float saturation = 5f; //Can be anything. The higher it is, the higher the plort price changes every day. I'd recommend making it small so you don't destroy the economy lol.   
            PlortRegistry.AddEconomyEntry(Id.MINE_PLORT, price, saturation); //Allows it to be sold while the one below this adds it to plort market.   
            PlortRegistry.AddPlortEntry(Id.MINE_PLORT); //PlortRegistry.AddPlortEntry(YourCustomEnum, new ProgressDirector.ProgressType[1]{ProgressDirector.ProgressType.NONE});   
            DroneRegistry.RegisterBasicTarget(Id.MINE_PLORT);
            AmmoRegistry.RegisterRefineryResource(Id.MINE_PLORT);  //For if you want to make a gadget that uses it  


            CreateMineSpawner();


            return (slimeDefinition, slimeObject);
        }
            public static void CreateMineSpawner()
            {
                SRCallbacks.PreSaveGameLoad += delegate (SceneContext s)
                {
                    foreach (DirectedSlimeSpawner directedSlimeSpawner in UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>().Where(delegate (DirectedSlimeSpawner ss)
                    {
                        ZoneDirector.Zone zoneId = ss.GetComponentInParent<Region>(true).GetZoneId();
                        return zoneId == ZoneDirector.Zone.RUINS;
                    }))
                    {
                        foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                        {
                            List<SlimeSet.Member> list = new List<SlimeSet.Member>(spawnConstraint.slimeset.members)
                        {
                            new SlimeSet.Member
                            {
                                prefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.MINE_SLIME)
                                ,weight = 0.5f
                            }
                        };
                            spawnConstraint.slimeset.members = list.ToArray();
                        }
                    }
                };


            }
    }
}

