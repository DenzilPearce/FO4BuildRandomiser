using System;
using System.Collections.Generic;

namespace FO4BuildRandomiser
{
    internal class Build
    {
        private static readonly Random rng = new Random();

        public Config config { get; set; }
        public int level { get; set; }
        public int[] baseSPECIALs { get; set; }
        public bool[] bobbleheads { get; set; }
        public int youreSPECIAL { get; set; }
        public List<List<Perk>> perks { get; set; }

        public Build() { }

        public Build(Config config)
        {
            int totalSPECIAL()
            {
                int total = 0;
                foreach (int stat in baseSPECIALs) total += stat;
                return total;
            }

            void modifyRandomStat(int amount)
            {
                int stat = rng.Next(0, 7);
                int newValue = baseSPECIALs[stat] + amount;

                if (newValue >= config.minimumSPECIAL && newValue <= config.maximumSPECIAL)
                {
                    baseSPECIALs[stat] = newValue;
                }
            }

            this.config = config;

            this.level = 1;

            baseSPECIALs = new int[7];
            for (int stat = 0; stat < 7; ++stat)
            {
                baseSPECIALs[stat] = rng.Next(config.minimumSPECIAL, config.maximumSPECIAL + 1);
            }
            while (totalSPECIAL() > 28) modifyRandomStat(-1);
            while (totalSPECIAL() < 28) modifyRandomStat(1);

            bobbleheads = new bool[7];
            for (int i = 0; i < 7; ++i) bobbleheads[i] = false;

            youreSPECIAL = -1;

            perks = Perk.getAllPerks(config.includeFarHarbor, config.includeNukaWorld);
        }

        public int modifiedSPECIAL(SPECIAL stat)
        {
            int value = baseSPECIALs[(int)stat];
            if (bobbleheads[(int)stat]) ++value;
            if (youreSPECIAL == (int)stat) ++value;
            return value;
        }

        public void applyYoureSPECIAL(SPECIAL stat)
        {
            if (youreSPECIAL == -1)
            {
                youreSPECIAL = (int)stat;
            }
        }

        public void applyBobblehead(SPECIAL stat)
        {
            bobbleheads[(int)stat] = true;
        }

        public List<int[]> getAvailablePerks()
        {
            List<int[]> availablePerks = new List<int[]>();

            for (int stat = 0; stat < 7; ++stat)
            {
                for (int perkIdx = 0; perkIdx < modifiedSPECIAL((SPECIAL)stat); ++perkIdx)
                {
                    Perk perk = perks[stat][perkIdx];

                    if (perk.level < perk.requirements.Length)
                    {
                        if (perk.requirements[perk.level + 1 - 1] <= level)
                        {
                            availablePerks.Add(new int[] { stat, perkIdx, perk.level + 1 });
                        }
                    }
                }
            }

            return availablePerks;
        }

        public void levelUp()
        {
            ++level;
        }

        public int[] selectRandomPerk()
        {
            List<int[]> availablePerks = getAvailablePerks();

            // naive solution, creates "scattered" builds
            // (with level 1 perks everywhere and no focus)
            //return availablePerks[rng.Next(availablePerks.Count)];

            // solution to make more "focused" builds instead of "scattered" builds
            // examine N perks, and pick the one that would reach the highest level if taken
            int N = config.bias;

            int[] perk = { -1, -1, -1 };
            for (int i = 0; i < N; ++i)
            {
                int[] examinedPerk = availablePerks[rng.Next(availablePerks.Count)];
                if (examinedPerk[2] > perk[2]) perk = examinedPerk;
            }

            return perk;
        }

        public void takePerk(SPECIAL stat, int perkIdx, int perkLevel)
        {
            perks[(int)stat][perkIdx].level = perkLevel;
        }
    }
}
