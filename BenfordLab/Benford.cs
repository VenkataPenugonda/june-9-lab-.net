using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BenfordLab
{
    public class BenfordData
    {
        public int Digit { get; set; }
        public int Count { get; set; }

        public BenfordData(int Digit, int Count) 
        {
            this.Digit = Digit;
            this.Count = Count;
        }
    }

    public class Benford
    {
       
        public static BenfordData[] calculateBenford(string csvFilePath)
        {
            // load the data
            var data = File.ReadAllLines(csvFilePath)
                .Skip(1) // For header
                .Select(s => Regex.Match(s, @"^(.*?),(.*?)$"))
                .Select(data => new
                {
                    Country = data.Groups[1].Value,
                    Population = int.Parse(data.Groups[2].Value)
                });

            // manipulate the data!
            //
            // Select() with:
            //   - Country
            //   - Digit (using: FirstDigit.getFirstDigit() )
            // 
            // Then:
            //   - you need to count how many of *each digit* there are
            //
            // Lastly:
            //   - transform (select) the data so that you have a list of
            //     BenfordData objects
            //
            //var m = ??? ;
            var benfordDataList = (from row in data
                                   group row.Country by FirstDigit.getFirstDigit(row.Population) into grp
                                   orderby grp.Key ascending
                                   select new BenfordData(grp.Key, grp.Count())).ToArray();
           
            return benfordDataList;
        }
    }
}
