using AssetsLib;
using HarmonyLib;
using SRML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace OceanRange
{
    public class Music
    {
        [HarmonyPatch(typeof(MusicDirector), "GetSceneCue")]
        class Patch_SceneCue
        {

            static void Postfix(Scene scene, ref SECTR_AudioCue __result)
            {
                if (scene.name == "MainMenu") __result = GameObjectUtils.CreateScriptableObject<SECTR_AudioCue>((x) => {
                    x.AudioClips.Add(new SECTR_AudioCue.ClipData(Custom_Creator.RosaSlimeAssetBundle.LoadAsset<AudioClip>("Great_Reef_Night_Theme_loopable_mp3"))); 
                    x.Volume *= 0.5f;
                });;
            }
        }
    }
}
