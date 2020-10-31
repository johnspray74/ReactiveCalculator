using ProgrammingParadigms;
using System;
using System.Reflection.Metadata;

namespace DomainAbstractions
{
    /// <summary>
    /// Converts any kind of IDataFlow to an IEvent. 
    /// The Generic Type 'T' should be assigned when it is instantiated.
    /// ------------------------------------------------------------------------------------------------------------------
    /// Ports:
    /// 1. IDataFlow<T> input: input data
    /// 2. IEvent eventOutput: output event
    /// </summary>
    /// 


    enum FormatMode { sig, fix, sci, eng }


    public class NumberFormatting : IDataFlow<string>, IDataFlow<int>, IDataFlow<FormatMode>
    {
        // Properties
        public string InstanceName { get; set; } = "Default";

        // Ports
        private IDataFlow<string> output;
        /// <summary>
        /// Converts string number with many digits into string number with limited digits
        /// Does significant digits, fixed digits, scientific, engineering
        /// </summary>
        public NumberFormatting() 
        {
            TestFormatSignificantDigits();
            TestFormatFixedPoint();
            TestFormatScientific();
            TestFormatEngineering();

        }


        // Private fields
        private string input = default;
        private FormatMode formatMode = FormatMode.sig;
        private int digits;


        // IDataFlow<T> implementation -----------------------------------------------------------------
        string IDataFlow<string>.Data
        {
            get => input;
            set
            {
                input = value;
                switch (formatMode)
                {
                    case FormatMode.sig:
                        input = FormatSignificantDigits(input, digits);
                        break;
                    case FormatMode.fix:
                        input = FormatFixedPoint(input, digits);
                        break;
                    case FormatMode.sci:
                        break;
                    case FormatMode.eng:
                        break;
                }
                output.Data = input;
            }
        }

        int IDataFlow<int>.Data { get => digits; set => digits = value; }




        FormatMode IDataFlow<FormatMode>.Data { get; set; }



        /// <summary>
        /// Formats a number (that is in string form) for a specified number of significant digits
        /// Only removes decimal places. Doesn't add trailing zeros. Doesn't change whole digits to zeros.
        /// Rounds the last digit of the result up if the first removed decimal place was 5 or more
        /// Works on scientific notation as well
        /// see unit tests below for examples
        /// </summary>
        /// <param name="input"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        private string FormatSignificantDigits(string input, int digits)
        {

            bool minus = false;
            if (digits<=0) goto done;  // digits=0 means that we keep the original number exactly as it was
            if (input.Length == 0) goto done;
            if (input[0] == '-')
            {
                minus = true;
                input = input.Substring(1, input.Length - 1);
            }
            int pointPosition = input.IndexOf(".");
            if (pointPosition == -1) goto done;  // if there is no decimal point, it is a whole number so we keep all digits regardless
            if (digits < pointPosition) digits = pointPosition; // we will not round whole digits (except to ripple)
            string exponent = "";
            string mantissa = input;
            int exponentPosition = input.IndexOf("E");
            if (exponentPosition != -1)
            {
                mantissa = input.Substring(0, exponentPosition);
                exponent = input.Substring(exponentPosition, input.Length - exponentPosition);
            }
            bool round = false;
            if (mantissa.Length - 1 > digits)  // we will discard at least one decimal place (-1 is for the decimal point)
            {
                if (mantissa[digits + 1] >= '5') round = true;
                // Truncate decimal places (decimal point stays)
                mantissa = mantissa.Substring(0, digits + 1);
                // do the ripple rounding of the mantissa
                // at this point the mantissa may or may not have a decimal point in it
                int i = mantissa.Length;
                while (round)
                {
                    i--;
                    if (i>=0)
                    {
                        char c = mantissa[i];
                        if (c=='.') continue;
                        if (c=='9')
                        {
                            c = '0';
                        }
                        else
                        {
                            c++;
                            round = false;
                        }
                        mantissa = mantissa.Remove(i, 1).Insert(i, c.ToString());
                    }
                    else
                    {
                        mantissa = mantissa.Insert(0, "1");
                        pointPosition = input.IndexOf(".");
                        if (pointPosition!=-1)
                        {
                            mantissa = mantissa.Substring(0, mantissa.Length - 1); // truncate a zero
                        }
                        round = false;
                    }
                }
            }
            pointPosition = mantissa.IndexOf(".");
            if (pointPosition == mantissa.Length-1)
            {
                mantissa = mantissa.Substring(0, mantissa.Length - 1); // remove trailing decimal point
            }

            input = mantissa + exponent;
        done:
            if (minus) input = "-" + input;
            return input;
        }




