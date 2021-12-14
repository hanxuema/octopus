using System;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Description;

namespace AWSServerless1.Controllers
{
    [Route("api/Redis")]
    public class RedisController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisController(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        [HttpGet]
        [Route("HealthCheck")]
        public String HealthCheck()
        {
            var db = _redis.GetDatabase();

            var pong = db.Ping();

            return "Pong";
        }

        [HttpGet]
        [Route("Demo")]
        public ActionResult<string> Demo()
        {
            Console.WriteLine("list all keys");
            var endpoints = _redis.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _redis.GetServer(endpoint);
                foreach (var key in server.Keys())
                {
                    Console.WriteLine(key);
                }
            }

            var db = _redis.GetDatabase();

            //string
            var stringKey = "key1";
            db.KeyDelete(stringKey);
            var good = db.StringSet(stringKey, "abc", TimeSpan.FromSeconds(60));
            if (good)
            {
                Console.WriteLine($"setting stringKey to abc");
            }
            var ttl = db.KeyTimeToLive(stringKey);
            Console.WriteLine($"ttl is {ttl}");

            var value = db.StringGet(stringKey);
            Console.WriteLine($"stringKey value is {value}");

            db.StringAppend(stringKey, "xyz");
            Console.WriteLine($"append stringKey is {value}");

            value = db.StringGet(stringKey);
            Console.WriteLine($"stringKey is {value}");

            db.KeyExpire(stringKey, TimeSpan.FromSeconds(1));
            ttl = db.KeyTimeToLive(stringKey);
            Console.WriteLine($"ttl is {ttl}");


            //list
            var listKey = "listkey";
            db.KeyDelete(listKey);

            var leftValues = new RedisValue[] { "value1", "value2" };
            db.ListLeftPush(listKey, leftValues);

            var values = db.ListRange(listKey);

            var rightValues = new RedisValue[] { "value3", "value4" };
            db.ListRightPush(listKey, rightValues);

            //stop here and show redis cli
            var popValue = db.ListLeftPop(listKey);
            Console.WriteLine($"first popValue is {popValue}");
            popValue = db.ListLeftPop(listKey);
            Console.WriteLine($"second popValue is {popValue}");

            popValue = db.ListRightPop(listKey);
            Console.WriteLine($"third popValue is {popValue}");
            popValue = db.ListRightPop(listKey);
            Console.WriteLine($"fourth popValue is {popValue}");

            //set
            var setKey = "setkey";
            db.KeyDelete(setKey);
            db.SetAdd(setKey, new RedisValue[] { "a", "b", "b", "c" });
            var vals = db.SetMembers(setKey);
            string setValue = string.Join(",", vals.OrderByDescending(x => x));

            var contains = db.SetContains(setKey, new RedisValue("a"));
            Console.WriteLine($"a is in {setKey} {contains}");

            contains = db.SetContains(setKey, new RedisValue("x"));
            Console.WriteLine($"x is in {setKey} {contains}");


            //hash
            var hashKey = "user:101";
            db.KeyDelete(hashKey);

            db.HashSet(hashKey, new[] {
                    new HashEntry("age", 20),
                    new HashEntry("name", "Peter"),
                    new HashEntry("mobile", 0411333555)});

            Console.WriteLine($"set user:101 {setKey} {contains}");
            var hashVals = db.HashGetAll(hashKey);

            var stringBuilder = new StringBuilder();
            foreach (var val in hashVals)
            {
                stringBuilder.Append($"[field {val.Name} - value {val.Value}] ");
            }

            Console.WriteLine($"hashVals is {stringBuilder.ToString()}");

            return "AllGood";
        }

        [HttpGet("{idKey}")]
        [ResponseType(typeof(string))]
        public ActionResult<string> Get(string idKey)
        {
            var db = _redis.GetDatabase();
            var value = db.StringGet(idKey);
            if (value.HasValue)
            {
                return value.ToString();
            }
            return $"No value found for {idKey}";
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        public ActionResult<String> Post([FromBody] RedisTest test)
        {
            var db = _redis.GetDatabase();
            var value = db.StringSet(test.key, test.value);
            return value.ToString();
        }

    }

    public class RedisTest
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public static class Extension
    {
        public static HashEntry[] ToHashEntries(this object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            return properties
                .Where(x => x.GetValue(obj) != null)
                .Select
                (
                      property =>
                      {
                          object propertyValue = property.GetValue(obj);
                          string hashValue;

                          if (propertyValue is IEnumerable<object>)
                          {
                              hashValue = JsonConvert.SerializeObject(propertyValue);
                          }
                          else
                          {
                              hashValue = propertyValue.ToString();
                          }

                          return new HashEntry(property.Name, hashValue);
                      }
                )
                .ToArray();
        }
    }
}
