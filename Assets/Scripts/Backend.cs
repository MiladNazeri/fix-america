using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class Backend : MonoBehaviour
{
	List<string> positive_verbs = new List<string>() {
        "Please",
        "Improve the life of",
        "Reduce taxes on",
		"Decriminalize",
		"Reduce Tax on",
		"Direct the Homeland Security council to protect the",
		"Authorize grants for",
		"Provide a study by the National Academy of Medicine for",
		"Establish a refund of grants for",
		"End the epidemic of",
		"Enlist the Director of the Centers for Disease Control and Prevention, to carry out a national program to conduct and support activities regarding all",
		"Reauthorize mandatory funding programs for",
		"Extend the program of temporary assistance for",
		"Promote the suffering of",
		"Require the Secretary of the Treasury to mint coins in commemoration of",
		"Award Congressional Gold Medals to",
		"Increase the rates of compensation for",
    };

	List<string> negative_verbs = new List<string>() {
        "Deport",
        "Increase taxes on",
        "Require legal registration of all",
		"Re-educate",
		"Freeze all assets of",
		"War on",
		"Ban all",
		"Increase Regulations on",
		"Declare national emergency on",
		"Eliminate public funding of",
		"Authorize the Marshal of the Supreme Court and the Supreme Court Police to protect the",
		"Amend the Balanced Budget and Emergency Deficit Control Act of",
		"Make emergency supplemental appropriations for",
		"Direct the Comptroller General of the United States to conduct an assessment of the responsibilities, workload, and vacancy rates of",
		"Require the Director of the Office of Management and Budget to issue guidance on", 
		"Create new ISO Standards for",
    };

	HashSet<string> alreadyDrawnBills = new HashSet<string>();

	long maxBillsPosibilitiesNumber = Int64.MaxValue;

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
			{"Electric Vehicle Owners", null},
			{"Scientologist", null},
			{"Minors", null},
			{"Anti-Vaxxers", null},
			{"Weather enthusiasts", null},
			{"Glasses owners", null},
			{"Gun owners", null},
			{"Retired people", null},
			{"Poor people", null},
			{"Victims of Facebook", null},
			{"Terrorists", null},
			{"Parents", null},
			{"Patriots", null},
			{"Commuters", null},
			{"First-Cousins", null},
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
		var coin_flip = (((int) (UnityEngine.Random.Range(0f, 100f))) % 2) == 0;
		foreach(var topic in keys) {
			var number1 = UnityEngine.Random.Range(0f, 1f);
			var number2 = UnityEngine.Random.Range(0f, 1f - number1);
			var pos = coin_flip ? number1 : number2;
			var neg = !coin_flip ? number1 : number2;
			var neutral = 1f - pos - neg;
			population_opinions[topic] = new Tuple<float, float, float>(pos, neg, neutral);
		}
		maxBillsPosibilitiesNumber = negative_verbs.Count * population_opinions.Keys.Count * negative_verbs.Count * population_opinions.Keys.Count;
		Debug.Log($"Max number of bills that can be generated: {maxBillsPosibilitiesNumber}");
		Debug.Log("Population has been initialized to:");
		DisplayPop();
	}

    private string randchoice(List<string> list) {
		var count = list.Count;
		var index = UnityEngine.Random.Range(0, count);
		return list[index];
	}

	// Returns:
	// Tuple<actual_text, negative_group_id, positive_group_id>
	public Tuple<string, string, string> GetNewBill() {
		if (alreadyDrawnBills.Count >= maxBillsPosibilitiesNumber * 0.75f)
		{
			Debug.LogWarning("We have actually drawn all/most possible bills!! Resetting the drawn bills");
			alreadyDrawnBills.Clear();
		}
		string g_pos = "";
		string g_neg = "";
		string actual_text = "";
		var groups = new List<string>();
		groups.AddRange(population_opinions.Keys);
		int attempts = 0;
		do
		{
			var pos = randchoice(positive_verbs);
			var neg = randchoice(negative_verbs);
			// For faster lookup
			g_neg = randchoice(groups);
			g_pos = g_neg;
			while (g_neg == g_pos) {
				g_pos = randchoice(groups);
			}
			actual_text = $"{neg} {g_neg} to {pos} {g_pos}";
			if (attempts++ > 1e5)
			{
				Debug.LogWarning("Tried too many times to find a new bill, giving up not to hang the game.");
				Debug.LogWarning($"Unique drawn bills: {alreadyDrawnBills.Count} / max possible: {maxBillsPosibilitiesNumber}");
				break; // Make sure not to freeze the game even if you are really unlucky in your random drawings
			}	
		} while (alreadyDrawnBills.Contains(actual_text));
		alreadyDrawnBills.Add(actual_text);
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

	public void GetBill()
	{
		GameState.Instance.CurrentBill = Backend.Instance.GetNewBill();
	}

	public ProtesterManager protesterManager;

	private void updateAvgPopularity(int popularity)
	{
		GameState.Instance.averagePopularity = (
			GameState.Instance.averagePopularity * GameState.Instance.daysPlayed
		 	+ popularity
		) / (GameState.Instance.daysPlayed + 1);
		if (GameState.Instance.daysPlayed > GameManager.DAYS_OF_IMMUNITY
			&& GameState.Instance.averagePopularity < GameManager.POPULARITY_AVG_LOSING_THRESHOLD)
		{
			GameManager.Instance.Lose(true);
		}
	}

	public void ApproveBill() 
	{
		Debug.Log("Backend::ApproveBill()");
		
		var popularity = (int) (Backend.Instance.GetBillPopularity(
					GameState.Instance.CurrentBill.Item2,
					GameState.Instance.CurrentBill.Item3
				) * 100);
		updateAvgPopularity(popularity);
		TVController.Instance.DisplayPopularity(popularity);
		protesterManager?.SetProtesterAmount(100 - popularity); 
	}

	public void DeclineBill() 
	{
		var popularity = (int) (Backend.Instance.GetBillPopularity(
					GameState.Instance.CurrentBill.Item2,
					GameState.Instance.CurrentBill.Item3
				) * 100);

		updateAvgPopularity(popularity);
		TVController.Instance.DisplayPopularity(100 - popularity);
		protesterManager?.SetProtesterAmount(popularity); 

	}
}