using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace UserAndNoteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class analyzerController : ControllerBase
    {

        /// <summary>
        /// Method for reverse A Num
        /// </summary>
        /// <param name="Number"></param>
        [HttpPost]
        [Route("reverseNum")]
        public async Task<JsonResult> AdminMessage([FromBody] uint Number)
        {
            string str = Number.ToString();
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            str = new string(charArray);
            uint reversed = uint.Parse(str);

            return new JsonResult(reversed);
        }

        /// <summary>
        /// Method for find Substring
        /// </summary>
        /// <param name="str"></param>
        [HttpPost]
        [Route("findSubstring")]
        public async Task<JsonResult> findSubstring([FromBody] string str)
        {
            Dictionary<string, int> substrings = new Dictionary<string, int>();
            string mostFrequent = "";
            int maxCount = 0;

            for (int i = 0; i < str.Length; i++)
            {
                HashSet<char> uniqueChars = new HashSet<char>();
                for (int j = i; j < str.Length; j++)
                {
                    if (!uniqueChars.Add(str[j]))
                    {
                        break;
                    }

                    string substring = str.Substring(i, j - i + 1);
                    if (substrings.ContainsKey(substring))
                    {
                        substrings[substring]++;
                    }
                    else
                    {
                        substrings[substring] = 1;
                    }

                    if (substrings[substring] > maxCount)
                    {
                        maxCount = substrings[substring];
                        mostFrequent = substring;
                    }
                }
            }

            return new JsonResult(mostFrequent);
        }

        /// <summary>
        /// Method for find Missing Of Smallest Nature Number
        /// </summary>
        /// <param name="str"></param>
        [HttpPost]
        [Route("findMissing")]
        public async Task<JsonResult> findMissing([FromBody] List<int> numbers)
        {
            var numberSet = new HashSet<int>(numbers);
            int i = 1;

            while (true)
            {
                if (!numberSet.Contains(i))
                {
                    return new JsonResult(i);
                }
                i++;
            }
        }
    }
}