        private void TestFormatSignificantDigits()
        {
            assertStringEq(FormatSignificantDigits("", 1), "");
            assertStringEq(FormatSignificantDigits("0", 1), "0");
            assertStringEq(FormatSignificantDigits("0.0", 0), "0.0");
            assertStringEq(FormatSignificantDigits("0.0", 1), "0");
            assertStringEq(FormatSignificantDigits("1",1),"1");
            assertStringEq(FormatSignificantDigits("1.", 1), "1");
            assertStringEq(FormatSignificantDigits("1.0", 1), "1");
            assertStringEq(FormatSignificantDigits("123", 0), "123");
            assertStringEq(FormatSignificantDigits("123.", 0), "123.");
            assertStringEq(FormatSignificantDigits("123.0", 0), "123.0");
            assertStringEq(FormatSignificantDigits("123", 2), "123");
            assertStringEq(FormatSignificantDigits("123", 3), "123");
            assertStringEq(FormatSignificantDigits("123.", 3), "123");
            assertStringEq(FormatSignificantDigits("123", 4), "123");
            assertStringEq(FormatSignificantDigits("123.5", 0), "123.5");
            assertStringEq(FormatSignificantDigits("123.5", 1), "124");
            assertStringEq(FormatSignificantDigits("123.5", 2), "124");
            assertStringEq(FormatSignificantDigits("123.5", 3), "124");
            assertStringEq(FormatSignificantDigits("123.5", 4), "123.5");
            assertStringEq(FormatSignificantDigits("123.456", 3), "123");
            assertStringEq(FormatSignificantDigits("123.449", 4), "123.4");
            assertStringEq(FormatSignificantDigits("123.450", 4), "123.5");
            assertStringEq(FormatSignificantDigits("123.456", 5), "123.46");
            assertStringEq(FormatSignificantDigits("123.456", 6), "123.456");
            assertStringEq(FormatSignificantDigits("123.456", 7), "123.456");
            assertStringEq(FormatSignificantDigits("123.495", 5), "123.50");
            assertStringEq(FormatSignificantDigits("129.995", 5), "130.00");
            assertStringEq(FormatSignificantDigits("123.456E+6", 3), "123E+6");
            assertStringEq(FormatSignificantDigits("123.456E+6", 4), "123.5E+6");
            assertStringEq(FormatSignificantDigits("123.456E+6", 6), "123.456E+6");
            assertStringEq(FormatSignificantDigits("123.456E-6", 7), "123.456E-6");
            assertStringEq(FormatSignificantDigits("1.2345678901234567890E-6", 7), "1.234568E-6");
            assertStringEq(FormatSignificantDigits("9.9999", 4), "10.00");
            assertStringEq(FormatSignificantDigits("9.999", 3), "10.0");
            assertStringEq(FormatSignificantDigits("9.99", 2), "10");
            assertStringEq(FormatSignificantDigits("999.99", 5), "999.99");
            assertStringEq(FormatSignificantDigits("999.99", 4), "1000");
            assertStringEq(FormatSignificantDigits("999.99", 3), "1000");
            assertStringEq(FormatSignificantDigits("999.99", 2), "1000");
            // assertStringEq(FormatSignificantDigits("00", 1), "0");
            assertStringEq(FormatSignificantDigits("-0", 1), "-0");
            assertStringEq(FormatSignificantDigits("-0.0", 0), "-0.0");
            assertStringEq(FormatSignificantDigits("-0.0", 1), "-0");
            assertStringEq(FormatSignificantDigits("-1", 1), "-1");
            assertStringEq(FormatSignificantDigits("-1.", 1), "-1");
            assertStringEq(FormatSignificantDigits("-1.0", 1), "-1");
            assertStringEq(FormatSignificantDigits("-123", 0), "-123");
            assertStringEq(FormatSignificantDigits("-123.", 0), "-123.");
            assertStringEq(FormatSignificantDigits("-123.0", 0), "-123.0");
            assertStringEq(FormatSignificantDigits("-123", 2), "-123");
            assertStringEq(FormatSignificantDigits("-123", 3), "-123");
            assertStringEq(FormatSignificantDigits("-123.", 3), "-123");
            assertStringEq(FormatSignificantDigits("-123", 4), "-123");
            assertStringEq(FormatSignificantDigits("-123.5", 0), "-123.5");
            assertStringEq(FormatSignificantDigits("-123.5", 1), "-124");
            assertStringEq(FormatSignificantDigits("-123.5", 2), "-124");
            assertStringEq(FormatSignificantDigits("-123.5", 3), "-124");
            assertStringEq(FormatSignificantDigits("-123.5", 4), "-123.5");
            assertStringEq(FormatSignificantDigits("-123.456", 3), "-123");
            assertStringEq(FormatSignificantDigits("-123.449", 4), "-123.4");
            assertStringEq(FormatSignificantDigits("-123.450", 4), "-123.5");
            assertStringEq(FormatSignificantDigits("-123.456", 5), "-123.46");
            assertStringEq(FormatSignificantDigits("-123.456", 6), "-123.456");
            assertStringEq(FormatSignificantDigits("-123.456", 7), "-123.456");
            assertStringEq(FormatSignificantDigits("-123.456E+6", 3), "-123E+6");
            assertStringEq(FormatSignificantDigits("-123.456E+6", 4), "-123.5E+6");
            assertStringEq(FormatSignificantDigits("-123.456E+6", 6), "-123.456E+6");
            assertStringEq(FormatSignificantDigits("-123.456E-6", 7), "-123.456E-6");
            assertStringEq(FormatSignificantDigits("-1.2345678901234567890E-6", 7), "-1.234568E-6");
        }



