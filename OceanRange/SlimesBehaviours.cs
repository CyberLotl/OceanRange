using UnityEngine;
using DG.Tweening;
using MonomiPark.SlimeRancher.Regions;
using System.Collections;

namespace OceanRange
{
    internal class SlimesBehaviours
    {
        class WhenAgitated : SlimeSubbehaviour
        {
            public override void Awake() => base.Awake();

            public override void Action()
            {
                //Everything that you want
            }

            public override float Relevancy(bool isGrounded)
            {
                if (this.emotions.GetCurr(SlimeEmotions.Emotion.AGITATION) >= 0.5f) //When the slime starts to get agitated
                    return 1f; //This means do the action. Important; don't remove it
                return 0f; //This means don't do the actions. Important; don't remove it
            }

            public override void Selected() { }

        }
    }
}
