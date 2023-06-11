namespace Edleron.Utils
{
    public static class Utils
    {
        // Zone Number Text Generate
        private static int[] GenerateRandomNumbers(int length)
        {
            int[] numbers = new int[length];
            System.Random random = new System.Random();

            for (int i = 0; i < length; i++)
            {
                int randomNumber;

                do
                {
                    randomNumber = random.Next(1, 12);
                } while (ArrayContains(numbers, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        private static bool ArrayContains(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return true;
                }
            }

            return false;
        }

    }
}