        /// <summary>
        /// Formats a number (that is in string form) for a specified number of decimal places
        /// Adds trailing zeros or removes decomal places.
        /// Rounds the last digit of the result up if the first removed decimal place was 5 or more
        /// Leaves scientific notation alone
        /// see unit tests below for examples
        /// </summary>
        /// <param name="input"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        private string FormatFixedPoint(string input, int digits)
        {
            bool minus = false;
            if (digits < 0) goto done;  // just keep all decimal places regardless
            if (input.Length == 0) goto done;
            if (input[0] == '-')
            {
                minus = true;
                input = input.Substring(1, input.Length - 1);
            }
            int exponentPosition = input.IndexOf("E");
            if (exponentPosition != -1) goto done; // if we have an exponent, don't attempt to go to fixed point
            int pointPosition = input.IndexOf(".");
            if (pointPosition != -1)
            {
                input = FormatSignificantDigits(input, pointPosition + digits);  // trim off decimal places and do rounding
                // also may remove the decimal point
            }
            pointPosition = input.IndexOf(".");
            if (pointPosition == -1)
            {
                if (digits > 0)  // will be adding trailing zeros
                {
                    input += ".";  // if there is no decimal point, add one
                    pointPosition = input.Length - 1;
                }
            }
            while (input.Length - pointPosition - 1 < digits) input += "0";

            done:
            if (minus) input = "-" + input;
            return input;
        }




