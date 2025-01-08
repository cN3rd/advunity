using Homework2.Scripts;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    [SerializeField] private Ob1 Ob1;
    [SerializeField] private Ob2 Ob2;
    public int Damage { get { return Ob1.Damage; } }
    public string Name { get { return Ob1.Name; } }

    private void Awake()
    {
        Ob2.SayHi += Ob1.OnHitHi;
        Ob2.SayInteger += Ob1.OnHitInt;
        Ob2.SayString += Ob1.OnHitString;
    }


}
