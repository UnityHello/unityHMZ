using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System.Collections.Generic;
public class Card  {

	public int ID{ get; set;}
	public int Level{ get; set;}
	public string Name{ get; set;}
	public int Type{ get; set;}
	public string Des{ get; set;}
	public bool IsEquip{ get; set;}
	public int Color{ get; set;}

	public override string ToString ()
	{
		return string.Format ("[Card: ID={0}, Level={1}, Name={2}, Type={3}, Des={4}, IsEquip={5}, Color={6}]", ID, Level, Name, Type, Des, IsEquip, Color);
	}

}
	

public class CardJson:MonoBehaviour{
	public List<Card> cardList = new List<Card> ();
	void Start(){
		ReadParse ();
	}
	void ReadParse()
	{
		TextAsset ass = Resources.Load ("AllCard") as TextAsset; 
		JsonData data = JsonMapper.ToObject (ass.text);

		JsonData jd = data ["cardList"];
		for (int i = 0; i < jd.Count; i++) {
			int id = (int)jd [i] ["id"];
			int level = (int)jd [i] ["level"];
			string name = (string)jd [i] ["name"];


			Card card = new Card ();
			card.ID = id;
			card.Level = level;
			card.Name = name;

			cardList.Add (card);
		
		}
		print (cardList);

	}
		
	
}

