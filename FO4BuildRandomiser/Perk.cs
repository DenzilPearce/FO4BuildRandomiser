using System.Collections.Generic;
using System.Linq;

namespace FO4BuildRandomiser
{
    internal class Perk
    {
        public string name { get; set; }
        public int level { get; set; }
        public int[] requirements { get; set; }

        public Perk() { }

        public Perk(string name, int level, int[] requirements)
        {
            this.name = name;
            this.level = level;
            this.requirements = requirements;
        }

        public Perk(string name, int[] requirements) : this(name, 0, requirements) { }

        static public List<List<Perk>> getAllPerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<List<Perk>> perks = new List<List<Perk>>();

            perks.Add(getStrengthPerks(includeFarHarbor, includeNukaWorld));
            perks.Add(getPerceptionPerks(includeFarHarbor, includeNukaWorld));
            perks.Add(getEndurancePerks(includeFarHarbor, includeNukaWorld));
            perks.Add(getCharismaPerks(includeFarHarbor, includeNukaWorld));
            perks.Add(getIntelligencePerks(includeFarHarbor, includeNukaWorld));
            perks.Add(getAgilityPerks(includeFarHarbor, includeNukaWorld));
            perks.Add(getLuckPerks(includeFarHarbor, includeNukaWorld));

            return perks;
        }

        static private List<Perk> getStrengthPerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<Perk> perks = new List<Perk>();
            Perk perk;