        private void TestFormatFixedPoint()
        {
            assertStringEq(FormatFixedPoint("0", 1), "0.0");
            assertStringEq(FormatFixedPoint("0.0", 0), "0");
            assertStringEq(FormatFixedPoint("0.0", 1), "0.0");
            assertStringEq(FormatFixedPoint("1", 1), "1.0");
            assertStringEq(FormatFixedPoint("123", 0), "123");
            assertStringEq(FormatFixedPoint("123", 2), "123.00");
            assertStringEq(FormatFixedPoint("123", 3), "123.000");
            assertStringEq(FormatFixedPoint("123", 4), "123.0000");
            assertStringEq(FormatFixedPoint("123.5", 0), "124");
            assertStringEq(FormatFixedPoint("123.5", 1), "123.5");
            assertStringEq(FormatFixedPoint("123.5", 2), "123.50");
            assertStringEq(FormatFixedPoint("123.456", 0), "123");
            assertStringEq(FormatFixedPoint("123.449", 1), "123.4");
            assertStringEq(FormatFixedPoint("123.450", 1), "123.5");
            assertStringEq(FormatFixedPoint("123.456", 2), "123.46");
            assertStringEq(FormatFixedPoint("123.456", 3), "123.456");
            assertStringEq(FormatFixedPoint("123.456", 4), "123.4560");
            assertStringEq(FormatFixedPoint("123.", 3), "123.000");
            assertStringEq(FormatFixedPoint("123.456E+6", 0), "123.456E+6");
            assertStringEq(FormatFixedPoint("0.012345678901234567890", 7), "0.0123457");
            assertStringEq(FormatFixedPoint("9.9999", 4), "9.9999");
            assertStringEq(FormatFixedPoint("9.999", 2), "10.00");
            assertStringEq(FormatFixedPoint("9.99", 0), "10");
            assertStringEq(FormatFixedPoint("999.99", 2), "999.99");
            assertStringEq(FormatFixedPoint("999.99", 1), "1000.0");
            assertStringEq(FormatFixedPoint("999.99", 0), "1000");
            // assertStringEq(FormatFixedPoint("00", 0), "0.");
            assertStringEq(FormatFixedPoint("-0", 1), "-0.0");
            assertStringEq(FormatFixedPoint("-0.0", 0), "-0");
            assertStringEq(FormatFixedPoint("-0.0", 1), "-0.0");
            assertStringEq(FormatFixedPoint("-1", 1), "-1.0");
            assertStringEq(FormatFixedPoint("-123", 0), "-123");
            assertStringEq(FormatFixedPoint("-123", 2), "-123.00");
            assertStringEq(FormatFixedPoint("-123", 3), "-123.000");
            assertStringEq(FormatFixedPoint("-123", 4), "-123.0000");
            assertStringEq(FormatFixedPoint("-123.5", 0), "-124");
            assertStringEq(FormatFixedPoint("-123.5", 1), "-123.5");
            assertStringEq(FormatFixedPoint("-123.5", 2), "-123.50");
            assertStringEq(FormatFixedPoint("-123.456", 0), "-123");
            assertStringEq(FormatFixedPoint("-123.449", 1), "-123.4");
            assertStringEq(FormatFixedPoint("-123.450", 1), "-123.5");
            assertStringEq(FormatFixedPoint("-123.456", 2), "-123.46");
            assertStringEq(FormatFixedPoint("-123.456", 3), "-123.456");
            assertStringEq(FormatFixedPoint("-123.456", 4), "-123.4560");
            assertStringEq(FormatFixedPoint("-123.", 3), "-123.000");
            assertStringEq(FormatFixedPoint("-123.456E+6", 0), "-123.456E+6");
            assertStringEq(FormatFixedPoint("-0.012345678901234567890", 7), "-0.0123457");
        }




        /// <summary>
        /// Formats a number (that is in string form) to scientific notation with the specified number of decimal places
        /// Adds trailing zeros or removes decimal places.
        /// Rounds the last digit of the result up if the first removed decimal place was 5 or more
        /// Works on numbers already in scientific notation as well
        /// if exponentMultiple is set to a positive value (usually 3), the exponent is made a multiple of 3
        /// and the mantissa has a greater dynamic range. 
        /// i.e. normal scientific, the mantissa range is:    1.0000... to   9.9999... 
        /// if the exponent is a multiple of 3, the range is: 1.0000... to 999.9999...
        /// if the mantissa is 0, it is a special case - the exponent is also set to 0. 
        /// see unit tests below for examples
        /// </summary>
        /// <param name="input"></param>
        /// <param name="digits"></param>
        /// <param name="digits"></param>
        /// <returns>"string"</returns>
        private string FormatScientific(string input, int digits, int exponentMultiple = 1)
        {
            bool minus = false;
            if (digits < 0) goto done;  // digits < 0 signals us to do no formatting
            // simplify out negative numbers
            if (input[0] == '-')
            {
                minus = true;
                input = input.Substring(1, input.Length - 1);
            }
            // split the mantissa and exponent
            string exponent = "E0";
            string mantissa = input;
            int exponentPosition = input.IndexOf("E");
            if (exponentPosition != -1)
            {
                mantissa = input.Substring(0, exponentPosition);
                exponent = input.Substring(exponentPosition, input.Length - exponentPosition);
            }
            int exponentInt;
            bool validExponent = int.TryParse(exponent.Remove(0, 1), out exponentInt);
            if (validExponent)
            {
                // remove the decimal point from the mantissa and adjust exponentInt as if its position is after the units digits
                int pointPosition = mantissa.IndexOf(".");
                if (pointPosition == -1)
                {
                    exponentInt += mantissa.Length - 1;
                }
                else
                {
                    mantissa = mantissa.Remove(pointPosition, 1);
                    exponentInt += pointPosition - 1;
                }

                // handle zero as a special case as it will always have exponent E0
                bool zero = true;
                for (int i = 0; i<mantissa.Length; i++)
                {
                    if (mantissa[i]!='0')
                    {
                        zero = false;
                        break;
                    }
                }
                if (zero)
                {
                    
                    mantissa = "0";
                    mantissa = FormatFixedPoint(mantissa, digits);  // add trailing zeros
                    input = mantissa + "E0";
                }
                else
                {

                    // remove leading zeros and adjust exponentInt accordingly
                    int i = 0;
                    while (i < mantissa.Length && mantissa[i] == '0') i++;
                    // if all zeros, leave alone, they will potentially be removed as trailing zeros
                    if (i > 0 && i < mantissa.Length)
                    {
                        mantissa = mantissa.Substring(i, mantissa.Length - i);
                        exponentInt -= i;
                    }

                    if (exponentMultiple <= 1)
                    {
                        // insert decimal point after the units digits 0 exponentInt has the correct value already for this position
                        mantissa = mantissa.Insert(1, ".");
                    }
                    else
                    {
                        // engineering mode
                        // shift the insert point position to get the exponent to the multiple we want
                        int newPointPosition = 1;
                        while (exponentInt % exponentMultiple != 0)
                        {
                            exponentInt--;
                            newPointPosition++;
                        }
                        while (mantissa.Length < newPointPosition) mantissa += "0";  // These are whole digits e.g. 1E2 will become 100E0
                        mantissa = mantissa.Insert(newPointPosition, ".");
                    }
                    mantissa = FormatFixedPoint(mantissa, digits);  // Add trailing zeros, or truncate decimal places with rounding

                    // FormatFixedPoint may have rounded from like 9.9 to 10.0 or 10
                    // If so we need to move the point again
                    pointPosition = mantissa.IndexOf(".");
                    if (pointPosition == -1)
                    {
                        pointPosition = mantissa.Length;
                    }
                    else
                    {
                        if (pointPosition > exponentMultiple) mantissa = mantissa.Remove(pointPosition, 1);
                    }
                    if (pointPosition > exponentMultiple)
                    {
                        pointPosition -= exponentMultiple;
                        exponentInt += exponentMultiple;
                        mantissa = mantissa.Insert(pointPosition, ".");
                        mantissa = FormatFixedPoint(mantissa, digits);  // might remove trailing zeros, but can't round again
                    }
                    input = mantissa + "E" + exponentInt;
                }

            }
            else
            {
                goto done;
            }


        done:
            if (minus) input = "-" + input;
            return input;
        }





