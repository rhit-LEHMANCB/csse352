using UnityEngine;

public interface IStrategy
{
    public Move Execute(Monster user, Monster enemy);
}