            perk = new Perk(
                "Iron Fist",
                new int[] { 1, 9, 18, 31, 46 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Big Leagues",
                new int[] { 1, 7, 15, 27, 42 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Armorer",
                new int[] { 1, 13, 25, 39 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Blacksmith",
                new int[] { 1, 16, 29 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Heavy Gunner",
                new int[] { 1, 11, 21, 35, 47 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Strong Back",
                new int[] { 1, 10, 20, 30 }
            );
            if (includeFarHarbor) perk.requirements = perk.requirements.Append(40).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Steady Aim",
                new int[] { 1, 28 }
            );
            if (includeNukaWorld) perk.requirements = perk.requirements.Append(49).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Basher",
                new int[] { 1, 5, 14, 26 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Rooted",
                new int[] { 1, 22, 43 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Pain Train",
                new int[] { 1, 24, 50 }
            );
            perks.Add(perk);

            return perks;
        }

        static private List<Perk> getPerceptionPerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<Perk> perks = new List<Perk>();
            Perk perk;

            perk = new Perk(
                "Pickpocket",
                new int[] { 1, 6, 17, 30 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Rifleman",
                new int[] { 1, 9, 18, 31, 46 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Awareness",
                new int[] { 1 }
            );
            if (includeNukaWorld) perk.requirements = perk.requirements.Append(14).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Locksmith",
                new int[] { 1, 7, 18, 41 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Demolition Expert",
                new int[] { 1, 10, 22, 34 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Night Person",
                new int[] { 1, 25 }
            );
            if (includeFarHarbor) perk.requirements = perk.requirements.Append(37).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Refractor",
                new int[] { 1, 11, 21, 35, 42 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Sniper",
                new int[] { 1, 13, 26 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Penetrator",
                new int[] { 1, 28 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Concentrated Fire",
                new int[] { 1, 26, 50 }
            );
            perks.Add(perk);

            return perks;
        }

        static private List<Perk> getEndurancePerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<Perk> perks = new List<Perk>();
            Perk perk;

            perk = new Perk(
                "Toughness",
                new int[] { 1, 9, 18, 31, 46 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Lead Belly",
                new int[] { 1, 6, 17 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Lifegiver",
                new int[] { 1, 8, 20 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Chem Resistant",
                new int[] { 1, 22 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Aquaboy / Aquagirl",
                new int[] { 1, 21 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Rad Resistant",
                new int[] { 1, 13, 26 }
            );
            if (includeFarHarbor) perk.requirements = perk.requirements.Append(35).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Adamantium Skeleton",
                new int[] { 1, 13, 26 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Cannibal",
                new int[] { 1, 19, 38 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Ghoulish",
                new int[] { 1, 24, 38 }
            );
            if (includeNukaWorld) perk.requirements = perk.requirements.Append(50).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Solar Powered",
                new int[] { 1, 27, 50 }
            );
            perks.Add(perk);

            return perks;
        }

        static private List<Perk> getCharismaPerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<Perk> perks = new List<Perk>();
            Perk perk;

            perk = new Perk(
                "Cap Collector",
                new int[] { 1, 20, 41 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Lady Killer / Black Widow",
                new int[] { 1, 7, 22 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Lone Wanderer",
                new int[] { 1, 17, 40 }
            );
            if (includeFarHarbor) perk.requirements = perk.requirements.Append(50).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Attack Dog",
                new int[] { 1, 9, 25 }
            );
            if (includeNukaWorld) perk.requirements = perk.requirements.Append(31).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Animal Friend",
                new int[] { 1, 12, 28 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Local Leader",
                new int[] { 1, 14 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Party Girl / Party Boy",
                new int[] { 1, 15, 37 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Inspirational",
                new int[] { 1, 19, 43 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Wasteland Whisperer",
                new int[] { 1, 21, 49 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Intimidation",
                new int[] { 1, 23, 50 }
            );
            perks.Add(perk);

            return perks;
        }

        static private List<Perk> getIntelligencePerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<Perk> perks = new List<Perk>();
            Perk perk;

            perk = new Perk(
                "V.A.N.S.",
                new int[] { 1 }
            );
            if (includeNukaWorld) perk.requirements = perk.requirements.Append(36).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Medic",
                new int[] { 1, 18, 30, 49 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Gun Nut",
                new int[] { 1, 13, 25, 39 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Hacker",
                new int[] { 1, 9, 21, 35 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Scrapper",
                new int[] { 1, 23 }
            );
            if (includeFarHarbor) perk.requirements = perk.requirements.Append(40).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Science!",
                new int[] { 1, 17, 28, 41 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Chemist",
                new int[] { 1, 16, 32, 45 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Robotics Expert",
                new int[] { 1, 19, 44 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Nuclear Physicist",
                new int[] { 1, 14, 26 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Nerd Rage!",
                new int[] { 1, 31, 50 }
            );
            perks.Add(perk);

            return perks;
        }

        static private List<Perk> getAgilityPerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<Perk> perks = new List<Perk>();
            Perk perk;

            perk = new Perk(
                "Gunslinger",
                new int[] { 1, 7, 15, 27, 42 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Commando",
                new int[] { 1, 11, 21, 35, 49 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Sneak",
                new int[] { 1, 5, 12, 23, 38 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Mister Sandman",
                new int[] { 1, 17, 30 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Action Boy / Action Girl",
                new int[] { 1, 18 }
            );
            if (includeFarHarbor) perk.requirements = perk.requirements.Append(38).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Moving Target",
                new int[] { 1, 24, 44 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Ninja",
                new int[] { 1, 16, 33 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Quick Hands",
                new int[] { 1, 28 }
            );
            if (includeNukaWorld) perk.requirements = perk.requirements.Append(40).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Blitz",
                new int[] { 1, 29 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Gun Fu",
                new int[] { 1, 26, 50 }
            );
            perks.Add(perk);

            return perks;
        }

        static private List<Perk> getLuckPerks(bool includeFarHarbor, bool includeNukaWorld)
        {
            List<Perk> perks = new List<Perk>();
            Perk perk;

            perk = new Perk(
                "Fortune Finder",
                new int[] { 1, 5, 25, 40 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Scrounger",
                new int[] { 1, 7, 24, 37 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Bloody Mess",
                new int[] { 1, 9, 31, 47 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Mysterious Stranger",
                new int[] { 1, 22, 41 }
            );
            if (includeNukaWorld) perk.requirements = perk.requirements.Append(49).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Idiot Savant",
                new int[] { 1, 11, 34 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Better Criticals",
                new int[] { 1, 15, 40 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Critical Banker",
                new int[] { 1, 17, 43 }
            );
            if (includeFarHarbor) perk.requirements = perk.requirements.Append(50).ToArray();
            perks.Add(perk);

            perk = new Perk(
                "Grim Reaper's Sprint",
                new int[] { 1, 19, 46 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Four Leaf Clover",
                new int[] { 1, 13, 32, 48 }
            );
            perks.Add(perk);

            perk = new Perk(
                "Ricochet",
                new int[] { 1, 29, 50 }
            );
            perks.Add(perk);

            return perks;
        }
    }
}
