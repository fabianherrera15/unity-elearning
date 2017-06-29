using UnityEngine;
using System;
using System.Collections;
using TinCan;
using TinCan.LRSResponses;

public class tinCanManager : MonoBehaviour {
	public RemoteLRS lrs = null;
	public Agent actor = null;
	public Verb verb = null;
	public Activity activity = null;
	public Statement statement = null;
	public StatementsQuery query = null;
	
	// Use this for initialization
	void Start () {

		//Save Experienced Statement
		lrs = new RemoteLRS(
			"https://cloud.scorm.com/tc/9UMU0X7QTB/sandbox/",
			"y6XVVMljFZo_MyHpPPw",
			"MjVovDDr5ui4-B4zW8g"
			);

		actor = new Agent();
		actor.mbox = "mailto:fabo15x@gmail.com";

		verb = new Verb();
		verb.id = new Uri ("http://adlnet.gov/expapi/verbs/experienced");
		verb.display = new LanguageMap();
		verb.display.Add("en-US", "experienced");
		
		activity = new Activity();
		activity.id = new Uri ("http://game/test");
		
		statement = new Statement();
		statement.actor = actor;
		statement.verb = verb;
		statement.target = activity;
		
		StatementLRSResponse lrsResponse = lrs.SaveStatement(statement);
		if (lrsResponse.success)
		{
			// Updated 'statement' here, now with id
			print("Save statement: " + lrsResponse.content.id);
		}
		else
		{
			print("Failed");
		}

		//Query to Get 10 Statements Since a Specific Time

		query = new StatementsQuery();
		query.since = DateTime.ParseExact("2014-08-29 07:42:10Z", "u", System.Globalization.CultureInfo.InvariantCulture);
		query.limit = 10;
		
		StatementsResultLRSResponse lrsResultResponse = lrs.QueryStatements(query);
		if (lrsResultResponse.success)
		{
			// List of statements available
			print("Count of statements: " + lrsResultResponse.content.statements.Count);
			for (int i = 0; i < lrsResultResponse.content.statements.Count; i++)
			{
				print(lrsResultResponse.content.statements[i].verb.id);
			}
		}
		else
		{
			print("Failed");
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}