        private void TestFormatScientific()
        {
            assertStringEq(FormatScientific("0", 1), "0.0E0");
            assertStringEq(FormatScientific("0.0", 0), "0E0");
            assertStringEq(FormatScientific("0.0", 1), "0.0E0");
            assertStringEq(FormatScientific("1", 0), "1E0");
            assertStringEq(FormatScientific("1", 1), "1.0E0");
            assertStringEq(FormatScientific("1", 2), "1.00E0");
            assertStringEq(FormatScientific("1", 12), "1.000000000000E0");
            assertStringEq(FormatScientific("123", 0), "1E2");
            assertStringEq(FormatScientific("123", 2), "1.23E2");
            assertStringEq(FormatScientific("123", 3), "1.230E2");
            assertStringEq(FormatScientific("123", 4), "1.2300E2");
            assertStringEq(FormatScientific("123.5", 0), "1E2");
            assertStringEq(FormatScientific("123.5", 2), "1.24E2");
            assertStringEq(FormatScientific("123.5", 3), "1.235E2");
            assertStringEq(FormatScientific("123.5", 4), "1.2350E2");
            assertStringEq(FormatScientific("153.5", 0), "2E2");
            assertStringEq(FormatScientific("0.01235", 2), "1.24E-2");
            assertStringEq(FormatScientific("0.01235", 4), "1.2350E-2");
            assertStringEq(FormatScientific("0.0000000000000000123456", 2), "1.23E-17");
            assertStringEq(FormatScientific("123456789101234567890.123", 3), "1.235E20");
            assertStringEq(FormatScientific("0.0E1", 0), "0E0");
            assertStringEq(FormatScientific("0.0E-1", 1), "0.0E0");
            assertStringEq(FormatScientific("123.456E6", 0), "1E8");
            assertStringEq(FormatScientific("123.456E-6", 1), "1.2E-4");
            assertStringEq(FormatScientific("123.456E+6", 3), "1.235E8");
            assertStringEq(FormatScientific("123.456E6", 6), "1.234560E8");
            assertStringEq(FormatScientific("1.2345678901234567890E-6", 7), "1.2345679E-6");
            assertStringEq(FormatScientific("9.99", 1), "1.0E1");
            // assertStringEq(FormatScientific("00", 0), "0.E0");
            assertStringEq(FormatScientific("-0", 1), "-0.0E0");
            assertStringEq(FormatScientific("-0.0", 0), "-0E0");
            assertStringEq(FormatScientific("-0.0", 1), "-0.0E0");
            assertStringEq(FormatScientific("-1", 0), "-1E0");
            assertStringEq(FormatScientific("-1", 1), "-1.0E0");
            assertStringEq(FormatScientific("-1", 2), "-1.00E0");
            assertStringEq(FormatScientific("-1", 12), "-1.000000000000E0");
            assertStringEq(FormatScientific("-123", 0), "-1E2");
            assertStringEq(FormatScientific("-123", 2), "-1.23E2");
            assertStringEq(FormatScientific("-123", 3), "-1.230E2");
            assertStringEq(FormatScientific("-123", 4), "-1.2300E2");
            assertStringEq(FormatScientific("-123.5", 0), "-1E2");
            assertStringEq(FormatScientific("-123.5", 2), "-1.24E2");
            assertStringEq(FormatScientific("-123.5", 3), "-1.235E2");
            assertStringEq(FormatScientific("-123.5", 4), "-1.2350E2");
            assertStringEq(FormatScientific("-153.5", 0), "-2E2");
            assertStringEq(FormatScientific("-0.01235", 2), "-1.24E-2");
            assertStringEq(FormatScientific("-0.01235", 4), "-1.2350E-2");
            assertStringEq(FormatScientific("-0.0000000000000000123456", 2), "-1.23E-17");
            assertStringEq(FormatScientific("-123456789101234567890.123", 3), "-1.235E20");
            assertStringEq(FormatScientific("-0.0E1", 0), "-0E0");
            assertStringEq(FormatScientific("-0.0E-1", 1), "-0.0E0");
            assertStringEq(FormatScientific("-123.456E6", 0), "-1E8");
            assertStringEq(FormatScientific("-123.456E-6", 1), "-1.2E-4");
            assertStringEq(FormatScientific("-123.456E+6", 3), "-1.235E8");
            assertStringEq(FormatScientific("-123.456E6", 6), "-1.234560E8");
            assertStringEq(FormatScientific("-1.2345678901234567890E-6", 7), "-1.2345679E-6");
        }





