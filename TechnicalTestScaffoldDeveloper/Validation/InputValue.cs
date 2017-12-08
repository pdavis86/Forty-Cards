namespace TechnicalTestScaffoldDeveloper.Validation
{
    public static class InputValue
    {
        public static Models.IntValidationResult IsInt(string inputStr)
        {
            int inputInt;
            if(!int.TryParse(inputStr, out inputInt))
            {
                return new Models.IntValidationResult()
                {
                    IsValid = false,
                    FailureReason = $"'{inputStr}' is not an number in the integer range"
                };
            }

            return new Models.IntValidationResult()
            {
                IsValid = true,
                Result = inputInt
            };
        }

        public static Models.ValidationResult IsIntInRange(int inputInt, int lowerBound, int upperBound)
        {
            if(inputInt < lowerBound)
            {
                return new Models.ValidationResult()
                {
                    IsValid = false,
                    FailureReason = $"{inputInt} is less than than {lowerBound}"
                };
            }

            if(inputInt > upperBound)
            {
                return new Models.ValidationResult()
                {
                    IsValid = false,
                    FailureReason = $"{inputInt} is greater than {upperBound}"
                };
            }

            return new Models.ValidationResult()
            {
                IsValid = true
            };
        }


    }
}
