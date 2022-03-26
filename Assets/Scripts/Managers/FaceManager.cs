using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyGarden.Gameplay 
{
    internal class FaceManager : Singleton<FaceManager>
    {
        [SerializeField] private List<GameObject> faces = new List<GameObject>();

        internal GameObject GetFace() {
            int randomNumber = Random.Range(0,faces.Count);

            GameObject face = Instantiate(faces[randomNumber]);

            return face;
        }
    }
}
