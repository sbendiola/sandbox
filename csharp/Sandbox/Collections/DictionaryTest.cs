using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using System.Linq;

namespace Sandbox.Collections
{
    [TestFixture]
    public class DictionaryTest
    {
        [Test]
        public void IsFailFast()
        {
            var strings = new Dictionary<string, string>();
            strings["1"] = "1";
            strings["2"] = "2";
            strings["3"] = "3";
            var resetEvent = new ManualResetEvent(false);
            var modified = new ManualResetEvent(false);
            var done = new ManualResetEvent(false);
            Exception caught = null;
            Exception caught2 = null;
            var t = new Thread(() =>
                                   {
                                       try
                                       {
                                           var enumerator = strings.Values.GetEnumerator();
                                           enumerator.MoveNext();
                                           var current = enumerator.Current;
                                           resetEvent.Set();
                                           Assert.IsTrue(modified.WaitOne(1000));

                                           while (enumerator.MoveNext()) 
                                           {
                                               current = enumerator.Current;
                                           }
                                       } catch(Exception e)
                                       {
                                           caught = e;
                                       } finally
                                       {
                                           done.Set();
                                       }
                                       
                                   });
            t.Start();
            var t2 = new Thread(() =>
                                    {
                                        try
                                        {
                                            resetEvent.WaitOne(1000);
                                            strings["4"] = "4";
                                            modified.Set();    
                                        } catch(Exception e)
                                        {
                                            caught2 = e;
                                        }
                                        
                                    });

            t2.Start();

            done.WaitOne(2000);
            Assert.IsNull(caught);
            Assert.IsNull(caught2);

            Reseet(new AbsCell<Int64>(33));
        }

        [Test]
        public void ChangingAllDictionaryValues()
        {
            var d = new Dictionary<string, bool>
                        {
                            {"foo", false},
                            {"bar", true}
                        };

            var dictionary = d.Keys.ToDictionary(k => k, v => false);
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual(false, dictionary["foo"]);
            Assert.AreEqual(false, dictionary["bar"]);
        }

      

        private void Reseet<T>(AbsCell<T> cell)
        {
            cell.Value = cell.Initial;
        }

      
    }

    class AbsCell<T>
    {
        private readonly T initial;
        private T value;

        public AbsCell(T init)
        {
            this.initial = init;
        }

        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public T Initial
        {
            get { return initial; }
        }
    }
}
