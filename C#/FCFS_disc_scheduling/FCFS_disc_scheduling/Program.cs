using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Andrew Montoya

namespace FCFS_disc_scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input head positions (separated by comma):");

            int input = 0;
            int number = 0;
            List<int> FCFS = new List<int>();

            do
            {
                input = Console.Read();
                if (input == (int)(',') || input == 10)
                {
                    FCFS.Add(number);
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
            int length = FCFS.Count();
            int anchor = startValue;
            bool isPos = true;


            // Algorithm for determining FCFS
            for (int i = 0; i < length; i++)
            {
                if (anchor < FCFS[i])
                {
                    isPos = true;
                }
                else
                {
                    isPos = false;
                }
                int gap = ChangeInValue(FCFS, i, isPos);
                finalValue += Math.Abs(anchor - FCFS[gap + i]);
                anchor = FCFS[gap + i];
                i += gap;

            }
            Console.WriteLine("Total Head Movements: " + finalValue);


        }

        private static int ChangeInValue(List<int> fcfs, int anchor, bool isPos)
        {
            int gap = 0;
            bool changed = false;
            if (fcfs.Count() - 1 == anchor + 1)
            {
                return gap;
            }
            for (int i = anchor; i < fcfs.Count() - 1 && !changed; i++)
            {
                if (fcfs[i] < fcfs[i + 1] && isPos)
                {
                    gap++;
                    continue;
                }
                if (fcfs[i] < fcfs[i + 1] && !isPos)
                {
                    changed = true;
                    continue;
                }

                if (fcfs[i] > fcfs[i + 1] && !isPos)
                {
                    gap++;
                    continue;
                }
                if (fcfs[i] > fcfs[i + 1] && isPos)
                {
                    changed = true;
                    continue;
                }

            }
            return gap;

        }
    }
}
