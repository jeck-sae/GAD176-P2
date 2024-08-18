using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
namespace Giacomo
{

    public class ProjectRequirements : MonoBehaviour
    {
        Rigidbody2D rb;
        public RequirementsSettings forceSettings;

        UnityEvent<bool> nearSomething;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        private bool isNearSomething;

        void Update()
        {
            var hit = Physics2D.BoxCastAll(transform.position, forceSettings.area, 0, Vector2.zero);

            if (hit.Any(x => x.transform != transform))
            {
                rb.AddForce(forceSettings.force, ForceMode2D.Impulse);
                
                if (!isNearSomething)
                {
                    nearSomething?.Invoke(true);
                    isNearSomething = true;
                }
            }
            else if (isNearSomething)
            {
                nearSomething?.Invoke(false);
                isNearSomething = false;
            }
        }
    }



}