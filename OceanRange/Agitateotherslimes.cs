using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace OceanRange
{
    class Empathize : MonoBehaviour
    {
        float time = 0;
        SlimeEmotions emo;
        bool canFeral = false;
        void Awake()
        {
            emo = GetComponent<SlimeEmotions>();
            canFeral = GetComponent<SlimeFeral>() && GetComponent<SlimeFeral>().enabled;
        }
        void Update()
        {
            time += Time.deltaTime;
            if (time > 1)
            {
                time %= 1;
                foreach (var col in Physics.OverlapSphere(transform.position, 4))
                {
                    var otherEmo = col.GetComponentInParent<SlimeEmotions>();
                    if (otherEmo && otherEmo != emo && otherEmo.enabled)
                    {
                        otherEmo.model.emotionAgitation.currVal = emo.model.emotionAgitation.currVal;
                        otherEmo.model.emotionFear.currVal = emo.model.emotionFear.currVal;
                        if (canFeral && otherEmo.GetComponent<SlimeFeral>())
                            otherEmo.model.isFeral = emo.model.isFeral;
                    }
                }
            }
        }
    }
}
