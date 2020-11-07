using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
	public TextMesh text;
	void Start()
	{
		string str = "";
		str += "Michael Todd - Development";
		str += "\nAlex Carpenter - Designer of Groove Wizard's Tower DLC";
		str += "\nENV - Lovely, amazing musical tracks";
		str += "\nReptiore - Lovely, amazing musical tracks";
		str += "\nGetSix - Lovely, amazing musical tracks";
		str += "\nSupa slick hot nick - Lovely, amazing musical tracks";
		str += "\nOrie Falconer - Level design";
		str += "\nTim McLennan - Code";
		str += "\nRyan Roth - Audio";
		str += "\nCassie Chui - Design, Art";
		str += "\nDon Nguyen - Biz Dev";
		str += "\nJeanneOskoure - Playtester for Groove Wizard's Tower DLC";
		str += "\nBlueLight218 - Playtester for Groove Wizard's Tower DLC";
		str += "\nThomas Penner - Playtester for Groove Wizard's Tower DLC";

		str += "\n";
		str += "\nCreepinSnake - Producer";
		str += "\nKardo \"Gelatinoid\" Rostam - Producer";
		str += "\nAccela - Producer";
		str += "\nRobert Mock - Producer";
		str += "\nGerrod Allen - Producer";
		str += "\nChristian Brooks - Producer";
		str += "\nChratis - Producer";
		
		str += "\n\nGold Supporters:";
		str += "\nAmanda Norton";
		str += "\nAndre K";
		str += "\ncayiika";
		str += "\nCorinthian Johnson";
		str += "\nJay Ellis-James";
		str += "\nJoshua Cox";
		str += "\nShadoonsky";
		str += "\nWill Carducci";
		str += "\nEyesofNova";
		
		str += "\n\nSilver Supporters:";
		str += "\nAlexander Zacherl";
		str += "\nDoomsday Cola";
		str += "\nEtzer";
		str += "\nFabien Tricoire";
		str += "\nGD Colon";
		str += "\nGreenCloud";
		str += "\nHeikki Kaperi";
		str += "\nJoe Robinson";
		str += "\nMatt Craven";
		str += "\nMint Thompson";
		str += "\nNEETenshi";
		str += "\nRuSyxx";
		str += "\nAdrian Kind";
		str += "\nDillon Mabry";
		str += "\nechfiffle";
		str += "\nEric Holm";
		str += "\nErin Bailey";
		str += "\nJoshua “Lunk” Sheldon";
		str += "\nSupa slick hot nick";
		str += "\nRyan Colaiacovo";
		str += "\nDylan Markham";
		str += "\nThomas Forest";
		str += "\nAdam Ashdown";
		str += "\nAdam Hartling";
		str += "\nDelsin Hiraeth";
		str += "\nAnthony Baker";
		str += "\nFuzzy Nukes";
		str += "\nDocWibbleyWobbley";
		str += "\nEven The Odd Co.";
		str += "\nGabriel Hernandez";
		str += "\nGilberto Aponte";
		str += "\nJepp";
		str += "\nJulien Robin Lapin Lavoie";
		str += "\nLaurent Clamart";
		str += "\nmagictim41";
		str += "\nMasterfireheart";
		str += "\nMassacretout";
		str += "\nAndrew \"Scooter\" Hoover";
		str += "\nScott Therens";
		str += "\nSebastian King";
		str += "\nSteven Potyok";
		str += "\nVixie Viz";
		str += "\nYaland Hong";
		str += "\nYonatan Yevilevich";

		
		str += "\n\nBasic Supporters:";
		str += "\nAlex Carpenter";
		str += "\nAlexander Roper";
		str += "\nPl4y3r";
		str += "\nArtimage";
		str += "\nArtūrs Babājevs";
		str += "\nBelkorin";
		str += "\nDaniel Dombrowsky";
		str += "\nF3ather";
		str += "\nFlorent Joannet";
		str += "\nFlorin Stancu";
		str += "\nJohan 'The Lunchbox' Vennerström";
		str += "\nAnari";
		str += "\nLuke";
		str += "\nMarco Antonio Gonzálvez Cutillas";
		str += "\nMarshall Rodrigue";
		str += "\nMathieu Dauré - 3l3ktr0";
		str += "\nArtyom Havok";
		str += "\nMx. Junie";
		str += "\nNiklas Jacobsen";
		str += "\nPhillip Shipe";
		str += "\nPolecat";
		str += "\nResponsibleReptarCollector";
		str += "\nRyan McQuaid";
		str += "\nScott Walker";
		str += "\nSebastian Suwała";
		str += "\nSeth Riter";
		str += "\nWilliam Djalal";
		str += "\nSylvain Basten";
		str += "\nnmd";
		str += "\nTheZwHD4210";
		str += "\nTill Wübbers";
		str += "\nronezy";
		str += "\nVitelg";
		str += "\nYoan Michaud";
		str += "\nАртём Бибиков";

		str += "\n\nSpecial thanks for being supportive, active & helping in the community:";
		str += "\nGDColon";
		str += "\nYuvira";
		str += "\nMx Junie";
		str += "\nChratis";
		str += "\nNEETenshi";
		str += "\nSupa slick hot nick";

		str += "\n\nSound Effects:";
		str += "\nBreviceps";
		str += "\nBlack Snow";
		str += "\nCorsica_S";
		str += "\nepanody";
		str += "\njoshuaempyre";
		str += "\n11linda";
		str += "\nspeedygonzo";
		str += "\nCGEffex";
		str += "\nCosmicEmbers";
		str += "\nDpoggioli";
		str += "\nJarAxe";
		str += "\nkantouth";
		str += "\nLjudmann";
		str += "\nThemfish";
		str += "\nHerbertBoland";
		str += "\ncylon8472";
		str += "\nwuola";
		str += "\nmenegass";
		str += "\nCGEffex";
		str += "\njunkfood2121";
		str += "\nkrwoox";
		str += "\nKataVlogsYT";
		str += "\nIFartInUrGeneralDirection";
		
		str += "\n\nThanks for the PHP help:";
		str += "\n@meersausteven";
		str += "\n@FuzzyGamesOn";
		
		str += "\n\nThanks for help with Twitch:";
		str += "\nAndrew Peggs - @H2OAcidic";

		str += "\n\nand a very special thanks to:\n\n";
		str += "\"Yuvira is fine. But just the \"Yuvira\" part, not the \"is fine.\"\n";
		str += "and I swear if you use this entire message in the credits you will be\n";
		str += "receiving some strongly worded Discord DMs.\"";
		
		str += "\n\n\nThanks for playing folks! :)";


		text.text = str;
	}

}
