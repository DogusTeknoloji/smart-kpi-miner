using DogusTeknoloji.SmartKPIMiner.Helpers;
using System.Diagnostics;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public class IntiutiveSiteMatcher
    {
        public string ExtractAppName(string fullDomain)
        {
            char separator = '.';
            string[] partsOfDomain = fullDomain.Split(separator);
            string appNamePart = string.Empty;
            foreach (string part in partsOfDomain)
            {
                if (part.IsPartExcluded()) { continue; }
                appNamePart = string.Join(separator.ToString(), part);
            }
            return appNamePart;
        }
        public void MatchOperation(string domain)
        {
            string appDomainBody = this.ExtractAppName(domain);
            double similarityPercentage = appDomainBody.CheckSimilarity();

            if (similarityPercentage > 70)
            {
                appDomainBody.AddAsPotentialAppName();
            }
            else
            {
                Debug.WriteLine($"This app: {appDomainBody} hasn't mark as potential similar app. Similarity Percentage {similarityPercentage}");
            }
        }
    }
}
