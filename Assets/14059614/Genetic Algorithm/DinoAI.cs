using UnityEngine;

public class DinoAI : MonoBehaviour
{
    [HideInInspector]
    public float Health, Weight, Hunger, Age, Speed;
    [HideInInspector]
    public int MaxAnkyHealth, MaxAnkyWeight, MaxAnkyHunger, MaxAnkyAge;
    [HideInInspector]
    public int MinAnkyHealth, MinAnkyWeight, MinAnkyHunger, MinAnkyAge;
    [HideInInspector]
    public int MaxRaptyHealth, MaxRaptyWeight, MaxRaptyHunger, MaxRaptyAge;
    [HideInInspector]
    public int MinRaptyHealth, MinRaptyWeight, MinRaptyHunger, MinRaptyAge;
    [HideInInspector]
    public bool Healthy = false, Fit = false, Hungry = false;
    

    float timer = 0.0f;

    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer > 0)
        {
            Hunger += 1.0f * Time.deltaTime;
            Weight -= 0.2f * Time.deltaTime;
        }
    }

    public void RNG(float stat, int min, int max)
    {
        stat = Random.Range(min, max);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Statistics(int Dino)
    {
        switch (Dino)
        {
            case 1:
                MinRaptyHealth = 60;
                MinRaptyWeight = 90;
                MinRaptyHunger = 45;
                MinRaptyAge = 1;

                MaxRaptyHealth = 120;
                MaxRaptyWeight = 130;
                MaxRaptyHunger = 85;
                MaxRaptyAge = 10;

                Speed = Age + 1;

                RNG(Health, MinRaptyHealth, MaxRaptyHealth);
                RNG(Weight, MinRaptyWeight, MaxRaptyWeight);
                RNG(Hunger, MinRaptyHunger, MaxRaptyHunger);
                RNG(Age, MinRaptyAge, MaxRaptyAge);
                break;
            case 2:
                MinAnkyHealth = 80;
                MinAnkyWeight = 100;
                MaxAnkyHunger = 35;
                MinAnkyAge = 1;

                MaxAnkyHealth = 140;
                MaxAnkyWeight = 140;
                MaxAnkyHunger = 75;
                MaxAnkyAge = 10;

                Speed = Age + 0.5f;

                RNG(Health, MinAnkyHealth, MaxAnkyHealth);
                RNG(Weight, MinAnkyWeight, MaxAnkyWeight);
                RNG(Hunger, MaxAnkyHunger, MaxAnkyHunger);
                RNG(Age, MinAnkyAge, MaxAnkyAge);
                break;
        }
    }
    public void BodyChecker(int Dino)
    {
        switch (Dino)
        {
            case 1:
                if (Hunger >= 90)
                {
                    Health -= 0.5f * Time.deltaTime;
                    if (Hunger >= MaxRaptyHunger)
                    {
                        Hunger = MaxRaptyHunger;
                    }
                    else if (Hunger <= MinRaptyHunger)
                    {
                        Hunger = MinRaptyHunger;
                    }
                }
                if (Weight > MaxRaptyWeight || Weight < MinRaptyWeight)
                {
                    Health -= 0.5f * Time.deltaTime;
                    if (Weight <= 0)
                    {
                        Weight = 0;
                    }
                }
                if (Hunger > MaxRaptyHunger) Health -= 0.5f * Time.deltaTime;
                if (Age > MaxRaptyAge) Die();
                if (Health <= 0) Die();

                if (Health >= MaxRaptyHealth * 0.7) Healthy = true;
                else if (Health < MaxRaptyHealth * 0.7) Healthy = false;

                if (Weight <= MaxRaptyWeight * 0.8 && Weight >= MaxRaptyWeight * 0.6) Fit = true;
                else if (Weight < MaxRaptyWeight * 0.6 || Weight > 0.8) Fit = false;

                if (Hunger <= MinRaptyHunger) Hungry = false;
                else if (Hunger > MinRaptyHunger) Hungry = true;
                break;
            case 2:
                if (Hunger >= 90)
                {
                    Health -= 0.5f * Time.deltaTime;
                    if (Hunger >= MaxAnkyHunger)
                    {
                        Hunger = MaxAnkyHunger;
                    }
                    else if (Hunger <= MinAnkyHunger)
                    {
                        Hunger = MinAnkyHunger;
                    }
                }
                if (Weight > MaxAnkyWeight || Weight < MinAnkyWeight)
                {
                    Health -= 0.5f * Time.deltaTime;
                    if (Weight <= 0)
                    {
                        Weight = 0;
                    }
                }
                if (Hunger > MaxAnkyHunger) Health -= 0.5f * Time.deltaTime;
                if (Age > MaxAnkyAge) Die();
                if (Health <= 0) Die();

                if (Health >= MaxAnkyHealth * 0.7) Healthy = true;
                else if (Health < MaxAnkyHealth * 0.7) Healthy = false;

                if (Weight <= MaxAnkyWeight * 0.8 && Weight >= MaxAnkyWeight * 0.6) Fit = true;
                else if (Weight < MaxAnkyWeight * 0.6 || Weight > 0.8) Fit = false;

                if (Hunger <= MinAnkyHunger) Hungry = false;
                else if (Hunger > MinAnkyHunger) Hungry = true;
                break;
        }
    }
}