        private void TestFormatEngineering()
        {
            assertStringEq(FormatScientific("0E1", 1, 3), "0.0E0");
            assertStringEq(FormatScientific("0.0E+4", 0, 3), "0E0");
            assertStringEq(FormatScientific("0.0E-4", 1, 3), "0.0E0");
            assertStringEq(FormatScientific("1.2345", 1, 3), "1.2E0");
            assertStringEq(FormatScientific("12.345", 1, 3), "12.3E0");
            assertStringEq(FormatScientific("123.45", 1, 3), "123.5E0");
            assertStringEq(FormatScientific("1234.5", 1, 3), "1.2E3");
            assertStringEq(FormatScientific("12345", 1, 3), "12.3E3");
            assertStringEq(FormatScientific("123450", 1, 3), "123.5E3");
            assertStringEq(FormatScientific("1234500", 1, 3),"1.2E6");
            assertStringEq(FormatScientific("99.99", 1, 3), "100.0E0");
            assertStringEq(FormatScientific("99999", 1, 3), "100.0E3");
            assertStringEq(FormatScientific("-0E1", 1, 3), "-0.0E0");
            assertStringEq(FormatScientific("-0.0E+4", 0, 3), "-0E0");
            assertStringEq(FormatScientific("-0.0E-4", 1, 3), "-0.0E0");
            assertStringEq(FormatScientific("-1.2345", 1, 3), "-1.2E0");
            assertStringEq(FormatScientific("-12.345", 1, 3), "-12.3E0");
            assertStringEq(FormatScientific("-123.45", 1, 3), "-123.5E0");
            assertStringEq(FormatScientific("-1234.5", 1, 3), "-1.2E3");
            assertStringEq(FormatScientific("-12345", 1, 3), "-12.3E3");
            assertStringEq(FormatScientific("-123450", 1, 3), "-123.5E3");
            assertStringEq(FormatScientific("-1234500", 1, 3), "-1.2E6");
            assertStringEq(FormatScientific("-99.99", 1, 3), "-100.0E0");
            assertStringEq(FormatScientific("-99999", 1, 3), "-100.0E3");
        }





        private void assertStringEq(string a, string b)
        {
            if (a != b) throw new Exception($"Failed: {a} should equal {b}");
        }


    }
}
