// Solution for 'Snail Sort' - https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1/java

namespace CodeWarsChallenges
{
    using System;
    using System.Collections.Generic;

    class Solution
    {
        private static List<int> GetSnail(int[,] array)
        {
            int numRowsandCols = array.GetLength(0);
            List<int> theSnail = new List<int>();
            List<List<int>> processedDimensions = new List<List<int>>();
            int currRow = 0, currCol = 0; // Start at [0,0]
            theSnail.Add(array[currRow, currCol]);
            string prevDirection = "";
            while (true)
            {
                List<int> currDimensions = new List<int>();
                currDimensions.Add(currRow);
                currDimensions.Add(currCol);
                // If next move right
                if (GetNextMove(prevDirection, currCol, currRow, numRowsandCols, processedDimensions) == "Right")
                {
                    theSnail.Add(array[currRow, currCol + 1]);
                    processedDimensions.Add(currDimensions);
                    currCol++;
                    prevDirection = "Right";
                    continue;
                }
                // If next move down
                if (GetNextMove(prevDirection, currCol, currRow, numRowsandCols, processedDimensions) == "Down")
                {
                    theSnail.Add(array[currRow + 1, currCol]);
                    processedDimensions.Add(currDimensions);
                    currRow++;
                    prevDirection = "Down";
                    continue;
                }
                // If next move up
                if (GetNextMove(prevDirection, currCol, currRow, numRowsandCols, processedDimensions) == "Up")
                {
                    theSnail.Add(array[currRow - 1, currCol]);
                    processedDimensions.Add(currDimensions);
                    currRow--;
                    prevDirection = "Up";
                    continue;
                }
                // If next move left
                if (GetNextMove(prevDirection, currCol, currRow, numRowsandCols, processedDimensions) == "Left")
                {
                    theSnail.Add(array[currRow, currCol - 1]);
                    processedDimensions.Add(currDimensions);
                    currCol--;
                    prevDirection = "Left";
                    continue;
                }
                // If no valid next move
                if (GetNextMove(prevDirection, currCol, currRow, numRowsandCols, processedDimensions) == "No move")
                {
                    return theSnail;
                }
                return theSnail;
            }
        }

        // The snail will continue in its previous direction of travel if possible.
        private static string GetNextMove(string prevDirection, int currCol, int currRow, int numRowsandCols, List<List<int>> processedDimensions)
        {
            // If no previous travel. 
            if (prevDirection == "")
            {
                if (currCol + 1 < numRowsandCols) return "Right";
                return "No move";
            }
            List<int> toCheck = new List<int>();
            
            
            toCheck.Add(currRow + 1);
            toCheck.Add(currCol);
            if (prevDirection == "Down")
            {
                // If previous direction down, can we continue going down? 
                if (currRow + 1 < numRowsandCols && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Down";
                }
                // If previous direction down, and we can't go further down, can we go left?
                toCheck.Clear();
                toCheck.Add(currRow);
                toCheck.Add(currCol - 1);
                if (currCol - 1 > -1 && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Left";
                }
                return "No move";
            }

            if (prevDirection == "Up")
            {
                // If previous direction was up, can we keep going up?
                toCheck.Clear();
                toCheck.Add(currRow - 1);
                toCheck.Add(currCol);
                if (currRow + 1 < numRowsandCols && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Up";
                }
                // If previous direction was up and we can't continue going up, can we go right? 
                toCheck.Clear();
                toCheck.Add(currRow);
                toCheck.Add(currCol + 1);
                if (currCol + 1 < numRowsandCols && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Right";
                }
                return "No move";
            }

            if (prevDirection == "Right")
            {
                // If previous direction was right, can we continue going right? 
                toCheck.Clear();
                toCheck.Add(currRow);
                toCheck.Add(currCol + 1);
                if (currCol + 1 < numRowsandCols && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Right";
                }
                // If previous direction was right, and we can't continue going right, can we go down? 
                toCheck.Clear();
                toCheck.Add(currRow + 1);
                toCheck.Add(currCol);
                if (currRow + 1 < numRowsandCols && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Down";
                }
                return "No move";
            }

            if (prevDirection == "Left")
            {
                // If previous direction left, can we continue going left?
                toCheck.Clear();
                toCheck.Add(currRow);
                toCheck.Add(currCol - 1);
                if (currCol - 1 > -1 && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Left";
                }
                // If previous direction left, can we go up? Only run if further move left not possible.
                toCheck.Clear();
                toCheck.Add(currRow + 1);
                toCheck.Add(currCol);
                if (currRow - 1 > -1 && !HasAlreadyBeenUsed(toCheck, processedDimensions))
                {
                    return "Up";
                }
            }
            return "No move";
        }

        private static bool HasAlreadyBeenUsed(List<int> toCheck, List<List<int>> processedDimensions)
        {
            foreach(var usedDim in processedDimensions)
            {
                if(usedDim[0] == toCheck[0] && usedDim[1] == toCheck[1])
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            int[,] array1 = new int[,]
            {
                {1, 2, 3},
                {8, 9, 4},
                {7, 6, 5 },
            };

            // Expected result: 1 2 3 4 5 6 7 8 9
            Console.WriteLine("Test 1 result is: \n");
            var snail = GetSnail(array1);
            foreach (var item in snail)
            {
                Console.Write(item + " ");
            }

            int[,] array2 = new int[,]
            {
                {1, 2, 3, 4},
                {5, 6, 7, 8},
                {9, 10, 11, 12 },
                {13, 14, 15, 16 }
            };
            // Expected result: 1 2 3 4 8 12 16 15 14 13 9 5 6 7 11 10
            Console.WriteLine("\nTest 2 result is:\n");
            snail = GetSnail(array2);
            foreach (var item in snail)
            {
                Console.Write(item + " ");
            }
        }
    }
}
