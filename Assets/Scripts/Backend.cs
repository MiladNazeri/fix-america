using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class Backend : MonoBehaviour
{
    // Keeping it a Dict<> so that we can associate weights later on if we want
    HashSet<string> positive_verbs = new HashSet<string>() {
        "Please",
        "Improve the life of",
        "Reduce taxes on",
		"Decriminalize",
		"Reduce Tax on",
		"Direct the Homeland Security council to protect the",
		"Authorize grants for",
		"To provide a study by the National Academy of Medicine for",
		"Establish a refund of grants for",
		"End the epidemic of",
		"Reduce the age of legalization of ownership of",
		"Improve honesty in sales of",
		"Protection of",
		"The Director of the Centers for Disease Control and Prevention, shall carry out a national program to conduct and support activities regarding",
		"To reauthorize mandatory funding programs for"
    };

    // Keeping it a Dict<> so that we can associate weights later on if we want
    HashSet<string> negative_verbs = new HashSet<string>() {
        "Deport",
        "Segregate",
        "Increase taxes on",
        "Ban all",
		"Increase the limitation on sales of",
		"Require the reporting of bullying of",
		"Freeze assets",
		"War on",
		"Declare national emergency on",
		"Eliminate public funding of ",
		"To authorize the Marshal of the Supreme Court and the Supreme Court Police to protect",
		"To amend the Balanced Budget and Emergency Deficit Control Act of"
    };

    Dictionary<string, Tuple<float, float, float>> population_opinions =
        new Dictionary<string, Tuple<float, float, float>>() {
            {"Women", null},
            {"Environmentalists", null},
            {"Cats", null},
            {"Dogs", null},
            {"Men", null},
            {"Mega corporations", null},
            {"Millionaires", null},
            {"Medicare beneficiaries", null},
            {"Football fans", null},
            {"Golf players", null},
            {"Flat Earthers", null},
			{"Electric Vehicles", null},
			{"Creationist", null},
			{"Scientologist", null},
			{"Cannabis users", null},
			{"Veterans", null},
			{"Weather enthusiasts", null},
			{"Glasses owners", null},
			{"Gun owners", null},
			{"Nazis", null},
			{"Robots", null},
			{"Victims of Facebook", null},
			{"Terrorists", null}
        };

    public float happyGroupMultiplier = 1f;
    public float unhappyGroupMultiplier = -1f;

    public static Backend Instance { get; private set; }

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Another one already exists, suicide ourselves, there can only be one
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        var keys = new List<string>();
        keys.AddRange(population_opinions.Keys);
        // Because we draw complementary numbers (number1 and number2), number2 will
        // always be smaller. So in order no to bias towards positive or negative
        // we have to, in addition to the random numbers, make a coin flip
        // for which population (positive, negative) will get the higher score
        var coin_flip = (((int)(UnityEngine.Random.Range(0f, 100f))) % 2) == 0;
        foreach (var topic in keys)
        {
            var number1 = UnityEngine.Random.Range(0f, 1f);
            var number2 = UnityEngine.Random.Range(0f, 1f - number1);
            var pos = coin_flip ? number1 : number2;
            var neg = !coin_flip ? number1 : number2;
            var neutral = 1f - pos - neg;
            population_opinions[topic] = new Tuple<float, float, float>(pos, neg, neutral);
        }
        Debug.Log("Population has been initialized to:");
        DisplayPop();
    }

    private string randchoice(IEnumerable<string> list)
    {
        var count = list.Count();
        var index = (int)UnityEngine.Random.Range(0, count - 1);
        return list.ElementAt(index);
    }

    // Returns:
    // Tuple<actual_text, negative_group_id, positive_group_id>
    public Tuple<string, string, string> GetNewBill()
    {
        var pos = randchoice(positive_verbs);
        var neg = randchoice(negative_verbs);
        var g_neg = randchoice(population_opinions.Keys);
        var g_pos = g_neg;
        while (g_neg == g_pos)
        {
            g_pos = randchoice(population_opinions.Keys);
        }
        var actual_text = $"{neg} {g_neg} to {pos} {g_pos}";
        return new Tuple<string, string, string>(actual_text, g_neg, g_pos);
    }

    private void DisplayPop()
    {
        foreach (var keyVal in population_opinions)
        {
            Debug.Log($"Topic pop distrib:\t{keyVal.Key}\t\t{keyVal.Value.Item1}\t{keyVal.Value.Item2}\t{keyVal.Value.Item3}");
        }
    }

    public float GetBillPopularity(string groupNegative, string groupPositive)
    {
        // Formula explanation:
        // happygroup * 1 - 1 * unhappygroup varies between -1 and +1
        // but we want to map it linearly to 0 - 1 in order to have a percentage
        // so for that we basically divide it by two, that gives -0.5 to +0.5 and then you add 1
        // and you're mapped to 0 to 1 now. If you're full negative (-1) you have 0 (0.5 + 0.5 * -1 = 0)
        // and if you are full positive you have 1 (0.5 + 0.5 * 1)
        // which can also be written as 0.5 * (1 + score)
        return 0.5f * (
            1 +
            happyGroupMultiplier * population_opinions[groupPositive].Item1 + unhappyGroupMultiplier * population_opinions[groupNegative].Item2
        );
    }

}