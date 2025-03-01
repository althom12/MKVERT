using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }
    public static FMODEvents instance {get; private set;}

    private void Awake()
    {
      if (instance != null)
      {
         Debug.Log("Found more than one FMOD Events instance in scene");
      } 
      instance = this;
   }
}
