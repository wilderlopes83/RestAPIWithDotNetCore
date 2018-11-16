using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNetCoreUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
    
        // GET api/calculator/sum/5/5
        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Sum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var op = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(op.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // GET api/calculator/subtractor/5/5
        [HttpGet("subtractor/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Subtractor(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var op = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(op.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // GET api/calculator/multiplication/5/5
        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Multiplication(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var op = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(op.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // GET api/calculator/division/5/5
        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Division(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var op = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(op.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // GET api/calculator/square-root/5
        [HttpGet("square-root/{number}")]
        public ActionResult<string> SquareRoot(string number)
        {
            if (IsNumeric(number))
            {
                var op = Math.Sqrt((double)ConvertToDecimal(number));
                return Ok(op.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // GET api/calculator/mean/5
        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Mean(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var op = (ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber))/2;
                return Ok(op.ToString());
            }

            return BadRequest("Invalid Input");
        }

        private decimal ConvertToDecimal(string number)
        {
            decimal decimalValue;
            if (decimal.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

        private bool IsNumeric(string value)
        {
            decimal number;

            bool isNumber = decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);
            return isNumber;

        }
    }
}
