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
		"Direct the Homeland Security Council to protect the",
		"Authorize research grants of",
		// "Direct a study by the National Academy of Medicine for",
		"End the epidemic of",
		// "Enlist the Director for Disease Control and Prevention to support activities regarding all",
		"Create programs of temporary assistance for",
		"Promote the suffering of",
		"Mint coins in commemoration of",
		"Award Congressional Gold Medals to",
		"Give $1000 to all"
	};

	List<string> negative_verbs = new List<string>() {
		// "Assimilate",
		"Nuke the",
		"Deport",
		"Increase taxes on",
		"Re-educate",
		"Freeze all assets of",
		"Declare war on",
		"Ban all",
		"Declare national emergency on",
		"Eliminate public funding of",
		"Increase regulations on",
		// "Authorize the Marshal of the Supreme Court to protect the",
		// "Enact a state of emergency of",
		// "Conduct an assessment of",
		"Issue reparations of",
		// "Legalize psychedelics for",
		 "Waterboard all",
	};

	HashSet<string> alreadyDrawnBills = new HashSet<string>();

	long maxBillsPosibilitiesNumber = Int64.MaxValue;

	Dictionary<string, Tuple<float, float, float>> population_opinions =
		new Dictionary<string, Tuple<float, float, float>>() {
			{"Women", null},
			{"Men", null},
			{"Tree Huggers", null},
			{"Kittens", null},
			{"Puppies", null},
			{"Beggers", null},
			{"Foreigners", null},
			{"Environmentalists", null},
			{"Men over 6 feet tall", null},
			{"Millionaires", null},
			{"Yacht owners", null},
			{"Medicare beneficiaries", null},
			{"Football fans", null},
			{"Patriots", null},
			{"Commuters", null},
			{"Parents", null},
			{"Viagra users", null},
			{"Flat Earthers", null},
			{"Electric Vehicle Owners", null},
			{"Mega corporations", null},
			// {"VIM users", null},
			{"Gun owners", null},
			{"Anti-Vaxxers", null},
			{"Weather enthusiasts", null},
			{"Victims of Facebook", null},
			// {"Googlers", null},
			{"Terrorists", null},
			{"Pickup truck drivers", null},
			{"People who shop at Whole Foods", null},
			// {"Streamers", null},
			// {"Basic Bitches", null},
			{"Body Builders", null},
			// {"Multi-Level Marketers", null},
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
		if (popularity >= 80)
		{
			GameState.Instance.veryPopularBills++;
		} else if (popularity <= 20) {
			GameState.Instance.riotBills++;
		}
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