using System;
using System.Collections.Generic;
using HarmonyLib;
using MonomiPark.SlimeRancher.DataModel;
using SRML.SR;
using SRML.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace OceanRange
{
    class Gordo_Creator
    {
        public static (SlimeDefinition, GameObject) CreateGordo(Identifiable.Id GordoId, String GordoName) 
        {
            GameObject Prefab =
                PrefabUtils.CopyPrefab(GameContext.Instance.LookupDirector?.GetGordo(Identifiable.Id.PINK_GORDO)); //The prefab of your gordo
            Prefab.name = GordoName;
            GameObject baseSlime =
                PrefabUtils.CopyPrefab(GameContext.Instance.LookupDirector?.GetPrefab(Id.ROSA_SLIME)); //The prefab of a slime that you will use for eyes, mouth and more
            baseSlime.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE; //Making this slime impossible to suck up
            SlimeDefinition baseSlimeDef =
                SRSingleton<GameContext>.Instance.SlimeDefinitions
                    .GetSlimeByIdentifiableId(Id.ROSA_SLIME); //The slimedefinition
                                                                            // Get Material
            Material ModelMat = baseSlimeDef.AppearancesDefault[0].Structures[0].DefaultMaterials[0];
            Material ModelMat2 = baseSlimeDef.AppearancesDefault[0].Structures[1].DefaultMaterials[0];
            Material ModelMat3 = baseSlimeDef.AppearancesDefault[0].Structures[2].DefaultMaterials[0];
            SlimeAppearanceObject ModelStructure = baseSlimeDef.AppearancesDefault[0].Structures[0].Element.Prefabs[0];
            SlimeAppearanceObject rosaGordoBodyApp2 = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Id.ROSA_SLIME).AppearancesDefault[0].Structures[1].Element.Prefabs[0];
            SlimeAppearanceObject rosaGordoBodyApp = GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Id.ROSA_SLIME).AppearancesDefault[0].Structures[2].Element.Prefabs[0];//The material and structure (colors and more)
            SlimeEyeComponents baseSlimeEyes = baseSlime.GetComponent<SlimeEyeComponents>(); //The eyes
            SlimeMouthComponents baseSlimeMouth = baseSlime.GetComponent<SlimeMouthComponents>(); //The mouth
                                                                                                  // Load Components
            Mesh RosaFrills01Mesh = rosaGordoBodyApp.GetComponent<Mesh>();
            Mesh RosaFrills02Mesh = rosaGordoBodyApp2.GetComponent<Mesh>();
            

            //Marker for map
            GordoDisplayOnMap disp = Prefab.GetComponent<GordoDisplayOnMap>(); //Making it possible to see the gordo on the map
            GameObject markerPrefab = PrefabUtils.CopyPrefab(disp.markerPrefab.gameObject); //Copying the gameobject of the maker

            GameObject Frills01 = new GameObject("Frills01");
            MeshFilter meshFilter = Frills01.AddComponent<MeshFilter>();
            meshFilter.mesh = RosaFrills01Mesh;
            Frills01.transform.position = new Vector3(434.3f, 6.0f, 3.5f);

            GameObject Frills02 = new GameObject("Frills02");
            MeshFilter meshFilter02 = Frills02.AddComponent<MeshFilter>();
            meshFilter.mesh = RosaFrills02Mesh;

            MeshRenderer meshRenderer01 = Frills01.AddComponent<MeshRenderer>();
            meshRenderer01.material = ModelMat2;

            MeshRenderer meshRenderer02 = Frills02.AddComponent<MeshRenderer>();
            meshRenderer02.material = ModelMat3;

            markerPrefab.name = "GordoRosaMarker";
            markerPrefab.GetComponent<Image>().sprite = null;
            //markerPrefab.GetComponent<Image>().sprite = Main.assetBundle.LoadAsset<Sprite>("NAME HERE"); //This is when you wanna add a custom image to your gordo, I assume you to know how asset bundles work
            disp.markerPrefab = markerPrefab.GetComponent<MapMarker>(); //Assign the custom mapmaker to the old one (replacing it with the new, said in other words)
                                                                        //Ids
            GordoIdentifiable iden = Prefab.GetComponent<GordoIdentifiable>(); //Getting the GordoIdentifiable (it's like the Identifiable.Id, but for Gordos)
            iden.id = GordoId; //Setting your own gordo id
            iden.nativeZones = new ZoneDirector.Zone[1] { ZoneDirector.Zone.REEF }; //Setting the native zones of the gordo (not sure what this does; probably it will become more probably to get that gordo on a gordo snare when setted in that zone. Or it will make the gordo spawn naturally)
                                                                                     //Appearance & diet
            GordoEat eat = Prefab.GetComponent<GordoEat>(); //Getting the gordoeat component
            SlimeDefinition oldDefinition = (SlimeDefinition)PrefabUtils.DeepCopyObject(eat.slimeDefinition); //copying the old slimedefinition of it
            if (Frills01.activeInHierarchy)
            {
                Debug.Log("Frills01 is active");
            }
            Debug.Log("Frills01's position is" + Frills01.transform.position);


            oldDefinition.AppearancesDefault = baseSlimeDef.AppearancesDefault;
            oldDefinition.Diet = baseSlimeDef.Diet;
            oldDefinition.IdentifiableId = GordoId;
            oldDefinition.name = GordoName;
            eat.slimeDefinition = oldDefinition;
            eat.targetCount = 50; //How many food it need to be fed to make it explode

            GameObject prefab =
                SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.KEY); //Choicing what the gordo will drop once it explodes

            List<GameObject> rews = new List<GameObject>() //Adding it on a custom list
            {
            prefab,
            prefab,
            prefab
            };

            GordoRewards rew = Prefab.GetComponent<GordoRewards>(); //Getting the gordoawards component
            rew.rewardPrefabs = rews.ToArray(); //Adding our list to the present one (replacing the old with the new)
            rew.slimePrefab = GameContext.Instance.LookupDirector.GetPrefab(Id.ROSA_SLIME); //Choicing what it will drop once it explodes
            rew.rewardOverrides = new GordoRewards.RewardOverride[0];

            //setting the materials
            GameObject child = Prefab.transform.Find("Vibrating/slime_gordo").gameObject;
            SkinnedMeshRenderer render = child.GetComponent<SkinnedMeshRenderer>();
            Frills01.transform.SetParent(child.transform, false);
            Frills02.transform.SetParent(child.transform, false);
            render.sharedMaterial = ModelMat;
            render.sharedMaterials[0] = ModelMat;
            render.material = ModelMat;
            render.materials[0] = ModelMat;
            render.materials[1] = ModelMat2;
            render.materials[2] = ModelMat3;
            //registering some stuff
            TranslationPatcher.AddPediaTranslation("t.rosa_gordo", "Rosa Gordo");
            LookupRegistry.RegisterGordo(Prefab);
            return (null, null);
        }

        [HarmonyPatch(typeof(GordoSnare))]
        [HarmonyPatch("GetGordoIdForBait")]
        public class Patch_Gordo
        {
            [HarmonyPriority(Priority.First)]
            public static bool Prefix(GordoSnare __instance, ref Identifiable.Id __result)
            {
                Identifiable.Id baitId = __instance.GetPrivateField<SnareModel>("model").baitTypeId;
                if (baitId == Identifiable.Id.DEEP_BRINE_CRAFT && Randoms.SHARED.GetInRange(0, 100) <= 100)//75% chance of your gordo. To make it clear, the 'RESOURCE' is the id of an slime science material, so like royal jelly and etc. But it can be your custom too! (see the next tutorials to see how to create custom resources)
                {
                    __result = Id.ROSA_GORDO;
                    return false;
                }
                return true;
            }
        }
    }
}
