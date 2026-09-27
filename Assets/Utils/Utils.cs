using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class Utils
    {

        public static IEnumerable<string> GetCSVLine(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                yield return line;
            }
        }

        public static void DebugStringArray(this string[] array)
        {
            foreach (var s in array)
            {
                Debug.Log(s);
            }
        }

        public static GameObject FindNearest(Transform character, float radius,string tagSearched)
        {
            GameObject nearest = null;
            float nearestDistance = float.MaxValue;
            Collider[] colliders =  Physics.OverlapSphere(character.position, radius);
            if (colliders.Length == 0)
            {
                Debug.Log("<color=red>Warning</color>: No targets found within radius.");
                return null;
            }
            foreach (Collider target in colliders)
            {
                if(Vector3.Distance(character.position, target.transform.position) < nearestDistance && target.CompareTag(tagSearched))
                {
                    nearest = target.gameObject;
                    nearestDistance = Vector3.Distance(character.position, target.transform.position);
                }
            }
            if(!nearest)
                Debug.Log("<color=red>Warning</color>: No nearest target found.");
            return nearest;
        }

    }

}
