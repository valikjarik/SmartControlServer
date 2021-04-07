using Microsoft.AspNetCore.Mvc;

using SmartControlServer.Models;

using System.Linq;

namespace SmartControlServer.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        // GET: api/<SensorController>
        [HttpGet]
        public IActionResult GetSensors()
        {
            return Ok(Data.Sensors);
        }

        [HttpGet]
        public IActionResult GetRules()
        {
            return Ok(Data.Rules);
        }

        [HttpGet("{id}")]
        public IActionResult GetState(string id)
        {
            var sensor = Data.Sensors.FirstOrDefault(s => s.Id == id);
            if (sensor is null)
            {
                return BadRequest("Sensor not found");
            }
            return Ok(sensor.State);
        }

        [HttpGet("{id}")]
        public IActionResult GetRuleState(string id)
        {
            var rule = Data.Rules.FirstOrDefault(s => s.Id == id);

            if (rule is null) return BadRequest("Sensor not found");

            var result = true;

            foreach (var item in rule.Sensors)
            {
                var sensor = Data.Sensors.FirstOrDefault(s => s.Id == item.id);

                if (sensor is null) continue;

                result = result && (sensor.State == item.value);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddSensor([FromQuery] string id)
        {
            var sensor = Data.Sensors.SingleOrDefault(s => s.Id == id);
            if (sensor is null)
            {
                Data.Sensors.Add(new Sensor { Id = id, State = false });
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddRule([FromQuery] string id)
        {
            var rule = Data.Rules.SingleOrDefault(r => r.Id == id);
            if (rule is null)
            {
                Data.Rules.Add(new Rule(id));
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddSensorToRules([FromQuery] string ruleId, [FromQuery] string sensorId, [FromQuery] bool value)
        {
            var sensor = Data.Sensors.FirstOrDefault(s => s.Id == sensorId);

            if (sensor is null) return BadRequest("Sensor not found");

            var rule = Data.Rules.FirstOrDefault(r => r.Id == ruleId);

            if (rule is null) return BadRequest("Rule not found");

            var ruleSensor = rule.Sensors.FirstOrDefault(s => s.id == sensorId);

            if (!(ruleSensor is (null, false))) ruleSensor.value = value;
            else rule.Sensors.Add((sensorId, value));

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult SetState(string id, [FromQuery] bool value)
        {
            var sensor = Data.Sensors.FirstOrDefault(s => s.Id == id);
            if (sensor is null)
            {
                return BadRequest("Sensor not found");
            }
            sensor.State = value;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensor(string id)
        {
            var sensor = Data.Sensors.FirstOrDefault(s => s.Id == id);
            if (sensor is null)
            {
                return BadRequest("Sensor not found");
            }
            Data.Sensors.Remove(sensor);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensorFromRule(string id, [FromQuery] string sensorId)
        {
            var rule = Data.Rules.FirstOrDefault(r => r.Id == id);

            if (rule is null) return BadRequest("Rule not found");

            var sensor = rule.Sensors.FirstOrDefault(s => s.id == sensorId);

            if (sensor is (null, false)) return BadRequest("Sensor not found");

            rule.Sensors.Remove(sensor);

            return Ok();
        }
    }
}