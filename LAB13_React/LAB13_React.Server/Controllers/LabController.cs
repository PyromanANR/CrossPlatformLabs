using Microsoft.AspNetCore.Mvc;
using LAB13_React.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;


namespace LAB13_React.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public LabController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("lab1")]
        public IActionResult GetLab1()
        {
            var model = new LabViewModel
            {
                TaskNumber = "1",
                TaskVariant = "2",
                TaskDescription = "Якось, прийшовши додому зі школи, Світлана виявила записку від мами, в якій вона просила зробити салат. " +
                "Світлана знала, що салат - це суміш двох або більше інгредієнтів, тому їй не склало труднощів виконати мамине прохання.\r\nАле Світлана хоче стати математиком, тому, для тренування, вирішила порахувати, скільки різних салатів вона зможе зробити з наявних продуктів (майонез, огірки, помідори). " +
                "Після невеликих розрахунків отримала відповідь: 4.\r\nЗнаючи, що ви любите цікаві завдання, і хочете стати програмістами, Світлана попросила вас написати програму, яка визначає кількість різних салатів для довільної кількості інгредієнтів.\r\n",
                InputDescription = "Вхідний файл INPUT.TXT містить натуральне число N – кількість наявних інгредієнтів (N < 32).",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть кількість різних салатів.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "3", Output = "4" },
                new TestCase { Input = "4", Output = "11" }
            }
            };

            return Ok(model);
        }

        [HttpGet("lab2")]
        public IActionResult GetLab2()
        {
            var model = new LabViewModel
            {
                TaskNumber = "2",
                TaskVariant = "2",
                TaskDescription = "Для проведення експерименту треба вибрати N наявних приладів тільки три. " +
                 "Для цього виконують наступну операцію - якщо в групі приладів більше трьох, то їх нумерують і вибирають одну з груп: з парними чи непарними номерами. " +
                 "Операцію повторюють до тих пір, поки в групі не залишиться три або менше приладів. Якщо їх залишається рівно три, то вони беруться для експерименту.\r\nПотрібно написати програму, яка підрахує кількість способів вибору приладів.\r\n",
                InputDescription = "У єдиному рядку вхідного файлу INPUT.TXT записано число N (1 ≤ N ≤ 2147483647).",
                OutputDescription = "У єдиний рядок вихідного файлу OUTPUT.TXT потрібно вивести одне число - знайдену кількість способів вибору приладів.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "3", Output = "1" },
                new TestCase { Input = "6", Output = "2" }
            }
            };

            return Ok(model);
        }

        [HttpGet("lab3")]
        public IActionResult GetLab3()
        {
            var model = new LabViewModel
            {
                TaskNumber = "3",
                TaskVariant = "2",
                TaskDescription = "У неорієнтованому графі потрібно знайти довжину найкоротшого шляху між двома вершинами",
                InputDescription = "У вхідному файлі INPUT.TXT записано спочатку число N - кількість вершин у графі (1 ≤ N ≤ 100)",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть довжину найкоротшого шляху. Якщо шляху немає, виведіть -1.",
                TestCases = new List<TestCase>
            {
                new TestCase
                {
                    Input = "5\n0 1 0 0 1\n1 0 1 0 0\n0 1 0 0 0\n0 0 0 0 0\n1 0 0 0 0\n3 5",
                    Output = "3"
                }
            }
            };

            return Ok(model);
        }

        [HttpPost("processLab")]
        public async Task<IActionResult> ProcessLab([FromForm] int labNumber, IFormFile inputFile)
        {
            if (inputFile == null || inputFile.Length == 0)
                return BadRequest("Please upload a file");

            // Read file contents into a string array
            string[] lines;
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                lines = fileContent.Split(Environment.NewLine); // Split into lines
            }

            // Variable to store the processed result
            string output;

            // Execute the lab processing method based on lab number
            switch (labNumber)
            {
                case 1:
                    output = LAB1.Program.ProcessLines(lines);
                    break;
                case 2:
                    output = LAB2.Program.ProcessLines(lines);
                    break;
                case 3:
                    output = LAB3.Program.ProcessGraph(lines);
                    break;
                default:
                    return BadRequest("Invalid lab number");
            }

            // Return result as JSON
            var result = new { output = output };
            return Ok(new { output = output });
        }
    }
}
