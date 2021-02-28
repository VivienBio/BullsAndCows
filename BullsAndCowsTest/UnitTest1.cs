using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCowsTest
{
    public class Tests
    {
        //Guess: 0473 bulls: 2  cows: 2


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var totalAnswers = new List<string>();
            List<char> validChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var guessList = new List<GuessObject>();
            guessList.Add(new GuessObject("1234", 4, 0));
            
            foreach(var g in guessList)
            {
                Helper.GetPossibilities(g, ref validChars, ref totalAnswers);
            }
           
            Assert.AreEqual(1, totalAnswers.Count);
            Assert.AreEqual("1234", totalAnswers.Single());
        }


        [Test]
        public void Test2()
        {
            var totalAnswers = new List<string>();
            List<char> validChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var guessList = new List<GuessObject>();
            guessList.Add(new GuessObject("0473", 2, 2));
            guessList.Add(new GuessObject("7403", 0, 4));

            foreach (var g in guessList)
            {
                Helper.GetPossibilities(g, ref validChars, ref totalAnswers);
            }
           
            Assert.AreEqual(1, totalAnswers.Count);
            Assert.AreEqual("0374", totalAnswers.Single());
        }

        [Test]
        public void Test3()
        {
            var totalAnswers = new List<string>();
            List<char> validChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var guessList = new List<GuessObject>();
            guessList.Add(new GuessObject("9073", 2, 0));
            guessList.Add(new GuessObject("1248", 2, 0));
            guessList.Add(new GuessObject("1043", 0, 0));

            foreach (var g in guessList)
            {
                Helper.GetPossibilities(g, ref validChars, ref totalAnswers);
            }

            Assert.AreEqual(1, totalAnswers.Count);
            Assert.AreEqual("9278", totalAnswers.Single());
        }

        [Test]
        public void Test5()
        {
            var totalAnswers = new List<string>();
            List<char> validChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var guessList = new List<GuessObject>();
            guessList.Add(new GuessObject("0123", 1, 0));
            guessList.Add(new GuessObject("4567", 1, 0));
            guessList.Add(new GuessObject("8901", 1, 0));
            guessList.Add(new GuessObject("8522",3, 0));
            guessList.Add(new GuessObject("8525", 3, 0));

            foreach (var g in guessList)
            {
                Helper.GetPossibilities(g, ref validChars, ref totalAnswers);
            }

            if (totalAnswers.Count > 1)
            {
                foreach (var ta in totalAnswers.ToList())
                {
                    if (!Helper.IsValid(ta, guessList,ref validChars))
                    {
                        totalAnswers.Remove(ta);
                    }

                }
            }

            Assert.AreEqual(1, totalAnswers.Count);
            Assert.AreEqual("8528", totalAnswers.Single());
        }

        [Test]
        public void Test6()
        {
            var totalAnswers = new List<string>();
            List<char> validChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
//            4
//0123 1 0
//4567 0 0
//8901 1 0
//1110 3 0
            var guessList = new List<GuessObject>();
            guessList.Add(new GuessObject("0123", 1, 0));
            guessList.Add(new GuessObject("4567", 0, 0));
            guessList.Add(new GuessObject("8901", 1, 0));
            guessList.Add(new GuessObject("1110", 3, 0));
        
            foreach (var g in guessList)
            {
                Helper.GetPossibilities(g, ref validChars, ref totalAnswers);
            }

            if (totalAnswers.Count > 1)
            {
                foreach (var ta in totalAnswers.ToList())
                {
                    if (!Helper.IsValid(ta, guessList, ref validChars))
                    {
                        totalAnswers.Remove(ta);
                    }

                }
            }

            Assert.AreEqual(1, totalAnswers.Count);
            Assert.AreEqual("1111", totalAnswers.Single());
        }



    }
}