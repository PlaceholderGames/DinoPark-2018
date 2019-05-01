using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Transform memberPrefab;
    public Transform enemyPrefab;

    public int numberOfMembers;
    public int numberOfEnemies;

    public List<Member> members;
    public List<EnemyNPC> enemies;

    public float bounds;
    public float spawnRadius;

    // Use this for initialization
    void Start ()
    {

        members = new List<Member>();
        enemies = new List<EnemyNPC>();

        Spawn(memberPrefab, numberOfMembers);
        Spawn(enemyPrefab, numberOfEnemies);

        members.AddRange(FindObjectsOfType<Member>());
        enemies.AddRange(FindObjectsOfType<EnemyNPC>());

    }

    void Spawn (Transform prefab, int count)
    {

        for (int i = 0; i < count; i++)
        {

            Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity);

        }

    }

    public List<Member> GetNeighbours (Member member, float radius)
    {

        List<Member> neighboursFound = new List<Member>();

        foreach (var otherMember in members)
        {

            if (otherMember == member)
            {

                continue;

            }

            if (Vector3.Distance(member.position, otherMember.position) <= radius)
            {

                neighboursFound.Add(otherMember);

            }

        }

        return neighboursFound;

    }

    public List<EnemyNPC> GetEnemies (Member member, float radius)
    {

        List<EnemyNPC> returnEnemies = new List<EnemyNPC>();

        foreach (var enemy in enemies)
        {

            if (Vector3.Distance(member.position, enemy.position) <= radius)
            {

                returnEnemies.Add(enemy);

            }

        }

        return returnEnemies;

    }

}
