using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Andrew Montoya

namespace SSTF_disc_scheduling
{
    struct Val {
        public bool isUsed;
        public int value;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input head positions (separated by comma):");

            int input = 0;
            int number = 0;
            List<Val> SSTF = new List<Val>();

            do
            {
                input = Console.Read();
                if (input == (int)(',') || input == 10)
                {
                    Val val = new Val() { isUsed = false, value = number };
                    SSTF.Add(val);
                    number = 0;
                }


                if (input > 47 && input < 58)
                {
                    number *= 10;
                    number += input - 48;
                }

            } while (input != 10);

            Console.WriteLine("Please input starting head position:");

            int startValue = Convert.ToInt32(Console.ReadLine());
            int finalValue = 0;
            int length = SSTF.Count();
            bool isPos = true;

            //sort list
            SSTF.Add(new Val { isUsed = false, value = startValue});
            SSTF.Sort((s1, s2) => s1.value.CompareTo(s2.value));
            int anchor = SSTF.FindIndex((s1) => s1.value == startValue);





            // Algorithm for determining FFCS
            for (int i = anchor, previous = 1, next = 1; previous != 0 && next != 0;)
            {
                int current = SSTF[i].value;
                next = GetNext(SSTF, i);
                previous = GetPrevious(SSTF, i);
                if (previous < next)
                {
                    isPos = false;
                }
                else
                {
                    isPos = true;
                }

                if (next == 0) { isPos = false; }
                if (previous == 0) { isPos = true; }
                Console.WriteLine("IsPos: " + isPos);

                Console.WriteLine("ANCHOR IS: " + i);
                int gap = ChangeInValue(SSTF, i, isPos);
                // calculate based on input and cancel values accordingly:
                // TODO: i++

                if (isPos)
                {
                    Console.WriteLine("ORIGINAL: " + SSTF[i].value);
                    Console.WriteLine("GAPPED: " + SSTF[i + gap].value);
                    Console.WriteLine("ABS_______ = " + Math.Abs(SSTF[i].value - SSTF[i + gap].value));
                    finalValue += Math.Abs(SSTF[i].value - SSTF[i + gap].value);
                    anchor = SSTF[i + gap].value;
                    i += gap;
                }
                else
                {
                    Console.WriteLine("ORIGINAL: " + SSTF[i].value);
                    Console.WriteLine("GAPPED: " + SSTF[i - gap].value);
                    Console.WriteLine("ABS_______ = " + Math.Abs(SSTF[i].value - SSTF[i - gap].value));
                    finalValue += Math.Abs(SSTF[i].value - SSTF[i - gap].value);
                    anchor = SSTF[i - gap].value;
                    i -= gap;
                }

            }
                Console.WriteLine("Total Head Movements: " + finalValue);
        }

        private static int GetNext(List<Val> sstf, int anchor)
        {
            int current = sstf[anchor].value;
            int next = 0;
            for (int j = anchor + 1; j < sstf.Count() - 1; j++)
            {
                if (sstf[j].isUsed == false)
                {
                    next = Math.Abs(sstf[j].value - current);
                    break;
                }
            }
            Console.WriteLine("Next: " + next);
            return next;
        }


        private static int GetPrevious(List<Val> sstf, int anchor)
        {
            int current = sstf[anchor].value;
            int previous = 0;
            for (int j = anchor - 1; j > 0 - 1; j--)
            {
                if (sstf[j].isUsed == false)
                {
                    previous = Math.Abs(sstf[j].value - current);
                    break;
                }
            }
            Console.WriteLine("Previous: " + previous);
            return previous;
        }


        private static int ChangeInValue(List<Val> sstf, int anchor, bool isPos)
        {
            Console.WriteLine("Running CIV");
            int gap = 0;
            bool changed = false;
            if (sstf.Count() - 1 == anchor + 1)
            {
                return gap;
            }
            // TODO: i++ is removed. Now, change how the loop handles getting the new anchor point
            int i = anchor;
            for (; i < sstf.Count() - 1 && !changed;)
            {
                Console.WriteLine("ANCHOR: " + i);
                int current = sstf[i].value;
                int next = GetNext(sstf, i);


                if (next == 0 && isPos)
                {
                    Console.WriteLine("RETURNING special next == 0: " + gap);
                    return gap;
                }
                int previous = GetPrevious(sstf, i);
                if (previous == 0 && !isPos)
                {
                    Console.WriteLine("RETURNING special  previous == 0: " + gap);
                    return gap;
                }

                sstf[i] = new Val { value = sstf[i].value, isUsed = true };



                // handle returns
                if ((next < previous && isPos) || (previous == 0 && isPos))
                {
                    Console.WriteLine("1");
                    gap++;
                    sstf[i + 1] = new Val { value = sstf[i + 1].value, isUsed = true };
                    // change of value i++
                    for (int k = i; k < sstf.Count(); k++)
                    {
                        if (sstf[k + 1].isUsed == true) { i++;}
                        else { break; }
                    }
                    continue;
                }

                if (next > previous && !isPos)
                {
                    Console.WriteLine("2");
                    gap++;
                    sstf[i - 1] = new Val { value = sstf[i - 1].value, isUsed = true };
                    // change value of i--
                    for (int k = i; k > 0; k--)
                    {
                        if (sstf[k].isUsed == true) { i--; }
                        else { break; }
                    }
                    continue;
                }
                if ((next < previous && !isPos) || (next == 0 && !isPos))
                {
                    Console.WriteLine("3");
                    changed = true;
                    continue;
                }
                if (next > previous && isPos)
                {
                    Console.WriteLine("4");
                    changed = true;
                    continue;
                }

            }

            Console.WriteLine("RETURNING: " + gap);
            return gap + ValueOffset(isPos, anchor, i);

        }
    }
}
